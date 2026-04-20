# Implementation Planning Pack v1.0.0

**Document version:** 1.0.0  
**Last updated:** 2026-04-20  
**Status:** DRAFT (MVP Build Order)

## 1. Status, Scope, Dependencies

This file serves as the consolidated execution and sequencing document for the Gnome Survivor MVP. It distills the authored Phase 2A/2B systems, content, UI, persistence, and monetization specifications into a buildable dependency-aware implementation plan.

This is NOT a design document—it is a production roadmap for the engineering, content, and UI teams to execute the prototype (Phase 3) build sprints.

**Dependencies:**
- `docs/04_systems/data_schema_v1.md`
- `docs/04_systems/core_loop_mvp_spec.md`
- `docs/04_systems/tutorial_and_onboarding_flow.md`
- `docs/06_content/loamwake_mvp_content_sheet.md`
- `docs/06_content/first_confidant_chain.md`
- `docs/06_content/first_wanderer_pool.md`
- `docs/06_content/lucky_draw_week_mvp.md`
- `docs/07_ui/unlock_flow_and_ui_map.md`
- `docs/08_production/save_state_and_profile_flow.md`
- `docs/08_production/iap_catalog_v1.md`

## 2. Planning Principles

- **Testable Checkpoints:** We prioritize build order based on verifiable features. Foundational systems (runtime shell, persistence) come before content hooks, which come before monetization gating.
- **Strict MVP Scope:** Features explicitly marked out of scope (War Armory, Strata 2–5, Deepening, Memory Shift, etc.) are excluded from this build timeline entirely.
- **Dependency Flow:** Upstream dependencies must be functioning and verifiable before downstream features are integrated (e.g. tutorial state must work before IAP catalog visibility).
- **No Inventions:** This pack implements only what has been canonically authored.
- **Local-First Boot:** Sprint 0 must boot and save locally even if Firebase/Auth/backend is unavailable. Backend wiring must be adapter-based, not required for first boot. The prototype runs entirely offline with no server dependency.
- **First-Playable Scope:** This plan targets a first playable vertical slice, not a full launch build. Scope is narrow: Burrow core, Loamwake only, Greta as first Confidant, first Rootrail reveal/station/repair step, Lucky Draw only as the first implemented event, and local-first persistence. All 5 Strata, all 5 Confidants, Deepening, full Clique, and full Crack are explicitly post-MVP.

## 3. Authored Document Inventory

| Domain | Source Document | Key MVP Deliverables |
|--------|-----------------|----------------------|
| **Schema & State** | `data_schema_v1.md`, `save_state_and_profile_flow.md` | `account`, `wallet`, `burrow_state`, `event_state`, `tutorial_flags`, bootstrap logic |
| **Core Gameplay** | `core_loop_mvp_spec.md` | First 10/60 min loop, Fixture progression logic |
| **Onboarding** | `tutorial_and_onboarding_flow.md` | Non-skippable cap 0→1, first craft/equip |
| **Content: Zone** | `loamwake_mvp_content_sheet.md` | Zones 1-3, `wdn_lw_001_mudgrip`, clues |
| **Content: Economy** | `economy_model_v1_sheet.csv` | 19 canonical currency/material drops and sinks |
| **Content: Social** | `first_confidant_chain.md`, `first_wanderer_pool.md` | Greta & Mossvane, 6 Wanderers, 4 Runaways, 4 Wild Ones |
| **Events** | `lucky_draw_week_mvp.md` | `evt_luckydraw_001` ladder, pull rates, hidden-until-Day-15 rules |
| **Monetization** | `iap_catalog_v1.md` | Soft vs premium bundles, restore logic, no-power rules |
| **UI Flow** | `unlock_flow_and_ui_map.md` | Notification caps (max 2 red dots), unlock timings |

## 4. MVP Dependency Graph

- **Phase A (Foundation):** Schema `->` Save/Profile Bootstrap
- **Phase B (Onboarding):** Foundation `->` Tutorial Flow (owns cap 0→1) `->` Core Loop `->` UI Map
- **Phase C (Content):** Onboarding `->` Loamwake Content & Economy `->` Duties/Posts (owns cap 1→2 via `dty_lw_002`)
- **Phase D (Social/World):** Duties `->` Confidants (Greta unlock via `post_lw_005`) `->` Wanderers/Encounters
- **Phase E (Event & Store):** Loamwake Content `->` Rootrail Reveal (`dty_lw_003`) `->` Lucky Draw (Day 15+ gate) `->` IAP Catalog

## 5. Recommended Build Order

1. **Foundation Runtime & Persistence**
2. **Tutorial & Onboarding Wiring**
3. **Loamwake Exploration Loop (Zones, Drops, Crafting)**
4. **Duties, Posts & Confidants**
5. **Encounters & World Interactions**
6. **Rootrail Reveal Hook**
7. **Event Engine & Gating (Lucky Draw)**
8. **Monetization & IAP Surface**

## 6. Phase Implementation Table

| Phase | Milestone | Engineering Focus | Content/Design Focus |
|-------|-----------|-------------------|----------------------|
| **1** | Runtime & Persistence | Local save file generation, bootstrap default wallet/flags | Schema validation |
| **2** | Onboarding Wiring | Tutorial state machine, UI flow lock (0→1 cap, first craft/equip) | Narrative integration |
| **3** | Loamwake Loop | Zone 1 unlocks, basic drop parsing, Fixture crafting (`fix_001` - `fix_005`) | Material tuning |
| **4** | Duties & Confidants | Duty progression logic, `post_lw_005_greta_intro` UI, `dty_lw_002` (1→2 cap) | Post/Duty integration |
| **5** | Encounters | Wanderer trade/help/barter logic, Wild One combat logic | Encounter balancing |
| **6** | Rootrail | Trigger hook `dty_lw_003_greta_rail_lead` -> reveal UI | Visual reveal polish |
| **7** | Events | Gating logic (`age_days >= 14` + Zone 1), pull rates, ladder logic | Asset hookup |
| **8** | Monetization | IAP receipt validation stubs, hiding store during tutorial | Store UI mapping |

## 7. Validation / Proof Gates

| Gate ID | Condition to Pass (Checklist) | Verifies |
|---------|-------------------------------|----------|
| **VG-1** | First-run profile boots with correct defaults (0 cap, starter wallet) | Persistence |
| **VG-2** | Force-quitting during tutorial safely resumes the tutorial window | State Recovery |
| **VG-3** | First craft & equip does not grant duplicate items/stats | Economy |
| **VG-4** | Greta unlocks precisely after `post_lw_005_greta_intro` completes | Quest Logic |
| **VG-5** | `dty_lw_002_hollow_survey` grants Fixture cap 1→2 | Progression |
| **VG-6** | Rootrail UI surfaces immediately upon `dty_lw_003_greta_rail_lead` completion | Reveal Triggers |
| **VG-7** | Lucky Draw icon remains hidden on Day 14; appears Day 15 | LiveOps Gating |
| **VG-8** | IAP catalog is hidden during tutorial; no premium power items available | Monetization Safety |

## 8. Deferred / Out-of-Scope Scope

The following features MUST NOT be built during the MVP. They remain safely documented for later phases.

| Feature | Reason |
|---------|--------|
| **War Armory / War Fixtures** | Post-MVP. We only build personal Fixtures 1-5 for now. |
| **Strata 2–5 & Deepening** | Post-MVP scope. |
| **Memory Shift & Rift Mechanics** | Later lifecycle season features. |
| **Cloud Sync & Conflict Resolution** | Local-only profile for MVP. |
| **Daily Tasks for Events** | Placeholder, non-blocking for MVP. |
| **Paid Cosmetic Hats** | Deferred until character model asset pipeline is ready. |

## 9. Risks / Blockers

| Risk | Mitigation Strategy | Owner |
|------|---------------------|-------|
| Event schedule logic requires manual clock spoofing for QA | Build a debug UI to override `account.age_days` | Engineering |
| Starter wallet economy imbalance blocks early flow testing | QA to log bottlenecks; game designer to patch CSV | Design |
| Unresolved event/IAP legal disclosure requirements | Avoid real-money transactions in early test builds | PM/LiveOps |

## 10. Discipline Lanes

- **Engineering:** Execute phase milestones; provide debug hooks for time and state spoofing.
- **Design/Content:** Validate that implemented features match `loamwake_mvp_content_sheet.md` and related specs exactly.
- **UI/UX:** Adhere to anti-overload caps (max 2 red dots) per onboarding spec.
- **LiveOps/Monetization:** Do not introduce monetization layers until VG-1 through VG-6 are certified.

## 11. Acceptance Checklist

- [ ] All authored documents mapped cleanly to implementation phases.
- [ ] Dependency hierarchy prevents premature feature building.
- [ ] Hard proof gates defined for critical gameplay/progression milestones.
- [ ] Deferred features explicitly blocked from MVP sprints.

## 12. Next Recommended Build Target

With the planning pack complete, the team should begin **Phase 1: Foundation Runtime & Persistence**, followed immediately by **Phase 2: Tutorial & Onboarding Wiring**. 

The design/content phase (Phase 2B) is fully complete. Moving to **Phase 3: First Playable** sprints.
