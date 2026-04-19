# Gnome Game — Phased Implementation Planning Pack
## Project Manager Edition · v1.0 · April 2026

---

## 0. Executive Summary

This document converts a completed system-design conversation into an implementation-ready planning pack. The game is a **post-fallout idle/meta-progression RPG** featuring gnomes rebuilding civilization, inspired structurally by Super Snail's layered progression, weekly event economy, and dense home-hub model — but re-themed around gnome politics, hats, shrine rituals, DNA evolution, and regional exploration.

All design decisions below were finalized in the source chat. Items still requiring resolution are marked **[UNRESOLVED]**.

---

## 1. System Architecture Map

### 1A. Core Progression Spine (Vertical)

```
Player Account
  └─ Gnome Chancellor (avatar + stats)
       ├─ Combat Stats: HP / ATK / DEF / RUSH-equivalent
       ├─ Culture Stats: 5 AFFCT-equivalent families
       │     (Renown / Craft / Lore / Order / Ingenuity — or simplest names TBD)
       ├─ Equipped Hat (1 active, all owned stats stack)
       ├─ Active Region Trait (1 at a time)
       ├─ DNA Forms (multiple lines, tiered unlock)
       ├─ Gear (12 slots, region gear + form gear + instruments)
       ├─ Exploration Compass (relic placement board)
       └─ Shell-equivalent removed; hats absorb that role
```

### 1B. Progression Layers (Horizontal, all permanent)

| Layer | Purpose | Resets? |
|-------|---------|--------|
| Capital / Home Resources | idle income, building upgrades, relic placement | Never |
| Exploration / Regions | campaign spine, INTEL trees, realm gear | Never (prestige adds depth) |
| Hats | major equipment identity, stacking passive stats | Never |
| DNA / Forms | evolution lines, form-gated content, partner ties | Never |
| Relics / Antiquities | culture stats, compass/museum/shrine/home boards | Never |
| Partners | named allies, research specialization, story | Never |
| Minions / Retainers | troop bodies, combat layer, relic synergy | Never |
| Fissure | endless side dungeon, own currency + shop | Never |
| Region Traits | per-region signature buff, upgradeable via prestige | Never (upgrade path deepens) |
| Clubs | social layer, donations, shared goals, later war | Never |
| Prestige | meta loop — trait upgrades, new unlocks, Rift access | Regions re-challenge; progress carries over |

### 1C. Meta Progression Arc

```
5 Launch Regions (clear all)
  → Secret quest + stat gate
    → Prestige 1
      → Prestige 2–5
        → RIFT 1 unlocks (endless, never closes)
          → Prestige 6–10 (Rift boss-gated)
            → RIFT 2 unlocks
              → Old Rifts remain permanently relevant
```

### 1D. System Dependency Web

```
Regions ──► DNA cells (random exploration drops)
DNA ──► Form unlocks (all lines must hit threshold for next tier)
DNA ──► Partner unlock gates
Partners ──► Research specialization ──► DNA speedup
Partners ──► Minion unlock (each partner brings unique minion)
Minions ──► Chores, troop combat, later Species War
Relics ──► Culture stats ──► RUSH trigger in combat
Relics ──► Compass / Museum / Shrine / Home boards
Hats ──► DNA line affinity (light tie, not hard-lock)
Region Traits ──► Prestige upgrades
Prestige ──► Rift access ──► Relic pool unlocks (Rift bosses)
Fissure ──► Independent endless lane (never resets)
Clubs ──► Region 1 clear + Day 3 + resource gate
```

---

## 2. Launch Scope vs Later Scope

### V1 — Launch

| System | Launch Scope |
|--------|-------------|
| Regions | 5, each with signature trait + farming identity |
| Weekly Events | 3 rotating (Lottery / Archive / Shrine), 1 side subloop each |
| Beginner Runway | 2-week beginner event + 2-week hoard event |
| Hats | 3 per DNA line (1 combat, 1 explore, 1 mixed) + 1 hidden Easter egg each |
| Hat Augments | None at launch; 1 slot unlocks after Prestige 3 (Easter egg) |
| DNA Lines | **[UNRESOLVED]** — number of launch DNA lines and their identities |
| Partners | 1 per DNA line |
| Minions | 1 type per partner (same DNA specialization) |
| Placement Boards | 4: Home resources, Compass, Shrine, Museum |
| Clubs | Roster + donations + shared goals + light chat + club rank |
| Club Rank | Emblems, trims, member slots |
| Species War | Stub architecture only — not playable |
| Fissure | Endless dig, own currency (Ancient Coins equiv), own shop |
| Garage | Dismantle gear + materials |
| Rocket | Long repair project (~2 months), opens later domain content |
| Rankings | Solo only, server-based (relic power, total power, DNA power) |
| Monetization | Cosmetics + light event pass + small packs; no premium-only power relics |
| Vanity | Portrait frames (power threshold), prestige trims (1/5/10), club visuals |
| Vanity Passives | Small stacking bonuses (+2% expedition speed, etc.), soft-capped per family |
| Cosmetic Store | Minimal; most earned through play |

### Later Expansion

| System | Later Scope |
|--------|------------|
| Regions | 6+ additional regions |
| Rift 1 & 2 | Rift 1 at Prestige 5, Rift 2 at Prestige 10 |
| Species War | Full club-vs-club multiplayer PvP |
| Hat Augment Expansion | Multiple augment slots, typed augments |
| 2nd Side Subloop | Per weekly event (if testing supports it) |
| Shrine Skins/Effects | Cosmetic shrine layer |
| More Partners | Expanded per DNA line |
| More Hats | 30+ per DNA line long-term |
| Alien / Story Arc | Aliens visit → Rift justification → deeper prestige story |
| Domain Content | Rocket-gated domain exploration |
| Advanced Fissure | New branches, instance types, deeper shop |
| Cross-Region Trait Synergy | Trait interactions with Rift and club war |

---

## 3. Currency / Sink / Faucet Map

### 3A. Core Currencies

| Currency | Role | Faucet (Source) | Sink (Spend) | Classification |
|----------|------|----------------|-------------|----------------|
| **Burrow Scrip** (B-tads equiv) | Main soft currency | Home resources, exploration, events, daily tasks | Building upgrades, relic activation, gear, unlocks | Core progression |
| **Glowcaps** (W-tads equiv) | Premium earnable | Daily trickle, events, milestones, purchasable | Vending machine premium items, event packs, convenience | Monetization + progression |
| **Mushrooms** (Food/Energy) | Exploration stamina | Home fungus farm, idle production | Exploration runs | Friction gate |
| **Wood & Stone** | Building materials | Home lumber/quarry, idle production | Capital building upgrades | Core progression |
| **Ancient Coins** (Fissure) | Fissure economy | Golden Pickaxe digging, Drill Rig | Fissure shop | Side progression |
| **Star Coins** (Club) | Club economy | Daily free claim, Species War later | Club shop | Social progression |

### 3B. Event Currencies

| Currency | Event | Ease of Acquisition | Notes |
|----------|-------|-------------------|-------|
| Raffle Slips (Lottery Tickets equiv) | Lottery Week | Easiest | Hoard for 2 weeks |
| Petition Seals (Wish Coins equiv) | Archive/Discovery Week | Rarer | Board focus → relic family |
| Shrine Offerings + Speedups | Shrine/Offering Week | Rarest components | 1h offering = 1 point; summon orbs separate |
| Spirit Crystals equiv | Shrine Week side | Earned from scoring tiers | Spent in Crystal Boom-style shop |
| Festival Stamps (Weekly Special equiv) | All 3 events (meta) | Across full 3-week cycle | Meta reward track |
| Feast Tickets (Supreme Feast equiv) | Weekly participation | From event participation | Roulette spins |

### 3C. Upgrade Materials

| Material | Used For |
|----------|---------|
| Reagent (tiered: green→blue→purple→orange) | Relic upgrades |
| Fragments (relic-specific) | Relic tier advancement |
| Glue | Gear upgrades / merging |
| Coating | Shell equiv → Hat upgrades |
| Demon God Cells equiv (per DNA type) | DNA form evolution |
| Scrolls (tiered) | Partner/minion advancement |
| Rocket Parts (special) | Rocket repair project |

### 3D. Currency Flow Diagram

```
HOME IDLE OUTPUT
  ├─ Burrow Scrip ──► buildings, relics, gear, unlocks
  ├─ Mushrooms ──► exploration runs
  ├─ Wood/Stone ──► capital upgrades
  └─ Time Machine output ──► premium trickle

EXPLORATION
  ├─ INTEL ──► perk trees
  ├─ Cards ──► card exchange items
  ├─ DNA Cells ──► form evolution (random drops)
  ├─ Gear drops ──► equipment
  └─ Region-specific resources ──► choke-point farming

WEEKLY EVENTS
  ├─ Raffle Slips ──► lottery pulls ──► relics, scrolls, materials
  ├─ Petition Seals ──► targeted relic pools
  ├─ Offering Speedups ──► shrine scoring ──► spirit crystals ──► crystal boom shop
  └─ Festival Stamps ──► meta reward track

FISSURE
  └─ Golden Pickaxes ──► floors ──► Ancient Coins ──► Fissure shop

CLUBS
  └─ Donations ──► club EXP ──► club level ──► member slots + shop stock
```

### 3E. Pressure Points / Friction Gates

| Gate | What It Blocks | Design Intent |
|------|---------------|--------------|
| Mushroom cap | Exploration session length | Return-to-game cadence |
| DNA cell rarity | Form tier advancement | Region farming loops |
| Reagent tiering | Relic upgrade speed | Long-tail engagement |
| Partner unlock requirements | DNA/region cross-gates | Layered discovery |
| Prestige stat gate | Next prestige tier | Build optimization |
| Golden Pickaxe scarcity | Fissure depth speed | Long-tail side lane |
| Glowcap trickle rate | Premium shop access speed | Light monetization pressure |

---

## 4. Home-Screen Module Map

### Screen A — Hearth / Main Hub
| Module | Function |
|--------|----------|
| Resource collectors (pond, fungus, mine, time machine equiv) | Idle income collection |
| Resource upgrade sign | Embed relics, upgrade production |
| Club entrance | Access club roster/goals/chat |
| Altar / Shrine entrance | Soul offerings, god encounters |
| Event billboard | Current weekly event access |
| Mailbox | Quests, encounters, partner letters, hitmen |
| Nursery entrance | Minion clone/heal/chores |
| Alchemy Furnace equiv | Wish coin spending, fragment conversion |

### Screen B — Expedition Yard
| Module | Function |
|--------|----------|
| Region travel / destination selector | Choose exploration realm |
| Supplies panel | Mushroom loading |
| Exploration compass | Relic embedding for HARD stat boosts |
| Region trait selector | 1 active trait at a time |
| Exploration yields popup | Idle exploration rewards |
| INTEL tree | Per-region perk advancement |

### Screen C — Archive Hall
| Module | Function |
|--------|----------|
| Relic inventory (tabbed by culture stat type) | Browse, upgrade, awaken relics |
| Museum (1 per region, 16 stands each) | Relic display for medals + buffs |
| Luggage / general inventory | Items, consumables, materials |
| Chest wall | Open accumulated chests |
| Bookcase / Rankings | Leaderboards, postcards, partner screen |

### Screen D — Gene Lab
| Module | Function |
|--------|----------|
| Gene Research Machine | DNA form evolution |
| Terminal / Computer | Daily tasks, gene sim, minigames, database |
| Gear Forge / Arsenal | View + upgrade gear |
| Incense Shrine (wall button toggle) | Hidden encounter system |
| Lottery Machine | Ticket-based gacha pulls |
| Archive Discovery Machine | Relic-targeting event interface |

### Screen E — Outer Works
| Module | Function |
|--------|----------|
| Fissure | Golden Pickaxe digging, instances, shop |
| Garage | Dismantle gear, later wormhole/refinement |
| Rocket | Long repair project, later domain access |
| Future alien/Rift hooks | Expansion-ready space |

### Unlock Visibility Order
1. **Immediate**: Hearth resources, Expedition (Region 1), basic Gene Lab
2. **Day 1–3**: Mailbox, Vending Machine, basic gear
3. **Region 1 clear**: Club unlock, Nursery start, expanded Gene Lab
4. **Region 2+**: Museum, Fissure, expanded compass
5. **Mid-game**: Garage, Rocket start, Arena
6. **Late-game**: Rift prep, Domain content, advanced Fissure

---

## 5. Region Progression Plan

### 5A. Launch Regions (5)

| Region # | Mechanical Identity | Farm Focus | Signature Trait Type | Unlock Order |
|----------|-------------------|-----------|---------------------|-------------|
| 1 | Tutorial + onboarding | Basic resources, first partner | Combat pressure (pre-combat damage) | Start |
| 2 | First real difficulty wall | DNA cells (specific line) | Relic / archive fragments | After R1 apostle |
| 3 | Mid-game depth | Shrine materials, souls | Shrine yield (offering speed/output) | After R2 apostle |
| 4 | Build optimization | Travel speed, exploration efficiency | Expedition tempo | After R3 apostle |
| 5 | Pre-prestige prep | Gene/retainer materials | Gene / retainer growth | After R4 apostle |

### 5B. Per-Region Structure

Each region contains:
- Multiple exploration zones with timed idle runs
- INTEL tree (2 branches, ~19 skills each, 1–3 levels per skill — per source model)
- Realm-specific gear (1 slot dedicated, DMG +N% in that realm)
- Apostle bosses (gated by stats/AFFCT)
- Card exchange system (exploration cards → items)
- Key clue events (partner unlock triggers)
- AFFCT check gates (culture stat requirements)
- Museum (unlocks after final apostle, 16 stands)
- Region trait (unlocks after full clear)

### 5C. Choke Points by Region

| Region | Choke Point | Player Must... |
|--------|------------|---------------|
| 1 | First apostle stat wall | Upgrade gear, embed compass relics |
| 2 | DNA cell farming | Farm R2 exploration for specific DNA type cells |
| 3 | Partner unlock gate | Have specific DNA form level + R2 partner |
| 4 | AFFCT check wall | Upgrade relics substantially for culture stat thresholds |
| 5 | Multi-system optimization | Balance DNA, partner, gear, trait, and relic builds |
| Prestige | Secret quest + stat gate | Complete all 5, find hidden requirement, hit numeric threshold |

### 5D. Prestige Loop

```
Clear all 5 regions
  → Complete secret quest (hidden, discoverable)
  → Meet stat gate (combat + culture minimums)
    → Prestige 1 (trait upgrade + small unlock + prestige resource)
      → Prestige 2
        → Prestige 3 (hat augment slot Easter egg)
          → Prestige 4
            → Prestige 5 → RIFT 1 OPENS
```

Each prestige grants:
- Stronger region trait numbers
- Trait system upgrade (new passive, new interaction)
- Small hat/relic/partner unlock path
- Prestige-specific resource
- Boss unlock interactions in Rift

---

## 6. Event Math Targets

### 6A. Weekly Cycle Structure

```
Week 1: Lottery Week
Week 2: Archive/Discovery Week
Week 3: Shrine/Offering Week
  (repeat forever)

Meta-event: Festival Ledger spans all 3 weeks
Sub-events per week:
  - Lottery: Clover-Vendor-style side vendor
  - Archive: targeted relic vendor/exchange
  - Shrine: Observatory-style side scoring
```

### 6B. Offering Week Math (Best Source Data Available)

Source confirms: **1 hour offered = 1 point**. 5 rounds, each round = 240 points. Total for all 5 rounds = 1,200 offering hours. Each round awards 1,100 Spirit Crystals + other rewards. Completing all 5 rounds awards 5,500 Spirit Crystals total.

**Target for your game:**

| Metric | F2P Target | Light Spender | Notes |
|--------|-----------|--------------|-------|
| Tiers completable | 1, maybe 2 | 2–3 | With disciplined hoarding |
| Total tiers at launch | 3–5 | 3–5 | Math must support F2P tier 1 |
| Hoard window | 2 weeks | 2 weeks | Same cadence as source |
| Paid capstone tier | — | Requires event pack purchase | No brute-buy of all tiers |

### 6C. Lottery Week Structure

Source: Lottery Tickets → pulls → pity/guarantee → voucher side shop → 5 scoring rounds.

**Your version should target:**
- ~60–80 tickets hoardable in 2 weeks for F2P (enough for 1 full tier)
- Each 10-pull has pity counter
- Voucher shop rotates relics
- Side subloop: Lucky-chain or bonus-pull Easter egg

### 6D. Archive/Discovery Week Structure

Rarer currency than Lottery. Player picks board focus first, then relic family.

**Target:**
- ~30–50 Petition Seals hoardable in 2 weeks for F2P
- Board selection UI (Compass / Museum / Shrine / Home)
- Family filter within selected board
- Clover-Vendor-style side exchange

### 6E. Event Pack Pricing Philosophy

| Pack Type | Contents | Price Point |
|-----------|----------|------------|
| Daily event pack | Small: tickets, souls, speedups | 1 Glowcap (earnable) |
| Weekly event pack | Medium: more tickets + materials | Small cash purchase |
| Festival Ledger (free lane) | Stamps → rewards | Free |
| Festival Ledger (paid lane) | Better stamp rewards | Light subscription |
| Spending tier milestone | Relic unlock at cumulative spend | Not direct purchase |

---

## 7. Risk List

### Critical Risks

| # | Risk | Impact | Mitigation |
|---|------|--------|-----------|
| 1 | **Backend architecture underestimated** | Game is a live service; client is easy, server state management is hard | Build backend-first; use Firebase Auth + custom server or PlayFab |
| 2 | **System overload at launch** | Too many systems visible = tutorial death | Strict unlock gating; most systems hidden until progression triggers |
| 3 | **Event economy imbalance** | F2P can't complete tier 1, or whales trivialize everything | Math model before build; 2-week hoard must yield tier 1 for disciplined F2P |
| 4 | **DNA/Partner/Region circular dependency confusion** | Players don't understand what gates what | Clear in-game dependency viewer; "what do I need?" button |
| 5 | **Content drought post-launch** | 5 regions + 3 events feels thin after month 2 | Template-driven content bricks; plan first expansion regions before launch |

### High Risks

| # | Risk | Impact | Mitigation |
|---|------|--------|-----------|
| 6 | **Hat system feels like reskinned shells** | Loses differentiation | Hats must be real equipment choices (1 active); shells in source are passive collection |
| 7 | **Club system too thin** | Players join, see nothing to do, leave | Shared goals + donation rewards + club rank must feel meaningful week 1 |
| 8 | **Fissure pacing** | Golden Pickaxes too rare = dead system; too common = trivial | Tune pickaxe drop rate to ~1–2 per day, enough for 1–2 floors |
| 9 | **Monetization too light** | Can't sustain development | Spending-tier relic milestones + Glowcap purchase + event packs should generate enough |
| 10 | **Vanity passive power creep** | Stacking bonuses become mandatory | Soft cap per bonus family (expedition speed: max +10–15%, etc.) |

### Medium Risks

| # | Risk | Impact | Mitigation |
|---|------|--------|-----------|
| 11 | **Region trait "one active" frustration** | Players want multiple traits | Design intention: forces farming/attack build swapping; communicate this clearly |
| 12 | **5th DNA form gating** | Feels punitive if other lines are boring to level | Make each DNA line have at least 1 interesting unlock even at low tiers |
| 13 | **Rocket repair too slow** | 2-month repair with nothing to show | Add intermediate milestones with small rewards during repair |
| 14 | **Prestige stat gate too opaque** | Players don't know what they need | Show progress bar toward prestige requirements |

---

## 8. Recommended Build Order

### Phase 0 — Foundation (Weeks 1–4)
**Specialist: Backend Architect + Game Designer**

- [ ] Freeze game fantasy one-pager
- [ ] Freeze design principles document
- [ ] Define data schema: accounts, characters, regions, relics, partners, minions, hats, DNA, clubs
- [ ] Choose backend stack (Firebase Auth + custom server OR PlayFab)
- [ ] Set up Android project skeleton
- [ ] Set up CI/CD pipeline
- [ ] Create content spreadsheet templates (regions, relics, hats, partners, incidents, events)

### Phase 1 — Core Loop (Weeks 5–10)
**Specialist: Systems Programmer + Economy Designer**

- [ ] Home resource idle loop (collect, cap, upgrade)
- [ ] Exploration system (destination, food, timer, idle yields, INTEL)
- [ ] Region 1 content (zones, apostles, card exchange, key clues)
- [ ] Basic combat system (auto/stat-check, RUSH trigger from culture stats)
- [ ] Gear system (12 slots, equip, merge/upgrade)
- [ ] Save/load + account sync

### Phase 2 — Identity Systems (Weeks 11–16)
**Specialist: Systems Programmer + Content Designer**

- [ ] Relic system (activate, upgrade, tier, awaken, resonance)
- [ ] Compass board (5 culture stat slots, relic embedding)
- [ ] Hat system (equip 1, owned stats stack, identity passive, synergy hook)
- [ ] DNA system (cell collection, form unlock, tier gating, 5th form rule)
- [ ] Partner system (unlock, specialization, mail/story, minion tie)
- [ ] Minion system (clone, heal, stats, troop combat basics)
- [ ] Region trait system (unlock on clear, 1 active at a time)

### Phase 3 — Economy & Events (Weeks 17–22)
**Specialist: Economy Designer + LiveOps Specialist**

- [ ] Currency system (Burrow Scrip, Glowcaps, event currencies, materials)
- [ ] Vending Machine / Burrow Exchange (daily shop)
- [ ] Lottery event system (tickets, pulls, pity, voucher shop, scoring tiers)
- [ ] Archive/Discovery event system (board focus, relic family targeting)
- [ ] Shrine/Offering event system (time-based scoring, speedups, summon orbs)
- [ ] Festival Ledger meta-event (stamps across 3-week cycle)
- [ ] Event pack / spending tier system
- [ ] Beginner Event (2-week onboarding event)
- [ ] Hoard Event (2-week stockpiling event)

### Phase 4 — Home Hub & Side Systems (Weeks 23–28)
**Specialist: UX Designer + Systems Programmer**

- [ ] Home screen layout (5 screens, tappable stations, unlock gating)
- [ ] Museum system (16 stands per region, scoring, medals, shop)
- [ ] Shrine / Altar (souls, offerings, god encounters, hidden encounter layer)
- [ ] Incense-equivalent system (rare item → hidden events, critter encounters)
- [ ] Fissure (golden pickaxes, 5 hits, floors, instances, shop, drill rig)
- [ ] Garage (dismantle gear, materials recovery)
- [ ] Terminal / Computer (daily tasks, gene sim, minigames, database)
- [ ] Mailbox (encounters, hitmen, partner letters)
- [ ] Notification / red-dot system
- [ ] Tutorial / onboarding flow

### Phase 5 — Social & Clubs (Weeks 29–32)
**Specialist: Social Systems Designer + Backend Engineer**

- [ ] Club creation (resource gate + R1 Easter egg item)
- [ ] Club roster, donations, shared goals, light chat
- [ ] Club rank system (emblems, trims, member slots)
- [ ] Club shop (Star Coins)
- [ ] Species War stub architecture (data model, not playable)
- [ ] Rankings (solo: relic power, total power, DNA power)

### Phase 6 — Regions 2–5 & Prestige (Weeks 33–40)
**Specialist: Content Designer + Balance Specialist**

- [ ] Regions 2–5 content (zones, apostles, INTEL trees, card exchanges, key clues)
- [ ] Region-specific gear sets
- [ ] Region-specific traits (one per region)
- [ ] Per-region museum
- [ ] Partner unlock chains (cross-region/DNA gates)
- [ ] Prestige system (secret quest, stat gate, reward structure)
- [ ] Prestige 1–5 content and pacing
- [ ] Rift 1 stub (architecture, not full content)

### Phase 7 — Polish & Launch Prep (Weeks 41–48)
**Specialist: QA Lead + LiveOps Specialist + Economy Balancer**

- [ ] Economy balance pass (F2P tier 1 completion, hoard math, spending tiers)
- [ ] Choke-point pacing verification (DNA farming, partner gates, AFFCT checks)
- [ ] Tutorial completeness check
- [ ] Notification fatigue audit
- [ ] Rocket repair intermediate milestones
- [ ] Easter egg placement and testing
- [ ] Vanity passive cap verification
- [ ] Soft-launch with 20–50 testers
- [ ] Analytics integration
- [ ] Store listing prep
- [ ] Launch

---

## 9. Specialist Roster

| Role | Responsibility | When Needed |
|------|---------------|------------|
| **Backend Architect** | Server state, auth, save sync, remote config, anti-cheat | Phase 0–1, ongoing |
| **Systems Programmer** | Core game systems, combat, exploration, relics, hats, DNA | Phase 1–4 |
| **Economy Designer** | Currency math, event pacing, F2P/spender balance, sink/faucet | Phase 3, then ongoing |
| **Content Designer** | Regions, incidents, relics, partners, bosses, Easter eggs | Phase 2–6 |
| **UX Designer** | Home layout, unlock flow, notification system, tutorial | Phase 4 |
| **Social Systems Designer** | Clubs, rankings, later Species War | Phase 5 |
| **Balance Specialist** | Choke points, DNA gating, prestige pacing, trait balance | Phase 6–7 |
| **LiveOps Specialist** | Event deployment, remote config, content pipeline | Phase 3, then ongoing |
| **QA Lead** | System interaction testing, economy edge cases, Easter eggs | Phase 7, then ongoing |
| **Naming / Lore Writer** | System naming, region identities, DNA lines, story beats | Phase 0 (parallel) |

---

## 10. Unresolved Items

| # | Item | Blocking? | Recommended Resolution |
|---|------|-----------|----------------------|
| 1 | **DNA line identities and count** | Yes — blocks hat families, partner design, region DNA ties | Define 5–6 DNA lines with names and themes |
| 2 | **5 launch region names and themes** | Yes — blocks content production | Name them; themes partly decided (combat/relic/shrine/expedition/gene) |
| 3 | **Final stat family names** | No — behavior locked, naming deferred | Pick simplest: "Combat Stats" and "Culture Stats" or specific names |
| 4 | **Final currency names** | No — roles defined, names in draft | Lock Burrow Scrip / Glowcaps or alternatives |
| 5 | **Game title** | No — explicitly deferred ("do not rename yet") | Decide before store listing |
| 6 | **Exact hat augment type** | Low — after Prestige 3 only | Universal at launch, typed later |
| 7 | **Mixed hat definition** | Low | Combat + exploration hybrid; confirm per DNA line |
| 8 | **Club unlock exact resource cost** | Medium | Playtesting determines "meaningful dent" amount |
| 9 | **Hoard event structure** | Medium | 2-week event with all 3 future currencies dropping; details TBD |
| 10 | **6 forms for DNA** | Yes | Source has 6 forms (Zombie, Demon, Angel, Mutant, Mecha, Dragon); your equivalents needed |

---

## 11. Super Snail → Gnome Translation Reference (Condensed)

| Super Snail Term | Your Game Equivalent | Status |
|-----------------|---------------------|--------|
| Snail | Gnome Chancellor / Regent | Decided |
| Home Base | Gnomehold / The Warren | Draft name |
| Realms | Marches / Provinces | Draft name |
| Time Rift | Deep Root Rift / Fey Fracture | Draft name |
| HARD stats | Combat Stats | Decided |
| AFFCT | Culture Stats / Influence | Decided (simplest) |
| FAME/ART/FTH/CIV/TECH | Renown/Craft/Lore/Order/Ingenuity | Draft names |
| Shells | **Removed** — Hats absorb this role | Decided |
| Relics | Antiquities / Treasures | Draft name |
| Museum | Hall of Antiquities | Draft name |
| Compass | Expedition Compass | Decided |
| Partners | Allies / Companions | Draft name |
| Minions | Retainers / Squads | Draft name |
| Leadership | Command / Authority | Draft name |
| Club | Guild / House | Draft name |
| Species War | Guild Campaign (later) | Decided |
| Black Tadpoles | Burrow Scrip | Draft name |
| White Tadpoles | Glowcaps | Draft name |
| Lottery Tickets | Raffle Slips | Draft name |
| Wish Coins | Petition Seals | Draft name |
| Offering Speedups | Shrine Resin | Draft name |
| Dragon Orbs | Elder Sigils / Crown Embers | Draft name |
| Souls | Offerings / Echoes | Draft name |
| Altar | Ancestor Shrine | Decided |
| Incense | Ancestor Resin / Glowcone | Draft name |
| Lottery Week | Raffle Week / Fair Week | Draft name |
| Wish Week | Petition Week / Archive Week | Draft name |
| Offering Week | Shrine Week / Vigil Week | Draft name |
| Weekly Special | Festival Ledger | Draft name |
| Vending Machine | Burrow Exchange | Draft name |
| Global Monitor | Operations Desk / Ledger Console | Draft name |
| Fissure | Fissure (may keep or rename) | Decided (keep concept) |
| Gene Forms (6) | DNA Lines (count TBD) | **[UNRESOLVED]** |
| Hitmen | **[UNRESOLVED]** | Need gnome equivalent |
| Fugitives | **[UNRESOLVED]** | Need gnome equivalent |
| Visitors | **[UNRESOLVED]** | Need gnome equivalent |

---

## 12. Key Design Principles (Carry Forward)

1. **Tiny world, grand stakes** — everything hilariously overdramatic
2. **Progress always matters** — no dead sessions
3. **Politics is gameplay** — not text fluff (even in lighter form)
4. **Easy to add content** — every system supports new regions, hats, relics, partners, incidents
5. **Distinct identity** — not "Super Snail with gnomes"; idle geopolitical absurdism
6. **Choke points are features** — deliberate farming walls create account planning
7. **Easter eggs are expandable layers** — hidden content deepens over time without overloading tutorials
8. **Simplest launch-safe version first** — complexity grows with the player, not before them

---

## 13. Next Steps for PM

### Immediate (This Sprint)
1. **Resolve DNA lines** — number, names, themes (blocks hat families, partner design, region ties)
2. **Resolve region names** — 5 launch regions with themes aligned to mechanical identity
3. **Assign Naming/Lore Writer** — parallel track; don't block systems on naming

### Next Sprint
4. **Commission Economy Model** — spreadsheet with event math, hoard targets, F2P tier completion, spending tier milestones
5. **Commission Data Schema** — database tables for accounts, regions, relics, hats, DNA, partners, minions, clubs, events
6. **Start Backend Architecture** — auth, save sync, remote config, content delivery

### Following Sprint
7. **Build Core Loop Prototype** — home resources + exploration + basic combat + gear
8. **Validate Fun** — is the minute-to-minute loop satisfying before adding density?

---

*Document generated from completed system design conversation. All decisions traced to source chat consensus. Unresolved items explicitly marked. Ready for PM implementation planning.*
