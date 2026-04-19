# claude-progress.md

## Phase 2A Salvage Repair — 2026-04-19

### What happened this session
Phase 2A was found to be entirely placeholder content. All 6 Phase 2A deliverables were near-empty stubs with no Gnome Survivor canon content. A full salvage rewrite (v0.2.1) was performed.

**Canon lock applied:**
- Clothes removed → replaced by Fixtures throughout
- Ascender removed → replaced by Rootrail throughout
- Hats modeled as permanent passive shell-style unlocks (not gear slots)
- War Fixtures live in separate War Armory (not personal Fixture cap)
- Forgotten Manuals are permanent, not consumed, separate from Elder Books
- Personal Fixture cap starts at 0, grows to 12 — no rigid slots
- Bronze Shovels stays Bronze Shovels

**Files rewritten (full):**
- `data/economy/economy_model_v1_sheet.csv` — 19 canonical rows (Mooncaps, Mushcaps, Glowcaps, Lucky Draws, Treasure Tickets, Echoes, Echo Shards, Festival Marks, Strata Seals, Elder Books, Strain Seeds, Fixture Materials, Polishes, Treasure Shards, Rootrail Parts, Forgotten Manuals, Crack Coins, Bronze Shovels, Favor Marks) with full faucet/sink/balance columns
- `docs/04_systems/data_schema_v1.md` — 23+ Gnome Survivor entities; account, wallet, burrow_state, strata_progress, deepening_state, crack_state, event_state, fixture_item, fixture_recipe, fixture_loadout, hat_unlock, hat_display_state, war_fixture_item, war_armory_loadout, rootrail_state, rootrail_repair_step, forgotten_manual, rootrail_codex_entry, confidant, burrowfolk_unit, strain_progress, post_state, encounter_state
- `docs/04_systems/core_loop_mvp_spec.md` — Full Loamwake MVP: first 10 min, first 60 min, first daily return, first Fixture acquisition, first hat unlock, first Rootrail reveal, in/out-of-scope matrix, proof-of-fun criteria, build acceptance checklist
- `docs/06_content/content_table_templates.md` — 20+ content type templates with required/optional columns, ID conventions, example rows: zones, wardens, buried clues, wanderers, runaways, wild ones, confidants, burrowfolk, treasures, fixtures, hats, war fixtures, rootrail repair steps, forgotten manuals, event ladders, festival ledger, burrow posts, duties
- `docs/07_ui/unlock_flow_and_ui_map.md` — Full unlock timing: minute-0, 10-min, 60-min, day-1, day-2/3, Loamwake-clear; Fixture/Hat/Rootrail/codex UI rules; red-dot anti-overload (max 3); stub treatment
- `docs/08_production/backend_architecture_options.md` — Options A–E evaluated; staged local→PlayFab/Firebase→custom path recommended; Rootrail timer server-authority spec; economy anti-cheat boundaries
- `CHANGELOG.md` — v0.2.1 patch entry added; v0.2.0 preserved with historical note

**Files created (new):**
- `docs/08_production/phase2a_salvage_report.md` — audit trail, canon lock, delta map, acceptance criteria, Phase 2B readiness statement
- `docs/04_systems/fixtures_and_hats_system_v1.md` — canon system spec for Fixtures, Hats, War Armory boundary
- `docs/04_systems/rootrail_system_v1.md` — canon system spec for Rootrail (replaces Ascender)
- `docs/08_production/specialist_prompt_phase2b.md` — executable Phase 2B brief with file order, acceptance criteria, dependency graph

**Tracking updated:**
- `feature_list.json` — prod-002 through prod-006 set to passes:true; 7 new flags added (canon-001 ✓, sys-001 ✓, sys-002 ✓, data-001 ✓, prod-007 ✓, canon-002 ✗ pending)
- `claude-progress.md` — this entry

### Known remaining debt (do not mark as done yet)
- `canon-002` — master_glossary.md NOT updated; needs a controlled glossary pass for Fixtures/Rootrail/War Armory/Forgotten Manuals/Hat passive rules
- `prod-001` — implementation_planning_pack.md NOT in Phase 2A scope; still false
- `doc-001` — full doc audit NOT performed; still false
- Loamwake Fixture material names are provisional — lock in Phase 2B

### What Phase 2B should do next
**First file: `docs/06_content/loamwake_mvp_content_sheet.md`**

See `docs/08_production/specialist_prompt_phase2b.md` for the full Phase 2B execution brief. The file order is:
1. `docs/06_content/loamwake_mvp_content_sheet.md` ← start here
2. `docs/06_content/first_confidant_chain.md`
3. `docs/06_content/first_wanderer_pool.md`
4. `docs/06_content/lucky_draw_week_mvp.md`
5. `docs/04_systems/tutorial_and_onboarding_flow.md`
6. `docs/08_production/save_state_and_profile_flow.md`
7. `docs/08_production/iap_catalog_v1.md`
