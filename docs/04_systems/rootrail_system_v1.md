# Rootrail System v1

**Status:** Canon — Phase 2A Salvage  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** Rootrail restoration system — repair loop, timers, route unlocks, passive bonuses, Forgotten Manual codex, MVP visibility rules.

---

## Canon Lock

- **The Ascender is removed.** Rootrail replaces it entirely. Do not use "Ascender" in any implementation context.
- Rootrail is an **old forgotten train**.
- Rootrail functions as: restoration project, travel/unlock system, passive account bonus system, route hub.
- Repair steps use **Rootrail Parts**, **Forgotten Manuals**, and a **timer**.
- Timer duration **increases** with each installed part.
- Forgotten Manuals are **permanent knowledge unlocks**, separate from Elder Books, tracked in the **codex/logbook**.
- Forgotten Manuals are **not consumable**. Installing one on a repair step does not destroy it.

---

## Replacement Note: Ascender → Rootrail

| Old Term | Replacement |
|----------|------------|
| The Ascender | Rootrail |
| Ascender Parts | Rootrail Parts |
| Ascender repair step | Rootrail repair step |
| Ascender route | Rootrail route |
| Ascender passive bonus | Rootrail account bonus |

Purge all references to The Ascender from implementation code, schema keys, UI strings, and event names. Use `rootrail_*` naming convention for all technical identifiers.

---

## System Fantasy

The Rootrail is the bones of the old Gnome world's long-forgotten underground rail network. No one alive remembers where it went. The player's Burrow sits near a half-collapsed terminal. Restoring it is not just a side project — it's the slowly opening door to regions, materials, and knowledge that the current Strata cannot provide.

Each repair step is a commitment: it costs real materials, requires a Forgotten Manual (specific knowledge), and takes real time. The longer the train gets restored, the harder the next step becomes — but the rewards grow accordingly.

The Rootrail does not compete with Strata exploration. It is the long-horizon project running in the background while the player does everything else.

---

## System Jobs

Rootrail serves four interconnected roles:

| Job | Description |
|-----|-------------|
| **Restoration project** | Long-horizon background project with escalating timer costs and material gates |
| **Travel/unlock system** | Completed routes unlock new Strata sub-regions, Buried Clues locations, and material sources |
| **Passive account bonus system** | Each completed repair step grants a permanent account-level bonus (similar to hat passives but Rootrail-sourced) |
| **Route hub** | The visual rail-map hub shows completed, in-progress, and silhouetted future routes |

---

## Core Repair Loop

```
1. Player visits the Rootrail station at The Burrow
2. Player sees the current repair step (or the first available one)
3. Step shows: required Rootrail Parts, required Forgotten Manual ID, base timer
4. Player provides Rootrail Parts (if enough in inventory)
5. Player provides the Forgotten Manual (must already be in codex — not consumed)
6. Timer begins counting down
7. Player continues normal gameplay
8. Timer completes → player taps "Collect" → repair step reward granted
9. Next repair step becomes visible
10. Repeat
```

**Manual gate note:** The Forgotten Manual must be in the player's codex before the timer can start. If the player does not have the required manual, the repair step shows the manual as a missing prerequisite. The manual is found in the world (drops, Duties, rare encounters), then permanently added to the codex.

---

## Rootrail Parts

| Property | Value |
|----------|-------|
| Canonical name | **Rootrail Parts** |
| Resource type | Progression material |
| Faucet sources | Loamwake Duties, encounter drops, event reward tracks, exchange shops |
| Sink | Rootrail repair steps |
| Cadence | Slow accumulation — not a daily currency |
| Scarcity | Medium-high; deliberately scarce to pace restoration |
| Storage cap | None (no cap) |
| Server authority | Required for spend validation |

Rootrail Parts are not a currency. They are a slow-accumulating material whose primary (and essentially only) use is Rootrail repair. This makes them feel purposeful rather than generic.

---

## Forgotten Manuals

| Property | Value |
|----------|-------|
| Canonical name | **Forgotten Manuals** |
| Resource type | Permanent unlock (knowledge) |
| Separate from | Elder Books (separate item; different role) |
| Faucet sources | Buried Clue discoveries, rare Encounter drops, specific Duty completions, Rootrail route sub-discoveries |
| Consumed on use | **No** — permanently added to codex |
| Tracking | Codex/logbook screen; each manual gets a codex entry |
| Stacking | No — each manual is unique (one entry per unique manual ID) |

### Forgotten Manual ≠ Elder Books

| | Forgotten Manuals | Elder Books |
|---|---|---|
| Role | Knowledge gate for Rootrail repair steps | Ritual/key items for Deepening and other upgrade systems |
| Consumed | Never | Context-dependent |
| Tracked | Codex/logbook | Inventory (like a currency) |
| Numeric accumulation | No accumulation — discovered once per unique manual | Yes — can hold N Elder Books in wallet |
| Iconography | Old paper scrolls / bound notebooks | Glowing ritual tomes |

---

## Timer Growth Rules

Timer duration increases with each Rootrail repair step completed. This is intentional: the further the restoration goes, the harder the next repair becomes, and the longer the commitment.

**Timer growth model:**

| Step Range | Base Timer (approx) | Notes |
|------------|---------------------|-------|
| Steps 1–3 (intro segment) | 2–8 hours | Accessible; teaches the loop |
| Steps 4–7 (Loamwake segment) | 12–24 hours | Requires daily return |
| Steps 8–14 (mid-game segment) | 1–3 days | Meaningful long-term project |
| Steps 15–22 (late-game segment) | 3–7 days | Major milestone reward at each step |
| Steps 23+ (deep expansion) | 7–14 days | Post-launch content; forgotten depths / new worlds |

Timer can be reduced (not eliminated) by:
- Account bonuses from prior Rootrail completions
- Specific Duty completions
- Potential festival event bonuses (seasonal)

> **UNRESOLVED:** Whether a premium speed-up option is allowed for Rootrail timers. If allowed, it must follow the same rule as all other timer reductions: no premium-only power advantage (cosmetic/convenience only). Recommend: allow a Mushcap-based partial speed-up but cap it at 25% of remaining time to preserve the long-horizon feel.

---

## Repair Step Outputs

Each completed repair step grants **one or more** of the following:

| Output Type | Description |
|-------------|-------------|
| **New route unlock** | Adds a new travel/exploration route to the Rootrail hub map |
| **Rare material unlock** | Adds a new rare material source accessible via that route |
| **New Fixture recipe** | Unlocks a Fixture recipe that uses route-specific materials |
| **Account bonus** | Permanent small passive boost (resource yield, stat, etc.) |
| **Codex entry** | Lore/world entry added to the Rootrail codex |
| **Fixture cap increase** | (Specific steps only) +1 personal Fixture cap |
| **Next step reveal** | The subsequent repair step is revealed with its requirements |

A single step may grant multiple of these. Granting none is not allowed — every step has a meaningful reward.

---

## Route Unlocks

Routes are the primary output of Rootrail restoration. Each route:
- Connects The Burrow to a sub-region, resource node, or edge location
- May be the source of specific Forgotten Manuals (via Buried Clues on the route)
- May be the source of specific rare materials (via encounters/gathering on the route)
- Shows as a visible line on the Rootrail hub map once unlocked
- Shows as a silhouette/ghost line before unlock (to communicate that more routes exist)

**Route naming convention:** `route_<strata>_<index>` (e.g., `route_loamwake_01`, `route_ashcrag_02`)

**MVP visibility:** In Loamwake MVP, only the first Rootrail segment (restoration site) is interactable. One silhouette route may be shown as a tease. No full route network exposed.

---

## Rare Material Unlocks

Some Rootrail routes unlock access to materials that do not appear in the standard Strata explore loop:

| Source | Example Material |
|--------|-----------------|
| Rootrail route: Loamwake lower shelf | Bleached Rail Fragment |
| Rootrail route: Ashcrag border | Cinder-Cured Board |
| Rootrail route: Forgotten Depth 1 | Null Residue |

These materials are used in high-tier Fixture recipes and, later, in Deepening-adjacent crafting.

---

## Fixture Recipe Unlocks

Rootrail repair steps are one of the primary ways new Fixture recipes enter the game as the player progresses.

**Pattern:**
- Early Fixture recipes (3–5) are available before Rootrail (pre-Loamwake Duties)
- Mid-tier Fixture recipes are gated behind Rootrail Step 5+
- High-tier Fixture recipes require both Rootrail progress and specific Forgotten Manuals

This creates a natural reason to invest in Rootrail beyond passive bonuses alone.

---

## Passive Bonuses

Each Rootrail repair step grants a permanent account-level passive bonus. These are tracked separately from hat passives but accumulate similarly.

**Rootrail passive examples:**

| Step | Bonus |
|------|-------|
| Step 1 | +2% Rootrail Parts gain from all sources |
| Step 2 | +1% Mushcap gather rate |
| Step 3 | +1 Fixture capacity (scripted cap increase) |
| Step 4 | +3% Mooncap yield from Duties |
| Step 5 | Unlocks route_loamwake_02 + new Fixture recipe |
| Step 7 | +2% Crack Coin bonus rate |
| Step 10 | +1 Fixture capacity |
| Step 14 | +5% all resource yield (milestone) |

Passive bonuses stack and are never lost, including across Deepening.

---

## Codex / Logbook

The Rootrail Codex is a in-game logbook UI that tracks:
- All Forgotten Manuals discovered (with title, source hint, and knowledge domain)
- All routes unlocked (with map visual)
- All repair steps completed (with timestamp and output summary)
- Current repair step status and timer

The codex is a **separate UI surface** from the main Rootrail station. It is the player's record of what they know and what the train has restored.

**Codex accessibility:** Available to view at any time (read-only outside the station). Not tied to an active repair step. Serves as a source of curiosity and completionism pressure.

### Tracking Rules for Forgotten Manuals

- Each Forgotten Manual has a unique `manual_id`.
- When discovered, `codex_entries[manual_id].discovered = true` is set server-side.
- The full codex entry (title, knowledge summary, unlock role) is displayed.
- The manual requirement display on a repair step resolves to: green tick (in codex) or red lock icon (not yet found).

---

## Phased Deeper Expansion

Rootrail is designed to expand in phases beyond launch:

| Phase | Expansion |
|-------|-----------|
| **Launch (Loamwake MVP)** | First visible restoration site; first repair step; teaches the loop |
| **Phase 2B** | First 5–7 steps fully specced with costs, timers, manuals, outputs |
| **Live-service S1–S2** | Additional Rootrail routes into Strata 2–3; new Forgotten Manuals introduced |
| **Live-service S3+** | Forgotten Depth routes added; possibility of new worlds hinted |
| **Future expansion** | Full depth of the old rail network; major lore payoffs |

The system is designed to never "end" — there is always one more restoration segment to work toward.

---

## MVP Scope

### In Scope (Loamwake MVP)
- Rootrail station visible at The Burrow after an early Loamwake milestone
- First repair step fully implemented (Rootrail Parts cost + Forgotten Manual gate + timer)
- Timer countdown active while player plays
- Step completion reward: one account bonus + codex entry + next step reveal
- Codex screen shows discovered manual + completed steps
- One silhouette future route shown on hub map

### Stubbed (visible, not yet playable)
- Routes 2+ (silhouettes shown)
- Full repair step chain beyond step 2–3
- Rare materials from routes (source shown, not obtainable yet)

### Out of Scope (MVP)
- Forgotten Depths routing
- New worlds
- Full passive bonus accumulation curves
- Full Fixture recipe unlock chain via Rootrail

---

## Data Requirements

See `docs/04_systems/data_schema_v1.md` entities:
- `rootrail_state` — per-account restoration progress cursor, current step, active timer
- `rootrail_repair_step` — content definition (cost, outputs, gate conditions)
- `forgotten_manual` — content definition (knowledge type, unlock role)
- `rootrail_codex_entry` — per-account per-manual discovered flag and codex display data

Key constraints:
- Timer must be server-authoritative in any live-service build (not client-trusted)
- `forgotten_manual.permanent = true` always — no consumption path
- Repair step timer stored as `timer_end_utc` timestamp, not duration remaining (prevents client tampering)

---

## UI Requirements

- **Rootrail Station:** Primary interaction screen. Shows current step, costs, timer, Collect button. Accessible after Loamwake milestone unlock.
- **Rootrail Hub Map:** Visual route map showing completed routes, in-progress segment, silhouetted future routes.
- **Rootrail Codex / Logbook:** Separate screen (tab or dedicated). Shows all Forgotten Manuals, all completed steps, all routes.
- **Manual requirement indicator:** On repair step screen, each required Forgotten Manual shows as either "In Codex ✓" or "Not Yet Found 🔒" with a hint about where to look.

See `docs/07_ui/unlock_flow_and_ui_map.md` for UI reveal timing.

---

## Economy Integration

| Resource | Rootrail Role |
|----------|--------------|
| Rootrail Parts | Primary repair cost; earns via Duties + drops |
| Forgotten Manuals | Gate material (not consumed); earns via Buried Clues + Duties |
| Mooncaps | Supplementary costs on some steps |
| Fixture Materials (route-specific) | Rare materials unlocked by Rootrail routes feed back into Fixture crafting |

See `data/economy/economy_model_v1_sheet.csv` rows: `rootrail_parts`, `forgotten_manuals` for full faucet/sink/balance details.

---

## Unresolved Items

> **UNRESOLVED:** First Forgotten Manual — what is it called, what is its knowledge domain, where is it found in Loamwake? Needs `loamwake_mvp_content_sheet.md` input.

> **UNRESOLVED:** Exact Rootrail Parts cost for repair steps 1–5. Suggest: Step 1 = 20 parts, Step 2 = 35, Step 3 = 55, Step 4 = 80, Step 5 = 120. Needs playtest validation.

> **UNRESOLVED:** Whether repair steps can be queued (start next timer immediately on collect) or require manual initiation. Current recommendation: manual initiation only — preserves the intentionality of each repair commitment.

> **UNRESOLVED:** Premium speed-up policy (see Timer Growth Rules section).

> **UNRESOLVED:** Rootrail aesthetic — is the station a surface ruin with vines/mushrooms, or underground? Affects concept art direction and UI theming.
