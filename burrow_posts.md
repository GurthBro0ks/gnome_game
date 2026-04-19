# Gnome Game — Burrow Posts

## Purpose
This document defines the **Post layer** as a scalable live-content system.

It covers:
- **Burrow Posts**
- **Confidant Posts**
- **Stranger Posts**
- **Wild Posts**
- how Posts deliver **Duties** and **Encounters**
- how the system should scale to a long-running library of roaming characters, notices, recurring chains, and compounding difficulty

This file assumes the current canon from:
- `master_glossary.md`
- `phase1_content_bible.md`
- `strains_bible.md`
- `clique_confidants_bible.md`
- `deepening_arc.md`
- `duties_and_encounters.md`
- `wanderers_runaways_wild_ones.md`

---

## Core rule
A **Post** is the player-facing delivery shell for a content packet.

That means:
- **Duties** are the trackable objective
- **Encounters** are the authored situation
- **Posts** are how the world asks the player to care

A Post should never feel like a generic quest node.
It should feel like paperwork, rumor, panic, opportunity, favor, gossip, or semi-official nonsense moving through a wounded civilization.

---

## Why Posts matter
The Post layer does four jobs at once:

1. turns UI surfaces into diegetic worldbuilding
2. lets content scale without every task needing a bespoke cutscene
3. keeps the Burrow feeling socially alive between major story beats
4. creates a controlled way to expand into **100+ roaming character sources** over time without collapsing readability

---

## Long-run scaling assumption
This system should be designed from day one for **very large content growth**.

Target assumption:
- launch with a modest but healthy Post library
- expand continuously through new Wanderers, Runaways, Wild Ones, Confidants, Strata incidents, and event variants
- support a future state where the game has **120+ named or semi-named roaming content sources** without the Post layer becoming unmanageable

Important design rule:
**Do not scale by only adding more names. Scale by adding more overlap, memory, escalation, and route pressure.**

That means a new Wanderer should not only add one new encounter.
A new Wanderer can also:
- appear in Stranger Posts
- intersect with existing Runaways
- trigger Buried Clues
- be referenced by Confidant Posts
- escalate Wild Posts
- affect Strata travel pressure
- unlock or complicate future Deepening content

---

## The four Post channels

### 1. Burrow Posts
These are internal notices, tasks, requests, disputes, repairs, and local administrative problems.

Tone:
- civic
- petty
- practical
- funny in a bureaucratic way
- occasionally alarming

They should feel like the Burrow trying to keep itself upright with paper, favors, and plausible deniability.

Use Burrow Posts for:
- Daily Duties
- repair tasks
- local supply problems
- Clique Queue spillover
- low-intensity investigations
- resource and production support tasks
- social comedy

---

### 2. Confidant Posts
These are personal, political, strategic, or relational posts tied to Confidants.

Tone:
- more specific
- more intimate
- more pointed
- more likely to alter Trust, Calling progress, or Confidence Trail state

Use Confidant Posts for:
- personal favors
- backstory reveals
- faction positioning
- clue delivery
- unlock chains
- Turned Confidant pressure
- special Burrowfolk tasks

---

### 3. Stranger Posts
These are the incoming requests, offers, warnings, and odd opportunities created by Wanderers.

Tone:
- curious
- opportunistic
- slightly uncertain
- sometimes useful, sometimes risky

Use Stranger Posts for:
- trade offers
- escort requests
- temporary route disruptions
- rumors
- item exchanges
- small mysteries
- social color that makes the world feel connected

---

### 4. Wild Posts
These are warnings, pursuits, bounties, disturbances, pressure events, and field-danger notices tied to The Wild Ones and Runaways.

Tone:
- urgent
- theatrical
- tense
- sometimes absurdly overstated

Use Wild Posts for:
- tracking targets
- route danger
- pursuit chains
- raid warnings
- containment failures
- high-pressure chase events
- escalating world instability

---

## Post content architecture
Every Post should be built from modular fields.

### Required fields
- **Post ID**
- **Channel**: Burrow / Confidant / Stranger / Wild
- **Source**: person, faction, office, or system origin
- **Headline**
- **Summary line**
- **Duty type**
- **Encounter type**
- **Strata tags**
- **Strain tags**
- **Difficulty tier**
- **Reward family**
- **State flags**
- **Repeatability**
- **Chain position**
- **Expiration behavior**

### Optional fields
- Confidant involved
- Wanderer subtype
- Runaway subtype
- Wild One subtype
- Buried Clue tie
- Deepening gate
- route pressure value
- social consequence
- truth reveal level
- humor tone level

---

## Post taxonomy for scale
To avoid chaos once the Post library grows large, every Post should belong to a visible taxonomy.

### By source family
- Burrow office
- Clique
- Confidant
- Wanderer
- Runaway
- The Wild Ones
- Warden-related
- Smoke Whispers
- system/event

### By player need served
- resources
- progression
- trust
- standing
- route safety
- clue discovery
- strain progression
- event scoring
- repair/building
- story escalation

### By friction style
- low-friction chore
- timed response
- route risk
- investigation
- dispute resolution
- pursuit
- moral choice
- chain unlock

### By narrative weight
- flavor only
- light worldbuilding
- relationship-bearing
- politically loaded
- truth-bearing
- Deepening-bearing

---

## Difficulty structure
Posts should compound difficulty over time, but not just through bigger numbers.

### Difficulty should scale across five axes
1. **travel burden**
2. **resource burden**
3. **combat burden**
4. **social consequence**
5. **truth complexity**

A later Stranger Post might not hit harder in a Clash, but it may:
- require crossing two Strata
- conflict with an active Clique Queue problem
- involve a Wanderer tied to a previous Runaway
- lock or unlock Confidence Trail state
- force a choice between Trust and Standing

That is the kind of compounding difficulty the Post layer should aim for.

---

## Recommended difficulty ladder

### Tier 0 — simple notice
One-step task.
Minimal risk.
Launch onboarding material.

### Tier 1 — routine complication
Adds travel, timing, or a second condition.
Still readable and low-pressure.

### Tier 2 — social entanglement
Two systems overlap.
Example: a Wanderer request collides with Clique expectations.

### Tier 3 — unstable route
Introduces threat, contradiction, pursuit, or partial truth.
Multiple outcomes possible.

### Tier 4 — faction heat
A choice meaningfully alters trust, standing, or future Post chains.

### Tier 5 — Deepening pressure
The player is clearly interacting with the world’s hidden shape.
Used sparingly.

### Tier 6 — remembered instability
Post-Deepening or Memory Shift spillover.
Requires old chain memory, active system overlap, or route mastery.

---

## Visibility rules for a large live library
If the game eventually contains 120+ roaming content sources, the player should **not** see 120+ active Posts at once.

Use strict readability rules:

### Active surface limits
- show a small, curated number of Burrow Posts at once
- keep Confidant Posts relationship-driven and slower
- rotate Stranger Posts based on current Strata, route relevance, and library freshness
- use Wild Posts as pressure spikes, not constant spam

### Archive rules
- expired Posts can move to a readable archive
- recurring characters should keep memory without constantly clogging the front surface
- older named Wanderers should re-enter through chains, callbacks, and event resurfacing

### Rotation logic
Prioritize Posts by:
1. current player Deepening
2. current Strata availability
3. Confidence Trail relevance
4. route pressure
5. library freshness
6. unresolved prior chains
7. event cycle relevance

---

## Character scaling model for 120+ Wanderers
The safest model is not “120 equal NPCs.”
It is a layered library.

### Layer A — evergreen utility Wanderers
Recurring functional NPCs who can reappear often.
Examples:
- Peddlers
- Scrappers
- Messengers

### Layer B — recurring named Wanderers
Characters with partial memory, recurring asks, and increasing cross-system ties.
These should feel like the backbone of the roaming cast.

### Layer C — chain Wanderers
Appear for 2–6 Posts, then transform, vanish, defect, or return later.

### Layer D — event Wanderers
Tied to Lucky Draw Week, Treasure Week, Echo Week, or Festival Ledger windows.

### Layer E — Deepening Wanderers
Characters whose meaning changes after Deepening or who become truth-bearing.

### Layer F — rare Oddfolk
Used for tone shocks, foreshadowing, or memorable long-tail content.

This keeps the cast expandable without flattening every new arrival into “another visitor.”

---

## Post memory rules
Posts should remember more than completion state.

Track:
- whether the player helped or refused
- whether the player took payment
- whether a truth was concealed or shared
- which Confidant approved or disapproved
- whether this Post links to a known Wanderer/Runaway/Wild One
- which Strata the event destabilized

That way, a later Post can say:
- this Wanderer remembers you shorted them
- this Runaway trusted you last time
- this Confidant sent you the name because you chose silence earlier
- this Wild Post escalates because a previous route was ignored

That is how “120+ sources” turns into compounding texture instead of noise.

---

## Channel-by-channel production rules

### Burrow Posts production rules
Best for high volume.
These can be produced quickly if they use good templates.

Strong content bricks:
- supply shortfall
- tool break
- route complaint
- civic dispute
- misplaced item
- odd request
- repair need
- low-level rumor verification

Avoid:
- turning every Burrow Post into comedy filler
- making them all interchangeable errands

---

### Confidant Posts production rules
Lower volume, higher meaning.

Strong content bricks:
- private concern
- political request
- research anomaly
- personal favor
- trust test
- buried clue hint
- Turned Confidant complication

Avoid:
- generic friendship quests
- writing them as exposition dumps

---

### Stranger Posts production rules
Medium to high volume.
These are the best place to grow a large cast.

Strong content bricks:
- offer with hidden cost
- route escort
- region-bound trade need
- item swap
- local warning
- rumor packet
- one-step moral choice
- recurring character callback

Avoid:
- every Stranger sounding like a merchant
- too much sameness between Peddlers, Drifters, Messengers, and Oddfolk

---

### Wild Posts production rules
Lower frequency, higher pressure.
These create spikes in urgency.

Strong content bricks:
- target spotted
- route unsafe
- raid signal
- containment broken
- pursuit opportunity
- chase interruption
- false alarm with consequences

Avoid:
- flooding the player with endless panic copy
- making Wild Posts the only source of meaningful tension

---

## Recommended Post templates

### Template 1 — simple Burrow Post
- Headline
- who posted it
- what is needed
- one primary task
- one optional flavor complication
- reward
- expiration window

### Template 2 — Stranger offer
- who arrived
- what they offer
- what they want
- what feels slightly off
- accept / decline / inspect branch
- follow-up result state

### Template 3 — Confidant request
- confidant identity
- private context
- task with emotional or political edge
- trust impact
- possible clue hook

### Template 4 — Wild warning
- threat source
- last known location
- urgency framing
- route or target pressure
- recommended response
- possible escalation if ignored

### Template 5 — Deep chain Post
- callback to earlier character or event
- contradiction or remembered detail
- task across multiple systems
- hard choice
- delayed consequence

---

## Reward philosophy
Posts should not all pay out the same way.

### Burrow Posts
Usually reward:
- Mooncaps
- Mushcaps
- minor Standing
- building support materials

### Confidant Posts
Usually reward:
- Trust
- Confidence Trail progress
- Calling progress
- Buried Clue fragments
- special items

### Stranger Posts
Usually reward:
- flexible resources
- item access
- Field Cards
- Treasure Tickets
- route information

### Wild Posts
Usually reward:
- pressure relief
- Standing
- combat materials
- Crack Coins
- threat reduction
- rare chase-specific drops

### Deepening-bearing Posts
Can reward:
- Strata Seals
- remembered records
- story-state changes
- rare Confidant or Wanderer advancement

---

## Escalation model over time
The Post system should evolve over the first two years like this:

### Early phase
- teach surfaces
- establish tone
- build a small recurring cast
- keep Posts readable and funny

### Middle phase
- increase overlap between channels
- let recurring Wanderers collide with Runaways and Cliques
- begin stronger trust and truth consequences

### Late live-service phase
- use remembered history
- reactivate old characters with changed roles
- create chain interference
- make Deepening and Memory Shift alter the meaning of older Posts

This is how the game can reach a “99 out of 120+ visitor” feeling without simple inflation.
The content should feel denser because old names keep mattering, not because the surface is buried in junk.

---

## Production recommendation
For scalable authoring, build Posts as a mix of:
- **templates**
- **named character packets**
- **chain flags**
- **Strata-specific overlays**
- **event-week overlays**
- **Deepening overlays**

This lets one core Stranger Post template generate many distinct outcomes depending on:
- who sent it
- where it appears
- which Deepening the player is in
- what older choices were made
- whether an event week is active

---

## Recommended launch targets
A good launch Post library should feel broad enough to grow.

Suggested launch shape:
- 30–40 Burrow Posts
- 12–20 Confidant Posts
- 25–35 Stranger Posts
- 12–18 Wild Posts
- 8–12 Smoke Whisper variants
- 10–15 Buried Clue packets

That gives enough breadth for rotation and memory while leaving huge room for future compounding.

---

## Canon rule for Posts
When writing or designing a Post, ask:
1. which channel is this
2. what Duty does it deliver
3. what Encounter makes it memorable
4. what future chain can remember it
5. what world pressure does it reveal

If it cannot answer those questions, it is probably filler.

---

## Immediate next-file recommendation
The strongest next companion file is:
- `burrow_posts_examples.md`

That file should contain:
- 10 example Burrow Posts
- 10 example Stranger Posts
- 10 example Confidant Posts
- 10 example Wild Posts
- 5 example Smoke Whispers
- 5 example escalating recurring Wanderer chains

That would turn this architecture into a usable writing pack.
