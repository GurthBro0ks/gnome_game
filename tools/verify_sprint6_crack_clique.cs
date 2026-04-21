using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint6CrackClique
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint6_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            var burrowProductionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();

            Assert(!saveManager.Profile.crack_progress.visible && !saveManager.Profile.crack_progress.unlocked, "Crack starts hidden/locked before Rootrail reveal gate");
            Assert(!saveManager.Profile.clique_progress.visible && !saveManager.Profile.clique_progress.unlocked, "Clique starts hidden/locked before Rootrail reveal gate");
            Assert(CrackCliqueService.IsGreatDisputeStubOnly(saveManager.Profile), "Great Dispute starts stub-only");
            Assert(CrackCliqueService.HasNoNetworkingOrSharedState(saveManager.Profile), "No networking or shared state is active on fresh profile");

            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, productionTick);
            saveManager.SaveProfile("sprint6 verification starter production");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager, burrowProductionService, explorationService, () => productionTick);

            Assert(!profileService.ProbeCrack(), "Crack probe is blocked before the gate");
            Assert(!profileService.ClaimCliqueStipend(), "Clique stipend is blocked before the gate");

            Assert(profileService.GatherDewpond(), "Gathering Dewpond succeeds");
            Assert(profileService.GatherMushpatch(), "Gathering Mushpatch succeeds");
            Assert(profileService.EnterLoamwake() || saveManager.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Entering Loamwake succeeds");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId), "Rootvine Shelf first clear succeeds");
            Assert(profileService.ReadGretaIntroPost(), "Greta intro Post completes");
            Assert(profileService.CompleteGretaFirstFollowup(), "Greta first follow-up completes");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.SafeRouteId), "Mudpipe Hollow first clear succeeds");
            Assert(profileService.RevealRootrailStation(), "Rootrail reveal gate completes");

            Assert(saveManager.Profile.crack_progress.visible && saveManager.Profile.crack_progress.unlocked, "Rootrail reveal unlocks The Crack shell");
            Assert(saveManager.Profile.clique_progress.visible && saveManager.Profile.clique_progress.unlocked, "Rootrail reveal unlocks Clique shell");

            var crackCoinsBefore = saveManager.Profile.wallet.crack_coins;
            Assert(profileService.ProbeCrack(), "Crack probe succeeds after unlock");
            Assert(saveManager.Profile.crack_progress.probe_count == 1, "Crack probe count increments");
            Assert(saveManager.Profile.crack_progress.current_depth == 5, "Crack current depth increments by prototype amount");
            Assert(saveManager.Profile.crack_progress.best_depth == 5, "Crack best depth updates");
            Assert(saveManager.Profile.wallet.crack_coins == crackCoinsBefore + 3, "Crack probe grants 3 Crack Coins");
            Assert(saveManager.Profile.crack_progress.reward_claim_summary.Contains("Crack Coins"), "Crack reward summary is recorded");

            var favorMarksBefore = saveManager.Profile.wallet.favor_marks;
            Assert(profileService.ClaimCliqueStipend(), "Clique local stipend succeeds after unlock");
            Assert(saveManager.Profile.clique_progress.local_stipend_claimed, "Clique stipend claimed flag persists in memory");
            Assert(saveManager.Profile.wallet.favor_marks == favorMarksBefore + 5, "Clique stipend grants 5 Favor Marks");
            Assert(!profileService.ClaimCliqueStipend(), "Clique stipend cannot be claimed twice");
            Assert(CrackCliqueService.IsGreatDisputeStubOnly(saveManager.Profile), "Great Dispute remains stub-only after Clique interaction");
            Assert(CrackCliqueService.HasNoNetworkingOrSharedState(saveManager.Profile), "No networking, multiplayer, or shared-state path activates after Clique interaction");

            saveManager.SaveProfile("sprint6 verification final state");

            var finalReload = new SaveManager(tempRoot);
            finalReload.LoadOrCreateProfile(auth.ActiveUid);
            Assert(finalReload.Profile.crack_progress.visible && finalReload.Profile.crack_progress.unlocked, "Save/load restores Crack visibility and unlock state");
            Assert(finalReload.Profile.crack_progress.probe_count == 1, "Save/load restores Crack probe count");
            Assert(finalReload.Profile.crack_progress.current_depth == 5, "Save/load restores Crack current depth");
            Assert(finalReload.Profile.crack_progress.best_depth == 5, "Save/load restores Crack best depth");
            Assert(finalReload.Profile.wallet.crack_coins == crackCoinsBefore + 3, "Save/load restores Crack Coins");
            Assert(finalReload.Profile.clique_progress.visible && finalReload.Profile.clique_progress.unlocked, "Save/load restores Clique visibility and unlock state");
            Assert(finalReload.Profile.clique_progress.local_stipend_claimed, "Save/load restores Clique stipend claim");
            Assert(finalReload.Profile.wallet.favor_marks == favorMarksBefore + 5, "Save/load restores Favor Marks");
            Assert(CrackCliqueService.IsGreatDisputeStubOnly(finalReload.Profile), "Save/load keeps Great Dispute stub-only");
            Assert(CrackCliqueService.HasNoNetworkingOrSharedState(finalReload.Profile), "Save/load keeps networking and shared state inactive");

            Console.WriteLine("PASS Sprint 6 Crack + Clique verification");
            Console.WriteLine("Save path: " + finalReload.SaveFilePath);
            Console.WriteLine("Crack probes: " + finalReload.Profile.crack_progress.probe_count);
            Console.WriteLine("Best depth: " + finalReload.Profile.crack_progress.best_depth);
            Console.WriteLine("Crack Coins: " + finalReload.Profile.wallet.crack_coins);
            Console.WriteLine("Clique role: " + finalReload.Profile.clique_progress.player_role);
            Console.WriteLine("Favor Marks: " + finalReload.Profile.wallet.favor_marks);
            Console.WriteLine("Great Dispute stub only: " + finalReload.Profile.clique_progress.great_dispute_stub_only);
            Console.WriteLine("Networking enabled: " + finalReload.Profile.clique_progress.networking_enabled);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 6 Crack + Clique verification: " + exception);
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
