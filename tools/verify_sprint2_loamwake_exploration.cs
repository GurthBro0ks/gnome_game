using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint2LoamwakeExploration
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint2_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            var burrowProductionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();

            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.unlocked, "Zone 1 starts unlocked");
            Assert(!saveManager.Profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.unlocked, "Zone 2 starts locked");
            Assert(!saveManager.Profile.strata_state.loamwake.zone_lw_003_glowroot_passage.unlocked, "Zone 3 starts locked");
            Assert(explorationService.IsStratumSelectable(saveManager.Profile, LoamwakeExplorationService.StratumId), "Loamwake is selectable");
            Assert(!explorationService.IsStratumSelectable(saveManager.Profile, "ledgerhollow"), "Later Strata are not interactable");

            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, productionTick);
            saveManager.SaveProfile("sprint2 verification production");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager, burrowProductionService, explorationService, () => productionTick);
            Assert(profileService.GatherDewpond(), "Dewpond gather succeeds before exploration");
            Assert(profileService.GatherMushpatch(), "Mushpatch gather succeeds before exploration");
            Assert(saveManager.Profile.wallet.mushcaps == 60, "Gathering after 600 seconds fills Mushcaps to the level-1 cap");

            Assert(profileService.EnterLoamwake() || saveManager.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Entering Loamwake sets the current stratum");

            Assert(profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId), "Zone 1 safe route succeeds");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear, "First clear of Zone 1 is recorded");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.unlocked, "First clear of Zone 1 unlocks Zone 2");
            Assert(saveManager.Profile.wallet.mushcaps == 59, "Safe route spends 1 Mushcap");

            Assert(profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.RiskyRouteId), "Zone 2 risky route succeeds");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.first_clear, "First clear of Zone 2 is recorded");
            Assert(saveManager.Profile.strata_state.loamwake.zone_lw_003_glowroot_passage.unlocked, "First clear of Zone 2 unlocks Zone 3");
            Assert(saveManager.Profile.strata_state.loamwake.keeper_lw_001_unlocked, "Keeper unlocks when Zone 3 unlocks");
            Assert(saveManager.Profile.wallet.mushcaps == 58, "Risky route spends 2 Mushcaps and returns 1 Mushcap on success");

            Assert(profileService.ExploreLoamwakeZone("zone_lw_003_glowroot_passage", LoamwakeExplorationService.RiskyRouteId), "Zone 3 risky route resolves");
            Assert(saveManager.Profile.strata_state.loamwake.last_exploration_result.result == "fail", "Zone 3 risky route records a fail result at Burrow level 1");
            Assert(saveManager.Profile.wallet.mushcaps == 56, "Zone 3 risky fail spends 2 Mushcaps with no rebate");
            Assert(saveManager.Profile.strata_state.loamwake.field_returns.result == "fail", "Field Returns stores the latest fail result");

            saveManager.SaveProfile("sprint2 verification pre-reload");

            var reloadedAuth = new AuthManager();
            reloadedAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);
            var reloadedSaveManager = new SaveManager(tempRoot);
            reloadedSaveManager.LoadOrCreateProfile(reloadedAuth.ActiveUid);
            Assert(reloadedSaveManager.Profile.strata_state.loamwake.zone_lw_003_glowroot_passage.unlocked, "Zone 3 unlock persists across save/load");
            Assert(reloadedSaveManager.Profile.strata_state.loamwake.last_exploration_result.result == "fail", "Latest fail result persists across save/load");
            Assert(reloadedSaveManager.Profile.strata_state.loamwake.keeper_lw_001_unlocked, "Keeper unlock persists across save/load");

            var reloadedProfileService = new ProfileService();
            reloadedProfileService.Initialize(reloadedSaveManager, burrowProductionService, explorationService, () => productionTick);
            Assert(reloadedProfileService.ExpandBurrow(), "Burrow expands to level 2 before the Keeper rematch");
            Assert(reloadedSaveManager.Profile.burrow_state.burrow_level == 2, "Burrow level is now 2");

            Assert(reloadedProfileService.ChallengeLoamwakeKeeper(), "Keeper challenge resolves");
            Assert(reloadedSaveManager.Profile.strata_state.loamwake.keeper_lw_001_defeated, "Keeper defeat is recorded");

            reloadedSaveManager.SaveProfile("sprint2 verification final save");

            var finalAuth = new AuthManager();
            finalAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);
            var finalSaveManager = new SaveManager(tempRoot);
            finalSaveManager.LoadOrCreateProfile(finalAuth.ActiveUid);

            Assert(finalSaveManager.Profile.strata_state.loamwake.keeper_lw_001_defeated, "Keeper defeat persists across save/load");
            Assert(finalSaveManager.Profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.clear_count >= 1, "Zone 1 clear count persists");
            Assert(finalSaveManager.Profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.clear_count >= 1, "Zone 2 clear count persists");

            Console.WriteLine("PASS Sprint 2 Loamwake exploration verification");
            Console.WriteLine("Save path: " + finalSaveManager.SaveFilePath);
            Console.WriteLine("Zone 3 unlocked: " + finalSaveManager.Profile.strata_state.loamwake.zone_lw_003_glowroot_passage.unlocked);
            Console.WriteLine("Keeper defeated: " + finalSaveManager.Profile.strata_state.loamwake.keeper_lw_001_defeated);
            Console.WriteLine("Current stratum after reload: " + finalSaveManager.Profile.strata_state.current_stratum_id);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 2 Loamwake exploration verification: " + exception.Message);
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
