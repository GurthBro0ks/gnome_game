# Save State and Profile Flow

**Status:** Canon - Phase 2B File  
**Version:** 1.0.0  
**Date:** 2026-04-20  
**Scope:** MVP profile creation, local save behavior, session resume, recovery, and migration-safe persistence ownership.

---

## 1. Status, Scope, Dependencies

This file defines the Loamwake MVP save/profile contract. It turns the current schema and Phase 2B authored content into buildable persistence guidance: what is created on first profile creation, which mutations save immediately, which UI state is recomputed, how interrupted sessions resume, and how local-first data can later migrate to cloud save without rewriting content definitions.

**Source documents:**
- `docs/04_systems/data_schema_v1.md`
- `docs/04_systems/core_loop_mvp_spec.md`
- `docs/04_systems/tutorial_and_onboarding_flow.md`
- `docs/07_ui/unlock_flow_and_ui_map.md`
- `docs/06_content/loamwake_mvp_content_sheet.md`
- `docs/06_content/first_confidant_chain.md`
- `docs/06_content/first_wanderer_pool.md`
- `docs/06_content/lucky_draw_week_mvp.md`
- `docs/08_production/backend_architecture_options.md`

**Explicitly out of scope:** backend code, cloud sync implementation, final Confidant replacement content, non-Loamwake Strata content, full live-service economy authority, and any new content IDs.

---

## 2. Save Philosophy and Ownership Rules

| Rule | Contract |
|------|----------|
| Content definitions are immutable | Zone, Duty, Fixture, Confidant, Wanderer, Rootrail, and event rows are bundled content. They are referenced by ID from save state, not copied into player saves. |
| Player state is mutable | Account, wallet, progression, inventory, tutorial, event participation, and timer records are per-profile save data. |
| Local-first MVP | Prototype save is a single local plaintext JSON profile container with an atomic write path and a last-good snapshot. No backend is required for MVP. |
| Future cloud-ready | State families match `data_schema_v1.md` so the same payload can be serialized to PlayFab/Firebase later. Cloud authority is planned, not implemented here. |
| Deterministic recovery | Resume from authoritative saved state and recompute UI. Do not force a hard reset for additive missing fields or interrupted tutorial beats. |
| No second schema | This file defines container and write behavior only. Field names and state families remain those in `data_schema_v1.md`. |

---

## 3. Source ID and State Lock Table

| Family | Locked key or ID | Save usage |
|--------|------------------|------------|
| Account cap | `account.fixture_cap` | Starts `0`; tutorial owns `0->1`; `dty_lw_002_hollow_survey` owns `1->2`. |
| Tutorial flags | `account.tutorial_flags` | Write-through completion flags for mandatory and skippable tutorial steps. |
| Wallet | `wallet` | All currency/material balances, including `lucky_draws` and `rootrail_parts`. |
| Fixture loadout | `fixture_loadout.equipped` | Ordered equipped fixture instance IDs, length <= `account.fixture_cap`. |
| Fixture inventory | `fixture_loadout.fixture_inventory` | Canonical owned Fixture instances. This is the schema-backed "owned fixtures" list. |
| Duty progress | `duty_progress` | Per-Duty state, objective progress, reward claim idempotency. |
| Post state | `post_state` | Burrow Post availability, acceptance, completion, reward claim idempotency. |
| Confidant state | `confidant_state` | Greta/Mossvane unlock, trust, calling, and chain progress. |
| Rootrail state | `rootrail_state` | Station unlock, active repair timer, completed steps, routes, bonuses. |
| Rootrail codex | `rootrail_codex_entry` | Forgotten Manual discovery; `consumed` is always false. |
| Event state | `event_state` | Lucky Draw participation, claims, spends, ladder progress once eligible. |
| Lucky Draw event | `evt_luckydraw_001` | Hidden until Day 15+ and `zone_lw_001_rootvine_shelf` unlocked. |
| Greta intro | `post_lw_005_greta_intro` | Sole MVP unlock path for `cnf_001_placeholder_greta`. |
| Cap 1->2 Duty | `dty_lw_002_hollow_survey` | Sole owner of the second Fixture cap grant. |
| Rootrail reveal Duty | `dty_lw_003_greta_rail_lead` | Triggers `rootrail_state.rootrail_unlocked = true`. |

---

## 4. Profile Container Definition

The MVP local save may be stored as plaintext JSON. Suggested local filename: `profile_container.json` under the platform save directory.

```json
{
  "profile_container": {
    "profile_id": "uuid",
    "save_version": 1,
    "created_at": "iso_utc",
    "last_saved_at": "iso_utc",
    "last_login_at": "iso_utc",
    "current_session_id": "uuid",
    "content_revision_seen": "bundled_content_revision",
    "migration_required": false,
    "account_state": {},
    "save_integrity": {
      "last_good_saved_at": "iso_utc",
      "checksum": "implementation_defined",
      "integrity_status": "ok"
    },
    "runtime_resume": {
      "last_persisted_screen_hint": null,
      "dirty_state_keys": []
    }
  }
}
```

`account_state` contains the schema state families listed in this file. `runtime_resume.last_persisted_screen_hint` is a convenience hint only; correctness must come from persisted state and gate recomputation.

---

## 5. First-Run Profile Bootstrap Table

| bootstrap_step_id | Trigger | Created state | Default values | Persisted immediately | Notes |
|-------------------|---------|---------------|----------------|-----------------------|-------|
| `boot_001_profile_container` | No local profile exists | `profile_container` | `profile_id = uuid`; `save_version = 1`; timestamps set to current UTC; `migration_required = false`; `integrity_status = ok` | Yes | Must complete before tutorial begins. |
| `boot_002_account` | Profile container created | `account` | `fixture_cap = 0`; `tutorial_flags = {}`; `hat_passives = {}`; `rootrail_passives = {}`; `settings = defaults`; `clique_id = null`; `schema_version = 1` in `meta` | Yes | `created_at` is the source for derived account age. |
| `boot_003_wallet` | Account created | `wallet` | `mooncaps = 80`; `mushcaps = 12`; `glowcaps = 0`; `lucky_draws = 0`; `treasure_tickets = 0`; `echoes = 0`; `echo_shards = 0`; `strain_seeds = 0`; `treasure_shards = 0`; `polishes = 0`; `crack_coins = 0`; `bronze_shovels = 0`; `favor_marks = 0`; `strata_seals = 0`; `elder_books = 0`; `festival_marks = 0`; `rootrail_parts = 0`; `fixture_materials = {}` | Yes | Starter `mooncaps` and `mushcaps` are the MVP tutorial-safe defaults for first explore plus first craft. |
| `boot_004_fixture_state` | Wallet created | `fixture_loadout` | `equipped = []`; `fixture_inventory = []`; `last_updated_at = now` | Yes | No Fixture panel is visible while `fixture_cap = 0`. |
| `boot_005_burrow_state` | Account created | `burrow_state` | `burrow_level = 0`; `unlocked_rooms = []`; `active_burrowfolk_slots = 0`; `burrow_work_queue = []`; `last_collected_at = null` | Yes | Burrowfolk deployment can be introduced by tutorial without a separate deployment schema. |
| `boot_006_strata_progress` | Account created | `strata_progress.loamwake` | `clears = 0`; `current_zone_id = zone_lw_001_rootvine_shelf`; `zones_unlocked = [zone_lw_001_rootvine_shelf]`; `warden_clears.wdn_lw_001_mudgrip = 0`; `active_strata_trait_id = null`; `buried_clues_found = []`; `strata_trait_history = []` | Yes | Zone 1 is unlocked at profile start; Lucky Draw still remains hidden until Day 15+. |
| `boot_007_progress_maps` | Account created | `duty_progress`, `post_state`, `encounter_state` | Empty maps; records are created when surfaced, accepted, triggered, or mutated | Yes | Gate resolver may compute availability from definitions, but save should persist only mutable player progress. |
| `boot_008_social_state` | Account created | `confidant_state` | Empty map; Greta and Mossvane records are created on unlock/first mutation | Yes | Do not pre-unlock Greta from her broad definition condition. |
| `boot_009_rootrail_state` | Account created | `rootrail_state`, `rootrail_codex_entry` | `rootrail_unlocked = false`; `current_step_id = null`; timers null; `completed_steps = []`; `routes_unlocked = []`; `account_bonuses_granted = []`; `fman_001_terminal_routing.discovered = false`; `consumed = false` | Yes | Station UI is hidden until `dty_lw_003_greta_rail_lead` completion. |
| `boot_010_hat_state` | Account created | `hat_unlock`, `hat_display_state` | No unlocked hats; `visible_hat_id = null`; passive accumulator empty | Yes | Hat passive values are recalculated from unlocked hat rows on load. |
| `boot_011_event_state` | Account created | `event_state` map, `festival_ledger_state` map | Empty maps; no `evt_luckydraw_001` record and no active Festival Ledger season row | Yes | Event visibility is recomputed. Create event/ledger rows only when an eligible event instance starts. |
| `boot_012_initial_flush` | All bootstrap rows built | Last-good snapshot | Copy current profile to last-good snapshot and set checksum | Yes | If this write fails, retry bootstrap or show profile creation error before tutorial starts. |

---

## 6. First-Run Initialization Flow

1. Detect that no valid local `profile_container` exists.
2. Create `profile_id`, `save_version`, timestamps, and `content_revision_seen`.
3. Initialize schema-backed account state with `account.fixture_cap = 0`.
4. Initialize the starter wallet, empty Fixture loadout/inventory, Loamwake Zone 1 progress, hidden Rootrail state, empty social state, and empty event participation.
5. Persist the full profile immediately using atomic write, then update the last-good snapshot.
6. Start `current_session_id` and set `last_login_at`.
7. Enter `tut_01_burrow_intro`.

The tutorial must never start from an in-memory-only profile. A crash after the opening tap should resume from a valid saved profile, not create a second account.

---

## 7. Mutable State Ownership Table

| state_key | Owner doc | Persisted? | Write timing | Authoritative trigger | Notes |
|-----------|-----------|------------|--------------|-----------------------|-------|
| `account.account_id`, `profile_id` | `data_schema_v1.md` / this file | Yes | Bootstrap only | Profile creation | Stable for local to cloud migration. |
| `account.created_at`, derived `account.age_days` | `data_schema_v1.md`, `lucky_draw_week_mvp.md` | `created_at` yes; `age_days` no | Bootstrap; recompute on load | Profile creation, session resume | `age_days` is derived from UTC day boundary, not stored as authority. |
| `account.fixture_cap` | Tutorial + Loamwake Duty chain | Yes | Immediate | `tut_03_fixture_cap_01`; `dty_lw_002_hollow_survey` reward claim | New account starts 0. Tutorial grants 0->1. `dty_lw_002_hollow_survey` is sole 1->2 owner. |
| `account.tutorial_flags` | `tutorial_and_onboarding_flow.md` | Yes | Immediate for progression flags | Tutorial step completion | Non-skippable flags are write-through. Cosmetic hint flags may batch. |
| `wallet` | `data_schema_v1.md` / content rewards | Yes | Immediate for spend/grant | Explore, craft, Duty/Post reward, event claim/spend | Wallet and paired progression mutation must save atomically. |
| `fixture_loadout.fixture_inventory` | Fixture system / tutorial craft | Yes | Immediate | Craft result, duplicate conversion, reward grant | `tut_05_first_craft` creates a Fixture instance once. |
| `fixture_loadout.equipped` | Fixture system / tutorial equip | Yes | Immediate | Equip/unequip action | Validate length <= `account.fixture_cap` before save. |
| `duty_progress` | Loamwake and Confidant content | Yes | Immediate for state/reward changes | Accept, objective progress, completion, reward claim | `reward_claimed` prevents duplicate cap, trust, wallet, or Rootrail rewards. |
| `post_state` | Burrow Post content | Yes | Immediate for accept/complete/reward | Post surfaced, accepted, completed, claimed | `post_lw_005_greta_intro` completion unlocks Greta. |
| `confidant_state` | `first_confidant_chain.md` | Yes | Immediate | Greta post completion; Confidant Duty reward claim | Placeholder IDs remain stable until explicit migration table says otherwise. |
| `rootrail_state` | Loamwake Rootrail hook | Yes | Immediate | `dty_lw_003_greta_rail_lead`, repair start/claim | Unlock persists even if reveal modal is interrupted. |
| `rootrail_codex_entry` | Forgotten Manual discovery | Yes | Immediate | `clue_lw_002_terminal_routing_cache` reward claim | `consumed` stays false; manuals are permanent. |
| `event_state` | `lucky_draw_week_mvp.md` | Yes after event join | Immediate for join/spend/claim | Eligibility check, pull, ladder claim, stall purchase | Visibility recomputes; claims/spends persist. |
| `festival_ledger_state` | Lucky Draw Week / liveops | Yes after event join | Immediate for marks/pulls | Festival Mark grant/spend, stall purchase | MVP can keep this local; live service later validates server-side. |
| `non_persistent_ui_cache` | Runtime UI only | No | Never required for correctness | Active screen, modal stack, queued red dots | Recompute from persisted state on load. |

---

## 8. Transient vs Persisted State Table

| State or behavior | Persisted? | Reason |
|-------------------|------------|--------|
| Current open tab | No | Resume may use a hint, but UI can safely open Burrow or earliest required tutorial surface. |
| Modal stack | No | Interrupted modals are reconstructed from progression state if still needed. |
| Queued red dots | No | Red dots are recomputed from saved outputs/unlocks; tutorial window cap is max 2 until `tutorial_window_complete = true`. |
| Tooltip currently open | No | Tooltip visibility is not progression. |
| One-time tooltip dismissal | Optional batched | May persist under `account.settings` or `account.tutorial_flags` if it suppresses future noise; never gates progression. |
| Encounter preview card | No | Preview is generated from encounter state/content definitions. |
| Crafted Fixture ownership | Yes | Player inventory and economy correctness. |
| Equipped Fixture IDs | Yes | Core build state and stat calculation. |
| Duty/Post completion and reward claim | Yes | Prevents duplicate rewards and gates new content. |
| Greta unlocked | Yes | `confidant_state.cnf_001_placeholder_greta.unlocked`. |
| Rootrail unlocked and repair progress | Yes | System access, timers, completed steps, and rewards. |
| Lucky Draw visibility result | No | Recompute from `account.created_at`, current UTC day, Zone 1 unlock, and event schedule. |
| Lucky Draw claims, pulls, spends, ladder tier | Yes | Event economy and idempotency. |

---

## 9. Autosave and Write Timing Rules

**Immediate write-through required after:**
- Profile bootstrap.
- `account.fixture_cap` changes.
- Any non-skippable tutorial step completion.
- First explore reward packet grant.
- First craft and first equip.
- Wallet spend/grant.
- Duty/Post accept, completion, and reward claim.
- Confidant unlock, trust level, trust points, calling unlock, or completed Duty mutation.
- Rootrail unlock, repair start, repair claim, timer field mutation, route unlock, or codex entry grant.
- Event join, Lucky Draw pull, ladder claim, stall purchase, Glowcap conversion, or free floor claim.

**Batch/deferred save allowed for:**
- Tooltip dismissal that does not gate progression.
- Cosmetic UI preferences.
- Last opened tab hint.
- Non-critical "seen" copy flags.

MVP autosave should flush immediately for critical mutations and also run a soft periodic flush every 15-30 seconds while dirty. Quit/session-end should attempt a final dirty-state flush. The game must not rely on player-facing manual save.

Critical multi-field mutations should be saved atomically. Example: first craft spends `wallet.mooncaps`, spends `wallet.fixture_materials.tangled_root_twine`, creates a `FixtureInstance`, and flips `account.tutorial_flags.tut_05_complete`. A crash must leave either the pre-craft state or the complete post-craft state, not a partial spend without the Fixture.

---

## 10. Session Resume Rules

| Resume case | Detection | Required resume behavior |
|-------------|-----------|--------------------------|
| Quit during tutorial before cap grant | `fixture_cap = 0`; mandatory tutorial flag before `tut_03_fixture_cap_01` missing | Resume at earliest unmet mandatory tutorial step. Preserve any saved wallet/explore rewards. |
| Quit after cap 0->1 but before craft | `fixture_cap = 1`; no `fix_001_root_shovel_strap` instance; `tut_05_complete != true` | Open Fixture/Recipe flow and prompt first craft. Do not re-run cap grant. |
| Quit after first craft but before equip | `fix_001_root_shovel_strap` instance exists; `fixture_loadout.equipped = []`; `tut_06_complete != true` | Resume on equip prompt. Do not re-award materials, Mooncaps, or a duplicate Fixture. |
| Quit after first equip | Equipped list contains first Fixture; `tut_06_complete = true` or equivalent state detected | Continue to first Duty/Burrow Post teaching. Do not replay cap/craft/equip rewards. |
| Quit during Greta intro post | `post_lw_005_greta_intro.state = available` or `active`; not completed | Keep the post available/red-dotted according to UI rules. Resume post objective without forcing modal replay. |
| Quit after Greta post completion | `post_lw_005_greta_intro.state = completed`; `confidant_state.cnf_001_placeholder_greta.unlocked = true` | Do not replay the intro as a new post. Greta card and first Confidant Duty remain available. |
| Quit during Rootrail reveal | `dty_lw_003_greta_rail_lead.state = completed` or reward claimed; `rootrail_state.rootrail_unlocked = true` | Rootrail tab remains unlocked. Reveal hint/modal may queue once, but access is never removed. |
| Cross Day 15 while offline | `account.created_at` now yields `account.age_days >= 14`; Zone 1 unlocked; current event schedule includes `evt_luckydraw_001` | On session start, create/join event_state if needed and show active Lucky Draw icon. No migration step required. |

Resume uses the earliest unmet mandatory action, but it must be state-aware. Flags are helpful, not the only source of truth; if the inventory/equip state proves a step was already completed, the game must not duplicate the reward.

---

## 11. Tutorial-State Persistence Note

Tutorial flags live in `account.tutorial_flags` and are saved immediately for all progression-bearing steps:

| tutorial step | Persistence rule |
|---------------|------------------|
| `tut_01_burrow_intro` | Save flag immediately after completion. |
| `tut_02_first_explore` | Save wallet reward and flag atomically. |
| `tut_03_fixture_cap_01` | Save `account.fixture_cap = 1` immediately; never reapply if cap is already >= 1. |
| `tut_04_first_recipe` | Save recipe-view flag immediately if it gates the craft prompt. |
| `tut_05_first_craft` | Save wallet spend, Fixture instance creation, and flag atomically. |
| `tut_06_first_equip` | Save equipped list and flag immediately. |
| `tut_07_first_duty` and later skippable hints | Save only if needed to suppress repeated teaching. They must not be required for core progression recovery. |
| `tutorial_window_complete` | Set after the main tutorial window ends; until true, main-nav red dots are capped at 2. |

If `tut_05_first_craft` is missing but a matching first Fixture instance exists, resume at equip, not craft. If `tut_03_fixture_cap_01` is missing but `fixture_cap >= 1`, write a repair flag and continue; do not grant another cap.

---

## 12. Duty, Post, and Confidant Persistence Note

Duty/Post reward application must be idempotent.

| Content ID | Save contract |
|------------|---------------|
| `dty_lw_001_clear_rootvine` | Completion persists in `duty_progress`; rewards materials/Mooncaps only. It never grants Fixture cap. |
| `dty_lw_002_hollow_survey` | Reward claim is the sole MVP owner of cap `1->2`; save `reward_claimed = true` in the same atomic mutation as the cap change and wallet rewards. |
| `post_lw_005_greta_intro` | Completion persists in `post_state` and creates/updates `confidant_state.cnf_001_placeholder_greta.unlocked = true`. This is Greta's MVP unlock path. |
| `dty_cnf001_01_shelf_scratches` | Availability follows Greta unlock. Objective progress and reward claim persist in `duty_progress`. |
| `dty_cnf001_02_hollow_measure` | Completion advances Greta trust state and persists objective progress. |
| `dty_lw_003_greta_rail_lead` | Completion reveals Rootrail and advances Greta chain state; save `rootrail_state.rootrail_unlocked = true` atomically with Duty completion/reward claim. |
| Mossvane placeholder chain | Persist keyed to `cnf_002_placeholder_mossvane` and `dty_cnf002_*` IDs. Future replacement must use a migration table, not hard deletion. |

Confidant `trust_level`, `trust_points`, `active_duty_chain_ids`, `completed_duty_ids`, `calling_unlocked`, and `last_interaction_at` are mutable player state. The content definition remains static.

---

## 13. Event and Lucky Draw Gating Persistence Note

Lucky Draw visibility must not be stored as a permanent `visible = true` authority. Recompute eligibility on app open, session resume, and UTC day-boundary checks from:

1. `account.created_at` -> derived `account.age_days >= 14` (Day 15 active).
2. `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf`.
3. Current event schedule/content revision contains active `evt_luckydraw_001`.

Before eligibility, do not create an `event_state.evt_luckydraw_001` row merely to remember that the icon is hidden. On eligibility, create the event row with `event_type = lucky_draw_week`, `started_at`, `ends_at`, `ladder_tier_reached = 0`, `points_accumulated = 0`, `currencies_spent = {}`, and `rewards_claimed = []`.

Persist these event changes immediately:
- Free floor claim.
- Lucky Draw spend/pull.
- Ladder tier claim.
- Stall purchase.
- Glowcap conversion.
- Festival Mark earn/spend.

If the player crosses Day 15 while offline, session resume recomputes eligibility and creates the event row if the schedule is active. No stale client-side event icon flag is required.

---

## 14. Rootrail Persistence Rules

Rootrail reveal and repair are saved state, not UI state.

| Rootrail item | Save contract |
|---------------|---------------|
| Reveal | `dty_lw_003_greta_rail_lead` completion sets `rootrail_state.rootrail_unlocked = true`. |
| First modal/hint | Transient or optional batched UI flag. It may reappear once after an interrupted reveal. |
| Active step | `current_step_id`, `current_step_started_at`, and `current_step_timer_end_utc` persist. MVP may use local UTC timestamps; live service later uses server-issued UTC. |
| Completed steps | Append to `completed_steps` only after timer claim; use `reward_claimed` for idempotency. |
| Forgotten Manual | `rootrail_codex_entry.fman_001_terminal_routing.discovered = true`; `consumed = false` always. |
| Missing manual requirement | Recomputed from codex state and repair step definition. It does not need a separate saved lock flag. |

An interrupted reveal must never hide Rootrail again. If `rootrail_unlocked` is true, the Rootrail tab is available even if the introductory UI was not fully viewed.

---

## 15. Failure and Recovery Rules

| Failure case | Recovery behavior |
|--------------|-------------------|
| No profile exists | Run first-run bootstrap and persist before tutorial. |
| Profile file missing but last-good exists | Restore last-good snapshot, mark `integrity_status = restored_from_snapshot`, and continue. |
| Partial save with valid JSON | Fill missing additive fields with safe defaults, preserve valid progression/resources, set `integrity_status = repaired`, and write a clean snapshot. |
| Partial critical mutation | Prefer the last complete atomic state. If impossible, reconstruct from authoritative paired fields without duplicating rewards. |
| Corrupt JSON/checksum failure | Try last-good snapshot. If none exists, enter safe-reset confirmation flow as last resort. |
| Missing optional map | Default to `{}` and continue. |
| Missing `fixture_loadout` but account exists | Recreate empty loadout only if no Fixture ownership evidence exists. If ownership evidence exists elsewhere, quarantine and flag for manual QA. |
| Save version newer than runtime | Set `migration_required = true`; block mutation and ask for client update. Do not wipe. |

Atomic write requirement for MVP: write to a temporary file, verify parse/checksum, then replace the active profile file. Keep one last-good snapshot after each successful critical flush.

---

## 16. Migration and Versioning Rules

| Versioning item | MVP contract |
|-----------------|--------------|
| `save_version` | Top-level profile container version. Starts at `1`. |
| `schema_version` | State-family version stored in each document's `meta` where implementation supports it, matching `data_schema_v1.md` migration guidance. |
| `content_revision_seen` | Bundled content revision used when the profile was last loaded. Used to detect content ID additions/deprecations. |
| `migration_required` | Boolean set on load if runtime cannot safely mutate the save without migration. |
| Additive fields | Fill with safe defaults and write migrated save. Never wipe profile for additive missing fields. |
| Deprecated content IDs | Keep saved state. Mark deprecated in content or map replacement through an explicit migration table. |
| Placeholder Confidants | Keep `cnf_001_placeholder_greta` and `cnf_002_placeholder_mossvane` state until a replacement migration maps them. Do not hard-delete player trust progress. |
| Local to cloud | Upload the same state families. Cloud service later becomes authority for wallet, event, and timers, but field ownership remains stable. |

Safe default examples:
- Missing `event_state`: `{}`.
- Missing `rootrail_state.rootrail_unlocked`: `false`.
- Missing `rootrail_codex_entry.*.consumed`: `false`.
- Missing `account.tutorial_flags`: `{}` followed by state-aware repair.
- Missing `festival_ledger_state`: zeroed fields and empty claimed list.

---

## 17. Unresolved Items

- Exact long-term cloud merge policy is deferred. MVP has one local profile and no multi-device conflict resolution.
- Exact checksum/hash implementation is deferred to engineering.
- Starter wallet defaults (`mooncaps = 80`, `mushcaps = 12`) are MVP tutorial-safe values and should be retuned if the economy model later locks a different account-created grant.
- Event daily task definitions remain unresolved in `lucky_draw_week_mvp.md`; save behavior can persist the task state once that table exists.
- Battle stat resolution and encounter outcome math remain outside this save/profile contract.

---

## 18. Acceptance Checklist

- [ ] New profile starts with `account.fixture_cap = 0`.
- [ ] Tutorial `0->1` cap grant writes immediately and cannot duplicate on resume.
- [ ] `dty_lw_002_hollow_survey` is the sole owner of Fixture cap `1->2`.
- [ ] First craft and first equip are atomic, saved immediately, and cannot duplicate rewards after restart.
- [ ] `post_lw_005_greta_intro` completion persists Greta unlock.
- [ ] `dty_lw_003_greta_rail_lead` completion persists Rootrail unlock.
- [ ] Rootrail access is not lost if the reveal modal is interrupted.
- [ ] Lucky Draw is hidden before Day 15 eligibility and recomputed on session start/day boundary.
- [ ] Lucky Draw claims, spends, ladder state, and stall purchases persist idempotently.
- [ ] Transient UI hints, modal stack, active tab, and red-dot queue are not required for correctness.
- [ ] Additive missing fields have safe defaults.
- [ ] Partial save recovery preserves valid progression/resources where possible.
- [ ] Corruption only reaches hard reset after last-good restore fails.
- [ ] Local-first save format can be serialized to future cloud save without renaming schema families.

---

## 19. Next Phase 2B File

Proceed to: `docs/08_production/iap_catalog_v1.md`

That file should use `wallet.glowcaps`, `wallet.lucky_draws`, Lucky Draw safety rules, and the no-premium-only-power constraint already locked in the current Phase 2B docs.
