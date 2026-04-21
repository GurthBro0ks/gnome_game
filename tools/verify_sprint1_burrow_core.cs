using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint1BurrowCore
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint1_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            Assert(File.Exists(saveManager.SaveFilePath), "default profile was saved to disk");
            Assert(saveManager.Profile.burrow_state.dewpond.level == 1, "fresh profile starts with Dewpond level 1");
            Assert(saveManager.Profile.burrow_state.mushpatch.level == 1, "fresh profile starts with Mushpatch level 1");
            Assert(saveManager.Profile.burrow_state.rootmine.unlocked == false, "Rootmine starts locked");

            var productionService = new BurrowProductionService();
            var startTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick);
            var producedAt = startTick.AddSeconds(150);

            Assert(productionService.ProcessProduction(saveManager.Profile, producedAt), "simulated elapsed time changes Burrow production");
            Assert(saveManager.Profile.burrow_state.dewpond.stored_output == 50, "150 seconds yields 50 Dewpond Mooncaps");
            Assert(saveManager.Profile.burrow_state.mushpatch.stored_output == 15, "150 seconds yields 15 Mushpatch Mushcaps");

            saveManager.SaveProfile("verification production tick");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager, productionService, () => producedAt);
            Assert(profileService.GatherDewpond(), "Gather Dewpond succeeds");
            Assert(profileService.GatherMushpatch(), "Gather Mushpatch succeeds");
            Assert(saveManager.Profile.wallet.mooncaps == 50, "Gather Dewpond transfers 50 Mooncaps into the wallet");
            Assert(saveManager.Profile.wallet.mushcaps == 15, "Gather Mushpatch transfers 15 Mushcaps into the wallet");
            Assert(saveManager.Profile.burrow_state.dewpond.stored_output == 0, "Dewpond stored output clears after gather");
            Assert(saveManager.Profile.burrow_state.mushpatch.stored_output == 0, "Mushpatch stored output clears after gather");

            Assert(profileService.ExpandBurrow(), "Expand from Burrow level 1 to 2 succeeds after enough Mooncaps");
            Assert(saveManager.Profile.burrow_state.burrow_level == 2, "Burrow level becomes 2");
            Assert(saveManager.Profile.burrow_state.expand_count == 1, "Expand count increments");
            Assert(saveManager.Profile.wallet.mooncaps == 0, "Expand spends 50 Mooncaps");
            Assert(saveManager.Profile.burrow_state.rootmine.unlocked, "Rootmine unlocks exactly at Burrow level 2");

            var capTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(5000);
            productionService.ProcessProduction(saveManager.Profile, capTick);
            Assert(saveManager.Profile.burrow_state.dewpond.stored_output <= saveManager.Profile.burrow_state.dewpond.storage_cap, "Dewpond output clamps to storage cap");
            Assert(saveManager.Profile.burrow_state.mushpatch.stored_output <= saveManager.Profile.burrow_state.mushpatch.storage_cap, "Mushpatch output clamps to storage cap");

            saveManager.SaveProfile("verification final save");

            var restartedAuth = new AuthManager();
            restartedAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var restartedSaveManager = new SaveManager(tempRoot);
            restartedSaveManager.LoadOrCreateProfile(restartedAuth.ActiveUid);

            Assert(restartedSaveManager.Profile.burrow_state.burrow_level == 2, "Burrow level persists across save/load");
            Assert(restartedSaveManager.Profile.burrow_state.rootmine.unlocked, "Rootmine unlock persists across save/load");
            Assert(restartedSaveManager.Profile.wallet.mushcaps == 15, "Wallet Mushcaps persist across save/load");

            Console.WriteLine("PASS Sprint 1 Burrow core verification");
            Console.WriteLine("Save path: " + saveManager.SaveFilePath);
            Console.WriteLine("Burrow level after expand: " + restartedSaveManager.Profile.burrow_state.burrow_level);
            Console.WriteLine("Rootmine unlocked: " + restartedSaveManager.Profile.burrow_state.rootmine.unlocked);
            Console.WriteLine("Wallet Mushcaps after reload: " + restartedSaveManager.Profile.wallet.mushcaps);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 1 Burrow core verification: " + exception.Message);
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
