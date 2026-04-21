using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;
using GnomeGame.Data;

public static class VerifySprint7Stabilization
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint7_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            Assert(saveManager.Profile != null, "Fresh profile boot creates a profile");
            Assert(saveManager.Profile.save_version.version == ProfileFactory.CurrentSaveVersion, "Fresh profile uses current save version");
            Assert(saveManager.Profile.burrow_state.dewpond.stored_output >= 50, "Fresh profile has starter Dewpond output to gather");
            Assert(saveManager.Profile.burrow_state.mushpatch.stored_output >= 12, "Fresh profile has starter Mushpatch output to gather");
            Assert(saveManager.Profile.tutorial_progress.current_step_id == TutorialService.GatherStep, "Tutorial initializes at gather guidance");
            Assert(saveManager.Profile.account.fixture_cap == 0, "Fresh profile starts with Fixture Cap 0");
            Assert(!saveManager.Profile.event_progress.lucky_draw_week.paid_path_active, "Paid event path starts inactive");
            Assert(!saveManager.Profile.event_progress.lucky_draw_week.iap_enabled, "IAP path starts inactive");

            var productionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();
            var profileService = new ProfileService();
            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(30);
            profileService.Initialize(saveManager, productionService, explorationService, () => productionTick);

            Assert(profileService.BuildTutorialGuidance().Contains("gather"), "Guide tells a fresh tester to gather first");
            Assert(profileService.DismissTutorialHint(), "Current tutorial hint can be dismissed");
            saveManager.SaveProfile("sprint7 dismissed tutorial hint");
            var dismissedReload = new SaveManager(tempRoot);
            dismissedReload.LoadOrCreateProfile(auth.ActiveUid);
            Assert(dismissedReload.Profile.tutorial_progress.dismissed_hint_ids.Contains(TutorialService.GatherStep), "Dismissed tutorial hint persists across save/load");
            profileService.Initialize(dismissedReload, productionService, explorationService, () => productionTick);
            Assert(profileService.ResetTutorialProgress(), "Tutorial progress can be reset from debug/API");
            Assert(!dismissedReload.Profile.tutorial_progress.dismissed_hint_ids.Contains(TutorialService.GatherStep), "Tutorial reset clears dismissed hints");

            Assert(profileService.GatherDewpond(), "Fresh tester can gather Dewpond immediately");
            Assert(profileService.GatherMushpatch(), "Fresh tester can gather Mushpatch immediately");
            Assert(dismissedReload.Profile.wallet.mooncaps >= 50, "Gathered Dewpond funds first Expand");
            Assert(dismissedReload.Profile.wallet.mushcaps >= 12, "Gathered Mushpatch funds Loamwake exploration");
            Assert(dismissedReload.Profile.tutorial_progress.completed_step_ids.Contains(TutorialService.GatherStep), "Tutorial records Gather step completion");

            Assert(profileService.ExpandBurrow(), "Fresh tester can Expand Burrow after starter gather");
            Assert(dismissedReload.Profile.burrow_state.burrow_level == 2, "Expand reaches Burrow level 2");
            Assert(dismissedReload.Profile.burrow_state.rootmine.unlocked, "Expand unlocks Rootmine shell");
            Assert(dismissedReload.Profile.tutorial_progress.completed_step_ids.Contains(TutorialService.ExpandStep), "Tutorial records Expand step completion");

            Assert(profileService.EnterLoamwake() || dismissedReload.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Loamwake entry is reachable");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId), "Rootvine Shelf first clear succeeds");
            Assert(dismissedReload.Profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear, "Rootvine first clear is recorded");
            Assert(dismissedReload.Profile.wallet.loamwake_materials.tangled_root_twine >= FixtureService.FirstFixtureTwineCost, "Rootvine first clear grants enough Twine for first Fixture");
            Assert(dismissedReload.Profile.wallet.mooncaps >= FixtureService.FirstFixtureMooncapCost, "Rootvine first clear grants enough Mooncaps for first Fixture");

            Assert(profileService.ReturnToBurrow(), "Tester can return to Burrow after Loamwake");
            Assert(profileService.UnlockFirstFixtureCap(), "Tutorial Fixture Cap unlock succeeds");
            Assert(profileService.CraftFirstFixture(), "First Fixture craft succeeds after first-clear packet");
            Assert(profileService.EquipFirstFixture(), "First Fixture equip succeeds");
            Assert(FixtureStateHelper.IsFirstFixtureEquipped(dismissedReload.Profile), "First Fixture remains equipped in state");
            Assert(dismissedReload.Profile.tutorial_progress.completed_step_ids.Contains(TutorialService.EquipFixtureStep), "Tutorial records first Fixture equip");

            Assert(SocialProgressService.GetPost(dismissedReload.Profile, SocialProgressService.GretaIntroPostId).state == "available", "Greta intro Burrow Post is available after Rootvine clear");
            Assert(profileService.ReadGretaIntroPost(), "Greta intro Post completes");
            Assert(dismissedReload.Profile.social_progress.greta.unlocked, "Greta unlocks");
            Assert(dismissedReload.Profile.event_progress.lucky_draw_week.unlocked, "Lucky Draw Week unlocks through prototype Greta gate");
            Assert(!dismissedReload.Profile.event_progress.lucky_draw_week.paid_path_active && !dismissedReload.Profile.event_progress.lucky_draw_week.iap_enabled, "Lucky Draw Week remains free-path only");

            Assert(profileService.ClaimLuckyDrawWeeklyTicket(), "Weekly free Lucky Draw claim succeeds");
            Assert(profileService.PullLuckyDraw(), "First Lucky Draw pull succeeds");
            Assert(dismissedReload.Profile.event_progress.lucky_draw_week.pull_count == 1, "Lucky Draw pull count increments");

            Assert(profileService.CompleteGretaFirstFollowup(), "Greta Trust follow-up completes");
            Assert(profileService.EnterLoamwake() || dismissedReload.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Loamwake can be re-entered");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.SafeRouteId), "Mudpipe Hollow first clear succeeds");
            Assert(dismissedReload.Profile.strata_state.loamwake.keeper_lw_001_unlocked, "First Warden unlocks after Mudpipe Hollow clear");
            Assert(profileService.ChallengeLoamwakeKeeper(), "First Warden challenge succeeds after balance pass");
            Assert(dismissedReload.Profile.strata_state.loamwake.keeper_lw_001_defeated, "First Warden defeat persists in memory");
            Assert(FixtureStateHelper.HasHat(dismissedReload.Profile, FixtureStateHelper.FirstHatId), "First Warden defeat unlocks Loamwake Dirt Cap");
            Assert(FixtureStateHelper.IsVaultShellOnly(dismissedReload.Profile), "Vault remains shell-only after Warden reward");

            Assert(profileService.RevealRootrailStation(), "Rootrail reveal succeeds after Greta follow-up and Mudpipe Hollow clear");
            Assert(dismissedReload.Profile.social_progress.rootrail.revealed, "Rootrail reveal state is recorded");
            Assert(SocialProgressService.IsRootrailRevealOnly(dismissedReload.Profile), "Rootrail remains reveal-only shell");

            Assert(dismissedReload.Profile.crack_progress.visible && dismissedReload.Profile.clique_progress.visible, "Rootrail reveal surfaces Crack and Clique shells");
            Assert(profileService.ProbeCrack(), "Crack probe still works");
            Assert(profileService.ClaimCliqueStipend(), "Clique local stipend still works");
            Assert(CrackCliqueService.IsGreatDisputeStubOnly(dismissedReload.Profile), "Great Dispute remains stub-only");
            Assert(CrackCliqueService.HasNoNetworkingOrSharedState(dismissedReload.Profile), "No networking/multiplayer/shared state is active");

            saveManager = dismissedReload;
            saveManager.SaveProfile("sprint7 final verification state");
            var finalReload = new SaveManager(tempRoot);
            finalReload.LoadOrCreateProfile(auth.ActiveUid);
            var profile = finalReload.Profile;

            Assert(profile.burrow_state.burrow_level == 2, "Save/load restores Burrow state");
            Assert(profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.first_clear, "Save/load restores Loamwake state");
            Assert(FixtureStateHelper.IsFirstFixtureEquipped(profile), "Save/load restores Fixture equip state");
            Assert(FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId), "Save/load restores Hat state");
            Assert(profile.social_progress.greta.unlocked && profile.social_progress.rootrail.revealed, "Save/load restores social and Rootrail state");
            Assert(profile.event_progress.lucky_draw_week.pull_count == 1, "Save/load restores Lucky Draw state");
            Assert(profile.crack_progress.probe_count == 1 && profile.clique_progress.local_stipend_claimed, "Save/load restores Crack and Clique state");
            Assert(profile.tutorial_progress.completed_step_ids.Contains(TutorialService.FutureShellStep), "Save/load restores tutorial state");
            Assert(!profile.event_progress.lucky_draw_week.paid_path_active && !profile.event_progress.lucky_draw_week.iap_enabled, "Save/load keeps monetization inactive");
            Assert(CrackCliqueService.HasNoNetworkingOrSharedState(profile), "Save/load keeps networking inactive");

            VerifyLegacyMigration(tempRoot);

            Console.WriteLine("PASS Sprint 7 stabilization verification");
            Console.WriteLine("Save path: " + finalReload.SaveFilePath);
            Console.WriteLine("Tutorial step: " + profile.tutorial_progress.current_step_id);
            Console.WriteLine("Burrow level: " + profile.burrow_state.burrow_level);
            Console.WriteLine("Warden defeated: " + profile.strata_state.loamwake.keeper_lw_001_defeated);
            Console.WriteLine("Lucky pulls: " + profile.event_progress.lucky_draw_week.pull_count);
            Console.WriteLine("Crack probes: " + profile.crack_progress.probe_count);
            Console.WriteLine("Clique stipend claimed: " + profile.clique_progress.local_stipend_claimed);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 7 stabilization verification: " + exception);
            return 1;
        }
        finally
        {
            if (Directory.Exists(tempRoot))
            {
                Directory.Delete(tempRoot, true);
            }
        }
    }

    private static void VerifyLegacyMigration(string parentTempRoot)
    {
        var legacyRoot = Path.Combine(parentTempRoot, "legacy");
        Directory.CreateDirectory(legacyRoot);
        var now = ProfileFactory.UtcNowIso();
        var legacyJson = "{" +
            "\"save_version\":{\"version\":1}," +
            "\"account\":{\"uid\":\"legacy-local\",\"display_name\":\"Legacy Gnome\",\"created_at\":\"" + now + "\",\"last_login\":\"" + now + "\",\"tutorial_state\":\"not_started\",\"fixture_cap\":0}," +
            "\"wallet\":{}," +
            "\"burrow_state\":{}" +
            "}";

        File.WriteAllText(Path.Combine(legacyRoot, "profile.json"), legacyJson);
        var legacySave = new SaveManager(legacyRoot);
        legacySave.LoadOrCreateProfile("legacy-local");

        Assert(legacySave.Profile.save_version.version == ProfileFactory.CurrentSaveVersion, "Legacy save migrates to current version");
        Assert(legacySave.Profile.tutorial_progress != null, "Legacy save gains tutorial state");
        Assert(legacySave.Profile.event_progress != null && legacySave.Profile.event_progress.lucky_draw_week != null, "Legacy save gains event state");
        Assert(legacySave.Profile.crack_progress != null && legacySave.Profile.clique_progress != null, "Legacy save gains Crack/Clique state");
        Assert(!legacySave.Profile.event_progress.lucky_draw_week.iap_enabled, "Legacy migration keeps IAP inactive");
        Assert(CrackCliqueService.HasNoNetworkingOrSharedState(legacySave.Profile), "Legacy migration keeps networking inactive");
    }

    private static DateTime ParseIso(string value)
    {
        return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).ToUniversalTime();
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
