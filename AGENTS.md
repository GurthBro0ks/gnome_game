# Gnome Game — Agent Operating Manual

You are an autonomous agent working on the Gnome Game design and prototype project.
This is a game design repo, not a live service (yet).

## Startup Sequence (EVERY session)

1. `pwd` — confirm you're in the repo root (`/home/mint/projects/gnome_game`)
2. `cat claude-progress.md` — what happened last session
3. `cat feature_list.json | head -200` — see current feature status
4. `git log --oneline -10` — see recent commits
5. `source init.sh` — validate the environment
6. Pick the highest-priority incomplete feature from feature_list.json
7. Only THEN begin work

## Repo Structure

- `docs/00_index/` — Navigation, roadmap, changelog
- `docs/01_core/` — Glossary, design principles, tone (CANON SOURCE for naming)
- `docs/02_world/` — Strata, Deepening, Memory Shift, worldbuilding
- `docs/03_factions/` — Strains, Cliques, Confidants, roaming cast
- `docs/04_systems/` — Economy, progression, gameplay mechanics
- `docs/05_liveops/` — Events, seasons, monetization, scaling rules
- `docs/06_content/` — Duties, encounters, posts, bosses
- `docs/07_ui/` — UI map, unlock flow, menus
- `docs/08_production/` — Build order, risks, specialist prompts
- `docs/99_archive/` — Legacy docs, Super Snail APK analysis
- `data/` — CSV/JSON content tables and balance sheets
- `art/` — Concept art, UI, sprites
- `src/` — Prototype code (when ready)

## Project Phase

**Current Phase: Pre-Production (0.x)**
- Design documentation is Phase 1 complete
- Production-enabling docs (schema, MVP spec, backend) are in progress
- No prototype code exists yet

## Canon Rules

1. `docs/01_core/master_glossary.md` wins on ALL naming conflicts
2. The most focused current bible wins on content detail
3. Archive docs (`docs/99_archive/`) are references, not final truth
4. When in doubt, check the glossary before inventing terms

## Key Game Terms (quick reference)

| Old Term | Canonical Term |
|----------|---------------|
| Snail | Gnome Survivor |
| Home Base | The Burrow |
| Realms | Strata |
| Relics | Treasures |
| Partners | Confidants |
| Minions | Burrowfolk |
| Club | Clique |
| Prestige | Deepening |
| Fissure | The Crack |
| DNA Forms | Strains |

## Truth Gate

A feature/doc is only "done" when:
1. It follows the naming conventions in `master_glossary.md`
2. It does not contradict existing canon docs
3. If it adds new systems/currencies, they are registered in the glossary
4. The markdown renders cleanly

## Forbidden Zones

- `.env*` files (never read/write secrets)
- Do NOT rename the game (working title only)
- Do NOT overcomplicate v1 — simplest launch-safe version first
- Do NOT create premium-only power items at launch
- Do NOT collapse Confidants and Burrowfolk into one system

## Work Rules

- ONE feature per session. Complete it or document where you stopped.
- Small, surgical commits with descriptive messages
- Follow the document versioning protocol: MAJOR.MINOR.PATCH
- If creating new content, use existing templates from `docs/05_liveops/` or `docs/06_content/`
- Mark anything unresolved clearly with `> UNRESOLVED:` callout blocks

## End-of-Session Checklist

1. All new docs follow naming conventions
2. `feature_list.json` updated (passes: true for completed features)
3. `claude-progress.md` updated with what you did, what's next
4. `git add -A && git commit -m "<type>: <description>"`
5. `git push origin main`

## Agreed Design Decisions (DO NOT CHANGE without discussion)

- funny post-fallout gnome world
- 5 launch regions (Strata)
- 2-week beginner event + 2-week hoard event
- 3 rotating weekly events (Lucky Draw / Treasure / Echo)
- clubs (Cliques) in v1, species-war (Great Dispute) stubs only
- hats as major equipment category
- separate Confidants and Burrowfolk (partners ≠ minions)
- multiple placement boards
- one active Strata Trait at a time
- Deepening into Memory Shift structure (prestige → rift)
- The Crack as never-ending progression (fissure)
- cosmetics / light monetization, no premium-only power Treasures at launch
- hidden content and Easter eggs as expandable layers (not v1 core)
