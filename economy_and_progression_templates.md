# Economy and Progression Templates

## Purpose

This file defines the reusable **faucet / sink / gate / reward** framework for **Gnome Survivor**.

It is meant to stop the economy from being split across separate features with no shared logic. Instead, every major system should answer the same questions:
- what is the resource?
- where does it come from?
- what does it buy or unlock?
- what pressure does it create?
- what long-term account problem does it solve?
- how does it stay relevant across multiple Deepenings?

This document covers the account-wide framework for:
- **Mooncaps**
- **Glowcaps**
- **Mushcaps**
- **Treasures** and Treasure materials
- **Clothes** and Clothes materials
- **Strain Seeds**
- **Favor Marks**
- **Crack Coins**
- **Strata Seals**
- supporting currencies, upgrade materials, and pacing gates

It is designed for a long-running live-service game where multiple systems compound over time instead of replacing each other.

---

## Core Economy Philosophy

### 1. Every resource needs a job
A resource should not exist just because a feature wants its own icon.

Every resource must have a clear identity:
- **core progression fuel**
- **friction gate**
- **specialization material**
- **social currency**
- **event currency**
- **premium currency**
- **endless-lane currency**
- **meta-progression currency**

If a resource cannot be explained in one sentence, it probably does not need to exist yet.

### 2. Short-term excitement must feed long-term planning
Good economy layers should let the player answer both:
- “What can I improve right now?”
- “What am I saving for three weeks from now?”

### 3. Pressure is intentional
Friction is not failure. It is pacing.

Players should feel some recurring tension around:
- time
- targeting
- specialization
- planning ahead
- choosing what not to spend

### 4. Old systems must remain partially useful
As the game grows, old currencies and items should not become total junk.
Instead they should:
- remain useful in low-volume sinks
- feed conversions
- support catch-up systems
- contribute to side systems
- rotate into events or exchange tables

### 5. Horizontal growth should protect vertical pacing
The game should offer many permanent growth lanes without letting one solved build trivialize all future content.
This means growth should spread across:
- Treasures
- Clothes
- Confidants
- Burrowfolk
- Strains
- Strata Traits
- Deepening rewards
- event preparation

---

## Economy Roles Overview

| Resource / family | Role | Main feeling | Main risk if overtuned |
|---|---|---|---|
| Mooncaps | main soft currency | constant usable progress | inflation / meaninglessness |
| Glowcaps | premium earnable currency | flexibility and temptation | pay-pressure or hoard paralysis |
| Mushcaps | stamina / expedition fuel | session planning | annoyance if too restrictive |
| Treasures + Treasure Shards / Polishes | permanent account power | collection and targeting | over-dense upgrade clutter |
| Clothes + Clothes materials | direct combat and slot identity | power spikes and build shaping | over-centralized stat dependency |
| Strain Seeds | strain progression and unlock gating | focused farming | hard stall if too scarce |
| Favor Marks | Clique progression and social identity | belonging and cooperation | dead social lane |
| Crack Coins | endless-lane currency | side progression and persistence | becomes mandatory second job |
| Strata Seals | Deepening meta currency | long-horizon advancement | oppressive reset-feeling if too central |
| Festival Marks | meta event currency | steady cycle progress | event fatigue |
| Echoes / Echo Shards / Elder Books | ritual progression family | ritual depth and timing | opaque shrine overload |

---

# Core Currency Templates

## 1. Mooncaps

### Role
Primary soft currency for everyday progression.

### Main sources
- Burrow Gather actions
- Strata progression
- Field Returns
- Daily Duties
- Duties and Burrow Posts
- Lucky Wheel / Treasure Finder rewards
- Burrow Exchange overflow value
- some Wanderer transactions

### Main sinks
- building Expand actions
- Treasure Set / Polish costs
- Cloth Assembly reinforcement costs
- unlocking utility surfaces
- some Burrowfolk Train / Raise costs
- small exchange fees

### Design purpose
Mooncaps should be the resource the player is almost always earning and almost always spending.
It is the **main rhythm currency**.

### Good player feeling
“I can always make at least some progress today.”

### Danger sign
If players stop caring when they see Mooncaps, the whole economy has become too top-heavy.

### Design rule
Mooncaps should support:
- frequent small upgrades
- medium planned spends
- occasional burst drains

But they should **not** be the hardest gate for late-game growth.

---

## 2. Glowcaps

### Role
Premium earnable currency that creates flexibility, convenience, and strategic temptation.

### Main sources
- long-term milestone rewards
- Festival Ledger
- event ladders
- first-clear achievements
- Deepening milestones
- purchasable bundles

### Main sinks
- premium shop entries in Burrow Exchange
- event support packs
- quality-of-life refreshes
- catch-up convenience
- special targeting offers
- cosmetic or vanity purchases

### Design purpose
Glowcaps should feel valuable, but not mandatory for basic survival.
They should speed planning, not replace it.

### Good player feeling
“I can spend this now to make my plan cleaner, or save it for a better cycle.”

### Danger sign
If Glowcaps feel required for basic pacing, the monetization layer is too aggressive.

### Design rule
Glowcaps should help with:
- convenience
- flexibility
- opportunity
- optional acceleration

They should not be the only way to reach functional viability.

---

## 3. Mushcaps

### Role
Exploration fuel and session-length governor.

### Main sources
- Mushpatch
- Field Returns
- Daily Duties
- Strata rewards
- Wanderer gifts or deals
- event support rewards

### Main sinks
- Strata exploration runs
- longer / richer route commitments
- optional high-yield expedition branches

### Design purpose
Mushcaps create the decision of **where to send time**.
They should shape route choice, not simply annoy the player.

### Good player feeling
“I cannot do everything, so I should choose where my next push matters most.”

### Danger sign
If Mushcaps only delay fun and create no real route decisions, they are just friction tax.

### Design rule
Mushcaps should gate:
- route frequency
- route length
- optional overextension

They should not completely block all meaningful play once spent.
The player should still have Burrow, Crack, Clique, Post, and event actions available.

---

# Permanent Progression Families

## 4. Treasures

### Role
Broad permanent account-strength layer tied to Heritage Stats, Pathwheel placement, event targeting, collection identity, and specialized growth.

### Main inputs
- Treasures from Lucky Wheel and Treasure Finder
- Treasure Shards
- Polishes
- targeted exchanges
- Strata rewards
- some Buried Clue chains

### Main verbs
- **Set**
- **Polish**
- **Strengthen**
- **Attune**
- form **Harmony** sets

### Main purposes
- stat growth
- Pathwheel optimization
- board specialization
- collection planning
- event preparation
- account-wide power smoothing

### Design purpose
Treasures are the game’s major long-tail account layer.
They should reward both broad collecting and focused planning.

### Good player feeling
“Even low-drama gains matter because this collection compounds forever.”

### Danger sign
If Treasure growth becomes unreadable or spreadsheet-only, the system has become too dense.

### Design rules
Treasures should support:
- broad account value
- specialized board choices
- long-tail collection goals
- event-targeting payoff

Not every Treasure needs equal rarity or equal urgency.
Some should be:
- baseline staples
- niche enablers
- event accelerators
- long-horizon vanity chase pieces

---

## 5. Clothes

### Role
Direct combat and slot-based power layer.

### Main inputs
- Clothes drops from Strata and reward pools
- reinforcement materials
- Bind merges
- Salvage recovery
- some event bundles

### Main verbs
- **Fit**
- **Reinforce**
- **Bind**
- **Salvage**

### Design purpose
Clothes should be the most immediately readable source of battle-strength adjustment.
Where Treasures are broad and compounding, Clothes should feel more tactical and slot-specific.

### Good player feeling
“I just improved a real part of my build and can feel it right away.”

### Danger sign
If Clothes overshadow every other system, then build diversity collapses into simple gear-chasing.

### Design rules
Clothes should provide:
- immediate power spikes
- slot identity
- Strata-specific adaptation
- satisfying salvage loops

They should **not** be the only thing that matters for late-game viability.

---

## 6. Strain Seeds

### Role
Unlock and growth material for Strains.

### Main sources
- Strata exploration tied to dominant regional lineages
- clue rewards
- Confidant trails
- selected event rewards
- Burrow Exchange rotation

### Main sinks
- unlock Strains or strain thresholds
- raise line depth
- gate Calling branches
- support Confidant and Burrowfolk line synergy

### Design purpose
Strain Seeds are the **identity gate** resource.
They should decide when the player can push a lineage deeper, not just passively accumulate everything at once.

### Good player feeling
“I finally have enough to push the line I care about.”

### Danger sign
If Strain Seeds are too scarce, players feel hard-stalled. If too common, line identity disappears.

### Design rules
Strain Seeds should create:
- route targeting
- regional farming identity
- delayed gratification
- real choices about which line to advance first

---

# Social and Endless-Lane Currencies

## 7. Favor Marks

### Role
Clique progression and social-store currency.

### Main sources
- Clique Queue participation
- Clique contributions
- shared goals
- light social milestones
- Great Dispute participation later

### Main sinks
- Clique support purchases
- social utility unlocks
- long-tail cooperative rewards
- cosmetic identity items
- special support resources for clique growth

### Design purpose
Favor Marks should make Clique participation feel useful from week one, not like a future placeholder.

### Good player feeling
“My Clique matters even before the biggest social mode exists.”

### Danger sign
If Favor Marks only buy trivial filler, the social layer will feel dead.

### Design rules
Favor Marks should support:
- useful but not mandatory assistance
- social belonging
- contribution reward loops
- steady weekly progress

---

## 8. Crack Coins

### Role
Endless-lane currency from **The Crack**.

### Main sources
- Bronze Shovel runs
- Crack floor clears
- Crack event nodes
- some hidden Crack variants

### Main sinks
- Crack Market purchases
- side-progression items
- specialized materials
- long-tail catch-up supports
- route utility for Crack runs

### Design purpose
Crack Coins should support a progression lane that is **always worth engaging, never fully finished, but not strictly mandatory every day**.

### Good player feeling
“I can always push this a little further and still get something useful.”

### Danger sign
If Crack Coins become too essential for all account growth, The Crack turns from optional endless lane into chore tax.

### Design rules
Crack Coins should support:
- evergreen side progression
- catch-up resources
- optional grind satisfaction
- moderate accumulation over long periods

---

## 9. Strata Seals

### Role
Deepening meta-progression currency.

### Main sources
- Deepening milestones
- Buried Requirement completion
- major Warden clears
- deep story thresholds
- limited late-cycle rewards

### Main sinks
- Deepening unlock gates
- meta-system enhancements
- long-horizon account advancement
- possibly Pathwheel / Strata Trait expansion over time

### Design purpose
Strata Seals are the **macro-progression proof** that the account has passed through major truth layers.
They should feel rare and important.

### Good player feeling
“I have crossed a real threshold, not just filled another bar.”

### Danger sign
If Strata Seals become common or heavily farmable, Deepening loses narrative and systemic weight.

### Design rules
Strata Seals should:
- be rare
- mark major advancement
- unlock meaningful new depth
- never feel like ordinary daily farm currency

---

# Supporting Resource Families

## 10. Echo family

### Members
- **Echoes**
- **Echo Shards**
- **Elder Books**
- **Echo Rushes**

### Role
Ritual progression family used for the Ancestral Depictor and Echo Week.

### Design purpose
This family should feel ceremonial, slightly strange, and more deliberate than ordinary currencies.
It should support memory, ritual, summoning, and hidden-history pressure.

### Design rule
The Echo family should have stronger emotional identity than Mooncaps or Clothes materials.
It should feel like participation in a system the player is not fully meant to understand at first.

---

## 11. Festival Marks

### Role
Meta-event progression feed across the 3-week cycle.

### Main sources
- Lucky Draw Week participation
- Treasure Week participation
- Echo Week participation
- selected event side loops

### Main sinks
- Festival Ledger track progress

### Design purpose
Festival Marks make every week matter, even if the player cannot fully commit to that specific event.

### Design rule
Festival Marks should reduce event discouragement by ensuring partial engagement still contributes to something permanent or near-permanent.

---

# Faucet / Sink Template

Use this table when designing any new resource or progression material.

| Question | What to define |
|---|---|
| What is it for? | one clear job |
| How often is it earned? | frequent / periodic / rare / milestone |
| What is the main faucet? | where most of it comes from |
| What is the emergency faucet? | catch-up or side acquisition |
| What is the main sink? | primary usage |
| What is the pressure? | scarcity, targeting, timing, social contribution, etc. |
| What system does it reinforce? | Burrow / Strata / Crack / Clique / event / Deepening |
| What keeps it relevant later? | exchange, catch-up, rotation, secondary use |

---

# Progression Layer Template

Each permanent progression lane should be described in the same way.

## Template

### Layer name

### Fantasy
What the player feels this layer represents.

### Inputs
What resources feed it.

### Outputs
What strength, access, or identity it creates.

### Main decisions
What choices the player makes.

### Tuning danger
How this layer can become annoying or overpowered.

### Long-term scaling rule
How this layer remains useful after multiple Deepenings.

---

# Progression Lane Snapshots

## Burrow lane
### Fantasy
Restore, expand, and stabilize the surviving heart of gnome civilization.

### Inputs
Mooncaps, building materials, Treasures, Burrow Rushes.

### Outputs
Higher Output, better Storage, more efficient gathering, broader utility.

### Main decisions
Which stations to Expand first, when to invest in utility vs income, when to Socket Treasures for production.

### Danger
Can become a boring checklist if upgrades are too linear.

### Long-term rule
Burrow upgrades should remain relevant because infrastructure supports every other lane.

---

## Strata lane
### Fantasy
Push outward into fractured regions, recover truth, and farm identity-specific progression.

### Inputs
Mushcaps, Clothes, Treasures, Strain Seeds, Duties rewards.

### Outputs
Field Returns, Warden progress, Buried Clues, region-specific materials.

### Main decisions
What Stratum to prioritize, how much to push route efficiency, whether to chase farm value or clue value.

### Danger
Can become pure stamina tax if route identity is weak.

### Long-term rule
Each Stratum must remain partially useful due to unique drops, clue hooks, or line-specific farming.

---

## Strain lane
### Fantasy
Shape the account’s bloodline logic and long-term identity.

### Inputs
Strain Seeds, selected rewards, Calling gates.

### Outputs
Line depth, synergy unlocks, Confidant and Burrowfolk interactions, future build identity.

### Main decisions
Which line to push, whether to broaden or specialize, when to save for a threshold.

### Danger
Can feel punitive if lines are too locked behind a single scarce farm.

### Long-term rule
Every Strain should continue offering useful specialization even after the first unlock cycle.

---

## Clique lane
### Fantasy
Belong to a petty, survival-driven political circle that still matters materially.

### Inputs
Favor Marks, Clique Queue participation, shared effort.

### Outputs
Support resources, identity, social momentum, future Great Dispute readiness.

### Main decisions
How much to contribute, what support purchases to prioritize, which social rewards matter.

### Danger
Becomes dead weight if early utility is weak.

### Long-term rule
Clique rewards must stay useful even before high-end social warfare exists.

---

## Crack lane
### Fantasy
Dig into a dangerous endless side lane that always offers one more push.

### Inputs
Bronze Shovels, Crack route capability, side-build investment.

### Outputs
Crack Coins, side resources, evergreen accumulation.

### Main decisions
When to spend shovels, how deep to push, whether to convert time into side gains.

### Danger
Turns into mandatory chore if payouts become too central.

### Long-term rule
The Crack should remain rewarding but not dominant.

---

## Deepening lane
### Fantasy
Descend into older truth and reshape the account through remembered history.

### Inputs
major progression completion, Buried Requirements, Strata Seals.

### Outputs
meta unlocks, deeper content, story escalation, stronger long-horizon identity.

### Main decisions
Whether the account is truly ready to cross the next gate.

### Danger
Feels like a fake reset if too much visible progress appears invalidated.

### Long-term rule
Deepening should add layers, not erase the value of the existing account.

---

# Tuning Heuristics

## Early game
Focus on:
- readable Mooncap spending
- satisfying Mushcap pacing
- obvious Clothes upgrades
- simple Treasure wins
- one clear Strain goal

## Mid game
Add:
- stronger targeting pressure
- Buried Clue-linked farming
- richer event preparation
- more meaningful Clique and Crack choices

## Late game
Shift toward:
- specialization tension
- event hoarding strategy
- Harmony / Pathwheel optimization
- line-depth identity
- Deepening readiness

## Live-service scaling
Do not mostly scale through raw number inflation.
Scale through:
- new sinks
- targeted pools
- variant reward tables
- route modifiers
- more nuanced exchanges
- rotating utility relevance

---

# Anti-Bloat Rules

1. Do not create a new currency if an existing one can do the job.
2. Do not create a new premium shortcut for a problem caused by bad base tuning.
3. Do not let every feature invent its own upgrade material stack.
4. Do not let old currencies fully die; rotate or convert them.
5. Do not hide core pacing behind too many sub-materials too early.
6. Do not let Deepening invalidate basic account pride.
7. Do not let the social lane become cosmetic-only before it has meaningful utility.
8. Do not let the event lane become the only real way to progress.

---

# Recommended Content-Sheet Columns

Use these columns in future economy sheets or spreadsheets:
- System
- Resource Name
- Resource Role
- Main Faucet
- Secondary Faucet
- Main Sink
- Secondary Sink
- Pressure Type
- Target Audience (new / mid / late / social / event / meta)
- Scaling Rule
- Notes

---

## Immediate next use for this file

This document should be used as the source for:
- economy spreadsheets
- upgrade-cost planning
- event reward balancing
- sink/faucet audits
- progression gate reviews
- anti-bloat checks before adding new currencies
