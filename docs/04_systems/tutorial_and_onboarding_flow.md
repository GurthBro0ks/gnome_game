# Tutorial and Onboarding Flow

**Status:** Canon — Phase 2B File  
**Version:** 1.0.0  
**Date:** 2026-04-20  
**Scope:** MVP first-session teaching path, early flow, and system reveal timings.

## 1. Status, Scope, Dependencies
This document defines Phase 2B onboarding. It outlines the MVP first-session teaching path, from account creation through the first 60 minutes and first daily return. It specifies required tutorial actions, ui surfaces, pacing limits, and Source ID locks. It relies on the locked core loop, UI unlock map, and Loamwake content sheets. No new major systems are introduced in this document.

**Source Documents:**
- `docs/04_systems/core_loop_mvp_spec.md`
- `docs/07_ui/unlock_flow_and_ui_map.md`
- `docs/06_content/loamwake_mvp_content_sheet.md`
- `docs/06_content/first_confidant_chain.md`
- `docs/04_systems/data_schema_v1.md`
- `docs/06_content/lucky_draw_week_mvp.md`

## 2. Canon Rules Applied
- **Fixture Cap Ownership:** The first Fixture cap increase (0→1) is owned entirely by this tutorial flow.
- **Loamwake Progression Ownership:** The second Fixture cap increase (1→2) is owned by Loamwake progression (`dty_lw_002_hollow_survey`).
- **No Forbidden Systems:** The tutorial does not surface Clique, War Armory, Deepening, Memory Shift, or Strata 2–5.

## 3. Source ID Lock Table
These IDs are locked and must be used exactly as authored:

| Type | Locked ID | Notes |
|------|-----------|-------|
| Zone | `zone_lw_001_rootvine_shelf` | First zone explored. |
| Duty | `dty_lw_001_clear_rootvine` | First story duty. |
| Duty | `dty_lw_002_hollow_survey` | Awards cap 1→2. |
| Duty | `dty_lw_003_greta_rail_lead` | Triggers Rootrail reveal. |
| Post | `post_lw_005_greta_intro` | Greta's intro post. |
| Confidant | `cnf_001_placeholder_greta` | Greta MVP confidant. |
| Fixture | `fix_001_root_shovel_strap` | First crafted fixture. |
| Recipe | `fix_rec_001` | First recipe revealed. |
| Rootrail Station | `rtr_station_lw_001_loamwake_terminal` | First station. |
| Rootrail Manual | `fman_001_terminal_routing` | First manual target. |
| Event | `evt_luckydraw_001` | Lucky Draw Week (deferred). |

## 4. NPE Pacing Rules
- **Anti-overload limits:** Maximum of 2 new surfaces per 10-minute block; target is 1.
- **Red-dot cap:** Maximum of 2 red dots visible during the main tutorial.
- **No event monetization:** No event UI or monetization pressure in the early tutorial flow.

## 5. Tutorial Step Definition Template

| `tutorial_step_id` | `time_window` | `trigger` | `required_source_state` | `player_action` | `ui_surface` | `copy_intent` | `system_unlocked` | `state_mutation` | `tutorial_flag_written` | `skip_allowed` | `red_dot_policy` | `breathe_after` | `mvp_status` |
|--------------------|---------------|-----------|-------------------------|-----------------|--------------|---------------|-------------------|------------------|-------------------------|----------------|------------------|-----------------|--------------|
| ID | Expected min | What fires it | State pre-requisite | What player clicks | Where | Teaching goal | New tab/icon | Schema field change | Flag flipped to true | true/false | Dot rules | true/false | Supported |

## 6. First 10 Minutes Beat Table

| `tutorial_step_id` | `trigger` | `player_action` | `ui_surface` | `copy_intent` | `system_unlocked` | `state_mutation` | `skip_allowed` |
|--------------------|-----------|-----------------|--------------|---------------|-------------------|------------------|----------------|
| `tut_01_burrow_intro` | Minute 0 | Tap first helper | Burrow Hub | Show hub and first Gnome | None | `account.tutorial_flags.tut_01_complete = true` | false |
| `tut_02_first_explore` | After intro | Spend 2 Mushcaps | Loamwake `zone_lw_001` | First explore cost | Loamwake zone map | `wallet.tangled_root_twine += 6`, `account.tutorial_flags.tut_02_complete = true` | false |
| `tut_03_fixture_cap_01` | Explore complete | Acknowledge cap | Fixture Panel | "You can equip 1 Fixture" | Fixture Panel | `account.fixture_cap = 1` | false |
| `tut_04_first_recipe` | Cap 1 granted | View Recipe | Recipe Browser | First recipe unlocked | Recipe Browser | `account.tutorial_flags.tut_04_complete = true` | false |
| `tut_05_first_craft` | Recipe viewed | Craft Fixture | Craft Screen | Craft the Root Shovel | None | `fix_001_root_shovel_strap` created | false |
| `tut_06_first_equip` | Craft complete | Equip Fixture | Fixture Panel | Counter becomes 1/1 | None | `account.fixture_loadout` updated | false |
| `tut_07_first_duty` | ~7 min | Check Duty | Burrow Post Board | Duties appear here | Burrow Post Board | `post_state` updated | true |

## 7. First 60 Minutes Onboarding Table

| `tutorial_step_id` | `trigger` | `copy_intent` | `system_unlocked` | `state_mutation` | `skip_allowed` |
|--------------------|-----------|---------------|-------------------|------------------|----------------|
| `tut_08_wanderer_trade` | `dty_lw_001` needs it | Handle first Wanderer trade | Encounter Card | `account.tutorial_flags.tut_08_complete = true` | true |
| `tut_09_greta_intro` | `dty_lw_001` complete | Greta's soft intro via Burrow Post | Confidant Tab | `cnf_001_placeholder_greta` unlocked | false |
| `tut_10_cap_02_grant` | `dty_lw_002` complete | Cap increases 1→2 | Fixture Slot 2 | `account.fixture_cap = 2` | false |
| `tut_11_rootrail_tease` | `dty_lw_003` complete | Frame as old structure | Rootrail Tab | `rootrail_state` updated | false |
| `tut_12_manual_hunt` | Rootrail tease over | Look for fman_001 | None | `account.tutorial_flags.tut_12_complete = true` | true |

## 8. Mandatory vs Skippable Matrix
- **Mandatory (Non-skippable):** Fixture cap unlock (0→1), first Fixture craft, first Fixture equip.
- **Skippable:** Burrowfolk output explanation, wallet tooltip, red-dot explanation, first Wanderer trade explanation, Rootrail station walkthrough.

## 9. Fixture Teaching Sequence
- **Cap Grant:** `account.fixture_cap` granted explicitly 0→1.
- **Recipe Reveal:** `fix_rec_001` explicitly revealed.
- **Crafting:** Player crafts `fix_001_root_shovel_strap`.
- **Equipping:** Player equips it. Counter updates 0/1 → 1/1.
- **No-Slot Wording:** Text focuses on capacity ("equip up to 1"), not specific slot types like "Head" or "Chest".

## 10. Loamwake Exploration Teaching
- **First Explore:** Player spends 2 Mushcaps on `zone_lw_001_rootvine_shelf`.
- **Guaranteed Craft Packet:** The first explore reward is strictly deterministic: guarantees `tangled_root_twine:6` to safely afford the first craft.
- **Wanderer Encounters:** First Wanderer (`wnd_lw_001_grumbling_trader`) is taught as the current interaction choice only.

## 11. Greta and Confidant Timing
- **Greta Intro:** `post_lw_005_greta_intro` appears softly after `dty_lw_001_clear_rootvine` is completed (target ~15–20 minutes). Uses an explicit Burrow Post red dot, not a forced modal.
- **Confidants Tab:** Opens only after Greta's post is accepted.

## 12. Rootrail Reveal Handling
- **Trigger:** Reveals after `dty_lw_003_greta_rail_lead` is completed.
- **Framing:** Player-facing framing is "Old track. Someone kept this running once. Bring parts; find the manual."
- **Scope:** Do not explain networks, future routes, Deepening, or long-term system mechanics. Show step 1 and the missing manual requirement.

## 13. Economy/Event Surfacing Rules
- **Resource Teaching Order:** Mushcaps (explore cost) → Mooncaps (common reward/craft cost) → Loamwake Fixture materials → Rootrail Parts → Glowcaps (late).
- **Lucky Draw Visibility:** Lucky Draw Week is hidden/deferred. No event UI, Festival Marks, tickets, or Glowcap conversion before Day 15+.
- **No Monetization Pressure:** No top-ups, Lucky Stalls, or limited-time language during the first 60 minutes.

## 14. Red-Dot and UI Teaching
- **What Can Dot:** New Burrowfolk output, Daily Duty refresh, new Burrow Post, Rootrail timer complete, new Manual, Hat unlock, new Recipe, Cap increase.
- **When It Clears:** Dots clear immediately upon viewing the respective tab/surface.
- **Queuing:** Max 2 red dots visible in the main nav during the tutorial window; additional dots queue silently until `tutorial_window_complete = true`.
- **Exclusions:** No wallet dots, passive accumulation dots, or stub UI dots.

## 15. Breathe Points
- **Breathe 1:** After the first Fixture equip. Let the player freely explore.
- **Breathe 2:** After the first Duty appears. Let the player read the board.
- **Breathe 3:** After the Greta post. Allow interaction with Confidant UI.
- **Breathe 4:** After the Rootrail reveal. Let the player digest the system.

## 16. Schema and State Mapping
- **Content Fields vs. Player State:** The step templates map `tutorial_flags`, `account.fixture_cap`, wallet balances, etc. Content IDs remain immutable definitions while player state is updated.
- **State Mutations:** Track changes precisely across `wallet`, `fixture_loadout`, `duty_progress`, `post_state`, and `tutorial_flags`.

## 17. Acceptance Checklist
- [ ] Fixture cap starts at 0.
- [ ] Fixture cap 0→1 is owned by tutorial and non-skippable.
- [ ] First craft and first equip are non-skippable.
- [ ] `dty_lw_002_hollow_survey` remains sole owner of the 1→2 cap increase.
- [ ] `post_lw_005_greta_intro` soft-introduces Greta after `dty_lw_001`.
- [ ] Rootrail reveals using `dty_lw_003_greta_rail_lead`.
- [ ] No Lucky Draw, Festival UI, or Glowcap conversion exists in the first 60 minutes.
- [ ] No mention of Clique, War Armory, Deepening, Memory Shift, or Strata 2-5 during tutorial steps.

## 18. Deferred/Unresolved Notes
- **Event Daily Tasks:** Event daily task type is unresolved. Event daily tasks are deferred, not visible before Day 15, and require a future event task table/liveops pass. Do not define in onboarding.
- **Burrowfolk IDs:** Use a placeholder helper (Mudger); actual first Burrowfolk ID should be mapped elsewhere if unauthored.
- **Combat Stats:** Combat/stat resolver not fully solved here.

## 19. Next Phase 2B File
Proceed to: `docs/08_production/save_state_and_profile_flow.md`
