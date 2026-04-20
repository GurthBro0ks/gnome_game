using System;
using System.IO;
using GnomeGame.Core;

public static class VerifySprint0Persistence
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint0_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            Assert(File.Exists(saveManager.SaveFilePath), "default profile was saved to disk");
            Assert(saveManager.Profile.account.tutorial_state == "not_started", "default tutorial_state is not_started");
            Assert(saveManager.Profile.wallet.mooncaps == 0, "default mooncaps is 0");
            Assert(saveManager.Profile.burrow_state.burrow_level == 1, "default burrow_level is 1");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager);
            for (var i = 0; i < 5; i++)
            {
                profileService.GatherMooncaps(10);
            }

            Assert(saveManager.Profile.wallet.mooncaps == 50, "five gathers produce 50 Mooncaps");

            var restartedAuth = new AuthManager();
            restartedAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var restartedSaveManager = new SaveManager(tempRoot);
            restartedSaveManager.LoadOrCreateProfile(restartedAuth.ActiveUid);

            Assert(restartedSaveManager.Profile.wallet.mooncaps == 50, "restart restores 50 Mooncaps");
            Assert(restartedSaveManager.Profile.account.uid == saveManager.Profile.account.uid, "restart preserves local UID");

            var failingAuth = new AuthManager();
            failingAuth.Initialize(tempRoot, authEnabled: true, simulateAuthFailure: true);

            var failureSaveManager = new SaveManager(tempRoot);
            failureSaveManager.LoadOrCreateProfile(failingAuth.ActiveUid);

            Assert(failingAuth.IsFallback, "auth failure uses fallback mode");
            Assert(failureSaveManager.Profile.wallet.mooncaps == 50, "auth failure still loads local profile");

            Console.WriteLine("PASS Sprint 0 persistence verification");
            Console.WriteLine("Save path: " + saveManager.SaveFilePath);
            Console.WriteLine("Auth state after simulated failure: " + failingAuth.StatusMessage);
            Console.WriteLine("Mooncaps after restart: " + failureSaveManager.Profile.wallet.mooncaps);
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 0 persistence verification: " + exception.Message);
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

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
