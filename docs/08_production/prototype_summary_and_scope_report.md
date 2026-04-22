# Prototype Summary and Scope Report

Date: 2026-04-22
Covers: Sprint 0 through Sprint 7 (commits 7a66c34 through 7795237)
Status: Manual verification PASSED

---

## Systems Implemented

### Burrow
- Local-first profile bootstrap with deterministic first-run defaults
- Dewpond (Mooncap output), Mushpatch (Mushcap output) with timed accumulation and storage caps
- Gather actions move stored output into wallet
- Expand spends Mooncaps, raises Burrow level, unlocks Rootmine at level 2
- Fixed header with wallet, guide text, and navigation; scrollable body for summaries and debug

### Loamwake
- Strata Gate with Loamwake as the only selectable Stratum
- Three zones: Rootvine Shelf, Mudpipe Hollow, Glowroot Passage
- Safe/risky route choice costing Mushcaps
- Deterministic Auto-Clash resolution with zone difficulty thresholds
- Loamwake material wallet (Tangled Root Twine, Crumbled Ore Chunk, Dull Glow Shard)
- First Keeper capstone tied to Zone 3

### First Warden (The Mudgrip)
- Warden unlock after Mudpipe Hollow first clear
- Deterministic Warden clash with a stronger threshold
- Persistent defeat state and clear reward
- Loamwake Dirt Cap (first Hat) unlocked on Warden defeat

### Fixture / Hat / Vault
- Rootmine material production after Burrow level 2
- First Fixture (Root-Bitten Shovel Strap) crafting and ordered-list equip
- Fixture cap checks with visible expedition power bonus (+2)
- First Hat (Loamwake Dirt Cap) as permanent passive (+1) outside Fixture cap
- Vault of Treasures as a shell-only page with no treasure progression

### Burrow Post / Greta / Duties
- Burrow Post surface with utility Post and Greta intro Post
- Greta unlock path through `post_lw_005_greta_intro`
- First Trust follow-up step after Greta intro
- Minimal Daily Duties loop (Dewpond gather, Loamwake explore, first Fixture check)
- Persisted duty progress and auto-claimed rewards

### Rootrail Reveal Shell
- Rootrail reveal/station shell gated by Greta follow-up and Mudpipe Hollow first clear
- Shell-only: no repair progression, no timer, no Forgotten Manual consumption
- Back navigation and clear stub messaging

### Lucky Draw Week / Lucky Stall / Festival Ledger
- Event state gated behind Greta unlock (prototype proxy for Day 15 eligibility)
- Weekly free ticket claim
- Controlled pull table with deterministic rewards
- Festival Ledger free lane with four pull-count milestones
- Lucky Stall with Mooncap-to-Lucky-Draw exchange and weekly limits
- No paid paths, IAP, or broader liveops scheduling active

### The Crack
- Visibility gated behind Rootrail reveal
- Repeatable `Probe the Crack` action incrementing depth and Crack Coins
- Persistent probe state and reward summary
- Shell-only: no ladder, sectors, or Deepening mechanics

### Clique
- Visibility gated behind Rootrail reveal
- Local Clique stub with one-time stipend granting Favor Marks
- Placeholder Clique Rolls roster
- Great Dispute flagged as stub-only
- No networking, chat, invites, or shared state

### Save / Load / Migrations / Debug Surfaces
- Local-first JSON save at `Application.persistentDataPath/profile.json`
- Autosave on every mutation
- Final save on app pause/quit
- Additive migration path from Sprint 0 through Sprint 7 state shapes
- Legacy profile migration with safe defaults for new fields
- Debug/status areas on every major page showing auth state, save state, and relevant system internals
- Tutorial guide state persisted with current step, completed steps, dismissed hints, and reset support

---

## Intentionally Stub-Only

These surfaces exist in the prototype but have no gameplay depth:

- Rootrail repair progression (station revealed, no repair steps or timers)
- Vault of Treasures (page exists, no treasure content or progression)
- The Crack ladder and sectors (probe only, no tiered progression)
- Clique backend and social systems (local stipend only)
- Great Dispute (stub flag only, no gameplay)

---

## Intentionally Out of Scope

These systems are not in the prototype at all:

- Strata 2 through 5 (only Loamwake exists)
- Deepening / Memory Shift
- Treasure progression and Treasure Week
- Echo Week
- War Armory and War Fixtures
- Confidant depth beyond Greta first Trust step
- Burrowfolk deployment
- Full Rootrail repair chain (steps 2+)
- IAP / real-money purchases / store SDKs
- Networking / multiplayer / shared state
- Cloud save / merge
- Cosmetics / hat display / visual customization
- Full liveops event scheduling

---

## Document Version

v1.0.0 — 2026-04-22
