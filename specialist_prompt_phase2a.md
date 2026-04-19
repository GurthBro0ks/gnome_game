# Phase 2A Specialist Prompt — Systems Designer

## Role
You are the Systems Designer for the Gnome Survivor project. Your job is to convert the completed conceptual design stack into 6 build-enabling production documents.

## Context
You have access to the full project documentation in `/docs/`. The canonical source of truth is:
- `master_glossary.md` — all naming decisions
- `economy_model_v1.md` — economy philosophy and target bands
- `economy_and_progression_templates.md` — faucet/sink/gate framework
- `system_interactions_matrix.md` — how systems feed each other
- `phase1_content_bible.md` — 5 launch Strata
- `strains_bible.md` — 5 Strains
- `deepening_arc.md` — prestige/Deepening structure
- `clique_confidants_bible.md` — social layer
- `event_content_templates.md` — event architecture
- `content_scaling_rules.md` — 100+ cast scaling plan
- `implementation_planning_pack.md` — system architecture, dependencies, build order

## Agreed Design Direction (DO NOT CHANGE)
- Funny post-fallout gnome world
- 5 launch regions (Strata): Loamwake, Ledgerhollow, Memory Fen, Wayworks, The Buried Court
- 5 Strains: Freeroot, Inkroot, Brassroot, Crownroot, Hushline
- 2-week beginner event + 2-week hoard event, then 3 rotating weekly events
- Clubs (Cliques) in v1, species-war (Great Dispute) stubs only
- Hats as the hero slot within the Clothes system
- Separate Confidants (partners) and Burrowfolk (minions)
- Multiple placement boards (Lucky Wheel, Treasure Finder, Ancestral Depictor, Pathwheel)
- One active Strata Trait at a time
- Deepening as prestige with Rift structure (Deepening + Memory Shift + The Crack)
- The Crack as never-ending progression (Fissure equivalent)
- Cosmetics/light monetization, no premium-only power items at launch

## Rules
- Do not rename the game or any locked terminology
- Do not overcomplicate v1 — recommend the simplest launch-safe version of each system
- Keep hidden content and Easter eggs as expandable layers
- Mark anything still unresolved with `[UNRESOLVED]`
- Use the exact currency/system names from master_glossary.md
- Version all output documents

---

## Task 1: economy_model_v1_sheet.csv

Convert economy_model_v1.md target bands into concrete prototype numbers.

### Requirements
Create a CSV with these columns:
- currency_name
- role
- daily_faucet_casual
- daily_faucet_engaged
- daily_faucet_saver
- daily_faucet_spender
- weekly_total_engaged
- main_sink
- main_sink_cost_early
- main_sink_cost_mid
- main_sink_cost_late
- scarcity_tier (LOW/MEDIUM/HIGH/VERY_HIGH)
- cap_type (NONE/SOFT/HARD)
- cap_value
- notes

### Currencies to cover
Mooncaps, Glowcaps, Mushcaps, Lucky Draws, Treasure Tickets, Echoes, Echo Shards, Elder Books, Crack Coins, Bronze Shovels, Favor Marks, Strata Seals, Festival Marks, Strain Seeds, Treasure Shards, Polishes, Clothes Materials, Ascender Parts

### Resolve these open questions with prototype-safe defaults:
1. Mooncap curve for Burrow upgrades: use exponential with base 1.15x per level
2. Treasure vs Clothes power split: 60% Treasures / 40% Clothes for total account power
3. Strain progression speed: 1 meaningful Strain level per Stratum cleared
4. Bronze Shovels rate: 1-2 per day from routine play, enough for 1 Crack run every 2-3 days
5. Glowcap faucet: milestone-only at launch, no Tickworks trickle
6. Elder Books: 1 per complete event cycle for disciplined F2P
7. Favor Marks minimum: 50/day from active Clique participation
8. Strata Seals per Deepening: 3-5 earned per band, 10 needed per gate
9. Currency caps: no hard caps at launch, soft warnings at 10x weekly income
10. Pity system: Lucky Wheel guaranteed SR at 50 pulls, SSR at 100 pulls

---

## Task 2: data_schema_v1.md

Define the game object model for all major systems.

### Requirements
For each entity, define:
- entity name
- key fields (name, type, constraints)
- relationships to other entities
- persistence model (permanent / session / event-scoped / Deepening-scoped)
- scaling notes (how this entity grows with live-service additions)

### Core entities to define:
1. account — player identity, created_at, last_login, deepening_level
2. gnome_survivor — player character state, battle_stats, heritage_stats, standing
3. burrow_state — building levels, output rates, unlocked modules
4. strata_progress — per-stratum: current_zone, cleared, strata_trait_active, warden_defeated
5. strain_progress — per-strain: level, seeds_invested, active_bonuses
6. confidant — id, name, role, strain_affinity, stratum_origin
7. confidant_trust — confidant_id, trust_level, calling_unlocked, backstory_progress
8. burrowfolk_unit — id, class, level, equipped, assigned_work, health
9. treasure — id, family, rarity, set_status, polish_level, strengthen_tier, attune_status, harmony
10. clothes_item — id, slot (HEAD/BODY/HANDS/FEET/ACC), rarity, reinforce_level, bind_status
11. clique_state — clique_id, role (founder/steward/burrowmate), favor_marks, queue_progress
12. post_state — post_id, channel, sender, status (active/resolved/escalated), reward_claimed
13. encounter_state — encounter_id, type, stratum, outcome, timestamp
14. event_state — event_type, cycle_number, currency_spent, ladder_tier, festival_marks_earned
15. deepening_state — current_deepening, seals_collected, buried_requirements_met, burrow_transformations
16. memory_shift_state — shift_encounters_seen, shift_wardens_defeated, intensity_level
17. crack_state — deepest_floor, crack_coins_total, bronze_shovels_held

### Key questions to answer:
- owned vs equipped vs unlocked: define status enum per entity type
- permanent vs repeatable: mark each entity
- global vs per-Stratum: mark scope
- how Burrow Posts are generated: define trigger conditions and template references
- how live-service additions work: define versioned content IDs and remote config hooks

---

## Task 3: backend_architecture_options.md

Evaluate and recommend a backend stack.

### Candidates to evaluate:
1. Firebase Auth + Firestore + Cloud Functions
2. PlayFab (Microsoft)
3. Unity Gaming Services (UGS)
4. Hybrid: Firebase Auth + custom Node.js/Python backend + PostgreSQL

### For each candidate, assess:
- setup complexity (1-5)
- cost at 0-10K DAU
- cost at 100K DAU
- anti-cheat capability
- remote config / event scheduling
- save sync reliability
- content delivery (seasonal data, event data)
- migration risk if switching later
- community/documentation quality

### Reference data:
- Super Snail (reference game) uses Unity with IL2CPP
- This game will likely use Unity
- Live-service features needed from day 1: event scheduling, save sync, remote config
- Economy-sensitive actions that need server authority: gacha pulls, currency transactions, event scoring

### Output: select one preferred stack with clear reasoning.

---

## Task 4: core_loop_mvp_spec.md

Define the exact scope of the first playable prototype.

### IN SCOPE:
- The Burrow: Dewpond, Mushpatch, Rootmine, Tickworks, Burrow Exchange, Cloth Assembly
- Loamwake: full region with 1 Warden, auto-clash, route choice
- 1 Confidant: Freeroot Pathfinder
- 1 Burrowfolk class: Hands
- Burrow Posts: 15-20 templates (Burrow + Stranger channels only)
- Daily Duties: 3 per day
- Clothes: head (hat) + body + hands slots, Reinforce loop
- Treasures: 5 starter treasures, Set/Polish loop
- Pathwheel: basic socketing
- Lucky Draw Week: 1 full cycle
- Lucky Stall: 4 items
- Festival Ledger: free lane only
- The Crack: entry, 10 floors, Crack Market with 5 items
- Clique: create/join stub, Clique Queue with 1 shared goal

### OUT OF SCOPE:
- Strata 2-5 (Ledgerhollow through Buried Court)
- Memory Shift encounters
- Remembered Wardens / Shift Wardens
- Great Dispute
- The Ascender
- Smoke Whispers
- Buried Clues (beyond 1 tutorial chain)
- Treasure Week / Echo Week (Lucky Draw only for MVP)
- Advanced Strain Loom
- Field Cards / Findings Exchange
- Burrow Games
- Standing Shelf leaderboards
- Turned Confidants
- Premium Festival Ledger lane

### Define:
- First 10 minutes (tutorial flow)
- First 60 minutes (system discovery)
- First daily return (retention hook)
- First meaningful decision (build choice)
- First long-term hook (what keeps them coming back)
- Proof of fun criteria (what success looks like in testing)

---

## Task 5: unlock_flow_and_ui_map.md

Map every module's visibility state from minute 0 through Loamwake clear.

### Format per module:
- module_name
- unlock_trigger
- visibility_before_unlock (hidden / grayed / teased)
- first_interaction_prompt
- red_dot_rule
- priority (CRITICAL / HIGH / MEDIUM / LOW)

### Unlock timeline to define:
1. Minute 0-1: what is on screen
2. Minute 1-5: tutorial unlock sequence
3. Minute 5-15: first session discoveries
4. Day 1 end: full day-1 module set
5. Day 2-3: progressive unlocks
6. Loamwake clear: everything available for launch scope

### Anti-overload rules:
- Maximum 3 new modules revealed per session
- Red dots capped at 5 simultaneously
- No module should appear without context for why it matters
- Hidden systems remain fully hidden until trigger (not grayed/teased)

---

## Task 6: content_table_templates.md

Create spreadsheet-ready templates for all content types.

### Template format (per content type):
- content_id (format: TYPE_STRATUM_NNN, e.g., WAN_LOAM_001)
- category
- subcategory
- name
- strain_affinity
- stratum_origin
- unlock_condition
- reward_lane
- reward_items
- escalation_rule
- recurrence_rule
- event_tag (if event-linked)
- season_tag (if season-linked)
- deepening_tag (pre/post which Deepening)
- notes

### Content types to template:
1. Wanderers (with subtype column)
2. Runaways (with subtype column)
3. Wild Ones (with subtype + threat tier)
4. Confidants (with role + calling)
5. Burrowfolk (with class)
6. Wardens (with tier: Keeper/Claimant/Warden)
7. Treasures (with family + board + rarity)
8. Clothes (with slot + rarity)
9. Burrow Posts (with channel + sender_family)
10. Duties (with tier: Surface/Social/Strange/Deepening)
11. Buried Clues (with chain_id + step)
12. Event ladders (with week_type + tier)
13. Festival Ledger tracks (with cycle + lane)
14. Strata zones (with stratum + zone_number + content_hooks)

### Include 3 example rows per template.

---

## Output Rules
- Version each document: v0.1.0
- Mark status: DRAFT
- Include date and author
- Mark unresolved items with `[UNRESOLVED]`
- Use master_glossary.md terminology exclusively
- Keep v1 simple — recommend simplest launch-safe version, note expansion paths separately
- Every document must pass: "Could a developer build from this without asking me questions?"
