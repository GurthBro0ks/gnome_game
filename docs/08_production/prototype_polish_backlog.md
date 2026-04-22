# Prototype Polish Backlog

Date: 2026-04-22
Source: Manual verification pass on build 8091b49 + 7795237

---

## Blocking Now

None.

---

## Non-Blocking UI/UX Issues

These were observed during manual verification and do not block the core loop or save/restore:

1. **Burrow/main-screen text clipping** — Some text is clipped or hidden lower on the page at default Game view. Players may miss information that falls below the visible area.

2. **Vertically cramped layout** — Several pages remain text-heavy and vertically cramped. Reducing information density or improving spacing would improve readability.

---

## Future Visual/UI Pass Items

These are not bugs but would improve the prototype experience before external playtesting:

1. **Page layout normalization** — Standardize spacing, padding, and font sizing across all debug UI pages for a consistent feel.
2. **Scroll area refinement** — Ensure all scrollable bodies have clear visual boundaries and that the fixed header/footer pattern is consistent.
3. **Button and card styling** — Improve affordance and touch-target sizing for interactive elements.
4. **Shell-page presentation** — Make stub-only pages (Rootrail, Vault, Crack, Clique) visually distinct from fully-implemented pages.
5. **Tutorial guide styling** — Differentiate guide/next-step text from regular UI labels.

---

## Future Systems Expansion Items

These are design-side items documented in existing canon but not yet in the prototype. Listed in approximate dependency order:

1. Strata 2 through 5 content and zone progression
2. Rootrail repair chain (steps 2+ with timers and Forgotten Manuals)
3. Full Confidant depth (trust levels, Confidence Trails, Callings)
4. Burrowfolk deployment and Burrow work queue
5. Vault of Treasures content and treasure progression
6. War Armory and War Fixtures
7. Deepening / Memory Shift
8. The Crack ladder and sector progression
9. Clique backend, social features, and Great Dispute gameplay
10. Liveops event scheduling (Treasure Week, Echo Week, full event rotation)
11. IAP integration, store SDKs, receipt validation
12. Cloud save and merge
13. Cosmetics and visual customization

---

## Document Version

v1.0.0 — 2026-04-22
