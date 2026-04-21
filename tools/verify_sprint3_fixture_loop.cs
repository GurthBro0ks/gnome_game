using System;
using System.Globalization;
using System.IO;
using GnomeGame.Core;
using GnomeGame.Data;

public static class VerifySprint3FixtureLoop
{
    public static int Main()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "gnome_game_sprint3_verify_" + Guid.NewGuid().ToString("N"));

        try
        {
            Directory.CreateDirectory(tempRoot);

            var auth = new AuthManager();
            auth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);

            var saveManager = new SaveManager(tempRoot);
            saveManager.LoadOrCreateProfile(auth.ActiveUid);

            var burrowProductionService = new BurrowProductionService();
            var explorationService = new LoamwakeExplorationService();
            var fixtureService = new FixtureService();

            Assert(!saveManager.Profile.burrow_state.rootmine.unlocked, "Rootmine remains locked before Burrow level 2");
            Assert(saveManager.Profile.account.fixture_cap == 0, "Fresh profile starts with Fixture cap 0");
            Assert(FixtureStateHelper.IsVaultShellOnly(saveManager.Profile), "Vault starts shell-only");

            var productionTick = ParseIso(saveManager.Profile.burrow_state.last_production_tick).AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, productionTick);
            saveManager.SaveProfile("sprint3 verification starter production");

            var profileService = new ProfileService();
            profileService.Initialize(saveManager, burrowProductionService, explorationService, () => productionTick);
            Assert(profileService.GatherDewpond(), "Dewpond gather succeeds");
            Assert(profileService.ExpandBurrow(), "Burrow expands to level 2");
            Assert(saveManager.Profile.burrow_state.rootmine.unlocked, "Rootmine unlocks at Burrow level 2");

            var rootmineTick = productionTick.AddSeconds(600);
            burrowProductionService.ProcessProduction(saveManager.Profile, rootmineTick);
            saveManager.SaveProfile("sprint3 verification rootmine production");
            Assert(saveManager.Profile.burrow_state.rootmine.stored_tangled_root_twine > 0, "Rootmine produces Tangled Root Twine after unlock");
            Assert(saveManager.Profile.burrow_state.rootmine.stored_crumbled_ore_chunk > 0, "Rootmine produces Crumbled Ore Chunk after unlock");

            var storedTwine = saveManager.Profile.burrow_state.rootmine.stored_tangled_root_twine;
            var storedOre = saveManager.Profile.burrow_state.rootmine.stored_crumbled_ore_chunk;
            Assert(profileService.GatherRootmine(), "Rootmine gather succeeds");
            Assert(saveManager.Profile.wallet.loamwake_materials.tangled_root_twine >= storedTwine, "Gathered Rootmine Twine enters wallet");
            Assert(saveManager.Profile.wallet.loamwake_materials.crumbled_ore_chunk >= storedOre, "Gathered Rootmine Ore enters wallet");
            Assert(saveManager.Profile.burrow_state.rootmine.stored_tangled_root_twine == 0, "Rootmine Twine storage clears after gather");

            saveManager.Profile.wallet.mooncaps = 200;
            saveManager.SaveProfile("sprint3 verification seed craft mooncaps");
            Assert(profileService.UnlockFirstFixtureCap(), "First Fixture cap unlock succeeds");
            Assert(saveManager.Profile.account.fixture_cap == 1, "Fixture cap is now 1");

            var mooncapsBeforeCraft = saveManager.Profile.wallet.mooncaps;
            var twineBeforeCraft = saveManager.Profile.wallet.loamwake_materials.tangled_root_twine;
            Assert(profileService.CraftFirstFixture(), "Crafting first Fixture succeeds");
            Assert(saveManager.Profile.wallet.mooncaps == mooncapsBeforeCraft - FixtureService.FirstFixtureMooncapCost, "Crafting consumes Mooncaps");
            Assert(saveManager.Profile.wallet.loamwake_materials.tangled_root_twine == twineBeforeCraft - FixtureService.FirstFixtureTwineCost, "Crafting consumes Tangled Root Twine");
            Assert(FixtureStateHelper.FindFirstFixture(saveManager.Profile) != null, "Crafting grants owned first Fixture");

            Assert(profileService.EquipFirstFixture(), "Equipping first Fixture succeeds");
            Assert(saveManager.Profile.fixture_state.equipped_fixture_instance_ids.Count == 1, "Equipped list contains one Fixture");
            Assert(FixtureStateHelper.GetEquippedFixtureBonus(saveManager.Profile) == 2, "Equipped Fixture grants +2 expedition power");

            saveManager.Profile.fixture_state.fixture_inventory.Add(new FixtureInstanceData
            {
                instance_id = "verify-extra-fixture",
                fixture_id = "verify_extra_fixture",
                upgrade_level = 0,
                acquired_at = ProfileFactory.UtcNowIso()
            });

            string equipStatus;
            Assert(!fixtureService.EquipFixtureInstance(saveManager.Profile, "verify-extra-fixture", out equipStatus), "Equipping another Fixture fails when cap is full");
            Assert(saveManager.Profile.fixture_state.equipped_fixture_instance_ids.Count == 1, "Fixture cap prevents extra equipped IDs");

            saveManager.Profile.strata_state.loamwake.keeper_lw_001_defeated = true;
            Assert(FixtureStateHelper.ApplyMilestoneUnlocks(saveManager.Profile), "Keeper milestone unlocks first Hat");
            saveManager.SaveProfile("sprint3 verification hat unlock");
            Assert(FixtureStateHelper.HasHat(saveManager.Profile, FixtureStateHelper.FirstHatId), "First Hat unlock is stored");
            Assert(saveManager.Profile.fixture_state.equipped_fixture_instance_ids.Count == 1, "Hat does not consume Fixture cap");
            Assert(FixtureStateHelper.GetHatExpeditionPowerBonus(saveManager.Profile) == 1, "First Hat grants +1 expedition power");
            Assert(FixtureStateHelper.GetTotalExpeditionPowerBonus(saveManager.Profile) == 3, "Fixture and Hat bonuses stack visibly");
            Assert(FixtureStateHelper.IsVaultShellOnly(saveManager.Profile), "Vault remains shell-only after Keeper milestone");

            var reloadedAuth = new AuthManager();
            reloadedAuth.Initialize(tempRoot, authEnabled: false, simulateAuthFailure: false);
            var reloadedSaveManager = new SaveManager(tempRoot);
            reloadedSaveManager.LoadOrCreateProfile(reloadedAuth.ActiveUid);

            Assert(reloadedSaveManager.Profile.burrow_state.rootmine.unlocked, "Rootmine unlock persists across save/load");
            Assert(reloadedSaveManager.Profile.wallet.loamwake_materials.crumbled_ore_chunk >= storedOre, "Rootmine gathered material wallet persists");
            Assert(FixtureStateHelper.FindFirstFixture(reloadedSaveManager.Profile) != null, "Fixture inventory restores across save/load");
            Assert(reloadedSaveManager.Profile.fixture_state.equipped_fixture_instance_ids.Count == 1, "Equipped ordered list restores across save/load");
            Assert(FixtureStateHelper.HasHat(reloadedSaveManager.Profile, FixtureStateHelper.FirstHatId), "Hat state restores across save/load");
            Assert(reloadedSaveManager.Profile.hat_state.visible_hat_id == FixtureStateHelper.FirstHatId, "Visible Hat restores across save/load");
            Assert(FixtureStateHelper.GetTotalExpeditionPowerBonus(reloadedSaveManager.Profile) == 3, "Power bonus restores across save/load");
            Assert(FixtureStateHelper.IsVaultShellOnly(reloadedSaveManager.Profile), "Vault shell does not create treasure progression on reload");

            Console.WriteLine("PASS Sprint 3 Fixture loop verification");
            Console.WriteLine("Save path: " + reloadedSaveManager.SaveFilePath);
            Console.WriteLine("Fixture cap: " + reloadedSaveManager.Profile.fixture_state.equipped_fixture_instance_ids.Count + "/" + reloadedSaveManager.Profile.account.fixture_cap);
            Console.WriteLine("First Hat visible: " + reloadedSaveManager.Profile.hat_state.visible_hat_id);
            Console.WriteLine("Sprint 3 power bonus: +" + FixtureStateHelper.GetTotalExpeditionPowerBonus(reloadedSaveManager.Profile));
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine("FAIL Sprint 3 Fixture loop verification: " + exception.Message);
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
