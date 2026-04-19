# Loamwake MVP Content Sheet

**Status:** Canon — Phase 2B Draft  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** All playable Loamwake MVP content. Dependency root for Phase 2B. All later Phase 2B files must reference IDs defined here.

---

## 1. Status, Version, Scope

This file is the **first real Phase 2B content file**. It locks every ID, material name, zone, encounter, Warden, recipe, Duty chain, and Burrow Post for the Loamwake MVP layer.

No Phase 2B file may introduce Loamwake zone IDs, material IDs, or encounter IDs that are not defined here without first updating this sheet.

**What this file authorizes:**
- 3 launch zones (zone_lw_001–003)
- 8 canonical Loamwake material names
- 1 Warden (wdn_lw_001_mudgrip)
- 4 Buried Clues
- 1 Forgotten Manual (fman_001_terminal_routing)
- 1 Rootrail station hook (rtr_station_lw_001)
- 5 Fixture definitions (fix_001–005)
- 5 Fixture recipes (fix_rec_001–005)
- 6 Wanderers, 4 Runaways, 4 Wild Ones
- 3 encounter pool tables
- 1 story Duty chain (4 Duties) + 3 daily Duties
- 4 Burrow Posts

**What this file does NOT cover:**
- War Fixtures or War Armory content
- Strata 2–5 content
- Final Confidant/Burrowfolk questionnaire content
- Full Rootrail repair chain (steps 2+)
- Events or Festival Ledger content

---

## 2. Canon Rules Applied

All content in this file follows the Phase 2A canon lock:

| Rule | Applied As |
|------|-----------|
| No Clothes | All equipables are Fixtures |
| No rigid slots | Fixtures in ordered list; no slot labels |
| No negative stats | All Fixture effects are positive only |
| Personal cap path | 0→1→2 in MVP session; never expose full 12 cap |
| Hats are not Fixtures | Hat not in personal cap count |
| War Fixtures separate | No War Fixtures in this file |
| Rootrail, not Ascender | Rootrail system used throughout |
| Forgotten Manuals permanent | `manual_consumed` = false; separate from Elder Books |
| Warden IDs use `wdn_` | Not `war_` |
| War Fixture IDs use `wfx_` | Not touched in this file |

---

## 3. Content Dependency Order

Sections are ordered so each depends only on sections above it:

1. Material Naming Lock — everything depends on these names
2. Launch Zone Table — Wardens, encounters, Duties, and Posts reference zone IDs
3. Encounter Pool Tables — zones reference `encounter_table_id`
4. Warden Table — references zone ID
5. Buried Clue Table — references zones
6. Forgotten Manual — references the clue that grants it
7. Rootrail Station Hook — references the manual
8. Fixture Definitions — references material IDs
9. Fixture Recipes — references material IDs and Fixture IDs
10. Duty Chain — references zones, materials, Fixtures, Rootrail hook
11. Burrow Posts — references zones, materials, Rootrail Parts

---

## 4. Loamwake Aesthetic Direction

> **LOCKED:** The Loamwake aesthetic is underground ruin with surface-break fungal intrusion. Rail tracks are buried under loam, root systems, and crumbled mineral deposits. The station ruins are partially surface-exposed with vines and spore growth. The gnome-world post-fallout tone is wry survival humor + genuine eerie wonder. Nothing is grimdark; everything is slightly absurd and quietly beautiful.

---

## 5. Material Naming Lock

These are the **canonical Loamwake material names**. All provisional notes in other files are resolved here. Use these IDs everywhere.

| material_id | display_name | Primary Sink | Source |
|-------------|-------------|-------------|--------|
| `tangled_root_twine` | Tangled Root Twine | Farming Fixture recipes; Burrow Posts | zone_lw_001 explore drops; Burrowfolk output |
| `crumbled_ore_chunk` | Crumbled Ore Chunk | Pure Attack Fixture recipes | zone_lw_002 explore drops; Rootnip output |
| `dull_glow_shard` | Dull Glow Shard | Strain Fixture recipes; clue rewards | zone_lw_003 explore drops; Buried Clue reward |
| `loam_fiber` | Loam Fiber | Early crafting recipes; Burrow Posts | zone_lw_001 explore drops; Mudger output |
| `mudglass_chip` | Mudglass Chip | Mid-Loamwake recipes; Warden-adjacent rewards | zone_lw_002 + zone_lw_003; first Warden clear |
| `bleached_rail_fragment` | Bleached Rail Fragment | Rootrail-adjacent crafting; codex flavor | zone_lw_002 rare drop; Buried Clue reward |
| `rusted_rail_pin` | Rusted Rail Pin | Rootrail repair flavor; Burrow Post reward | zone_lw_001 rare drop; clue_lw_001 |
| `dewcap_sporewax` | Dewcap Sporewax | Burrow Post recipes; utility secondary | zone_lw_003 drop; Wanderer trade |

> **RESOLVED:** All Loamwake material names are now canonical. The provisional note in `core_loop_mvp_spec.md` is superseded by this table.

---

## 6. Launch Zone Table

All Loamwake MVP launch zones. Zone order determines exploration sequence.

| zone_id | display_name | strata_id | zone_order | unlock_condition | mushcap_cost | encounter_table_id | warden_id | mvp_status |
|---------|-------------|-----------|----------|-----------------|-------------|-------------------|----------|-----------|
| `zone_lw_001_rootvine_shelf` | Rootvine Shelf | loamwake | 1 | `always` | 2 | `enc_table_lw_001` | null | launch |
| `zone_lw_002_mudpipe_hollow` | Mudpipe Hollow | loamwake | 2 | `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf and duty_progress.dty_lw_001_clear_rootvine.state == completed` | 3 | `enc_table_lw_002` | `wdn_lw_001_mudgrip` | launch |
| `zone_lw_003_glowroot_passage` | Glowroot Passage | loamwake | 3 | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | 4 | `enc_table_lw_003` | null | launch |

### Zone Flavor Notes

| zone_id | ambient_flavor |
|---------|---------------|
| `zone_lw_001_rootvine_shelf` | Dense root-shelf terrain. Loam fiber catches on everything. Rusted spikes jut from collapsed track segments. |
| `zone_lw_002_mudpipe_hollow` | A wide hollow carved by old drainage pipes. The rail bed is still visible. The Mudgrip patrols the far end. |
| `zone_lw_003_glowroot_passage` | A narrow passage lit by bioluminescent root clusters. Glow shards flake off the walls. The air smells of sporemoss. |

---

## 7. Zone Drop Table

Resource drop ranges per explore action. Values are per-tap ranges (min–max units).

| zone_id | drop_id | drop_min | drop_max | drop_weight | notes |
|---------|---------|---------|---------|------------|-------|
| `zone_lw_001_rootvine_shelf` | `tangled_root_twine` | 4 | 8 | 40 | Primary early material |
| `zone_lw_001_rootvine_shelf` | `loam_fiber` | 3 | 6 | 35 | Secondary common |
| `zone_lw_001_rootvine_shelf` | `rusted_rail_pin` | 1 | 2 | 15 | Rare; flavor + Rootrail |
| `zone_lw_001_rootvine_shelf` | `rootrail_parts` | 0 | 2 | 10 | Rare; stamina-building |
| `zone_lw_002_mudpipe_hollow` | `crumbled_ore_chunk` | 3 | 6 | 40 | Primary mid material |
| `zone_lw_002_mudpipe_hollow` | `bleached_rail_fragment` | 1 | 3 | 25 | Rare; Rootrail-adjacent |
| `zone_lw_002_mudpipe_hollow` | `mudglass_chip` | 2 | 4 | 25 | Mid recipe input |
| `zone_lw_002_mudpipe_hollow` | `rootrail_parts` | 1 | 3 | 10 | Common here; drives Rootrail project |
| `zone_lw_003_glowroot_passage` | `dull_glow_shard` | 3 | 5 | 40 | Primary Strain material |
| `zone_lw_003_glowroot_passage` | `dewcap_sporewax` | 2 | 4 | 35 | Utility material |
| `zone_lw_003_glowroot_passage` | `mudglass_chip` | 1 | 3 | 15 | Secondary mid |
| `zone_lw_003_glowroot_passage` | `rootrail_parts` | 1 | 2 | 10 | Consistent low yield |

---

## 8. Encounter Pool Tables

Three encounter pool tables, one per zone. Tables reference Wanderer, Runaway, Wild One, and Buried Clue IDs defined in later sections.

> **Column guide:** `encounter_id` = the spawnable entity or event; `weight` = relative spawn weight (higher = more common); `trigger` = what action fires the check.

### enc_table_lw_001 — Rootvine Shelf

| enc_table_id | encounter_id | encounter_type | weight | trigger |
|-------------|-------------|---------------|--------|---------|
| `enc_table_lw_001` | `wnd_lw_001_grumbling_trader` | wanderer | 25 | explore_action |
| `enc_table_lw_001` | `wnd_lw_002_rail_scrap_picker` | wanderer | 20 | explore_action |
| `enc_table_lw_001` | `run_lw_001_startled_rootpup` | runaway | 20 | explore_action |
| `enc_table_lw_001` | `wld_lw_001_rootmaw_grub` | wild_one | 15 | explore_action |
| `enc_table_lw_001` | `clue_lw_001_bent_rail_spike` | buried_clue | 10 | explore_action |
| `enc_table_lw_001` | `clue_lw_002_terminal_routing_cache` | buried_clue | 10 | explore_action |

### enc_table_lw_002 — Mudpipe Hollow

| enc_table_id | encounter_id | encounter_type | weight | trigger |
|-------------|-------------|---------------|--------|---------|
| `enc_table_lw_002` | `wnd_lw_003_pipe_surveyor` | wanderer | 20 | explore_action |
| `enc_table_lw_002` | `wnd_lw_004_mud_courier` | wanderer | 20 | explore_action |
| `enc_table_lw_002` | `run_lw_002_lost_mudpup` | runaway | 20 | explore_action |
| `enc_table_lw_002` | `wld_lw_002_hollow_scraper` | wild_one | 20 | explore_action |
| `enc_table_lw_002` | `wld_lw_003_pipe_rattler` | wild_one | 10 | explore_action |
| `enc_table_lw_002` | `clue_lw_003_glowroot_marker` | buried_clue | 10 | explore_action |

### enc_table_lw_003 — Glowroot Passage

| enc_table_id | encounter_id | encounter_type | weight | trigger |
|-------------|-------------|---------------|--------|---------|
| `enc_table_lw_003` | `wnd_lw_005_spore_merchant` | wanderer | 25 | explore_action |
| `enc_table_lw_003` | `wnd_lw_006_passage_mapmaker` | wanderer | 20 | explore_action |
| `enc_table_lw_003` | `run_lw_003_shy_glowcrawler` | runaway | 15 | explore_action |
| `enc_table_lw_003` | `run_lw_004_fleeing_tinker` | runaway | 10 | explore_action |
| `enc_table_lw_003` | `wld_lw_004_glow_biter` | wild_one | 20 | explore_action |
| `enc_table_lw_003` | `clue_lw_004_mudpipe_handprint` | buried_clue | 10 | explore_action |

---

## 9. Warden Table

### First Warden: The Mudgrip

| Field | Value |
|-------|-------|
| `warden_id` | `wdn_lw_001_mudgrip` |
| `display_name` | The Mudgrip |
| `flavor_title` | "Shaper of the Hollow" |
| `strata_id` | loamwake |
| `zone_id` | `zone_lw_002_mudpipe_hollow` |
| `unlock_condition` | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow and duty_progress.dty_lw_002_hollow_survey.state == completed` |
| `battle_stat_difficulty` | 3 |
| `required_battle_stat` | force |
| `recommended_fixture_cap` | 2 |
| `mvp_status` | launch |

**First-clear reward:**
```
{
  mooncaps: 300,
  strain_seeds: 15,
  fixture_material.crumbled_ore_chunk: 8,
  fixture_material.mudglass_chip: 4,
  hat_id: hat_001_loamwake_dirt_cap
}
```

**Repeat-clear reward:**
```
{
  mooncaps: 50,
  fixture_material.crumbled_ore_chunk: 3,
  rootrail_parts: 2
}
```

**Lore note:** The Mudgrip is a large mineral-crusted hollow creature that has fused itself into the collapsed far-end of Mudpipe Hollow. It isn't malicious — just fiercely territorial. Greta remarks: *"It didn't choose this. The hollow shaped it."*

---

## 10. Buried Clue Table

All Buried Clues that can surface in Loamwake zones.

| clue_id | display_name | zone_id | discovery_trigger | discovery_chance | mvp_status |
|---------|-------------|---------|------------------|-----------------|-----------|
| `clue_lw_001_bent_rail_spike` | Bent Rail Spike | `zone_lw_001_rootvine_shelf` | explore_action | 0.12 | launch |
| `clue_lw_002_terminal_routing_cache` | Terminal Routing Cache | `zone_lw_001_rootvine_shelf` | explore_action | 0.08 | launch |
| `clue_lw_003_glowroot_marker` | Glowroot Boundary Marker | `zone_lw_002_mudpipe_hollow` | explore_action | 0.10 | launch |
| `clue_lw_004_mudpipe_handprint` | Mudpipe Worker's Handprint | `zone_lw_003_glowroot_passage` | explore_action | 0.10 | launch |

### Buried Clue Reward Packets and Notes

| clue_id | reward | forgotten_manual_id | lore_note |
|---------|--------|-------------------|-----------|
| `clue_lw_001_bent_rail_spike` | `{fixture_material.rusted_rail_pin: 3, rootrail_parts: 2}` | null | A rail spike bent at a perfect right angle. Someone knew exactly what they were doing — and then fled. |
| `clue_lw_002_terminal_routing_cache` | `{forgotten_manual_id: fman_001_terminal_routing, fixture_material.bleached_rail_fragment: 2}` | `fman_001_terminal_routing` | A wax-sealed tin punched into a root hollow. Inside: a routing index, barely water-damaged. |
| `clue_lw_003_glowroot_marker` | `{fixture_material.dull_glow_shard: 3, mooncaps: 40}` | null | A carved marker reading: "PASSAGE CLEAR — GLOW SECTOR." Someone maintained this route long after the fallout. |
| `clue_lw_004_mudpipe_handprint` | `{fixture_material.dewcap_sporewax: 4, mooncaps: 60}` | null | A small gnome-sized handprint in dried pipe-mud. Next to it: a tally of seven scratches. Days? Loads? |

---

## 11. Forgotten Manual Table

### First Forgotten Manual: Terminal Routing Manual

| Field | Value |
|-------|-------|
| `manual_id` | `fman_001_terminal_routing` |
| `title` | Loamwake Terminal Routing Manual |
| `codex_category` | Rail Knowledge |
| `knowledge_domain` | rail_routing |
| `acquisition_source` | Buried Clue `clue_lw_002_terminal_routing_cache` in `zone_lw_001_rootvine_shelf` |
| `unlock_condition` | `rootrail_state.rootrail_unlocked == true` |
| `permanent_unlock_type` | rootrail_step_gate |
| `permanent_unlock_id` | `rtr_step_001` |
| `rootrail_step_gate_ids` | `rtr_step_001` |
| `fixture_recipe_ids` | null |
| `codex_entry_summary` | *"A water-damaged routing index for the lower Loamwake terminal. The margin notes suggest someone was still running trains here long after everyone else had given up."* |
| `discovered_in_strata` | loamwake |
| `separate_from_elder_books_confirmed` | true |
| `consumed` | false |
| `mvp_status` | launch |

> **RESOLVED:** `fman_001_terminal_routing` is the canonical first Forgotten Manual. The provisional title is confirmed. It is permanently unlocked on discovery (not consumed). It is the sole gate for `rtr_step_001`.

---

## 12. Rootrail Station and Repair Step 1 Hook

This section defines the MVP-visible Rootrail presence in Loamwake. Full Rootrail network content is out of scope for this file.

### Station Definition

| Field | Value |
|-------|-------|
| `station_id` | `rtr_station_lw_001_loamwake_terminal` |
| `display_name` | Loamwake Terminal |
| `ui_label` | Rootrail Terminal — Loamwake Station |
| `strata_id` | loamwake |
| `reveal_condition` | `duty_progress.dty_lw_003_greta_rail_lead.state == completed` |
| `first_route_id` | `route_loamwake_01` |
| `mvp_status` | launch |

### Repair Step 1 Row

| Field | Value |
|-------|-------|
| `rtr_step_id` | `rtr_step_001` |
| `route_id` | `route_loamwake_01` |
| `step_order` | 1 |
| `step_name` | Clear the Platform |
| `repair_phase` | loamwake_terminal |
| `unlock_condition` | `rootrail_state.rootrail_unlocked == true` |
| `rootrail_parts_cost` | 20 |
| `forgotten_manual_required` | true |
| `forgotten_manual_id` | `fman_001_terminal_routing` |
| `manual_consumed` | false |
| `timer_seconds_base` | 28800 (8 hours) |
| `output_unlock_type` | account_bonus |
| `output_unlock_id` | null |
| `new_route_unlocked` | null |
| `rare_material_unlocks` | null |
| `fixture_recipe_unlocks` | null |
| `account_bonus_type` | rootrail_parts_yield |
| `account_bonus_value` | 0.02 |
| `codex_entry_unlocked` | `codex_lore_lw_rail_001` |
| `mvp_status` | launch |

**Player-facing gate message:** *"Forgotten Manual: Loamwake Terminal Routing Manual — Not in Codex. Look for it somewhere in the Rootvine Shelf."*

---

## 13. Fixture Definitions (First 5)

Personal Fixtures only. No War Fixtures. All `fixture_scope = personal`. All `no_negative_stats_confirmed = true`.

### fix_001_root_shovel_strap — Root-Bitten Shovel Strap

| Field | Value |
|-------|-------|
| `fixture_id` | `fix_001_root_shovel_strap` |
| `display_name` | Root-Bitten Shovel Strap |
| `fixture_scope` | personal |
| `fixture_category` | farming |
| `rarity` | common |
| `unlock_condition` | `account.fixture_cap >= 1` |
| `acquisition_source` | crafting |
| `recipe_id` | `fix_rec_001` |
| `rootrail_recipe_gate` | null |
| `primary_effect_type` | mushcap_gather_rate |
| `primary_effect_value` | 0.08 |
| `context_condition` | null |
| `context_bonus_type` | null |
| `context_bonus_value` | null |
| `no_negative_stats_confirmed` | true |
| `duplicate_style_allowed` | true |
| `personal_capacity_required` | 1 |
| `max_upgrade_level` | 3 |
| `salvage_output` | `tangled_root_twine:3;mooncaps:20` |
| `mvp_status` | launch |

### fix_002_loamwake_canister — Loamwake Canister

| Field | Value |
|-------|-------|
| `fixture_id` | `fix_002_loamwake_canister` |
| `display_name` | Loamwake Canister |
| `fixture_scope` | personal |
| `fixture_category` | pure_attack |
| `rarity` | common |
| `unlock_condition` | `account.fixture_cap >= 1` |
| `acquisition_source` | crafting |
| `recipe_id` | `fix_rec_002` |
| `rootrail_recipe_gate` | null |
| `primary_effect_type` | battle_stat_force |
| `primary_effect_value` | 0.12 |
| `context_condition` | `strata_progress.loamwake.warden_encounter == true` |
| `context_bonus_type` | force_vs_wardens |
| `context_bonus_value` | 0.06 |
| `no_negative_stats_confirmed` | true |
| `duplicate_style_allowed` | true |
| `personal_capacity_required` | 1 |
| `max_upgrade_level` | 3 |
| `salvage_output` | `crumbled_ore_chunk:2;mooncaps:30` |
| `mvp_status` | launch |

### fix_003_pale_glow_lens — Pale Glow Lens

| Field | Value |
|-------|-------|
| `fixture_id` | `fix_003_pale_glow_lens` |
| `display_name` | Pale Glow Lens |
| `fixture_scope` | personal |
| `fixture_category` | strain |
| `rarity` | uncommon |
| `unlock_condition` | `account.fixture_cap >= 2` |
| `acquisition_source` | crafting |
| `recipe_id` | `fix_rec_003` |
| `rootrail_recipe_gate` | null |
| `primary_effect_type` | strain_seed_capacity |
| `primary_effect_value` | 10 |
| `context_condition` | `strain_progress.active_strain_id == pale_glow` |
| `context_bonus_type` | strain_seed_yield |
| `context_bonus_value` | 0.05 |
| `no_negative_stats_confirmed` | true |
| `duplicate_style_allowed` | true |
| `personal_capacity_required` | 2 |
| `max_upgrade_level` | 5 |
| `salvage_output` | `dull_glow_shard:3;mooncaps:40` |
| `mvp_status` | launch |

### fix_004_burrowers_wrist_wrap — Burrower's Wrist Wrap

| Field | Value |
|-------|-------|
| `fixture_id` | `fix_004_burrowers_wrist_wrap` |
| `display_name` | Burrower's Wrist Wrap |
| `fixture_scope` | personal |
| `fixture_category` | farming |
| `rarity` | common |
| `unlock_condition` | `account.fixture_cap >= 1` |
| `acquisition_source` | crafting |
| `recipe_id` | `fix_rec_004` |
| `rootrail_recipe_gate` | null |
| `primary_effect_type` | burrowfolk_output_rate |
| `primary_effect_value` | 0.10 |
| `context_condition` | `burrow_state.active_burrowfolk_slots >= 2` |
| `context_bonus_type` | burrowfolk_output_rate |
| `context_bonus_value` | 0.05 |
| `no_negative_stats_confirmed` | true |
| `duplicate_style_allowed` | true |
| `personal_capacity_required` | 1 |
| `max_upgrade_level` | 3 |
| `salvage_output` | `loam_fiber:4;mooncaps:15` |
| `mvp_status` | launch |

### fix_005_dirtclad_gauntlet — Dirtclad Gauntlet

| Field | Value |
|-------|-------|
| `fixture_id` | `fix_005_dirtclad_gauntlet` |
| `display_name` | Dirtclad Gauntlet |
| `fixture_scope` | personal |
| `fixture_category` | pure_attack |
| `rarity` | uncommon |
| `unlock_condition` | `account.fixture_cap >= 2` |
| `acquisition_source` | crafting |
| `recipe_id` | `fix_rec_005` |
| `rootrail_recipe_gate` | null |
| `primary_effect_type` | battle_stat_force |
| `primary_effect_value` | 0.18 |
| `context_condition` | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` |
| `context_bonus_type` | mooncap_drop_rate_vs_wardens |
| `context_bonus_value` | 0.08 |
| `no_negative_stats_confirmed` | true |
| `duplicate_style_allowed` | false |
| `personal_capacity_required` | 2 |
| `max_upgrade_level` | 5 |
| `salvage_output` | `crumbled_ore_chunk:4;tangled_root_twine:2;mooncaps:50` |
| `mvp_status` | launch |

---

## 14. Fixture Recipe Table (First 5)

All material IDs reference Section 5 (Material Naming Lock). All `craft_time_seconds = 0` (instant craft for early recipes).

| recipe_id | output_fixture_id | rootrail_step_gate | mooncap_cost | material_inputs | craft_time_seconds | unlock_condition |
|-----------|------------------|-------------------|-------------|----------------|------------------|-----------------|
| `fix_rec_001` | `fix_001_root_shovel_strap` | null | 80 | `tangled_root_twine:6` | 0 | `account.fixture_cap >= 1` |
| `fix_rec_002` | `fix_002_loamwake_canister` | null | 100 | `crumbled_ore_chunk:4` | 0 | `account.fixture_cap >= 1` |
| `fix_rec_003` | `fix_003_pale_glow_lens` | null | 90 | `dull_glow_shard:5` | 0 | `account.fixture_cap >= 2` |
| `fix_rec_004` | `fix_004_burrowers_wrist_wrap` | null | 60 | `loam_fiber:8` | 0 | `account.fixture_cap >= 1` |
| `fix_rec_005` | `fix_005_dirtclad_gauntlet` | null | 110 | `crumbled_ore_chunk:6;tangled_root_twine:2` | 0 | `account.fixture_cap >= 2` |

---

## 15. Wanderer Rows

Wanderers are roaming encounter characters. All reference valid Loamwake zone IDs.

| wnd_id | display_name | character_type | encounter_zones | unlock_condition | encounter_chance | interaction_options | reward_on_help | flavor_intro | return_cooldown_hours | mvp_status |
|--------|-------------|---------------|----------------|-----------------|-----------------|--------------------|--------------|-----------|--------------------|-----------|
| `wnd_lw_001_grumbling_trader` | Gumwick the Grumbling Trader | wanderer | `zone_lw_001_rootvine_shelf` | always | 0.25 | trade;barter | `{mooncaps:30, fixture_material.loam_fiber:3}` | *"I'm not lost. I'm efficiently misplaced."* | 8 | launch |
| `wnd_lw_002_rail_scrap_picker` | Plank the Rail Scrap Picker | wanderer | `zone_lw_001_rootvine_shelf` | always | 0.20 | trade;help | `{rootrail_parts:3, fixture_material.rusted_rail_pin:1}` | *"You want junk or you want useful junk? Same price, different pile."* | 12 | launch |
| `wnd_lw_003_pipe_surveyor` | Ortiz the Pipe Surveyor | wanderer | `zone_lw_002_mudpipe_hollow` | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | 0.20 | help;barter | `{fixture_material.bleached_rail_fragment:2, mooncaps:40}` | *"Technically, I'm mapping. Technically."* | 12 | launch |
| `wnd_lw_004_mud_courier` | Swick the Mud Courier | wanderer | `zone_lw_002_mudpipe_hollow` | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | 0.20 | trade | `{fixture_material.crumbled_ore_chunk:3, rootrail_parts:2}` | *"Faster delivery guaranteed, as long as nothing moves."* | 8 | launch |
| `wnd_lw_005_spore_merchant` | Vellum the Spore Merchant | wanderer | `zone_lw_003_glowroot_passage` | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | 0.25 | trade;help | `{fixture_material.dewcap_sporewax:4, fixture_material.dull_glow_shard:2}` | *"Everything grows eventually. Even the things that shouldn't."* | 16 | launch |
| `wnd_lw_006_passage_mapmaker` | Dreln the Passage Mapmaker | wanderer | `zone_lw_003_glowroot_passage` | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | 0.20 | help;barter | `{mooncaps:60, rootrail_parts:3}` | *"I've mapped sixty-two passages. This one lied to me twice."* | 16 | launch |

---

## 16. Runaway Rows

Runaways are fleeing characters; resolved via a catch check. No combat — just a stat pass/fail.

| run_id | display_name | character_type | encounter_zones | catch_battle_stat | catch_difficulty | reward_on_catch | escape_consequence | flavor_scared_line | mvp_status |
|--------|-------------|---------------|----------------|-----------------|-----------------|----------------|------------------|-------------------|-----------|
| `run_lw_001_startled_rootpup` | Startled Rootpup | runaway | `zone_lw_001_rootvine_shelf` | speed | 1 | `{fixture_material.loam_fiber:4, mooncaps:20}` | nothing (pup escapes unharmed) | *"AAAA"* | launch |
| `run_lw_002_lost_mudpup` | Lost Mudpup | runaway | `zone_lw_002_mudpipe_hollow` | speed | 2 | `{fixture_material.crumbled_ore_chunk:2, rootrail_parts:1}` | nothing | *"NotNotNotNotNot—"* | launch |
| `run_lw_003_shy_glowcrawler` | Shy Glowcrawler | runaway | `zone_lw_003_glowroot_passage` | grit | 2 | `{fixture_material.dull_glow_shard:3, mooncaps:50}` | nothing (retreats into wall crevice) | *"…please don't."* | launch |
| `run_lw_004_fleeing_tinker` | Fleeing Tinker | runaway | `zone_lw_003_glowroot_passage` | speed | 3 | `{fixture_material.mudglass_chip:3, rootrail_parts:2}` | nothing | *"I swear I didn't touch it — it was like that when I found it!"* | launch |

---

## 17. Wild One Rows

Wild Ones require a successful battle stat check. No run mechanic — only combat resolution.

| wld_id | display_name | character_type | encounter_zones | combat_difficulty | required_battle_stat | reward_on_win | spawn_weight | lore_note | mvp_status |
|--------|-------------|---------------|----------------|-----------------|---------------------|--------------|-------------|----------|-----------|
| `wld_lw_001_rootmaw_grub` | Rootmaw Grub | wild_one | `zone_lw_001_rootvine_shelf` | 1 | force | `{fixture_material.tangled_root_twine:3, mooncaps:15}` | 20 | Juvenile root-borers. Not dangerous unless you put your hand in one. | launch |
| `wld_lw_002_hollow_scraper` | Hollow Scraper | wild_one | `zone_lw_002_mudpipe_hollow` | 3 | force | `{fixture_material.crumbled_ore_chunk:4, mooncaps:35}` | 20 | A flat-bodied scavenger that uses the pipe walls for navigation. The scraping is loud. Very loud. | launch |
| `wld_lw_003_pipe_rattler` | Pipe Rattler | wild_one | `zone_lw_002_mudpipe_hollow` | 4 | force | `{fixture_material.bleached_rail_fragment:2, rootrail_parts:3, mooncaps:50}` | 10 | Something that lives in the old drainage pipes. Nobody knows what it eats. Probably the pipes. | launch |
| `wld_lw_004_glow_biter` | Glow Biter | wild_one | `zone_lw_003_glowroot_passage` | 3 | grit | `{fixture_material.dull_glow_shard:4, fixture_material.dewcap_sporewax:2}` | 20 | Bioluminescent and angry. It glows brighter when startled. Treat this as a warning, not an invitation. | launch |

---

## 18. Loamwake Duty Chain

### Story Duty Chain: Loamwake Zone Progression

This chain gates zone_lw_002 and the Rootrail reveal. It is the primary story spine for Loamwake MVP.

| dty_id | display_name | duty_type | strata_id | unlock_condition | objective | reward | repeatable | repeat_cadence | chain_next_dty_id | mvp_status |
|--------|-------------|----------|---------|-----------------|---------|-------|-----------|--------------|-----------------|-----------|
| `dty_lw_001_clear_rootvine` | Clear the Rootvine Shelf | story | loamwake | always | Explore Rootvine Shelf 3 times and resolve 1 Wanderer encounter | `{mooncaps:120, fixture_material.tangled_root_twine:6}` | false | once | `dty_lw_002_hollow_survey` | launch |
| `dty_lw_002_hollow_survey` | Survey the Mudpipe Hollow | story | loamwake | `duty_progress.dty_lw_001_clear_rootvine.state == completed` | Explore Mudpipe Hollow 3 times and find 1 Buried Clue | `{mooncaps:150, fixture_material.crumbled_ore_chunk:5, rootrail_parts:4, account_bonus:{type:fixture_cap_increase, value:1}}` | false | once | `dty_lw_003_greta_rail_lead` | launch |
| `dty_lw_003_greta_rail_lead` | Follow Greta's Lead | story | loamwake | `duty_progress.dty_lw_002_hollow_survey.state == completed` | Explore the border area of Mudpipe Hollow (zone_lw_002) and trigger the Rootrail discovery event | `{mooncaps:80, rootrail_parts:5}` | false | once | `dty_lw_004_find_the_manual` | launch |
| `dty_lw_004_find_the_manual` | Find the Routing Manual | story | loamwake | `duty_progress.dty_lw_003_greta_rail_lead.state == completed and rootrail_state.rootrail_unlocked == true` | Find the Forgotten Manual (search Rootvine Shelf Buried Clues) | `{mooncaps:100, rootrail_parts:8, fixture_material.bleached_rail_fragment:3}` | false | once | null | launch |

> **NOTE:** The first Fixture cap increase (0→1) is **tutorial-owned**. It fires during the guided onboarding sequence (within the first 5 minutes), not from any Loamwake Duty reward. `dty_lw_001_clear_rootvine` does NOT grant a cap increase — it rewards materials only. `dty_lw_002_hollow_survey` grants the second cap increase (1→2) via `account_bonus:{type:fixture_cap_increase, value:1}`. This aligns with `core_loop_mvp_spec.md` cap path 0→1→2.

### Daily Duties (Loamwake)

| dty_id | display_name | duty_type | strata_id | unlock_condition | objective | reward | repeatable | repeat_cadence | chain_next_dty_id | mvp_status |
|--------|-------------|----------|---------|-----------------|---------|-------|-----------|--------------|-----------------|-----------|
| `dty_lw_daily_001_gather_rootvine` | Gather From the Shelf | daily | loamwake | `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf` | Explore Rootvine Shelf 2 times | `{mooncaps:60, fixture_material.tangled_root_twine:4}` | true | daily | null | launch |
| `dty_lw_daily_002_hollow_ore` | Ore From the Hollow | daily | loamwake | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | Explore Mudpipe Hollow 2 times | `{mooncaps:75, fixture_material.crumbled_ore_chunk:3, rootrail_parts:2}` | true | daily | null | launch |
| `dty_lw_daily_003_encounter_resolve` | Handle Any Two Encounters | daily | loamwake | `strata_progress.loamwake.clears >= 1` | Resolve 2 encounters (any type) in any Loamwake zone | `{mooncaps:80, strain_seeds:5}` | true | daily | null | launch |

---

## 19. Burrow Post Pool

First 4 Burrow Posts. All support early Loamwake progression. Posts are not Duties — they are notice-board tasks sent by Burrowfolk or the local gnome community.

| post_id | display_name | post_type | strata_id | unlock_condition | objective | duration_hours | reward | repeatable | repeat_cooldown_hours | mvp_status |
|---------|-------------|---------|---------|-----------------|---------|--------------|-------|-----------|---------------------|-----------|
| `post_lw_001_root_twine_run` | Root Twine Run | gather | loamwake | `burrow_state.burrow_level >= 1` | Gather 12× Tangled Root Twine from Rootvine Shelf | 2 | `{mooncaps:70, rootrail_parts:2}` | true | 4 | launch |
| `post_lw_002_ore_haul` | Ore Haul from the Hollow | gather | loamwake | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | Gather 8× Crumbled Ore Chunk from Mudpipe Hollow | 3 | `{mooncaps:90, fixture_material.mudglass_chip:2}` | true | 6 | launch |
| `post_lw_003_parts_salvage` | Parts Salvage Request | gather | loamwake | `rootrail_state.rootrail_unlocked == true` | Gather 10× Rootrail Parts from any Loamwake zone | 4 | `{mooncaps:100, fixture_material.bleached_rail_fragment:2}` | true | 8 | launch |
| `post_lw_004_sporewax_collection` | Sporewax Collection | craft | loamwake | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | Craft 1 Fixture using Loamwake materials | 1 | `{mooncaps:120, fixture_material.dewcap_sporewax:3, rootrail_parts:3}` | true | 12 | launch |

---

## 20. Reward Packet Reference Notes

All reward packets in this file follow the standard format from `content_table_templates.md`. Implementation notes:

- `account_bonus:{type:fixture_cap_increase, value:1}` — trigger via account state mutation on reward claim; validated server-side
- `forgotten_manual_id: fman_001_terminal_routing` — grants codex entry on claim; sets `rootrail_codex_entry.fman_001_terminal_routing.discovered = true`
- `hat_id: hat_001_loamwake_dirt_cap` — triggers Hat Collection unlock notification on claim
- `rootrail_parts` — credited to `wallet.rootrail_parts` through normal wallet mutation
- All `fixture_material.*` keys credit to `wallet.fixture_materials[material_id]`

---

## 21. MVP Progression Gate Table

Summary view of all Loamwake gate conditions. Useful for implementation validation.

| Gate | Condition | System Unlocked |
|------|----------|----------------|
| Fixture cap 0→1 | Tutorial sequence (first 5 min; not a Duty reward) | Fixture panel appears; fix_rec_001–002 available |
| Zone 2 opens | `dty_lw_001_clear_rootvine.state == completed` | zone_lw_002_mudpipe_hollow tile visible |
| Fixture cap 1→2 | `dty_lw_002_hollow_survey` reward claimed (`account_bonus.fixture_cap_increase: 1`) | fix_rec_003–005 available |
| Warden encounter available | `dty_lw_002_hollow_survey.state == completed` | wdn_lw_001_mudgrip encounter trigger active |
| Rootrail station reveals | `dty_lw_003_greta_rail_lead.state == completed` | Rootrail tab appears in Burrow nav |
| First hat unlock | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | hat_001_loamwake_dirt_cap granted |
| Rootrail step 1 startable | `rootrail_codex_entry.fman_001_terminal_routing.discovered == true and wallet.rootrail_parts >= 20` | rtr_step_001 timer can begin |
| Zone 3 opens | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | zone_lw_003_glowroot_passage visible |

---

## 22. Validation Checklist

- [x] All `zone_id` values follow `zone_lw_NNN_slug` convention
- [x] All material names are canonical — no provisional notes remaining
- [x] First Forgotten Manual columns fully match the Forgotten Manual template spec
- [x] All Fixture recipes reference only material IDs defined in Section 5 of this file
- [x] No Clothes, Ascender Parts, rigid slots, or non-canon resources in any drop table
- [x] `mvp_status` is set for every row
- [x] Warden ID uses `wdn_lw_001_mudgrip` — no `war_` prefix
- [x] `manual_consumed` is `false` on the Forgotten Manual
- [x] `separate_from_elder_books_confirmed` is `true` on the Forgotten Manual
- [x] `no_negative_stats_confirmed` is `true` on all Fixture definition rows
- [x] `counts_against_fixture_cap` is `false` on hat row (hat in Section 9 reward packet only; hat definition in content_table_templates.md)
- [x] Fixture cap 0→1 is tutorial-owned; `dty_lw_001_clear_rootvine` does NOT carry a cap-increase reward
- [x] Fixture cap 1→2 is owned by `dty_lw_002_hollow_survey`; aligns with `core_loop_mvp_spec.md` cap path 0→1→2
- [x] No `quest_chain_ids` terminology used — all references use `duty_chain_ids` or `chain_next_dty_id`
- [x] No War Fixture content included
- [x] No Strata 2–5 content included
- [x] Rootrail aesthetic direction locked (Section 4)
- [x] Encounter pool tables reference only zone IDs defined in Section 6

---

## 23. Unresolved Items

> **UNRESOLVED:** Fixture cap increase delivery mechanism — `dty_lw_002_hollow_survey` reward packet uses `account_bonus:{type:fixture_cap_increase, value:1}`. The gate evaluator and account mutation system must support this reward type before this Duty can function. The tutorial-owned 0→1 cap increase must also be implemented as a scripted account mutation in the onboarding flow. Flag both for `save_state_and_profile_flow.md`.

> **UNRESOLVED:** Battle stat model — `force`, `speed`, and `grit` are referenced as battle stats for Wanderers, Runaways, and Wild Ones. The full combat resolution system is not defined in Phase 2A. Recommend a Phase 2B combat resolution spec stub.

> **UNRESOLVED:** Hat definition rows — `hat_001_loamwake_dirt_cap` is referenced as a Warden first-clear reward. Its full definition row (using Hat Template) should live in a hat content file. This file does not duplicate the hat definition; it only references the hat ID. Phase 2B hat content file TBD.

> **UNRESOLVED:** Confidant integration — `dty_lw_003_greta_rail_lead` references Greta's second Duty as the Rootrail reveal trigger. Greta's full Confidant definition and Duty chain must be defined in `first_confidant_chain.md`. This file uses her Duty ID without defining her full record.

> **UNRESOLVED:** Strata Trait modifiers — Zone records do not include `strata_trait_modifier` values; this system is out of MVP scope but the column is defined in the Zone Template. Confirm stub treatment for Loamwake launch.

---

## 24. Next Recommended Phase 2B File

**Next file:** `docs/06_content/first_confidant_chain.md`

This file depends on:
- Zone IDs from Section 6 of this file
- Duty IDs from Section 18 of this file (specifically `dty_lw_003_greta_rail_lead` as Greta's trigger)
- Material IDs from Section 5 of this file
- Rootrail station ID from Section 12 of this file

The Confidant chain file must define `cnf_001_placeholder_greta` with at least 3 Duties, including one that matches the `dty_lw_003_greta_rail_lead` Loamwake story Duty trigger, and `cnf_002_placeholder_mossvane` with at least 2 Duties.
