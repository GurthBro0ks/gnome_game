# Phase 2A Salvage Report

**Status:** Completed — Phase 2A Salvage Rewrite  
**Version:** 0.1.0  
**Date:** 2026-04-19  
**Author:** Gemini (drafting executor, Phase 2A salvage pass)

---

## Purpose

This document is the controlling audit trail for the Phase 2A Salvage Repair. It records what was wrong, what canon lock was applied, what was rewritten, what was created, and what remains as known debt for Phase 2B or later.

Future models should read this document before touching any Phase 2A deliverable.

---

## Executive Summary

The original Phase 2A pass (commit `0caaeb6`) placed six files and marked them complete. Inspection reveals all six are near-empty placeholders with no Gnome Survivor canon content:

| File | Problem |
|------|---------|
| `data/economy/economy_model_v1_sheet.csv` | Generic fantasy currencies (Aurum, Silver, Iron, Mithril, etc.). No Gnome Survivor resources. 18 rows of irrelevant data. |
| `docs/04_systems/data_schema_v1.md` | Generic RPG entities (Currency, Quest, NPC, Spell, Guild, Dialogue). No Gnome Survivor state model. |
| `docs/04_systems/core_loop_mvp_spec.md` | Five-line stub. No Burrow, Loamwake, Fixtures, Rootrail, Confidants, Burrowfolk, or proof-of-fun. |
| `docs/06_content/content_table_templates.md` | 15 generic template names (LootTable_Tier1, Region_Weather, etc.). No columns defined. |
| `docs/07_ui/unlock_flow_and_ui_map.md` | 4-line stub. Generic Tier 1–5 progression with HUD/Inventory/Quest Log. No Gnome Survivor modules. |
| `docs/08_production/backend_architecture_options.md` | Three-line stub recommending microservices. No evaluation, no prototype path, no Gnome Survivor context. |

The `feature_list.json` records `prod-002` through `prod-006` as `"passes": false`, which is correct — they were never actually completed.

None of these files would be usable by a developer, content designer, or future AI specialist without complete replacement.

---

## Current Problem

The repo also carries several **obsolete canon assumptions** in its prior draft content and in the structure of the placeholders:

1. **Clothes** — The original plan treated Clothes as the gear/equipment system. Clothes are removed.  
2. **The Ascender** — The original plan described The Ascender as the long-project travel/unlock system. Ascender is removed.  
3. **Rigid gear slots** — The placeholder schema structure implies fixed equipment slots. Fixtures have no rigid slots.  
4. **War gear merged with personal gear** — No separation between personal Fixtures and War Armory existed.  
5. **Forgotten Manuals merged with Elder Books** — No distinction was drawn between permanent knowledge unlocks and ritual/key items.  
6. **Ascender Parts** — Referenced as a long-project material. Replaced by Rootrail Parts + Forgotten Manuals.

These assumptions would corrupt any implementation that relied on the old placeholders.

---

## Canon Lock Applied

The following canon rules are applied throughout all Phase 2A salvage rewrites. These override any conflicting prior content.

### Fixtures
- Clothes are removed. Every equipable piece is a **Fixture**.
- Each equipable piece is also called a Fixture.
- No rigid slots. Duplicate-style builds allowed.
- No negative stats. Tradeoffs are contextual, not punitive.
- Personal Fixture cap starts at **0** and grows to **12**.
- Fixture categories: **Farming**, **Pure Attack**, **Strain**, **War**.

### Hats
- Hats are a special Fixture type but are **NOT** part of the 12 normal Fixtures.
- Only one hat is **visible** at a time.
- Every unlocked hat grants a **tiny permanent passive bonus** (all stack).
- Hats are **identity/status/social items first**, stat items second.
- Model hats closer to shell-style permanent unlock bonuses than normal gear slots.

### War Armory
- War Fixtures live in a **separate War Armory**.
- They are **not** part of the 12 personal Fixtures.
- They mix war-specific traits with normal-style traits.

### Rootrail
- **Ascender is removed/replaced.** New system name: **Rootrail**.
- Rootrail is an old forgotten train functioning as: restoration project, travel/unlock system, passive account bonus system, route hub.
- Each repair step uses: **Rootrail Parts**, **Forgotten Manuals**, a **timer**.
- Timer gets longer with each installed part.
- Rootrail unlocks: new routes, rare materials, new Fixture recipes, account bonuses.

### Forgotten Manuals / Books
- **Forgotten Manuals** are separate from **Elder Books**.
- Forgotten Manuals are **permanent knowledge unlocks** tracked in a **codex/logbook**.
- They are **not consumable**.

### Other
- **Bronze Shovels** stays Bronze Shovels.
- Temporary placeholder first-set Confidant/Burrowfolk content is allowed for MVP planning.
- Final friend-questionnaire replacements come later.

---

## Files Rewritten (Phase 2A Salvage)

| File | Action | Notes |
|------|--------|-------|
| `data/economy/economy_model_v1_sheet.csv` | Full rewrite | All generic rows replaced with 19 canonical resources |
| `docs/04_systems/data_schema_v1.md` | Full rewrite | Replaced generic RPG entities with full Gnome Survivor schema |
| `docs/04_systems/core_loop_mvp_spec.md` | Full rewrite | Loamwake MVP slice, first 10/60 min, Fixture onboarding, Rootrail reveal |
| `docs/06_content/content_table_templates.md` | Full rewrite | 20+ content type templates with required/optional columns and example rows |
| `docs/07_ui/unlock_flow_and_ui_map.md` | Full rewrite | Concrete unlock timing for Loamwake MVP, red-dot rules, stub treatment |
| `docs/08_production/backend_architecture_options.md` | Full rewrite | Staged prototype→live-service options with recommended path |
| `CHANGELOG.md` | Appended | Added v0.2.1 salvage patch entry; original v0.2.0 preserved |

---

## Files Created (Phase 2A Salvage)

| File | Purpose |
|------|---------|
| `docs/08_production/phase2a_salvage_report.md` | This document — audit trail and canon lock |
| `docs/04_systems/fixtures_and_hats_system_v1.md` | Canon system spec for Fixtures, Hats, and War Armory |
| `docs/04_systems/rootrail_system_v1.md` | Canon system spec for Rootrail |
| `docs/08_production/specialist_prompt_phase2b.md` | Executable prompt directing the Phase 2B specialist |

---

## Canon Delta Map

| Old Assumption | Replacement | Status |
|----------------|-------------|--------|
| Clothes are the gear system | Fixtures replace Clothes entirely | Applied |
| Hats are normal gear slots | Hats are shell-style permanent unlock/display | Applied |
| The Ascender is the long-project system | Rootrail replaces The Ascender | Applied |
| No war/personal gear split | War Fixtures live in separate War Armory | Applied |
| Rigid equipment slots | No rigid slots; 0→12 personal cap | Applied |
| Ascender Parts as long-project material | Rootrail Parts + Forgotten Manuals | Applied |
| Forgotten Manuals merge with Elder Books | Kept separate; different role | Applied |
| Bronze Shovels might be renamed | Bronze Shovels stays as-is | Confirmed |
| Generic RPG entities in schema | Gnome Survivor-specific entities | Applied |
| Microservices recommended immediately | Staged local→live-service path | Applied |

---

## Acceptance Criteria

All Phase 2A salvage files must pass these criteria before feature flags are marked `true`:

- [ ] Game-specific (no generic fantasy or RPG placeholder content)
- [ ] Canon-accurate (all 10 canon lock rules applied throughout)
- [ ] Usable by a developer/content designer without guessing
- [ ] Not placeholder text (real structures, columns, examples)
- [ ] Includes real columns, examples, and implementation-facing detail
- [ ] Does not use: Clothes, Cloth Assembly, Fit, Ascender, Ascender Parts, rigid slots, Guild, generic Spell/Quest/Region

---

## Known Out-of-Scope Debt

The following items are **not** addressed in Phase 2A Salvage and must be handled separately:

1. **`docs/01_core/master_glossary.md`** — Does not yet include Fixtures, Rootrail, War Armory, Forgotten Manuals, or Hat passive rules. Requires a dedicated glossary update pass. See Glossary Mismatch Note below.
2. **`docs/08_production/implementation_planning_pack.md`** — Not in Phase 2A scope; `prod-001` remains false.
3. **Final Confidant/Burrowfolk friend-questionnaire content** — Placeholder content only in MVP planning; final content comes later.
4. **War Armory content** — Schema and template columns defined; actual War Fixture content is Phase 2B+.
5. **`doc-001`** — Full audit of all 17 existing canon docs not performed; remains false.
6. **Loamwake specific content tables** — `loamwake_mvp_content_sheet.md` is Phase 2B file 1.

---

## Glossary Mismatch Note

> **UNRESOLVED:** `docs/01_core/master_glossary.md` does not yet contain entries for: Fixtures, Hats (as shell-style permanent unlocks), Rootrail, War Armory, Forgotten Manuals, Rootrail Parts, or Fixture categories (Farming / Pure Attack / Strain / War). A glossary update pass is needed. Feature flag `canon-002` tracks this. Do not update `master_glossary.md` without a controlled pass — the glossary is the source of truth and must not be written during salvage without explicit scope authorization.

---

## Feature Tracking Recommendations

| Flag | Recommendation | Reason |
|------|----------------|--------|
| `prod-002` | `true` after data_schema_v1.md rewrite verified | Schema now complete with canonical entities |
| `prod-003` | `true` after core_loop_mvp_spec.md verified | MVP loop now substantial |
| `prod-004` | `true` after unlock_flow_and_ui_map.md verified | Unlock flow now concrete |
| `prod-005` | `true` after content_table_templates.md verified | Templates now have real columns |
| `prod-006` | `true` after backend_architecture_options.md verified | Backend options now substantial |
| `canon-001` | `true` — salvage report created | This document |
| `sys-001` | `true` after fixtures_and_hats_system_v1.md created | New doc |
| `sys-002` | `true` after rootrail_system_v1.md created | New doc |
| `data-001` | `true` after economy CSV verified | CSV replaced with canonical rows |
| `prod-007` | `true` after specialist_prompt_phase2b.md created | New doc |
| `canon-002` | `false` — remains until master_glossary.md is updated | Glossary not in scope |
| `doc-001` | `false` — not audited in this pass | Out of scope |
| `prod-001` | `false` — implementation_planning_pack not in scope | Out of scope |

---

## Verification Checklist

Before marking Phase 2A Salvage complete:

- [ ] `data/economy/economy_model_v1_sheet.csv` contains all 19 specified canonical rows; no generic currencies
- [ ] `docs/04_systems/data_schema_v1.md` models all 23 required entities; no Guild, Spell, or NPC
- [ ] `docs/04_systems/core_loop_mvp_spec.md` contains first-10-min, first-60-min, first-daily sections; Fixture acquisition; Rootrail reveal
- [ ] `docs/06_content/content_table_templates.md` has templates for all 20+ content types including Fixtures, Hats, War Fixtures, Rootrail repair steps, Forgotten Manuals
- [ ] `docs/07_ui/unlock_flow_and_ui_map.md` has minute-0, day-1, day-2/3, Loamwake-clear unlock sequences; no Ascender
- [ ] `docs/08_production/backend_architecture_options.md` evaluates ≥3 concrete options; recommends staged path
- [ ] `docs/04_systems/fixtures_and_hats_system_v1.md` exists and covers all fixture rules, hat rules, War Armory boundary
- [ ] `docs/04_systems/rootrail_system_v1.md` exists and covers repair loop, timers, Forgotten Manuals, route unlocks, passive bonuses
- [ ] `docs/08_production/specialist_prompt_phase2b.md` exists and is executable
- [ ] `CHANGELOG.md` has v0.2.1 entry; v0.2.0 preserved
- [ ] `feature_list.json` updated with new flags; no incorrect `true` marks
- [ ] `claude-progress.md` updated with Phase 2A Salvage summary

---

## Phase 2B Readiness Statement

Phase 2B may begin once all items in the Verification Checklist above are satisfied. The first Phase 2B file to tackle is:

**`docs/06_content/loamwake_mvp_content_sheet.md`**

This file establishes the first playable content layer (Loamwake routes, rewards, Warden gate, Burrow Post chain, MVP progression beats) and is a prerequisite for meaningful prototype work.

See `docs/08_production/specialist_prompt_phase2b.md` for the full Phase 2B execution brief.
