# Lucky Draw Week MVP

**Status:** Canon — Phase 2B Draft
**Version:** 1.0.0
**Date:** 2026-04-20
**Scope:** First rotating Lucky Draw event in implementation-ready content form. Defines event definition, unlock timing, ladder tiers, pull table, Lucky Stall, free participation floor, Glowcap conversion rules, Festival Marks integration, economy safety rules, and schema mapping. MVP-safe only — no War Fixtures, no Strata 2–5, no Deepening content.

---

## 1. Purpose

This file turns the Lucky Draw Week event concept into buildable content. A developer and content designer can implement the event using only this file, `loamwake_mvp_content_sheet.md`, `data_schema_v1.md`, and the economy CSV without guessing.

**Explicitly OUT of scope for this pass:**
- War Armory / War Fixtures
- Strata 2–5 rewards
- Deepening / Memory Shift content
- Final Confidant lore
- Premium-only power Fixtures
- New Loamwake zones or material IDs
- New Hat IDs (hat reward rows deferred to hat content file)

---

## 2. Canon and Dependency Lock

| Dependency | File | Fields Used |
|------------|------|-------------|
| Wallet fields | `data_schema_v1.md §wallet` | `mooncaps`, `glowcaps`, `lucky_draws`, `festival_marks`, `rootrail_parts`, `treasure_tickets`, `polishes`, `strain_seeds` |
| Event state | `data_schema_v1.md §event_state` | `ladder_tier_reached`, `points_accumulated`, `currencies_spent`, `rewards_claimed` |
| Festival ledger state | `data_schema_v1.md §festival_ledger_state` | `festival_marks_earned`, `ledger_tiers_claimed`, `lucky_stall_pulls` |
| Loamwake materials | `loamwake_mvp_content_sheet.md §5` | All 8 canonical material IDs |
| Fixture IDs | `loamwake_mvp_content_sheet.md §13` | `fix_001` through `fix_005` |
| Event ID prefix | `content_table_templates.md §ID Conventions` | `evt_` for event ladder, `fstv_` for festival ledger |
| Economy intent | `economy_model_v1_sheet.csv` | `cur_003` (Glowcaps), `cur_004` (Lucky Draws), `cur_008` (Festival Marks) |
| Account age gate | `data_schema_v1.md §account` | `account.created_at` → `account.age_days >= 14` |

> **RULE:** No ID introduced in this file may contradict IDs locked in the above sources. Any reward packet must use only IDs already defined in those files.

---

## 3. Reward ID Readiness Table

| reward_family | allowed_ids | allowed_use | forbidden_use | notes |
|---------------|-------------|-------------|---------------|-------|
| Mooncaps | `mooncaps` | Pull table, ladder, stall, help floor | — | Primary common reward |
| Glowcaps | `glowcaps` | Small pull return only | Exclusive tier gate | Optional top-up; capped conversion |
| Lucky Draws | `lucky_draws` | Stall purchase, free floor grant | — | Canonical wallet key |
| Festival Marks | `festival_marks` | Pull table row, ladder tier, stall source/sink | Premium gate | Weekly cap enforced |
| Treasure Tickets | `treasure_tickets` | Rare pull row, high ladder tier | Common reward | Max 1 per row |
| Polishes | `polishes` | Common pull row, ladder | — | Upgrade acceleration |
| Rootrail Parts | `rootrail_parts` | Small capped amounts only | Station unlock, Manual grants, repair skips | Weekly event cap: 6 total |
| Strain Seeds | `strain_seeds` | Mid pull row | — | Schema-valid; safe |
| Loamwake materials | All 8 from `loamwake_mvp_content_sheet.md §5` | Common pull rows, ladder | — | No new IDs invented |
| Fixture (direct) | `fix_001_root_shovel_strap`, `fix_002_loamwake_canister`, `fix_004_burrowers_wrist_wrap` | Rare pull row only | fix_003/fix_005 as direct grants (use material-kit instead) | Must be craftable; duplicate converts to materials |
| Hats | none | Not in MVP pull table | Any new hat ID | Deferred to hat content file |
| War Fixtures | none | Forbidden | Any `wfx_` ID | Phase 3 only |

---

## 4. Event Definition

| Field | Value |
|-------|-------|
| `evt_id` | `evt_luckydraw_001` |
| `display_name` | Lucky Draw Week: Loose Lots |
| `event_type` | `lucky_draw_week` |
| `active_duration_hours` | 168 (7 days) |
| `currency_input_type` | `lucky_draws` |
| `unlock_condition` | `account.age_days >= 14 and strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf` |
| `cadence` | Weekly rotating (3-event rotation: Lucky Draw / Treasure / Echo) |
| `mvp_status` | `launch` |

---

## 5. Unlock and Timing Rules

| Rule | Value |
|------|-------|
| Account age gate | Active from Day 15 (condition: `account.age_days >= 14`) |
| First-session protection | Event UI does not appear in first 14 days; tab is hidden, not just locked |
| MVP cadence | One 7-day event active per week; Lucky Draw rotates weekly with Treasure Week and Echo Week |
| Event start | Server-authoritative; `event_state.started_at` and `event_state.ends_at` set on account join |
| Carry-over rule | No ladder or pull count carries between event instances; `event_state` resets each rotation |
| Strata gate | Loamwake Zone 1 must be unlocked (ensures player has seen core loop before event appears) |

---

## 6. Free Weekly Participation Floor

Minimum **3 Lucky Draws per week** without spending Glowcaps. Active free players can reach **5** via the optional daily task path.

| source_id | source_type | lucky_draws_granted | condition | weekly_limit | notes |
|-----------|-------------|--------------------:|-----------|:------------:|-------|
| `floor_lw_001_weekly_claim` | Weekly open claim | 1 | `event_state.evt_luckydraw_001.started_at != null` | 1 | Claimed from event tab on event open |
| `floor_lw_002_activity_milestone` | Event activity milestone | 1 | Complete any 3 Duties or Burrow Posts during event week | 1 | Does not require a specific Duty type |
| `stall_lucky_001_mooncap_draw` | Lucky Stall (Mooncaps only) | 1 | `wallet.mooncaps >= 250` | 1/week | Mooncap-only purchase — no Glowcap required |
| `floor_lw_003_daily_task_bonus` | Optional daily event task | 1 per 2 completions | Complete event daily task on 4 of 7 days | 2 | Optional path; brings active free players to 5/week |

> **GUARANTEE:** Every player who claims the weekly open reward, completes any 3 Duties, and buys the Mooncap stall item reaches the 3-draw floor with zero Glowcap spending.

---

## 7. Event Ladder Table

Currency input: `lucky_draws` (1 pull = 1 Lucky Draw spent = 1 point accumulated).

| tier | tier_threshold | tier_reward | reward_packet | safety_note |
|------|:--------------:|-------------|---------------|-------------|
| 1 | 1 | First Pull Bonus | `{mooncaps:80, polishes:2}` | Guaranteed T1 — free players hit this from floor alone |
| 2 | 3 | Material Bundle | `{fixture_material.loam_fiber:6, fixture_material.tangled_root_twine:5, mooncaps:60}` | Free players reliably reach T2 |
| 3 | 5 | Polish Cache | `{polishes:5, mooncaps:100}` | Active free players can reach T3 |
| 4 | 8 | Rootrail Support | `{rootrail_parts:3, festival_marks:20, mooncaps:80}` | Rootrail Parts capped at 3 for this tier |
| 5 | 12 | Strain Boost | `{strain_seeds:40, fixture_material.crumbled_ore_chunk:5, mooncaps:120}` | Glowcap converters reach T5 |
| 6 | 16 | Treasure Ticket | `{treasure_tickets:1, polishes:6, festival_marks:25}` | Max 1 Treasure Ticket at this tier |
| 7 | 20 | Fixture Kit | `{fixture_material.loam_fiber:8, fixture_material.dull_glow_shard:5, fixture_material.crumbled_ore_chunk:6, mooncaps:200}` | Material kit equivalent to fix_003 recipe inputs; no direct Fixture grant |

> **PACING NOTE:** Free players (3 draws/week) reliably reach Tier 2. Active free players (5 draws) can reach Tier 3. Moderate Glowcap converters (10 draws max via conversion + 5 free = 15 total) can reach Tier 7.

---

## 8. Lucky Draw Pull Table

Total weight = 100. One pull consumes 1 `lucky_draw`.

| pull_row_id | category | weight | reward_packet | quantity_range | weekly_cap | duplicate_rule | safety_note |
|-------------|----------|:------:|---------------|:--------------:|:----------:|----------------|-------------|
| `pull_lw_001_loam_bundle` | Loamwake materials | 20 | `{fixture_material.loam_fiber:4, fixture_material.tangled_root_twine:3}` | fixed | — | — | Common Zone 1 materials |
| `pull_lw_002_mid_materials` | Loamwake materials | 15 | `{fixture_material.crumbled_ore_chunk:3, fixture_material.mudglass_chip:2}` | fixed | — | — | Mid materials |
| `pull_lw_003_mooncaps` | Mooncaps | 20 | `{mooncaps:80–150}` | range | — | — | Roll 80–150 uniformly |
| `pull_lw_004_polish` | Polishes | 12 | `{polishes:3–5}` | range | — | — | Upgrade support |
| `pull_lw_005_festival_marks` | Festival Marks | 10 | `{festival_marks:10–20}` | range | 30 total/week | — | Weekly Festival Marks cap from pulls |
| `pull_lw_006_strain_seeds` | Strain Seeds | 5 | `{strain_seeds:20–35}` | range | — | — | Schema-valid |
| `pull_lw_007_rootrail_parts` | Rootrail Parts | 8 | `{rootrail_parts:1–2}` | range | 3 total/week from pulls | — | Small amounts; pacing-safe |
| `pull_lw_008_glowcap_return` | Glowcap return | 4 | `{glowcaps:5–8}` | range | — | — | Small return; not a faucet |
| `pull_lw_009_treasure_ticket` | Treasure Ticket | 3 | `{treasure_tickets:1}` | fixed | 1 total/week | — | Rare; hard-capped weekly |
| `pull_lw_010_fixture_grant` | Fixture (direct) | 3 | `{fixture_id: fix_001_root_shovel_strap OR fix_002_loamwake_canister OR fix_004_burrowers_wrist_wrap}` | 1 of 3 equal-weight | 1 total/week | Convert duplicate to `{mooncaps:80, fixture_material.loam_fiber:3}` | Only craftable Fixtures; no premium-only power |

> **WEIGHT CHECK:** 20+15+20+12+10+5+8+4+3+3 = **100** ✓

> **HAT RULE:** No Hat reward in this pull table. Hat content file not yet authored; hat_001_loamwake_dirt_cap is tied to Warden first-clear context and must not appear here.

> **FIXTURE SAFETY:** All three Fixture IDs in pull_lw_010 are craftable via fix_rec_001, fix_rec_002, fix_rec_004 respectively. A player can obtain any of these through normal crafting without spending Glowcaps.

---

## 9. Lucky Stall Inventory

The Lucky Stall is available during Lucky Draw Week. Inventory resets each event rotation.

| stall_id | display_name | cost | reward_packet | weekly_limit | purchase_type | unlock_condition | safety_note |
|----------|-------------|------|---------------|:------------:|---------------|-----------------|-------------|
| `stall_lucky_001_mooncap_draw` | One Lucky Draw (Mooncap) | `{mooncaps:250}` | `{lucky_draws:1}` | 1/week | mooncap_only | `event_state.evt_luckydraw_001 active` | **Mooncap-only path — no Glowcap required** |
| `stall_lucky_002_material_cache` | Loamwake Material Cache | `{lucky_draws:2}` | `{fixture_material.loam_fiber:8, fixture_material.tangled_root_twine:6, fixture_material.crumbled_ore_chunk:5}` | 2/week | lucky_draw | `event_state.evt_luckydraw_001 active` | Multi-material bundle; good value for active crafters |
| `stall_lucky_003_rootrail_pouch` | Small Rootrail Pouch | `{lucky_draws:1}` | `{rootrail_parts:3}` | 1/week | lucky_draw | `rootrail_state.rootrail_unlocked == true` | Rootrail-gated; weekly limit prevents pacing bypass |
| `stall_lucky_004_festival_exchange` | Festival Marks Exchange | `{festival_marks:30}` | `{lucky_draws:1}` | 1/week | festival_marks | `event_state.evt_luckydraw_001 active` | Festival Marks → Lucky Draws sink; reinforces dual economy |
| `stall_lucky_005_fixture_kit` | Fixture Material Kit | `{lucky_draws:3}` | `{fixture_material.dull_glow_shard:5, fixture_material.crumbled_ore_chunk:4, mooncaps:60}` | 1/week | lucky_draw | `account.fixture_cap >= 2` | Materials for fix_003/fix_005 recipes; no direct Fixture grant |
| `stall_lucky_006_polish_bundle` | Polish Bundle | `{lucky_draws:1}` | `{polishes:6}` | 2/week | lucky_draw | `event_state.evt_luckydraw_001 active` | Upgrade support; reasonable value |

---

## 10. Glowcap Conversion Rules

| Rule | Value |
|------|-------|
| Conversion rate | 10 Glowcaps = 1 Lucky Draw |
| Weekly conversion cap | 10 Lucky Draws maximum per event week via Glowcap conversion |
| UI placement | Dedicated "Top Up" button in event UI; separate from free floor display |
| Purpose | Optional acceleration only; Glowcaps must not gate any exclusive power reward |
| Combined ceiling | Free (5 max) + Glowcap conversion (10 max) = 15 pulls/week theoretical maximum for moderate spenders |

> **SAFETY:** Glowcap conversion is optional. All meaningful ladder tiers are reachable at 15 pulls. No exclusive power Fixture exists at any ladder tier or pull row.

---

## 11. Festival Marks Integration

### Mark Sources (during Lucky Draw Week)

| source | marks_granted | cap | notes |
|--------|:-------------:|:---:|-------|
| Pull participation (pull_lw_005_festival_marks) | 10–20 per pull hit | 30 total/week from pulls | Weighted pull row; not guaranteed each pull |
| Ladder Tier 4 reward | 20 (one-time per rotation) | 1 per event | Claimed with tier reward |
| Ladder Tier 6 reward | 25 (one-time per rotation) | 1 per event | Claimed with tier reward |
| Lucky Stall purchase (stall_lucky_004_festival_exchange) | Source, not sink here | — | This stall item *spends* Festival Marks |

**Total Festival Marks earnable from Lucky Draw Week (non-paying player):** approximately 50–75 marks/week (pull participation + two ladder tiers if reached).

### Mark Sinks (during Lucky Draw Week)

| sink | cost | reward |
|------|:----:|--------|
| `stall_lucky_004_festival_exchange` | 30 marks | 1 Lucky Draw |

### Festival Ledger Hook

Lucky Draw Week maps to a `festival_ledger_state` mini-entry. Lucky Draw pulls update `festival_ledger_state.lucky_stall_pulls`. Festival Marks earned update `festival_ledger_state.festival_marks_earned`.

> **PACING RULE:** Festival Marks from Lucky Draw Week alone should not fully complete the Festival Ledger. Marks from Lucky Draw Week are one of three rotating event sources. The full Festival Ledger requires engagement across multiple event types.

---

## 12. Fixture Reward Safety Rules

| Rule | Applied As |
|------|-----------|
| Only craftable Fixtures allowed | `pull_lw_010` uses only fix_001, fix_002, fix_004 — all have standard recipes |
| fix_003 and fix_005 are material-kit targets only | Appear as recipe inputs in stall_lucky_005; no direct grant |
| Duplicate Fixture conversion | Duplicate from pull_lw_010 converts to `{mooncaps:80, fixture_material.loam_fiber:3}` — NOT a cap expansion |
| No War Fixtures (`wfx_`) | Forbidden in all rows of this file |
| No exclusive event-only Fixtures | No Fixture ID introduced that is only obtainable through Lucky Draw spending |

---

## 13. Rootrail Reward Safety Rules

| Rule | Applied As |
|------|-----------|
| Rootrail Parts in pulls | Max 1–2 per pull_lw_007 hit; weekly pull cap of 3 total |
| Rootrail Parts in stall | stall_lucky_003 grants 3 Parts; weekly limit 1; gated behind `rootrail_state.rootrail_unlocked == true` |
| Maximum Rootrail Parts from event | ~6 total/week (3 from pulls + 3 from stall) — does not bypass rtr_step_001 cost of 20 Parts |
| Forbidden rewards | No Forgotten Manuals, no Elder Books, no station unlocks, no repair step skips |
| Ladder Tier 4 Rootrail Parts | 3 Parts (one-time per rotation); within safe pacing |

---

## 14. Template and Schema Mapping

### Content-definition fields (authored in this file)

| field | schema entity | notes |
|-------|--------------|-------|
| `evt_id` | `event` definition | Primary key |
| `event_type` | `event_state` | `lucky_draw_week` |
| `tier`, `tier_threshold`, `tier_reward` | Event Ladder Template | Per-tier rows |
| `currency_input_type` | `event_state.currencies_spent` | `lucky_draws` |
| `active_duration_hours` | `event_state.ends_at` computation | 168 hours |
| `pull_row_id`, `weight`, `reward_packet` | Local pull table | Resolved server-side |
| `stall_id`, `cost`, `weekly_limit` | Local stall table | `festival_ledger_state.lucky_stall_pulls` tracks stall use |
| `mvp_status` | All rows | `launch` |

### Player-state fields (backend-owned)

| field | schema entity | mutation trigger |
|-------|--------------|-----------------|
| `event_state.points_accumulated` | `event_state` | +1 per pull |
| `event_state.ladder_tier_reached` | `event_state` | Updated when threshold crossed |
| `event_state.currencies_spent` | `event_state` | Updated on each pull or stall purchase |
| `event_state.rewards_claimed` | `event_state` | Updated on ladder/pull reward claim |
| `wallet.lucky_draws` | `wallet` | Decremented on pull/stall purchase; incremented on floor grants |
| `wallet.mooncaps` | `wallet` | Decremented on stall_lucky_001; incremented via pull_lw_003 |
| `wallet.festival_marks` | `wallet` | Incremented via pull_lw_005 and ladder rewards; decremented via stall_lucky_004 |
| `wallet.rootrail_parts` | `wallet` | Incremented via pull_lw_007 and stall_lucky_003 and ladder T4 |
| `festival_ledger_state.festival_marks_earned` | `festival_ledger_state` | Updated on any Festival Mark grant |
| `festival_ledger_state.lucky_stall_pulls` | `festival_ledger_state` | Incremented per stall purchase |

---

## 15. Implementation Notes

1. **Pull resolution order:** Server rolls against weighted pull table; applies weekly caps in order (rootrail_parts cap → treasure_ticket cap → festival_marks cap → fixture_grant cap). If a capped row would fire, re-roll within remaining non-capped pool.
2. **Reward claim idempotency:** Ladder tier rewards use `event_state.rewards_claimed` list to prevent double-claim on reconnect.
3. **Duplicate Fixture conversion:** If pull_lw_010 rolls a Fixture the player already owns (instance exists in `fixture_loadout.fixture_inventory`), the server automatically converts to the duplicate reward packet without surfacing a choice prompt.
4. **Free floor grant timing:** `floor_lw_001_weekly_claim` is available immediately on event join. `floor_lw_002_activity_milestone` is evaluated in real-time against Duty/Post completion events during the event window.
5. **Stall weekly limits:** Weekly limit resets with each new event_state instance (new rotation), not on calendar week boundary.
6. **Glowcap conversion UI:** Conversion must display current week's Lucky Draws already earned from free sources before prompting conversion, to respect free floor communication.
7. **Age gate enforcement:** `event_state` record is not created until `account.age_days >= 14`. The event tab is hidden client-side and the event_state join endpoint rejects early accounts server-side.

---

## 16. Validation Checklist

- [x] `evt_luckydraw_001` defined with all required Event Ladder Template fields
- [x] Active from Day 15 (`account.age_days >= 14`) — explicit in unlock_condition
- [x] Free weekly floor is at least 3 Lucky Draws without Glowcap spending (3 confirmed: weekly claim + activity milestone + Mooncap stall)
- [x] At least one Lucky Stall item purchasable with Mooncaps only (`stall_lucky_001_mooncap_draw`, cost 250 Mooncaps)
- [x] Pull table weights sum to 100
- [x] No new Loamwake material IDs invented
- [x] No new zone IDs invented
- [x] No War Fixtures (`wfx_`) in any row
- [x] No premium-only power Fixture — all Fixture rewards in pull_lw_010 are craftable
- [x] fix_003 and fix_005 not granted directly — appear as material kit targets only
- [x] Hats not in pull table — deferred to hat content file
- [x] Rootrail Parts event total capped at ~6/week — does not bypass rtr_step_001 cost of 20
- [x] No Forgotten Manuals, Elder Books, station unlocks, or repair-step skips in any reward packet
- [x] Festival Marks have both source (pull_lw_005, ladder T4/T6) and sink (stall_lucky_004)
- [x] Glowcap conversion is capped at 10 Lucky Draws/week and is optional
- [x] Duplicate Fixture handling defined (convert to materials/mooncaps; no cap expansion)
- [x] All wallet field names match `data_schema_v1.md §wallet`
- [x] All material IDs validated against `loamwake_mvp_content_sheet.md §5`
- [x] All Fixture IDs validated against `loamwake_mvp_content_sheet.md §13`
- [x] Schema/state mapping distinguishes content-definition from player-state fields
- [x] Implementation notes cover pull resolution, idempotency, duplicate handling, and age gate

---

## 17. Unresolved Items

> **UNRESOLVED:** Exact Festival Ledger tier thresholds — how many Festival Marks does a full Festival Ledger tier cost? Flagged in economy CSV (`cur_008`). Lucky Draw Week mark yield (50–75/week) is calibrated against a placeholder assumption of ~150 marks per Ledger tier. Confirm during live balancing.

> **UNRESOLVED:** Pull table balance tuning — weights are MVP-safe starting points. The exact Mooncap range (80–150), Polish range (3–5), and Festival Mark range (10–20) should be A/B tested against target session length and spend rate in early access. Do not change reward families — only tune ranges.

> **UNRESOLVED:** Glowcap conversion weekly cap (10 Lucky Draws) is a conservative MVP value. May be adjusted upward if free player ladder completion rate is too high relative to moderate spenders. Confirm with economy telemetry key `event_lucky_draw_pull`.

> **UNRESOLVED:** Event daily task definition — `floor_lw_003_daily_task_bonus` references "event daily task" but the specific task type (e.g., resolve 2 encounters, explore 2 zones) is not defined here. Define in the tutorial/onboarding flow doc or a dedicated event task table.

> **UNRESOLVED:** stall_lucky_004 Festival Marks cost (30 marks) assumes the player has participated in at least one prior event week. If Lucky Draw Week fires before any other Festival mark source exists, the stall item may be unreachable on first occurrence. Consider reducing cost to 20 for first event instance only, or removing the gated stall item from the first-ever Lucky Draw event rotation. Flag for live ops review.

---

## 18. Acceptance Checklist

Done when all of the following are true:

- [ ] Developer can implement the pull table with only this file and `data_schema_v1.md`
- [ ] Content designer can add future pull rows by following the pull_row_id pattern and adjusting weights to sum to 100
- [ ] QA can verify: account age gate fires at day 15; free floor grants 3 draws minimum; Mooncap stall item costs 250 Mooncaps only
- [ ] Engineer can implement stall weekly limits using `festival_ledger_state.lucky_stall_pulls` + per-stall item purchase ledger
- [ ] No implementation step requires inventing an ID not in this file or its dependency files
- [ ] Economy reviewer can confirm no premium-only power path exists at any pull row or ladder tier
- [ ] Festival Marks integration connects to `festival_ledger_state` without additional schema changes

---

## 19. Next Recommended Phase 2B File

**Next file:** `docs/04_systems/tutorial_and_onboarding_flow.md`

Depends on:
- `loamwake_mvp_content_sheet.md` — for first zone, first Duty, first Fixture craft beat
- `core_loop_mvp_spec.md` — for 10-minute and 60-minute flow sequence
- `data_schema_v1.md` — for tutorial_flags field and fixture_cap mutation

Must define:
- Step-by-step tutorial beat sequence with trigger, UI action, system unlocked, skip_allowed flag
- Fixture cap 0→1 tutorial beat (skip_allowed: false)
- First Fixture craft and equip beat (skip_allowed: false)
- Anti-overload checkpoints
- Rootrail mention as "an old structure to investigate" only
