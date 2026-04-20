# Gnome Survivor — Implementation Planning Pack v0.1.0

**Document version:** 0.1.0  
**Status:** DRAFT — awaiting owner sign-off  
**Date:** 2026-04-19  
**Author:** Project Manager (Claude)  
**Supersedes:** `next_steps_report.md` planning recommendations  

---

## 0. Project Status Assessment

### Phase 1: Conceptual Foundation — COMPLETE (17/17 docs)

| Doc | Status | Quality |
|-----|--------|---------|
| master_glossary.md | ✅ locked | strong — canonical naming authority |
| master_index.md | ✅ locked | strong — folder structure + reading order |
| phase1_content_bible.md | ✅ locked | strong — 5 Strata defined |
| strains_bible.md | ✅ locked | strong — 5 Strains with contradiction layers |
| clique_confidants_bible.md | ✅ locked | strong — social layer defined |
| deepening_arc.md | ✅ locked | strong — 5 Deepenings mapped |
| duties_and_encounters.md | ✅ locked | strong — content taxonomy complete |
| wanderers_runaways_wild_ones.md | ✅ locked | strong — 3 roaming families defined |
| burrow_posts.md | ✅ locked | strong — scalable post system |
| burrow_posts_examples.md | ✅ locked | good — example content |
| event_content_templates.md | ✅ locked | strong — reusable event bricks |
| event_examples.md | ✅ locked | good — 3 cycle examples |
| economy_and_progression_templates.md | ✅ locked | strong — faucet/sink framework |
| economy_model_v1.md | ✅ draft | good — prototype bands, no final math |
| system_interactions_matrix.md | ✅ locked | strong — dependency web mapped |
| content_scaling_rules.md | ✅ locked | strong — 100+ cast scaling plan |
| seasonal_expansion_framework.md | ✅ locked | strong — Year 1-2 cadence |
| season_briefs_pack.md | ✅ locked | strong — 6 season briefs |

### Phase 2: Production Foundation — NOT STARTED (0/6 docs)

| Doc | Status | Blocker |
|-----|--------|---------|
| data_schema_v1.md | ❌ not started | needs economy model finalized |
| backend_architecture_options.md | ❌ not started | needs schema + scope decision |
| core_loop_mvp_spec.md | ❌ not started | needs economy + schema |
| unlock_flow_and_ui_map.md | ❌ not started | needs MVP spec |
| content_table_templates.md | ❌ not started | needs schema |
| project_roadmap.md | ❌ not started | THIS DOCUMENT begins it |

### Verdict

Phase 1 is complete and remarkably strong. The conceptual foundation is better than most indie projects at this stage. The project's biggest risk is now **not transitioning fast enough into Phase 2**. Every day spent adding more lore without build artifacts increases the gap between vision and prototype.

---

## 1. Flags and Clarifications Needed

### FLAG 1 — "Hats as major equipment" (UNRESOLVED)
The task brief lists "hats as major equipment" as an agreed direction. However, no project document mentions hats as a distinct equipment category. The docs define **Clothes** as the gear system with: Common / Fine / Rare / Grand / Ancient tiers and Cloth Assembly as the crafting surface. 

**Decision needed:** Are hats a sub-slot within Clothes (like head slot), or a separate parallel equipment system? If separate, this needs its own design sheet and economy lane. Recommend: treat hats as the **hero slot** within Clothes — the most visible, most upgrade-worthy, most cosmetically important slot. This preserves existing Clothes architecture while giving hats narrative weight.

### FLAG 2 — "Rift structure" for prestige (NEEDS MAPPING)
The task brief mentions "prestige into Rift structure." The docs define **Deepening** as prestige and **Memory Shift** as the time-rift equivalent. The Rift as a separate structural concept is not yet documented as a distinct system. 

**Current mapping:** Deepening = prestige reset/escalation. Memory Shift = rift content (encounters, Shift Wardens, alternate-timeline moments). The Crack = endless vertical progression (Fissure equivalent). These three together form the "Rift structure" but no single doc unifies them. Recommend: create a `rift_systems.md` in Phase 2 that maps how Deepening, Memory Shift, and The Crack interlock.

### FLAG 3 — "Multiple placement boards" (PARTIALLY RESOLVED)
The docs define three distinct placement/pull systems: Lucky Wheel (Lucky Draw Week), Treasure Finder (Treasure Week), Ancestral Depictor (Echo Week). These are the "boards." Additionally, the Pathwheel serves as a passive build-shaping board. 

**Decision needed:** Is this the complete set, or are additional board types planned? Recommend: lock these 4 as v1 boards and add new board types via seasonal expansion only.

### FLAG 4 — Economy model has bands, not numbers
economy_model_v1.md provides excellent target ranges (e.g., 2,500-4,500 Mooncaps/day) but acknowledges 14 open tuning questions. These must be resolved in spreadsheet form before prototype implementation.

### FLAG 5 — "Bronze Shovels" naming concern
This is the only currency name that sounds generic rather than world-flavored. Every other currency has a strong gnome-world identity (Mooncaps, Glowcaps, Mushcaps, Echoes). Bronze Shovels could be confused with a real-world reference. Consider: **Crack Picks**, **Digger's Marks**, or **Deepstone Tokens**.

### FLAG 6 — No tutorial/onboarding flow exists
The docs describe what systems exist but not how a brand-new player encounters them minute by minute. This is critical for gacha/idle games where the first 10 minutes determine retention.

### FLAG 7 — Monetization model is philosophy only
The docs say "cosmetics/light monetization, no premium-only power relics at launch" but there is no IAP catalog, no pricing structure, no battle pass equivalent spec, no first-purchase offer design.

### FLAG 8 — Super Snail APK reference data is unintegrated
The project folder contains full APK analysis of Super Snail (file_info.txt, aapt_permissions.txt, aapt_badging.txt, unity_libs.txt, etc.). This confirms the reference game runs on Unity with IL2CPP. This data should inform the backend architecture decision but is not yet referenced in any design doc.

---

## 2. System Architecture Map

### Core Loop Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    THE BURROW (Home Base)                     │
│                                                               │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐       │
│  │ Dewpond  │ │Mushpatch │ │ Rootmine │ │Tickworks │       │
│  │(Mooncaps)│ │(Mushcaps)│ │(Mooncaps)│ │(Mooncaps)│       │
│  └────┬─────┘ └────┬─────┘ └────┬─────┘ └────┬─────┘       │
│       └──────┬─────┴──────┬─────┴──────┬─────┘             │
│              ▼                                                │
│  ┌────────────────────────────────────────────┐             │
│  │         DAILY LOOP OUTPUTS                  │             │
│  │  Mooncaps / Mushcaps / Materials / Glowcaps │             │
│  └──────────────────┬─────────────────────────┘             │
│                     │                                        │
│  ┌──────────┐  ┌────┴─────┐  ┌──────────┐  ┌──────────┐   │
│  │  Burrow  │  │  Daily   │  │  Burrow  │  │  Cloth   │   │
│  │  Post    │  │  Duties  │  │ Exchange │  │ Assembly │   │
│  └──────────┘  └──────────┘  └──────────┘  └──────────┘   │
│                                                               │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌──────────┐   │
│  │  Strain  │  │  Vault   │  │  Burrow  │  │ Standing │   │
│  │  Hall    │  │  of Trea │  │  Box     │  │  Shelf   │   │
│  └──────────┘  └──────────┘  └──────────┘  └──────────┘   │
└───────────────────────┬─────────────────────────────────────┘
                        │
                        ▼
┌─────────────────────────────────────────────────────────────┐
│                   STRATA (5 Launch Regions)                   │
│                                                               │
│  Loamwake → Ledgerhollow → Memory Fen → Wayworks → Buried  │
│  (starter)   (archives)    (ritual)    (infra)    Court     │
│                                                               │
│  Each Stratum produces:                                      │
│  Treasures / Clothes / Strain Seeds / Buried Clues /        │
│  Confidants / Wardens / Strata Traits                        │
└───────────────────────┬─────────────────────────────────────┘
                        │
            ┌───────────┼───────────┐
            ▼           ▼           ▼
    ┌──────────┐ ┌──────────┐ ┌──────────┐
    │Confidants│ │ Treasures│ │  Clothes │
    │(Partners)│ │+ Pathwh. │ │+ Battle  │
    │  Trust   │ │ Heritage │ │  Stats   │
    │  Trails  │ │  Stats   │ │          │
    └────┬─────┘ └──────────┘ └──────────┘
         │
         ▼
    ┌──────────┐
    │Burrowfolk│
    │(Minions) │
    │  Raise / │
    │  Train   │
    └──────────┘
```

### Side Systems Architecture

```
┌──────────────┐  ┌──────────────┐  ┌──────────────┐
│  THE CRACK   │  │  THE ASCENDER│  │    CLIQUE    │
│  (Fissure)   │  │  (Long proj) │  │   (Social)   │
│              │  │              │  │              │
│ Bronze Shov. │  │ Ascender     │  │ Favor Marks  │
│ → Crack Runs │  │ Parts        │  │ → Clique Q.  │
│ → Crack Coins│  │ → Repair     │  │ → Great Disp │
│ → Crack Mkt  │  │ → ???        │  │   (stubs)    │
│              │  │              │  │              │
│ ENDLESS      │  │ ASPIRATIONAL │  │ SOCIAL       │
└──────────────┘  └──────────────┘  └──────────────┘
```

### Event Cycle Architecture

```
         ┌─────────── 6-WEEK CYCLE ───────────┐
         │                                      │
    Week 1-2         Week 3-4         Week 5-6
┌──────────────┐ ┌──────────────┐ ┌──────────────┐
│ LUCKY DRAW   │ │ TREASURE     │ │ ECHO         │
│ WEEK         │ │ WEEK         │ │ WEEK         │
│              │ │              │ │              │
│ Lucky Draws  │ │ Treasure     │ │ Echoes       │
│ → Lucky Wheel│ │ Tickets      │ │ → Ancestral  │
│ → Lucky Stall│ │ → Treasure   │ │   Depictor   │
│              │ │   Finder     │ │ → Echo Watch │
│ BURST/LUCK   │ │ → Treasure   │ │              │
│              │ │   Exchange   │ │ RITUAL/SLOW  │
│              │ │              │ │              │
│              │ │ PLANNING     │ │              │
└──────┬───────┘ └──────┬───────┘ └──────┬───────┘
       │                │                │
       └────────┬───────┴────────┬───────┘
                ▼                ▼
         ┌──────────────────────────┐
         │    FESTIVAL LEDGER       │
         │    (Meta-event track)    │
         │    Festival Marks →      │
         │    cross-cycle rewards   │
         └──────────────────────────┘
```

### Prestige / Rift Architecture

```
ACCOUNT MATURITY (Battle Stats + Heritage Stats + Strains + Confidants + Treasures)
         │
         ▼
┌──────────────────────────────────────────────┐
│              DEEPENING GATE                   │
│  Strata Seals + Buried Requirements          │
│  + broad build readiness check               │
└──────────────────┬───────────────────────────┘
                   │
    ┌──────────────┼──────────────┐
    ▼              ▼              ▼
1st Deepening  2nd Deepening  3rd Deepening  → 4th → 5th
The Stirring   The Record     The Fracture    Recall  Buried Court
    │              │              │
    ▼              ▼              ▼
Burrow grows   Archive opens  Ritual chamber  Route    Seat of
deeper layer   + clue depth   + Echo depth    systems  authority
                                               open     opens
         │
         ▼
┌──────────────────────────────────────────────┐
│              MEMORY SHIFT                     │
│  (Rift encounters — intensify per Deepening) │
│  Shift Wardens / Remembered Wardens          │
│  Alternate-timeline content                  │
└──────────────────────────────────────────────┘
         │
         ▼
┌──────────────────────────────────────────────┐
│              THE CRACK (Fissure)              │
│  Endless vertical progression                │
│  Bronze Shovels → Crack Runs → Crack Coins  │
│  Never-ending, always deeper                 │
└──────────────────────────────────────────────┘
```

---

## 3. Launch Scope vs Later Scope

### V1 LAUNCH SCOPE

| System | V1 Scope | Simplest launch-safe version |
|--------|----------|------------------------------|
| The Burrow | 4 gather buildings (Dewpond, Mushpatch, Rootmine, Tickworks) + Burrow Exchange + Burrow Post + Cloth Assembly + core UI surfaces | All buildings functional, upgradeable, idle-producing |
| Strata | 5 regions, linear unlock order, 1 Warden per region | Auto-Clash progression, route choice within region |
| Strains | 5 Strains visible, 1-2 meaningfully investable at launch | Strain Hall + Strain Loom functional, Strain Seeds farmable |
| Treasures | 1 starter set per Stratum, Pathwheel with basic socketing | Set/Polish/Strengthen loop functional |
| Clothes | Head (hats = hero slot) + 4-5 other slots, Cloth Assembly | Reinforce/Bind/Salvage loop functional |
| Confidants | 5 launch Confidants (1 per Stratum), Trust + Calling | Confidence Trail unlocks Burrowfolk types |
| Burrowfolk | 3 launch classes (Hands, Scouts, Bruisers) | Raise/Train/Command/Burrow Work loops |
| Clique | Create/Join, Clique Queue, Favor Marks, Clique Call | Social features functional, no Great Dispute |
| Events | 3-week rotating cycle (Lucky Draw / Treasure / Echo) | 1 side loop + 1 shop per week + Festival Ledger |
| The Crack | Entry with Bronze Shovels, Crack Coins, Crack Market | Endless floors, simple scaling |
| Deepening | Gate visible, 1st Deepening achievable | Strata Seals collectible, gate check functional |
| Wanderers | 18-30 at launch | 6 subtypes represented |
| Runaways | 10-18 at launch | Chase/reward/moral choice |
| Wild Ones | 8-15 at launch | Threat/disruption/pressure |
| Burrow Posts | 40-70 templates | 4 post channels functional |
| Monetization | Glowcap packs, cosmetic shop, starter bundle | No premium-only power items |

### NOT IN V1 (Later Scope)

| System | When | Reason for deferral |
|--------|------|---------------------|
| Great Dispute (species war) | v2+ | Needs critical mass of Clique engagement first |
| Memory Shift encounters | v1.5+ | Needs 1st Deepening completion data |
| The Ascender full loop | v2+ | Long-project aspirational system |
| Smoke Whispers hidden layer | v1.2+ | Hidden content can layer in after core is stable |
| Remembered Wardens | v2+ | Post-prestige boss variants |
| Shift Wardens | v2+ | Memory Shift combat content |
| Burrowfolk classes 4-7 | v1.1+ | Keepers, Chantfolk, Stewards, Fixers |
| Advanced Strain Loom | v1.2+ | Cross-strain experimentation |
| Lore Map full tree | v1.1+ | Intel/research progression |
| Field Cards + Findings Exchange | v1.2+ | Collection side system |
| Burrow Games (minigames) | v1.3+ | Engagement variety layer |
| Standing Shelf leaderboards | v1.1+ | Needs player base first |
| Turned Confidants | v1.2+ | Tamed-enemy mechanic |
| Premium Festival Ledger lane | v1.1+ | Monetization expansion |
| Cross-Strata travel pressure | v2+ | Needs multiple-region mastery |
| Hidden Easter egg layers | v1.1+ ongoing | Expandable discovery content |

---

## 4. Currency / Sink / Faucet Map

### Daily Loop Currencies

| Currency | Faucets | Sinks | Daily Target (Engaged) | Scarcity |
|----------|---------|-------|----------------------|----------|
| **Mooncaps** | Gather (all 4), Duties, Posts, Field Returns, Lucky Wheel, events | Expand, Set/Polish, Reinforce, Raise/Train, utility | 2,500-4,500 | LOW — always flowing |
| **Mushcaps** | Mushpatch, Field Returns, Duties, Wanderer gifts | Strata runs, branch routes, higher-yield pushes | 55-85 | MEDIUM — route choice pressure |
| **Glowcaps** | Milestones, Festival Ledger, Deepening, first-clears, IAP | Burrow Exchange premium, event support, cosmetics, catch-up | 3-8 (F2P) | HIGH — premium feel |

### Event Currencies (2-week hoard cycles)

| Currency | Hoard Window | F2P Target/Cycle | Spend Action | Tier 1 Threshold |
|----------|-------------|-----------------|--------------|------------------|
| **Lucky Draws** | 2 weeks | 60-80 | Lucky Wheel | 40-60 |
| **Treasure Tickets** | 2 weeks | 30-50 | Treasure Finder | 25-35 |
| **Echoes** | steady accumulation | 180-350/week | Ancestral Depictor | TBD |

### Specialization Currencies

| Currency | Primary Source | Primary Sink | Pressure Type |
|----------|---------------|-------------|---------------|
| **Strain Seeds** | Strata farming | Strain Hall / Loom | Build identity |
| **Treasure Shards** | Strata, events | Polish/Strengthen/Attune | Collection targeting |
| **Polishes** | Strata, events | Treasure upgrades | Upgrade pacing |
| **Clothes materials** | Strata, Rootmine | Reinforce/Bind | Combat readiness |

### Side-Lane / Social Currencies

| Currency | Primary Source | Primary Sink | Design Intent |
|----------|---------------|-------------|---------------|
| **Crack Coins** | Crack progression | Crack Market | Persistent side growth |
| **Bronze Shovels** | Routine play, duties, events | Crack entry/progress | Crack access gating |
| **Favor Marks** | Clique contribution | Clique rewards, social identity | Social throughput |

### Meta / Milestone Currencies

| Currency | Primary Source | Primary Sink | Scarcity Level |
|----------|---------------|-------------|----------------|
| **Strata Seals** | Deepening milestones, Warden clears, Buried Requirements | Deepening gates, meta-system | VERY HIGH |
| **Elder Books** | High event tiers, rare discovery, ritual milestones | Special ritual/summon layers | VERY HIGH |
| **Echo Shards** | Echo Week scoring | Echo Watch shop, ritual progression | HIGH (event-gated) |
| **Festival Marks** | All 3 weekly events | Festival Ledger track progress | MEDIUM-HIGH |
| **Ascender Parts** | Rare drops, milestones | The Ascender repair | VERY HIGH (aspirational) |

### Conversion Rules (Launch)

| Allowed | Not Allowed |
|---------|-------------|
| Low-tier materials convert upward at poor efficiency | Direct Mooncaps ↔ Glowcaps |
| Stale event leftovers → generic support value | Premium → everything bypass |
| Excess common Treasure materials → low-value utility | Free conversion between event currencies |
| Burrow Exchange/Crack Market relief valves | Unlimited old junk → top-value progression |

---

## 5. Home Screen Module Map

### Visible at Minute 1

| Module | Function | V1 Priority |
|--------|----------|-------------|
| The Burrow (main view) | Home base, gather buildings, idle output | CRITICAL |
| Dewpond | Mooncap gathering | CRITICAL |
| Mushpatch | Mushcap gathering | CRITICAL |
| Strata Gate | Region travel selector | CRITICAL |
| Pack (inventory) | Items and equipment | CRITICAL |
| Daily Duties | 3 trackable daily tasks | CRITICAL |

### Unlocked Day 1 (progressive reveal)

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Cloth Assembly | First Clothes drop | HIGH |
| Rootmine | Burrow level 2 | HIGH |
| Burrow Post | First post received | HIGH |
| Provision Board | First Expand action | HIGH |
| Burrow Exchange | Complete tutorial duties | MEDIUM |

### Unlocked Day 2-3

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Tickworks | Burrow level 3 or Strata milestone | HIGH |
| Vault of Treasures | First Treasure obtained | HIGH |
| Strain Hall | Loamwake mid-progress | MEDIUM |
| Burrow Box (terminal) | Tutorial completion | MEDIUM |
| Standing Shelf | First Standing gain | LOW |

### Unlocked by Loamwake Clear

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Pathwheel | First Treasure Set | HIGH |
| Clique (create/join) | Loamwake clear | MEDIUM |
| Lucky Wheel (event) | First event cycle start | HIGH |
| Burrow Nursery | First Burrowfolk unlock | MEDIUM |

### Unlocked by Ledgerhollow Progress

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Lore Ledger | Ledgerhollow entry | MEDIUM |
| Treasure Finder (event) | Treasure Week start | HIGH |
| Salvage Shed | Enough Clothes for salvage | MEDIUM |

### Unlocked by Memory Fen+

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Ancestral Depictor | Memory Fen entry | MEDIUM |
| Strain Loom | Strain Hall level 3+ | MEDIUM |
| The Crack | Mid-game milestone | HIGH |
| Crack Market | First Crack Coins earned | MEDIUM |

### Unlocked Late / Post-Deepening

| Module | Unlock Trigger | V1 Priority |
|--------|---------------|-------------|
| Deepening Gate | Buried Court approach | HIGH |
| Burrow Monitor | Clique membership + mid-game | LOW |
| The Ascender | Late milestone (stub only v1) | LOW |

---

## 6. Region Progression Plan

### Linear Unlock Order with Pacing Targets

| # | Stratum | Dominant Strain | Deepening Tie | Time Target (Engaged) | Key Unlock |
|---|---------|----------------|---------------|----------------------|------------|
| 1 | **Loamwake** | Freeroot | 1st: The Stirring | Days 1-7 | Tutorial, first Confidant, first Burrowfolk, Clique |
| 2 | **Ledgerhollow** | Inkroot | 2nd: The Record | Weeks 1-2 total | Lore Ledger, archive systems, Treasure depth |
| 3 | **Memory Fen** | Hushline | 3rd: The Fracture | Weeks 2-4 total | Ancestral Depictor, Echo economy, ritual layer |
| 4 | **Wayworks** | Brassroot | 4th: The Recall | Weeks 4-7 total | Route optimization, infrastructure systems |
| 5 | **The Buried Court** | Crownroot | 5th: The Buried Court | Weeks 6-10 total | Pre-Deepening convergence, political truth |

### Per-Stratum Content Targets (Launch)

| Content Type | Per Stratum |
|-------------|-------------|
| Warden (boss) | 1 main + 1-2 Keepers/Claimants |
| Confidant | 1 (unique role archetype) |
| Buried Clue chain | 1 |
| Wanderer pool | 4-6 regionally weighted |
| Runaway leads | 2-3 |
| Wild Ones presence | 1-2 crews |
| Burrow Post templates | 8-14 |
| Strata Trait | 1 per clear (only 1 active at a time) |

### Warden Difficulty Curve

| Tier | Boss Type | Encounter Rate | Reward Weight |
|------|-----------|---------------|---------------|
| Minor | Keepers | Every 2-3 zones within Stratum | Low |
| Mid | Claimants | Mid-Stratum gates | Medium |
| Major | Wardens | Stratum boss | High + Strata Trait unlock |
| Hidden | Remembered Wardens | Post-Deepening only | Very High (v2) |
| Rift | Shift Wardens | Memory Shift only | Very High (v2) |

---

## 7. Event Math Targets

### 3-Week Cycle Calendar Model

```
Week 01-02: Lucky Draw Week     ← 2 weeks prep, 1 week active
Week 03-04: Treasure Week       ← 2 weeks prep, 1 week active  
Week 05-06: Echo Week           ← 2 weeks prep, 1 week active
            Festival Ledger     ← runs across all 6 weeks
            
REPEAT
```

### Beginner Event (First 2 weeks after tutorial)

| Parameter | Target |
|-----------|--------|
| Duration | 14 days |
| Purpose | Teach event rhythm, reward discipline, introduce hoarding |
| Rewards | Starter Treasures, Clothes, Mushcap bundles, tutorial completion bonuses |
| Difficulty | Very low — every active player completes Tier 1 |
| Special | Guided pulls, guaranteed good outcomes, Lucky Stall tutorial |

### Hoard Event (Follows beginner event, 2 weeks)

| Parameter | Target |
|-----------|--------|
| Duration | 14 days |
| Purpose | Teach saving behavior, prep-window pressure |
| Rewards | Account-shaping Treasures, Strain Seeds, early Confidant items |
| Difficulty | Moderate — disciplined savers complete Tier 2 |
| Special | Savings tracker UI, milestone preview, temptation offers |

### Steady-State Weekly Event Targets

| Metric | Lucky Draw Week | Treasure Week | Echo Week |
|--------|----------------|---------------|-----------|
| F2P Tier 1 | 40-60 Lucky Draws | 25-35 Treasure Tickets | Steady Echo accumulation |
| F2P Tier 2 | 70-100 Lucky Draws | 40-55 Treasure Tickets | Regular ritual + prep |
| Spender Tier 3+ | 120+ | 70+ | Heavy ritual investment |
| Side loop | Lucky Stall (4 rotating items) | Treasure Exchange (targeted correction) | Echo Watch (ritual scoring) |
| Festival Marks/week | ~30-50 | ~25-40 | ~20-35 |
| Emotional peak | Pull excitement burst | Account planning satisfaction | Ritual payoff, eerie calm |

### Festival Ledger Cycle Targets

| Lane | Completion Target | Reward Type |
|------|------------------|-------------|
| Free lane | Reachable by Daily Engaged across full cycle | Universal materials, cosmetics |
| Stretch lane | Reachable by Disciplined Saver | Targeted progression items |
| Capstone | Reachable by committed + light spend | Identity/prestige rewards |

---

## 8. System Dependencies and Choke Points

### Critical Path Dependencies

```
Economy Model (math) ──→ Data Schema ──→ Backend Architecture
       │                      │                    │
       ▼                      ▼                    ▼
  Event Tuning          MVP Spec ──────→ Prototype Build
       │                      │                    │
       ▼                      ▼                    ▼
  Balance Sheet        Unlock Flow ──→ UI Implementation
                              │
                              ▼
                     Content Tables ──→ Content Production
```

### Choke Points (systems that block multiple others)

| Choke Point | What It Blocks | Severity |
|-------------|---------------|----------|
| **Economy math finalization** | Event tuning, Deepening pacing, all sink/faucet numbers, IAP pricing | CRITICAL |
| **Data schema** | Backend, save system, content tables, prototype, all implementation | CRITICAL |
| **Backend architecture decision** | Save sync, event scheduling, anti-cheat, remote config, all server work | HIGH |
| **Strata auto-clash balance** | Core fun proof, retention test, difficulty curve | HIGH |
| **Confidant unlock triggers** | Burrowfolk availability, story pacing, build variety timing | MEDIUM |
| **Burrow Post generation rules** | Content pipeline velocity, scalability proof | MEDIUM |
| **Clique MVP feature set** | Social retention, Favor Marks economy viability | MEDIUM |

### System Dependency Matrix

| System | Hard Dependencies | Soft Dependencies |
|--------|------------------|-------------------|
| Strata progression | Battle Stats, Mushcaps, Clothes | Treasures, Pathwheel |
| Treasures | Strata drops, Treasure Shards | Events, Pathwheel |
| Clothes | Strata drops, Cloth Assembly, Mooncaps | Events |
| Confidants | Strata progression, Clue Triggers | Burrow Posts, Strains |
| Burrowfolk | Confidant unlocks | Burrow Nursery, Clique |
| Clique | Player base (min count), Favor Marks | Burrowfolk, Standing |
| Events | Economy model, event currencies | Wanderers, Posts |
| The Crack | Bronze Shovels, account readiness | Main progression |
| Deepening | ALL major systems (broad readiness check) | Buried Requirements |
| Memory Shift | Deepening (1st complete) | Smoke Whispers |

---

## 9. Risk Register

| # | Risk | Impact | Likelihood | Mitigation |
|---|------|--------|-----------|------------|
| R1 | **Prototype scope creep** — trying to build too many systems for first playable | CRITICAL | HIGH | Lock MVP to Loamwake + 1 Confidant + 1 event cycle. Prove core fun first. |
| R2 | **Economy drift** — too many currencies without tested math | HIGH | HIGH | Finalize economy spreadsheet before any prototype currency implementation. |
| R3 | **Backend complexity underestimated** — live-service requirements hit early | HIGH | MEDIUM | Choose managed backend (Firebase/PlayFab) over custom for prototype. Defer custom to v1.1+. |
| R4 | **Onboarding collapse** — too many systems visible too early | HIGH | MEDIUM | Hard-lock unlock flow. Nothing beyond Strata Gate + Gather + Duties visible at minute 1. |
| R5 | **Content pipeline inconsistency** — docs drift from schema | MEDIUM | HIGH | Create content table templates before any content production. Enforce schema. |
| R6 | **Naming fatigue** — too many unique terms overwhelm new players | MEDIUM | MEDIUM | Limit visible terminology in first 30 minutes. Use tooltips, not walls of text. |
| R7 | **Event pacing too aggressive** — players feel behind if they miss a cycle | MEDIUM | MEDIUM | Festival Ledger catch-up lane. No FOMO-critical exclusive power items. |
| R8 | **Clique dead on arrival** — not enough players or reasons to engage socially | MEDIUM | HIGH | Launch Clique with Clique Queue (co-op goals) not just chat. Ensure Favor Marks have day-1 utility. |
| R9 | **The Crack becomes mandatory homework** — side lane feels like second job | MEDIUM | MEDIUM | Cap daily Bronze Shovel income. Keep Crack Coins rewards supplementary, not essential. |
| R10 | **Single-developer bottleneck** — one person building everything | CRITICAL | depends on team | Phase the build order. Prove fun with smallest possible slice. |
| R11 | **Super Snail comparison too close** — IP/design overlap perception | MEDIUM | LOW | Lean hard into gnome identity, political satire, and post-apocalyptic humor. Differentiate tone. |
| R12 | **Hats not differentiated** — if hats are just another Clothes slot, the "major equipment" promise falls flat | LOW | MEDIUM | Give hats unique visual prominence, special upgrade path, or passive bonuses that other slots lack. |

---

## 10. Recommended Build Order

### Phase 2A — Production Foundation (CURRENT PRIORITY)

**Specialist assignment: Systems Designer / Technical Designer**  
**Duration target: 2-3 weeks**  
**Deliverables:**

| # | Deliverable | Purpose | Depends On |
|---|------------|---------|------------|
| 1 | `economy_model_v1_sheet.csv` | Actual numbers, not just bands | economy_model_v1.md |
| 2 | `data_schema_v1.md` | Game object model for all systems | Economy sheet |
| 3 | `backend_architecture_options.md` | Stack decision with tradeoffs | Schema, APK reference data |
| 4 | `core_loop_mvp_spec.md` | Exact features in/out of first playable | Economy + Schema |
| 5 | `unlock_flow_and_ui_map.md` | Minute-by-minute onboarding plan | MVP spec |
| 6 | `content_table_templates.md` | Spreadsheet-ready templates for all content types | Schema |

### Phase 2B — Prototype Readiness

**Specialist assignment: Game Designer + Backend Dev**  
**Duration target: 3-4 weeks**  
**Deliverables:**

| # | Deliverable | Purpose |
|---|------------|---------|
| 7 | `loamwake_mvp_content_sheet.md` | All content for first region |
| 8 | `first_confidant_chain.md` | Complete Freeroot Pathfinder trust chain |
| 9 | `first_wanderer_pool.md` | 6 Wanderers with full content bricks |
| 10 | `lucky_draw_week_mvp.md` | First event cycle fully specified |
| 11 | `tutorial_and_onboarding_flow.md` | First 10 minutes scripted |
| 12 | `save_state_and_profile_flow.md` | Save/load/sync spec |
| 13 | `iap_catalog_v1.md` | Monetization items, pricing, bundles |

### Phase 3 — First Playable (Prototype)

**Specialist assignment: Unity Developer + UI Artist**  
**Duration target: 6-10 weeks**  

| Sprint | Focus | Proof Target |
|--------|-------|-------------|
| Sprint 1 | Burrow + Gather loop + idle output | Does the home base feel alive? |
| Sprint 2 | Strata Gate + Loamwake + Auto-Clash | Does exploration feel rewarding? |
| Sprint 3 | Clothes + Treasures + basic upgrade loops | Do power spikes feel good? |
| Sprint 4 | Burrow Posts + Daily Duties + first Confidant | Does the world feel socially alive? |
| Sprint 5 | Lucky Draw Week (1 cycle) | Does the event create excitement? |
| Sprint 6 | Clique stub + The Crack stub + polish | Does the game loop hold for 7 days? |

### Phase 4 — Content Production

**Specialist assignment: Content Designer / Writer**  
**Duration target: ongoing**  

| Priority | Content |
|----------|---------|
| P0 | Remaining 4 Strata content (Ledgerhollow → Buried Court) |
| P1 | Remaining 4 Confidants |
| P2 | Full Wanderer/Runaway/Wild Ones launch pool |
| P3 | Burrow Post library to 70 templates |
| P4 | Treasure and Clothes content tables |
| P5 | Deepening Gate content and requirements |

### Phase 5 — Live-Service Prep

| Deliverable | Purpose |
|------------|---------|
| Remote config setup | Event scheduling without client updates |
| Analytics + telemetry | Retention, funnel, economy monitoring |
| Season 1 content pack | First post-launch season ready |
| Community tools | Clique management, social features |
| Balance monitoring dashboard | Currency inflation/deflation tracking |

---

## 11. Unresolved Items Register

| # | Item | Owner | Priority | Notes |
|---|------|-------|----------|-------|
| U1 | Hat system design — standalone vs Clothes sub-slot | Game Designer | HIGH | Blocks Clothes implementation |
| U2 | Bronze Shovels rename consideration | Naming | LOW | Suggestion: Crack Picks or Digger's Marks |
| U3 | Exact Mooncap curve for Burrow building upgrades | Economy Designer | HIGH | Prototype-blocking |
| U4 | Treasure vs Clothes power split ratio | Economy Designer | HIGH | How much power from each? |
| U5 | Strain line progression speed vs Strata completion | Economy Designer | MEDIUM | |
| U6 | Bronze Shovels appearance rate tuning | Economy Designer | MEDIUM | Crack feeling mandatory vs optional |
| U7 | Glowcap faucet — Tickworks trickle vs milestone only | Economy Designer | MEDIUM | |
| U8 | Elder Books per cycle for F2P | Economy Designer | MEDIUM | |
| U9 | Favor Marks minimum viable output | Economy Designer | MEDIUM | Clique feeling worthwhile |
| U10 | Strata Seals per Deepening band | Economy Designer | HIGH | Deepening pacing |
| U11 | Currency hard caps vs soft caps vs no caps | Economy Designer | MEDIUM | |
| U12 | Lucky Wheel / Treasure Finder pity system ranges | Economy Designer | HIGH | Gacha compliance |
| U13 | IAP catalog and pricing structure | Monetization | HIGH | Revenue model |
| U14 | Backend stack selection | Tech Lead | CRITICAL | Firebase vs PlayFab vs UGS vs hybrid |
| U15 | Anti-cheat approach for economy-sensitive actions | Tech Lead | HIGH | |
| U16 | Tutorial script — first 10 minutes | Game Designer | HIGH | Retention-critical |
| U17 | Pity system legal compliance by region | Legal/Compliance | HIGH | Gacha laws vary by country |
| U18 | Music/audio direction | Audio Lead | MEDIUM | |
| U19 | Art style guide | Art Lead | HIGH | Blocks all visual production |
| U20 | Memory Shift encounter format | Game Designer | LOW | Deferred to v1.5+ |

---

## 12. Document Versioning Protocol

### Version Format
`MAJOR.MINOR.PATCH`

- **MAJOR** = phase transition (0.x = pre-production, 1.x = production, 2.x = live)
- **MINOR** = significant system change or new doc addition
- **PATCH** = corrections, clarifications, formatting

### Changelog Requirements
Every doc change must include:
- date
- author
- what changed
- why

### Build Versioning (once prototype exists)
`BUILD-YYYY.MM.DD-NNN`

Example: `BUILD-2026.05.15-001`

### Document Status Tags
- `DRAFT` — working document, not approved
- `REVIEW` — ready for owner review
- `LOCKED` — approved, changes require formal revision
- `SUPERSEDED` — replaced by newer version
- `ARCHIVED` — historical reference only

---

## 13. GitHub Repository Structure

```
gnome-survivor/
├── README.md
├── CHANGELOG.md
├── .github/
│   └── ISSUE_TEMPLATE/
│       ├── design_decision.md
│       ├── bug_report.md
│       └── content_request.md
│
├── docs/
│   ├── 00_index/
│   │   ├── master_index.md
│   │   ├── project_roadmap.md
│   │   ├── implementation_planning_pack.md
│   │   └── changelog.md
│   │
│   ├── 01_core/
│   │   ├── master_glossary.md
│   │   ├── design_principles.md
│   │   └── pillars_and_tone.md
│   │
│   ├── 02_world/
│   │   ├── phase1_content_bible.md
│   │   ├── deepening_arc.md
│   │   └── memory_shift.md
│   │
│   ├── 03_factions/
│   │   ├── strains_bible.md
│   │   ├── clique_confidants_bible.md
│   │   ├── wanderers_runaways_wild_ones.md
│   │   └── burrowfolk_classes.md
│   │
│   ├── 04_systems/
│   │   ├── economy_model_v1.md
│   │   ├── economy_and_progression_templates.md
│   │   ├── system_interactions_matrix.md
│   │   ├── data_schema_v1.md          ← PHASE 2
│   │   ├── core_loop_mvp_spec.md      ← PHASE 2
│   │   └── rift_systems.md            ← PHASE 2
│   │
│   ├── 05_liveops/
│   │   ├── event_content_templates.md
│   │   ├── event_examples.md
│   │   ├── seasonal_expansion_framework.md
│   │   ├── season_briefs_pack.md
│   │   ├── content_scaling_rules.md
│   │   └── monetization_spec.md       ← PHASE 2
│   │
│   ├── 06_content/
│   │   ├── duties_and_encounters.md
│   │   ├── burrow_posts.md
│   │   ├── burrow_posts_examples.md
│   │   └── content_table_templates.md ← PHASE 2
│   │
│   ├── 07_ui/
│   │   ├── unlock_flow_and_ui_map.md  ← PHASE 2
│   │   ├── tutorial_and_onboarding.md ← PHASE 2
│   │   └── home_screen_module_map.md
│   │
│   ├── 08_production/
│   │   ├── build_order.md
│   │   ├── risk_register.md
│   │   ├── unresolved_items.md
│   │   ├── backend_architecture.md    ← PHASE 2
│   │   └── specialist_prompts/
│   │       ├── economy_specialist.md
│   │       ├── schema_specialist.md
│   │       └── mvp_specialist.md
│   │
│   └── 99_archive/
│       ├── next_steps_report.md
│       ├── chat_coverage_report.md
│       └── super_snail_reference/
│           ├── file_info.txt
│           ├── aapt_badging.txt
│           ├── aapt_permissions.txt
│           ├── unity_libs.txt
│           ├── il2cpp_libs.txt
│           └── [other APK analysis files]
│
├── data/
│   ├── economy/
│   │   └── economy_model_v1_sheet.csv  ← PHASE 2
│   ├── content/
│   │   ├── wanderers.csv
│   │   ├── runaways.csv
│   │   ├── wild_ones.csv
│   │   ├── confidants.csv
│   │   ├── burrowfolk.csv
│   │   ├── treasures.csv
│   │   ├── clothes.csv
│   │   ├── wardens.csv
│   │   ├── burrow_posts.csv
│   │   └── duties.csv
│   └── balance/
│       └── [tuning sheets]
│
├── src/                               ← PHASE 3 (prototype code)
│   └── [engine-specific structure]
│
└── art/                               ← when art pipeline starts
    ├── concepts/
    ├── ui/
    └── sprites/
```

---

## 14. Specialist Assignment: Phase 2A Completion

### Assigned Specialist: Systems Designer

**Mission:** Complete Phase 2A Production Foundation (6 deliverables)

**Work order:**

1. **economy_model_v1_sheet.csv** — Convert economy_model_v1.md target bands into actual prototype numbers. Resolve open tuning questions 1-10. Create daily/weekly/monthly output tables for all 4 player segments.

2. **data_schema_v1.md** — Define game object model. Cover: account, gnome_survivor, burrow_state, strata_progress, strain_progress, confidant, confidant_trust, burrowfolk_unit, treasure, clothes_item, clique_state, post_state, encounter_state, event_state, deepening_state, memory_shift_state, crack_state. Define owned vs equipped vs unlocked. Define permanent vs repeatable. Define global vs per-Stratum.

3. **backend_architecture_options.md** — Evaluate Firebase Auth + custom, PlayFab, Unity Gaming Services, hybrid. Reference Super Snail APK analysis (Unity + IL2CPP confirmed). Cover: cost, complexity, anti-cheat, remote config, event scheduling, save sync. Select preferred stack.

4. **core_loop_mvp_spec.md** — Define exact in/out for first playable. Loamwake only. 1 Confidant. 1 Burrowfolk class. 1 Warden. 1 event cycle. 1 Crack stub. Define first 10 min, first 60 min, first day, first clear. Define proof-of-fun criteria.

5. **unlock_flow_and_ui_map.md** — Minute-1 visible modules. Day-1 unlock sequence. Day-2-3 unlock logic. Loamwake-clear unlocks. When each system appears. Red-dot rules.

6. **content_table_templates.md** — Spreadsheet-ready templates for: Strata zones, Wardens, Buried Clues, Wanderers, Runaways, Wild Ones, Confidants, Burrowfolk, Treasures, Clothes, event ladders, Festival Ledger, Burrow Posts, Duties. Each with: content_id, category, unlock_condition, reward_identity, escalation_rule, recurrence_rule, strain_tag, stratum_tag, event_tag.

**Acceptance criteria:** Every document passes the test: "Could a developer start building from this without asking clarifying questions?"

---

## 15. Next Phase Preview: Phase 2B

Once Phase 2A is complete, Phase 2B (Prototype Readiness) begins. That phase produces the actual content needed to build the first playable slice, including the Loamwake content sheet, first Confidant chain, first Wanderer pool, first event cycle spec, tutorial script, and save system spec.

The gap between Phase 2A and Phase 2B should be minimal — ideally the same sprint, with Phase 2A outputs feeding directly into Phase 2B content production.

---

*End of Implementation Planning Pack v0.1.0*
