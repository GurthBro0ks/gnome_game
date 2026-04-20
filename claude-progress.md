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


---

## Phase 2B — Support Patch + First Wanderer Pool — 2026-04-19

### What happened this session

Support patch applied to `docs/06_content/loamwake_mvp_content_sheet.md`, then the third Phase 2B content file created.

**Support patch: `docs/06_content/loamwake_mvp_content_sheet.md`**

1. Added `post_lw_005_greta_intro` (Rail Scratches on the Board) as the 5th Burrow Post row in Section 19 — resolves the UNRESOLVED item from `first_confidant_chain.md`. Unlock: `dty_lw_001_clear_rootvine.state == completed`. Completion sets `confidant_state.cnf_001_placeholder_greta.unlocked = true` and makes `dty_cnf001_01_shelf_scratches` available. Non-repeatable, one-shot, instant.
2. Updated "What this file authorizes" bullet: 4 Burrow Posts → 5 Burrow Posts.
3. Updated Section 19 intro text: "First 4 Burrow Posts" → "First 5 Burrow Posts". Added NOTE callout below the new row explaining state side-effects.
4. Updated Section 24 (Next Recommended File): changed pointer from `first_confidant_chain.md` (completed) → `first_wanderer_pool.md`. Added COMPLETED note summarizing Confidant chain state.

**New file created: `docs/06_content/first_wanderer_pool.md`**

390 lines. 21 sections. Expands all 14 locked Loamwake roaming encounter IDs into build-ready interaction content.

- Source Roster Mirror: all 14 encounter IDs with zone, table, weight, unlock
- Zone Linkage Summary: per-zone encounter pacing notes
- Wanderer Detail Table: all 6 Wanderers with role identity, duty_support_tags
- Wanderer Interaction Matrix: trade/help/barter availability per Wanderer
- Wanderer Trade Offer Table: 8 trade rows (`trade_lw_001` through `trade_lw_005b`)
- Wanderer Help Outcome Table: 4 help rows (`help_lw_002`, `help_lw_003`, `help_lw_005`, `help_lw_006`)
- Wanderer Barter Offer Table: 5 barter rows (`barter_lw_001` through `barter_lw_006b`)
- Runaway Detail Table: all 4 Runaways with lore-safe notes
- Runaway Catch Outcome Table: success reward, fail consequence, cooldown, duty tags
- Wild One Detail Table: all 4 Wild Ones with difficulty scaling note
- Wild One Combat Reward Detail: win reward, fall-back, cooldown, duty tags
- Greta Support Note: post_lw_005_greta_intro confirmed; environmental encounter support via wnd_lw_002, wnd_lw_003
- Mossvane Support Note: full 3-Duty support chain documented; trade_lw_005_sporewax_bundle is primary dty_cnf002_01 path
- Reward Reference Table: all 10 reward items verified against locked Loamwake IDs
- Schema/State Mapping: content-definition vs. player-state fields separated
- Validation Checklist: 20 checks, all passing
- 5 UNRESOLVED items flagged

**Tracking updated:**

- `feature_list.json` — `content-003` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt (unchanged)

- `canon-002` — master_glossary.md still not updated
- `prod-001` — implementation_planning_pack.md still false
- `doc-001` — full doc audit still false

### What Phase 2B should do next

**Fourth file: `docs/06_content/lucky_draw_week_mvp.md`**

Depends on:
- Economy model (`data/economy/economy_model_v1_sheet.csv`) for Lucky Draw ticket currency
- Loamwake material IDs from `loamwake_mvp_content_sheet.md §5` for prize pools
- Event cadence rules from `docs/05_liveops/` for 3-week rotation structure


---

## Phase 2B — Support Patch + Lucky Draw Week MVP — 2026-04-20

### What happened this session

Support patch applied to `docs/06_content/first_wanderer_pool.md`, then the fourth Phase 2B content file created.

**Support patch: `docs/06_content/first_wanderer_pool.md`**

- Line 386 (Next Recommended File section): `lucky_draw_tickets` → `lucky_draws` (canonical wallet key per `data_schema_v1.md §wallet` and economy CSV `cur_004`). This was the only naming inconsistency found; no other files required patching.

**New file created: `docs/06_content/lucky_draw_week_mvp.md`**

19 sections. Implementation-ready Lucky Draw Week event content.

- Event definition: `evt_luckydraw_001` — "Lucky Draw Week: Loose Lots"; 168h; unlocks Day 15 (`account.age_days >= 14` + Zone 1 unlocked)
- Reward ID Readiness Table: 11 reward families audited; Hats and War Fixtures explicitly forbidden
- Free weekly floor: 3 confirmed sources (weekly claim, activity milestone, Mooncap stall item); optional 4th+5th via daily task path (5/week for active free players)
- 7-tier ladder: thresholds 1/3/5/8/12/16/20 pulls; T1–T3 reachable by free players; T7 cap is a material kit (not a direct Fixture grant)
- Pull table: 10 rows; weights sum to 100; weekly caps on rootrail_parts (3), treasure_tickets (1), festival_marks (30), fixture_grant (1)
- Pull table Fixture row: only fix_001, fix_002, fix_004 (all craftable); fix_003/fix_005 appear as material-kit targets in stall only; duplicates convert to materials
- Lucky Stall: 6 items; stall_lucky_001 is Mooncap-only (250 Mooncaps = 1 Lucky Draw); stall_lucky_004 is Festival Marks sink (30 marks = 1 Lucky Draw)
- Glowcap conversion: 10 Glowcaps = 1 Lucky Draw; weekly cap 10 via conversion; optional only
- Festival Marks: sources from pull_lw_005 (weekly cap 30) + ladder T4 (20) + ladder T6 (25); sink via stall_lucky_004 (30 marks → 1 draw)
- Rootrail safety: pulls cap 3/week; stall cap 3/week (1 purchase); event total ~6 Rootrail Parts max — does not bypass rtr_step_001 cost of 20
- Schema/state mapping: content-definition vs. player-state fields clearly separated
- Validation checklist: 21 items, all passing
- 5 UNRESOLVED items flagged (Festival Ledger tier thresholds, pull table balance tuning, Glowcap cap adjustment, daily task definition, stall_lucky_004 first-week edge case)

**Tracking updated:**

- `feature_list.json` — `content-004` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt (unchanged)

- `canon-002` — master_glossary.md still not updated
- `prod-001` — implementation_planning_pack.md still false
- `doc-001` — full doc audit still false

### What Phase 2B should do next

**Fifth file: `docs/04_systems/tutorial_and_onboarding_flow.md`**

Depends on:
- `loamwake_mvp_content_sheet.md` — for first zone, first Duty, first Fixture craft beat
- `core_loop_mvp_spec.md` — for 10-minute and 60-minute flow sequence
- `data_schema_v1.md` — for `tutorial_flags` field and `fixture_cap` mutation

Must define:
- Step-by-step tutorial beat sequence (trigger, UI action, system unlocked, skip_allowed flag)
- `fixture_cap 0→1` tutorial beat — `skip_allowed: false`
- First Fixture craft and equip beat — `skip_allowed: false`
- Anti-overload checkpoints
- Rootrail mention as "an old structure to investigate" only — no full explanation

---

## Phase 2B — Support Patch + Tutorial and Onboarding Flow — 2026-04-20

### What happened this session

Support patch applied to `docs/07_ui/unlock_flow_and_ui_map.md`, then the fifth Phase 2B content file created.

**Support patch: `docs/07_ui/unlock_flow_and_ui_map.md`**

- Aligned Lucky Draw Week visibility: hidden through first 14 days, active icon appears Day 15+.
- Clarified Rootrail Station reveal trigger: `dty_lw_003_greta_rail_lead` completion instead of generic text.

**New file created: `docs/04_systems/tutorial_and_onboarding_flow.md`**

- Defined Phase 2B onboarding, covering the first 60 minutes and first daily return.
- Locked source IDs for first zone, duties, confidant, fixtures, and event.
- Established anti-overload pacing limits (max 2 new surfaces per 10-min block, max 2 red dots).
- Authored the first 10 minutes beat table, including Burrow intro, first explore, Fixture cap grant (0→1), first recipe, craft, and equip.
- Authored the first 60 minutes onboarding table, including Wanderer trade, Greta intro, cap 02 grant (1→2), and Rootrail tease.
- Explicitly marked Fixture cap 0→1, first craft, and first equip as non-skippable (mandatory).
- Clarified Rootrail reveal as an old structure to investigate (missing manual) rather than a deep system explanation.
- Handled Greta's soft intro via Burrow Post after `dty_lw_001_clear_rootvine`.
- Defined economy and event surfacing rules, ensuring Lucky Draw is deferred and no monetization pressure exists early.
- Included Schema and State Mapping, separating content fields from player-state fields.

**Tracking updated:**

- `feature_list.json` — `content-005` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt (unchanged)

- `canon-002` — master_glossary.md still not updated
- `prod-001` — implementation_planning_pack.md still false
- `doc-001` — full doc audit still false

### What Phase 2B should do next

**Sixth file: `docs/08_production/save_state_and_profile_flow.md`**

Depends on the systems and states defined in the earlier documentation passes to map the backend data payload.

---

## Phase 2B — Support Patch + Save State and Profile Flow — 2026-04-20

### What happened this session

Support patches applied, then the sixth Phase 2B production file created.

**Support patch: `docs/04_systems/tutorial_and_onboarding_flow.md`**

- Normalized the tutorial-window red-dot queue rule to max 2 visible main-nav red dots until `tutorial_window_complete = true`, matching the Section 4 tutorial cap.

**Support patch: `docs/04_systems/core_loop_mvp_spec.md` and `docs/07_ui/unlock_flow_and_ui_map.md`**

- Replaced pre-Day-15 Lucky Draw "Coming Soon" icon language with the locked hidden-until-eligible rule.
- Lucky Draw now remains hidden before Day 15 eligibility and appears only after `zone_lw_001_rootvine_shelf` is unlocked.

**New file created: `docs/08_production/save_state_and_profile_flow.md`**

- Defined the MVP local-first profile container and first-run bootstrap order.
- Locked first-run defaults, including `account.fixture_cap = 0`, starter wallet values, empty Fixture inventory/loadout, Loamwake Zone 1 baseline, hidden Rootrail state, and no early Lucky Draw event row.
- Authored autosave/write-through rules for tutorial, wallet, craft/equip, Duties, Posts, Confidants, Rootrail, and Lucky Draw event mutations.
- Defined mutable-state ownership for `tutorial_flags`, `wallet`, `fixture_loadout`, `duty_progress`, `post_state`, `confidant_state`, `rootrail_state`, `rootrail_codex_entry`, `event_state`, and transient UI cache.
- Defined resume/recovery behavior for interruption during tutorial, after first craft before equip, Greta intro, Rootrail reveal, and Day 15 Lucky Draw eligibility crossing.
- Defined migration/versioning rules using `save_version`, schema-versioned state families, safe defaults for additive fields, last-good snapshot recovery, and placeholder Confidant migration notes.

**Tracking updated:**

- `feature_list.json` — `content-006` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt / unresolved notes

- Cloud merge policy is deferred; MVP assumes one local profile.
- Exact checksum/hash implementation is engineering-owned.
- Starter wallet defaults are MVP tutorial-safe and should be retuned if economy grants are later locked elsewhere.
- Lucky Draw event daily task definitions remain deferred to a future event task/liveops pass.
- Battle stat and encounter resolution math remain outside this save/profile contract.
- Existing unrelated UI map tail typo remains untouched.

### What Phase 2B should do next

**Seventh file: `docs/08_production/iap_catalog_v1.md`**

Depends on Lucky Draw, wallet, and economy safety rules. Must preserve the no-premium-only-power constraint and avoid exclusive paid progression.

---

## Phase 2B — IAP Catalog v1 — 2026-04-20

### What happened this session

No support patch was required before drafting. The active tutorial, core loop, UI, Lucky Draw, save/profile, and economy sources already preserve the hidden-until-Day-15 event rule and keep monetization out of the tutorial window. Purchase restore and entitlement behavior is defined in the new IAP catalog as production store metadata without changing `data_schema_v1.md`.

**New file created: `docs/08_production/iap_catalog_v1.md`**

- Defined the MVP monetization catalog as a planning/content contract, not backend code.
- Locked monetization safety rules: no pay-only progression, no premium-only power Fixtures, no stronger paid Hat passives, no tutorial or first-60-minute purchase pressure, and no Lucky Draw free-floor reduction.
- Authored catalog categories for starter bundle, Glowcap packs, optional Lucky Draw support, utility/value packs, soft-currency routes, deferred cosmetics, and forbidden hard progression unlocks.
- Added pricing tier labels from `T1_USD_0_99` through reserved `T6_USD_49_99`.
- Authored four one-time offers, including a starter bundle hidden until after onboarding and first daily-return eligibility.
- Authored five repeatable packs: three Glowcap packs plus two weekly utility caches.
- Defined Lucky Draw-linked purchase rules, including the shared 10 paid top-up draw cap across direct IAP grants and Glowcap conversion.
- Preserved `stall_lucky_001_mooncap_draw` as the Mooncap-only non-IAP path.
- Defined soft-currency vs premium-currency distinctions and purchase restore/entitlement behavior for consumables, one-time limited bundles, and event-limited bundles.
- Mapped purchase behavior to `wallet`, `event_state`, `festival_ledger_state`, `account.meta`, and production-only entitlement/fulfillment metadata.
- Added failure/recovery rules for receipt validation failure, pending fulfillment, crash recovery, restore unavailable, duplicate receipts, and refund policy gaps.

**Tracking updated:**

- `feature_list.json` — `content-007` appended with passes: true
- `claude-progress.md` — this entry

### Known remaining debt / unresolved notes

- Formal `iap_entitlement_state` and `iap_fulfillment_ledger` schema documents are still needed before real-money launch.
- Refund and chargeback wallet correction policy remains unresolved.
- Final localized pricing, tax handling, store registration, and receipt-validation provider choice remain production/business decisions.
- Paid cosmetic hat policy and subscriptions are deferred.
- Age-rating, parental consent, loot-box disclosure, and regional compliance review are required before Lucky Draw-linked IAP ships.

### What should happen next

**Next recommended file: `docs/08_production/implementation_planning_pack.md`**

Phase 2B content is now ready to roll into an implementation planning pack that sequences prototype build work, validates remaining production blockers, and connects the content docs to engineering milestones.

---

## Phase 2B — Implementation Planning Pack — 2026-04-20

### What happened this session

Support patch applied to `project_roadmap.md`, then the Phase 2B MVP implementation planning pack was created.

**Support patch: `project_roadmap.md`**

- Updated Phase 2B deliverable table to include `2B.8 | Implementation plan | implementation_planning_pack.md` and marked all eight deliverables as `✅` (complete).
- Updated the summary header `7 docs` to `8 docs` for Phase 2B.
- No other sequence/logic contradictions were found across the other Phase 2A/2B docs; the systems are already correctly aligned.

**New file created: `docs/08_production/implementation_planning_pack.md`**

- Created the build-order source of truth for the MVP execution.
- Authored the core dependency graph tracing Schema -> Save/Profile -> Onboarding -> Content -> Duties -> Encounters -> Rootrail -> Events -> Monetization.
- Defined 8 sequential implementation phases with corresponding discipline lanes for engineering and design.
- Established 8 hard Validation/Proof Gates (`VG-1` through `VG-8`), ensuring strict testing criteria for state bootstrap, onboarding recovery, economy, Greta unlock, Rootrail reveal, and monetization limits.
- Consolidated an exhaustive Authored Document Inventory spanning schema, core loop, UI, and all content files.
- Defined explicit deferred/out-of-scope boundaries (e.g., War Armory, Strata 2-5, Deepening, Cloud Sync, Daily Tasks).
- Listed immediate production risks (e.g., event schedule QA testing).

**Tracking updated:**

- `feature_list.json` — `content-008` appended with `passes: true`.
- `claude-progress.md` — this entry.

### Known remaining debt / unresolved notes

- Master glossary (`docs/01_core/master_glossary.md`) was never formally updated to reflect the Phase 2A Salvage (e.g., Ascender -> Rootrail, Clothes -> Fixtures).
- Document audit (`doc-001`) and implementation planning (`prod-001`) flags in `feature_list.json` refer to older salvage/planning needs. `prod-001` is functionally solved by the new planning pack but may need structural review in tracking.

### What should happen next

**Next recommended execution target:**
- **Phase 3 (First Playable) Sprints.**
- Specifically, the engineering team should begin building **Phase 1: Foundation Runtime & Persistence** (Local save file generation, bootstrap default wallet/flags), followed immediately by **Phase 2: Tutorial & Onboarding Wiring**.

---

## Phase 2 Closeout Audit — 2026-04-20

### What happened this session

Phase 2 closeout executor performed a full audit of all Phase 2A/2B deliverables. All patches applied, tracking updated, Phase 2 declared complete.

**Fixes applied:**

1. `project_roadmap.md` — marked Phase 2B.8 (implementation_planning_pack.md) ✅; marked all Phase 2A milestones ✅; updated status headers from "CURRENT⬅/NEXT" to "COMPLETE ✅"; fixed stale Clothes→Fixtures (S3 sprint, Phase 4 table); fixed Ascender→Rootrail (Long-Term Roadmap); bumped version to 0.1.1

2. `docs/01_core/master_glossary.md` — replaced "Cloth Assembly"→"Fixture Workshop", "Clothes"→"Fixture", "Fit"→"Equip"; replaced "The Ascender"→"Rootrail", "Ascender Parts"→"Rootrail Parts"; added new section "Fixtures, Hats, and equipment system" (12 entries); added new section "Rootrail system" (9 entries)

3. `docs/08_production/phase2_completion_audit.md` — created full audit document verifying canon consistency (12 checks, all PASS), cross-doc ID consistency (11 key IDs, all consistent across 3-8 files each), tracking consistency, substantial-file audit (14 files, all substantial), and glossary consistency

4. `feature_list.json` — set `prod-001`, `doc-001`, `canon-002` to `passes: true` with completion dates and audit notes; added `audit-001` flag

### Audit scope

Full audit of all Phase 2 authored docs:
- 6 Phase 2A salvage files (schema, core loop, UI map, content templates, backend options, economy CSV)
- 4 Phase 2A new system specs (fixtures/hats, rootrail, salvage report, specialist prompt)
- 8 Phase 2B content/production files (Loamwake sheet, confidant chain, wanderer pool, Lucky Draw, tutorial, save/profile, IAP catalog, implementation planning pack)

### Audit results

- **Canon consistency:** All 12 canon rules verified PASS across all Phase 2 docs
- **Cross-doc IDs:** All 11 key authored IDs consistent across all referencing files
- **Stale terminology:** Clothes and Ascender appear only in Phase 1 pre-salvage docs (expected, archived reference) and "replaced by" context in Phase 2 docs
- **Substantial-file audit:** All 14 major files are 130–701 lines; no placeholders remain
- **Glossary:** Corrected from salvage canon lag; new Fixtures/Hats/Rootrail/Forgotten Manuals/War Armory entries added
- **Tracking:** All previously-outstanding flags (prod-001, doc-001, canon-002) resolved

### Remaining blockers

**None.** Phase 2 is complete.

### Deferred items (not Phase 2 blockers)

- War Armory content (post-MVP)
- Strata 2–5 content (post-MVP)
- Final Confidant/Burrowfolk questionnaire content (post-MVP)
- Full Rootrail repair chain steps 2+ (post-MVP)
- Battle stat resolution system stub
- Event daily task definitions
- Cloud merge policy
- Paid cosmetic hat policy
- Formal IAP entitlement schema

### Phase 2 status: COMPLETE

Phase 3 (First Playable) sprints may begin. The implementation_planning_pack.md provides the build order, validation gates, and dependency graph.

---

## Phase 2 Reconciliation — 2026-04-20

### What happened this session
Tracking reconciliation pass. Verified all Phase 2 files exist.
Updated feature_list.json to match repo truth.
Root-level junk files archived to docs/99_archive/root_cleanup/.
Phase 2 tracking now matches PM report and closeout audit.

### Current state
- Phase 1: complete
- Phase 2A: complete (salvaged v0.2.1)
- Phase 2B: complete (7 files + patches)
- Phase 2 closeout: complete
- Phase 3: not started — ready to begin

### Next step
Phase 3: Foundation Runtime & Persistence

---

## Phase 3 Bootstrap — 2026-04-20

### What happened this session
Phase 3 startup pass completed.
Verified Phase 2 reconciliation state.
Added Phase 3 sprint tracking entries.
Patched Phase 3 planning docs for canon consistency and first-playable scope discipline.

### Current state
- Phase 1: complete
- Phase 2: complete
- Phase 3 bootstrap: complete
- Sprint 0: ready to begin

### Next step
Sprint 0 — Unity scaffold + local-first persistence shell
