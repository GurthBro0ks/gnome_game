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
