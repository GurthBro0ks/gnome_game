# CHANGELOG

## v0.4.1 — Phase 3 Bootstrap Cleanup (2026-04-20)

### Fixed
- Phase 3 planning docs aligned to Fixture canon
- First-playable scope narrowed to the actual vertical slice
- Phase 3 sprint tracking added to feature_list.json

### Status
Sprint 0 ready to begin.

---

## v0.3.0 — Phase 2B Content & Planning Complete (2026-04-20)

### Added
* docs/06_content/loamwake_mvp_content_sheet.md — Loamwake MVP content root
* docs/06_content/first_confidant_chain.md — Greta and Mossvane placeholder chains
* docs/06_content/first_wanderer_pool.md — 14 Loamwake encounter entries
* docs/06_content/lucky_draw_week_mvp.md — evt_luckydraw_001 full spec
* docs/04_systems/tutorial_and_onboarding_flow.md — Step-by-step tutorial beats
* docs/08_production/save_state_and_profile_flow.md — Local-first persistence contract
* docs/08_production/iap_catalog_v1.md — MVP monetization catalog
* docs/08_production/implementation_planning_pack.md — Build sequencing pack
* docs/08_production/phase2_completion_audit.md — Phase 2 closeout verification

### Fixed
* Root-level junk files archived to docs/99_archive/root_cleanup/
* feature_list.json reconciled with repo truth
* claude-progress.md brought current

### Status
Phase 2 complete. Phase 3 ready to begin.

---

## v0.2.1 — Phase 2A Salvage Repair (2026-04-19)

### Added
- `docs/08_production/phase2a_salvage_report.md` — Controlling audit trail for the salvage pass; records canon lock, delta map, acceptance criteria, and Phase 2B readiness statement
- `docs/04_systems/fixtures_and_hats_system_v1.md` — Canon system spec for Fixtures, Hats (permanent passive/shell model), and War Armory boundary; defines 0→12 cap, no-slot philosophy, contextual tradeoffs, duplicate-build rules
- `docs/04_systems/rootrail_system_v1.md` — Canon system spec for Rootrail (replaces The Ascender); defines repair loop, timer growth, Rootrail Parts, Forgotten Manual codex, route unlocks, and passive account bonuses
- `docs/08_production/specialist_prompt_phase2b.md` — Executable brief for Phase 2B specialist with file order, canon lock, acceptance criteria, dependency graph, and unresolved canon questions

### Changed (full rewrites)
- `data/economy/economy_model_v1_sheet.csv` — Replaced all generic fantasy currencies (Aurum, Silver, etc.) with 19 canonical Gnome Survivor resources: Mooncaps, Mushcaps, Glowcaps, Lucky Draws, Treasure Tickets, Echoes, Echo Shards, Festival Marks, Strata Seals, Elder Books, Strain Seeds, Fixture Materials, Polishes, Treasure Shards, Rootrail Parts, Forgotten Manuals, Crack Coins, Bronze Shovels, Favor Marks — with full faucet/sink/balance columns
- `docs/04_systems/data_schema_v1.md` — Replaced generic RPG entities (Currency, Quest, NPC, Spell, Guild, etc.) with full Gnome Survivor schema: 23+ entities across account, wallet, burrow, strata, fixtures, hats, war armory, Rootrail, forgotten manuals, deepening, crack, events, clique, confidants, burrowfolk, duties, posts, encounters
- `docs/04_systems/core_loop_mvp_spec.md` — Replaced 5-line generic loop spec with full Loamwake MVP: first 10 min, first 60 min, first daily return, first Fixture acquisition, first hat unlock, first Rootrail reveal, in/out-of-scope system matrix, proof-of-fun criteria, build acceptance checklist
- `docs/06_content/content_table_templates.md` — Replaced generic template list (LootTable, Region_Weather, etc.) with 20+ Gnome Survivor content type templates (zones, wardens, buried clues, wanderers, runaways, wild ones, confidants, burrowfolk, treasures, personal fixtures, hats, war fixtures, Rootrail repair steps, forgotten manuals, event ladders, festival ledger, burrow posts, duties, encounters) — all with required columns, optional columns, ID conventions, and example rows
- `docs/07_ui/unlock_flow_and_ui_map.md` — Replaced generic tier/HUD list with full Loamwake MVP unlock timing: minute-0, 10-min, 60-min, day-1, day-2/3, Loamwake-clear sequences; Fixture/Hat/Rootrail/codex UI rules; red-dot anti-overload logic (max 3 concurrent); stubbed system treatment
- `docs/08_production/backend_architecture_options.md` — Replaced 3-line "use microservices" stub with staged evaluation: Option A (local JSON prototype), Option B (PlayFab), Option C (Firebase/Supabase), Option D (Unity Gaming Services), Option E (custom backend later); recommended staged path; Rootrail timer server-authority spec; economy anti-cheat boundaries; MVP acceptance checklist

### Fixed (Canon Corrections)
- Removed all Clothes/Cloth Assembly references from implementation-facing documents
- Removed all Ascender/Ascender Parts references; replaced throughout with Rootrail/Rootrail Parts
- Removed rigid gear slot model; Fixtures are now an ordered list bounded by a cap
- Hats correctly modeled as shell-style permanent unlock/display (not gear slots)
- War Fixtures correctly separated from personal Fixture cap in schema and templates
- Forgot Manuals correctly separated from Elder Books in schema, economy CSV, and templates
- Bronze Shovels confirmed as canon name (not renamed)

### Canon Notes
- `docs/01_core/master_glossary.md` NOT updated in this pass. A glossary update for Fixtures, Rootrail, War Armory, Forgotten Manuals, and Hat passive rules is required. Tracked as `canon-002` in feature_list.json.
- `docs/08_production/implementation_planning_pack.md` NOT created in this pass (`prod-001` remains false).
- Placeholder Confidant/Burrowfolk content allowed for MVP; final friend-questionnaire replacements are Phase 2B+.

### Remaining Follow-Up
- Phase 2B: `loamwake_mvp_content_sheet.md` is the first file to tackle (see `specialist_prompt_phase2b.md`)
- Glossary update pass needed (`canon-002`)
- Rootrail repair table (steps 1–5 with locked costs and timers) needed in Phase 2B
- Master_glossary.md does not yet include: Fixtures, Rootrail, War Armory, Forgotten Manuals, Hat passive rules

---

## v0.2.0 — Phase 2A Production Foundation (2026-04-19)

> Historical note: This pass placed 6 files that were later found to be near-empty placeholders. The files were substantially rewritten in v0.2.1. The entry below records the original pass for audit purposes.

- Added economy model CSV (placeholder: generic fantasy currencies)
- Added data schema with 17+ generic entities (placeholder: no Gnome Survivor schema)
- Added core loop MVP spec (placeholder: 5-line generic loop)
- Added content table templates (placeholder: 14 named templates, no columns)
- Added unlock flow and UI map (placeholder: 4-line tier list)
- Added backend architecture options (placeholder: 3-line microservices recommendation)
