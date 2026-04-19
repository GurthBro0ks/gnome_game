# Fixtures and Hats System v1

**Status:** Canon — Phase 2A Salvage  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** Personal Fixtures, Hats, War Armory boundary. Implementation and content-design reference.

---

## Canon Lock

- Clothes are removed. Every equipable piece is a **Fixture**.
- No rigid slots. Duplicate-style builds allowed.
- No negative stats. Tradeoffs are contextual.
- Personal Fixture cap: starts at **0**, grows to **12**.
- Hats are a special Fixture type. Hats are **NOT** counted against the 12-Fixture cap.
- War Fixtures live in a separate **War Armory**, also NOT counted against the 12-Fixture cap.

Do not use the words Clothes, Cloth Assembly, or Fit in any implementation context. Use Fixture.

---

## Design Goals

1. **Freedom over slots** — Players compose their own set from a pool. The game never forces a "head/chest/legs" pattern. The player asks: *What do I want to emphasize right now?*
2. **Growth unlocks capacity** — The sense of expanding power comes from earning more Fixture slots, not from upgrading within a fixed grid.
3. **No punishing swaps** — Removing a Fixture to try another one never incurs a penalty. Equipping costs only Fixture Materials (crafting), not a swap tax.
4. **Social legibility via Hats** — Hats give players identity and status expression without affecting gameplay power balance.
5. **War separation** — Club War has its own armory. War Fixtures never pollute the personal Fixture screen.

---

## Terminology

| Term | Definition |
|------|-----------|
| **Fixture** | Any equippable piece that occupies one slot of the personal Fixture cap |
| **Fixture Cap** | Maximum number of Fixtures a player may have equipped at once; starts at 0 |
| **Fixture Slot** | One position in the player's active Fixture list; not a rigid slot type |
| **Hat** | A special wearable item that is displayed on the Gnome Survivor; not part of the Fixture cap |
| **War Fixture** | A Fixture stored and used only in the War Armory; not part of the personal Fixture cap |
| **Fixture Category** | The design-facing classification of a Fixture (Farming / Pure Attack / Strain / War) |
| **Contextual Bonus** | An additional effect on a Fixture that activates only under specific gameplay or stratum conditions |
| **Permanent Passive** | A bonus from a Hat unlock that is always active as long as the hat is unlocked; does not require equipping |

---

## Personal Fixtures — Overview

Personal Fixtures are the primary gear layer for solo Gnome Survivor progression. They are active during Strata exploration, Duties, Burrow work, Crack runs, and encounters.

War Fixtures are separate (see War Armory Boundary section).

### Fixture Capacity Rules

| State | Cap |
|-------|-----|
| New account / pre-tutorial | 0 |
| Tutorial reward (Loamwake intro) | 1 |
| After first Loamwake clear | 2 |
| Mid-Loamwake progression | 3–4 |
| Full Loamwake complete | 4–5 |
| Post-Loamwake, mid-Strata (Strata 2–3) | 6–9 |
| Late game (Strata 4–5) | 10–12 |
| Hard cap | **12** |

Capacity increases come from:
- Loamwake milestone beats (scripted)
- Specific Duty completions
- Specific Rootrail repair unlocks
- Deepening rewards (post-Deepening path)

> **UNRESOLVED:** Exact per-milestone capacity increase schedule needs balancing. Target range above is directionally correct but should be validated against playtime curve.

### No-Slot Rules

- A player's equipped Fixtures are a **list** (ordered by player), not a grid of typed slots.
- There is no "weapon slot," "ring slot," or "armour slot."
- Any Fixture from any category can be placed in any position in the list.
- The only constraint is: the number of equipped Fixtures ≤ personal Fixture cap.

**Implementation note:** Backend must store `equipped_fixtures` as an ordered array of fixture IDs, bounded by `fixture_cap`. Not as a keyed slot object.

### Duplicate-Style Build Rules

- A player **may** equip two Fixtures with the same primary effect type (e.g., two Farming-category Fixtures).
- Stat contributions from duplicates stack additively (not multiplicatively) unless a specific Fixture defines its own stacking rule.
- There is no "unique item" restriction at this layer.
- Content designers may flag individual Fixtures as `duplicate_diminish: true` to apply a reduced secondary stack if balance requires it. This is opt-in per Fixture, not the default.

### Effect Rules

- Every Fixture has a **primary stat contribution** (always active while equipped).
- Fixtures may have a **contextual bonus**: an additional effect that activates only when a condition is met.
- Contextual conditions include: active Strata, active Strain, encounter type, Burrow work state, Clique status, or time of week.
- Contextual bonuses are **always positive or neutral**. They are not penalties.

**Examples:**
- *Root-Bitten Shovel Strap* — Farming. `+8% Mushcap gather rate.` Context bonus: `+4% Crack Coin bonus while in an active Crack run.`
- *Loamwake Canister* — Pure Attack. `+12% Battle Stat: Force.` Context bonus: `+6% Force vs Wardens in Loamwake.`
- *Pale Glow Lens* — Strain. `+10 Strain Seed capacity.` Context bonus: `+5% Strain Seed yield when active Strain is Pale Glow variant.`

### No Negative Stats Rule

All Fixtures have positive primary contributions. No Fixture has a negative stat as a drawback. If a design wants a tradeoff, it achieves it through:
- What the Fixture does *not* provide (opportunity cost of the equipped slot)
- Contextual bonuses being narrow (strong in one situation, neutral in others)
- Crafting material cost

**Forbidden:** "This Fixture gives +20% Force but -15% Mushcap gather." Do not write this.

---

## Fixture Categories

| Category | Role | Example Stat Focus |
|----------|------|--------------------|
| **Farming** | Gathering, resource yield, Mushcap-linked efficiency | Mushcap gather rate, Duty speed, Crack Coin bonus |
| **Pure Attack** | Combat, Warden fights, Battle Stats | Force, Grit, Clash score, Encounter win rate |
| **Strain** | Strain Seed capacity/yield, Strain Loom efficiency | Seed capacity, Strain unlock rate, Loom speed |
| **War** | Used only in War Armory; not in personal Fixture cap | See War Fixtures section |

Categories are design-facing tags, not mechanical constraints. A player may equip Fixtures from any mix of categories.

---

## Fixture Acquisition

Fixtures are obtained through crafting primarily, with drops as a secondary channel.

| Acquisition Type | Description |
|-----------------|-------------|
| **Crafting (primary)** | Use a Fixture recipe (unlocked via progression) + Fixture Materials. All players can access craftable Fixtures. |
| **Encounter drops** | Rare Fixtures drop from Warden clears or hard Encounters. Not premium-exclusive. |
| **Duty rewards** | Some Duties reward specific Fixtures as scripted rewards. |
| **Rootrail unlocks** | Rootrail repair steps unlock new Fixture recipes (see Rootrail system doc). |
| **Event rewards** | Event ladders may offer Fixtures with event-themed traits. Must pass canon: no premium-only power Fixtures. |
| **Treasure Shard exchange** | Treasure Shards may be exchanged for select Fixtures via exchanges. |

> **No premium-only power Fixtures at launch.** Any event or IAP Fixture must be obtainable via the free path within a reasonable timeframe (one event cycle or via Treasure Shards).

---

## Fixture Crafting

1. Player unlocks a Fixture recipe (via progression, Rootrail, Duty reward).
2. Recipe shows: required Fixture Materials, Mooncap cost (optional), crafting time (if any).
3. Player crafts the Fixture and it enters their Fixture inventory (un-equipped).
4. Player may then assign it to an open Fixture slot (if cap allows) or save it for later.
5. Crafting an additional copy of the same Fixture is allowed (duplicate builds).

**Early crafting recipe examples (Loamwake MVP):**

| Recipe ID | Name | Category | Inputs |
|-----------|------|----------|--------|
| `fix_rec_001` | Root-Bitten Shovel Strap | Farming | 6× Tangled Root Twine, 80 Mooncaps |
| `fix_rec_002` | Loamwake Canister | Pure Attack | 4× Crumbled Ore Chunk, 100 Mooncaps |
| `fix_rec_003` | Pale Glow Lens | Strain | 5× Dull Glow Shard, 90 Mooncaps |
| `fix_rec_004` | Burrower's Wrist Wrap | Farming | 8× Loam Fiber, 60 Mooncaps |
| `fix_rec_005` | Dirtclad Gauntlet | Pure Attack | 6× Crumbled Ore Chunk, 2× Tangled Root Twine, 110 Mooncaps |

> **UNRESOLVED:** Loamwake material names above are provisional. `loamwake_mvp_content_sheet.md` (Phase 2B file 1) should define canonical Loamwake material names and lock these recipe inputs.

---

## Fixture Upgrades / Salvage

- Fixtures may be **upgraded** using Fixture Materials and Polishes. Upgrades increase the primary stat contribution.
- Fixtures may be **salvaged** to recover partial Fixture Materials.
- Salvage does not return Mooncaps.
- Upgrade levels cap at a per-rarity maximum (e.g., Common: 3 upgrades, Uncommon: 5, Rare: 8).

---

## Hats — Overview

Hats are wearable cosmetic items that also confer passive permanent bonuses. They are a distinct system from personal Fixtures.

### Hat Unlock Rules

- Hats are **unlocked**, not equipped in the slot-based sense.
- Unlocking a hat permanently adds it to the player's Hat Collection.
- Once unlocked, a hat's passive bonus is **permanently active**, always.
- Unlocking a second hat does not replace the first hat's bonus. All unlocked hat bonuses stack.

**Model:** Hats behave like the shell-passive system in Super Snail — permanent stacking bonuses from collection, not slot-maintenance.

### Hat Display Rules

- The player selects **one hat** to display on their Gnome Survivor at any time.
- Changing the displayed hat does not affect bonuses — all unlocked hat bonuses remain active regardless of which is displayed.
- Display selection is in the Hat Collection / Wardrobe screen, separate from the Fixture screen.

### Hat Permanent Passive Rules

- Each hat grants one small permanent passive bonus.
- Hat passives are intentionally minor — they contribute to long-term account growth but do not create large power gaps between players.
- Hat passive categories: resource yield boosts, small stat bonuses, small Crack Coin bonuses, small Deepening bonuses, etc.
- Hat passives do **not** count as Fixture stat contributions — they are tracked in a separate `hat_passives` accumulator on the account.

**Example Hat passives:**
- *Loamwake Dirt Cap* — +1% Mushcap gather rate (permanent, stacks).
- *Clique Crest Cap* — +1% Favor Mark gain per Clique activity.
- *Glow Rim Hat* — +1% Echo Shard drop rate.
- *Three-Strata Brim* — +0.5% Strata Seal gain from Deepening.

### Hat Social / Identity Role

Hats are the **primary visible identity item** for Gnome Survivors in social contexts:
- Displayed in Clique roster views
- Displayed in Crack leaderboards
- Displayed in Festival Ledger top-contributor views
- Used in profile sharing / social surfaces

Hat acquisition is tied to progression milestones, events, Clique rank, and festival performance — not purely to premium spending. Premium hat availability is cosmetically distinct but must not have superior passives versus earnable hats.

---

## War Armory Boundary

War Fixtures are documented here for boundary clarity. Full War system design is out of scope for v1.

| Rule | Value |
|------|-------|
| War Fixtures count against personal Fixture cap | **No** |
| War Fixtures appear in personal Fixture screen | **No** |
| War Fixtures are accessible outside of active Club War context | No (War Armory is a separate UI surface) |
| War Fixtures use the same crafting system | Partial — same recipe/material framework, different material types |
| War Fixtures can have war-specific trait tags | Yes — they may have both normal-style and war-specific traits |
| War Armory visible at Loamwake MVP launch | **No** — stubbed |

War Fixtures are defined in the War Fixture content template (see `content_table_templates.md`).

---

## MVP Scope

### In Scope (Loamwake MVP)
- Personal Fixture cap 0→1 (tutorial) and 0→2 (first Loamwake clear)
- First 3–5 Fixture crafting recipes (Farming, Pure Attack, Strain)
- Fixture inventory display
- Hat tease: show the Hat Collection screen, unlock first hat as a Loamwake milestone reward
- First hat's permanent passive active on account

### Stubbed (visible but not playable)
- Full 0→12 capacity journey
- Full Hat Collection expansion
- Hat social display in Clique/Festival contexts
- Upgrade depth beyond basic upgrade

### Out of Scope (MVP)
- War Armory
- War Fixtures
- Duplicate-build UI (available but no tutorial for it)
- Full Rootrail-gated recipes (first few are pre-Rootrail)

---

## Data Requirements

See `docs/04_systems/data_schema_v1.md` entities: `fixture_item`, `fixture_recipe`, `fixture_loadout`, `hat_unlock`, `hat_display_state`, `war_fixture_item`, `war_armory_loadout`.

Key constraints:
- `fixture_loadout.equipped` must be an ordered array; length bounded by `account.fixture_cap`
- `hat_unlock.permanent_passive_active` must be always `true` once unlocked (no deactivation path)
- `hat_display_state.visible_hat_id` is a single ID; null = no hat displayed

---

## UI Requirements

- **Fixture Panel:** Shows equipped Fixture list, cap counter (e.g., "3/5"), craftable Fixtures, recipe browser
- **Hat Collection:** Shows all unlocked hats, all passives (always-active indicator), display selector
- **No overlap:** Fixture Panel and Hat Collection are separate screens or tabs
- **War Armory:** Hidden/locked until Clique War system unlocks; shown as a locked icon in the Fixture-adjacent nav

See `docs/07_ui/unlock_flow_and_ui_map.md` for timing of each unlock.

---

## Economy Requirements

Fixture-related economy flows:
- **Fixture Materials** (various named materials per Strata) — primary crafting inputs
- **Mooncaps** — secondary crafting cost (soft currency)
- **Polishes** — Fixture upgrade inputs
- **Treasure Shards** — Fixture exchange inputs (mid-game)
- **Rootrail Parts** — Indirectly unlock higher-tier Fixture recipes

See `data/economy/economy_model_v1_sheet.csv` for full faucet/sink rows.

---

## Balance Risks

| Risk | Mitigation |
|------|-----------|
| Duplicate-build creates dominant meta | Contextual bonuses reduce universal best-in-slot pressure; content monitoring |
| Hat passive accumulation curves too fast | Keep individual hat passives ≤ 1–2% per resource; total stack remains minor |
| Personal Fixture cap growth too slow → boredom | Validate first 4 cap increases happen within first 7 days of play |
| Personal Fixture cap growth too fast → no sense of progress | Validate final cap increase (reaching 12) is a late-game milestone |
| War Fixture power bleed into personal screen | Strict code-level separation; no shared loadout state |

---

## Unresolved Items

> **UNRESOLVED:** Exact Fixture Material names for Loamwake-tier crafting. Lock in `loamwake_mvp_content_sheet.md`.

> **UNRESOLVED:** Exact per-milestone cap increase schedule (how many Fixtures per Loamwake beat). Needs playtest data target.

> **UNRESOLVED:** Fixture upgrade cost curve — how many Polishes per level per rarity tier.

> **UNRESOLVED:** Whether Fixtures from events should use the same Fixture Materials pool or event-specific materials.

> **UNRESOLVED:** Hat passive interaction with Deepening / Memory Shift — do hat passives survive a Deepening reset? Current assumption: yes, permanently kept.
