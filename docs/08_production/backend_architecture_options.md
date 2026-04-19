# Backend Architecture Options

**Status:** Canon — Phase 2A Salvage Rewrite  
**Version:** 1.0.0  
**Date:** 2026-04-19  
**Scope:** Stack evaluation for Gnome Game prototype through live-service. Decision-facing document for technical leads.

---

## Status, Purpose, Decision Frame

This document evaluates realistic backend paths from local prototype (Loamwake MVP, no backend) through a live-service build. It replaces the generic microservices recommendation from the original placeholder.

**Primary question:** What should we build on, and when?

**Decision frame:**
1. Pre-prototype: No backend needed. Everything is local saves.
2. Prototype to closed beta: Lightweight cloud save + remote config enough.
3. Live-service launch: Real economy server, timer authority, content delivery, anti-cheat.

The recommendation is: **start with Option A, plan for Option B, migrate at closed-beta gate.**

---

## Prototype Assumptions

The Loamwake MVP build makes these technical assumptions:

- Single player, no multiplayer (except Clique — stub)
- All data lives in local save files (JSON or SQLite)
- Economy is not exploitable by cheating (prototype; cheating is accepted risk)
- No real-money transactions in prototype
- Remote config is simulated via local JSON files
- Content is bundled into the build (no CDN)

These simplify the first build substantially. No backend is required to ship a playable MVP.

---

## What Can Be Local in MVP

| System | Why Local is Fine |
|--------|------------------|
| Fixture loadout | Pure player preference; no economy impact |
| Hat display state | Cosmetic only |
| Strata progress / zone clears | Single-player; no cheating exploit |
| Confidant bond state | Single-player story |
| Burrow work (Burrowfolk output) | Can use client timestamp for MVP; server-needed later |
| Daily Duty reset | Client clock is acceptable in prototype |
| Tutorial flags | Persistent local flag |
| Wallet (Mooncap/Mushcap/Materials) | Prototype only; no real spend |
| Rootrail timer | **Semi-critical:** Client timestamp acceptable in prototype, server-required in live build |

---

## What Must Be Server-Authored Later

| System | Why Server Authority Is Required |
|--------|----------------------------------|
| Rootrail repair timers | Client timestamp manipulation is trivial; timers must have `timer_end_utc` set server-side |
| Wallet mutations (Glowcap, Lucky Draw, Treasure Ticket) | Premium-adjacent; must be server-validated on spend and grant |
| IAP fulfillment | All real-money grants must be server-verified via receipt validation |
| Event state (participation, tier claims) | Prevents multiple-claims and reward duplication |
| Clique state | Shared persistent data; cannot be client-owned |
| Leaderboard entries (Crack depth, Festival Ledger scoring) | Requires server aggregation |
| Anti-cheat for economy resources | Glowcaps, lucky draws, and all sink transactions |
| Content delivery (new events, patches) | Remote config and content flags for live-service pacing |

---

## Option A: Local JSON + Local Save Prototype

**Description:** All game state in local JSON files or a lightweight local DB (SQLite / PlayerPrefs). No internet required. Content is bundled.

**Use case:** Labs build, internal playtesting, proof-of-fun milestone.

| Aspect | Detail |
|--------|--------|
| Backend required | None |
| Save format | JSON or SQLite (local) |
| Timer handling | Client timestamp (acceptable for prototype) |
| IAP | Not applicable |
| Cheat exposure | All economy values client-accessible; accepted for prototype |
| Cost | $0 |
| Time to implement | Lowest |
| Risk | All data is local; no cloud save, no backup |

**Recommendation for:** All work until closed beta invite gate.

---

## Option B: PlayFab-Oriented Live-Service Path

**Description:** Microsoft PlayFab as the cloud backend. Handles player accounts, cloud save (Player Data / Title Data), economy (Virtual Currencies, Catalog), server-hosted timers via CloudScript/Azure Functions, IAP receipt validation (via PlayFab + store connectors), and analytics.

**Use case:** Closed beta onwards; first real-money build.

| Aspect | Detail |
|--------|--------|
| Player accounts | PlayFab account + custom ID link (no login barrier for mobile) |
| Cloud save | PlayFab Player Data (structured JSON blobs per player) |
| Economy | PlayFab Virtual Currencies (Mooncaps, Glowcaps, Lucky Draws, etc.) + Catalog v2 |
| Timer authority | CloudScript validates `timer_end_utc`; client cannot self-report completion |
| IAP | PlayFab IAP validation (iOS App Store + Google Play receipt check) |
| Remote config | PlayFab Title Data + Experiments for A/B |
| Content delivery | PlayFab CDN or hosted JSON bundles |
| Anti-cheat | Server-validates all economy spend/earn paths; suspicious patterns flagged via analytics |
| Analytics | PlayFab Analytics + custom events; can pipe to Azure Events Hub |
| Clique/social | PlayFab Groups API (fits Clique model well) |
| Cost | Free tier up to ~1,000 MAU; scales with usage — evaluate pricing at 10K MAU gate |
| Time to implement | Medium (2–4 weeks for core integration from prototype) |
| Risk | Vendor lock-in; PlayFab pricing changes; requires Azure credits |

**Gnome-specific implementation notes:**
- Wallet: each canonical currency becomes a PlayFab Virtual Currency (code 6 chars, e.g., `MNCP` for Mooncaps, `GLCP` for Glowcaps)
- Rootrail timer: CloudScript function `StartRailRepair(step_id)` sets `timer_end_utc`; `ClaimRailRepair(step_id)` validates server time before granting reward
- Hat unlocks: stored in Player Read-Only Data; set server-side when unlock condition met
- Forgotten Manuals: stored in Player Data as a codex map; set to `discovered: true` server-side on discovery event
- Fixture loadout: stored in Player Data as `equipped: [id, id, ...]`; validate `length <= account.fixture_cap` server-side before accepting

**Recommendation for:** Closed beta and launch if team has PlayFab experience or is on Unity.

---

## Option C: Firebase + Supabase + Cloud Functions Path

**Description:** Firebase Realtime DB or Firestore for player state, Firebase Authentication, Firebase Cloud Functions for server-side validation, Firebase Remote Config for content delivery. Supabase as a Postgres-backed alternative for structured relational data.

| Aspect | Detail |
|--------|--------|
| Player accounts | Firebase Auth (anonymous + social login upgrade) |
| Cloud save | Firestore (player document per UID) |
| Economy | Custom service layer in Cloud Functions |
| Timer authority | Cloud Function `startRepairTimer` writes server timestamp |
| IAP | Firebase Extension for receipt validation |
| Remote config | Firebase Remote Config |
| Analytics | Firebase Analytics / BigQuery |
| Clique/social | Custom Firestore subcollection; Clique documents |
| Cost | Firebase Spark (free) for prototype; Blaze (pay-as-you-go) for live |
| Time to implement | Medium; requires more custom economy work than PlayFab |
| Risk | Firestore costs spike with frequent reads; requires careful data structure design |

**Recommendation for:** Teams with existing Google Cloud / Firebase expertise. Good choice if the team prefers open ecosystem over PlayFab's opinionated structure. Supabase is preferred over raw Firebase if the data model needs strong relational queries (e.g., Clique leaderboards).

---

## Option D: Unity Gaming Services Path

**Description:** Unity Cloud Save, UGS Economy, UGS Remote Config, Unity Analytics, Unity IAP. All within Unity's ecosystem.

| Aspect | Detail |
|--------|--------|
| Player accounts | Unity Authentication (anonymous → upgrade) |
| Cloud save | Unity Cloud Save (key-value per player) |
| Economy | UGS Economy (currencies, inventory, virtual shop) |
| Timer authority | UGS Cloud Code for server-side timer logic |
| Remote config | UGS Remote Config |
| IAP | Unity IAP |
| Analytics | Unity Analytics |
| Cost | Free tier; scales; pricing subject to Unity business model changes |
| Time to implement | Low if using Unity Engine; all services native |
| Risk | Unity's reputation for pricing/licensing changes; less mature than PlayFab or Firebase |

**Recommendation for:** Teams building in Unity Engine who want native SDK cohesion and are comfortable with Unity's business stability.

---

## Option E: Custom Backend (Later Phase)

**Description:** Build a proprietary backend (e.g., Go/Node + Postgres + Redis). Full control, no vendor dependency, required eventually if the game scales to 500K+ MAU and economics of SaaS backends outweigh custom build cost.

**Not recommended before:** 100K MAU or a Series A funding event. Custom backend before this point is engineering overhead that delays shipping.

---

## Recommended Path

| Phase | Recommendation |
|-------|---------------|
| **Prototype (now → closed beta)** | **Option A — Local save.** Zero backend cost, zero delay. Build the loop, not the infrastructure. |
| **Closed Beta** | **Option B (PlayFab) or Option C (Firebase).** Choose based on engine (Unity → PlayFab; other → Firebase). Integrate cloud save + timer authority first. |
| **Soft Launch** | Add PlayFab/Firebase economy validation + IAP receipt validation. Add basic analytics event tracking. |
| **Global Launch** | Full server-side economy validation, Rootrail timer authority, Clique Groups API, leaderboard service, cheat telemetry. |
| **Scale (500K+ MAU)** | Evaluate Option E (custom backend) cost/benefit. |

---

## Economy and Anti-Cheat Boundaries

The following economy flows **MUST** be server-authoritative at live launch (not prototype):

| Flow | Reason |
|------|--------|
| Glowcap grants (any source) | Premium-adjacent; easily farmed if client-trusted |
| Lucky Draw pulls | Gacha — must be server-rolled, not client-rolled |
| Treasure Ticket grants | Premium event tokens |
| IAP fulfillment (any) | Legal requirement for Apple/Google receipt validation |
| Rootrail timer completion | Timer validation prevents acceleration cheating |
| Clique Favor Mark transactions | Shared social economy |
| Event tier claims | Anti-duplicate claim |

**Client-trusted (acceptable in live-service, monitored via telemetry):**
- Mushcap consumption per explore (client explore tap; server flags velocity anomalies)
- Fixture equip/unequip (no economy impact)
- Hat display selection (cosmetic)
- Loamwake zone exploration result (drops; server can re-validate via daily totals anomaly detection)

---

## Remote Config and Content Delivery

All live-service tuning values must be remote-configurable without a client update:

| Value | System |
|-------|--------|
| Rootrail repair step costs | Remote Config |
| Event active windows (start/end timestamps) | Remote Config |
| Lucky Draw week pool compositions | Remote Config / Title Data |
| Economy balance (Mooncap earn rates) | Remote Config |
| Fixture recipe availability | Remote Config gate flag |
| Hat unlock condition overrides | Remote Config |
| Content flags (enable/disable stub systems) | Remote Config |

---

## Timer Handling for Rootrail

**Prototype (Option A):**
```
timer_start = client_timestamp_now()
timer_end = timer_start + step_duration_seconds
// Store locally; display countdown client-side
// On collect: validate client_timestamp_now() >= timer_end → grant reward
```

**Live-service (Option B/C):**
```
// Client calls: POST /api/rootrail/start_repair { step_id }
// Server: validates player has parts, manual in codex, no active timer
// Server: writes timer_end_utc = server_utc_now() + step_duration_seconds
// Server: deducts Rootrail Parts from wallet (server-auth)
// Server: returns { timer_end_utc }
// Client displays countdown from that timestamp

// On collect: POST /api/rootrail/claim_repair { step_id }
// Server: validates server_utc_now() >= timer_end_utc → grants reward
// Server: advances rootrail_state.current_step to next step
```

Key: timer must never be client-computed in live. Use `timer_end_utc` as a server-issued constant, displayable countdown only.

---

## Inventory / Fixture / Hat / Manual State

| Data Type | Prototype Storage | Live Storage |
|-----------|------------------|-------------|
| Fixture inventory | Local JSON array | Player Data (cloud) |
| Fixture loadout (equipped) | Local JSON array | Player Data; validated length ≤ cap |
| Hat unlocks | Local JSON map | Player Read-Only Data (set server-side) |
| Hat display state | Local JSON | Player Data |
| Forgotten Manuals / codex | Local JSON map | Player Data; set server-side on discovery event |
| Rootrail state | Local JSON | Player Data; timer field is server-authoritative |

---

## Event Scheduling

Events are scheduled server-side in live-service:
- Event windows stored in Remote Config (`event_start_utc`, `event_end_utc`)
- Client checks Remote Config on session start to determine active events
- Lucky Draw week pool: fetched from Title Data, not embedded in client build
- Player event participation state stored in Player Data per `event_id`

---

## Analytics and Telemetry Needs (MVP → Live)

| Event | When to Track |
|-------|--------------|
| `session_start` | On every login |
| `fixture_crafted` | On each craft |
| `fixture_equipped` | On equip |
| `fixture_cap_increased` | On cap change |
| `rootrail_repair_started` | On timer start |
| `rootrail_repair_claimed` | On collect |
| `hat_unlocked` | On hat unlock |
| `warden_cleared` | On Warden win (with warden_id) |
| `forgotten_manual_discovered` | On codex entry added |
| `duty_completed` | On Duty completion |
| `burrowfolk_output_collected` | On collection |
| `day_n_retention` | Day 1, 3, 7, 14, 30 |
| `iap_initiated` / `iap_fulfilled` | On purchase flow |

---

## Migration Risks

| Risk | Mitigation |
|------|-----------|
| Local save → cloud save data loss | Export local save as migration payload on first cloud session |
| Schema version mismatch | All player state docs carry `schema_version`; migration scripts on server |
| PlayFab pricing increase | Abstract economy layer so migration to option C/E is feasible without client rewrite |
| Content key renames between builds | Content ID stability rule — never rename published IDs |
| Timer drift between client and server | Always use server UTC timestamps; display countdown client-side only |

---

## MVP Backend Acceptance Checklist

For prototype (Option A):

- [ ] All game state persists across app close/reopen via local save
- [ ] Rootrail timer persists across app close (stores `timer_end_local` timestamp)
- [ ] Daily Duty reset uses device UTC clock only (acceptable for prototype)
- [ ] No premium currency (Glowcap) in prototype build; disable or hardcode to 0
- [ ] No IAP in prototype build
- [ ] Save file can be inspected for debugging (plaintext JSON preferred for prototype)
- [ ] Save file includes `schema_version: 1` for future migration

For closed beta (Option B/C):

- [ ] Rootrail timer is server-issued `timer_end_utc`
- [ ] Glowcap grants are server-authoritative
- [ ] Cloud save syncs on session end + session start
- [ ] IAP receipt validation integrated before any real-money build
- [ ] Remote Config active for event scheduling and economy balance values
