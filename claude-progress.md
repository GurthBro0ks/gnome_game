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

---

## Phase 2B Start — Cleanup + Loamwake Content Sheet — 2026-04-19

### What happened this session
Targeted Phase 2A consistency cleanup applied. First Phase 2B content file created.

**Cleanup changes applied (surgical, no Phase 2A rewrites):**

1. `docs/04_systems/core_loop_mvp_spec.md`
   - Corrected second Fixture cap increase from `0→2` to `1→2` in 3 locations (MVP Slice Summary table, First 60 Minutes beat, Systems In Scope list)

2. `docs/04_systems/data_schema_v1.md`
   - Renamed `quest_chain_ids` → `duty_chain_ids` on the `confidant` definition table
   - Fixed hat passive constant from `all_unlocked_hats_stack` → `all_unlocked_hats_stack_tiny_passives`
   - Added `confidant_state` player-state section (11 fields including trust_level, active_duty_chain_ids)
   - Added `duty_progress` player-state section (11 fields including objective_progress, repeat_window_id)
   - Added Warden/War Fixture ID prefix note to Unlock Conditions section
   - Updated gate expression example from `bond_level` → `trust_level` (consistent with new confidant_state)

3. `docs/06_content/content_table_templates.md`
   - Warden prefix in ID Naming Conventions table: `war_` → `wdn_`; example `war_lw_001_mudgrip` → `wdn_lw_001_mudgrip`
   - Zone example row: `war_lw_001_mudgrip` → `wdn_lw_001_mudgrip`
   - Warden example row: `war_lw_001_mudgrip` → `wdn_lw_001_mudgrip`
   - Hat example row unlock condition: `warden_clears.war_lw_001_mudgrip` → `warden_clears.wdn_lw_001_mudgrip`
   - `wfx_` War Fixture IDs unchanged

4. `docs/08_production/specialist_prompt_phase2b.md`
   - Warden ID example: `war_lw_001_mudgrip` → `wdn_lw_001_mudgrip`
   - Added Warden ID prefix rule to Canon Lock section
   - Added "Do not use war_ for Warden IDs" and "Do not change wfx_ War Fixture IDs" to What NOT to Do

**Files verified (no changes needed):**
- `docs/07_ui/unlock_flow_and_ui_map.md` — cap sequence already compatible with 0→1→2; no `war_lw_` IDs present

**New file created:**
- `docs/06_content/loamwake_mvp_content_sheet.md` — Phase 2B content root
  - 8 canonical Loamwake material names (locked; resolves provisional note in core_loop_mvp_spec.md)
  - 3 launch zones (zone_lw_001–003) with drops and encounter table links
  - 3 encounter pool tables (enc_table_lw_001/002/003) with 14 total spawn entries
  - First Warden: `wdn_lw_001_mudgrip` (The Mudgrip) with first-clear and repeat rewards
  - 4 Buried Clues; `clue_lw_002_terminal_routing_cache` grants `fman_001_terminal_routing`
  - First Forgotten Manual: `fman_001_terminal_routing` — fully specified (permanent, consumed:false, separate from Elder Books)
  - Rootrail station `rtr_station_lw_001_loamwake_terminal` + repair step `rtr_step_001`
  - 5 personal Fixture definitions (fix_001–005, covering Farming/Pure Attack/Strain)
  - 5 Fixture recipes (fix_rec_001–005) using only locked material IDs
  - 6 Wanderers, 4 Runaways, 4 Wild Ones (all zone-referenced)
  - Story Duty chain (4 Duties: dty_lw_001–004); cap increases embedded in dty_lw_001 and dty_lw_002 rewards
  - 3 daily Duties
  - 4 Burrow Posts

**Tracking updated:**
- `feature_list.json` — `content-001` appended with passes:true
- `claude-progress.md` — this entry

### Known remaining debt (unchanged)
- `canon-002` — master_glossary.md still not updated
- `prod-001` — implementation_planning_pack.md still false
- `doc-001` — full doc audit still false

### What Phase 2B should do next
**Second file: `docs/06_content/first_confidant_chain.md`**

Depends on Loamwake content sheet. Must define:
- `cnf_001_placeholder_greta` with 3+ Duties; one Duty must hook to `dty_lw_003_greta_rail_lead`
- `cnf_002_placeholder_mossvane` with 2+ Duties
- Bond level scale (1–5)
- `placeholder_flag: true` on all rows
- All Duty rewards reference material IDs locked in `loamwake_mvp_content_sheet.md`

---

## Phase 2B — Patch Pass + First Confidant Chain — 2026-04-19

### What happened this session

Canon-consistency patch applied to two existing files, then the second Phase 2B content file created.

**Patch 1: `docs/06_content/loamwake_mvp_content_sheet.md`**

- Removed `account_bonus:{type:fixture_cap_increase, value:1}` from `dty_lw_001_clear_rootvine` reward — the tutorial now unambiguously owns the 0→1 cap increase
- Updated the NOTE below the Duty chain table: tutorial owns 0→1; `dty_lw_002_hollow_survey` is the sole Loamwake story Duty owning 1→2
- Updated the MVP Progression Gate Table: row "Fixture cap 0→1" now reads "Tutorial sequence (first 5 min; not a Duty reward)"
- Fixed typo: `Bury Clue` → `Buried Clue` in `fman_001_terminal_routing` acquisition_source field
- Normalized short codex key `rootrail_codex_entry.fman_001.discovered` → full ID `rootrail_codex_entry.fman_001_terminal_routing.discovered` in the gate table
- Updated the Unresolved cap note to correctly flag only `dty_lw_002_hollow_survey` plus the tutorial mutation path
- Updated the Validation Checklist to two explicit checkboxes (tutorial owns 0→1; dty_lw_002 owns 1→2)
- Updated Reward Notes: codex mutation key now uses full ID `fman_001_terminal_routing.discovered`

**Patch 2: `docs/04_systems/data_schema_v1.md`**

- Aligned the Definition Tables vs. Player State matrix: `burrowfolk_unit` row now maps to `burrow_state (active_burrowfolk_slots, burrow_work_queue)` instead of the non-existent `burrowfolk_deployment`
- Added MVP NOTE under `BurrowWorkItem` explaining that Burrowfolk deployment is tracked inside `burrow_state` for MVP; documented what a future `burrowfolk_deployment` document would need if the system is expanded

**New file created:**

- `docs/06_content/first_confidant_chain.md` — Phase 2B Confidant root
  - 16 sections
  - Source ID Lock Table: 15 cross-file IDs verified against loamwake_mvp_content_sheet.md
  - Trust scale 0–5 with per-level labels; trust_point accumulation rules; state field mapping
  - cnf_001_placeholder_greta: Pathfinder role, bond_stat_focus craft, max_bond_level 5, placeholder_flag true
  - cnf_002_placeholder_mossvane: Archivist role, bond_stat_focus lore, max_bond_level 5, placeholder_flag true
  - Calling table: calling_greta_rail_sense (UI hint at Rootrail station) + calling_mossvane_market_eye (Wanderer reveal stub); both unlock at trust_level 2
  - Greta Confidence Trail: dty_cnf001_01_shelf_scratches → dty_cnf001_02_hollow_measure → dty_lw_003_greta_rail_lead (cross-file reference; not redefined)
  - Mossvane Confidence Trail: dty_cnf002_01_sporewax_trade → dty_cnf002_02_map_the_glowroot → dty_cnf002_03_parts_for_a_route (all defined in this file)
  - Trust Progression Trigger Table: 6 rows covering all Duty steps for both chains
  - Reward Reference Table: all 9 reward items verified against locked Loamwake IDs
  - Schema/State Mapping: content-definition fields vs. player-state fields separated clearly
  - Validation Checklist: 20 checks, all passing
  - 5 UNRESOLVED items flagged for future passes

**Tracking updated:**

- `feature_list.json` — `content-002` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt (unchanged)

- `canon-002` — master_glossary.md still not updated
- `prod-001` — implementation_planning_pack.md still false
- `doc-001` — full doc audit still false

### What Phase 2B should do next

**Third file: `docs/06_content/first_wanderer_pool.md`**

Depends on:
- Zone IDs from loamwake_mvp_content_sheet.md Section 6
- Wanderer IDs partially defined in loamwake_mvp_content_sheet.md Section 15 (6 Wanderers already locked)
- Mossvane trade Duty (dty_cnf002_01_sporewax_trade) references Wanderer trade interactions
- Greta introduction Burrow Post (post_lw_005_greta_intro) is flagged as UNRESOLVED in first_confidant_chain.md

