#!/usr/bin/env bash
# ============================================================
# Gnome Game — Full Project Scaffold + Harness v3 Setup
# 
# Creates the canonical repo structure, places all existing
# design docs, sets up slimy-harness v3 tracking files,
# and pushes to GitHub.
#
# Usage: bash setup_gnome_game.sh
# ============================================================
set -euo pipefail

PROJECT_ROOT="/home/mint/projects/gnome_game"
REMOTE="https://github.com/GurthBro0ks/gnome_game.git"

GREEN='\033[0;32m'; YELLOW='\033[1;33m'; RED='\033[0;31m'; NC='\033[0m'
log()      { echo -e "$1"; }
log_done() { log "  ${GREEN}✓${NC}  $1"; }
log_info() { log "  ${YELLOW}~${NC}  $1"; }

# ============================================================
# PHASE 0: Safety checks
# ============================================================
log "${GREEN}=== Gnome Game — Project Scaffold ===${NC}"
log ""

if [ -d "$PROJECT_ROOT/.git" ]; then
  log_info "Git repo already exists at $PROJECT_ROOT — skipping init"
  cd "$PROJECT_ROOT"
else
  mkdir -p "$PROJECT_ROOT"
  cd "$PROJECT_ROOT"
  git init
  log_done "Git initialized at $PROJECT_ROOT"
fi

# ============================================================
# PHASE 1: Create folder structure
# ============================================================
log ""
log "${GREEN}[Phase 1] Creating folder structure${NC}"

DIRS=(
  "docs/00_index"
  "docs/01_core"
  "docs/02_world"
  "docs/03_factions"
  "docs/04_systems"
  "docs/05_liveops"
  "docs/06_content"
  "docs/07_ui"
  "docs/08_production"
  "docs/08_production/specialist_prompts"
  "docs/99_archive/super_snail_reference"
  "data/economy"
  "data/content"
  "data/balance"
  "art/concepts"
  "art/ui"
  "art/sprites"
  ".github/ISSUE_TEMPLATE"
)

for d in "${DIRS[@]}"; do
  mkdir -p "$d"
  log_done "$d"
done

# ============================================================
# PHASE 2: Create .gitignore
# ============================================================
log ""
log "${GREEN}[Phase 2] Creating .gitignore${NC}"

cat > .gitignore << 'GITIGNORE'
# Environment and secrets
.env
.env.*
.env.local
.secrets/
*.pem
*.key

# Logs
*.log
harness-logs/

# Cache and build
node_modules/
__pycache__/
.pytest_cache/
dist/
build/
.next/
coverage/
.turbo/

# OS
.DS_Store
Thumbs.db

# Editor
.vscode/
.idea/
*.swp
*.swo

# Temporary files
tmp/
temp/
*.tmp

# Docker
docker-volumes/

# Test results
test-results/
playwright-report/

# Unity (future)
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
[Uu]ser[Ss]ettings/
*.csproj
*.unityproj
*.sln
*.suo
*.userprefs
GITIGNORE
log_done ".gitignore"

# ============================================================
# PHASE 3: Create README.md
# ============================================================
log ""
log "${GREEN}[Phase 3] Creating README.md${NC}"

cat > README.md << 'README'
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
README
log_done "README.md"

# ============================================================
# PHASE 4: Create CHANGELOG.md
# ============================================================
log ""
log "${GREEN}[Phase 4] Creating CHANGELOG.md${NC}"

TODAYS_DATE=$(date +%Y-%m-%d)

cat > CHANGELOG.md << CHANGELOG
# Changelog — Gnome Game

## [$TODAYS_DATE] — Initial scaffold
### Added
- Full project folder structure
- All existing design documentation placed in canonical locations
- Harness v3 files (AGENTS.md, feature_list.json, claude-progress.md, init.sh)
- README, CHANGELOG, .gitignore
- GitHub issue templates

### Notes
- Project bootstrapped from Claude PM chat design sessions
- Design docs are Phase 1 complete; production docs (schema, MVP spec) still needed
CHANGELOG
log_done "CHANGELOG.md"

# ============================================================
# PHASE 5: Place design docs in canonical locations
# ============================================================
log ""
log "${GREEN}[Phase 5] Placing design docs${NC}"

# --- 00_index ---
# master_index.md → docs/00_index/
if [ -f "master_index.md" ]; then
  cp master_index.md docs/00_index/master_index.md
  log_done "docs/00_index/master_index.md (copied from root)"
fi

# --- 01_core ---
declare -A CORE_DOCS=(
  ["master_glossary.md"]="docs/01_core/master_glossary.md"
)
for src in "${!CORE_DOCS[@]}"; do
  dst="${CORE_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "MISSING source: $src — create placeholder"
    echo "# $(basename "$src" .md | tr '_' ' ' | sed 's/\b\(.\)/\u\1/g')" > "$dst"
    echo "" >> "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 02_world ---
declare -A WORLD_DOCS=(
  ["phase1_content_bible.md"]="docs/02_world/phase1_content_bible.md"
  ["deepening_arc.md"]="docs/02_world/deepening_arc.md"
)
for src in "${!WORLD_DOCS[@]}"; do
  dst="${WORLD_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 03_factions ---
declare -A FACTION_DOCS=(
  ["strains_bible.md"]="docs/03_factions/strains_bible.md"
  ["clique_confidants_bible.md"]="docs/03_factions/clique_confidants_bible.md"
  ["wanderers_runaways_wild_ones.md"]="docs/03_factions/wanderers_runaways_wild_ones.md"
)
for src in "${!FACTION_DOCS[@]}"; do
  dst="${FACTION_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 04_systems ---
declare -A SYSTEM_DOCS=(
  ["economy_model_v1.md"]="docs/04_systems/economy_model_v1.md"
  ["economy_and_progression_templates.md"]="docs/04_systems/economy_and_progression_templates.md"
  ["system_interactions_matrix.md"]="docs/04_systems/system_interactions_matrix.md"
)
for src in "${!SYSTEM_DOCS[@]}"; do
  dst="${SYSTEM_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 05_liveops ---
declare -A LIVEOPS_DOCS=(
  ["event_content_templates.md"]="docs/05_liveops/event_content_templates.md"
  ["event_examples.md"]="docs/05_liveops/event_examples.md"
  ["seasonal_expansion_framework.md"]="docs/05_liveops/seasonal_expansion_framework.md"
  ["season_briefs_pack.md"]="docs/05_liveops/season_briefs_pack.md"
  ["content_scaling_rules.md"]="docs/05_liveops/content_scaling_rules.md"
)
for src in "${!LIVEOPS_DOCS[@]}"; do
  dst="${LIVEOPS_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 06_content ---
declare -A CONTENT_DOCS=(
  ["duties_and_encounters.md"]="docs/06_content/duties_and_encounters.md"
  ["burrow_posts.md"]="docs/06_content/burrow_posts.md"
  ["burrow_posts_examples.md"]="docs/06_content/burrow_posts_examples.md"
)
for src in "${!CONTENT_DOCS[@]}"; do
  dst="${CONTENT_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 08_production ---
declare -A PROD_DOCS=(
  ["next_steps_report.md"]="docs/08_production/next_steps_report.md"
  ["chat_coverage_report.md"]="docs/08_production/chat_coverage_report.md"
)
for src in "${!PROD_DOCS[@]}"; do
  dst="${PROD_DOCS[$src]}"
  if [ -f "$src" ]; then
    cp "$src" "$dst"
    log_done "$dst"
  else
    log_info "Placeholder: $dst"
    echo "# $(basename "$dst" .md | tr '_' ' ')" > "$dst"
    echo "> TODO: Copy from project knowledge" >> "$dst"
  fi
done

# --- 99_archive/super_snail_reference ---
ARCHIVE_FILES=(
  "file_info.txt"
  "aapt_badging.txt"
  "aapt_permissions.txt"
  "unity_libs.txt"
  "il2cpp_libs.txt"
  "analysis_log.txt"
  "sdk_indicators.txt"
  "network_endpoints.txt"
  "key_strings.txt"
  "launcher_activities.txt"
  "ui_classes.txt"
  "file_inventory.txt"
  "sha256.txt"
  "res_structure.txt"
  "unity_activities.txt"
)
for f in "${ARCHIVE_FILES[@]}"; do
  if [ -f "$f" ]; then
    cp "$f" "docs/99_archive/super_snail_reference/$f"
    log_done "docs/99_archive/super_snail_reference/$f"
  fi
done

# ============================================================
# PHASE 6: GitHub issue templates
# ============================================================
log ""
log "${GREEN}[Phase 6] Creating GitHub issue templates${NC}"

cat > .github/ISSUE_TEMPLATE/design_decision.md << 'TEMPLATE'
---
name: Design Decision
about: Propose or record a game design decision
title: "[DESIGN] "
labels: design
---

## Decision
<!-- What was decided? -->

## Context
<!-- Why was this decision made? What alternatives were considered? -->

## Systems Affected
<!-- Which game systems does this impact? -->

## Status
- [ ] Proposed
- [ ] Approved
- [ ] Implemented
TEMPLATE
log_done ".github/ISSUE_TEMPLATE/design_decision.md"

cat > .github/ISSUE_TEMPLATE/content_request.md << 'TEMPLATE'
---
name: Content Request
about: Request new game content (Strata, Wanderers, events, etc.)
title: "[CONTENT] "
labels: content
---

## Content Type
<!-- Wanderer / Runaway / Wild One / Burrow Post / Duty / Event / etc. -->

## Description
<!-- What should this content do/say/feel like? -->

## Related Systems
<!-- Which Stratum? Which Strain? Which event cycle? -->

## Priority
- [ ] Launch required
- [ ] Post-launch
- [ ] Nice-to-have
TEMPLATE
log_done ".github/ISSUE_TEMPLATE/content_request.md"

# ============================================================
# PHASE 7: Harness v3 files (SEPARATE from other projects)
# ============================================================
log ""
log "${GREEN}[Phase 7] Creating Harness v3 files${NC}"

# --- AGENTS.md ---
cat > AGENTS.md << 'AGENTS'
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
AGENTS

log_done "AGENTS.md"

# --- init.sh ---
cat > init.sh << 'INITSH'
#!/usr/bin/env bash
# Gnome Game — Agent Environment Init
# Run at the start of every agent session: source init.sh
set -euo pipefail

echo "=== Gnome Game — Project Init ==="

# 1. Confirm correct directory
if [ ! -f "AGENTS.md" ] || ! grep -q "Gnome Game" AGENTS.md 2>/dev/null; then
  echo "ERROR: Not in gnome_game root. cd to /home/mint/projects/gnome_game first."
  exit 1
fi

# 2. Check tools
echo "[1/4] Checking tools..."
for cmd in git python3; do
  command -v $cmd &>/dev/null && echo "  ✓ $cmd" || echo "  ✗ $cmd not found"
done

# 3. Verify doc structure
echo "[2/4] Verifying doc structure..."
EXPECTED_DIRS=("docs/00_index" "docs/01_core" "docs/02_world" "docs/03_factions" "docs/04_systems" "docs/05_liveops" "docs/06_content" "docs/07_ui" "docs/08_production")
MISSING=0
for d in "${EXPECTED_DIRS[@]}"; do
  if [ ! -d "$d" ]; then
    echo "  ✗ MISSING: $d"
    MISSING=$((MISSING + 1))
  fi
done
if [ "$MISSING" -eq 0 ]; then
  echo "  ✓ All doc directories present"
fi

# 4. Count docs
echo "[3/4] Doc inventory..."
DOC_COUNT=$(find docs/ -name "*.md" -type f 2>/dev/null | wc -l)
DATA_COUNT=$(find data/ -name "*.csv" -o -name "*.json" -type f 2>/dev/null | wc -l)
echo "  Design docs: $DOC_COUNT"
echo "  Data files: $DATA_COUNT"

# 5. Git status
echo "[4/4] Git status..."
BRANCH=$(git branch --show-current 2>/dev/null || echo "?")
DIRTY=$(git status --porcelain 2>/dev/null | wc -l)
echo "  Branch: $BRANCH"
echo "  Uncommitted files: $DIRTY"

echo ""
echo "=== Init complete. Read claude-progress.md and feature_list.json next. ==="
INITSH
chmod +x init.sh
log_done "init.sh"

# --- feature_list.json ---
cat > feature_list.json << 'FEATURES'
{
  "_meta": {
    "project": "gnome_game",
    "scope": "game-design-repo",
    "last_updated": "2026-04-19",
    "rules": "NEVER remove or edit existing features. Only update 'passes' after verification. Add new features at the end. The 'risk' field guides Prompt P planning depth."
  },
  "features": [
    {
      "id": "scaffold-001",
      "project": "gnome_game",
      "description": "Repo scaffold complete: folder structure, all existing design docs placed, harness files created, pushed to GitHub",
      "priority": "critical",
      "passes": false,
      "risk": "low",
      "plan": [
        "Run setup_gnome_game.sh",
        "Verify all docs placed in correct folders",
        "Verify git remote set to GurthBro0ks/gnome_game",
        "Verify push succeeds"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "doc-001",
      "project": "gnome_game",
      "description": "All 17 existing design docs verified present and readable in canonical locations",
      "priority": "critical",
      "passes": false,
      "risk": "low",
      "plan": [
        "Check docs/01_core/master_glossary.md exists and has content",
        "Check docs/02_world/phase1_content_bible.md exists",
        "Check docs/02_world/deepening_arc.md exists",
        "Check docs/03_factions/ has all 3 faction docs",
        "Check docs/04_systems/ has economy docs",
        "Check docs/05_liveops/ has 5 liveops docs",
        "Check docs/06_content/ has 3 content docs",
        "Verify no empty placeholder files remain for existing docs"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-001",
      "project": "gnome_game",
      "description": "Implementation planning pack created: system architecture map, launch scope, currency map, home-screen module map, region plan, event math, risk list, build order",
      "priority": "critical",
      "passes": false,
      "risk": "medium",
      "plan": [
        "Create docs/08_production/implementation_planning_pack.md",
        "Include system architecture map",
        "Include launch scope vs later scope table",
        "Include currency/sink/faucet map",
        "Include home-screen module map",
        "Include region progression plan",
        "Include event math targets",
        "Include risk list",
        "Include recommended build order",
        "Mark all unresolved items clearly"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-002",
      "project": "gnome_game",
      "description": "Data schema v1 created: all major game entities, relationships, persistent player state",
      "priority": "high",
      "passes": false,
      "risk": "medium",
      "plan": [
        "Create docs/04_systems/data_schema_v1.md",
        "Define account, burrow_state, strata_progress, strain_progress entities",
        "Define confidant, burrowfolk, treasure, clothes entities",
        "Define event_state, deepening_state, crack_state",
        "Define post_state, encounter_state",
        "Document owned vs equipped vs unlocked distinctions",
        "Document scaling approach for live-service additions"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-003",
      "project": "gnome_game",
      "description": "Core loop MVP spec created: first 10 minutes, first 60 minutes, first daily return, proof of fun criteria",
      "priority": "high",
      "passes": false,
      "risk": "medium",
      "plan": [
        "Create docs/04_systems/core_loop_mvp_spec.md",
        "Define MVP slice: Burrow + Loamwake only",
        "Define first-session flow",
        "Define first-day flow",
        "Define first meaningful decision",
        "Define proof of fun criteria",
        "List exact features in scope vs deliberately out of scope"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-004",
      "project": "gnome_game",
      "description": "Unlock flow and UI map created: minute-1 visible modules, day-1/2/3 unlock order, red-dot logic",
      "priority": "high",
      "passes": false,
      "risk": "low",
      "plan": [
        "Create docs/07_ui/unlock_flow_and_ui_map.md",
        "Define minute-1 visible modules",
        "Define day-1 unlock order",
        "Define Loamwake-clear unlocks",
        "Define when Clique, Crack, Vault, Ascender, Strain Hall appear",
        "Establish red-dot / notification rules"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-005",
      "project": "gnome_game",
      "description": "Content table templates created: CSV column specs for all major content types",
      "priority": "medium",
      "passes": false,
      "risk": "low",
      "plan": [
        "Create docs/06_content/content_table_templates.md",
        "Define CSV columns for Wanderers, Runaways, Wild Ones",
        "Define CSV columns for Confidants, Burrowfolk, Treasures, Clothes",
        "Define CSV columns for Wardens, Duties, Burrow Posts",
        "Define CSV columns for event ladders, Festival Ledger tracks",
        "Create sample data/ CSV files with headers"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    },
    {
      "id": "prod-006",
      "project": "gnome_game",
      "description": "Backend architecture options documented: stack candidates, pros/cons, recommended path",
      "priority": "medium",
      "passes": false,
      "risk": "medium",
      "plan": [
        "Create docs/08_production/backend_architecture.md",
        "Evaluate Firebase, PlayFab, Unity Gaming Services, hybrid",
        "Document anti-cheat considerations",
        "Document content delivery / remote config options",
        "Select preferred stack with reasons",
        "Identify prototype path and migration risks"
      ],
      "qa_verified": false,
      "added": "2026-04-19",
      "completed": null
    }
  ]
}
FEATURES
log_done "feature_list.json"

# --- claude-progress.md ---
cat > claude-progress.md << PROGRESS
# Claude Progress Log — Gnome Game

> This file is append-only. Every agent session adds an entry at the top.
> Never edit or delete old entries.

---

## Session: $TODAYS_DATE (Project Scaffold)

**Agent:** Setup script (human-initiated)
**Duration:** N/A
**Features worked on:** scaffold-001

**What was done:**
- Created full repo folder structure matching canonical design index
- Placed all 17 existing design docs in canonical locations
- Created harness v3 files (AGENTS.md, feature_list.json, claude-progress.md, init.sh)
- Created README, CHANGELOG, .gitignore
- Created GitHub issue templates
- Pushed initial commit to https://github.com/GurthBro0ks/gnome_game

**What needs to happen next:**
- Verify all docs copied correctly (feature doc-001)
- Create implementation planning pack (feature prod-001)
- Create data schema v1 (feature prod-002)
- Create core loop MVP spec (feature prod-003)

**Environment state:** Fresh scaffold, no prototype code yet
**Git state:** main branch, clean after initial push

---
PROGRESS
log_done "claude-progress.md"

# ============================================================
# PHASE 8: Copy docs from root to canonical locations
#           (for docs that were placed at project root by user)
# ============================================================
log ""
log "${GREEN}[Phase 8] Checking for root-level docs to relocate${NC}"

# This handles the case where the user manually drops .md files at root
ROOT_DOCS=(
  "master_glossary.md:docs/01_core/master_glossary.md"
  "master_index.md:docs/00_index/master_index.md"
  "phase1_content_bible.md:docs/02_world/phase1_content_bible.md"
  "deepening_arc.md:docs/02_world/deepening_arc.md"
  "strains_bible.md:docs/03_factions/strains_bible.md"
  "clique_confidants_bible.md:docs/03_factions/clique_confidants_bible.md"
  "wanderers_runaways_wild_ones.md:docs/03_factions/wanderers_runaways_wild_ones.md"
  "economy_model_v1.md:docs/04_systems/economy_model_v1.md"
  "economy_and_progression_templates.md:docs/04_systems/economy_and_progression_templates.md"
  "system_interactions_matrix.md:docs/04_systems/system_interactions_matrix.md"
  "event_content_templates.md:docs/05_liveops/event_content_templates.md"
  "event_examples.md:docs/05_liveops/event_examples.md"
  "seasonal_expansion_framework.md:docs/05_liveops/seasonal_expansion_framework.md"
  "season_briefs_pack.md:docs/05_liveops/season_briefs_pack.md"
  "content_scaling_rules.md:docs/05_liveops/content_scaling_rules.md"
  "duties_and_encounters.md:docs/06_content/duties_and_encounters.md"
  "burrow_posts.md:docs/06_content/burrow_posts.md"
  "burrow_posts_examples.md:docs/06_content/burrow_posts_examples.md"
  "next_steps_report.md:docs/08_production/next_steps_report.md"
  "chat_coverage_report.md:docs/08_production/chat_coverage_report.md"
)

for mapping in "${ROOT_DOCS[@]}"; do
  src="${mapping%%:*}"
  dst="${mapping##*:}"
  if [ -f "$src" ] && [ ! -f "$dst" ]; then
    mv "$src" "$dst"
    log_done "Moved $src → $dst"
  elif [ -f "$src" ] && [ -f "$dst" ]; then
    log_info "$dst already exists, root copy ignored"
  fi
done

# ============================================================
# PHASE 9: Create sample data CSV headers
# ============================================================
log ""
log "${GREEN}[Phase 9] Creating sample data CSV headers${NC}"

cat > data/content/wanderers.csv << 'CSV'
id,name,strain_affinity,home_stratum,rarity,visit_type,gift_currency,gift_amount,lore_tag,season_tag,unlock_condition
CSV
log_done "data/content/wanderers.csv"

cat > data/content/confidants.csv << 'CSV'
id,name,strain,stratum_origin,calling,trust_max,unlock_source,backstory_tag,burrowfolk_class_unlock
CSV
log_done "data/content/confidants.csv"

cat > data/content/treasures.csv << 'CSV'
id,name,rarity,heritage_stat,pathwheel_slot,set_name,source,season_tag
CSV
log_done "data/content/treasures.csv"

cat > data/content/duties.csv << 'CSV'
id,type,title,description,reward_currency,reward_amount,stratum,repeatable,unlock_condition
CSV
log_done "data/content/duties.csv"

cat > data/content/burrow_posts.csv << 'CSV'
id,sender,type,tone,reward_currency,reward_amount,choice_count,escalation_chain,stratum,strain_tag
CSV
log_done "data/content/burrow_posts.csv"

# ============================================================
# PHASE 10: Git add, commit, push
# ============================================================
log ""
log "${GREEN}[Phase 10] Git commit and push${NC}"

# Set remote
if ! git remote get-url origin &>/dev/null; then
  git remote add origin "$REMOTE"
  log_done "Remote added: $REMOTE"
else
  CURRENT_REMOTE=$(git remote get-url origin)
  if [ "$CURRENT_REMOTE" != "$REMOTE" ]; then
    git remote set-url origin "$REMOTE"
    log_done "Remote updated to: $REMOTE"
  else
    log_info "Remote already set: $REMOTE"
  fi
fi

# Ensure we're on main
git checkout -B main 2>/dev/null || true

# Add and commit
git add -A
git commit -m "feat: initial project scaffold with design docs and harness v3" || log_info "Nothing to commit"

# Push
git push -u origin main 2>&1 && log_done "Pushed to origin/main" || {
  log_info "Push failed — try: git push -u origin main --force (if new empty repo)"
  log_info "Or ensure the repo exists at: $REMOTE"
}

log ""
log "${GREEN}=== Scaffold complete ===${NC}"
log ""
log "Next steps:"
log "  1. Verify docs at: https://github.com/GurthBro0ks/gnome_game"
log "  2. Copy any missing design docs from Claude project knowledge"
log "  3. Start a new agent session with the Codex auto-prompt"
log "  4. First task: create the implementation planning pack (prod-001)"
