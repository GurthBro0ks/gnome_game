# Gnome Game

**Status:** Pre-Production — Design Documentation Phase

A post-fallout gnome world idle/live-service game inspired by Super Snail's structure, rebuilt with original lore, naming, and game identity.

## Project Structure

```
gnome_game/
├── docs/           ← Design documentation (canonical source of truth)
│   ├── 00_index/   ← Navigation, roadmap, changelog
│   ├── 01_core/    ← Glossary, design principles, tone
│   ├── 02_world/   ← Strata, Deepening, Memory Shift, worldbuilding
│   ├── 03_factions/ ← Strains, Cliques, Confidants, roaming cast
│   ├── 04_systems/ ← Gameplay mechanics and loops
│   ├── 05_liveops/ ← Events, seasons, monetization
│   ├── 06_content/ ← Duties, encounters, posts, bosses
│   ├── 07_ui/      ← UI map, unlock flow, menus
│   ├── 08_production/ ← Build order, risks, specialist prompts
│   └── 99_archive/ ← Legacy planning docs, APK analysis
├── data/           ← CSV/JSON content tables and balance sheets
├── art/            ← Concept art, UI mockups, sprites
└── src/            ← Prototype code (Phase 3+)
```

## Quick Start — Reading Order

1. `docs/01_core/master_glossary.md`
2. `docs/02_world/phase1_content_bible.md`
3. `docs/03_factions/strains_bible.md`
4. `docs/03_factions/clique_confidants_bible.md`
5. `docs/02_world/deepening_arc.md`

## Agent Harness

This project uses SlimyAI Harness v3 for autonomous agent sessions.
See `AGENTS.md` for the operating manual.

## Canon Rule

When docs conflict:
1. `master_glossary.md` wins on naming
2. The most focused current bible wins on content detail
3. Archive docs are references, not final truth
