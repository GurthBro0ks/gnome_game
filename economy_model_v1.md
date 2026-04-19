# Gnome Survivor — Economy Model v1

## Status
**Draft:** v1  
**Purpose:** first-pass production economy model  
**Scope:** launch-era assumptions for prototype and balancing alignment  
**Use with:** `master_glossary.md`, `phase1_content_bible.md`, `strains_bible.md`, `deepening_arc.md`, `economy_and_progression_templates.md`, `system_interactions_matrix.md`

---

## 1. Purpose

This file converts the current design stack into a **playable balancing model**.

It is not the final economy.
It is the first pass that gives the project concrete answers to questions like:
- What does a normal day produce?
- What should players spend immediately versus hoard?
- What is scarce by design?
- What does each currency actually do?
- How should the three weekly event lanes feel for free players and light spenders?
- How long should it take to reach each Deepening gate?

This file is written to support:
- prototype implementation
- first balancing spreadsheet setup
- content-table construction
- backend/state modeling
- event tuning

---

## 2. Economy philosophy

### 2.1 Design goals
The launch economy should create these feelings:
- **steady daily progress** from The Burrow
- **meaningful route choice** in Strata
- **clear preparation pressure** for weekly events
- **many permanent growth lanes** without making all lanes mandatory every day
- **old resources staying useful** after new systems unlock
- **Deepening feeling like earned escalation, not punishment**

### 2.2 Economy shape
The economy should be built around five simultaneous layers:
1. **daily loop currencies** — resources spent almost every session
2. **specialization currencies** — materials that shape builds and targeting
3. **event currencies** — hoarded across cycles and spent in bursts
4. **social/endless currencies** — slower side-lane resources
5. **meta currencies** — Deepening and long-horizon account growth

### 2.3 Core anti-failure rules
- No currency should exist without a clear job.
- No major system should depend on a currency with undefined faucet cadence.
- F2P players must be able to complete **at least the first meaningful event tier** with disciplined hoarding.
- Light spenders should accelerate progress, not bypass structure.
- Launch should favor **clarity over density**.

---

## 3. Player segment assumptions

These are not marketing personas. They are balancing lenses.

| Segment | Sessions | Event prep quality | Spend profile | Design use |
|---|---:|---|---|---|
| Casual | 1 short login/day | poor to moderate | none | minimum survivability check |
| Daily Engaged | 2 sessions/day | moderate | none | baseline expected player |
| Disciplined Saver | 2–3 sessions/day | high | none | F2P event target model |
| Light Spender | 2–3 sessions/day | high | small recurring/event spend | soft monetization target |

### 3.1 Session assumptions
- **Short session:** 5–10 minutes; gather, quick upgrades, one or two Strata decisions
- **Standard session:** 15–25 minutes; gather, expedition planning, one feature interaction, one post resolution
- **Extended session:** 30–45 minutes; event play, Crack push, preparation, reshaping build choices

### 3.2 Balancing anchor
Most launch pacing should be tuned around the **Daily Engaged** and **Disciplined Saver** segments.
Casuals should progress more slowly but not feel hard-locked out of core systems.

---

## 4. Currency catalog

### 4.1 Core launch currencies

| Currency | Role | Main feeling | Primary scarcity style |
|---|---|---|---|
| Mooncaps | main soft currency | constant progress | broad spend pressure |
| Glowcaps | premium earnable currency | flexibility and temptation | low faucet, high optionality |
| Mushcaps | exploration stamina | route planning | refill cadence |
| Lucky Draws | Lucky Draw Week pull currency | burst excitement | hoard pressure |
| Treasure Tickets | Treasure Week targeting currency | deliberate targeting | lower supply than draws |
| Echoes | shrine/offering base resource | ritual commitment | medium slow-burn |
| Echo Shards | Echo Week reward shop currency | event payout satisfaction | gated by scoring |
| Elder Books | rare ritual/key item | milestone gravitas | high rarity |
| Crack Coins | Crack lane currency | persistent side growth | endless-lane pace |
| Favor Marks | Clique currency | belonging and contribution | social throughput |
| Strata Seals | Deepening/meta currency | long-horizon power | major milestone scarcity |

### 4.2 Upgrade material families

| Material | Role | Primary sinks |
|---|---|---|
| Treasure Shards | treasure-specific progression | Polish / Strengthen / Attune |
| Polishes | treasure upgrade reagent | Polish / Strengthen |
| Strain Seeds | strain unlock/growth | Strains / Strain Loom |
| Clothes materials | clothes progression | Reinforce / Bind |
| Ascender Parts | long project repair | The Ascender |
| Bronze Shovels | Crack run access | The Crack floors / runs |

---

## 5. Currency roles, faucets, and sinks

### 5.1 Mooncaps
**Role:** main soft currency  
**Primary faucets:**
- Gather from Dewpond / Mushpatch / Rootmine / Tickworks
- Field Returns
- Duties
- Burrow Posts and simple encounters
- Lucky Wheel / Treasure Finder side rewards
- low-tier event ladders

**Primary sinks:**
- Expand costs in The Burrow
- Treasure Set / Polish costs
- Clothes Reinforce costs
- minor Burrowfolk Train / Raise costs
- utility unlocks and convenience spending

**Target feeling:** always useful, never exciting by itself, never worthless.

**Design note:** Mooncaps should be the resource players spend several times per session.

---

### 5.2 Glowcaps
**Role:** premium earnable currency  
**Primary faucets:**
- milestone rewards
- Festival Ledger
- Deepening milestones
- first-clear achievements
- limited event ladders
- direct purchase

**Primary sinks:**
- Burrow Exchange premium entries
- event support bundles
- convenience/catch-up offers
- cosmetics or vanity lanes later

**Target feeling:** optional acceleration and strategic flexibility.

**Design note:** Glowcaps should never be required for basic system viability.

---

### 5.3 Mushcaps
**Role:** exploration fuel  
**Primary faucets:**
- Mushpatch
- Field Returns
- Duties
- Strata rewards
- Wanderer gifts/deals

**Primary sinks:**
- Strata runs
- optional branch routes
- higher-yield or rarer-field pushes

**Target feeling:** “I need to choose where this session’s expedition energy goes.”

**Design note:** Mushcaps should create route choice, not pure delay.

---

### 5.4 Lucky Draws
**Role:** Lucky Draw Week pull currency  
**Primary faucets:**
- two-week saving window
- Duties
- Burrow Posts
- event prep rewards
- Festival Ledger
- minor shop offers
- light spender pack support

**Primary sinks:**
- Lucky Wheel pulls
- scoring tiers during Lucky Draw Week

**Target feeling:** fun burst spending after planned restraint.

---

### 5.5 Treasure Tickets
**Role:** Treasure Week targeting currency  
**Primary faucets:**
- two-week saving window
- Duties
- Treasure prep rewards
- Buried Clues / Strata rewards
- Festival Ledger
- limited Burrow Exchange offers

**Primary sinks:**
- Treasure Finder targeting pulls
- scoring tiers during Treasure Week

**Target feeling:** precision and long-term account planning.

---

### 5.6 Echoes / Echo Shards / Elder Books
**Role:** ritual family  
**Primary faucets:**
- Echoes: ritual generation, posts, exploration, event prep
- Echo Shards: Echo Week scoring ladders and shop payouts
- Elder Books: milestone ritual rewards, high event tiers, rare discovery loops

**Primary sinks:**
- Ancestral Depictor offerings
- ritual progression
- Echo Week score conversion
- special summon/encounter layers

**Target feeling:** eerie ritual preparation and delayed payoff.

---

### 5.7 Crack Coins / Bronze Shovels
**Role:** endless side-lane economy  
**Primary faucets:**
- Bronze Shovels from routine play, duties, posts, and events
- Crack Coins from Crack progression and Crack Market

**Primary sinks:**
- Bronze Shovels consumed for Crack entry/progress
- Crack Coins spent in Crack Market

**Target feeling:** a persistent side job that always matters but never dominates the whole account.

---

### 5.8 Favor Marks
**Role:** Clique currency  
**Primary faucets:**
- Clique Queue participation
- contribution actions
- Clique milestone rewards
- later Great Dispute outputs

**Primary sinks:**
- Clique support purchases
- contribution unlocks
- social progression rewards

**Target feeling:** “my social group matters and pays off over time.”

---

### 5.9 Strata Seals
**Role:** Deepening/meta resource  
**Primary faucets:**
- Deepening milestone completion
- hidden/post-Deepening systems
- Buried Requirement completion

**Primary sinks:**
- Deepening upgrades
- Strata Trait extension
- later Memory Shift and post-launch layers

**Target feeling:** rare, meaningful, long-horizon advancement.

---

## 6. First-pass pacing targets

These are intentionally approximate prototype targets, not live balance certainties.

### 6.1 Daily baseline output targets
**Daily Engaged player target**

| Resource | Daily target | Notes |
|---|---:|---|
| Mooncaps | 2,500–4,500 | depends on Burrow level and one proper Strata session |
| Mushcaps | 120–180 | enough for 2–4 meaningful route decisions |
| Glowcaps | 5–15 | mostly from milestones, not routine |
| Lucky Draws | 2–4 outside active week | prep trickle only |
| Treasure Tickets | 1–3 outside active week | rarer than Lucky Draws |
| Echoes | 20–50 | baseline ritual progress |
| Bronze Shovels | 0.5–1.5 effective/day | some days zero, some days 2 |
| Favor Marks | 20–60 | depends on Clique participation |

### 6.2 Weekly baseline output targets
**Disciplined Saver target, outside direct event week spending**

| Resource | 1 week target | 2 week target | Design use |
|---|---:|---:|---|
| Lucky Draws | 30–40 | 60–80 | matches launch event philosophy |
| Treasure Tickets | 15–25 | 30–50 | lower than Lucky Draws by design |
| Echoes | 250–450 | 500–900 | fuels Echo Week prep |
| Bronze Shovels | 7–10 | 14–20 | enough to keep Crack alive without dominance |
| Glowcaps | 40–80 | 80–160 | slow but meaningful premium earn rate |

### 6.3 Event access rule
A disciplined F2P player saving over the intended prep window should reliably complete:
- **Lucky Draw Week:** first major tier, sometimes second tier
- **Treasure Week:** first major tier, with meaningful targeting choices
- **Echo Week:** first full scoring tier, sometimes second with strong prep

A light spender should reach:
- one extra meaningful tier or cleaner completion path
- better optional targeting or shop extraction
- **not** full auto-clear of all event tiers

---

## 7. Event math framework

### 7.1 Lucky Draw Week
**Intent:** most emotionally exciting week; widest burst-spend appeal.

**First-pass assumptions**
- 10 Lucky Draws = one standard multi-pull
- F2P disciplined prep window = **60–80 Lucky Draws / 2 weeks**
- first meaningful ladder tier should sit around **40–60 spend**
- second meaningful tier should sit around **70–100 spend**
- upper ladder tiers should clearly exceed F2P baseline

**Shop rule**
Lucky Stall should offer:
- one safe universal pickup
- one treasure-focused pickup
- one clothes/burrowfolk utility pickup
- one rotating “temptation” item

### 7.2 Treasure Week
**Intent:** lower-volume, higher-precision account shaping.

**First-pass assumptions**
- disciplined F2P prep window = **30–50 Treasure Tickets / 2 weeks**
- board/family targeting should feel stronger than Lucky Draw randomness
- first meaningful ladder tier should sit around **25–35 spend**
- second meaningful tier should sit around **40–55 spend**

**Shop rule**
Treasure Exchange should offer:
- targeted progression tools
- treasure family acceleration
- low-volume high-value options

### 7.3 Echo Week
**Intent:** slower ritual week with clear planning payoff.

**First-pass assumptions**
- Echoes should accumulate more steadily than burst currencies
- first meaningful ladder tier should be reachable through disciplined prep without premium spend
- Echo Watch should convert event scoring into **Echo Shards** at a satisfying rate
- Elder Books should remain scarce and prestigious

**Tuning principle**
Echo Week should feel slower and stranger than Lucky Draw Week, not weaker.

### 7.4 Festival Ledger
**Intent:** steady meta progress across all three weekly cycles.

**First-pass assumptions**
- every week contributes meaningful Festival Marks
- free lane should always feel worthwhile
- paid/light-spender support lane can sharpen choices but should not invalidate week-specific ladders

---

## 8. Launch scarcity map

### 8.1 Always available, always useful
- Mooncaps
- Mushcaps
- low-volume Clothes materials
- minor Treasure Shards

### 8.2 Moderately scarce, planning-sensitive
- Treasure Tickets
- Echoes
- Bronze Shovels
- Favor Marks
- Strain Seeds

### 8.3 Rare and identity-defining
- Glowcaps
- Elder Books
- Strata Seals
- high-tier Treasure Shards
- Ascender Parts

### 8.4 Scarcity intention
| Resource | Why it must stay scarce |
|---|---|
| Glowcaps | protects monetization and meaningful choice |
| Elder Books | keeps ritual capstones special |
| Strata Seals | preserves Deepening gravity |
| Ascender Parts | supports long-project aspiration |
| high-tier Strain Seeds | prevents too-fast strain flattening |

---

## 9. System-by-system pressure points

### Burrow
Pressure point: Mooncaps vs Expand vs Treasure Set costs  
Goal: always one obvious small upgrade, never enough for everything.

### Strata
Pressure point: Mushcaps and route choice  
Goal: create meaningful “where do I push next?” decisions.

### Treasures
Pressure point: Treasure Shards and Polishes  
Goal: collection feels broad, upgrade depth feels selective.

### Clothes
Pressure point: reinforcement pacing and bind costs  
Goal: power spikes matter but do not erase Treasure and Strain importance.

### Strains
Pressure point: Strain Seeds  
Goal: each line grows, but line identity remains distinct.

### Crack
Pressure point: Bronze Shovels  
Goal: enough activity to matter weekly, not enough to become mandatory grind.

### Clique
Pressure point: contribution and Favor Marks  
Goal: social lane feels rewarding without being oppressive.

### Deepening
Pressure point: Strata Seals plus broad build readiness  
Goal: Deepening feels earned through whole-account growth.

---

## 10. Conversion and exchange rules

### 10.1 Allowed at launch
- some low-tier resources can convert upward at poor efficiency
- stale event leftovers can partially convert into generic support value
- excess common Treasure materials may be sink-converted into low-value utility

### 10.2 Not allowed at launch
- direct Mooncaps → Glowcaps
- direct premium-to-everything bypass
- full free conversion between event currencies
- unlimited conversion from old low-value junk into top-value progression

### 10.3 Exchange philosophy
Burrow Exchange and Crack Market should create:
- relief valves for oversupply
- temptation choices
- partial catch-up
- weekly decision pressure

They should not erase scarcity.

---

## 11. First-pass Deepening pacing

### 11.1 Launch pacing target
The model below is deliberately broad because content depth will change final numbers.

| Stage | Target feel | Rough time target for Daily Engaged |
|---|---|---|
| Loamwake clear | onboarding mastery | 3–7 days |
| Ledgerhollow clear | system broadening | 1–2 weeks total |
| Memory Fen clear | ritual understanding | 2–4 weeks total |
| Wayworks clear | optimization pressure | 4–7 weeks total |
| Buried Court unlock path | pre-Deepening convergence | 6–10 weeks total |
| First Deepening | major account transition | 8–12 weeks total |

These numbers are not final launch promises. They are **prototype target bands**.

### 11.2 Deepening gate requirements should combine
- Battle Stats readiness
- Heritage Stats readiness
- strain maturity
- one or two meaningful Confidant tracks
- basic Burrow and Treasure depth
- hidden or clue-based completion requirements

Deepening must not become a single-stat check.

---

## 12. Inflation control rules

### 12.1 Mooncaps inflation prevention
- introduce new medium sinks before adding major new faucets
- keep some upgrade curves exponential or stair-stepped
- rotate utility expenditures and recurring costs

### 12.2 Event currency inflation prevention
- preserve prep windows
- avoid over-rewarding off-week accumulation
- rotate side shops instead of increasing base drop rates too quickly

### 12.3 Side-lane inflation prevention
- Crack Market inventory should rotate and cap efficiency
- Clique rewards should emphasize identity and support, not raw universal power

---

## 13. Prototype numeric test sheet

These are sample prototype numbers for feel-testing only.

### 13.1 Example Burrow daily output at early-mid prototype state
- Dewpond Gather: 250 Mooncaps
- Mushpatch Gather: 30 Mushcaps
- Rootmine Gather: 300 Mooncaps + low chance basic Clothes material
- Tickworks Gather: 200 Mooncaps + tiny Glowcap trickle chance or timed bonus token
- 3 Daily Duties: 700 Mooncaps + 25 Mushcaps + 2 Lucky Draws equivalent prep value
- 1 Burrow Post completion: 200–400 Mooncaps + small side reward
- 2 Strata routes: 500–1200 Mooncaps equivalent + 1–3 Treasure Shards / Strain Seeds chance

**Result:** a routine daily engaged player can generate around **2,500–4,500 Mooncaps** and enough side materials to make 1–3 meaningful choices.

### 13.2 Example weekly engaged totals
- Mooncaps: 20,000–30,000+
- Mushcaps: 900–1,200
- Lucky Draws prep value: 15–25 from routine, plus extra from event ladders and posts
- Treasure Tickets prep value: 8–16 from routine, plus targeted bonuses
- Echoes: 180–350
- Bronze Shovels: 4–8

These numbers should be used as **prototype feel checks**, not locked balance.

---

## 14. Open tuning questions

These are still unresolved and should be decided in spreadsheet form next:

1. What exact Mooncap curve should Burrow building upgrades use?
2. How much Treasure power should come from Set/Polish/Strengthen versus Clothes Reinforce?
3. How quickly should each Strain line progress relative to Strata completion?
4. How often should Bronze Shovels appear before the Crack feels like mandatory homework?
5. Should Glowcaps trickle from Tickworks directly, or only via milestone conversion?
6. How many Elder Books should a disciplined F2P player expect per cycle/month?
7. What is the minimum Favor Marks output needed so Clique membership feels worthwhile at launch?
8. How many Strata Seals should the player earn per Deepening band?
9. Which currencies should have hard caps, soft caps, or no caps?
10. What are the first acceptable ranges for Lucky Wheel and Treasure Finder pity systems?

---

## 15. Acceptance criteria for v1

This file should be considered successful if it gives the team:
- a clear role for each currency
- prototype target output bands
- event prep expectations
- first-pass Deepening pacing targets
- launch scarcity rules
- system pressure points
- a clear list of unresolved tuning questions

If a future spreadsheet contradicts this file, the spreadsheet wins.
This file exists to make spreadsheet setup faster and more coherent.

---

## 16. Recommended immediate follow-up

After this file, create:
1. `economy_model_v1_sheet.csv` or spreadsheet equivalent
2. `data_schema_v1.md`
3. `backend_architecture_options.md`
4. `core_loop_mvp_spec.md`

This is the point where the project should transition from **content-complete concept stack** to **prototype-ready systems stack**.
