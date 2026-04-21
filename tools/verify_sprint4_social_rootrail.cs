using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint4SocialRootrail
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint4_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            var burrowProductionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();

            Assert(!saveManager.Profile.social_progress.greta.unlocked, "Greta is locked on fresh state");
            Assert(SocialProgressService.GetPost(saveManager.Profile, SocialProgressService.GretaIntroPostId).state == "locked", "Greta intro Post starts locked");
            Assert(!saveManager.Profile.social_progress.rootrail.revealed, "Rootrail starts unrevealed");

            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, productionTick);
            saveManager.SaveProfile("sprint4 verification starter production");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager, burrowProductionService, explorationService, () => productionTick);

            Assert(profileService.GatherDewpond(), "Gathering Dewpond succeeds");
            Assert(profileService.GatherMushpatch(), "Gathering Mushpatch succeeds for Loamwake stamina");
            var dewpondDuty = SocialProgressService.GetDailyDuty(saveManager.Profile, SocialProgressService.DewpondDutyId);
            Assert(dewpondDuty.completed && dewpondDuty.reward_claimed, "Dewpond Daily Duty completes and auto-claims reward");

            Assert(profileService.EnterLoamwake() || saveManager.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Entering Loamwake succeeds");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId), "First Rootvine Shelf clear succeeds");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear, "Rootvine Shelf first clear is recorded");
            Assert(SocialProgressService.GetPost(saveManager.Profile, SocialProgressService.GretaIntroPostId).state == "available", "Rootvine clear causes Greta intro Burrow Post to appear");
            var exploreDuty = SocialProgressService.GetDailyDuty(saveManager.Profile, SocialProgressService.ExploreDutyId);
            Assert(exploreDuty.completed && exploreDuty.reward_claimed, "Explore Daily Duty completes and persists in state");

            Assert(profileService.ReadGretaIntroPost(), "Reading Greta intro Burrow Post succeeds");
            Assert(saveManager.Profile.social_progress.greta.unlocked, "Greta intro Post unlocks Greta");
            Assert(saveManager.Profile.social_progress.greta.intro_post_completed, "Greta intro completion state is stored");
            Assert(SocialProgressService.GetPost(saveManager.Profile, SocialProgressService.GretaIntroPostId).completed, "Greta intro Post marks completed");

            Assert(profileService.CompleteGretaFirstFollowup(), "Greta first follow-up completes");
            Assert(saveManager.Profile.social_progress.greta.trust_level == 1, "Greta first follow-up increments Trust to 1");
            Assert(saveManager.Profile.social_progress.greta.first_followup_completed, "Greta follow-up completion persists in memory");

            Assert(!profileService.RevealRootrailStation(), "Rootrail reveal is blocked before Mudpipe Hollow first clear");
            Assert(!saveManager.Profile.social_progress.rootrail.revealed, "Blocked reveal does not mutate Rootrail state");

            Assert(profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.SafeRouteId), "Mudpipe Hollow clear succeeds");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.first_clear, "Mudpipe Hollow first clear is recorded");
            Assert(profileService.RevealRootrailStation(), "Rootrail reveal succeeds after Greta follow-up and Mudpipe Hollow clear");
            Assert(saveManager.Profile.social_progress.rootrail.revealed, "Rootrail reveal state is stored");
            Assert(saveManager.Profile.social_progress.rootrail.station_visible, "Rootrail station shell becomes visible");
            Assert(SocialProgressService.IsRootrailRevealOnly(saveManager.Profile), "Rootrail remains reveal-only with no repair progression");

            saveManager.SaveProfile("sprint4 verification pre-reload");

            var reloadedAuth = new AuthManager();
            reloadedAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);
            var reloadedSaveManager = new SaveManager(tempRoot);
            reloadedSaveManager.LoadOrCreateProfile(reloadedAuth.ActiveUid);

            Assert(reloadedSaveManager.Profile.social_progress.greta.unlocked, "Greta unlock restores across save/load");
            Assert(reloadedSaveManager.Profile.social_progress.greta.trust_level == 1, "Greta Trust restores across save/load");
            Assert(SocialProgressService.GetPost(reloadedSaveManager.Profile, SocialProgressService.GretaIntroPostId).completed, "Burrow Post completion restores across save/load");
            Assert(SocialProgressService.GetDailyDuty(reloadedSaveManager.Profile, SocialProgressService.DewpondDutyId).completed, "Daily Duty completion restores across save/load");
            Assert(SocialProgressService.GetDailyDuty(reloadedSaveManager.Profile, SocialProgressService.ExploreDutyId).reward_claimed, "Daily Duty reward claim restores across save/load");
            Assert(reloadedSaveManager.Profile.social_progress.rootrail.revealed, "Rootrail reveal restores across save/load");
            Assert(SocialProgressService.IsRootrailRevealOnly(reloadedSaveManager.Profile), "Rootrail shell remains reveal-only across save/load");

            Console.WriteLine("PASS Sprint 4 social/rootrail verification");
            Console.WriteLine("Save path: " + reloadedSaveManager.SaveFilePath);
            Console.WriteLine("Greta Trust: " + reloadedSaveManager.Profile.social_progress.greta.trust_level);
            Console.WriteLine("Rootrail revealed: " + reloadedSaveManager.Profile.social_progress.rootrail.revealed);
            Console.WriteLine("Rootrail repair enabled: " + reloadedSaveManager.Profile.social_progress.rootrail.repair_progression_enabled);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 4 social/rootrail verification: " + exception);
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
