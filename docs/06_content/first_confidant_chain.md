# First Confidant Chain

**Status:** Canon — Phase 2B Draft  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** MVP placeholder Confidants for Loamwake. Defines Greta and Mossvane, their Confidence Trails, Trust model, Calling stubs, and all Duty chain content. Dependency: `loamwake_mvp_content_sheet.md`.

---

## 1. Purpose and Dependency Declaration

This file defines the first two playable Confidants for the Loamwake MVP. It is a **Phase 2B content root** — all Confidant-related content in subsequent files must reference IDs locked here.

**This file depends on the following already-locked IDs from `loamwake_mvp_content_sheet.md`:**

| source_id | type | source_file | used_for |
|-----------|------|-------------|----------|
| `zone_lw_001_rootvine_shelf` | zone | loamwake_mvp_content_sheet.md | Greta + Mossvane Duty zones |
| `zone_lw_002_mudpipe_hollow` | zone | loamwake_mvp_content_sheet.md | Greta Duty zones; Rootrail reveal |
| `zone_lw_003_glowroot_passage` | zone | loamwake_mvp_content_sheet.md | Mossvane Duty zone |
| `dty_lw_003_greta_rail_lead` | duty (story) | loamwake_mvp_content_sheet.md | Greta chain step 3 (cross-file reference) |
| `dty_lw_004_find_the_manual` | duty (story) | loamwake_mvp_content_sheet.md | Optional Greta post-chain cross-reference |
| `fman_001_terminal_routing` | forgotten_manual | loamwake_mvp_content_sheet.md | Greta chain context; Rootrail gate |
| `rtr_station_lw_001_loamwake_terminal` | rootrail_station | loamwake_mvp_content_sheet.md | Greta Calling context |
| `tangled_root_twine` | material | loamwake_mvp_content_sheet.md | Duty + Calling rewards |
| `crumbled_ore_chunk` | material | loamwake_mvp_content_sheet.md | Duty rewards |
| `dull_glow_shard` | material | loamwake_mvp_content_sheet.md | Mossvane Duty rewards |
| `loam_fiber` | material | loamwake_mvp_content_sheet.md | Duty rewards |
| `mudglass_chip` | material | loamwake_mvp_content_sheet.md | Mossvane Duty rewards |
| `bleached_rail_fragment` | material | loamwake_mvp_content_sheet.md | Greta Duty rewards |
| `dewcap_sporewax` | material | loamwake_mvp_content_sheet.md | Mossvane Duty rewards |
| `rootrail_parts` | material/wallet | loamwake_mvp_content_sheet.md | Greta Duty rewards |

**No new Loamwake zones, materials, or Rootrail IDs are introduced in this file.**

---

## 2. Canon and Placeholder Rules

| Rule | Applied As |
|------|-----------|
| Confidants are NOT Burrowfolk | Separate systems; Confidants have Duty chains and Trust bonds; Burrowfolk have Burrow work queues |
| MVP allows placeholder Confidants | `placeholder_flag: true` on all rows in this file |
| No final friend-questionnaire content | No personal lore, relationship history, or questionnaire-derived personality |
| No Clothes, no Ascender | No references to removed canon items |
| Greta bridges dty_lw_003_greta_rail_lead | That story Duty is defined in loamwake_mvp_content_sheet.md; this file references it, does not redefine it |
| All Duty rewards use locked Loamwake IDs | No invented materials, zones, or IDs in reward packets |
| Placeholder names are gnome-world-flavored | Greta, Mossvane — readable but not final |

> **PLACEHOLDER RULE:** Every row in this file with `placeholder_flag: true` must be replaced before a content-locked release build. The replacement source is the friend-questionnaire system (not yet designed). Do not treat these rows as final.

---

## 3. Trust Scale — MVP Bond Model

The Confidant Trust scale is a 0–5 integer range, consistent with `data_schema_v1.md` (`confidant_state.trust_level`).

| trust_level | label | meaning | calling_unlocked |
|-------------|-------|---------|-----------------|
| 0 | Not Bonded | Confidant exists but has not been introduced | false |
| 1 | Introduced | First Duty completed; bond is active | false |
| 2 | Trusted | Second Duty completed; Calling stub unlocks | **true** |
| 3 | Confident | MVP chain complete; full chain confidence reached | true |
| 4 | (Reserved) | Future replacement chain space | true |
| 5 | (Reserved) | Final questionnaire-content cap | true |

**Trust point accumulation rules (MVP):**
- Each completed Confidant Duty awards `+1 trust_point` toward the next level.
- Trust level advances when `trust_points >= trust_threshold` (MVP threshold: 1 point per level for simplicity).
- Trust points do NOT decay.
- `last_interaction_at` updates on every Duty completion or Calling trigger.

**State fields used** (from `confidant_state` in `data_schema_v1.md`):
```
trust_level: int          // 0–5
trust_points: int         // accumulator toward next level
calling_unlocked: bool    // true once trust_level >= 2
active_duty_chain_ids: list[string]
completed_duty_ids: list[string]
last_interaction_at: timestamp
```

---

## 4. Confidant Definition Table

| cnf_id | display_name | backstory_hook | role_tag | bond_stat_focus | max_bond_level | duty_chain_ids | unlock_condition | placeholder_flag | replacement_note | mvp_status |
|--------|-------------|---------------|---------|----------------|--------------|---------------|-----------------|-----------------|-----------------|-----------|
| `cnf_001_placeholder_greta` | Greta (placeholder) | A Loamwake Pathfinder who reads rail scars in mud and points the Survivor toward the old terminal before the hollow shifts again. | Pathfinder | craft | 5 | `dty_cnf001_01_shelf_scratches; dty_cnf001_02_hollow_measure; dty_lw_003_greta_rail_lead` | `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf` | true | Pending friend-questionnaire replacement | launch |
| `cnf_002_placeholder_mossvane` | Mossvane (placeholder) | A sporewax mapkeeper with half-useful Glowroot notes and a habit of pricing rumors like produce. | Archivist | lore | 5 | `dty_cnf002_01_sporewax_trade; dty_cnf002_02_map_the_glowroot; dty_cnf002_03_parts_for_a_route` | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | true | Pending friend-questionnaire replacement | launch |

### Confidant Notes

**Greta:**
- Introduced via a Burrow Post in the first 15–20 minutes of play (per `core_loop_mvp_spec.md` 60-minute arc).
- Her chain naturally leads into the cross-file story Duty `dty_lw_003_greta_rail_lead`, which is the Rootrail reveal trigger.
- She should NOT appear in zone_lw_003 before the Warden clear — her role is Mudpipe Hollow-focused.

**Mossvane:**
- Introduced after first Warden clear of `wdn_lw_001_mudgrip`.
- Her role is distinctly trade/archive/material-oriented — she does not replicate Greta's Rootrail discovery role.
- She supports post-Warden Glowroot Passage engagement and Rootrail Parts accumulation.

---

## 5. Calling Unlock Table

Callings are per-Confidant special abilities that unlock at Trust Level 2. All MVP Callings are stubs — they provide a small hint or passive effect, not a large power bonus.

| confidant_id | calling_key | calling_display | unlock_trust_level | unlock_duty_id | mvp_effect | state_mutation | mvp_status |
|-------------|------------|----------------|-------------------|---------------|-----------|---------------|-----------|
| `cnf_001_placeholder_greta` | `calling_greta_rail_sense` | Rail Sense | 2 | `dty_lw_003_greta_rail_lead` | Greta provides a passive hint icon on the Rootrail station screen when `wallet.rootrail_parts < 10`, indicating she knows a shortcut. No mechanical bonus — UI flavor hint only. | `confidant_state.cnf_001_placeholder_greta.calling_unlocked = true` | launch |
| `cnf_002_placeholder_mossvane` | `calling_mossvane_market_eye` | Market Eye | 2 | `dty_cnf002_02_map_the_glowroot` | Mossvane reveals one Wanderer trade offer per day in zone_lw_003 that is otherwise hidden (UI stub: "Mossvane tipped you off"). No material bonus — discovery flavor only. | `confidant_state.cnf_002_placeholder_mossvane.calling_unlocked = true` | launch |

> **STUB NOTE:** Both Callings are pure UI/flavor stubs for MVP. Final Callings will be designed when the Confidant system is fully implemented. Do not implement mechanical bonuses based on these stubs.

---

## 6. Greta Confidence Trail — Overview

Greta's chain is ordered around Loamwake exploration and the Rootrail reveal arc. Step 3 (`dty_lw_003_greta_rail_lead`) is a **cross-file Loamwake story Duty** — it is defined in `loamwake_mvp_content_sheet.md` and referenced here without being redefined.

```
Step 1: dty_cnf001_01_shelf_scratches       [Confidant-owned; defined in this file]
Step 2: dty_cnf001_02_hollow_measure        [Confidant-owned; defined in this file]
Step 3: dty_lw_003_greta_rail_lead          [CROSS-FILE; defined in loamwake_mvp_content_sheet.md]
(Optional ref): dty_lw_004_find_the_manual  [CROSS-FILE; Greta comments, not her chain step]
```

**Thematic arc:** Greta starts by noticing surface signs of rail activity (shelf scratches), moves to direct hollow survey work, then leads the Rootrail discovery event. Her chain is complete at Trust 3 once the Rootrail station is revealed.

---

## 7. Greta Duty Definition Rows

Only `dty_cnf001_*` rows are defined here. `dty_lw_003_greta_rail_lead` is referenced but NOT redefined.

| dty_id | display_name | duty_type | strata_id | unlock_condition | objective | reward | repeatable | repeat_cadence | confidant_id | chain_next_dty_id | placeholder_flag | trust_points_delta | mvp_status |
|--------|-------------|----------|---------|-----------------|---------|-------|-----------|--------------|------------|-----------------|-----------------|-------------------|-----------|
| `dty_cnf001_01_shelf_scratches` | Read the Shelf Scratches | confidant | loamwake | `confidant_state.cnf_001_placeholder_greta.unlocked == true` | Explore Rootvine Shelf 2 times and report findings to Greta | `{mooncaps:80, fixture_material.tangled_root_twine:4, fixture_material.bleached_rail_fragment:1}` | false | once | `cnf_001_placeholder_greta` | `dty_cnf001_02_hollow_measure` | true | +1 | launch |
| `dty_cnf001_02_hollow_measure` | Measure the Hollow | confidant | loamwake | `duty_progress.dty_cnf001_01_shelf_scratches.state == completed` | Explore Mudpipe Hollow 2 times and find 1 encounter (any type) | `{mooncaps:100, rootrail_parts:3, fixture_material.crumbled_ore_chunk:3}` | false | once | `cnf_001_placeholder_greta` | `dty_lw_003_greta_rail_lead` | true | +1 | launch |

### Cross-file reference (not redefined):

> `dty_lw_003_greta_rail_lead` — **Defined in `loamwake_mvp_content_sheet.md` Section 18.** This Loamwake story Duty is step 3 of Greta's chain. On completion it: (a) triggers the Rootrail station reveal, (b) advances Greta to `trust_level: 3`, and (c) unlocks `calling_greta_rail_sense` (via the trust level reaching 2 at step 3 completion). Reward: `{mooncaps:80, rootrail_parts:5}`.

---

## 8. Mossvane Confidence Trail — Overview

Mossvane's chain is oriented around Loamwake's trade/material/archive layer. She does not discover the Rootrail — she helps the player engage with what already exists in Glowroot Passage and the Wanderer economy.

```
Step 1: dty_cnf002_01_sporewax_trade        [Confidant-owned; defined in this file]
Step 2: dty_cnf002_02_map_the_glowroot      [Confidant-owned; defined in this file]  ← Calling unlocks here
Step 3: dty_cnf002_03_parts_for_a_route     [Confidant-owned; defined in this file]
```

**Thematic arc:** Mossvane starts with a simple trade introduction, then helps the player navigate Glowroot Passage for her own archive purposes, then turns to Rootrail Parts accumulation as a practical goal. Her chain reinforces daily-return material loop engagement.

---

## 9. Mossvane Duty Definition Rows

| dty_id | display_name | duty_type | strata_id | unlock_condition | objective | reward | repeatable | repeat_cadence | confidant_id | chain_next_dty_id | placeholder_flag | trust_points_delta | mvp_status |
|--------|-------------|----------|---------|-----------------|---------|-------|-----------|--------------|------------|-----------------|-----------------|-------------------|-----------|
| `dty_cnf002_01_sporewax_trade` | The Sporewax Trade | confidant | loamwake | `confidant_state.cnf_002_placeholder_mossvane.unlocked == true` | Trade with 1 Wanderer in any Loamwake zone and collect Dewcap Sporewax | `{mooncaps:70, fixture_material.dewcap_sporewax:3, fixture_material.loam_fiber:4}` | false | once | `cnf_002_placeholder_mossvane` | `dty_cnf002_02_map_the_glowroot` | true | +1 | launch |
| `dty_cnf002_02_map_the_glowroot` | Map the Glowroot Passage | confidant | loamwake | `duty_progress.dty_cnf002_01_sporewax_trade.state == completed` | Explore Glowroot Passage 3 times and resolve 1 Runaway or Wanderer encounter | `{mooncaps:110, fixture_material.dull_glow_shard:4, fixture_material.mudglass_chip:2}` | false | once | `cnf_002_placeholder_mossvane` | `dty_cnf002_03_parts_for_a_route` | true | +1 | launch |
| `dty_cnf002_03_parts_for_a_route` | Parts for a Route | confidant | loamwake | `duty_progress.dty_cnf002_02_map_the_glowroot.state == completed` | Accumulate 15 Rootrail Parts across any Loamwake zones | `{mooncaps:130, rootrail_parts:5, fixture_material.bleached_rail_fragment:2}` | false | once | `cnf_002_placeholder_mossvane` | null | true | +1 | launch |

---

## 10. Trust Progression Trigger Table

Maps each Duty completion to Trust state changes. Used by the gate evaluator to update `confidant_state`.

| duty_id | confidant_id | trust_points_delta | trust_level_after_claim | calling_unlocked_after_claim | notes |
|---------|-----------|--------------------|------------------------|------------------------------|-------|
| `dty_cnf001_01_shelf_scratches` | `cnf_001_placeholder_greta` | +1 | 1 | false | Greta introduced; trust_level advances 0→1 |
| `dty_cnf001_02_hollow_measure` | `cnf_001_placeholder_greta` | +1 | 2 | false | trust_level advances 1→2; Calling unlocks at step 3 |
| `dty_lw_003_greta_rail_lead` | `cnf_001_placeholder_greta` | +1 | 3 | **true** | Rootrail revealed; trust_level 2→3; `calling_greta_rail_sense` unlocks |
| `dty_cnf002_01_sporewax_trade` | `cnf_002_placeholder_mossvane` | +1 | 1 | false | Mossvane introduced; trust_level advances 0→1 |
| `dty_cnf002_02_map_the_glowroot` | `cnf_002_placeholder_mossvane` | +1 | 2 | **true** | trust_level 1→2; `calling_mossvane_market_eye` unlocks |
| `dty_cnf002_03_parts_for_a_route` | `cnf_002_placeholder_mossvane` | +1 | 3 | true | trust_level 2→3; MVP chain complete |

> **Implementation note:** `calling_unlocked` in `confidant_state` should be set to `true` when `trust_level` first reaches 2, regardless of which specific Duty triggers that level advance. The trust trigger table above shows the expected path; the gate evaluator should use `trust_level >= 2` as the Calling gate, not a specific Duty ID.

---

## 11. Reward Reference Table

All rewards in this file use material and resource IDs already locked in `loamwake_mvp_content_sheet.md`. No new IDs are introduced.

| reward_item | type | locked_in | used_in_duties |
|------------|------|-----------|---------------|
| `mooncaps` | wallet | core schema | all Duties |
| `rootrail_parts` | wallet | loamwake_mvp_content_sheet.md §5 | dty_cnf001_02, dty_lw_003_greta_rail_lead, dty_cnf002_03 |
| `tangled_root_twine` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf001_01 |
| `crumbled_ore_chunk` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf001_02 |
| `bleached_rail_fragment` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf001_01, dty_cnf002_03 |
| `loam_fiber` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf002_01 |
| `dewcap_sporewax` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf002_01 |
| `dull_glow_shard` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf002_02 |
| `mudglass_chip` | fixture_material | loamwake_mvp_content_sheet.md §5 | dty_cnf002_02 |

> **Validation:** No reward in any Duty row in this file references a material, zone, or ID not present in the table above. No cross-Strata or cross-phase IDs are used.

---

## 12. Schema and State Mapping

### Content definition fields (this file authors these)

| field | schema entity | notes |
|-------|--------------|-------|
| `cnf_id` | `confidant` | Primary key |
| `display_name` | `confidant` | |
| `backstory_hook` | `confidant` | |
| `bond_stat_focus` | `confidant` | |
| `max_bond_level` | `confidant` | Fixed at 5 for all MVP Confidants |
| `duty_chain_ids` | `confidant` | Ordered list; includes cross-file Loamwake IDs |
| `unlock_condition` | `confidant` | Gate expression |
| `placeholder_flag` | `confidant` | `true` on all rows |
| `replacement_note` | `confidant` | "Pending friend-questionnaire replacement" |
| `mvp_status` | `confidant` | `launch` |
| `dty_id` | `duty` | Primary key |
| `duty_type` | `duty` | `confidant` for all new rows |
| `confidant_id` | `duty` | FK → confidant |
| `chain_next_dty_id` | `duty` | Next step; null at chain end |
| `placeholder_flag` | `duty` | File-local implementation helper |
| `trust_points_delta` | `duty` | File-local implementation helper |

### Player state fields (implemented by backend, not authored here)

| field | schema entity | notes |
|-------|--------------|-------|
| `unlocked` | `confidant_state` | Set true on first Duty accept |
| `unlocked_at` | `confidant_state` | Timestamp |
| `trust_level` | `confidant_state` | 0–5 int |
| `trust_points` | `confidant_state` | Accumulator |
| `active_duty_chain_ids` | `confidant_state` | Drives progress UI |
| `completed_duty_ids` | `confidant_state` | For gate resolution |
| `calling_unlocked` | `confidant_state` | true when trust_level >= 2 |
| `last_interaction_at` | `confidant_state` | Updated on Duty completion |
| `state` | `duty_progress` | `available / active / completed / locked` |
| `objective_progress` | `duty_progress` | Objective key → current count |
| `reward_claimed` | `duty_progress` | Bool; must be true before trust_points_delta applies |
| `chain_next_dty_id` | `duty_progress` | Copied from definition for fast lookup |

---

## 13. Validation Checklist

- [x] `cnf_001_placeholder_greta` row defined with all required Confidant Template columns
- [x] `cnf_002_placeholder_mossvane` row defined with all required Confidant Template columns
- [x] Both rows have `placeholder_flag: true`
- [x] Both rows have `replacement_note: "Pending friend-questionnaire replacement"`
- [x] Both rows have `max_bond_level: 5`
- [x] Trust scale defined as 0–5; matches `confidant_state.trust_level` in `data_schema_v1.md`
- [x] Greta chain has 3 steps including cross-file `dty_lw_003_greta_rail_lead`
- [x] Mossvane chain has 3 steps; all defined in this file
- [x] `dty_lw_003_greta_rail_lead` is referenced but NOT redefined; its rewards remain as specified in `loamwake_mvp_content_sheet.md`
- [x] Greta and Mossvane have clearly distinct roles (Pathfinder/Rootrail vs. Archivist/Trade)
- [x] Mossvane does NOT duplicate the Rootrail reveal role
- [x] All Duty rewards use material IDs locked in `loamwake_mvp_content_sheet.md`
- [x] No new Loamwake zones, materials, or Rootrail IDs invented
- [x] Calling stubs defined for both Confidants; both are flavor/UI only
- [x] Callings unlock at trust_level 2 for both Confidants
- [x] Trust Progression Trigger Table covers all Duty steps for both chains
- [x] No Clothes, Ascender, rigid slots, or War Armory content
- [x] No final friend-questionnaire personal lore or dialogue invented
- [x] All Duty IDs use correct prefixes: `dty_cnf001_*` (Greta) and `dty_cnf002_*` (Mossvane)
- [x] Schema and State Mapping table distinguishes content-definition fields from player-state fields
- [x] `mvp_status: launch` set on all active rows

---

## 14. Unresolved Items

> **UNRESOLVED:** Calling implementation scope — both MVP Callings are UI/flavor stubs (hint icons, discovery flags). The full Calling mechanic (mechanical bonus, activation cost, cooldown) is not designed. This file defines the unlock trigger only. Flag for a future Confidant system design doc.

> **UNRESOLVED:** Confidant unlock flow — Greta's unlock condition (`strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf`) fires very early, but the Burrow Post introduction moment (per `core_loop_mvp_spec.md` 15–20 min beat) needs a specific post ID. A `post_lw_005_greta_intro` Burrow Post may be needed in a future patch. Flag for `first_wanderer_pool.md` or a Burrow Post expansion pass.

> **UNRESOLVED:** Trust point threshold — MVP uses 1 trust_point per level (flat). If the game feels too fast to max bond, the threshold table may need to be: Level 1: 1pt, Level 2: 2pt, Level 3: 3pt. This should be validated in playtesting before final.

> **UNRESOLVED:** `dty_cnf001_02_hollow_measure` unlock_condition depends on `duty_progress.dty_cnf001_01_shelf_scratches.state == completed`. The gate evaluator must support `duty_progress.<dty_id>.state` expressions for Confidant Duty chaining. Flag for `save_state_and_profile_flow.md`.

> **UNRESOLVED:** Greta's Calling (`Rail Sense`) references a UI hint on the Rootrail station screen. The Rootrail station UI spec is not yet defined. The Calling implementation must wait for `docs/07_ui/` Rootrail screen design. Flag for UI pass.

---

## 15. Replacement Notes

When the friend-questionnaire system is ready to replace placeholder Confidants:

1. Replace `backstory_hook`, `role_tag`, `display_name` with questionnaire-derived values.
2. Set `placeholder_flag: false`.
3. Replace Duty chain flavor text and objectives with questionnaire-personalized content.
4. Keep the same `cnf_id` values — IDs are stable once published.
5. Calling mechanics may be redesigned at replacement time; coordinate with the UI and systems teams.
6. Do not change `bond_stat_focus`, `max_bond_level`, or `duty_chain_ids` structure without a full Confidant system review.

---

## 16. Next Recommended Phase 2B File

**Next file:** `docs/06_content/first_wanderer_pool.md`

This file depends on:
- Zone IDs from `loamwake_mvp_content_sheet.md` Section 6
- Wanderer IDs partially defined in `loamwake_mvp_content_sheet.md` Section 15
- Mossvane's trade Duty (`dty_cnf002_01_sporewax_trade`) references Wanderer trade interactions that should be pooled and expanded in that file

The Wanderer pool file should also define the introductory Burrow Post for Greta (resolving the UNRESOLVED item in Section 14 above).
