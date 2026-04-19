# Gnome Game — System Interactions Matrix

## Purpose
This file maps how the major systems in **Gnome Survivor** feed into each other.

It exists to prevent a common live-service design failure:
- strong individual systems
- weak connective tissue
- unclear reasons to engage with older features
- accidental dead ends or mandatory grinds

This is not a balance sheet.
It is a **dependency and pressure map** for the game’s long-term design.

It should be read alongside:
- `master_glossary.md`
- `phase1_content_bible.md`
- `strains_bible.md`
- `clique_confidants_bible.md`
- `deepening_arc.md`
- `economy_and_progression_templates.md`

---

## Core design rule
Every major system should do at least **two jobs**:
1. improve its own lane
2. feed another lane

If a system only feeds itself, it becomes isolated.
If a system feeds everything, it becomes mandatory and oppressive.

The target is a **web**, not a ladder and not a pile.

---

## System list

### Account / meta spine
- The Burrow
- Deepening
- Memory Shift
- Strata
- Strains

### Power and build systems
- Battle Stats
- Heritage Stats
- Treasures
- Clothes
- Pathwheel
- Strata Traits

### Character and social systems
- Confidants
- Burrowfolk
- Cliques
- Wanderers / Runaways / The Wild Ones

### Side and event systems
- The Crack
- Lucky Draw Week
- Treasure Week
- Echo Week
- Festival Ledger

### Economy systems
- Mooncaps
- Glowcaps
- Mushcaps
- Treasure Shards / Polishes
- Strain Seeds
- Favor Marks
- Crack Coins
- Strata Seals
- Echoes / Echo Shards / Elder Books

---

# 1. High-level interaction map

```text
The Burrow
  ├─ produces Mooncaps / Mushcaps / materials
  ├─ improves long-term account stability
  ├─ powers exploration readiness
  └─ unlocks more systems through Deepening

Strata
  ├─ award resources, Strain Seeds, Clothes, Treasures, Buried Clues
  ├─ unlock Confidants, Burrowfolk, Strata Traits, Duties, posts
  ├─ gate Deepening
  └─ provide the main route for account expansion

Strains
  ├─ shape available synergies and unlock logic
  ├─ influence Confidants and Burrowfolk identities
  ├─ affect what kinds of builds feel natural
  └─ frame political/story interpretation of progression

Treasures + Pathwheel + Heritage Stats
  ├─ feed combat and gate checks
  ├─ improve ritual, collection, and region readiness
  └─ support multiple other systems at once

Clothes + Battle Stats
  ├─ sharpen direct Clash performance
  ├─ overcome Warden walls
  └─ keep field progression legible and immediate

Confidants
  ├─ unlock Burrowfolk types
  ├─ open Backstories and Confidence Trails
  ├─ connect Strains to story and utility
  └─ support specialization choices

Burrowfolk
  ├─ handle Burrow Work and Clash support
  ├─ create another growth lane
  └─ deepen Confidant and Clique relevance

Cliques
  ├─ create shared goals, social pacing, and Favor Marks
  ├─ support Great Dispute later
  └─ turn social play into long-horizon account value

The Crack
  ├─ provides Crack Coins and side progression
  ├─ prevents total dependence on the main route
  └─ offers endless depth without replacing Strata

Events
  ├─ accelerate planning, targeting, and hoarding decisions
  ├─ keep old resources useful
  └─ add cyclical pressure without replacing core progression

Deepening
  ├─ recontextualizes everything above
  ├─ opens new Burrow layers and system depth
  ├─ rewards Strata Seals and new strategic stakes
  └─ prevents early systems from becoming narratively dead
```

---

# 2. Primary dependency matrix

## Reading key
- **Primary Feed** = a major output that meaningfully powers another system
- **Secondary Feed** = a smaller or indirect contribution
- **Gate** = a system that controls access or pacing
- **Pressure** = what decision or tension it creates

| System | Primary Feed | Secondary Feed | Gate / pressure created |
|---|---|---|---|
| The Burrow | Mooncaps, Mushcaps, passive materials | utility upgrades, quality-of-life stability | daily return pressure, output optimization |
| Strata | Treasures, Clothes, Strain Seeds, Buried Clues, Field Cards | Confidants, region-specific resources, progression identity | route choice, region prioritization, Warden walls |
| Strains | build identity, Confidant logic, story framing | faction flavor, region political tone | specialization choice, social interpretation |
| Treasures | Heritage Stats, Pathwheel power, long-tail account growth | event targeting value, collection bonuses | upgrade clutter risk, targeting pressure |
| Clothes | Battle Stats, direct Clash readiness | slot identity, region optimization | immediate progression pressure |
| Pathwheel | exploration and stat shaping | Treasure relevance, route planning | socketing and build-choice pressure |
| Strata Traits | region payoff, long-run account identity | build-swapping depth | commitment pressure after clear |
| Confidants | Burrowfolk unlocks, Trust, Calling, story power | Burrow Posts, Backstory hooks | clue-chain dependency |
| Burrowfolk | Burrow Work, Clash support, side growth | Clique value, Confidant reinforcement | roster upkeep, training investment |
| Cliques | Favor Marks, social retention | shared goals, Great Dispute future, queue planning | coordination pressure |
| Wanderers | deals, disruption, gifts, opportunities | lore, side tension, rotating cast growth | attention management |
| Runaways | chase content, rewards, moral texture | clue chains, tension spikes | pursuit vs ignore choice |
| The Wild Ones | conflict injection, difficulty spikes | posts, encounter pressure, recurring threat identity | rising challenge curve |
| The Crack | Crack Coins, side rewards, persistence | alternate progression, endless content | optional second-job risk |
| Lucky Draw Week | burst rewards, hoard payoff | collection variance, event momentum | saving pressure |
| Treasure Week | targeting control | Treasure progression depth | hoard discipline, preference clarity |
| Echo Week | ritual progression, Echo economy payoff | memory/ritual lore reinforcement | timing and preparation pressure |
| Festival Ledger | cross-week continuity | player retention and cumulative reward pace | streak pressure |
| Deepening | Strata Seals, Burrow transformation, new stakes | recontextualized truth, system refresh | long-horizon planning |
| Memory Shift | story escalation, late hidden loops | ritual danger, encounter weirdness | uncertainty and curiosity |

---

# 3. System-by-system interaction notes

## The Burrow
### Feeds
- generates baseline progression stability through Gather loops
- powers exploration readiness through Mushcaps and general supply flow
- supports Treasure and production scaling through Provision Board, Output, and Socket choices
- becomes the visible proof of Deepening progress

### Receives from
- Strata rewards
- Deepening unlocks
- Treasure investment
- event overflow and catch-up value

### Design role
The Burrow is where progress becomes **felt**.
If a player grows stronger but The Burrow does not look or function better, the account can feel oddly flat.

---

## Strata
### Feeds
- main acquisition lane for story, power, Strain Seeds, Clothes, Treasures, Duties, Buried Clues, Confidants, and Warden progression
- defines farming identity by region
- gives the game its chapter structure and cultural variety

### Receives from
- Battle Stats
- Heritage Stats
- Pathwheel setup
- Strata Traits
- Confidant and Burrowfolk preparation
- Burrow readiness through Mushcaps and output

### Design role
Strata are the **front line** of the game.
Most systems should matter here, directly or indirectly.

---

## Strains
### Feeds
- identity and worldview
- region-specific social logic
- Confidant and Burrowfolk flavor
- build preference and faction contrast

### Receives from
- story exposure through Deepening and Buried Clues
- Strain Hall / Strain Loom investment
- Strain Seed farming

### Design role
Strains should change what progression **means**, not only what it grants.

---

## Treasures + Pathwheel + Heritage Stats
### Feeds
- stat gates
- collection depth
- ritual and event relevance
- long-term power that survives new content additions

### Receives from
- Strata
- Treasure Week
- Lucky Draw Week
- Burrow investment
- targeting systems

### Design role
This cluster is the safest place for long-run account depth because it stays useful across multiple Deepenings.

---

## Clothes + Battle Stats
### Feeds
- direct field performance
- clearer short-term upgrades
- Warden progression
- battle pacing legibility

### Receives from
- Strata farming
- Cloth Assembly loops
- event rewards
- Burrow materials and Mooncaps

### Design role
Clothes keep progression concrete.
They should feel more immediate than Treasures, but less permanently foundational.

---

## Confidants
### Feeds
- new Burrowfolk classes
- utility identity through Calling
- Backstory and Buried Clue expansion
- specialized account shaping

### Receives from
- Strata progression
- Clue Triggers
- Duties and Posts
- Strain-specific story logic

### Design role
Confidants are the bridge between narrative and optimization.
They should never feel like “just story NPCs” or “just passive bonuses.”

---

## Burrowfolk
### Feeds
- Burrow Work
- Clash support
- class diversity
- secondary account depth
- Clique identity support

### Receives from
- Confidants
- training investment
- Burrow Nursery management
- social and event support rewards

### Design role
Burrowfolk should make the player feel like they are managing a living settlement, not just a solo hero.

---

## Cliques
### Feeds
- social retention
- shared planning and coordination
- Favor Marks economy
- future Great Dispute stakes

### Receives from
- Burrowfolk investment
- Burrow Posts and social content
- event pacing and contribution loops

### Design role
Cliques should feel useful early, meaningful midgame, and strategically significant later.
They should not be dead weight until PvP exists.

---

## Wanderers / Runaways / The Wild Ones
### Feeds
- variety and recurring cast growth
- pressure, opportunity, chase, and surprise
- economic flexibility through deals or disruptions
- post-driven content scaling

### Receives from
- Burrow Posts framework
- event rotations
- Deepening escalation
- Strata-specific content pools

### Design role
This roaming-content triangle is the easiest way to scale the game toward a very large recurring cast without breaking the main chapter structure.

---

## The Crack
### Feeds
- endless-lane persistence
- Crack Coins
- side-identity and optional mastery
- relief from main-route stagnation

### Receives from
- Bronze Shovels
- account readiness from main systems
- event rewards and side hooks

### Design role
The Crack must feel rewarding, but not mandatory homework.
It should be strong enough to matter and soft enough that skipping it for a day does not feel punishing.

---

## Weekly event cycle
### Lucky Draw Week
Feeds burst reward variance, hoard payoff, and the emotional high of planned spending.

### Treasure Week
Feeds targeting, collection control, and Treasure planning discipline.

### Echo Week
Feeds ritual depth, Echoes, Echo Shards, Elder Books, and story texture around memory and ancestors.

### Festival Ledger
Feeds continuity across all three, making the cycle feel cumulative rather than isolated.

### Design role
The weekly cycle should not replace the game.
It should sharpen existing systems and redirect attention.

---

## Deepening + Memory Shift
### Feeds
- new Burrow layers
- stronger stakes and reinterpretation of old content
- Strata Seals
- late-game content legitimacy

### Receives from
- total account maturity across multiple lanes
- clue completion
- Warden clears
- system comprehension

### Design role
Deepening is the game’s answer to content mortality.
Instead of leaving early content behind, it changes what that content **means**.

---

# 4. Pressure map

## Short-term pressures
- spend Mooncaps now vs save for a larger upgrade
- use Mushcaps in current Stratum vs save for a more targeted push
- reinforce Clothes now vs hold materials for a better slot
- follow current Burrow Posts vs push field progression

## Mid-term pressures
- invest in Treasures vs Clothes vs Strain growth
- focus one Confidant path vs spread investment
- support Clique contribution vs personal speed
- push event tiers vs preserve resources for the next cycle

## Long-term pressures
- optimize for current Warden success vs overall account health
- pursue Deepening efficiently vs over-farm comfort loops
- treat old systems as support lanes vs abandon them too early
- chase every event vs build repeatable discipline

---

# 5. Loop bundles

## A. Main progression loop
```text
The Burrow output
→ Mushcaps / Mooncaps / readiness
→ Strata runs
→ Treasures / Clothes / Seeds / Clues / Confidants
→ stronger build and more systems
→ Warden clears
→ Strata Traits / Deepening progress
→ The Burrow expands and the account deepens
```

## B. Social loop
```text
Strata and Burrow progression
→ Burrowfolk and Confidants improve
→ Clique participation improves
→ Favor Marks / shared goals / retention improve
→ broader account value and future Great Dispute readiness
```

## C. Event loop
```text
normal play and hoarding
→ weekly event begins
→ targeted spending / burst progression
→ Festival Marks accumulate
→ Festival Ledger rewards feed back into main systems
```

## D. Endless side loop
```text
main account growth
→ Bronze Shovels and Crack readiness
→ The Crack progression
→ Crack Coins and side rewards
→ support main account without replacing it
```

## E. Story-reveal loop
```text
Strata play
→ Duties / Posts / Buried Clues / Confidants
→ Deepening Gate progress
→ new Deepening
→ Memory Shift intensifies
→ old systems gain new meaning
```

---

# 6. Failure-state warnings

## If Treasures dominate too hard
- Clothes feel cosmetic
- Pathwheel becomes mandatory homework
- all builds collapse into one optimal collection path

## If Clothes dominate too hard
- Treasures feel like slow dead weight
- long-horizon account planning weakens
- event targeting depth collapses

## If Confidants are too passive
- story and optimization split apart
- Confidence Trails feel optional
- Burrowfolk lose emotional relevance

## If Burrowfolk are too strong
- solo build expression weakens
- roster upkeep becomes chore pressure

## If The Crack is too rewarding
- it becomes mandatory daily homework
- the main chapter route weakens

## If weekly events are too rewarding
- normal progression feels inefficient
- hoarding becomes emotionally mandatory
- every off-week feels like dead time

## If Deepening is too dominant
- players stop caring about local Strata identity
- launch content feels like a long tutorial instead of real content

## If Cliques are too weak
- social identity collapses
- Favor Marks become forgettable
- Great Dispute has no foundation later

---

# 7. Content-scaling rules

## Rule 1: New content should attach to at least two old systems
A new Wanderer family, event reward, Stratum branch, or Crack variant should reinforce something that already exists.

## Rule 2: One new system should not obsolete two old ones
If a late addition replaces Treasure planning and Clothes planning at once, it is too broad.

## Rule 3: Reuse identity, not just rewards
When new content is added, it should carry one of the established identities:
- Freeroot softness
- Inkroot record power
- Brassroot action and argument
- Crownroot hierarchy and polish
- Hushline memory secrecy

## Rule 4: Old resources must stay lightly useful
Mooncaps, Treasure Shards, Burrow Work outputs, and event leftovers should retain at least one ongoing sink.

## Rule 5: Deepening should reframe, not erase
New Deepening stages should make earlier Strata and story beats more complex, not just less relevant.

---

# 8. Recommended implementation order for interaction testing

## First-pass validation order
1. The Burrow → Strata → Clothes / Treasures
2. Strata → Confidants → Burrowfolk
3. Burrow → event cycle hooks
4. Strata clear → Strata Traits → Deepening Gate
5. The Crack as optional support lane
6. Clique loops after enough reasons to cooperate exist

## Why this order
This sequence proves:
- the game is fun moment to moment
- power feeds story
- story feeds unlocks
- events sharpen systems instead of replacing them
- side systems remain optional but meaningful

---

# 9. Practical production use
Use this file whenever a new feature is proposed.

Ask these questions:
1. what two existing systems does it feed?
2. what resource family does it belong to?
3. what pressure does it add?
4. what old content does it keep relevant?
5. does it strengthen the web or create a dead-end lane?

If those questions cannot be answered clearly, the feature is not ready.

---

## Closing note
This project should feel like a dense, compounding live-service game where cozy satire, buried history, and account planning all reinforce each other.

The interaction matrix is the safeguard against drift.
It makes sure:
- story is not detached from systems
- social play is not detached from progression
- events are not detached from the base game
- Deepening is not detached from the Burrow
- new content does not collapse the older web
