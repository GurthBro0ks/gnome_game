#!/usr/bin/env python3
"""
Bulk MediaWiki scraper for supersnail.wiki.gg-like wikis.

What it does:
- Enumerates pages through the MediaWiki Action API (list=allpages)
- Fetches raw wikitext, page metadata, and categories (prop=revisions|categories|info)
- Saves newline-delimited JSON plus per-page raw wikitext files
- Produces a light Markdown-ish text version for downstream LLM use
- Heuristically extracts infobox/template stats for likely item pages
- Writes a scrape summary report

Notes:
- This is intentionally API-first. It does not scrape the frontend unless you extend it.
- Cargo support on many wiki.gg sites can be reached through action=cargoquery,
  but table names are wiki-specific. This script keeps Cargo optional.
"""

from __future__ import annotations

import argparse
import json
import os
import re
import sys
import time
from collections import Counter, defaultdict
from dataclasses import dataclass
from pathlib import Path
from typing import Dict, Iterable, Iterator, List, Optional, Tuple
from urllib.parse import quote

try:
    import requests
except ImportError as exc:
    raise SystemExit("Missing dependency: requests. Install with: pip install requests") from exc

try:
    import mwparserfromhell  # type: ignore
except ImportError:
    mwparserfromhell = None

DEFAULT_API = "https://supersnail.wiki.gg/api.php"
DEFAULT_NAMESPACES = [0]
DEFAULT_BATCH_SIZE = 20
DEFAULT_SLEEP = 0.15
DEFAULT_TIMEOUT = 60
DEFAULT_USER_AGENT = (
    "SupersnailWikiScraper/1.0 (+https://example.invalid; contact=replace-me)"
)
COMMON_INFOBOX_HINTS = (
    "infobox",
    "item",
    "artifact",
    "gear",
    "equipment",
    "relic",
    "companion",
    "character",
    "enemy",
    "consumable",
    "material",
)
LIKELY_ITEM_CATEGORY_HINTS = (
    "item",
    "artifact",
    "gear",
    "equipment",
    "relic",
    "materials",
    "consumable",
    "weapon",
    "armor",
)


@dataclass
class Config:
    api: str
    output_dir: Path
    namespaces: List[int]
    batch_size: int
    sleep_seconds: float
    timeout: int
    user_agent: str
    max_pages: Optional[int]
    include_redirects: bool
    save_wikitext_files: bool
    save_markdown_files: bool
    cargo_table: Optional[str]
    cargo_fields: Optional[str]


class MediaWikiClient:
    def __init__(self, api: str, user_agent: str, timeout: int = DEFAULT_TIMEOUT):
        self.api = api
        self.timeout = timeout
        self.session = requests.Session()
        self.session.headers.update({"User-Agent": user_agent, "Accept": "application/json"})

    def request(self, params: Dict[str, object], method: str = "GET") -> Dict[str, object]:
        merged = {"format": "json", "formatversion": 2, **params}
        for attempt in range(5):
            try:
                if method.upper() == "POST":
                    resp = self.session.post(self.api, data=merged, timeout=self.timeout)
                else:
                    resp = self.session.get(self.api, params=merged, timeout=self.timeout)
                if resp.status_code in (429, 500, 502, 503, 504):
                    raise requests.HTTPError(f"HTTP {resp.status_code}", response=resp)
                resp.raise_for_status()
                data = resp.json()
                if "error" in data:
                    raise RuntimeError(f"API error: {data['error']}")
                return data
            except Exception:
                if attempt == 4:
                    raise
                time.sleep((2 ** attempt) * 0.8)
        raise RuntimeError("Unreachable")

    def get_siteinfo(self) -> Dict[str, object]:
        return self.request(
            {
                "action": "query",
                "meta": "siteinfo",
                "siprop": "general|namespaces|namespacealiases",
            }
        )

    def iter_allpages(self, namespace: int, include_redirects: bool = True) -> Iterator[Dict[str, object]]:
        cont: Optional[str] = None
        while True:
            params: Dict[str, object] = {
                "action": "query",
                "list": "allpages",
                "apnamespace": namespace,
                "aplimit": "max",
            }
            if not include_redirects:
                params["apfilterredir"] = "nonredirects"
            if cont:
                params["apcontinue"] = cont
            data = self.request(params)
            for page in data.get("query", {}).get("allpages", []):
                yield page
            cont = data.get("continue", {}).get("apcontinue")
            if not cont:
                break

    def fetch_page_bundle(self, titles: List[str]) -> List[Dict[str, object]]:
        if not titles:
            return []
        params = {
            "action": "query",
            "prop": "revisions|categories|info",
            "titles": "|".join(titles),
            "redirects": 1,
            "inprop": "url",
            "cllimit": "max",
            "rvslots": "main",
            "rvprop": "ids|timestamp|sha1|size|content",
        }
        data = self.request(params, method="POST")
        return data.get("query", {}).get("pages", [])

    def iter_allcategories(self) -> Iterator[Dict[str, object]]:
        cont: Optional[str] = None
        while True:
            params: Dict[str, object] = {
                "action": "query",
                "list": "allcategories",
                "aclimit": "max",
            }
            if cont:
                params["accontinue"] = cont
            data = self.request(params)
            for cat in data.get("query", {}).get("allcategories", []):
                yield cat
            cont = data.get("continue", {}).get("accontinue")
            if not cont:
                break

    def cargo_query(self, table: str, fields: str, where: Optional[str] = None, limit: int = 500) -> List[Dict[str, object]]:
        params: Dict[str, object] = {
            "action": "cargoquery",
            "tables": table,
            "fields": fields,
            "limit": str(limit),
        }
        if where:
            params["where"] = where
        data = self.request(params)
        return data.get("cargoquery", [])


# ---------- Text processing ----------

COMMENT_RE = re.compile(r"<!--.*?-->", flags=re.DOTALL)
REF_RE = re.compile(r"<ref\b[^>/]*?>.*?</ref>|<ref\b[^>]*/>", flags=re.DOTALL | re.IGNORECASE)
FILE_LINK_RE = re.compile(r"\[\[(?:File|Image):[^\]]+\]\]", flags=re.IGNORECASE)
CATEGORY_LINK_RE = re.compile(r"\[\[Category:([^\]|]+)(?:\|[^\]]*)?\]\]", flags=re.IGNORECASE)
INTERNAL_LINK_RE = re.compile(r"\[\[([^\]|]+)\|([^\]]+)\]\]|\[\[([^\]]+)\]\]")
EXTERNAL_LINK_RE = re.compile(r"\[(https?://[^\s\]]+)\s+([^\]]+)\]")
HEADING_RE = re.compile(r"^(={2,6})\s*(.*?)\s*\1\s*$", flags=re.MULTILINE)
TEMPLATE_BLOCK_RE = re.compile(r"\{\{.*?\}\}", flags=re.DOTALL)
TABLE_BLOCK_RE = re.compile(r"\{\|.*?\|\}", flags=re.DOTALL)


def slugify(title: str) -> str:
    slug = re.sub(r"[^A-Za-z0-9._-]+", "_", title).strip("_")
    return slug[:180] or "untitled"


def strip_wiki_markup_to_markdown(wikitext: str) -> str:
    text = COMMENT_RE.sub("", wikitext)
    text = REF_RE.sub("", text)
    text = FILE_LINK_RE.sub("", text)

    def heading_repl(match: re.Match[str]) -> str:
        level = max(1, min(6, len(match.group(1)) - 1))
        return f"{'#' * level} {match.group(2).strip()}"

    text = HEADING_RE.sub(heading_repl, text)

    def internal_link_repl(match: re.Match[str]) -> str:
        if match.group(2):
            return match.group(2)
        return match.group(3) or match.group(1) or ""

    text = INTERNAL_LINK_RE.sub(internal_link_repl, text)
    text = EXTERNAL_LINK_RE.sub(lambda m: m.group(2), text)
    text = CATEGORY_LINK_RE.sub("", text)
    text = TABLE_BLOCK_RE.sub("\n\n[WIKITABLE OMITTED]\n\n", text)
    text = TEMPLATE_BLOCK_RE.sub("", text)
    text = re.sub(r"'''(.*?)'''", r"**\1**", text)
    text = re.sub(r"''(.*?)''", r"*\1*", text)
    text = re.sub(r"^\*", "-", text, flags=re.MULTILINE)
    text = re.sub(r"^#+", "1.", text, flags=re.MULTILINE)
    text = re.sub(r"\n{3,}", "\n\n", text)
    return text.strip()


def extract_categories_from_wikitext(wikitext: str) -> List[str]:
    return sorted({m.group(1).strip() for m in CATEGORY_LINK_RE.finditer(wikitext)})


def parse_infobox_templates(title: str, wikitext: str) -> List[Dict[str, object]]:
    results: List[Dict[str, object]] = []

    if mwparserfromhell is not None:
        try:
            code = mwparserfromhell.parse(wikitext)
            for template in code.filter_templates(recursive=True):
                name = str(template.name).strip()
                lowered = name.lower()
                if not any(hint in lowered for hint in COMMON_INFOBOX_HINTS):
                    continue
                params: Dict[str, str] = {}
                for p in template.params:
                    key = str(p.name).strip()
                    value = str(p.value).strip()
                    if key:
                        params[key] = value
                if params:
                    results.append({"template": name, "params": params})
        except Exception:
            pass

    if results:
        return results

    # fallback heuristic: only captures top-level-ish templates reasonably well
    for block in re.findall(r"\{\{([^{}]{0,8000})\}\}", wikitext, flags=re.DOTALL):
        lines = [line.strip() for line in block.splitlines() if line.strip()]
        if not lines:
            continue
        head = lines[0].lstrip("{").strip()
        lowered = head.lower()
        if not any(hint in lowered for hint in COMMON_INFOBOX_HINTS):
            continue
        params: Dict[str, str] = {}
        for line in lines[1:]:
            if line.startswith("|") and "=" in line:
                key, value = line[1:].split("=", 1)
                params[key.strip()] = value.strip()
        if params:
            results.append({"template": head, "params": params})
    return results


def simplify_infoboxes(infoboxes: List[Dict[str, object]]) -> List[Dict[str, object]]:
    cleaned: List[Dict[str, object]] = []
    for box in infoboxes:
        params = box.get("params", {})
        if not isinstance(params, dict):
            continue
        useful = {}
        for k, v in params.items():
            key = str(k).strip()
            val = str(v).strip()
            if not key or not val:
                continue
            if len(val) > 500:
                val = val[:500] + " …"
            useful[key] = val
        if useful:
            cleaned.append({"template": box.get("template"), "params": useful})
    return cleaned


def likely_item_page(title: str, categories: List[str], infoboxes: List[Dict[str, object]]) -> bool:
    lowered_title = title.lower()
    lowered_cats = [c.lower() for c in categories]
    if any(h in lowered_title for h in ("item", "artifact", "relic", "gear", "weapon", "armor")):
        return True
    if any(any(h in c for h in LIKELY_ITEM_CATEGORY_HINTS) for c in lowered_cats):
        return True
    for box in infoboxes:
        t = str(box.get("template", "")).lower()
        if any(h in t for h in COMMON_INFOBOX_HINTS):
            return True
    return False


def chunked(items: List[str], size: int) -> Iterator[List[str]]:
    for i in range(0, len(items), size):
        yield items[i : i + size]


def write_json(path: Path, obj: object) -> None:
    path.write_text(json.dumps(obj, ensure_ascii=False, indent=2), encoding="utf-8")


def write_ndjson(path: Path, rows: Iterable[Dict[str, object]]) -> None:
    with path.open("w", encoding="utf-8") as f:
        for row in rows:
            f.write(json.dumps(row, ensure_ascii=False) + "\n")


def fetch_and_save(cfg: Config) -> int:
    client = MediaWikiClient(cfg.api, cfg.user_agent, timeout=cfg.timeout)
    cfg.output_dir.mkdir(parents=True, exist_ok=True)
    raw_dir = cfg.output_dir / "wikitext"
    md_dir = cfg.output_dir / "markdown"
    pages_dir = cfg.output_dir / "pages"
    raw_dir.mkdir(exist_ok=True)
    md_dir.mkdir(exist_ok=True)
    pages_dir.mkdir(exist_ok=True)

    siteinfo = client.get_siteinfo()
    write_json(cfg.output_dir / "siteinfo.json", siteinfo)

    all_page_stubs: List[Dict[str, object]] = []
    for ns in cfg.namespaces:
        for stub in client.iter_allpages(ns, include_redirects=cfg.include_redirects):
            stub["namespace_requested"] = ns
            all_page_stubs.append(stub)
            if cfg.max_pages and len(all_page_stubs) >= cfg.max_pages:
                break
        if cfg.max_pages and len(all_page_stubs) >= cfg.max_pages:
            break

    write_json(cfg.output_dir / "page_index.json", all_page_stubs)

    titles = [str(p["title"]) for p in all_page_stubs if p.get("title")]
    scraped_rows: List[Dict[str, object]] = []
    category_counter: Counter[str] = Counter()
    template_counter: Counter[str] = Counter()
    namespace_counter: Counter[int] = Counter()
    likely_items = 0

    for batch in chunked(titles, cfg.batch_size):
        pages = client.fetch_page_bundle(batch)
        for page in pages:
            title = str(page.get("title", ""))
            ns = int(page.get("ns", 0))
            namespace_counter[ns] += 1

            revisions = page.get("revisions", []) or []
            revision = revisions[0] if revisions else {}
            slots = revision.get("slots", {}) if isinstance(revision, dict) else {}
            main_slot = slots.get("main", {}) if isinstance(slots, dict) else {}
            wikitext = str(main_slot.get("content", "")) if isinstance(main_slot, dict) else ""

            api_categories = [c.get("title", "") for c in (page.get("categories", []) or []) if c.get("title")]
            clean_api_categories = [c.removeprefix("Category:") for c in api_categories]
            text_categories = extract_categories_from_wikitext(wikitext)
            categories = sorted({*clean_api_categories, *text_categories})
            category_counter.update(categories)

            infoboxes = simplify_infoboxes(parse_infobox_templates(title, wikitext))
            for box in infoboxes:
                template_counter[str(box.get("template", ""))] += 1

            md_text = strip_wiki_markup_to_markdown(wikitext)
            is_likely_item = likely_item_page(title, categories, infoboxes)
            if is_likely_item:
                likely_items += 1

            row: Dict[str, object] = {
                "pageid": page.get("pageid"),
                "ns": ns,
                "title": title,
                "fullurl": page.get("fullurl"),
                "touched": page.get("touched"),
                "length": page.get("length"),
                "lastrevid": page.get("lastrevid"),
                "revision": {
                    "revid": revision.get("revid"),
                    "parentid": revision.get("parentid"),
                    "timestamp": revision.get("timestamp"),
                    "sha1": revision.get("sha1"),
                    "size": revision.get("size"),
                },
                "categories": categories,
                "likely_item_page": is_likely_item,
                "infoboxes": infoboxes,
                "wikitext_path": None,
                "markdown_path": None,
            }

            slug = slugify(title)
            if cfg.save_wikitext_files:
                raw_path = raw_dir / f"{slug}.wiki"
                raw_path.write_text(wikitext, encoding="utf-8")
                row["wikitext_path"] = str(raw_path.relative_to(cfg.output_dir))
            if cfg.save_markdown_files:
                md_path = md_dir / f"{slug}.md"
                md_path.write_text(md_text, encoding="utf-8")
                row["markdown_path"] = str(md_path.relative_to(cfg.output_dir))

            write_json(pages_dir / f"{slug}.json", row)
            scraped_rows.append(row)
        time.sleep(cfg.sleep_seconds)

    write_ndjson(cfg.output_dir / "pages.ndjson", scraped_rows)
    write_json(cfg.output_dir / "pages.json", scraped_rows)

    categories_dump = list(client.iter_allcategories())
    write_json(cfg.output_dir / "allcategories.json", categories_dump)

    cargo_rows: List[Dict[str, object]] = []
    cargo_error: Optional[str] = None
    if cfg.cargo_table and cfg.cargo_fields:
        try:
            cargo_rows = client.cargo_query(cfg.cargo_table, cfg.cargo_fields, limit=500)
            write_json(cfg.output_dir / "cargoquery.json", cargo_rows)
        except Exception as exc:
            cargo_error = str(exc)
            write_json(cfg.output_dir / "cargoquery_error.json", {"error": cargo_error})

    summary = {
        "api": cfg.api,
        "namespaces": cfg.namespaces,
        "pages_indexed": len(all_page_stubs),
        "pages_fetched": len(scraped_rows),
        "likely_item_pages": likely_items,
        "unique_categories": len(category_counter),
        "top_categories": category_counter.most_common(50),
        "top_infobox_templates": template_counter.most_common(25),
        "namespaces_seen": dict(namespace_counter),
        "cargo_enabled": bool(cfg.cargo_table and cfg.cargo_fields),
        "cargo_rows": len(cargo_rows),
        "cargo_error": cargo_error,
    }
    write_json(cfg.output_dir / "summary.json", summary)

    report_lines = [
        "# MediaWiki Scrape Report",
        "",
        f"- API: `{cfg.api}`",
        f"- Namespaces requested: `{', '.join(map(str, cfg.namespaces))}`",
        f"- Pages indexed: **{len(all_page_stubs)}**",
        f"- Pages fetched: **{len(scraped_rows)}**",
        f"- Likely item pages: **{likely_items}**",
        f"- Unique categories: **{len(category_counter)}**",
        f"- Cargo enabled: **{'yes' if cfg.cargo_table and cfg.cargo_fields else 'no'}**",
        "",
        "## Top Categories",
        "",
    ]
    for name, count in category_counter.most_common(20):
        report_lines.append(f"- {name}: {count}")

    report_lines.extend(["", "## Top Infobox Templates", ""])
    if template_counter:
        for name, count in template_counter.most_common(20):
            report_lines.append(f"- {name}: {count}")
    else:
        report_lines.append("- No infobox-like templates were detected by the current heuristic.")

    report_lines.extend(["", "## Output Files", ""])
    for rel in [
        "siteinfo.json",
        "page_index.json",
        "pages.ndjson",
        "pages.json",
        "allcategories.json",
        "summary.json",
    ]:
        report_lines.append(f"- `{rel}`")
    if cfg.cargo_table and cfg.cargo_fields:
        report_lines.append("- `cargoquery.json` or `cargoquery_error.json`")

    report_lines.extend(["", "## Notes", ""])
    report_lines.append("- Raw wikitext was saved for each page for lossless downstream parsing.")
    report_lines.append("- Markdown files are lightweight approximations intended for LLM/RAG use, not faithful page renders.")
    report_lines.append("- Item stats are heuristic unless you provide a known Cargo table and field list.")
    if cargo_error:
        report_lines.append(f"- Cargo query error: `{cargo_error}`")

    (cfg.output_dir / "REPORT.md").write_text("\n".join(report_lines) + "\n", encoding="utf-8")
    return len(scraped_rows)


def parse_args(argv: Optional[List[str]] = None) -> Config:
    parser = argparse.ArgumentParser(description="Bulk scrape a MediaWiki API into machine-friendly files.")
    parser.add_argument("--api", default=DEFAULT_API, help="MediaWiki API endpoint")
    parser.add_argument("--output-dir", default="./supersnail_wiki_dump", help="Output directory")
    parser.add_argument(
        "--namespaces",
        default=",".join(map(str, DEFAULT_NAMESPACES)),
        help="Comma-separated namespace IDs, e.g. 0,10,14",
    )
    parser.add_argument("--batch-size", type=int, default=DEFAULT_BATCH_SIZE)
    parser.add_argument("--sleep-seconds", type=float, default=DEFAULT_SLEEP)
    parser.add_argument("--timeout", type=int, default=DEFAULT_TIMEOUT)
    parser.add_argument("--user-agent", default=os.getenv("MW_SCRAPER_USER_AGENT", DEFAULT_USER_AGENT))
    parser.add_argument("--max-pages", type=int, default=None)
    parser.add_argument("--include-redirects", action="store_true")
    parser.add_argument("--no-wikitext-files", action="store_true")
    parser.add_argument("--no-markdown-files", action="store_true")
    parser.add_argument("--cargo-table", default=None, help="Optional Cargo table name")
    parser.add_argument("--cargo-fields", default=None, help="Optional Cargo fields, e.g. _pageName=Page,Name,Rarity,Type")
    args = parser.parse_args(argv)

    namespaces = [int(x.strip()) for x in args.namespaces.split(",") if x.strip()]
    return Config(
        api=args.api,
        output_dir=Path(args.output_dir),
        namespaces=namespaces,
        batch_size=args.batch_size,
        sleep_seconds=args.sleep_seconds,
        timeout=args.timeout,
        user_agent=args.user_agent,
        max_pages=args.max_pages,
        include_redirects=args.include_redirects,
        save_wikitext_files=not args.no_wikitext_files,
        save_markdown_files=not args.no_markdown_files,
        cargo_table=args.cargo_table,
        cargo_fields=args.cargo_fields,
    )


def main(argv: Optional[List[str]] = None) -> int:
    cfg = parse_args(argv)
    try:
        count = fetch_and_save(cfg)
    except KeyboardInterrupt:
        print("Interrupted.", file=sys.stderr)
        return 130
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1

    print(f"Done. Scraped {count} pages into {cfg.output_dir}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
