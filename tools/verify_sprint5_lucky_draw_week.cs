using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint5LuckyDrawWeek
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint5_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            var burrowProductionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();
            var profileService = new ProfileService();

            var eventState = saveManager.Profile.event_progress.lucky_draw_week;
            Assert(!eventState.unlocked && !eventState.active, "Lucky Draw Week starts hidden before Greta gate");
            Assert(saveManager.Profile.wallet.lucky_draws == 0, "Fresh profile starts with 0 Lucky Draw tickets");

            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, productionTick);
            saveManager.SaveProfile("sprint5 verification starter production");

            profileService.Initialize(saveManager, burrowProductionService, explorationService, () => productionTick);
            Assert(profileService.GatherDewpond(), "Gathering Dewpond succeeds");
            Assert(profileService.GatherMushpatch(), "Gathering Mushpatch succeeds");
            Assert(profileService.EnterLoamwake() || saveManager.Profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId, "Entering Loamwake succeeds");
            Assert(profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId), "Rootvine Shelf first clear succeeds");
            Assert(!saveManager.Profile.event_progress.lucky_draw_week.unlocked, "Lucky Draw Week remains hidden before Greta unlock");

            Assert(profileService.ReadGretaIntroPost(), "Greta intro Post completes");
            eventState = saveManager.Profile.event_progress.lucky_draw_week;
            Assert(eventState.unlock_gate_met, "Greta unlock satisfies Lucky Draw gate");
            Assert(eventState.unlocked && eventState.active, "Lucky Draw Week unlocks and becomes active");

            Assert(profileService.ClaimLuckyDrawWeeklyTicket(), "Weekly free Lucky Draw claim succeeds");
            Assert(saveManager.Profile.wallet.lucky_draws == 1, "Weekly claim grants 1 Lucky Draw");
            saveManager.SaveProfile("sprint5 after weekly claim");

            var reloadedAfterClaim = new SaveManager(tempRoot);
            reloadedAfterClaim.LoadOrCreateProfile(auth.ActiveUid);
            Assert(reloadedAfterClaim.Profile.event_progress.lucky_draw_week.weekly_claimed, "Weekly claim state persists");
            Assert(reloadedAfterClaim.Profile.wallet.lucky_draws == 1, "Lucky Draw ticket count persists after weekly claim");

            profileService.Initialize(reloadedAfterClaim, burrowProductionService, explorationService, () => productionTick);
            Assert(profileService.PullLuckyDraw(), "One Lucky Draw pull succeeds");
            var pulledProfile = reloadedAfterClaim.Profile;
            Assert(pulledProfile.wallet.lucky_draws == 0, "Pull consumes exactly 1 Lucky Draw");
            Assert(pulledProfile.event_progress.lucky_draw_week.pull_count == 1, "Pull count increments to 1");
            Assert(pulledProfile.event_progress.lucky_draw_week.ladder_progress == 1, "Ladder progress follows pull count");
            Assert(pulledProfile.event_progress.lucky_draw_week.festival_ledger.progress_points == 1, "Festival Ledger free lane progress follows pull count");
            Assert(pulledProfile.event_progress.lucky_draw_week.pull_history.Count == 1, "Pull history records latest pull");
            Assert(pulledProfile.wallet.mooncaps >= 90, "First controlled pull grants a valid Mooncap reward");

            Assert(profileService.ClaimFestivalLedgerReward(), "First Festival Ledger free-lane reward claims");
            Assert(pulledProfile.event_progress.lucky_draw_week.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_001_first_pull"), "Claimed Festival Ledger tier persists in memory");

            var mooncapsBeforeStall = pulledProfile.wallet.mooncaps;
            Assert(mooncapsBeforeStall >= 250, "Verification profile has enough Mooncaps for Lucky Stall Mooncap sink");
            Assert(profileService.BuyLuckyStallMooncapDraw(), "Lucky Stall Mooncap Lucky Draw purchase succeeds");
            Assert(pulledProfile.wallet.mooncaps == mooncapsBeforeStall - 250, "Lucky Stall purchase consumes 250 Mooncaps");
            Assert(pulledProfile.wallet.lucky_draws == 1, "Lucky Stall purchase grants 1 Lucky Draw");
            Assert(!profileService.BuyLuckyStallMooncapDraw(), "Lucky Stall weekly purchase limit blocks a second Mooncap draw");
            Assert(LuckyDrawEventService.GetRemainingPurchases(pulledProfile, LuckyDrawEventService.StallMooncapDrawId, 1) == 0, "Lucky Stall purchase count persists in state");

            Assert(!pulledProfile.event_progress.lucky_draw_week.paid_path_active, "No paid event path is active");
            Assert(!pulledProfile.event_progress.lucky_draw_week.iap_enabled, "No IAP path is enabled");

            reloadedAfterClaim.SaveProfile("sprint5 verification final state");
            var finalReload = new SaveManager(tempRoot);
            finalReload.LoadOrCreateProfile(auth.ActiveUid);
            var finalEvent = finalReload.Profile.event_progress.lucky_draw_week;
            Assert(finalEvent.unlocked && finalEvent.active, "Save/load restores event unlock and active state");
            Assert(finalReload.Profile.wallet.lucky_draws == 1, "Save/load restores ticket count");
            Assert(finalEvent.pull_count == 1, "Save/load restores pull count");
            Assert(finalEvent.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_001_first_pull"), "Save/load restores claimed Festival Ledger milestone");
            Assert(LuckyDrawEventService.GetRemainingPurchases(finalReload.Profile, LuckyDrawEventService.StallMooncapDrawId, 1) == 0, "Save/load restores Lucky Stall purchase count");
            Assert(!finalEvent.paid_path_active && !finalEvent.iap_enabled, "Save/load keeps paid/IAP paths inactive");

            Console.WriteLine("PASS Sprint 5 Lucky Draw Week verification");
            Console.WriteLine("Save path: " + finalReload.SaveFilePath);
            Console.WriteLine("Lucky Draw tickets: " + finalReload.Profile.wallet.lucky_draws);
            Console.WriteLine("Pull count: " + finalEvent.pull_count);
            Console.WriteLine("Ledger claimed tiers: " + finalEvent.festival_ledger.claimed_tier_ids.Count);
            Console.WriteLine("Paid path active: " + finalEvent.paid_path_active + ", IAP enabled: " + finalEvent.iap_enabled);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 5 Lucky Draw Week verification: " + exception);
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
