# Data Schema v1 — Gnome Game

**Status:** Canon — Phase 2A Salvage Rewrite  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** All major game entities and player state. Implementation and content-design reference.

---

## Status, Version, Scope

This document defines the production-facing data model for the Gnome Game prototype and future live-service backend. It covers content definitions (static, server-authored data) and player state (per-account mutable data).

All entity names use `snake_case`. All IDs are strings. Timestamps are ISO 8601 UTC.

**This schema does NOT model:**
- Clothes (removed from canon)
- The Ascender (replaced by Rootrail)
- Rigid gear slots (replaced by ordered Fixture loadout array)
- Generic RPG entities (Quest → Duties; NPC → Confidant/Burrowfolk/Warden; Guild → Clique)

---

## Schema Principles

1. **Definitions vs. State** — Content definitions are static tables authored by the team (e.g., `fixture_recipe`, `rootrail_repair_step`). Player state records are per-account mutable documents (e.g., `fixture_loadout`, `rootrail_state`).
2. **Additive expansion** — All state documents include a `meta` field for live-service expansion without schema migrations.
3. **No client trust on economy** — All currency/material spend, unlock grants, and timer completions must be validated server-side.
4. **Unlock conditions as expressions** — Gates are stored as human-readable condition strings resolvable by a gate evaluator (e.g., `"strata_progress.loamwake.clears >= 1"`).
5. **Soft-delete everywhere** — State records use `deleted_at` / `active: false` rather than hard delete; enables audit/rollback.

---

## Definition Tables vs. Player State

| Definition (static content) | Player State (mutable) |
|-----------------------------|----------------------|
| `fixture_recipe` | `fixture_item` (instance) |
| `hat_definition` | `hat_unlock`, `hat_display_state` |
| `rootrail_repair_step` | `rootrail_state`, `rootrail_codex_entry` |
| `forgotten_manual` | `rootrail_codex_entry` |
| `treasure` (definition) | `treasure_embed_state` |
| `confidant` (definition) | `confidant_state` (per-account) |
| `burrowfolk_unit` (definition) | `burrowfolk_deployment` |
| `duty` (definition) | `duty_progress` |
| `encounter` (definition) | `encounter_state` |
| `event` (definition) | `event_state` |
| `war_fixture_item` (definition) | `war_armory_loadout` |

---

## Account and Profile State

### `account`

Core player identity record.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string (UUID) | Primary key |
| `display_name` | string | Player-set name |
| `created_at` | timestamp | Account creation time |
| `last_login_at` | timestamp | Last session open |
| `platform` | enum: `ios`, `android`, `pc` | Platform at account creation |
| `fixture_cap` | int | Current personal Fixture cap (starts 0) |
| `hat_passives` | map[string → float] | Accumulated hat passive bonuses |
| `rootrail_passives` | map[string → float] | Accumulated Rootrail account bonuses |
| `tutorial_flags` | map[string → bool] | Per-tutorial-step completion flags |
| `settings` | object | Player preferences (notifications, etc.) |
| `clique_id` | string \| null | Current Clique membership |
| `meta` | object | Expansion fields |

---

## Currency, Material, and Inventory Ledger

### `wallet`

Per-account resource ledger. Single document; all soft currencies and materials stored here.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `mooncaps` | int | Primary soft currency |
| `glowcaps` | int | Secondary soft currency (premium-adjacent; earnable) |
| `mushcaps` | int | Exploration stamina currency |
| `lucky_draws` | int | Lucky Draw event tickets |
| `treasure_tickets` | int | Treasure Week access tokens |
| `echoes` | int | Echo Week offering resource |
| `echo_shards` | int | Secondary echo resource |
| `strain_seeds` | int | Strain progression material |
| `treasure_shards` | int | Treasure fragment currency |
| `polishes` | int | Fixture upgrade input |
| `crack_coins` | int | The Crack progression currency |
| `bronze_shovels` | int | The Crack special dig resource |
| `favor_marks` | int | Clique social currency |
| `strata_seals` | int | Deepening resource |
| `elder_books` | int | Ritual/key item for Deepening and upgrades |
| `festival_marks` | int | Festival Ledger participation currency |
| `rootrail_parts` | int | Rootrail repair material |
| `fixture_materials` | map[string → int] | Named material counts (e.g., `tangled_root_twine: 14`) |
| `last_updated_at` | timestamp | Last mutation timestamp |
| `meta` | object | Expansion fields |

---

## Burrow State

### `burrow_state`

The Burrow is the player's home base. Its state tracks expansion, active posts, and work slots.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `burrow_level` | int | Current Burrow expansion level |
| `unlocked_rooms` | list[string] | Room/area IDs unlocked |
| `active_burrowfolk_slots` | int | Number of active Burrowfolk deployment slots |
| `burrow_work_queue` | list[BurrowWorkItem] | Current queued Burrow work tasks |
| `last_collected_at` | timestamp | Last Burrow output collection |
| `meta` | object | Expansion fields |

### `BurrowWorkItem`
```
burrowfolk_unit_id: string
task_id: string
started_at: timestamp
completes_at: timestamp
output_resource: string
output_qty: int
```

---

## Strata Progress State

### `strata_progress`

Per-account per-Strata exploration and unlock state.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `strata_id` | string | e.g., `loamwake`, `ashcrag`, `deepfen` |
| `clears` | int | Number of full Strata clears |
| `current_zone_id` | string | Active zone within this Strata |
| `zones_unlocked` | list[string] | All zone IDs discovered |
| `warden_clears` | map[string → int] | Warden ID → clear count |
| `active_strata_trait_id` | string \| null | Currently active Strata Trait |
| `buried_clues_found` | list[string] | Buried Clue IDs discovered |
| `strata_trait_history` | list[string] | Past Strata Trait IDs used |
| `meta` | object | Expansion fields |

---

## Deepening State

### `deepening_state`

Tracks the player's Deepening (prestige) cycle history and current position.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `deepening_count` | int | Total Deepenings completed |
| `current_deepening_level` | int | Post-last-Deepening level within cycle |
| `memory_shift_unlocked` | bool | Has the player reached Memory Shift |
| `memory_shift_depth` | int | Current Memory Shift depth |
| `strata_seals_spent_total` | int | Lifetime Strata Seals spent on Deepening |
| `elder_books_spent_total` | int | Lifetime Elder Books spent |
| `deepening_bonuses` | list[BonusEntry] | Permanent bonuses from Deepenings |
| `meta` | object | Expansion fields |

---

## Crack State

### `crack_state`

The Crack is the never-ending progression system (long-horizon depth).

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `crack_depth` | int | Current depth reached in The Crack |
| `crack_coins_earned_total` | int | Lifetime Crack Coins earned |
| `bronze_shovels_used_total` | int | Lifetime Bronze Shovels used |
| `current_run_depth` | int \| null | Depth in the currently active run (null if no active run) |
| `current_run_started_at` | timestamp \| null | Active run start time |
| `best_run_depth` | int | Personal best single-run depth |
| `crack_unlocks` | list[string] | Content IDs unlocked via Crack depth milestones |
| `meta` | object | Expansion fields |

---

## Event State

### `event_state`

Per-account per-event participation state.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `event_id` | string | FK → event definition |
| `event_type` | enum: `lucky_draw_week`, `treasure_week`, `echo_week`, `festival_ledger`, `beginner_event`, `hoard_event` | |
| `ladder_tier_reached` | int | Highest event ladder tier completed |
| `points_accumulated` | int | Total points earned this event instance |
| `currencies_spent` | map[string → int] | Resource type → quantity spent |
| `rewards_claimed` | list[string] | Reward IDs claimed |
| `started_at` | timestamp | When player joined this event |
| `ends_at` | timestamp | Event end time (from definition) |
| `meta` | object | Expansion fields |

---

## Treasure and Pathwheel State

### `treasure`

Content definition for a Treasure item.

| Field | Type | Description |
|-------|------|-------------|
| `treasure_id` | string | e.g., `tre_001_mossy_dial` |
| `display_name` | string | |
| `treasure_category` | string | e.g., `Relic`, `Curio`, `Monument` |
| `rarity` | enum: `common`, `uncommon`, `rare`, `legendary` | |
| `pathwheel_slots` | int | Number of embed slots on this Treasure's Pathwheel |
| `base_effect` | string | Always-active effect description |
| `acquisition_source` | string | How to obtain |
| `mvp_status` | enum: `launch`, `stub`, `future` | |

### `treasure_embed_state`

Per-account Treasure ownership and Pathwheel embed configuration.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `treasure_id` | string | FK → treasure definition |
| `owned` | bool | Whether this treasure is owned |
| `pathwheel_embeds` | list[EmbedSlot] | Current gem/item embed configuration |
| `acquired_at` | timestamp | |
| `meta` | object | Expansion fields |

### `EmbedSlot`
```
slot_index: int
embedded_item_id: string | null
embedded_at: timestamp | null
```

---

## Fixture Definitions and Player Fixture State

### `fixture_item` (definition)

Content definition for a Fixture.

| Field | Type | Description |
|-------|------|-------------|
| `fixture_id` | string | e.g., `fix_001_root_shovel_strap` |
| `display_name` | string | |
| `fixture_scope` | enum: `personal`, `war_armory` | |
| `fixture_category` | enum: `farming`, `pure_attack`, `strain` | (war scope uses war_fixture_item) |
| `rarity` | enum: `common`, `uncommon`, `rare`, `legendary` | |
| `unlock_condition` | string | Gate expression or `null` for always-available |
| `recipe_id` | string \| null | FK → fixture_recipe |
| `primary_effect_type` | string | e.g., `mushcap_gather_rate` |
| `primary_effect_value` | float | |
| `context_condition` | string \| null | Condition string for context bonus |
| `context_bonus_type` | string \| null | Bonus type when context condition met |
| `context_bonus_value` | float \| null | |
| `no_negative_stats_confirmed` | bool | Always `true`; validation sentinel |
| `duplicate_diminish` | bool | `false` by default; `true` = reduced second-stack |
| `max_upgrade_level` | int | Upgrade cap for this rarity |
| `salvage_output` | map[string → int] | Material yields on salvage |
| `mvp_status` | enum: `launch`, `stub`, `future` | |

### `fixture_recipe` (definition)

| Field | Type | Description |
|-------|------|-------------|
| `recipe_id` | string | e.g., `fix_rec_001` |
| `output_fixture_id` | string | FK → fixture_item |
| `rootrail_step_gate` | string \| null | Required Rootrail step ID, or null |
| `mooncap_cost` | int | |
| `material_inputs` | map[string → int] | Material ID → quantity |
| `craft_time_seconds` | int | 0 = instant |
| `unlock_condition` | string \| null | Gate expression |

### `fixture_loadout` (player state)

Per-account Fixture equipped state.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `equipped` | list[string] | Ordered array of fixture instance IDs; length ≤ `account.fixture_cap` |
| `fixture_inventory` | list[FixtureInstance] | All owned, unequipped Fixtures |
| `last_updated_at` | timestamp | |

### `FixtureInstance`
```
instance_id: string (UUID)
fixture_id: string
upgrade_level: int
acquired_at: timestamp
```

---

## Hat Definitions and Player Hat State

### `hat_definition` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `hat_id` | string | e.g., `hat_001_loamwake_dirt_cap` |
| `display_name` | string | |
| `hat_tier` | int | Rarity/prestige tier |
| `identity_theme` | string | e.g., `explorer`, `clique_leader`, `festival_veteran` |
| `social_status_tag` | string \| null | Tag shown in social contexts |
| `unlock_condition` | string | Gate expression |
| `acquisition_source` | string | |
| `visible_asset_key` | string | Art asset reference |
| `permanent_passive_type` | string | Bonus category |
| `permanent_passive_value` | float | Bonus amount |
| `passive_stack_rule` | const: `all_unlocked_hats_stack_tiny_passives` | Always this value |
| `counts_against_fixture_cap` | const: `false` | Always false |
| `related_event` | string \| null | |
| `mvp_status` | enum: `launch`, `stub`, `future` | |

### `hat_unlock` (player state)

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `hat_id` | string | FK → hat_definition |
| `unlocked` | bool | Whether this hat is in the collection |
| `unlocked_at` | timestamp \| null | |
| `permanent_passive_active` | const: `true` | Always true once unlocked; no deactivation |

### `hat_display_state` (player state)

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `visible_hat_id` | string \| null | Currently displayed hat; null = bare head |
| `last_changed_at` | timestamp | |

---

## War Fixture and War Armory State

### `war_fixture_item` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `war_fixture_id` | string | e.g., `wfx_001_clique_ram_brace` |
| `display_name` | string | |
| `fixture_scope` | const: `war_armory` | Always `war_armory` |
| `war_role` | string | e.g., `frontline`, `support`, `sabotage` |
| `rarity` | enum | |
| `clique_or_war_requirement` | string | Gate: must be in Clique or active war |
| `normal_style_traits` | list[TraitEntry] | Traits shared with personal Fixtures |
| `war_specific_traits` | list[TraitEntry] | War-only traits |
| `counts_against_personal_fixture_cap` | const: `false` | Always false |
| `mvp_status` | const: `stub` | War Armory is out of MVP scope |

### `war_armory_loadout` (player state)

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `clique_id` | string | Active Clique |
| `war_equipped` | list[string] | War Fixture instance IDs for active war |
| `war_inventory` | list[WarFixtureInstance] | Owned war Fixtures |
| `armory_locked` | bool | `true` until War Armory unlocks |

---

## Rootrail Definitions and Player Rootrail State

### `rootrail_repair_step` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `step_id` | string | e.g., `rtr_step_001` |
| `route_id` | string | Which Rootrail route this step belongs to |
| `step_order` | int | Order within the route (1-indexed) |
| `step_name` | string | Display name |
| `repair_phase` | string | e.g., `loamwake_terminal`, `ashcrag_line` |
| `rootrail_parts_cost` | int | Rootrail Parts required |
| `forgotten_manual_required` | bool | Whether a Forgotten Manual gates this step |
| `forgotten_manual_id` | string \| null | Required manual ID |
| `timer_seconds_base` | int | Timer duration at this step |
| `output_unlock_type` | string | What the step unlocks |
| `output_unlock_id` | string \| null | ID of the unlocked content |
| `new_route_unlocked` | string \| null | Route ID unlocked on completion |
| `rare_material_unlocks` | list[string] | Material IDs whose source is gated |
| `fixture_recipe_unlocks` | list[string] | Recipe IDs unlocked |
| `account_bonus_type` | string \| null | Bonus type granted to account |
| `account_bonus_value` | float \| null | |
| `codex_entry_unlocked` | string \| null | Lore codex entry ID |
| `mvp_status` | enum: `launch`, `stub`, `future` | |

### `rootrail_state` (player state)

Per-account Rootrail restoration progress.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `rootrail_unlocked` | bool | Has the station been revealed |
| `current_step_id` | string \| null | Active repair step ID |
| `current_step_started_at` | timestamp \| null | When the timer began |
| `current_step_timer_end_utc` | timestamp \| null | Server-authoritative completion timestamp |
| `completed_steps` | list[CompletedStep] | All steps finished |
| `routes_unlocked` | list[string] | Route IDs accessible |
| `account_bonuses_granted` | list[BonusEntry] | Bonuses applied to account |
| `meta` | object | Expansion fields |

### `CompletedStep`
```
step_id: string
completed_at: timestamp
reward_claimed: bool
reward_claimed_at: timestamp | null
```

---

## Forgotten Manual and Codex State

### `forgotten_manual` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `manual_id` | string | e.g., `fman_001_terminal_routing` |
| `title` | string | Display title |
| `codex_category` | string | e.g., `Rail Knowledge`, `World History`, `Craft Secrets` |
| `knowledge_domain` | string | Thematic domain |
| `acquisition_source` | string | Hint text (where to find it) |
| `permanent_unlock_type` | string | What knowledge this permanently unlocks |
| `rootrail_step_gate_ids` | list[string] | Repair steps that require this manual |
| `fixture_recipe_ids` | list[string] | Recipes this manual contributes to |
| `codex_entry_summary` | string | Lore-facing description in codex |
| `discovered_in_strata` | string \| null | Which Strata this is typically found in |
| `separate_from_elder_books_confirmed` | const: `true` | Always true |
| `mvp_status` | enum | |

### `rootrail_codex_entry` (player state)

Per-account per-manual discovery record.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `manual_id` | string | FK → forgotten_manual |
| `discovered` | bool | Whether this manual is in the codex |
| `discovered_at` | timestamp \| null | |
| `consumed` | const: `false` | Always false; Forgotten Manuals are never consumed |

---

## Strain Progress State

### `strain_progress`

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `active_strain_id` | string \| null | Currently active Strain |
| `strains_unlocked` | list[string] | All Strain IDs unlocked |
| `strain_seeds_spent` | map[string → int] | Per-Strain seeds spent |
| `strain_loom_queue` | list[LoomItem] | Active Strain Loom research queue |
| `lore_map_progress` | map[string → bool] | Lore Map node ID → unlocked |
| `meta` | object | Expansion fields |

---

## Confidant and Burrowfolk State

### `confidant` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `confidant_id` | string | e.g., `cnf_001_placeholder_greta` |
| `display_name` | string | |
| `backstory_hook` | string | One-line narrative hook |
| `bond_stat_focus` | string | Which Heritage Stats they develop |
| `duty_chain_ids` | list[string] | Duty chain IDs for this Confidant |
| `unlock_condition` | string | Gate expression |
| `mvp_status` | enum | Note: MVP allows placeholder content |
| `placeholder_flag` | bool | `true` = temp content pending friend-questionnaire replacement |

### `burrowfolk_unit` (content definition)

| Field | Type | Description |
|-------|------|-------------|
| `burrowfolk_id` | string | e.g., `bfk_001_placeholder_mudger` |
| `display_name` | string | |
| `unit_role` | string | e.g., `gatherer`, `crafter`, `clash_support` |
| `work_output_type` | string | Resource type produced in Burrow work |
| `work_output_rate` | float | Output per hour |
| `clash_support_bonus` | string \| null | Bonus provided in Clash encounters |
| `upgrade_material_ids` | list[string] | Materials for upgrading this unit |
| `unlock_condition` | string | |
| `mvp_status` | enum | |
| `placeholder_flag` | bool | `true` = temp content |

### `confidant_state` (player state)

Per-account per-Confidant relationship and progress record.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `confidant_id` | string | FK → confidant definition |
| `unlocked` | bool | Has this Confidant been introduced |
| `unlocked_at` | timestamp \| null | |
| `trust_level` | int | Current bond level (1–5 scale; 0 = not yet bonded) |
| `trust_points` | int | Points accumulated toward next trust level |
| `active_duty_chain_ids` | list[string] | Duty chain IDs currently in progress |
| `completed_duty_ids` | list[string] | All individual Duty IDs completed with this Confidant |
| `calling_unlocked` | bool | Whether this Confidant's special ability is active |
| `last_interaction_at` | timestamp \| null | Last Duty completed or dialogue triggered |
| `meta` | object | Expansion fields |

---

## Duties, Burrow Posts, Encounters

### `duty` (content definition)

Duties are the primary quest/task system (do not call these "quests" in-game).

| Field | Type | Description |
|-------|------|-------------|
| `duty_id` | string | e.g., `dty_001_lw_gather_rootvine` |
| `display_name` | string | |
| `duty_type` | enum: `daily`, `story`, `confidant`, `chain`, `challenge` | |
| `strata_id` | string | Which Strata this Duty is in |
| `unlock_condition` | string | Gate expression |
| `objective` | string | Plain-English objective description |
| `reward_packet` | RewardPacket | |
| `repeatable` | bool | Can this Duty be done more than once |
| `repeat_cadence` | string \| null | e.g., `daily`, `weekly` |
| `mvp_status` | enum | |

### `duty_progress` (player state)

Per-account per-Duty tracking record.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `duty_id` | string | FK → duty definition |
| `duty_chain_id` | string \| null | FK → parent chain, if applicable |
| `state` | enum: `available`, `active`, `completed`, `expired`, `locked` | |
| `objective_progress` | map[string → int] | Objective key → current count |
| `started_at` | timestamp \| null | When duty was accepted |
| `completed_at` | timestamp \| null | When objective was fulfilled |
| `reward_claimed` | bool | Whether reward packet has been collected |
| `repeat_window_id` | string \| null | e.g., `daily_2026-04-19` for daily cadence tracking |
| `chain_next_dty_id` | string \| null | Next Duty in chain, copied from definition for fast lookup |
| `meta` | object | Expansion fields |

### `post_state`

Burrow Posts are notice-board encounters/tasks available from The Burrow.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `post_id` | string | Content definition ID |
| `state` | enum: `available`, `active`, `completed`, `expired` | |
| `accepted_at` | timestamp \| null | |
| `completed_at` | timestamp \| null | |
| `reward_claimed` | bool | |

### `encounter_state`

Per-account per-encounter tracking.

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `encounter_id` | string | Content definition ID |
| `encounter_type` | enum: `warden`, `wanderer`, `runaway`, `wild_one`, `buried_clue` | |
| `times_triggered` | int | How many times this encounter has appeared |
| `times_resolved` | int | How many times resolved successfully |
| `last_triggered_at` | timestamp \| null | |
| `outcome_history` | list[OutcomeEntry] | Last N outcomes |
| `active` | bool | Is an instance of this encounter currently in progress |

---

## Event State and Festival Ledger State

(See `event_state` in the Event State section above.)

### `festival_ledger_state`

| Field | Type | Description |
|-------|------|-------------|
| `account_id` | string | FK → account |
| `season_id` | string | Active season ID |
| `festival_marks_earned` | int | Marks earned this season |
| `ledger_tiers_claimed` | list[int] | Tier indices claimed |
| `lucky_stall_pulls` | int | Pulls from Lucky Stall this week |
| `echo_watch_score` | int | Current Echo Watch score |
| `meta` | object | |

---

## Clique, Crack, Deepening, and Memory Shift State

### `clique_state`

| Field | Type | Description |
|-------|------|-------------|
| `clique_id` | string | Clique entity ID |
| `founder_account_id` | string | FK → account |
| `clique_name` | string | |
| `roster` | list[CliqueMember] | `{account_id, role: founder|steward|burrowmate, joined_at}` |
| `clique_queue` | list[string] | Shared goal IDs in the Clique Queue |
| `favor_marks_pool` | int | Shared Clique Favor Marks pool |
| `war_armory_unlocked` | bool | Whether War Armory is active for this Clique |
| `great_dispute_stub` | bool | `true` = species war system is stubbed |

See `deepening_state` and `crack_state` above.

---

## Unlock Conditions and Gate Expressions

Unlock conditions are stored as human-readable strings evaluated by a gate resolver. Examples:

```
"strata_progress.loamwake.clears >= 1"
"account.fixture_cap >= 3"
"rootrail_state.completed_steps contains rtr_step_003"
"wallet.mooncaps >= 500"
"confidant_state.cnf_001.trust_level >= 2"
"event_state.lucky_draw_week_001.ladder_tier_reached >= 5"
"strata_progress.loamwake.warden_clears.wdn_lw_001_mudgrip >= 1"
```

The gate resolver must support: `>=`, `<=`, `==`, `contains`, `and`, `or`, `not`.

> **ID Prefix Note:** Warden IDs use the `wdn_` prefix (e.g., `wdn_lw_001_mudgrip`) and are stored in `strata_progress.<strata_id>.warden_clears`. War Fixture IDs use the `wfx_` prefix (e.g., `wfx_001_clique_ram_brace`) and are stored in `war_armory_loadout`. These prefixes must not be confused — `wdn_` is for Wardens; `wfx_` is for War Fixtures; `war_` is reserved for War Armory schema fields only.

---

## Save Migration and Live-Service Expansion Rules

1. **Additive only** — New fields append to existing documents; never rename or remove existing fields.
2. **`meta` object** — All documents include a free-form `meta` object for temporary live-service fields pending schema formalization.
3. **Version field** — All player state documents carry `schema_version: int`; migration scripts bump this.
4. **Content ID stability** — Once a `fixture_id`, `hat_id`, `manual_id`, etc. is published to production, it is immutable. Deprecate via `deprecated: true` flag, never delete.

---

## MVP Minimum Save Payload

For Loamwake MVP prototype, the minimum save state is:

```
account (core fields only)
wallet (all currency fields, fixture_materials subset)
burrow_state
strata_progress (loamwake stratum only)
fixture_loadout
hat_unlock (first hat only)
hat_display_state
rootrail_state (unlocked flag + step 1 state)
rootrail_codex_entry (first manual)
tutorial_flags
```

All other entities may be stubbed or omitted for prototype builds.

---

## Open Questions / Unresolved Items

> **UNRESOLVED:** Confidant bond system — what is the bond level scale? Assumed 1–5 (from Super Snail reference), but not yet validated against canon.

> **UNRESOLVED:** Pathwheel — is it per-Treasure or a shared board? Current model: per-Treasure. Needs validation.

> **UNRESOLVED:** Duty chain vs. Duty — should linked sequential Duties be modeled as a `duty_chain` parent entity with ordered `duty` children, or as `chain_type` duties with `next_duty_id` pointers? Recommend: parent-chain model for clarity.

> **UNRESOLVED:** War Armory unlock condition — is it tied to Clique rank, Clique level, or a server toggle? Kept as stub for now.

> **UNRESOLVED:** Analytics event naming convention not defined here. Recommend a separate analytics spec.
