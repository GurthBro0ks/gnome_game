# Specialist Prompt — Phase 2B Execution

**File:** `docs/08_production/specialist_prompt_phase2b.md`  
**Status:** Ready for use  
**Date:** 2026-04-19  
**Target:** Next AI specialist or content designer taking over Phase 2B work

---

## Mission

You are the **Phase 2B content and flow specialist** for Gnome Game.

Phase 2A established the canon system specs, data schema, economy model, MVP core loop, UI flow, content table templates, and backend options. All those documents are now substantially complete.

Phase 2B's job is to create the **first actual content**: the Loamwake content sheet, the first Confidant chain, the first Wanderer pool, the Lucky Draw Week MVP, the tutorial/onboarding flow, the save/profile flow, and the IAP catalog. These files turn the Phase 2A specs into a buildable game.

---

## Mandatory Startup Sequence

1. `pwd` — confirm you are in `/home/mint/projects/gnome_game`
2. `cat claude-progress.md`
3. `cat feature_list.json`
4. `git log --oneline -10`
5. `source init.sh`
6. Read these four files in full before writing anything:
   - `docs/04_systems/fixtures_and_hats_system_v1.md`
   - `docs/04_systems/rootrail_system_v1.md`
   - `docs/04_systems/core_loop_mvp_spec.md`
   - `docs/08_production/phase2a_salvage_report.md`

---

## Canon Lock (Do Not Change Without Discussion)

These rules are non-negotiable. They come from the Phase 2A canon lock and must be applied to every Phase 2B file:

- Clothes are removed. Every equipable piece is a Fixture.
- No rigid equipment slots.
- Duplicate-style builds are allowed.
- No negative stats on any Fixture.
- Personal Fixture cap starts at 0 and grows. First session: 0→1→2.
- Hats are NOT part of the 12-Fixture cap.
- Only one hat visible at a time. Every unlocked hat grants a tiny permanent passive.
- War Fixtures live in the War Armory only — NOT in personal Fixture lists.
- Ascender is removed. The Rootrail is the long-project restoration system.
- Rootrail repair steps require: Rootrail Parts + Forgotten Manual (if gated) + timer.
- Forgotten Manuals are permanent, NOT consumed, tracked in the codex.
- Forgotten Manuals are SEPARATE from Elder Books.
- Bronze Shovels stays Bronze Shovels.
- Temporary placeholder Confidant/Burrowfolk content is ALLOWED for MVP.
- Final friend-questionnaire Confidant content comes later — do not write it yet.

**Forbidden:**
- Do not use: Clothes, Cloth Assembly, Fit, Ascender, Ascender Parts
- Do not create: rigid slot types (head/chest/legs etc.)
- Do not put: War Fixtures in personal Fixture cap
- Do not merge: Forgotten Manuals into Elder Books
- Do not create: premium-only power Fixtures or pay-only progression
- Do not write: final friend-questionnaire Confidant content (placeholder only)

---

## File Order

Tackle files in this order. Do not skip ahead — each file is a prerequisite for the next.

### 1. `docs/06_content/loamwake_mvp_content_sheet.md` ← **START HERE**

**Why first:** All other Phase 2B content references Loamwake zones, materials, encounters, and rewards. Until this is locked, other content references are provisional.

**What it must contain:**
- All launch zones (zone_lw_001 through zone_lw_00N) with zone IDs, names, Mushcap costs, resource drops
- All Loamwake Fixture material names (lock the names used in core_loop_mvp_spec.md as provisional)
- First Warden definition (The Mudgrip / war_lw_001_mudgrip) with stats and rewards
- All Buried Clue definitions for Loamwake, including the one containing fman_001_terminal_routing
- First Rootrail Forgotten Manual definition (fman_001_terminal_routing) with full columns
- First 5 Fixture recipes with locked material names
- Encounter pool table for Loamwake (Wanderers, Runaways, Wild Ones — placeholder names ok)
- Loamwake zone progression Duty chain (zone_lw_001 clear Duties)
- First Burrow Post pool (3–5 posts)

**Template to use:** `docs/06_content/content_table_templates.md` — Zone, Warden, Buried Clue, Fixture, Fixture Recipe, Forgotten Manual, Wanderer, Runaway, Wild One, Duty, Burrow Post templates

**Acceptance criteria:**
- [ ] All zone_id values follow `zone_lw_NNN_slug` convention
- [ ] All Fixture material names are canonical (no "provisional" notes remaining)
- [ ] First Forgotten Manual columns fully match the Forgotten Manual template spec
- [ ] All Fixture recipes reference material names defined in this same sheet
- [ ] No Clothes, Ascender Parts, or non-canon resources in any drop table
- [ ] mvp_status is set for every row

---

### 2. `docs/06_content/first_confidant_chain.md`

**Depends on:** loamwake_mvp_content_sheet.md (needs zone IDs and Duty references)

**What it must contain:**
- Two placeholder Confidants (cnf_001_placeholder_greta, cnf_002_placeholder_mossvane — rename if needed)
- Each Confidant's full entry using the Confidant template columns
- Each Confidant's full Duty chain (at least 3 Duties per Confidant)
- `placeholder_flag: true` on all rows
- `replacement_note: "Pending friend-questionnaire replacement"` in notes
- Bond level scale (should be 1–5 based on Duty chain completion)
- Heritage Stat bonuses per bond level (use craft/lore as primary focus)

**Acceptance criteria:**
- [ ] Both Confidants are functional with real Duty objectives (not placeholder text in objectives)
- [ ] All Duty rewards reference real resource IDs from loamwake_mvp_content_sheet.md
- [ ] `placeholder_flag: true` on all Confidant rows
- [ ] No final questionnaire lore — backstory hooks are brief and world-flavored only
- [ ] Each Duty chain has at least one Duty that leads the player toward the Rootrail station

---

### 3. `docs/06_content/first_wanderer_pool.md`

**Depends on:** loamwake_mvp_content_sheet.md (needs zone IDs)

**What it must contain:**
- 5–8 Wanderer entries using the Wanderer template
- 3–5 Runaway entries using the Runaway template
- 3–5 Wild One entries using the Wild One template
- All encounter_zones reference valid Loamwake zone IDs
- spawn_weight values set for each (drives encounter table frequency)
- flavor_intro lines for each Wanderer (one sentence; gnome-world tone)

**Acceptance criteria:**
- [ ] All zone references match zone IDs in loamwake_mvp_content_sheet.md
- [ ] At least one Wanderer has a trade interaction that rewards Rootrail Parts
- [ ] All Wild One entries have valid `required_battle_stat` values
- [ ] Flavor intros are gnome-world-appropriate (post-fallout, underground, fungal)
- [ ] No crossover with Confidant names

---

### 4. `docs/06_content/lucky_draw_week_mvp.md`

**Depends on:** loamwake_mvp_content_sheet.md, first_confidant_chain.md (for reward refs)

**What it must contain:**
- Lucky Draw Week event definition (evt_luckydraw_001)
- Event ladder: 5–8 tiers with Lucky Draw pull thresholds and reward packets
- Lucky Stall shop: 4–6 items purchasable with Lucky Draws/Glowcaps
- Lucky Draw pull table: weighted reward pool (Fixtures, Hats, materials, Mooncaps)
- Free Lucky Draw floor rule: how many free Lucky Draws a non-paying player gets per week
- Festival Ledger integration: how Lucky Draw Week contributes Festival Marks

**Acceptance criteria:**
- [ ] Free Lucky Draw floor is at least 3 per week without spending Glowcaps
- [ ] No premium-only power Fixture in the pull table
- [ ] All Fixture rewards in the pull table are also obtainable via crafting (within one event cycle)
- [ ] Lucky Stall includes at least one item buyable with Mooncaps only (no Glowcap gate)
- [ ] Event timing: active from day 15 after account creation (matches UI unlock rules)

---

### 5. `docs/04_systems/tutorial_and_onboarding_flow.md`

**Depends on:** loamwake_mvp_content_sheet.md, core_loop_mvp_spec.md (use as reference)

**What it must contain:**
- Step-by-step tutorial beat sequence (matches core_loop_mvp_spec.md first 10 minutes)
- Each tutorial step: trigger, UI action, text shown, system unlocked, skip-allowed flag
- Tutorial beats that CANNOT be skipped: Fixture cap tutorial, first Fixture craft, first equip
- Optional tutorial beats: Burrowfolk output explanation, Rootrail reveal walkthrough
- New Player Experience (NPE) pacing: how many new surfaces appear per step
- Anti-overload checkpoints: moments where the game pauses new unlocks to let the player breathe

**Acceptance criteria:**
- [ ] Fixture cap 0→1 tutorial beat is marked `skip_allowed: false`
- [ ] First Fixture craft and equip beats are marked `skip_allowed: false`
- [ ] No more than 2 new UI surfaces introduced in any single 10-minute block
- [ ] Tutorial does not mention Clique, War Armory, Deepening, or Memory Shift
- [ ] Tutorial mentions Rootrail only as "an old structure to investigate" — not a full explanation

---

### 6. `docs/08_production/save_state_and_profile_flow.md`

**Depends on:** data_schema_v1.md (use as field reference)

**What it must contain:**
- Profile creation flow: what is created on first account creation (with specific field defaults)
- Local save structure for prototype (which fields, which file, format)
- Cloud save migration flow: from local → cloud (field by field mapping)
- Session start / session end behavior: what gets loaded and saved
- MVP minimum save payload (list of field/entity subsets — matches data_schema_v1.md)
- Schema versioning: how `schema_version` is incremented
- Hat passive accumulation: how `hat_passives` accumulator is recalculated on unlock

**Acceptance criteria:**
- [ ] Profile creation includes `fixture_cap: 0` as a default (not a higher value)
- [ ] `hat_unlock.permanent_passive_active` is always `true` once set
- [ ] `rootrail_codex_entry.consumed` is always `false`
- [ ] Local save format is plaintext JSON for prototype debuggability
- [ ] Cloud migration section describes how `schema_version` gates migration logic
- [ ] All field defaults match data_schema_v1.md entity definitions

---

### 7. `docs/08_production/iap_catalog_v1.md`

**Depends on:** economy model CSV, lucky_draw_week_mvp.md

**What it must contain:**
- IAP product catalog with SKU IDs, display names, Glowcap or resource grants
- Starter pack definition (if any — must be one-time only)
- Monthly subscription (if any — must include earnable-equivalent content)
- Pricing tiers: $0.99, $2.99, $4.99, $9.99, $19.99, $49.99 (standard mobile tiers)
- Free vs. paid Glowcap ratio: how many Glowcaps can a free player earn per month vs. a moderate spender
- No-IAP-required path: explicit statement of what content is reachable without spending

**Acceptance criteria:**
- [ ] No premium-only power Fixture in any IAP bundle
- [ ] All IAP Fixture content is obtainable via crafting within 2 event cycles
- [ ] IAP hats are cosmetically distinct but do NOT have superior passives vs. earnable hats
- [ ] Free Glowcap monthly earn rate is at least 20% of a moderate spender's total
- [ ] Each IAP product has a `contains_power_item: false` field (explicit validation sentinel)

---

## Dependencies Summary

```
loamwake_mvp_content_sheet.md         ← Start here; no deps
    ├─► first_confidant_chain.md
    ├─► first_wanderer_pool.md
    └─► lucky_draw_week_mvp.md
         ├─► tutorial_and_onboarding_flow.md
         ├─► save_state_and_profile_flow.md
         └─► iap_catalog_v1.md
```

---

## Quality Bar

A Phase 2B file is only **done** when:

1. It is game-specific (no generic RPG placeholder content)
2. It is canon-accurate (all Phase 2A canon lock rules applied)
3. A developer or content designer can use it to build or populate the system **without guessing**
4. It references only ID strings and column names that exist in Phase 2A definitions
5. All `mvp_status` fields are set
6. All placeholder content is flagged with `placeholder_flag: true`
7. The markdown renders cleanly

---

## Tracking Updates Required on Completion

After completing each Phase 2B file, update:
- `feature_list.json` — add new feature flags for each file (prefix `content-`)
- `claude-progress.md` — append a brief log entry describing what was created and what comes next
- Commit with descriptive message: `content: <filename> — Phase 2B [file number]`

---

## End-of-Session Checklist

1. All new docs follow naming conventions from `docs/01_core/master_glossary.md`
2. `feature_list.json` updated
3. `claude-progress.md` updated
4. `git add -A && git commit -m "content: <description>"`
5. `git push origin main`

---

## Unresolved Canon Questions for Phase 2B to Resolve

The following items are flagged as UNRESOLVED in Phase 2A docs and should be locked in Phase 2B:

1. **Loamwake Fixture material names** — Provisional names exist in core_loop_mvp_spec.md; lock in loamwake_mvp_content_sheet.md
2. **First Forgotten Manual identity** — `fman_001_terminal_routing` is a placeholder title; confirm or revise in loamwake_mvp_content_sheet.md
3. **Exact Rootrail Parts costs for steps 1–5** — Suggested values in rootrail_system_v1.md; validate in loamwake repair table
4. **Confidant bond level scale** — Assumed 1–5; confirm in first_confidant_chain.md
5. **Crack unlock condition** — Not yet defined; add to feature_list.json as a separate flag if needed
6. **Free Lucky Draw floor per week** — Set the minimum in lucky_draw_week_mvp.md
7. **Rootrail aesthetic direction** — Surface ruin with vines vs. underground; specify in loamwake_mvp_content_sheet.md visual notes

---

## What NOT to Do

- Do not rewrite Phase 2A system docs (fixtures_and_hats_system_v1.md, rootrail_system_v1.md, data_schema_v1.md) unless you find a genuine canon error
- Do not update master_glossary.md without explicit scope — flag any needed glossary entries as UNRESOLVED
- Do not create War Armory content (War Fixtures) — that is Phase 3
- Do not write Strata 2–5 content
- Do not design Great Dispute or Memory Shift gameplay
- Do not write final friend-questionnaire Confidant content
- Do not mark feature flags `true` without verifying the file passes its acceptance criteria
