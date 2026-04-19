# MediaWiki Scrape Report

- API: `https://supersnail.wiki.gg/api.php`
- Namespaces requested: `0`
- Pages indexed: **4125**
- Pages fetched: **4125**
- Likely item pages: **3508**
- Unique categories: **562**
- Cargo enabled: **no**

## Top Categories

- Relics: 2294
- Green Relics: 731
- Orange Relics: 728
- FAME Relics: 465
- TECH Relics: 427
- Purple Relics: 418
- CIV Relics: 398
- Pages with broken file links: 395
- ART Relics: 373
- Blue Relics: 355
- ATK Boost: 326
- FAITH Relics: 315
- Clones: 226
- Self: 222
- HP Boost: 211
- DEF Boost: 206
- Green FAME Relics: 190
- RUSH Boost: 186
- Green TECH Relics: 155
- Orange FAME Relics: 140

## Top Infobox Templates

- CargoRelic: 2088
- CargoRelicResonance: 1142
- gearrow: 422
- RewardIconWithGearlink: 215
- Miniongearrow: 87
- Gearlink: 55
- CargoRelicSkills: 18
- #cargo_query:table=Relics: 8
- IconExampleRowWithGearlink: 4
- SupremeRelic: 1
- Relic: 1
- #cargo_query:table=Relic_Resonance: 1

## Output Files

- `siteinfo.json`
- `page_index.json`
- `pages.ndjson`
- `pages.json`
- `allcategories.json`
- `summary.json`

## Notes

- Raw wikitext was saved for each page for lossless downstream parsing.
- Markdown files are lightweight approximations intended for LLM/RAG use, not faithful page renders.
- Item stats are heuristic unless you provide a known Cargo table and field list.
