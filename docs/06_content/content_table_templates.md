# Content Table Templates

**Status:** Canon — Phase 2A Salvage Rewrite  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** Column specifications for all major content types. Use these as the canonical source when creating new content CSV/sheet rows.

---

## Status, Purpose, Usage Rules

This file defines the required and optional columns for every major content type in Gnome Game. Content designers should populate these columns when creating new content. Developers should validate content rows against these specs.

**Rules:**
- Do not use generic RPG naming (Quest, NPC, Region, Guild). Use canonical names.
- Do not include Clothes rows. Clothes are removed from canon; use Fixture instead.
- Do not write final friend-questionnaire Confidant/Burrowfolk content yet. Mark `placeholder_flag = true` for all MVP first-set content.
- War Fixtures must NOT appear in the normal Fixture template rows. They have a separate template.

---

## Global Column Conventions

| Column | Format | Notes |
|--------|--------|-------|
| `*_id` | `prefix_NNN_slug` | e.g., `fix_001_root_shovel_strap` |
| `display_name` | Title Case string | Shown to players |
| `unlock_condition` | Gate expression string | See Unlock Condition Format below |
| `reward_packet` | Reward Packet reference | See Reward Packet Format below |
| `mvp_status` | `launch` / `stub` / `future` | MVP launch-readiness flag |
| `placeholder_flag` | `true` / `false` | `true` = temp MVP content pending revision |
| `notes` | Free text | Designer/developer notes |

---

## ID Naming Conventions

| Content Type | Prefix | Example |
|-------------|--------|---------|
| Zone | `zone_` | `zone_lw_001_rootvine_shelf` |
| Warden | `wdn_` | `wdn_lw_001_mudgrip` |
| Buried Clue | `clue_` | `clue_lw_001_rail_spike` |
| Wanderer | `wnd_` | `wnd_001_grumbling_trader` |
| Runaway | `run_` | `run_001_lost_burrowpup` |
| Wild One | `wld_` | `wld_001_feral_rooter` |
| Confidant | `cnf_` | `cnf_001_placeholder_greta` |
| Burrowfolk | `bfk_` | `bfk_001_placeholder_mudger` |
| Treasure | `tre_` | `tre_001_mossy_dial` |
| Fixture (personal) | `fix_` | `fix_001_root_shovel_strap` |
| Fixture Recipe | `fix_rec_` | `fix_rec_001` |
| Hat | `hat_` | `hat_001_loamwake_dirt_cap` |
| War Fixture | `wfx_` | `wfx_001_clique_ram_brace` |
| Rootrail Step | `rtr_step_` | `rtr_step_001` |
| Forgotten Manual | `fman_` | `fman_001_terminal_routing` |
| Event Ladder | `evt_` | `evt_luckydraw_001` |
| Festival Ledger | `fstv_` | `fstv_001_week01` |
| Burrow Post | `post_` | `post_001_lw_gather_run` |
| Duty | `dty_` | `dty_001_lw_gather_rootvine` |
| Encounter | `enc_` | `enc_001_lw_wanderer_meet` |

---

## Unlock Condition Format

Conditions are gate expression strings. Supported operators: `>=`, `<=`, `==`, `contains`, `and`, `or`, `not`.

Examples:
```
"strata_progress.loamwake.clears >= 1"
"account.fixture_cap >= 3 and rootrail_state.completed_steps contains rtr_step_003"
"always"
"never" (stub content)
```

---

## Reward Packet Format

Reward packets are JSON-like objects referenced inline or by ID:

```
{mooncaps: 200, strain_seeds: 10, fixture_material.tangled_root_twine: 4}
{fixture_id: fix_002_loamwake_canister}
{hat_id: hat_001_loamwake_dirt_cap}
{forgotten_manual_id: fman_001_terminal_routing}
{account_bonus: {type: fixture_cap_increase, value: 1}}
```

---

## Zone Template

Zones are explorable areas within a Stratum.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `zone_id` | string | `zone_<strata>_NNN_slug` |
| `display_name` | string | |
| `strata_id` | string | Parent Stratum |
| `zone_order` | int | Order within Stratum |
| `unlock_condition` | gate expression | |
| `mushcap_cost` | int | Mushcaps consumed per explore action |
| `encounter_table_id` | string | FK → encounter pool |
| `resource_drops` | list[ResourceDrop] | e.g., `tangled_root_twine:6–12` |
| `warden_id` | string \| null | Warden gating this zone |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `buried_clue_pool` | List of possible Buried Clue IDs to surface |
| `strata_trait_modifier` | How the active Strata Trait affects this zone |
| `ambient_flavor` | Short flavor description (1 line) |
| `notes` | |

**Example rows:**
```
zone_id, display_name, strata_id, zone_order, unlock_condition, mushcap_cost, encounter_table_id, resource_drops, warden_id, mvp_status
zone_lw_001_rootvine_shelf, Rootvine Shelf, loamwake, 1, always, 2, enc_table_lw_001, tangled_root_twine:4-8, null, launch
zone_lw_002_mudpipe_hollow, Mudpipe Hollow, loamwake, 2, strata_progress.loamwake.clears >= 1, 3, enc_table_lw_002, crumbled_ore_chunk:3-6, wdn_lw_001_mudgrip, launch
```

---

## Warden Template

Wardens are boss encounters gating Strata zone progression.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `warden_id` | string | |
| `display_name` | string | |
| `strata_id` | string | |
| `zone_id` | string | Zone the Warden guards |
| `unlock_condition` | gate expression | |
| `battle_stat_difficulty` | int | 1–10 difficulty rating |
| `required_battle_stat` | string | Primary stat needed (e.g., `force`, `grit`) |
| `recommended_fixture_cap` | int | Suggested Fixture cap to attempt |
| `first_clear_reward` | reward packet | One-time reward |
| `repeat_clear_reward` | reward packet | Repeat clears |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `flavor_title` | e.g., "The Mudgrip" — subtitle shown on encounter card |
| `lore_note` | Brief context |
| `forgotten_manual_drop` | Manual ID if clearing this Warden can drop a Forgotten Manual |
| `notes` | |

**Example row:**
```
wdn_lw_001_mudgrip, The Mudgrip, loamwake, zone_lw_002_mudpipe_hollow, strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow, 3, force, 2, {mooncaps:300 strain_seeds:15 fixture_material.crumbled_ore_chunk:8}, {mooncaps:50}, launch
```

---

## Buried Clue Template

Buried Clues are hidden discoveries within zones — main source of Forgotten Manuals and world lore.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `clue_id` | string | |
| `display_name` | string | |
| `zone_id` | string | Which zone this can surface in |
| `unlock_condition` | gate expression | |
| `discovery_trigger` | string | e.g., `explore_action`, `warden_clear`, `duty_complete` |
| `discovery_chance` | float | 0.0–1.0 (per trigger) |
| `reward` | reward packet | |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `forgotten_manual_id` | If this Buried Clue grants a Forgotten Manual |
| `lore_note` | Flavor on discovery |
| `multi_discover` | `true` = can be found again by other trigger types |
| `notes` | |

---

## Wanderer Template

Wanderers are roaming encounter characters (travellers, traders, visitors) that appear in Strata zones.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `wnd_id` | string | |
| `display_name` | string | |
| `character_type` | const: `wanderer` | |
| `encounter_zones` | list[string] | Zone IDs where this Wanderer can appear |
| `unlock_condition` | gate expression | |
| `encounter_trigger` | string | e.g., `explore_action` |
| `encounter_chance` | float | Per trigger |
| `interaction_options` | list[InteractionOption] | e.g., `{trade, barter, help}` |
| `reward_on_help` | reward packet | |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `flavor_intro` | Opening line of dialogue (one line max) |
| `return_cooldown_hours` | How long before they can appear again |
| `notes` | |

---

## Runaway Template

Runaways are fleeing characters that can be caught for rewards.

**Required columns:** Same as Wanderer, with additions:

| Column | Type | Notes |
|--------|------|-------|
| `run_id` | string | |
| `character_type` | const: `runaway` | |
| `catch_battle_stat` | string | Stat used to catch (e.g., `speed`, `grit`) |
| `catch_difficulty` | int | 1–5 |
| `reward_on_catch` | reward packet | |
| `escape_consequence` | string | What happens if player fails to catch (usually nothing) |

**Optional columns:** `flavor_scared_line`, `notes`

---

## Wild One Template

The Wild Ones are feral encounter creatures with combat-only resolution.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `wld_id` | string | |
| `display_name` | string | |
| `character_type` | const: `wild_one` | |
| `encounter_zones` | list[string] | |
| `unlock_condition` | gate expression | |
| `combat_difficulty` | int | 1–10 |
| `required_battle_stat` | string | |
| `reward_on_win` | reward packet | |
| `spawn_weight` | int | Relative spawn weight in encounter table |
| `mvp_status` | enum | |

**Optional columns:** `lore_note`, `ambient_behavior`, `notes`

---

## Confidant Template

Confidants are named characters with personal story chains (separate from Burrowfolk). MVP content uses placeholder names.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `cnf_id` | string | |
| `display_name` | string | |
| `backstory_hook` | string | One-line narrative summary |
| `bond_stat_focus` | string | Heritage Stats this Confidant develops (`renown`, `craft`, etc.) |
| `max_bond_level` | int | Usually 5 |
| `duty_chain_ids` | list[string] | Duty IDs for this Confidant's story chain |
| `unlock_condition` | gate expression | |
| `placeholder_flag` | bool | `true` for MVP content |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `replacement_note` | Where the final content will come from (e.g., "friend questionnaire") |
| `visual_theme` | Rough art direction note |
| `notes` | |

**Example rows:**
```
cnf_id, display_name, backstory_hook, bond_stat_focus, max_bond_level, duty_chain_ids, unlock_condition, placeholder_flag, mvp_status
cnf_001_placeholder_greta, Greta (placeholder), A retired Strata surveyor who knows where the old rail ran., craft, 5, dty_cnf001_01;dty_cnf001_02;dty_cnf001_03, strata_progress.loamwake.clears >= 1, true, launch
cnf_002_placeholder_moss, Mossvane (placeholder), A wandering spore merchant with incomplete maps., lore, 5, dty_cnf002_01;dty_cnf002_02, strata_progress.loamwake.clears >= 2, true, launch
```

---

## Burrowfolk Template

Burrowfolk are work unit companions deployed in The Burrow (distinct from Confidants).

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `bfk_id` | string | |
| `display_name` | string | |
| `unit_role` | string | `gatherer`, `crafter`, `clash_support`, `mixed` |
| `work_output_type` | string | Resource ID produced |
| `work_output_rate_per_hour` | float | Units of resource per hour while deployed |
| `clash_support_bonus` | string \| null | e.g., `+5% force_vs_wanderers` |
| `upgrade_material_ids` | list[string] | |
| `upgrade_levels` | int | How many upgrade levels |
| `unlock_condition` | gate expression | |
| `placeholder_flag` | bool | |
| `mvp_status` | enum | |

**Optional columns:** `visual_theme`, `flavor_trait`, `notes`

**Example rows:**
```
bfk_id, display_name, unit_role, work_output_type, work_output_rate_per_hour, clash_support_bonus, upgrade_material_ids, upgrade_levels, unlock_condition, placeholder_flag, mvp_status
bfk_001_placeholder_mudger, Mudger (placeholder), gatherer, tangled_root_twine, 2.5, null, loam_fiber;mooncaps, 3, burrow_state.burrow_level >= 1, true, launch
bfk_002_placeholder_rootnip, Rootnip (placeholder), clash_support, crumbled_ore_chunk, 1.0, +5% force vs wardens, crumbled_ore_chunk;polishes, 3, strata_progress.loamwake.clears >= 1, true, launch
```

---

## Treasure Template

Treasures are unique collectibles displayed in the Vault of Treasures, with Pathwheel embed slots.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `tre_id` | string | |
| `display_name` | string | |
| `treasure_category` | string | `Relic`, `Curio`, `Monument` |
| `rarity` | enum | |
| `pathwheel_slots` | int | 1–6 |
| `base_effect` | string | Always-active effect |
| `acquisition_source` | string | |
| `unlock_condition` | gate expression | |
| `mvp_status` | enum | |

**Optional columns:** `flavor_lore`, `related_strata`, `related_event`, `notes`

---

## Fixture Template

Personal Fixtures only. Do NOT include War Fixtures here — use the War Fixture Template.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `fix_id` | string | |
| `display_name` | string | |
| `fixture_scope` | const: `personal` | Always `personal` in this template |
| `fixture_category` | enum: `farming`, `pure_attack`, `strain` | |
| `rarity` | enum | |
| `unlock_condition` | gate expression | |
| `acquisition_source` | string | |
| `recipe_id` | string \| null | FK → fix_rec_ ID |
| `rootrail_recipe_gate` | string \| null | Rootrail step required before recipe unlocks |
| `primary_effect_type` | string | e.g., `mushcap_gather_rate` |
| `primary_effect_value` | float | |
| `context_condition` | string \| null | When context bonus activates |
| `context_bonus_type` | string \| null | |
| `context_bonus_value` | float \| null | |
| `no_negative_stats_confirmed` | const: `true` | Always true |
| `duplicate_style_allowed` | bool | `true` by default; `false` = unique-build intent |
| `personal_capacity_required` | int | Minimum cap needed to equip |
| `max_upgrade_level` | int | |
| `salvage_output` | string | e.g., `tangled_root_twine:3;mooncaps:20` |
| `mvp_status` | enum | |

**Optional columns:**

| Column | Notes |
|--------|-------|
| `related_strata` | |
| `related_strain` | |
| `duplicate_diminish` | `true` if second copy gives reduced bonus |
| `notes` | |

**Example rows:**
```
fix_id, display_name, fixture_scope, fixture_category, rarity, unlock_condition, acquisition_source, recipe_id, rootrail_recipe_gate, primary_effect_type, primary_effect_value, context_condition, context_bonus_type, context_bonus_value, no_negative_stats_confirmed, duplicate_style_allowed, personal_capacity_required, max_upgrade_level, salvage_output, mvp_status
fix_001_root_shovel_strap, Root-Bitten Shovel Strap, personal, farming, common, account.fixture_cap >= 1, crafting, fix_rec_001, null, mushcap_gather_rate, 0.08, crack_state.current_run_depth > 0, crack_coin_bonus, 0.04, true, true, 1, 3, tangled_root_twine:3;mooncaps:20, launch
fix_002_loamwake_canister, Loamwake Canister, personal, pure_attack, common, account.fixture_cap >= 1, crafting, fix_rec_002, null, battle_stat_force, 0.12, strata_progress.loamwake.warden_encounter == true, force_vs_wardens, 0.06, true, true, 1, 3, crumbled_ore_chunk:2;mooncaps:30, launch
fix_003_pale_glow_lens, Pale Glow Lens, personal, strain, uncommon, account.fixture_cap >= 2, crafting, fix_rec_003, null, strain_seed_capacity, 10, account.active_strain contains pale_glow, strain_seed_yield, 0.05, true, true, 2, 5, dull_glow_shard:3;mooncaps:40, launch
```

---

## Hat Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `hat_id` | string | |
| `display_name` | string | |
| `hat_tier` | int | 1 = basic; higher = more prestige |
| `identity_theme` | string | e.g., `explorer`, `clique_leader`, `veteran` |
| `social_status_tag` | string \| null | Social display tag |
| `unlock_condition` | gate expression | |
| `acquisition_source` | string | |
| `visible_asset_key` | string | Art asset key |
| `display_rule` | const: `one_visible_hat_at_a_time` | Always this value |
| `permanent_passive_type` | string | e.g., `mushcap_gather_rate` |
| `permanent_passive_value` | float | Small; typically 0.01–0.02 |
| `passive_stack_rule` | const: `all_unlocked_hats_stack_tiny_passives` | Always this value |
| `counts_against_fixture_cap` | const: `false` | Always false |
| `mvp_status` | enum | |

**Optional columns:** `related_event`, `related_strata`, `related_clique_context`, `notes`

**Example rows:**
```
hat_id, display_name, hat_tier, identity_theme, social_status_tag, unlock_condition, acquisition_source, visible_asset_key, display_rule, permanent_passive_type, permanent_passive_value, passive_stack_rule, counts_against_fixture_cap, mvp_status
hat_001_loamwake_dirt_cap, Loamwake Dirt Cap, 1, explorer, Loamwake Scout, strata_progress.loamwake.clears >= 1, duty_reward, hat_loamwake_dirt_cap_v1, one_visible_hat_at_a_time, mushcap_gather_rate, 0.01, all_unlocked_hats_stack_tiny_passives, false, launch
hat_002_glow_rim, Glow Rim Hat, 2, collector, Glowchaser, strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 3, warden_drop, hat_glow_rim_v1, one_visible_hat_at_a_time, echo_shard_drop_rate, 0.01, all_unlocked_hats_stack_tiny_passives, false, launch
```

---

## War Fixture Template

War Fixtures are separate from personal Fixtures and live in the War Armory. Do not model these in the Fixture Template.

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `wfx_id` | string | |
| `display_name` | string | |
| `fixture_scope` | const: `war_armory` | Always `war_armory` |
| `war_role` | string | `frontline`, `support`, `sabotage` |
| `rarity` | enum | |
| `unlock_condition` | gate expression | Must include Clique/war gate |
| `war_armory_gate` | string | e.g., `clique_state.war_armory_unlocked == true` |
| `normal_style_trait_tags` | list[string] | Traits shared with personal Fixtures |
| `war_specific_trait_tags` | list[string] | War-only traits |
| `primary_war_effect` | string | Main effect in war context |
| `counts_against_personal_fixture_cap` | const: `false` | Always false |
| `armory_limit_rule` | string | Max equipped per war roster |
| `mvp_status` | const: `stub` | War Armory is MVP-stub |

**Optional columns:** `secondary_war_effect`, `non_war_effect_rule`, `upgrade_inputs`, `salvage_output`, `related_event`, `notes`

---

## Rootrail Repair Step Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `rtr_step_id` | string | |
| `route_id` | string | Which Rootrail route |
| `step_order` | int | |
| `step_name` | string | |
| `repair_phase` | string | e.g., `loamwake_terminal` |
| `unlock_condition` | gate expression | |
| `rootrail_parts_cost` | int | |
| `forgotten_manual_required` | bool | |
| `forgotten_manual_id` | string \| null | Required manual ID; null if not required |
| `manual_consumed` | const: `false` | Always false |
| `timer_seconds_base` | int | |
| `output_unlock_type` | string | What type of thing is unlocked |
| `output_unlock_id` | string \| null | |
| `new_route_unlocked` | string \| null | |
| `rare_material_unlocks` | string \| null | Semicolon-separated material IDs |
| `fixture_recipe_unlocks` | string \| null | Semicolon-separated recipe IDs |
| `account_bonus_type` | string \| null | |
| `account_bonus_value` | float \| null | |
| `codex_entry_unlocked` | string \| null | |
| `mvp_status` | enum | |

**Optional columns:** `notes`

**Example rows:**
```
rtr_step_id, route_id, step_order, step_name, repair_phase, unlock_condition, rootrail_parts_cost, forgotten_manual_required, forgotten_manual_id, manual_consumed, timer_seconds_base, output_unlock_type, output_unlock_id, new_route_unlocked, rare_material_unlocks, fixture_recipe_unlocks, account_bonus_type, account_bonus_value, codex_entry_unlocked, mvp_status
rtr_step_001, route_loamwake_01, 1, Clear the Platform, loamwake_terminal, rootrail_state.rootrail_unlocked == true, 20, true, fman_001_terminal_routing, false, 7200, account_bonus, null, null, null, null, rootrail_parts_yield, 0.02, codex_lore_001, launch
rtr_step_002, route_loamwake_01, 2, Reconnect the First Car, loamwake_terminal, rootrail_state.completed_steps contains rtr_step_001, 35, false, null, false, 28800, route_unlock, route_loamwake_01a, route_loamwake_01a, null, fix_rec_006, mushcap_gather_rate, 0.01, codex_lore_002, launch
```

---

## Forgotten Manual Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `manual_id` | string | |
| `title` | string | |
| `codex_category` | string | `Rail Knowledge`, `World History`, `Craft Secrets` |
| `knowledge_domain` | string | Thematic area |
| `acquisition_source` | string | Hint for players |
| `unlock_condition` | gate expression | When this manual can be found |
| `permanent_unlock_type` | string | What this knowledge permanently unlocks |
| `permanent_unlock_id` | string \| null | |
| `rootrail_step_gate_ids` | string \| null | Semicolon-separated step IDs this manual gates |
| `fixture_recipe_ids` | string \| null | Semicolon-separated recipe IDs this manual contributes to |
| `codex_entry_summary` | string | Player-facing lore text |
| `discovered_in_strata` | string \| null | |
| `separate_from_elder_books_confirmed` | const: `true` | Always true |
| `mvp_status` | enum | |

**Optional columns:** `related_route_id`, `account_bonus_ids`, `notes`

**Example rows:**
```
manual_id, title, codex_category, knowledge_domain, acquisition_source, unlock_condition, permanent_unlock_type, permanent_unlock_id, rootrail_step_gate_ids, fixture_recipe_ids, codex_entry_summary, discovered_in_strata, separate_from_elder_books_confirmed, mvp_status
fman_001_terminal_routing, Terminal Routing Manual, Rail Knowledge, rail_engineering, buried_clue in zone_lw_001_rootvine_shelf, rootrail_state.rootrail_unlocked == true, rootrail_step_gate, rtr_step_001, rtr_step_001, null, A water-damaged routing index for the lower Loamwake terminal., loamwake, true, launch
```

---

## Event Ladder Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `evt_id` | string | |
| `display_name` | string | |
| `event_type` | enum: `lucky_draw_week`, `treasure_week`, `echo_week`, `beginner`, `hoard` | |
| `tier` | int | Ladder tier (1-indexed) |
| `tier_threshold` | int | Points/resource required to reach this tier |
| `tier_reward` | reward packet | |
| `currency_input_type` | string | Which currency is spent/earned |
| `active_duration_hours` | int | Event window |
| `mvp_status` | enum | |

**Optional columns:** `bonus_tier_flag`, `clique_bonus_multiplier`, `notes`

---

## Festival Ledger Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `fstv_id` | string | |
| `display_name` | string | |
| `week_index` | int | Which festival week |
| `daily_tasks` | list[string] | Duty/activity IDs that generate Festival Marks |
| `ledger_tiers` | int | Number of claimable tiers |
| `tier_rewards` | list[reward packet] | One per tier |
| `festival_mark_cost_per_tier` | int | |
| `lucky_stall_available` | bool | |
| `lucky_stall_cost` | string \| null | Currency and quantity |
| `mvp_status` | enum | |

---

## Burrow Post Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `post_id` | string | |
| `display_name` | string | |
| `post_type` | string | `gather`, `escort`, `craft`, `investigate` |
| `strata_id` | string | |
| `unlock_condition` | gate expression | |
| `objective` | string | Plain-English |
| `duration_hours` | float | How long to complete |
| `reward` | reward packet | |
| `repeatable` | bool | |
| `repeat_cooldown_hours` | float \| null | |
| `mvp_status` | enum | |

---

## Duty Template

**Required columns:**

| Column | Type | Notes |
|--------|------|-------|
| `dty_id` | string | |
| `display_name` | string | |
| `duty_type` | enum: `daily`, `story`, `confidant`, `chain`, `challenge` | |
| `strata_id` | string | |
| `unlock_condition` | gate expression | |
| `objective` | string | |
| `reward` | reward packet | |
| `repeatable` | bool | |
| `repeat_cadence` | string \| null | `daily`, `weekly`, `once` |
| `confidant_id` | string \| null | FK if confidant Duty |
| `chain_next_dty_id` | string \| null | Next Duty in chain |
| `mvp_status` | enum | |

**Optional columns:** `fixture_recipe_unlock`, `forgotten_manual_reward`, `notes`

---

## MVP Placeholder Rules

For Loamwake MVP, the following rules apply to all content using placeholder content:

1. Set `placeholder_flag = true` on every Confidant and Burrowfolk row.
2. Add `replacement_note = "Pending friend-questionnaire replacement"` in the notes field.
3. Do NOT write final lore text or dialogue for placeholder Confidants.
4. Placeholder names should be generic but gnome-world-flavored (e.g., Greta, Mossvane, Mudger, Rootnip).
5. Placeholder content must still have realistic stat values and functional Duty chains — placeholders are playable, not empty.

---

## Validation Checklist

Before submitting any content sheet for review:

- [ ] All IDs follow the naming convention for their type
- [ ] No row uses "Quest," "NPC," "Guild," "Region Weather," or "Clothes"
- [ ] No War Fixture appears in the Fixture template rows
- [ ] All `no_negative_stats_confirmed` values are `true` on Fixture rows
- [ ] All `counts_against_fixture_cap` values are `false` on Hat rows
- [ ] All `counts_against_personal_fixture_cap` values are `false` on War Fixture rows
- [ ] All `manual_consumed` values are `false` on Rootrail repair step rows
- [ ] All `separate_from_elder_books_confirmed` values are `true` on Forgotten Manual rows
- [ ] MVP placeholder content has `placeholder_flag = true`
- [ ] All `mvp_status` values are set and valid
