# Gnome Game — Master Index

## Purpose
This file is the top-level index for the project documentation set.

It does two jobs:
1. shows the **recommended folder/file structure** for the design project
2. acts as a quick map for what each file is for

This is a design-oriented structure, not a final engine/repo structure.

---

## Recommended project folder structure

```text
gnome-game/
├── 00_index/
│   ├── master_index.md
│   ├── roadmap.md
│   └── changelog.md
│
├── 01_core/
│   ├── master_glossary.md
│   ├── design_principles.md
│   ├── core_story_spine.md
│   └── pillars_and_tone.md
│
├── 02_world/
│   ├── phase1_content_bible.md
│   ├── strata_bible.md
│   ├── deepening_arc.md
│   ├── memory_shift.md
│   └── world_history.md
│
├── 03_factions/
│   ├── strains_bible.md
│   ├── clique_confidants_bible.md
│   ├── wanderers_runaways_wild_ones.md
│   └── burrowfolk_classes.md
│
├── 04_systems/
│   ├── progression_spine.md
│   ├── combat_and_clash.md
│   ├── treasures_and_pathwheel.md
│   ├── clothes_and_cloth_assembly.md
│   ├── strain_hall_and_loom.md
│   ├── clique_systems.md
│   ├── burrow_modules.md
│   ├── the_crack.md
│   └── the_ascender.md
│
├── 05_liveops/
│   ├── event_cycle.md
│   ├── lucky_draw_week.md
│   ├── treasure_week.md
│   ├── echo_week.md
│   ├── festival_ledger.md
│   └── monetization_notes.md
│
├── 06_content/
│   ├── duties_and_encounters.md
│   ├── buried_clues.md
│   ├── confidant_posts.md
│   ├── burrow_posts.md
│   ├── wardens_and_bosses.md
│   ├── loot_tables.md
│   └── region_rewards.md
│
├── 07_ui/
│   ├── ui_surface_map.md
│   ├── menu_tree.md
│   ├── feature_unlock_order.md
│   ├── naming_usage_guide.md
│   └── red_dot_and_notifications.md
│
├── 08_production/
│   ├── implementation_phases.md
│   ├── content_pipeline.md
│   ├── balancing_questions.md
│   ├── unresolved_items.md
│   └── specialist_prompts.md
│
└── 99_archive/
    ├── gnome_game_planning_pack.md
    └── old_notes/
```

---

## Files already created

### Active canonical docs
- `master_glossary.md`  
  Canonical naming sheet for systems, currencies, events, regions, strains, social roles, and story-facing terms.

- `phase1_content_bible.md`  
  Initial world/content guide focused on the 5 launch Strata, their tone, public face, hidden truth, mechanical role, and production anchors.

- `strains_bible.md`  
  Core faction document for the 5 Strains, including public myth, contradiction, visual direction, survival philosophy, Confidant patterns, and Deepening truths.

- `clique_confidants_bible.md`  
  Social/faction guide for Cliques, Confidants, Burrowfolk ties, Favor Marks, and social-political tone.

### Source / upstream planning doc
- `gnome_game_planning_pack.md`  
  Original planning pack that established the system architecture, unresolved items, launch scope, event cycle, and build order.

---

## Recommended reading order

### Fast onboarding order
1. `master_glossary.md`
2. `phase1_content_bible.md`
3. `strains_bible.md`
4. `clique_confidants_bible.md`

### Full design order
1. `master_glossary.md`
2. `phase1_content_bible.md`
3. `strains_bible.md`
4. `clique_confidants_bible.md`
5. `gnome_game_planning_pack.md`

---

## What each top-level folder is for

### `00_index/`
Navigation and project-control docs.
Use this area for indexes, roadmap files, and change tracking.

### `01_core/`
The project’s immutable backbone.
Anything that affects naming, tone, fantasy, or central design identity belongs here.

### `02_world/`
Worldbuilding, Strata, history, Deepening, Memory Shift, and setting logic.

### `03_factions/`
Strains, Cliques, Confidants, Wanderers, Runaways, Wild Ones, and Burrowfolk.

### `04_systems/`
Mechanical systems and gameplay loops.
This is where implementation-facing docs should live.

### `05_liveops/`
Event design, reward cadence, Festival Ledger structure, monetization-adjacent notes, and scheduling logic.

### `06_content/`
Quest content, encounter packets, Buried Clues, posts, bosses, and authored scenario bricks.

### `07_ui/`
UI naming, menu structure, unlock flow, red-dot logic, and surface maps.

### `08_production/`
Execution docs, specialist prompts, content pipeline notes, and unresolved questions.

### `99_archive/`
Legacy inputs and older drafts kept for reference, not active design truth.

---

## Recommended next files to create

### High priority
- `deepening_arc.md`
- `clique_systems.md`
- `event_cycle.md`
- `duties_and_encounters.md`
- `burrow_modules.md`

### Medium priority
- `wanderers_runaways_wild_ones.md`
- `wardens_and_bosses.md`
- `memory_shift.md`
- `feature_unlock_order.md`
- `unresolved_items.md`

### Nice-to-have after that
- `confidant_posts.md`
- `region_rewards.md`
- `naming_usage_guide.md`
- `specialist_prompts.md`
- `content_pipeline.md`

---

## Naming convention recommendation
Use lowercase snake-case style for file names:
- good: `clique_confidants_bible.md`
- good: `phase1_content_bible.md`
- avoid: `Clique Confidants Bible Final V2.md`

Keep one canonical file per subject whenever possible.
If a subject grows too large, split by system or region rather than making near-duplicates.

---

## Canon rule
When two files seem to conflict:
1. `master_glossary.md` wins on naming
2. the most focused current bible wins on content detail
3. old planning docs are references, not final truth

---

## Immediate next-step suggestion
The next strongest files to build are:
1. `deepening_arc.md`
2. `clique_systems.md`
3. `duties_and_encounters.md`

Those three would connect story progression, social mechanics, and authored content structure into one usable production bundle.
