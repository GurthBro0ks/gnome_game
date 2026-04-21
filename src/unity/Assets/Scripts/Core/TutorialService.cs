using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public static class TutorialService
    {
        public const string GatherStep = "tut_01_gather";
        public const string ExpandStep = "tut_02_expand";
        public const string EnterLoamwakeStep = "tut_03_enter_loamwake";
        public const string ClearRootvineStep = "tut_04_clear_rootvine";
        public const string UnlockFixtureCapStep = "tut_05_unlock_fixture_cap";
        public const string CraftFixtureStep = "tut_06_craft_fixture";
        public const string EquipFixtureStep = "tut_07_equip_fixture";
        public const string GretaStep = "tut_08_unlock_greta";
        public const string LuckyDrawStep = "tut_09_unlock_lucky_draw";
        public const string RootrailStep = "tut_10_reveal_rootrail";
        public const string FutureShellStep = "tut_11_future_shells";
        public const string CompleteStep = "tutorial_complete";

        private static readonly string[] OrderedSteps =
        {
            GatherStep,
            ExpandStep,
            EnterLoamwakeStep,
            ClearRootvineStep,
            UnlockFixtureCapStep,
            CraftFixtureStep,
            EquipFixtureStep,
            GretaStep,
            LuckyDrawStep,
            RootrailStep,
            FutureShellStep
        };

        public static void EnsureDefaults(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return;
            }

            if (profile.account == null)
            {
                profile.account = new AccountData();
            }

            if (profile.tutorial_progress == null)
            {
                profile.tutorial_progress = new TutorialProgressData();
            }

            if (profile.tutorial_progress.completed_step_ids == null)
            {
                profile.tutorial_progress.completed_step_ids = new List<string>();
            }

            if (profile.tutorial_progress.dismissed_hint_ids == null)
            {
                profile.tutorial_progress.dismissed_hint_ids = new List<string>();
            }

            if (string.IsNullOrEmpty(profile.tutorial_progress.current_step_id))
            {
                profile.tutorial_progress.current_step_id = GatherStep;
            }

            if (string.IsNullOrEmpty(profile.tutorial_progress.latest_guidance))
            {
                profile.tutorial_progress.latest_guidance = GetGuidance(profile.tutorial_progress.current_step_id);
            }

            if (string.IsNullOrEmpty(profile.tutorial_progress.last_updated_at))
            {
                profile.tutorial_progress.last_updated_at = ProfileFactory.UtcNowIso();
            }
        }

        public static bool SyncProgress(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            ExplorationStateHelper.EnsureDefaults(profile);
            FixtureStateHelper.EnsureDefaults(profile);
            SocialProgressService.EnsureDefaults(profile);
            LuckyDrawEventService.EnsureDefaults(profile);
            CrackCliqueService.EnsureDefaults(profile);

            var state = profile.tutorial_progress;
            var changed = false;

            changed |= SetCompleted(state, GatherStep, HasGathered(profile));
            changed |= SetCompleted(state, ExpandStep, profile.burrow_state != null && profile.burrow_state.burrow_level >= 2);
            changed |= SetCompleted(state, EnterLoamwakeStep, HasEnteredLoamwake(profile));
            changed |= SetCompleted(state, ClearRootvineStep, profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear);
            changed |= SetCompleted(state, UnlockFixtureCapStep, profile.account.fixture_cap >= 1);
            changed |= SetCompleted(state, CraftFixtureStep, FixtureStateHelper.FindFirstFixture(profile) != null);
            changed |= SetCompleted(state, EquipFixtureStep, FixtureStateHelper.IsFirstFixtureEquipped(profile));
            changed |= SetCompleted(state, GretaStep, profile.social_progress.greta.unlocked);
            changed |= SetCompleted(state, LuckyDrawStep, profile.event_progress.lucky_draw_week.unlocked);
            changed |= SetCompleted(state, RootrailStep, profile.social_progress.rootrail.revealed);
            changed |= SetCompleted(state, FutureShellStep, profile.crack_progress.visible && profile.clique_progress.visible);

            var nextStep = FindNextStep(state);
            var nextGuidance = GetGuidance(nextStep);
            var complete = nextStep == CompleteStep;

            if (state.current_step_id != nextStep)
            {
                state.current_step_id = nextStep;
                changed = true;
            }

            if (state.latest_guidance != nextGuidance)
            {
                state.latest_guidance = nextGuidance;
                changed = true;
            }

            if (state.tutorial_window_complete != complete)
            {
                state.tutorial_window_complete = complete;
                changed = true;
            }

            var accountTutorialState = complete ? "complete" : nextStep;
            if (profile.account.tutorial_state != accountTutorialState)
            {
                profile.account.tutorial_state = accountTutorialState;
                changed = true;
            }

            if (changed)
            {
                state.last_updated_at = ProfileFactory.UtcNowIso();
            }

            return changed;
        }

        public static bool DismissCurrentHint(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            SyncProgress(profile);

            var state = profile.tutorial_progress;
            if (state.current_step_id == CompleteStep || state.dismissed_hint_ids.Contains(state.current_step_id))
            {
                return false;
            }

            state.dismissed_hint_ids.Add(state.current_step_id);
            state.last_updated_at = ProfileFactory.UtcNowIso();
            return true;
        }

        public static bool ResetProgress(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            profile.tutorial_progress.completed_step_ids.Clear();
            profile.tutorial_progress.dismissed_hint_ids.Clear();
            profile.tutorial_progress.current_step_id = GatherStep;
            profile.tutorial_progress.tutorial_window_complete = false;
            profile.tutorial_progress.latest_guidance = GetGuidance(GatherStep);
            profile.tutorial_progress.last_updated_at = ProfileFactory.UtcNowIso();
            profile.account.tutorial_state = GatherStep;
            SyncProgress(profile);
            return true;
        }

        public static string BuildGuidance(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            SyncProgress(profile);

            var state = profile.tutorial_progress;
            if (state.current_step_id == CompleteStep)
            {
                return "Guide complete: all first playable beats are reachable. Keep testing save/load and page navigation.";
            }

            if (state.dismissed_hint_ids.Contains(state.current_step_id))
            {
                return "Guide hidden for this step. Use Reset Guide in the Burrow debug area to show it again.";
            }

            return state.latest_guidance;
        }

        private static bool HasGathered(PlayerProfileData profile)
        {
            return profile != null &&
                profile.wallet != null &&
                (profile.wallet.mooncaps > 0 || profile.wallet.mushcaps > 0 || profile.burrow_state.expand_count > 0);
        }

        private static bool HasEnteredLoamwake(PlayerProfileData profile)
        {
            return profile != null &&
                profile.strata_state != null &&
                (profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId ||
                    profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear);
        }

        private static bool SetCompleted(TutorialProgressData state, string stepId, bool complete)
        {
            if (!complete || state.completed_step_ids.Contains(stepId))
            {
                return false;
            }

            state.completed_step_ids.Add(stepId);
            return true;
        }

        private static string FindNextStep(TutorialProgressData state)
        {
            for (var i = 0; i < OrderedSteps.Length; i++)
            {
                if (!state.completed_step_ids.Contains(OrderedSteps[i]))
                {
                    return OrderedSteps[i];
                }
            }

            return CompleteStep;
        }

        private static string GetGuidance(string stepId)
        {
            switch (stepId)
            {
                case GatherStep:
                    return "Guide: gather Dewpond and Mushpatch output first. Those starter piles fund Expand and Loamwake travel.";
                case ExpandStep:
                    return "Guide: use Expand Burrow once. Level 2 opens Rootmine and gives Loamwake enough expedition power.";
                case EnterLoamwakeStep:
                    return "Guide: enter Loamwake from the Strata Gate, then try Rootvine Shelf.";
                case ClearRootvineStep:
                    return "Guide: clear Rootvine Shelf. The first clear gives enough Twine and Mooncaps for the first Fixture.";
                case UnlockFixtureCapStep:
                    return "Guide: open Fixture Workshop and unlock the first Fixture Cap.";
                case CraftFixtureStep:
                    return "Guide: craft Root-Bitten Shovel Strap. The first Rootvine clear should pay for it.";
                case EquipFixtureStep:
                    return "Guide: equip Root-Bitten Shovel Strap so Loamwake checks include its power bonus.";
                case GretaStep:
                    return "Guide: return to Burrow Post and read Greta's intro after Rootvine Shelf is clear.";
                case LuckyDrawStep:
                    return "Guide: Greta unlocks Lucky Draw Week in this prototype. Claim the free Lucky Draw before pulling.";
                case RootrailStep:
                    return "Guide: complete Greta's follow-up, clear Mudpipe Hollow, then follow Greta to reveal Rootrail.";
                case FutureShellStep:
                    return "Guide: after Rootrail reveals, The Crack and Clique appear as future-facing shells from the Burrow.";
                default:
                    return "Guide complete.";
            }
        }
    }
}
