# CHANGELOG

## v0.12.0 — Phase 3 Sprint 7 Tutorial Wiring, Stabilization, Tester Proof Pack (2026-04-21)

### Added
- Lightweight persisted tutorial guide state with current step, completed steps, dismissed hints, reset support, and Burrow/Loamwake/Fixture guidance text
- Sprint 7 aggregate Mono verification harness covering fresh boot, tutorial state, core progression, first Fixture, Greta/Rootrail, Warden, Lucky Draw free path, Crack/Clique stub safety, save/load, and legacy migration
- Operator Unity smoke checklist, 5-tester checklist, and bug report template under `docs/08_production/`

### Changed
- Fresh profiles now start with gatherable Dewpond/Mushpatch output so a cold tester can Gather and Expand immediately
- Rootvine Shelf first clear now grants the intended first-craft packet for Root-Bitten Shovel Strap
- First capstone presentation now reads as The Mudgrip Warden / Warden Clash instead of generic Keeper-facing UI text
- First Warden reward/result summary is clearer and better aligned to the Loamwake content sheet

### Status
Sprint 7 complete. The first playable is ready for manual Unity smoke testing and a small 5-tester proof pass, with known broad layout debt intentionally left out of scope.

---

## v0.11.0 — Phase 3 Sprint 6 Crack + Clique Stub (2026-04-21)

### Added
- Crack persistence state for visibility/unlock, current depth, best depth, probe count, Crack Coins earned total, reward summary, and latest result
- Clique persistence state for visibility/unlock, local Clique name, player role, placeholder Clique Rolls roster, one-time local stipend claim, Favor Marks, and stub-only Great Dispute flags
- `CrackCliqueService` with Rootrail-reveal gating, one repeatable `Probe the Crack` action, one deterministic local Clique stipend action, and explicit no-networking/no-shared-state guards
- The Crack and Clique debug UI pages with back navigation, shell status, action controls, summaries, and debug/status areas
- Burrow integration showing Crack/Clique summaries and entry points once the Rootrail reveal gate is met
- Sprint 6 Mono verification harness and Unity manual smoke checklist

### Changed
- Profile save data now migrates additive Sprint 6 Crack/Clique state while preserving local-only prototype behavior

### Status
Sprint 6 complete. The prototype now shows two future-facing progression shells without full Deepening, Clique backend/social systems, Great Dispute gameplay, Crack ladder/sectors, monetization, or networking.

---

## v0.10.0 — Phase 3 Sprint 5 Lucky Draw Week, Lucky Stall, Festival Ledger (2026-04-21)

### Added
- Lucky Draw Week event state, Greta-gated prototype unlock, weekly free ticket claim, activity ticket hook, controlled pull table, persisted latest pull, and pull history
- Festival Ledger free lane with four pull-count milestones and idempotent claim persistence
- Lucky Stall side sink with Mooncap, Lucky Draw, and Festival Mark exchanges plus persisted weekly purchase counts
- Lucky Draw Week debug UI page with pull controls, Festival Ledger summary, Lucky Stall section, and debug/status area
- Sprint 5 Mono verification harness covering event gating, free ticket claim, pull rewards, ladder progress, ledger claim, Lucky Stall limits, persistence, and no paid/IAP path

### Changed
- Burrow UI now shows Lucky Draw Week only after the Greta prototype gate and includes a small event summary with current Lucky Draw tickets
- Profile save data now migrates additive Sprint 5 event, stall, and Festival Ledger state without activating monetization or full liveops scheduling

### Status
Sprint 5 complete. The prototype now has a repeatable free event loop without paid ticket paths, IAP, Treasure Week, Echo Week, subscriptions, receipts, store SDKs, or broader liveops backend work.

---

## v0.9.0 — Phase 3 Sprint 4 Burrow Posts, Daily Duties, Greta, Rootrail Reveal (2026-04-21)

### Added
- Burrow Post state and UI surface with a utility Post and Greta intro Post
- Minimal Daily Duties loop for Dewpond gather, Loamwake explore, and first Fixture check with persisted progress and auto-claimed rewards
- Greta unlock path through `post_lw_005_greta_intro` plus a first Trust follow-up step
- Rootrail reveal/station shell gated by Greta follow-up and Mudpipe Hollow first clear
- Sprint 4 Mono verification harness covering Greta, Posts, Duties, Rootrail reveal gating, persistence, and shell-only repair state

### Changed
- Profile save data now migrates additive Sprint 4 social progression, Daily Duties, Greta, and Rootrail reveal state
- Burrow UI now exposes Burrow Post, Daily Duties, Greta summary, and Rootrail Station entry while retaining debug/status visibility

### Status
Sprint 4 complete. The prototype now has a first return/social reason loop without full Confidant depth, Rootrail repair progression, events, Clique, Crack, monetization, or treasure progression.

---

## v0.8.0 — Phase 3 Sprint 3 Fixtures, Hat, Rootmine, Vault Shell (2026-04-20)

### Added
- Rootmine material production and gather flow for Tangled Root Twine and Crumbled Ore Chunk after Burrow level 2
- First Fixture slice for Root-Bitten Shovel Strap crafting, ordered-list equip/unequip, Fixture cap checks, and visible expedition power bonus
- First Hat shell for Loamwake Dirt Cap as a permanent passive outside the Fixture cap
- Vault of Treasures shell page with explicit Sprint 3 stub/debug status and no treasure progression
- Sprint 3 Mono verification harness covering Rootmine, Fixture, Hat, persistence, and Vault shell-only behavior

### Changed
- Profile save data now migrates additive Sprint 3 state for Fixtures, Hats, Vault shell, account Fixture cap, and Rootmine material storage
- Loamwake expedition power now includes equipped Fixture and unlocked Hat passive bonuses
- Burrow UI now exposes Rootmine gather, Fixture Workshop, Vault shell entry, Fixture summary, visible Hat summary, and retained debug/status surfaces

### Status
Sprint 3 complete. The first small equipment power-spike loop is implemented without Treasure, War Armory, Rootrail, Duty, or Confidant expansion.

---

## v0.7.0 — Phase 3 Sprint 2 Loamwake Exploration (2026-04-20)

### Added
- `strata_state` persistence shell for Loamwake zone progression, Keeper state, latest exploration result, and Field Returns
- `LoamwakeExplorationService` with Strata Gate selection, zones 1–3, safe/risky route choice, deterministic Auto-Clash, reward grants, and Keeper capstone resolution
- Loamwake material wallet fields for Tangled Root Twine, Crumbled Ore Chunk, and Dull Glow Shard
- Sprint 2 Mono verification harness for zone unlocks, route spend, fail persistence, Keeper persistence, and later-Strata lock checks

### Changed
- MainScene now includes a Burrow Strata Gate card, Field Returns snippet, and a Loamwake exploration page with its own debug/status area
- Profile service now supports entering Loamwake, exploring zones, Keeper challenge flow, and returning to the Burrow
- Save migration now rehydrates Sprint 1 profiles into the new Loamwake exploration state

### Fixed
- Loamwake now shows a top `Back to Burrow` button before zone cards so return navigation stays visible without scrolling

### Status
Sprint 2 complete. Loamwake now has a minimal local-first exploration slice with route choice and Keeper capstone persistence.

---

## v0.6.0 — Phase 3 Sprint 1 Burrow Core (2026-04-20)

### Added
- Burrow production state for Dewpond, Mushpatch, Rootmine lock state, unlocked rooms, stored output, and last production tick
- `BurrowProductionService` for deterministic local-first idle output and storage cap handling
- Burrow gather actions for Dewpond and Mushpatch
- Burrow expand flow with Mooncap spend, level increase, and Rootmine unlock at Burrow level 2
- Sprint 1 Mono verification harness for timed output, gather, expand, unlock, and persistence

### Changed
- MainScene Burrow UI now shows wallet values, Dewpond, Mushpatch, Burrow status, Rootmine state, and the debug/status panel together
- Boot and resume now process offline Burrow production before refreshing the UI
- Sprint 0 save migration now rehydrates the new Burrow state shape on load

### Status
Sprint 1 complete. The Burrow now has a working local-first gather loop and expand unlock shell.

---

## v0.5.0 — Phase 3 Sprint 0 Runtime & Persistence Shell (2026-04-20)

### Added
- Unity scaffold under `src/unity/` with `MainScene`, project settings, package manifest, and required script folders
- Local-first profile data model for save version, account, wallet, burrow state, and profile container shell
- `SaveManager`, `ProfileService`, `AuthManager`, and `AppBootstrap` runtime services
- Plain debug UI for The Burrow with Mooncaps, Gather, Force Save, Reload Save, auth status, save status, active UID, and save path
- Sprint 0 verification harness covering first boot, save/load, restart persistence, and auth failure fallback

### Status
Sprint 0 complete. Local profile persistence works without Firebase/Auth/backend availability.

---

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
