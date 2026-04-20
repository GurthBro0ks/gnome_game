# IAP Catalog v1

**Status:** Canon - Phase 2B File  
**Version:** 1.0.0  
**Date:** 2026-04-20  
**Scope:** MVP monetization catalog planning, offer visibility rules, Lucky Draw support rules, entitlement/restore behavior, and purchase safety boundaries.

---

## 1. Status, Scope, Dependencies

This file defines the MVP in-app purchase catalog as a bounded support layer for the existing Loamwake economy and Lucky Draw Week. It is content and planning guidance only. It does not implement receipt validation, backend catalog code, store SDK calls, tax handling, regional pricing, or cloud save.

**Source documents:**
- `docs/08_production/save_state_and_profile_flow.md`
- `docs/06_content/lucky_draw_week_mvp.md`
- `docs/04_systems/data_schema_v1.md`
- `docs/08_production/backend_architecture_options.md`
- `data/economy/economy_model_v1_sheet.csv`
- `docs/04_systems/tutorial_and_onboarding_flow.md`
- `docs/04_systems/core_loop_mvp_spec.md`

**Explicitly out of scope:** War Armory, War Fixtures, Strata 2-5, Deepening, Memory Shift, final Confidant content, backend purchase code, premium-only power Fixtures, stronger paid Hat passives, and first-session monetization prompts.

**Prototype note:** `backend_architecture_options.md` keeps the local prototype free of real-money transactions. This catalog is for the first monetization-ready MVP or closed-beta build after receipt validation, cloud save, and server-authoritative wallet fulfillment exist.

---

## 2. Monetization Safety Rules

| Rule | MVP contract |
|------|--------------|
| No pay-only progression walls | All Loamwake progression, Fixture cap growth, Greta unlock, Rootrail reveal, and Lucky Draw free floor remain reachable without spending. |
| No premium-only power Fixtures | IAP may not grant exclusive power Fixtures. Direct Fixture grants are excluded from this catalog. |
| No superior paid Hat passives | Hats are not sold in MVP IAP. Future paid cosmetic hats must use passives equal to or weaker than comparable earnable hats. |
| Tutorial is monetization-clean | No store entry, offer modal, top-up prompt, event icon, limited-time copy, or depletion prompt appears during the tutorial window or first 60 minutes. |
| Lucky Draw remains optional | Paid support may accelerate pulls, but cannot remove the weekly free floor, bypass the Mooncap path, or add exclusive power rewards. |
| Mooncaps stay meaningful | Mooncap-only Lucky Stall purchase remains authored and visible during event eligibility; IAP cannot replace it. |
| Glowcaps are bounded | Glowcap conversion into Lucky Draws follows the Lucky Draw Week cap: 10 paid top-up draws per event week. |
| No pressure on resource depletion | Running out of Mushcaps, Mooncaps, Rootrail Parts, or materials must not trigger a forced purchase prompt. |
| Refund-safe fulfillment | Purchase grants must be idempotent and tied to transaction IDs so restore/refund handling can be audited later. |

---

## 3. Catalog Philosophy

The MVP store sells optional convenience and acceleration, not exclusive gameplay.

- **Content definitions remain static.** Store SKUs reference already-authored wallet fields and event IDs. They do not define new systems.
- **Player value is visible but not mandatory.** Paid bundles can reduce grind for materials, Glowcaps, or event pulls, but free play keeps a viable path through the core loop and Lucky Draw floor.
- **Offers are quiet by default.** Store rows appear in an optional store surface after the player understands the loop. Starter offers are not modal pressure.
- **Purchases map to saved state atomically.** A verified purchase either fully grants its wallet/event mutation and records fulfillment, or grants nothing and remains pending.
- **Live-service authority is required before real money.** Receipt validation, server-authoritative wallet mutation, and entitlement restore must exist before these SKUs are sold.

---

## 4. Reward Family Readiness Table

| reward_family | Canonical state key | IAP use in MVP | Constraints |
|---------------|---------------------|----------------|-------------|
| Glowcaps | `wallet.glowcaps` | Main premium-adjacent grant in packs | Earnable without paying; event conversion capped. |
| Mooncaps | `wallet.mooncaps` | Small bundle support | Must remain primarily earned through Duties, Posts, and play. |
| Mushcaps | `wallet.mushcaps` | Not sold directly in MVP | Avoid monetizing exploration pressure during early play. |
| Lucky Draws | `wallet.lucky_draws` | Event support only | Any IAP-granted Lucky Draws count against the same paid top-up cap as Glowcap conversion. |
| Festival Marks | `wallet.festival_marks` | Small event support only | Cannot fully complete Festival Ledger tiers by itself. |
| Polishes | `wallet.polishes` | Utility/value packs | Earnable through Duties and events; no exclusive upgrade tier. |
| Rootrail Parts | `wallet.rootrail_parts` | Small one-time support after Rootrail unlock | No station unlock, no Forgotten Manual grant, no timer skip. |
| Loamwake materials | `wallet.fixture_materials[material_id]` | Utility/value packs | Use only authored Loamwake material IDs. |
| Fixtures | `fixture_loadout.fixture_inventory` | Not sold directly in MVP IAP | Avoid premium-only power and duplicate handling complexity. |
| Hats | `hat_unlock` | Not sold in MVP IAP | Future cosmetics must not carry superior passives. |
| Forgotten Manuals | `rootrail_codex_entry` | Never sold | Manuals remain discovery/codex rewards. |
| Fixture cap increases | `account.fixture_cap` | Never sold | Tutorial owns 0->1; `dty_lw_002_hollow_survey` owns 1->2. |

---

## 5. Catalog Structure Overview

| catalog_category | Purpose | Example SKU family | MVP status |
|------------------|---------|--------------------|------------|
| Starter bundle | One-time post-onboarding value bundle | `sku_once_001_burrow_starter_bundle` | Launch candidate |
| Glowcap packs | Repeatable premium-adjacent currency packs | `sku_glow_*` | Launch candidate |
| Utility/value pack | Repeatable capped material and polish support | `sku_utility_*` | Launch candidate |
| Lucky Draw support | Optional event acceleration within existing cap | `sku_lucky_*` | Launch candidate |
| Soft-currency shop | Non-IAP Mooncap/Lucky Stall exchanges | `stall_lucky_*` | Already authored in Lucky Draw Week |
| Paid cosmetics | Hats or social identity items | none | Deferred |
| Subscription | Monthly pass | none | Deferred |
| Hard progression unlocks | Cap increases, zones, Rootrail unlocks | none | Forbidden |

---

## 6. Pricing Tier Labels

These labels are planning handles. Final localized prices must be configured in the platform stores and backend catalog.

| price_tier_label | Reference USD tier | MVP use |
|------------------|-------------------:|---------|
| `T1_USD_0_99` | $0.99 | Small Glowcap pack, low-risk event support |
| `T2_USD_2_99` | $2.99 | Starter/event bundles |
| `T3_USD_4_99` | $4.99 | Medium Glowcap pack, Loamwake support |
| `T4_USD_9_99` | $9.99 | Large Glowcap pack |
| `T5_USD_19_99` | $19.99 | Reserved; not used at MVP launch unless economy review approves |
| `T6_USD_49_99` | $49.99 | Reserved; not used in MVP |

---

## 7. Catalog Visibility Rules

| Store surface or offer type | Visibility condition | Pressure rule |
|-----------------------------|----------------------|---------------|
| Store tab | `tutorial_window_complete = true` and `dty_lw_001_clear_rootvine.state == completed` and first 60-minute window is over | Optional nav only; no modal. |
| Starter bundle | Store tab visible and `account.age_days >= 1` | May appear as a store row; no forced popup on first daily return. |
| Glowcap packs | Store tab visible and `strata_progress.loamwake.clears >= 1` or first event eligibility reached | Store browsing only; no depletion prompt. |
| Utility/value pack | Store tab visible and `account.fixture_cap >= 2` | Do not show before the player has crafted/equipped and seen Duties. |
| Rootrail helper | `rootrail_state.rootrail_unlocked == true` | Cannot appear before `dty_lw_003_greta_rail_lead`. |
| Lucky Draw support | `evt_luckydraw_001` active and eligible: `account.age_days >= 14` and `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf` | Event store row only; not shown before Day 15. |
| Limited-time copy | Day 15+ event surfaces only | No limited-time urgency before event unlock. |

Starter offers can exist without violating first-session safety only because they are hidden during the tutorial window, hidden during the first 60 minutes, and not displayed as a forced modal.

---

## 8. One-Time Offers Table

| sku_id | display_name | price_tier_label | contents | unlock_condition | visibility_timing | purchase_limit | safety_note | contains_power_item | mvp_status |
|--------|--------------|------------------|----------|------------------|-------------------|----------------|-------------|---------------------|------------|
| `sku_once_001_burrow_starter_bundle` | Burrow Starter Bundle | `T2_USD_2_99` | `{glowcaps:240, mooncaps:600, polishes:6, fixture_materials:{tangled_root_twine:8, loam_fiber:8, crumbled_ore_chunk:4}}` | `tutorial_window_complete == true and dty_lw_001_clear_rootvine.state == completed and account.age_days >= 1` | Store row only after first daily return eligibility | 1/account | Helps early crafting without granting cap, Fixture, Hat, Rootrail unlock, or event access. | false | launch_candidate |
| `sku_once_002_loamwake_crafter_cache` | Loamwake Crafter Cache | `T3_USD_4_99` | `{glowcaps:360, mooncaps:900, polishes:10, fixture_materials:{crumbled_ore_chunk:8, dull_glow_shard:6, mudglass_chip:4}}` | `account.fixture_cap >= 2 and strata_progress.loamwake.zones_unlocked contains zone_lw_002_mudpipe_hollow` | Store row after cap 2 teaching is complete | 1/account | Materials are all earnable in Loamwake; no direct Fixture grant. | false | launch_candidate |
| `sku_once_003_rootrail_helper_pouch` | Rootrail Helper Pouch | `T3_USD_4_99` | `{glowcaps:300, rootrail_parts:6, mooncaps:500, fixture_materials:{rusted_rail_pin:2, bleached_rail_fragment:2}}` | `rootrail_state.rootrail_unlocked == true` | Store row in general store and Rootrail help link after reveal | 1/account | Six Rootrail Parts are not enough to complete `rtr_step_001`; no Forgotten Manual, station unlock, or timer skip. | false | launch_candidate |
| `sku_once_004_loose_lots_welcome_pack` | Loose Lots Welcome Pack | `T2_USD_2_99` | `{glowcaps:180, lucky_draws:3, mooncaps:250}` | `evt_luckydraw_001 active and account.age_days >= 14 and strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf` | Event store row only during Lucky Draw Week | 1/event instance | The 3 Lucky Draws count against the same 10-draw paid top-up cap used by Glowcap conversion. | false | launch_candidate |

---

## 9. Repeatable Packs Table

| sku_id | premium_price_tier_label | contents | cadence_reset_rule | purchase_limit | value_intent | safety_note | contains_power_item | mvp_status |
|--------|--------------------------|----------|--------------------|----------------|--------------|-------------|---------------------|------------|
| `sku_glow_001_small` | `T1_USD_0_99` | `{glowcaps:80}` | No calendar reset | Soft limit 10/day/account; backend may hard-limit suspicious velocity | Small top-up for event conversion or shop flexibility | Does not grant Lucky Draws directly; conversion cap still applies. | false | launch_candidate |
| `sku_glow_002_medium` | `T3_USD_4_99` | `{glowcaps:480}` | No calendar reset | Soft limit 6/day/account; backend may hard-limit suspicious velocity | Moderate spender baseline pack | Free Glowcap earn remains meaningful against this pack size. | false | launch_candidate |
| `sku_glow_003_large` | `T4_USD_9_99` | `{glowcaps:1050}` | No calendar reset | Soft limit 3/day/account; backend may hard-limit suspicious velocity | Larger value pack without MVP whale tier | No exclusive store-only sinks are added for this pack. | false | launch_candidate |
| `sku_utility_001_craft_cache_weekly` | `T2_USD_2_99` | `{mooncaps:500, polishes:6, fixture_materials:{tangled_root_twine:6, loam_fiber:6, crumbled_ore_chunk:4}}` | Weekly UTC reset | 1/week/account | Optional catch-up for crafting and upgrades | Hidden until cap 2; materials remain earnable through play. | false | launch_candidate |
| `sku_utility_002_polish_cache_weekly` | `T1_USD_0_99` | `{polishes:8, mooncaps:200}` | Weekly UTC reset | 1/week/account | Small upgrade support | No unique upgrade tier; no premium-only stat ceiling. | false | launch_candidate |

**Reserved repeatable tiers:** `T5_USD_19_99` and `T6_USD_49_99` remain disabled for MVP until economy telemetry proves that larger packs do not distort Lucky Draw or Rootrail pacing.

---

## 10. Lucky Draw-Linked Purchase Table

| sku_or_route_id | purchase_kind | contents_or_effect | unlock_condition | cadence_or_limit | Interaction with Lucky Draw rules | safety_note | contains_power_item | mvp_status |
|-----------------|---------------|--------------------|------------------|------------------|-----------------------------------|-------------|---------------------|------------|
| `sku_once_004_loose_lots_welcome_pack` | IAP one-time event bundle | `{glowcaps:180, lucky_draws:3, mooncaps:250}` | `evt_luckydraw_001 active` and Day 15+ eligibility | 1/event instance | `lucky_draws:3` counts toward the event's 10 paid top-up draw cap | Does not affect free floor and adds no exclusive reward row. | false | launch_candidate |
| `route_lucky_glowcap_conversion` | Non-IAP currency conversion using owned Glowcaps | `10 glowcaps -> 1 lucky_draw` | `evt_luckydraw_001 active` | Max 10 Lucky Draws/week via paid top-up bucket | Existing Lucky Draw Week rule; IAP Glowcaps can feed it but cannot exceed it | Conversion UI must show free floor progress first. | false | authored_in_lucky_draw |
| `stall_lucky_001_mooncap_draw` | Non-IAP soft-currency purchase | `{mooncaps:250} -> {lucky_draws:1}` | `evt_luckydraw_001 active` | 1/week | Preserves the free-player floor path | Must never be replaced by a Glowcap-only path. | false | authored_in_lucky_draw |
| `sku_lucky_002_festival_booth_pouch` | IAP event utility bundle | `{festival_marks:20, mooncaps:150, polishes:3}` | `evt_luckydraw_001 active` | 1/event instance | Does not grant Lucky Draws directly; can support Festival Marks exchange only when combined with earned marks | Not enough by itself to complete a Festival Ledger tier. | false | optional_candidate |

**Event ceiling rule:** Direct IAP Lucky Draw grants, Glowcap conversion into Lucky Draws, and any future paid event ticket grants share one weekly paid top-up bucket of 10 Lucky Draws per event instance. The free floor remains separate and must not be reduced because a player bought a pack.

---

## 11. Soft Currency and Non-IAP Distinction

Not every store-like exchange is IAP.

| Route | IAP? | State mutation | Notes |
|-------|------|----------------|-------|
| Mooncap Lucky Stall item | No | `wallet.mooncaps -= 250`; `wallet.lucky_draws += 1`; `event_state.currencies_spent.mooncaps += 250` | Authored free-player floor support. |
| Glowcap conversion | No direct real-money transaction | `wallet.glowcaps -= 10`; `wallet.lucky_draws += 1`; event paid top-up count increments | Uses owned Glowcaps, which may be earned or purchased. |
| Glowcap pack | Yes | Verified purchase grants `wallet.glowcaps` | Consumable; fulfillment ledger prevents duplicate grants. |
| One-time starter bundle | Yes | Verified purchase grants wallet contents and records one-time entitlement | Reinstall restore must not duplicate wallet grants. |
| Utility cache | Yes | Verified purchase grants wallet contents | Weekly limit enforced by purchase ledger and server clock. |

Mooncaps, Duties, Burrow Posts, and event free-floor routes remain the primary participation path. IAP may add flexibility but may not be required to enter or complete Loamwake MVP progression.

---

## 12. Restore and Entitlement Behavior

| Purchase class | Examples | Restore expectation | Duplicate-grant rule | Required saved metadata |
|----------------|----------|---------------------|----------------------|-------------------------|
| Consumable currency pack | `sku_glow_001_small`, `sku_glow_002_medium`, `sku_glow_003_large` | Platform restore does not regrant already fulfilled consumables; cloud save restores resulting wallet balance | Never grant twice for the same `transaction_id` | `iap_fulfillment_ledger.transaction_id`, `sku_id`, `fulfilled_at`, `grant_hash`, `platform` |
| One-time consumable bundle with limit | `sku_once_001_burrow_starter_bundle`, `sku_once_002_loamwake_crafter_cache` | Restore confirms purchased/fulfilled state; it does not duplicate wallet contents | If already fulfilled, mark entitlement restored and skip grant | `iap_entitlement_state.sku_id.purchased = true`, fulfillment ledger row |
| Event-limited bundle | `sku_once_004_loose_lots_welcome_pack`, `sku_lucky_002_festival_booth_pouch` | Restore checks same event instance and transaction status | If event ended, restore purchased state but do not create a new event grant outside the original event instance | `event_instance_id`, `sku_id`, `transaction_id`, `fulfilled_at` |
| Non-consumable cosmetic | None in MVP | Would restore entitlement on reinstall | No duplicate wallet grant because no wallet grant exists | Deferred |
| Subscription | None in MVP | Deferred | Deferred | Deferred |

**Restore on reinstall:**
1. Player installs and links to the same store/cloud identity.
2. Client asks backend/store wrapper for owned non-consumable and one-time-limited purchase records.
3. Backend reconciles receipt records against `iap_fulfillment_ledger`.
4. Already fulfilled wallet grants are not replayed.
5. Missing but valid fulfilled wallet state can be repaired only from authoritative cloud save or server ledger.
6. If restore service is unavailable, show store unavailable and do not start a purchase flow.

Local-only prototype builds must not sell real-money SKUs because a local save alone cannot safely restore consumable balances after reinstall.

---

## 13. Save/Profile Mapping

| Purchase behavior | Save/profile state family | Write timing | Notes |
|-------------------|---------------------------|--------------|-------|
| Glowcap pack fulfillment | `wallet.glowcaps` plus purchase fulfillment metadata | Immediate after verified receipt | Wallet grant and ledger row must be atomic. |
| Starter bundle purchased flag | `iap_entitlement_state` or `account.meta.iap_entitlements` until formal schema exists | Immediate after verified receipt | One-time flag prevents duplicate purchase or duplicate grant. |
| Weekly utility purchase count | `iap_fulfillment_ledger` or `account.meta.iap_weekly_limits` | Immediate after fulfillment | Reset by server UTC week, not client clock. |
| Lucky Draw direct bundle | `wallet.lucky_draws`, `wallet.glowcaps`, `event_state.currencies_spent`, event paid top-up counter | Immediate after verified receipt | Counts against 10 paid top-up draws/week. |
| Event support pack state | `event_state.meta.iap_support_grants` until schema formalization | Immediate after fulfillment | Bound to `event_instance_id`. |
| Store UI placement | Transient UI cache | Not required | Store tab, row order, and badge state recompute from eligibility and purchases. |
| Refund/reversal | Server purchase ledger; wallet correction policy deferred | Server-authoritative | MVP document flags need; backend implementation owns exact reversal flow. |

This file does not replace `data_schema_v1.md`. IAP entitlement and fulfillment records are a production addendum required before real-money launch. Until a formal `iap_entitlement_state` document is added, implementation may store minimal purchase metadata under `account.meta` and event-specific `event_state.meta`, following the schema rule that additive `meta` fields are allowed.

---

## 14. Onboarding and Age Pressure Guardrails

| Guardrail | Rule |
|-----------|------|
| Tutorial window | No store tab, SKU row, offer badge, top-up button, event icon, or limited-time copy before `tutorial_window_complete = true`. |
| First 60 minutes | No monetization prompt even if the player runs low on Mooncaps, Mushcaps, or materials. |
| Starter timing | Starter bundle is hidden until `account.age_days >= 1` and after `dty_lw_001_clear_rootvine` completion. |
| Event timing | Lucky Draw support is hidden until `account.age_days >= 14`, `strata_progress.loamwake.zones_unlocked contains zone_lw_001_rootvine_shelf`, and `evt_luckydraw_001` is active. |
| Depletion state | Resource depletion may show earnable paths first: Duty, Post, explore, Burrow work, Mooncap stall. |
| Store badge | Store badge is cosmetic and low priority; it must not consume one of the tutorial-window red-dot slots. |
| Push notifications | No purchase push notifications in MVP. |
| Countdown copy | Countdown copy is event-only and Day 15+; no "last chance" language during onboarding. |

---

## 15. Free vs Paid Glowcap Ratio

`economy_model_v1_sheet.csv` defines Glowcaps as premium-adjacent but earnable, with a target of roughly 10-30/day and 100-200/week once Glowcap faucets are active. The MVP catalog should preserve that relationship.

| Player path | Monthly Glowcap expectation | Notes |
|-------------|----------------------------:|-------|
| Free active player | 400-800 earned Glowcaps/month once event/achievement faucets are active | Uses economy sheet weekly target. Not all of this is available during first 14 days. |
| Moderate spender baseline | One `sku_glow_002_medium` purchase/month = 480 paid Glowcaps, plus earned Glowcaps | Free earn remains at least 20% of moderate-spender total. |
| Event-focused spender | One medium Glowcap pack plus `sku_once_004_loose_lots_welcome_pack` during a Lucky Draw event | Still constrained by 10 paid top-up Lucky Draws per event instance. |

If telemetry shows free Glowcap earn below 20% of moderate-spender total, reduce pack value, increase earnable Glowcaps, or reduce Glowcap sinks before adding larger paid tiers.

---

## 16. Forbidden and Not-Sold Categories

| Category | MVP catalog status | Reason |
|----------|--------------------|--------|
| Fixture cap increases | Forbidden | Violates progression ownership and cap milestone pacing. |
| Rootrail station unlock | Forbidden | Must come from `dty_lw_003_greta_rail_lead`. |
| Forgotten Manuals | Forbidden | Permanent codex discoveries, not purchases. |
| Rootrail timer skips | Forbidden for MVP | Would pressure long-horizon restoration pacing. |
| Direct Warden clears | Forbidden | Breaks Loamwake proof-of-fun loop. |
| Confidant trust or Calling unlocks | Forbidden | Story/social progress must be earned. |
| Premium-only Fixtures | Forbidden | Violates no premium-only power rule. |
| Paid Hats with stronger passives | Forbidden | Paid identity cannot overpower earnable hats. |
| War Armory, War Fixtures, Strata 2-5, Deepening, Memory Shift | Forbidden | Out of MVP scope. |
| Subscription | Deferred | Requires separate entitlement, cadence, and value-safety design. |

---

## 17. Failure and Recovery Rules

| Failure case | Required behavior |
|--------------|-------------------|
| Purchase initiated but receipt validation fails | Grant nothing; show failed/pending state; allow retry. |
| Receipt validation succeeds but wallet write fails | Keep purchase pending server-side; retry fulfillment until wallet grant and ledger row commit atomically. |
| Client crashes during fulfillment | On next session, reconcile pending `transaction_id` against server ledger and finish or confirm already fulfilled. |
| Restore called while offline | Show restore unavailable; do not infer entitlements from local-only data. |
| Reinstall without cloud identity | Non-consumable/one-time purchase records may be discoverable through platform restore, but consumable wallet balances require cloud save or server ledger. |
| Event ends before pending event purchase fulfills | Fulfill only if receipt time and event_instance_id are valid; otherwise refund/support policy needed. Do not grant into a new event instance silently. |
| Duplicate receipt replay | Reject as already fulfilled; do not add wallet currency again. |
| Refund or chargeback | Backend policy required; minimum behavior is to mark transaction reversed and block duplicate restore. Wallet correction rules are unresolved. |

---

## 18. Acceptance Checklist

- [ ] Store tab remains hidden during tutorial and first 60 minutes.
- [ ] Starter bundle is hidden until after the player understands the core loop and `account.age_days >= 1`.
- [ ] Lucky Draw support remains hidden before Day 15 eligibility and Zone 1 unlock.
- [ ] No SKU grants Fixture cap, Rootrail unlock, Forgotten Manuals, Warden clears, Confidant trust, War content, Deepening, or Memory Shift.
- [ ] No SKU grants a premium-only power Fixture.
- [ ] Hats are not sold in MVP IAP.
- [ ] Every product row has `contains_power_item: false`.
- [ ] Repeatable Glowcap packs use canonical `wallet.glowcaps`.
- [ ] Utility packs use only canonical wallet fields and Loamwake material IDs.
- [ ] Lucky Draw direct support counts against the existing 10 paid top-up draws/week cap.
- [ ] Mooncap-only Lucky Stall path remains intact and is not replaced by a paid path.
- [ ] Free Lucky Draw floor remains at least 3/week without spending Glowcaps.
- [ ] Free Glowcap earn remains meaningful against moderate paid spend.
- [ ] Consumable purchase fulfillment is idempotent by `transaction_id`.
- [ ] One-time bundle restore cannot duplicate wallet grants.
- [ ] Event-limited purchases are bound to `event_instance_id`.
- [ ] Local-only prototype builds do not sell real-money SKUs.
- [ ] Receipt validation and server-authoritative wallet grants are required before real-money launch.

---

## 19. Unresolved Items

- Final localized pricing and tax handling are store/business decisions.
- Exact Glowcap pack values should be retuned after economy telemetry; current values are planning targets.
- Formal `iap_entitlement_state` and `iap_fulfillment_ledger` schema documents are still needed before real-money launch.
- Refund and chargeback wallet correction policy is not authored.
- Paid cosmetic hat policy is deferred because the MVP catalog does not sell hats.
- Subscription/pass design is deferred.
- Age-rating, parental consent, loot-box disclosure, and regional compliance review are required before Lucky Draw-linked IAP ships.
- Server provider choice for receipt validation remains tied to the backend path decision: PlayFab, Firebase, Unity Gaming Services, or custom backend.

---

## 20. Next Recommended File

Proceed to: `docs/08_production/implementation_planning_pack.md`

Phase 2B content is now ready to roll into an implementation planning pack that sequences prototype build work, validates remaining production blockers, and connects the content docs to engineering milestones.
