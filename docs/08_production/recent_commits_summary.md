# Recent Commits Summary

Date: 2026-04-22
Purpose: PM-readable summary of the major Sprint 0–7 commit trail

---

## 7a66c34 — Sprint 0: Unity scaffold + local-first persistence shell

Created the Unity project structure under `src/unity/` and implemented local-first profile persistence. The game boots into MainScene, creates a default `profile.json` on first run, reloads it on restart, and autosaves on every mutation. Auth is abstracted but optional — failed auth falls back to a local UID and does not block boot. A plain debug UI shows Mooncaps, a Gather button, save/reload controls, and system status.

---

## 33cbc16 — Sprint 1: Burrow core gather loop, expand, Rootmine unlock shell

Added the first Burrow gameplay layer. Dewpond produces Mooncaps, Mushpatch produces Mushcaps, both accumulate in storage and transfer to the wallet on Gather. Expand spends Mooncaps, raises Burrow level, and unlocks Rootmine at level 2 (Rootmine itself is a locked shell at this point). The Sprint 0 proof panel was replaced with a readable Burrow screen showing wallet, building cards, and debug status.

---

## 6d88b64 — Loamwake back-nav fix

Added a dedicated `Back to Burrow` button at the top of the Loamwake page so the player can always return to the Burrow without scrolling past zone cards and debug output. The bottom navigation and debug area were left intact.

---

## a952a0d — Sprint 2: Loamwake exploration, route choice, Keeper capstone

Built the first Loamwake exploration slice. Players enter Loamwake from a Strata Gate on the Burrow, select safe or risky routes costing Mushcaps, and resolve encounters through deterministic Auto-Clash. Three zones unlock sequentially. A first Keeper capstone appears after Zone 2 clear, with a stronger difficulty threshold and persistent defeat state. Loamwake material wallet fields were added for exploration drops.

---

## f4b30d4 — Sprint 3: First Fixture loop, Hat unlock, Rootmine materials, Vault shell

Implemented the first equipment power-spike loop. Rootmine produces materials after Burrow level 2. The first Fixture (Root-Bitten Shovel Strap) can be crafted and equipped from a Fixture Workshop, adding expedition power. The first Hat (Loamwake Dirt Cap) is a permanent passive bonus separate from the Fixture cap. A Vault of Treasures page was added as a shell with no treasure content. Loamwake expedition power now includes Fixture and Hat bonuses.

---

## b23c815 — Sprint 4: Burrow Posts, Daily Duties, Greta unlock, Rootrail reveal

Added the first social/return-reason layer. Burrow Post surface shows utility posts and a Greta intro post triggered after Rootvine Shelf clear. Greta unlocks as the first Confidant with an initial Trust step. A minimal Daily Duties loop tracks Dewpond gather, Loamwake explore, and first Fixture check. Rootrail reveal/station shell appears after Greta follow-up and Mudpipe Hollow clear, but has no repair progression.

---

## 9f05e8e — Sprint 5: Lucky Draw Week, Lucky Stall, Festival Ledger free lane

Built the first event-layer vertical slice. Lucky Draw Week is prototype-gated through Greta unlock (standing in for the Day 15 eligibility gate). Players claim a weekly free ticket, pull from a controlled reward table, and progress a Festival Ledger with four pull-count milestones. A Lucky Stall allows Mooncap-to-Lucky-Draw exchange with weekly limits. No paid paths, IAP, or liveops scheduling are active.

---

## 818258a — Sprint 6: Crack probe loop and Clique stub shell

Added two future-facing progression shells. The Crack is a repeatable probe action incrementing depth and Crack Coins, gated behind Rootrail reveal. Clique is a local stub with a one-time stipend granting Favor Marks and a placeholder roster, also gated behind Rootrail reveal. Great Dispute is flagged as stub-only. No networking, shared state, or multiplayer systems are active.

---

## 8091b49 — Sprint 7: Tutorial wiring, stabilization, tester proof pack

Stabilized the full prototype for human testing. Added lightweight persisted tutorial guide state with step tracking and dismiss/reset support. Burrow and long debug pages were reworked with a fixed header and scrollable body pattern so controls stay reachable at default Game view. Fresh profiles now start with gatherable output. First-clear and Warden rewards were tuned. The Mudgrip Warden presentation was cleaned up. An operator smoke checklist, 5-tester checklist, and bug report template were prepared.

---

## 7795237 — Burrow layout/navigation/readability fix

Follow-up layout fix ensuring the Burrow main screen is navigable and readable at the default Unity Game view. Applied the same fixed-back-navigation plus scrollable-body pattern consistently.

---

## Document Version

v1.0.0 — 2026-04-22
