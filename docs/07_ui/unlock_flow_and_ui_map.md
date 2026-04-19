# Unlock Flow and UI Map

**Status:** Canon — Phase 2A Salvage Rewrite  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** Loamwake MVP UI unlock order, module visibility rules, red-dot logic, and stub treatment.

---

## Status, Purpose, Scope

This document defines when each UI module and system surface becomes visible to the player, for the Loamwake MVP build. It is the single source of truth for unlock timing during implementation.

**Key canon rules for this doc:**
- Personal Fixture board starts at 0 (no slots visible until cap unlocks)
- Hat lane is separate from the Fixture screen
- War Armory is a later unlock; hidden for the full MVP window
- Rootrail is a mid-early unlock (around 50 min / day 1)
- No fixed gear slots in any UI

---

## Navigation Principles

1. **Drip, don't flood.** New players see only what they need right now. Each session opens slightly more.
2. **Every new surface gets a one-line intro.** No surface appears silently.
3. **Red dots are earned, not defaulted.** A red dot appears only when a player-initiated action has produced collectable output or when a content unlock is genuinely new and relevant.
4. **Stub surfaces are visible but clearly locked.** A lock icon + one-line "How to unlock" message. Never a blank screen.
5. **War Armory is invisible until Clique joins.** Not even a lock icon in the Fixture screen.

---

## Minute-0 Visible UI (On First Login)

The following modules are visible immediately on first account creation:

| Module | Surface | Notes |
|--------|---------|-------|
| The Burrow hub | Home screen | Main hub; Gnome Survivor visible |
| Loamwake button | Main nav | Shows as the first Strata; accessible |
| Wallet summary | Top HUD bar | Shows Mooncap and Mushcap counts only |
| Burrowfolk panel | Burrow tab | Shows "Deploy your first helper" prompt |
| Burrow Post board | Burrow tab | Shows as "notice board"; 0 posts until Burrow level 1 |
| Settings / Profile | Top-right icon | Always accessible |

**NOT visible at minute-0:**
- Fixture panel (cap is 0; no reason to show it yet)
- Hat Collection
- Rootrail station
- Strain Hall
- Vault of Treasures
- Lucky Draw / events
- Clique
- The Crack
- Deepening

---

## First 10-Minute Unlocks

Triggered by tutorial progression (scripted, not skippable):

| Unlock | Trigger | What appears |
|--------|---------|--------------|
| Fixture panel | Fixture cap increased to 1 (tutorial beat, ~2:30) | "Fixture" tab appears in nav; shows 0/1 equipped |
| Recipe browser | First Fixture recipe revealed (~3:00) | Within Fixture panel; shows craftable recipe |
| Craft + Equip tutorial | After crafting first Fixture (~4:30) | Overlay walkthrough; dismissed by completing equip |
| First Duty (Burrow Post) | 7 minutes in, after first Burrowfolk output | Post appears on Burrow Post board |

---

## First 60-Minute Unlocks

These surfaces open progressively through first-session play:

| Unlock | Trigger | What appears |
|--------|---------|--------------|
| Mudpipe Hollow (zone 2) | Completing zone_lw_001 Duty chain | Second zone tile visible on Loamwake map |
| Greta Confidant panel | Greta's intro Duty accepted (~20 min) | "Confidants" tab appears in Burrow; Greta card shown |
| Rootnip Burrowfolk unit | Burrow Post complete + Burrow level 1 (~35 min) | Second Burrowfolk card appears in deployment panel |
| Second Burrowfolk deployment slot | Burrow level 1 reached (~40 min) | Slot opens in Burrowfolk screen |
| Fixture cap 2 | Completing Mudpipe Hollow Duty chain (~45 min) | Cap counter changes to `*/2`; new slot visible in Fixture panel |
| Rootrail Station | Greta Duty 2 complete + zone_lw_002 border discovery (~55 min) | "Rootrail" tab appears in Burrow nav with intro overlay |
| Hat Collection tease | First Warden clear (The Mudgrip) | Hat unlock notification + Hat Collection button in profile area |

---

## Day-1 Unlock Order

(Assuming 1.5–3 hour first session + return visit)

| Order | Unlock | Source |
|-------|--------|--------|
| 1 | Daily Duty refresh visible | UTC midnight or first return |
| 2 | Strain Hall stub | After Loamwake zone 2 cleared |
| 3 | Vault of Treasures (stub, locked) | After first Warden clear |
| 4 | Forgotten Manual hunted | Player explores Rootvine Shelf looking for the Buried Clue |
| 5 | Rootrail step 1 timer starts | After finding fman_001 + having 20 Rootrail Parts |
| 6 | Second Confidant (Mossvane) stub | After first Warden clear |

---

## Day-2 / Day-3 Unlock Order

| Day | Unlock | Notes |
|-----|--------|-------|
| Day 2 | Lucky Draw icon becomes visible (stub) | Shown in nav as "Coming soon — Lucky Draw Week begins in X days" |
| Day 2 | Rootrail step 1 timer completes (if started day 1) | Step 1 reward: account bonus + codex entry + step 2 revealed |
| Day 2 | Step 2 of Rootrail visible (requirements shown) | Timer will be 8 hours base |
| Day 3 | Mossvane's first Duty chain becomes available | After day-2 daily Duties complete |
| Day 3 | Strain Hall becomes interactable (first Seed use) | After player has 30+ Strain Seeds |

---

## Loamwake-Clear Unlocks

These unlock **after the player clears Loamwake** (all launch zones explored, Warden cleared):

| Unlock | Notes |
|--------|-------|
| Deepening stub visible | Shows "Deepening — The Next Layer" teaser card in Burrow |
| Crack stub revealed | "Something is cracking under here." — entry point becomes visible |
| Lucky Draw Week start | If day 14+ since account creation, Lucky Draw Week activates |
| Rootrail silhouette route 2 shown | Second route appears as ghost on hub map |
| Fixture cap potential 3 post-clear | One Duty reward after Loamwake clear grants cap +1 |

---

## Fixture UI Rules

| Rule | Detail |
|------|--------|
| **Panel visibility** | Fixture panel is hidden until cap increases from 0 to 1 (tutorial) |
| **No slot labels** | Equipped list shows open circles, not typed slot names (no "Head", "Chest" etc.) |
| **Cap counter** | Always visible in Fixture panel header: `equipped / cap` (e.g., `2/3`) |
| **Equip action** | Tap a Fixture in inventory → "Equip" button → added to list; if at cap, must unequip one first |
| **Unequip action** | Tap equipped Fixture → "Unequip" (free, no cost) |
| **Recipe browser** | Tab within Fixture panel; shows all known recipes with lock state for unknown ones |
| **War Armory** | Not visible in Fixture panel until Clique War unlocks. No lock icon, no button. |

---

## Hat Display and Hat Collection UI

| Rule | Detail |
|------|--------|
| **Hat Collection screen** | Accessible from Profile area; not part of Fixture panel |
| **Display selector** | Shows all unlocked hats; one is selected as display hat; tap to change |
| **Passive summary** | Bottom of Hat Collection: shows all active permanent passives from unlocked hats, stacked total |
| **Locked hat tiles** | Shown as silhouettes with acquisition hint below |
| **Social display** | Display hat visible on Gnome Survivor sprite in Burrow, Clique roster, and Crack leaderboard (stub for MVP) |
| **First hat unlock flow** | Triggered by post: "Loamwake Dirt Cap — now yours." → taps into Hat Collection | 

---

## Rootrail UI Reveal and Route Hub Rules

| Rule | Detail |
|------|--------|
| **Rootrail tab** | Appears in Burrow nav after Greta Duty 2 discovery event; labelled "Rootrail" |
| **Station screen** | Shows: current repair step, Rootrail Parts count, Forgotten Manual status, timer (if running) |
| **Hub map** | Tab within Rootrail screen; shows The Burrow at center, one ghost/silhouette route visible from start |
| **Route unlock animation** | When a route unlocks, the ghost route lights up with a brief restoration effect |
| **Repair step UI** | Shows: step name, cost (parts + manual), current manual status (in codex / not found), timer (if active), Collect button (if complete) |
| **Manual requirement display** | Green tick + name = "In Codex"; Red lock + hint = "Not Found — look in [zone hint]" |
| **Codex / Logbook** | Separate tab in Rootrail screen; scrollable list of all discovered Forgotten Manuals and completed steps |

---

## Forgotten Manual Codex / Logbook UI

| Rule | Detail |
|------|--------|
| **Accessibility** | Always readable from Rootrail screen (Logbook tab) |
| **Entry format** | Manual card: title, codex category, discovered-in indicator, knowledge summary, and which steps it gates |
| **Discovery notification** | "New Manual Added to Codex" toast notification + codex tab badge |
| **Undiscovered entries** | Not shown in logbook — the codex only shows what the player has found |
| **Elder Books** | NOT in the Rootrail codex. Elder Books live in the wallet as a numeric resource. |

---

## Event UI Timing

| Event | Visibility | Unlock |
|-------|-----------|--------|
| Lucky Draw Week | Icon in nav; stub "Coming Soon" for first 14 days | Day 15+ or account age 14+ days |
| Treasure Week | Locked; shown after Lucky Draw Week completes | Week 3 onwards |
| Echo Week | Locked | Week 5 onwards |
| Festival Ledger | Visible in nav Week 1 as "Festival Board" but 0 tasks until Week 2 | Week 2 |
| Beginner Event | Active from Day 1; shown as "Your First Fortnight" banner | Account age 0–14 days |

---

## Clique / War Armory Visibility

| Stage | Visibility |
|-------|-----------|
| **Before Loamwake clear** | Clique screen not shown. No icon, no lock. |
| **After Loamwake clear** | Clique icon appears in nav, labelled "Clique — Find Your Burrowmates" |
| **After joining a Clique** | Clique screen becomes active with Clique queue, Clique Call, roster |
| **War Armory** | Visible within Clique screen only after `clique_state.war_armory_unlocked == true`. Shown as locked icon with "Unlock via Clique Queue" until then. |
| War Armory in personal Fixture panel | **Never visible.** War Fixtures do not appear in the personal Fixture UI under any circumstance. |

---

## The Crack Visibility

| Stage | Visibility |
|-------|-----------|
| **Pre-Loamwake clear** | Not visible. No hint. |
| **After Loamwake clear** | "Crack" icon appears in Burrow nav. One-line: "Something is cracking down here." Shows locked screen with "Uncover this mystery" prompt. |
| **After Crack unlock condition met** | Crack run screen becomes active. |

> **UNRESOLVED:** Exact Crack unlock condition (see data_schema_v1.md unresolved items).

---

## Red-Dot / Notification Rules

| Trigger | Red Dot On | Red Dot Clears |
|---------|-----------|----------------|
| Burrowfolk output ready to collect | Burrowfolk panel | On collection |
| Daily Duty refresh | Duties tab | When player views Duties tab |
| New Burrow Post available | Burrow Post board | When player views board |
| Rootrail timer completed | Rootrail tab | On collect tap |
| New Forgotten Manual discovered | Codex tab (within Rootrail) | On viewing Codex tab |
| Hat unlock | Hat Collection (profile) | On viewing Hat Collection |
| New Fixture recipe unlocked | Recipe browser (within Fixture panel) | On viewing recipe |
| Fixture cap increased | Fixture panel | On viewing Fixture panel |

**Anti-overload rules:**
- Maximum **3 red dots** visible in main nav at any time. If more than 3 events trigger simultaneously, prioritise: (1) Burrowfolk output, (2) Rootrail timer, (3) Daily Duties. Others are silently queued until their respective dots clear.
- No red dot for: wallet balance changes, passive accumulation, stub system notifications.
- Red dots never appear on Clique, Crack, or War Armory tabs until those systems are fully unlocked for the player.

---

## Hidden / Stubbed System Treatment

| System | Treatment |
|--------|-----------|
| War Armory | Completely invisible until Clique War unlocks. No lock icon. No mention. |
| Deepening | After Loamwake clear: "Deepening" card visible in Burrow with "Reach the next Stratum to begin." Not interactive. |
| Memory Shift | No visibility until post-first-Deepening (far future). |
| Great Dispute | No visibility at launch. |
| Forgotten Depths / new worlds | No visibility at launch. Rootrail hub shows only silhouettes for routes within Strata 1–2. |

---

## MVP Screen List

Minimum screens required for Loamwake MVP build:

| Screen | Notes |
|--------|-------|
| Burrow Hub | Main home screen |
| Loamwake Map | Zone tile grid |
| Zone Explore | Resource drop results, encounter events |
| Encounter Card | Wanderer / Runaway / Wild One interaction |
| Warden Fight | Battle stat check screen |
| Fixture Panel | Equipped list + recipe browser |
| Craft Screen | Recipe detail + confirm craft |
| Hat Collection | Unlock list + display selector |
| Rootrail Station | Repair step + timer |
| Rootrail Hub Map | Route map |
| Rootrail Codex | Manual logbook |
| Burrowfolk Panel | Deployment slots + output |
| Confidant Panel | Bond card + active Duty |
| Duties Board | Daily + story Duties |
| Burrow Post Board | Notice board |
| Wallet / Inventory | All currencies + Fixture Materials |
| Profile / Settings | Display hat selection + account info |

---

## Acceptance Checklist

- [ ] Fixture panel is completely hidden at account creation (cap = 0)
- [ ] Fixture panel appears only after the tutorial cap increase to 1
- [ ] Hat Collection is NOT inside the Fixture panel; accessible from profile area
- [ ] Rootrail tab appears in Burrow nav only after Greta Duty 2 trigger
- [ ] No "Ascender" string exists in any UI label, button, or toast
- [ ] War Armory is not visible in the Fixture panel under any account state
- [ ] Red dot count in main nav never exceeds 3 simultaneously
- [ ] Lucky Draw icon shows "Coming Soon" state for first 14 days
- [ ] Clique icon does not appear until Loamwake is cleared
- [ ] The Crack does not appear until Loamwake is cleared
- [ ] Deepening card appears (non-interactive) after first Loamwake clear
- [ ] Forgotten Manual codex does not show Elder Books; Elder Books are in wallet only
