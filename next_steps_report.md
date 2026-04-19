# Gnome Survivor — Next Steps Report

## Purpose
This report lays out the next recommended steps after the work completed in this chat.

It is designed to answer one question:

**What should be done next, in what order, and why?**

The answer below assumes the current documentation stack is the approved conceptual foundation.

---

## 1. Recommended direction

The project should now pause broad lore expansion and move into **build-enabling artifacts**.

The next phase should focus on:
- economy model
- schema design
- backend decision
- MVP scope definition
- unlock/UI flow
- content production templates

This is the transition from:
- concept docs
- naming docs
- content bibles

into:
- implementation docs
- balancing docs
- production docs
- prototype specs

---

## 2. Immediate next milestone

## Milestone A — Production Foundation

The goal of this milestone is to convert the current design stack into something a game team or solo builder can actually prototype against.

### Deliverables in this milestone
1. `economy_model_v1.md` or spreadsheet equivalent
2. `data_schema_v1.md`
3. `backend_architecture_options.md`
4. `core_loop_mvp_spec.md`
5. `unlock_flow_and_ui_map.md`
6. `content_table_templates.md`

These are the six highest-value files to create next.

---

## 3. Priority order

## Step 1 — Economy model v1

### Why this is first
The current doc stack defines many currencies and progression loops, but does not yet define the actual balancing math.

Without that, it is impossible to know:
- how fast Burrow growth should feel
- whether events are too generous or too stingy
- how long Deepening gates should take
- how often players can engage with The Crack
- whether F2P event completion is realistic
- whether old systems stay relevant over time

### This file should define
- every current currency
- faucet sources
- sinks
- daily/weekly gain assumptions
- hoarding windows
- first-pass event completion targets
- player segment assumptions:
  - casual
  - daily engaged
  - disciplined saver
  - light spender

### Core currencies to cover
- Mooncaps
- Glowcaps
- Mushcaps
- Favor Marks
- Crack Coins
- Strata Seals
- Echoes / Echo Shards / Elder Books
- Lucky Draws
- Treasure Tickets

### Key questions this file should answer
- What does a normal daily session produce?
- What does one week of disciplined saving produce?
- What should be purchasable with each currency?
- What must remain scarce?
- Which currencies can convert indirectly into others?
- What is the intended pressure point for each resource?

### Acceptance criteria
- every currency has a role
- every currency has at least one faucet and one sink
- event math supports first-pass balancing assumptions
- no major system depends on undefined resource flow

---

## Step 2 — Data schema v1

### Why this is second
The project now has a strong content language, but there is not yet a formal data model for how all these systems exist as game objects.

Without schema work, content will sprawl and prototype implementation will drift.

### This file should define
- major entities
- relationships between entities
- persistent player state
- event-state tracking
- post/encounter generation hooks
- trust / confidence progress state
- Deepening state
- seasonal content flags

### Core schema objects to include
- account
- gnome_survivor
- burrow_state
- strata_progress
- strain_progress
- confidant
- confidant_trust
- burrowfolk_unit
- treasure
- clothes_item
- clique_state
- post_state
- encounter_state
- event_state
- deepening_state
- memory_shift_state
- crack_state

### Key questions this file should answer
- what is owned vs equipped vs unlocked?
- what is permanent vs repeatable?
- what is global vs per-Stratum?
- how are recurring cast members stored?
- how are Burrow Posts generated, tracked, resolved, and escalated?
- how are live-service additions safely introduced later?

### Acceptance criteria
- every major system has a schema home
- recurring content can scale without ad hoc fields
- Deepening and seasonal expansion can be layered cleanly

---

## Step 3 — Backend architecture options

### Why this is third
This project is structurally a live-service game.
That means backend decisions matter early.

The team needs to decide whether the first prototype should use:
- Firebase Auth + custom backend
- PlayFab
- Unity Gaming Services
- a hybrid model

### This file should define
- candidate stack options
- pros and cons
- cost/complexity tradeoffs
- anti-cheat considerations
- content delivery / remote config options
- event scheduling options
- save sync approach

### Core questions this file should answer
- what can be safely client-side?
- what must be server-authored?
- how should rotating events be configured?
- how should season data be delivered?
- how are economy-sensitive actions verified?
- what is the cheapest viable prototype architecture?

### Acceptance criteria
- one preferred stack selected
- reasons documented clearly
- prototype path identified
- future migration risks noted

---

## Step 4 — Core loop MVP spec

### Why this is fourth
The project now needs a sharply limited first playable slice.

The goal is not to prototype the whole game.
The goal is to prototype one tight, convincing loop.

### Recommended MVP slice
- The Burrow base loop only
- Loamwake only
- one Pathwheel starter setup
- one Confidant unlock path
- one Burrowfolk class
- one Warden
- one Burrow Post chain
- one Lucky Wheel lane
- one simple Treasure upgrade loop
- one first Deepening gate stub

### This file should define
- exact features in scope
- exact features deliberately out of scope
- first-session flow
- first-day flow
- first-clear flow
- tutorial targets
- proof of fun criteria

### Core questions this file should answer
- what is the first 10 minutes?
- what is the first 60 minutes?
- what is the first daily return?
- what is the first meaningful decision?
- what is the first long-term hook?

### Acceptance criteria
- prototype scope is small enough to build
- core fantasy is visible quickly
- no unnecessary launch-scope bleed

---

## Step 5 — Unlock flow and UI map

### Why this is fifth
The current naming and content structure is rich.
That is a strength, but it also creates risk.

If too much is visible too early, onboarding will collapse.

### This file should define
- minute-1 visible modules
- day-1 unlock order
- day-2/day-3 unlock logic
- Loamwake-clear unlocks
- when Clique appears
- when The Crack appears
- when Vault of Treasures appears
- when The Ascender appears
- when Strain Hall appears in meaningful form

### Core questions this file should answer
- what should the player see first?
- what should remain hidden?
- what unlock should feel exciting rather than overwhelming?
- what systems should be backgrounded until the player has context?

### Acceptance criteria
- launch readability is protected
- system density ramps instead of dumping
- red-dot / notification overload is avoided

---

## Step 6 — Content table templates

### Why this is sixth
The current docs are strong conceptually, but production will need table-based content templates.

This is where the project begins preparing for real implementation throughput.

### This file should define reusable templates for
- Strata zones
- Wardens
- Buried Clues
- Wanderers
- Runaways
- The Wild Ones crews
- Confidants
- Burrowfolk units
- Treasures
- Clothes
- event ladders
- Festival Ledger tracks
- Burrow Posts
- Duties

### Each template should specify
- content ID
- category / family
- unlock condition
- reward identity
- escalation rule
- recurrence rule
- related Strain
- related Stratum
- related event / season tags

### Acceptance criteria
- a content designer can add new entries without inventing format each time
- the project can scale live-service content cleanly
- cross-file drift is reduced

---

## 4. Suggested follow-up milestone after that

## Milestone B — Prototype Readiness

Once the six foundation files above exist, the next milestone should be:

1. `ui_navigation_and_screen_list.md`
2. `loamwake_mvp_content_sheet.md`
3. `first_confidant_chain.md`
4. `first_wanderer_pool.md`
5. `lucky_draw_week_mvp.md`
6. `burrow_resources_balance_notes.md`
7. `save_state_and_profile_flow.md`
8. `analytics_and_telemetry_notes.md`

This milestone converts abstract structure into a real prototype content pack.

---

## 5. Key production risks to actively manage

### Risk 1 — Too much concept work, not enough implementation prep
You now have a strong design library.
The risk is continuing to add lore and naming without producing build documents.

### Risk 2 — Economy drift
The current design uses many currencies and progression lanes.
Without a balancing pass, the pressure structure could easily become muddy.

### Risk 3 — Backend complexity underestimated
The game’s recurring cast, event loops, and live-service cadence will create backend requirements earlier than a simple single-player idle game.

### Risk 4 — Prototype scope creep
It will be tempting to put too many of the exciting systems into the MVP.
That would be a mistake.
The first prototype should prove the fantasy, not the whole roadmap.

### Risk 5 — Content pipeline inconsistency
Without table templates and schema discipline, content will sprawl into document-only ideas that are hard to implement.

---

## 6. Recommended immediate work order

If this project continues in the current doc-building format, the recommended next file creation order is:

1. `economy_model_v1.md`
2. `data_schema_v1.md`
3. `backend_architecture_options.md`
4. `core_loop_mvp_spec.md`
5. `unlock_flow_and_ui_map.md`
6. `content_table_templates.md`

If the project instead shifts to prototype implementation immediately, then the minimum first set should be:

1. economy model
2. data schema
3. MVP scope

That is the smallest usable production trio.

---

## 7. Short version

### What the project has now
- strong lore spine
- strong naming layer
- strong content taxonomy
- strong scaling framework
- strong seasonal concept framework

### What it needs next
- balancing math
- object schema
- backend decision
- MVP scope control
- unlock-flow clarity
- production content templates

### Best next move
Create **`economy_model_v1.md`** first.

That is the highest-value next artifact because it will force the project to answer real questions about pacing, saving, scarcity, and event structure before implementation starts.

---

## 8. Final recommendation

Do not immediately create more lore bibles.

Use the current documentation stack as the **creative foundation**, and spend the next phase on the **production foundation**.

That is how this project moves from:
- strong idea
- strong docs

into:
- strong prototype
- strong build path
