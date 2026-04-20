# Gnome Survivor — Project Roadmap v0.1.0

**Document version:** 0.1.1  
**Last updated:** 2026-04-20  
**Status:** DRAFT  

---

## Roadmap Overview

```
PHASE 1          PHASE 2A         PHASE 2B          PHASE 3           PHASE 4         PHASE 5
Conceptual       Production        Prototype         First             Content         Live-Service
Foundation       Foundation        Readiness         Playable          Production      Prep
                                                                                        
17 docs          6 docs            8 docs            6 sprints         Ongoing         Pre-launch
COMPLETE ✅       COMPLETE ✅       COMPLETE ✅       ~6-10 weeks       Parallel        ~4 weeks
                  ~2-3 weeks        ~3-4 weeks                          w/ Phase 3
```

---

## Phase 1 — Conceptual Foundation ✅ COMPLETE

**Goal:** Establish canonical naming, world, factions, systems, economy philosophy, and content architecture.

**Delivered:** 17 documents covering glossary, world bible, strains, cliques, deepening arc, duties, encounters, wanderers, burrow posts, events, economy templates, economy model, system interactions, content scaling, seasonal framework, and season briefs.

**Phase version:** v1.0.0 (locked)

---

## Phase 2A — Production Foundation ✅ COMPLETE

**Goal:** Convert design stack into build-enabling specifications. Answer all questions a developer would ask before writing code.

**Duration:** 2-3 weeks  
**Owner:** Systems Designer  

### Milestones

| ID | Milestone | Deliverable | Target | Status |
|----|-----------|------------|--------|--------|
| 2A.1 | Economy math finalized | economy_model_v1_sheet.csv | Week 1 | ✅ |
| 2A.2 | Game object model defined | data_schema_v1.md | Week 1-2 | ✅ |
| 2A.3 | Backend stack selected | backend_architecture_options.md | Week 2 | ✅ |
| 2A.4 | MVP scope locked | core_loop_mvp_spec.md | Week 2 | ✅ |
| 2A.5 | Onboarding flow mapped | unlock_flow_and_ui_map.md | Week 2-3 | ✅ |
| 2A.6 | Content templates ready | content_table_templates.md | Week 3 | ✅ |

### Exit Criteria
- A developer can read the MVP spec and start building without design ambiguity
- Economy spreadsheet exists with real numbers, not just bands
- Backend is selected with migration risks documented
- Content designers can add entries using templates without inventing format

---

## Phase 2B — Prototype Readiness ✅ COMPLETE

**Goal:** Produce all content and specs needed for the first playable build.

**Duration:** 3-4 weeks  
**Owner:** Game Designer + Content Designer  

### Milestones

| ID | Milestone | Deliverable | Target | Status |
|----|-----------|------------|--------|--------|
| 2B.1 | Loamwake content complete | loamwake_mvp_content_sheet.md | Week 1 | ✅ |
| 2B.2 | First Confidant chain written | first_confidant_chain.md | Week 1 | ✅ |
| 2B.3 | Wanderer pool populated | first_wanderer_pool.md (6 Wanderers) | Week 1-2 | ✅ |
| 2B.4 | First event cycle specified | lucky_draw_week_mvp.md | Week 2 | ✅ |
| 2B.5 | Tutorial scripted | tutorial_and_onboarding_flow.md | Week 2-3 | ✅ |
| 2B.6 | Save system specified | save_state_and_profile_flow.md | Week 3 | ✅ |
| 2B.7 | IAP catalog drafted | iap_catalog_v1.md | Week 3-4 | ✅ |
| 2B.8 | Implementation plan | implementation_planning_pack.md | Week 4 | ✅ |

### Exit Criteria
- All content for Loamwake exists in spreadsheet-ready format
- Tutorial can be implemented without guessing
- IAP items and prices are specified
- Save/load/sync approach is documented

---

## Phase 3 — First Playable (Prototype)

**Goal:** Build the smallest possible version of the game that proves the core fantasy is fun.

**Duration:** 6-10 weeks (6 sprints)  
**Owner:** Unity Developer + UI Artist  

### Sprint Plan

| Sprint | Duration | Focus | Proof Question |
|--------|----------|-------|---------------|
| S1 | 1-2 weeks | Burrow + Gather loop + idle output | Does the home base feel alive? |
| S2 | 1-2 weeks | Strata Gate + Loamwake + Auto-Clash | Does exploration feel rewarding? |
| S3 | 1-2 weeks | Fixtures + Treasures + upgrade loops | Do power spikes feel good? |
| S4 | 1 week | Posts + Duties + first Confidant | Does the world feel socially alive? |
| S5 | 1-2 weeks | Lucky Draw Week (1 cycle) | Does the event create excitement? |
| S6 | 1 week | Clique stub + Crack stub + polish | Does the full loop hold for 7 days? |

### Exit Criteria
- Core loop is playable for 7 days without major gaps
- At least 3 testers confirm "I want to keep playing"
- Economy feels roughly right (too generous beats too stingy at this stage)
- Event cycle creates measurable excitement spike

---

## Phase 4 — Content Production

**Goal:** Fill out all 5 Strata, all Confidants, full Wanderer/Runaway/Wild Ones pool, and Burrow Post library.

**Duration:** Ongoing, parallel with Phase 3 late sprints  
**Owner:** Content Designer / Writer  

### Content Targets

| Content Type | Launch Target | Phase 4 Goal |
|-------------|---------------|-------------|
| Strata fully playable | 5 | 5 |
| Confidants | 5 (1 per Stratum) | 10-15 |
| Burrowfolk classes | 3 (Hands, Scouts, Bruisers) | 5-7 |
| Wanderers | 18-30 | 30+ |
| Runaways | 10-18 | 18+ |
| Wild Ones | 8-15 | 15+ |
| Burrow Post templates | 40-70 | 70+ |
| Wardens | 5 (1 per Stratum) | 5 + sub-bosses |
| Buried Clue chains | 5 | 5 |
| Treasures | 20-30 | 40+ |
| Fixture items | 30-50 | 60+ |

---

## Phase 5 — Live-Service Prep

**Goal:** Prepare infrastructure for post-launch operation.

**Duration:** ~4 weeks before soft launch  
**Owner:** Tech Lead + Game Designer  

### Milestones

| ID | Milestone | Purpose |
|----|-----------|---------|
| 5.1 | Remote config for events | Schedule events without client update |
| 5.2 | Analytics + telemetry | Track retention, funnels, economy health |
| 5.3 | Season 1 content pack ready | First post-launch season (The First Stirring) |
| 5.4 | Balance monitoring dashboard | Currency inflation/deflation alerts |
| 5.5 | Community management tools | Clique moderation, report system |
| 5.6 | Soft launch plan | Target regions, KPI targets, go/no-go criteria |

---

## Long-Term Roadmap

### Year 1 Post-Launch

| Quarter | Focus |
|---------|-------|
| Q1 | Season 1 (First Stirring) + Season 2 (Record Bends) + stability |
| Q2 | Season 3 (Fen of Echoes) + Season 4 (Brass in Motion) + Great Dispute stubs |
| Q3 | Season 5 (Court of Splendor) + Memory Shift introduction |
| Q4 | Season 6 (The Recall War) + Year 2 planning |

### Year 2

| Focus | Systems |
|-------|---------|
| Reinterpretation seasons | Older Strata gain new truth states |
| Contradiction seasons | Strain vs Strain narrative pressure |
| Excavation seasons | The Crack + machine-era content |
| Rootrail seasons | Long-project aspirational escalation |
| Memory Shift seasons | Rift content becomes central |

---

## Version History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 0.1.1 | 2026-04-20 | PM (Claude) | Phase 2B.8 marked complete; all Phase 2A/2B milestones marked ✅; status headers updated; stale Clothes→Fixtures and Ascender→Rootrail terminology corrected |
| 0.1.0 | 2026-04-19 | PM (Claude) | Initial roadmap created from Phase 1 docs |

---

*This roadmap is a living document. Update after every phase transition.*
