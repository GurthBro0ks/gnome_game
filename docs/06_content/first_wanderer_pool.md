# First Wanderer Pool

**Status:** Canon — Phase 2B Draft
**Version:** 1.0.0
**Date:** 2026-04-19
**Scope:** Expands the locked Loamwake roaming encounter roster into build-ready interaction content. Adds trade, help, barter, catch, and combat reward detail for all 14 locked encounter IDs. Does NOT change source encounter IDs or roster ownership.

---

## 1. Purpose and Dependencies

This file adds the **interaction layer** on top of the locked encounter roster defined in `loamwake_mvp_content_sheet.md`. It does not replace or fork that file's encounter IDs, zones, spawn weights, or base rows.

**Source docs (read-only inputs):**

| source_file | sections used |
|-------------|--------------|
| `loamwake_mvp_content_sheet.md` | §5 Materials, §6 Zones, §8 Encounter Tables, §15–17 Encounter Rows |
| `first_confidant_chain.md` | §7 Greta Duties, §9 Mossvane Duties |
| `content_table_templates.md` | Wanderer/Runaway/Wild One templates |
| `data_schema_v1.md` | `encounter_state`, `confidant_state`, `duty_progress` |

**No new zone IDs, material IDs, Warden IDs, or War Fixture IDs are introduced in this file.**

---

## 2. Canon Rules

| Rule | Applied As |
|------|-----------|
| Loamwake IDs locked | All encounter IDs mirror loamwake_mvp_content_sheet.md; this file adds interaction rows only |
| No new material IDs | All reward packets use §5 material IDs only |
| Wanderers: trade/help/barter | No combat resolution for Wanderers |
| Runaways: catch only | No trade/help menus; stat pass/fail only |
| Wild Ones: combat only | No run or trade options |
| No War Armory | No `wfx_` content |
| No Strata 2–5 | Loamwake only |
| Greta: placeholder-safe | No personal lore, no questionnaire content |
| Mossvane: placeholder-safe | No personal lore, no questionnaire content |
| Warden IDs use `wdn_` | War Fixture IDs use `wfx_` |

---

## 3. Source Roster Mirror

Loamwake remains **source of truth** for all values in this table. If any value here differs from loamwake_mvp_content_sheet.md, align to Loamwake.

### Wanderers

| wnd_id | display_name | enc_table_id | zone_id | spawn_weight | unlock_condition |
|--------|-------------|-------------|---------|-------------|-----------------|
| `wnd_lw_001_grumbling_trader` | Gumwick the Grumbling Trader | `enc_table_lw_001` | `zone_lw_001_rootvine_shelf` | 25 | always |
| `wnd_lw_002_rail_scrap_picker` | Plank the Rail Scrap Picker | `enc_table_lw_001` | `zone_lw_001_rootvine_shelf` | 20 | always |
| `wnd_lw_003_pipe_surveyor` | Ortiz the Pipe Surveyor | `enc_table_lw_002` | `zone_lw_002_mudpipe_hollow` | 20 | zone_lw_002 unlocked |
| `wnd_lw_004_mud_courier` | Swick the Mud Courier | `enc_table_lw_002` | `zone_lw_002_mudpipe_hollow` | 20 | zone_lw_002 unlocked |
| `wnd_lw_005_spore_merchant` | Vellum the Spore Merchant | `enc_table_lw_003` | `zone_lw_003_glowroot_passage` | 25 | warden_clears.wdn_lw_001_mudgrip >= 1 |
| `wnd_lw_006_passage_mapmaker` | Dreln the Passage Mapmaker | `enc_table_lw_003` | `zone_lw_003_glowroot_passage` | 20 | warden_clears.wdn_lw_001_mudgrip >= 1 |

### Runaways

| run_id | display_name | enc_table_id | zone_id | spawn_weight | catch_stat | catch_diff |
|--------|-------------|-------------|---------|-------------|-----------|-----------|
| `run_lw_001_startled_rootpup` | Startled Rootpup | `enc_table_lw_001` | `zone_lw_001_rootvine_shelf` | 20 | speed | 1 |
| `run_lw_002_lost_mudpup` | Lost Mudpup | `enc_table_lw_002` | `zone_lw_002_mudpipe_hollow` | 20 | speed | 2 |
| `run_lw_003_shy_glowcrawler` | Shy Glowcrawler | `enc_table_lw_003` | `zone_lw_003_glowroot_passage` | 15 | grit | 2 |
| `run_lw_004_fleeing_tinker` | Fleeing Tinker | `enc_table_lw_003` | `zone_lw_003_glowroot_passage` | 10 | speed | 3 |

### Wild Ones

| wld_id | display_name | enc_table_id | zone_id | spawn_weight | combat_diff | battle_stat |
|--------|-------------|-------------|---------|-------------|-----------|-----------|
| `wld_lw_001_rootmaw_grub` | Rootmaw Grub | `enc_table_lw_001` | `zone_lw_001_rootvine_shelf` | 15 | 1 | force |
| `wld_lw_002_hollow_scraper` | Hollow Scraper | `enc_table_lw_002` | `zone_lw_002_mudpipe_hollow` | 20 | 3 | force |
| `wld_lw_003_pipe_rattler` | Pipe Rattler | `enc_table_lw_002` | `zone_lw_002_mudpipe_hollow` | 10 | 4 | force |
| `wld_lw_004_glow_biter` | Glow Biter | `enc_table_lw_003` | `zone_lw_003_glowroot_passage` | 20 | 3 | grit |

---

## 4. Zone Linkage Summary

| zone_id | zone_order | wanderers | runaways | wild_ones | encounter_pacing_note |
|---------|-----------|----------|---------|---------|----------------------|
| `zone_lw_001_rootvine_shelf` | 1 | wnd_lw_001, wnd_lw_002 | run_lw_001 | wld_lw_001 | Tutorial-safe. Low difficulty. Gumwick for early trade, Plank for Rootrail Parts intro. Rootpup catch is trivially easy. Grub fight is beginner combat. |
| `zone_lw_002_mudpipe_hollow` | 2 | wnd_lw_003, wnd_lw_004 | run_lw_002 | wld_lw_002, wld_lw_003 | Mid-tier. Survey and courier support Greta chain. Mudpup catch is moderate. Hollow Scraper requires real force; Pipe Rattler is the rare hard encounter and the best Rootrail Parts combat source. |
| `zone_lw_003_glowroot_passage` | 3 | wnd_lw_005, wnd_lw_006 | run_lw_003, run_lw_004 | wld_lw_004 | Post-Warden. Spore Merchant and Mapmaker support Mossvane chain. Two Runaways add variety. Glow Biter introduces grit pressure. |

---

## 5. Wanderer Detail Table

All fields mirror Loamwake source rows. Local helpers (`spawn_weight_from_enc_table`, `duty_support_tags`) added by this file.

| wnd_id | role_identity | encounter_zones | unlock_condition | encounter_trigger | encounter_chance | interaction_options | return_cooldown_hours | duty_support_tags | mvp_status |
|--------|--------------|----------------|-----------------|------------------|-----------------|--------------------|--------------------|------------------|-----------|
| `wnd_lw_001_grumbling_trader` | Early Peddler — low-stakes trade and small barter; entry-level economy introduction | `zone_lw_001_rootvine_shelf` | always | explore_action | 0.25 | trade; barter | 8 | — | launch |
| `wnd_lw_002_rail_scrap_picker` | Rootrail Scrapper — primary Zone 1 Rootrail Parts and rusted_rail_pin source via trade/help | `zone_lw_001_rootvine_shelf` | always | explore_action | 0.20 | trade; help | 12 | `dty_cnf002_03_parts_for_a_route` | launch |
| `wnd_lw_003_pipe_surveyor` | Hollow Survey Helper — barter for bleached_rail_fragment; helps with hollow measurement Duty | `zone_lw_002_mudpipe_hollow` | zone_lw_002 unlocked | explore_action | 0.20 | help; barter | 12 | `dty_cnf001_02_hollow_measure` | launch |
| `wnd_lw_004_mud_courier` | Route Courier — practical trade for crumbled_ore_chunk and rootrail_parts; Mudpipe material loop | `zone_lw_002_mudpipe_hollow` | zone_lw_002 unlocked | explore_action | 0.20 | trade | 8 | `dty_cnf002_03_parts_for_a_route` | launch |
| `wnd_lw_005_spore_merchant` | Glowroot Spore Peddler — primary Mossvane support; sole provider of sporewax_bundle trade | `zone_lw_003_glowroot_passage` | warden_clears.wdn_lw_001_mudgrip >= 1 | explore_action | 0.25 | trade; help | 16 | `dty_cnf002_01_sporewax_trade` | launch |
| `wnd_lw_006_passage_mapmaker` | Passage Pathfinder — help/barter around route hints and rootrail_parts; supports Mossvane map and parts chain | `zone_lw_003_glowroot_passage` | warden_clears.wdn_lw_001_mudgrip >= 1 | explore_action | 0.20 | help; barter | 16 | `dty_cnf002_02_map_the_glowroot; dty_cnf002_03_parts_for_a_route` | launch |

---

## 6. Wanderer Interaction Matrix

Which interaction types each Wanderer supports. Determines which menus appear on encounter.

| wnd_id | trade | help | barter | notes |
|--------|-------|------|--------|-------|
| `wnd_lw_001_grumbling_trader` | ✓ | — | ✓ | Trade for loam_fiber; barter small swaps |
| `wnd_lw_002_rail_scrap_picker` | ✓ | ✓ | — | Trade or help for rootrail_parts |
| `wnd_lw_003_pipe_surveyor` | — | ✓ | ✓ | Help for survey credit; barter for bleached_rail_fragment |
| `wnd_lw_004_mud_courier` | ✓ | — | — | Trade-only; no help or barter |
| `wnd_lw_005_spore_merchant` | ✓ | ✓ | — | Trade for sporewax (Mossvane Duty); help for glow shards |
| `wnd_lw_006_passage_mapmaker` | — | ✓ | ✓ | Help for Mossvane map Duty; barter rootrail_parts |

---

## 7. Wanderer Trade Offer Table

| trade_id | wnd_id | cost_packet | reward_packet | stock_rule | cooldown_hours | unlock_condition | duty_support_tags | mvp_status |
|---------|--------|------------|--------------|-----------|---------------|-----------------|------------------|-----------|
| `trade_lw_001_loam_bundle` | `wnd_lw_001_grumbling_trader` | `{mooncaps:40}` | `{fixture_material.loam_fiber:5}` | unlimited | 8 | always | — | launch |
| `trade_lw_001b_twine_swap` | `wnd_lw_001_grumbling_trader` | `{mooncaps:30}` | `{fixture_material.tangled_root_twine:4}` | unlimited | 8 | always | — | launch |
| `trade_lw_002_scrap_parts` | `wnd_lw_002_rail_scrap_picker` | `{mooncaps:50}` | `{rootrail_parts:3}` | 2 per visit | 12 | always | `dty_cnf002_03_parts_for_a_route` | launch |
| `trade_lw_002b_pin_trade` | `wnd_lw_002_rail_scrap_picker` | `{mooncaps:35}` | `{fixture_material.rusted_rail_pin:2}` | unlimited | 12 | always | — | launch |
| `trade_lw_004_ore_bundle` | `wnd_lw_004_mud_courier` | `{mooncaps:55}` | `{fixture_material.crumbled_ore_chunk:4}` | unlimited | 8 | zone_lw_002 unlocked | — | launch |
| `trade_lw_004b_courier_parts` | `wnd_lw_004_mud_courier` | `{mooncaps:65}` | `{rootrail_parts:3, fixture_material.crumbled_ore_chunk:2}` | 1 per visit | 8 | zone_lw_002 unlocked | `dty_cnf002_03_parts_for_a_route` | launch |
| `trade_lw_005_sporewax_bundle` | `wnd_lw_005_spore_merchant` | `{mooncaps:50}` | `{fixture_material.dewcap_sporewax:3}` | 1 per visit | daily | `confidant_state.cnf_002_placeholder_mossvane.unlocked == true` | `dty_cnf002_01_sporewax_trade` | launch |
| `trade_lw_005b_spore_open` | `wnd_lw_005_spore_merchant` | `{mooncaps:45}` | `{fixture_material.dewcap_sporewax:2}` | unlimited | 16 | warden_clears.wdn_lw_001_mudgrip >= 1 | — | launch |

> **MOSSVANE SUPPORT:** `trade_lw_005_sporewax_bundle` is the **primary fulfillment path** for `dty_cnf002_01_sporewax_trade`. Gate: `confidant_state.cnf_002_placeholder_mossvane.unlocked == true`. When the player trades using this row while the Mossvane Duty is active, the objective `collect_dewcap_sporewax` advances. The open trade `trade_lw_005b_spore_open` is also valid but lacks the Duty tag and requires no Confidant unlock.

> **ROOTRAIL PARTS NOTE:** At least three Wanderers provide Rootrail Parts via trade: `wnd_lw_002_rail_scrap_picker` (trade_lw_002_scrap_parts), `wnd_lw_004_mud_courier` (trade_lw_004b_courier_parts), and `wnd_lw_006_passage_mapmaker` (barter — see Section 9). These collectively fulfill `dty_cnf002_03_parts_for_a_route`.

---

## 8. Wanderer Help Outcome Table

Help interactions are free (no cost) but have a success rule and cooldown. On success the player receives the reward packet. On failure the Wanderer declines without penalty.

| help_id | wnd_id | trigger | success_rule | reward_packet | cooldown_hours | duty_support_tags | mvp_status |
|---------|--------|---------|-------------|--------------|---------------|------------------|-----------|
| `help_lw_002_scrap_assist` | `wnd_lw_002_rail_scrap_picker` | player initiates help | always succeeds (no stat check; flavor beat only) | `{rootrail_parts:3, fixture_material.rusted_rail_pin:1}` | 12 | `dty_cnf002_03_parts_for_a_route` | launch |
| `help_lw_003_survey_count` | `wnd_lw_003_pipe_surveyor` | player initiates help | `strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | `{fixture_material.bleached_rail_fragment:2, mooncaps:40}` | 12 | `dty_cnf001_02_hollow_measure` | launch |
| `help_lw_005_spore_sample` | `wnd_lw_005_spore_merchant` | player initiates help | `strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1` | `{fixture_material.dewcap_sporewax:2, fixture_material.dull_glow_shard:2}` | 16 | — | launch |
| `help_lw_006_route_hint` | `wnd_lw_006_passage_mapmaker` | player initiates help | `duty_progress.dty_cnf002_02_map_the_glowroot.state == active or completed` | `{mooncaps:60, rootrail_parts:2}` | 16 | `dty_cnf002_02_map_the_glowroot` | launch |

> **GRETA-ADJACENT NOTE:** `help_lw_003_survey_count` from Ortiz the Pipe Surveyor provides bleached_rail_fragment and a mooncap reward consistent with Greta's `dty_cnf001_02_hollow_measure` objective environment. Completing this help interaction while `dty_cnf001_02_hollow_measure` is active counts toward the "find 1 encounter (any type)" objective. Ortiz does NOT reference Greta by name; his role is environmental rail/survey support.

---

## 9. Wanderer Barter Offer Table

Barters are material-for-material swaps. No Mooncap cost. Max daily uses enforced to prevent farming.

| barter_id | wnd_id | input_packet | output_packet | max_uses_per_day | unlock_condition | mvp_status |
|----------|--------|-------------|--------------|-----------------|-----------------|-----------|
| `barter_lw_001_twine_for_fiber` | `wnd_lw_001_grumbling_trader` | `{fixture_material.tangled_root_twine:3}` | `{fixture_material.loam_fiber:4}` | 2 | always | launch |
| `barter_lw_001b_fiber_for_twine` | `wnd_lw_001_grumbling_trader` | `{fixture_material.loam_fiber:4}` | `{fixture_material.tangled_root_twine:3}` | 2 | always | launch |
| `barter_lw_003_ore_for_rail` | `wnd_lw_003_pipe_surveyor` | `{fixture_material.crumbled_ore_chunk:3}` | `{fixture_material.bleached_rail_fragment:2}` | 2 | zone_lw_002 unlocked | launch |
| `barter_lw_006_shard_for_parts` | `wnd_lw_006_passage_mapmaker` | `{fixture_material.dull_glow_shard:4}` | `{rootrail_parts:3}` | 2 | warden_clears.wdn_lw_001_mudgrip >= 1 | `dty_cnf002_03_parts_for_a_route` | launch |
| `barter_lw_006b_sporewax_for_map` | `wnd_lw_006_passage_mapmaker` | `{fixture_material.dewcap_sporewax:3}` | `{mooncaps:60}` | 1 | warden_clears.wdn_lw_001_mudgrip >= 1 | launch |

---

## 10. Runaway Detail Table

Runaways have no trade/help/barter menus. Interaction is always catch check → success or fail. All flavor is environment-safe (no character lore).

| run_id | display_name | character_type | encounter_zones | catch_battle_stat | catch_difficulty | flavor_scared_line | lore_safe_note | mvp_status |
|--------|-------------|---------------|----------------|-----------------|-----------------|-------------------|---------------|-----------|
| `run_lw_001_startled_rootpup` | Startled Rootpup | runaway | `zone_lw_001_rootvine_shelf` | speed | 1 | *"AAAA"* | A small burrowing creature that panics on first contact. Tutorial-safe. Escape is harmless. | launch |
| `run_lw_002_lost_mudpup` | Lost Mudpup | runaway | `zone_lw_002_mudpipe_hollow` | speed | 2 | *"NotNotNotNotNot—"* | A juvenile creature disoriented by the hollow's drainage echo. More skittish than the Rootpup. | launch |
| `run_lw_003_shy_glowcrawler` | Shy Glowcrawler | runaway | `zone_lw_003_glowroot_passage` | grit | 2 | *"…please don't."* | Slow-moving but wall-hugging; requires patience (grit) rather than speed. Retreats into wall crevice on escape. | launch |
| `run_lw_004_fleeing_tinker` | Fleeing Tinker | runaway | `zone_lw_003_glowroot_passage` | speed | 3 | *"I swear I didn't touch it — it was like that when I found it!"* | A gnome-type scavenger in a panic. Highest catch difficulty in Loamwake. Good Rootrail Parts reward. | launch |

---

## 11. Runaway Catch Outcome Table

| run_id | success_reward | fail_consequence | cooldown_hours | duty_support_tags | mvp_status |
|--------|--------------|-----------------|---------------|------------------|-----------|
| `run_lw_001_startled_rootpup` | `{fixture_material.loam_fiber:4, mooncaps:20}` | Rootpup escapes unharmed; no penalty; encounter closes | 6 | — | launch |
| `run_lw_002_lost_mudpup` | `{fixture_material.crumbled_ore_chunk:2, rootrail_parts:1}` | Mudpup retreats into pipe; no penalty | 8 | — | launch |
| `run_lw_003_shy_glowcrawler` | `{fixture_material.dull_glow_shard:3, mooncaps:50}` | Glowcrawler retreats into wall crevice; no penalty | 8 | `dty_cnf002_02_map_the_glowroot` | launch |
| `run_lw_004_fleeing_tinker` | `{fixture_material.mudglass_chip:3, rootrail_parts:2}` | Tinker disappears into Passage; no penalty | 12 | `dty_cnf002_02_map_the_glowroot` | launch |

> **IMPLEMENTATION NOTE:** "Resolve 1 Runaway or Wanderer encounter" in `dty_cnf002_02_map_the_glowroot` counts both a **successful catch** and a **failed catch attempt** as encounter resolution. The encounter trigger fires on first contact; the resolution fires on outcome (success or fail). Both run_lw_003 and run_lw_004 carry this Duty tag.

---

## 12. Wild One Detail Table

Wild Ones are combat-only. No catch, trade, or help menus. Difficulty scales across zones.

| wld_id | display_name | character_type | encounter_zones | combat_difficulty | required_battle_stat | ambient_behavior | lore_note | mvp_status |
|--------|-------------|---------------|----------------|-----------------|---------------------|-----------------|----------|-----------|
| `wld_lw_001_rootmaw_grub` | Rootmaw Grub | wild_one | `zone_lw_001_rootvine_shelf` | 1 | force | Burrows into root substrate; surfaces when zone is disturbed | Juvenile root-borers. Not dangerous unless you put your hand in one. Tutorial-safe first combat. | launch |
| `wld_lw_002_hollow_scraper` | Hollow Scraper | wild_one | `zone_lw_002_mudpipe_hollow` | 3 | force | Uses pipe walls for lateral navigation; loud constant scraping | A flat-bodied scavenger navigating by pipe-wall friction. The noise arrives before it does. | launch |
| `wld_lw_003_pipe_rattler` | Pipe Rattler | wild_one | `zone_lw_002_mudpipe_hollow` | 4 | force | Retreats into drainage pipe between encounters; rare spawn | Zone 2's hard encounter. Nobody knows what it eats. Probably the pipes. Best Rootrail Parts combat source in Zone 2. | launch |
| `wld_lw_004_glow_biter` | Glow Biter | wild_one | `zone_lw_003_glowroot_passage` | 3 | grit | Pulses brighter on disturbance; uses light bursts to disorient | Bioluminescent and angry. Glows brighter when startled. First encounter to require grit instead of force — introduces stat variety. | launch |

---

## 13. Wild One Combat Reward Detail

| wld_id | win_reward | fall_back_consequence | cooldown_hours | duty_support_tags | mvp_status |
|--------|------------|----------------------|---------------|------------------|-----------|
| `wld_lw_001_rootmaw_grub` | `{fixture_material.tangled_root_twine:3, mooncaps:15}` | Player takes no damage; encounter closes (stat-check model, not HP) | 4 | — | launch |
| `wld_lw_002_hollow_scraper` | `{fixture_material.crumbled_ore_chunk:4, mooncaps:35}` | Player fails check; encounter closes; no material loss | 8 | — | launch |
| `wld_lw_003_pipe_rattler` | `{fixture_material.bleached_rail_fragment:2, rootrail_parts:3, mooncaps:50}` | Player fails check; encounter closes; no material loss | 12 | `dty_cnf002_03_parts_for_a_route` | launch |
| `wld_lw_004_glow_biter` | `{fixture_material.dull_glow_shard:4, fixture_material.dewcap_sporewax:2}` | Player fails check; encounter closes; no material loss | 8 | `dty_cnf002_02_map_the_glowroot` | launch |

> **SCALING NOTE:** Wild One difficulty scales within Loamwake as: Zone 1 (diff 1, force, tutorial) → Zone 2 common (diff 3, force) → Zone 2 rare (diff 4, force, Rootrail reward) → Zone 3 (diff 3, grit, introduces second stat). The Zone 3 Glow Biter is equal difficulty to Zone 2 common but harder to prepare for due to stat type switch.


---

## 14. Greta Support Note

`post_lw_005_greta_intro` is **now defined** in `loamwake_mvp_content_sheet.md` Section 19 (support patch applied this session).

| item | value |
|------|-------|
| post_id | `post_lw_005_greta_intro` |
| display_name | Rail Scratches on the Board |
| unlock_condition | `duty_progress.dty_lw_001_clear_rootvine.state == completed` |
| objective | Read Greta's rail-scratch note and inspect one Rootvine Shelf sign |
| reward | `{mooncaps:40, fixture_material.loam_fiber:2}` |
| repeatable | false |
| state side-effect | Sets `confidant_state.cnf_001_placeholder_greta.unlocked = true`; makes `dty_cnf001_01_shelf_scratches` available |
| source_file | `loamwake_mvp_content_sheet.md §19` |

**Greta-adjacent encounter support in this file:**

- `wnd_lw_002_rail_scrap_picker` — Plank provides rail-scar and scrap-environment support without naming Greta. The rail language (rusted pins, scrap piles) reinforces the same atmospheric layer.
- `wnd_lw_003_pipe_surveyor` — Ortiz's `help_lw_003_survey_count` credits toward `dty_cnf001_02_hollow_measure` (Explore Mudpipe Hollow + find 1 encounter).
- Neither Wanderer references Greta by name. Greta's role remains Confidant-only. No personal lore invented here.

> **PLACEHOLDER BOUNDARY:** Greta is `placeholder_flag: true`. This file contains zero personal Greta lore, backstory, or questionnaire content. All encounter support is environmental only.

---

## 15. Mossvane Support Note

`dty_cnf002_01_sporewax_trade` is now **concretely supported** by `trade_lw_005_sporewax_bundle`.

| item | value |
|------|-------|
| trade_id | `trade_lw_005_sporewax_bundle` |
| wnd_id | `wnd_lw_005_spore_merchant` |
| cost | `{mooncaps:50}` |
| reward | `{fixture_material.dewcap_sporewax:3}` |
| stock_rule | 1 per visit |
| cooldown | daily |
| unlock_condition | `confidant_state.cnf_002_placeholder_mossvane.unlocked == true` |
| duty_support_tags | `dty_cnf002_01_sporewax_trade` |

**Full Mossvane Duty support chain:**

| duty_id | supported_by | mechanism |
|---------|-------------|----------|
| `dty_cnf002_01_sporewax_trade` | `trade_lw_005_sporewax_bundle` (wnd_lw_005) | Trade with Wanderer + collect dewcap_sporewax |
| `dty_cnf002_02_map_the_glowroot` | `help_lw_006_route_hint` (wnd_lw_006); run_lw_003; run_lw_004; wld_lw_004 | "Explore Zone 3 + resolve 1 encounter" — any Zone 3 encounter resolves |
| `dty_cnf002_03_parts_for_a_route` | `trade_lw_002_scrap_parts`, `trade_lw_004b_courier_parts`, `barter_lw_006_shard_for_parts`; also zone drops and clue_lw_001 | "Accumulate 15 Rootrail Parts" — multiple paths |

> **PLACEHOLDER BOUNDARY:** Mossvane is `placeholder_flag: true`. Vellum the Spore Merchant is NOT Mossvane. Vellum is an unrelated roaming character whose trade inventory happens to include dewcap_sporewax. No NPC-to-Confidant identity link exists at the content layer; the Duty system tracks objective progress independently.

---

## 16. Reward Reference Table

All reward items used in this file. All locked in `loamwake_mvp_content_sheet.md §5` or core wallet.

| reward_item | type | locked_in | used_in |
|------------|------|-----------|---------|
| `mooncaps` | wallet | core schema | most trades, catch rewards, help rewards |
| `rootrail_parts` | wallet | loamwake_mvp §5 | trade_lw_002, trade_lw_004b, barter_lw_006, help_lw_006, wld_lw_003 win, run_lw_002/004 catch |
| `fixture_material.loam_fiber` | material | loamwake_mvp §5 | trade_lw_001, barter_lw_001b, run_lw_001 catch, post_lw_005 reward |
| `fixture_material.tangled_root_twine` | material | loamwake_mvp §5 | trade_lw_001b, barter_lw_001, wld_lw_001 win |
| `fixture_material.rusted_rail_pin` | material | loamwake_mvp §5 | trade_lw_002b, help_lw_002 |
| `fixture_material.crumbled_ore_chunk` | material | loamwake_mvp §5 | trade_lw_004, trade_lw_004b, barter_lw_003, wld_lw_002 win, run_lw_002 catch |
| `fixture_material.bleached_rail_fragment` | material | loamwake_mvp §5 | help_lw_003, barter_lw_003, wld_lw_003 win |
| `fixture_material.mudglass_chip` | material | loamwake_mvp §5 | run_lw_004 catch |
| `fixture_material.dewcap_sporewax` | material | loamwake_mvp §5 | trade_lw_005, trade_lw_005b, help_lw_005, barter_lw_006b, wld_lw_004 win |
| `fixture_material.dull_glow_shard` | material | loamwake_mvp §5 | help_lw_005, barter_lw_006, run_lw_003 catch, wld_lw_004 win |

> **VALIDATION:** No reward packet in this file references a material, zone, or ID outside this table. No cross-Strata or cross-phase IDs used. No new material IDs invented.

---

## 17. Schema and State Mapping

### Content-definition fields (this file authors these)

| field | schema entity | notes |
|-------|--------------|-------|
| `wnd_id` | `wanderer` | Primary key; mirrors Loamwake source |
| `trade_id` | `wanderer_trade_offer` | New local table; FK → wnd_id |
| `help_id` | `wanderer_help_outcome` | New local table; FK → wnd_id |
| `barter_id` | `wanderer_barter_offer` | New local table; FK → wnd_id |
| `run_id` | `runaway` | Primary key; mirrors Loamwake source |
| `wld_id` | `wild_one` | Primary key; mirrors Loamwake source |
| `duty_support_tags` | content helper | Maps interaction rows to Duty IDs for gate evaluator |
| `stock_rule` | `wanderer_trade_offer` | "unlimited" or "N per visit" |
| `cooldown_hours` | `wanderer_trade_offer` / `wanderer_help_outcome` | Per-encounter return or per-offer cooldown |

### Player-state fields (backend-owned, not authored here)

| field | schema entity | notes |
|-------|--------------|-------|
| `encounter_id` | `encounter_state` | Populated by encounter system on spawn |
| `encounter_type` | `encounter_state` | wanderer / runaway / wild_one |
| `times_triggered` | `encounter_state` | Incremented on spawn |
| `times_resolved` | `encounter_state` | Incremented on outcome |
| `last_triggered_at` | `encounter_state` | For cooldown enforcement |
| `outcome_history` | `encounter_state` | List of outcomes; used for Duty progress tracking |
| `active` | `encounter_state` | True while encounter is live |
| `state` | `duty_progress` | available / active / completed / locked |
| `objective_progress` | `duty_progress` | Key → count; e.g., `collect_dewcap_sporewax: 3` |
| `unlocked` | `confidant_state` | True after unlock Duty or Post completion |
| `trust_level` | `confidant_state` | 0–5; gated by trust_points accumulation |

---

## 18. Validation Checklist

- [x] All 6 locked Wanderers present with role, zones, unlock, interactions, and cooldown
- [x] All 4 locked Runaways present with catch stat, difficulty, success/fail outcomes
- [x] All 4 locked Wild Ones present with difficulty, battle stat, win reward, cooldown
- [x] All zone references are `zone_lw_001_rootvine_shelf`, `zone_lw_002_mudpipe_hollow`, or `zone_lw_003_glowroot_passage`
- [x] Spawn weights in Source Roster Mirror match `enc_table_lw_001/002/003`
- [x] `wnd_lw_005_spore_merchant` supports `dty_cnf002_01_sporewax_trade` via `trade_lw_005_sporewax_bundle`
- [x] Rootrail Parts reward exists via at least three Wanderer paths (wnd_lw_002 trade, wnd_lw_004 trade, wnd_lw_006 barter)
- [x] Runaways have catch success/fail outcomes only — no trade or help menus
- [x] Wild Ones have combat-only outcomes — no catch, trade, or barter options
- [x] `post_lw_005_greta_intro` defined in loamwake_mvp_content_sheet.md (support patch applied); referenced correctly here
- [x] Greta encounter support is environmental (rail-scar, survey) — no personal lore
- [x] Mossvane Duty chain fully supported: all 3 Duties have concrete encounter paths
- [x] No new Loamwake material IDs invented
- [x] No new zone IDs invented
- [x] No War Armory, wfx_, or Strata 2–5 content
- [x] No final Confidant lore or questionnaire content
- [x] All reward packets use IDs from locked Reward Reference Table (Section 16)
- [x] Loamwake remains source of truth for encounter IDs, zones, weights, and base roster rows
- [x] Schema/state mapping distinguishes content-definition from player-state fields
- [x] `mvp_status: launch` on all rows

---

## 19. Unresolved Items

> **UNRESOLVED:** Battle stat resolution system — `force`, `speed`, and `grit` are used as catch and combat stats throughout this file. The full stat resolution model (roll method, pass threshold, player stat source) is not defined in Phase 2A or 2B. Flag for a combat/encounter resolution spec stub.

> **UNRESOLVED:** Trade stock refresh timing — `stock_rule: N per visit` assumes "visit" means per encounter instance. If encounters are persistent (same instance exists across sessions), stock refresh must tie to `return_cooldown_hours` reset. Implementation detail; flag for `save_state_and_profile_flow.md`.

> **UNRESOLVED:** Mossvane's `calling_mossvane_market_eye` reveals one hidden Wanderer trade per day in Zone 3. The mechanism for "hidden trade offer" is not defined. This Calling is a UI stub only; do not implement until the Calling system is designed.

> **UNRESOLVED:** Wanderer encounter_chance stacking — multiple Wanderers share the same zone and encounter table. If two Wanderers share a zone, the encounter system must correctly weight them via `enc_table_lw_*` spawn weights, not double-fire. Confirm with encounter system spec.

> **UNRESOLVED:** `dty_cnf002_02_map_the_glowroot` objective "resolve 1 Runaway or Wanderer encounter" — the encounter system must tag catch attempts (including failed catches) as resolved to count. Confirm resolution tagging with encounter state spec.

---

## 20. Acceptance Checklist

Done when all of the following are true:

- [ ] Content designer can expand any Wanderer row into a trade/help/barter implementation without guessing
- [ ] Engineer can implement `trade_lw_005_sporewax_bundle` and know it fulfills `dty_cnf002_01_sporewax_trade`
- [ ] Engineer can implement Runaway catch checks using only this file and loamwake_mvp_content_sheet.md
- [ ] Engineer can implement Wild One combat outcomes using only this file and loamwake_mvp_content_sheet.md
- [ ] QA can verify: all 6 Wanderers spawn in correct zones; all 4 Runaways have correct catch difficulty; all 4 Wild Ones have correct stat requirements
- [ ] `post_lw_005_greta_intro` is in loamwake_mvp_content_sheet.md and its state side-effects are documented
- [ ] No implementation requires inventing IDs not in this file or loamwake_mvp_content_sheet.md

---

## 21. Next Recommended Phase 2B File

**Next file:** `docs/06_content/lucky_draw_week_mvp.md`

Depends on:
- Economy model (`data/economy/economy_model_v1_sheet.csv`)
- Lucky Draw currency (`lucky_draws` — canonical wallet field; defined in `data_schema_v1.md §wallet` and economy model)
- Loamwake material IDs (Section 5 of `loamwake_mvp_content_sheet.md`) for prize pools
- Event cadence spec (`docs/05_liveops/`) for 3-week rotation rules

The Lucky Draw week file should define the rotating Lucky Draw event structure, prize pool tiers, ticket costs, pity system, and Loamwake-era prize pool rows.
