using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public static class FixtureStateHelper
    {
        public const string FirstFixtureId = "fix_001_root_shovel_strap";
        public const string FirstFixtureRecipeId = "fix_rec_001";
        public const string FirstHatId = "hat_001_loamwake_dirt_cap";
        public const string FirstHatDisplayName = "Loamwake Dirt Cap";

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

            if (profile.wallet == null)
            {
                profile.wallet = new WalletData();
            }

            if (profile.wallet.loamwake_materials == null)
            {
                profile.wallet.loamwake_materials = new LoamwakeMaterialsData();
            }

            if (profile.fixture_state == null)
            {
                profile.fixture_state = new FixtureStateData();
            }

            if (profile.fixture_state.fixture_inventory == null)
            {
                profile.fixture_state.fixture_inventory = new List<FixtureInstanceData>();
            }

            if (profile.fixture_state.equipped_fixture_instance_ids == null)
            {
                profile.fixture_state.equipped_fixture_instance_ids = new List<string>();
            }

            if (profile.fixture_state.crafted_recipe_ids == null)
            {
                profile.fixture_state.crafted_recipe_ids = new List<string>();
            }

            RemoveInvalidEquippedIds(profile.fixture_state);
            EnforceFixtureCap(profile);

            if (string.IsNullOrEmpty(profile.fixture_state.last_updated_at))
            {
                profile.fixture_state.last_updated_at = ProfileFactory.UtcNowIso();
            }

            if (profile.hat_state == null)
            {
                profile.hat_state = new HatStateData();
            }

            if (profile.hat_state.unlocked_hats == null)
            {
                profile.hat_state.unlocked_hats = new List<HatUnlockData>();
            }

            if (string.IsNullOrEmpty(profile.hat_state.last_updated_at))
            {
                profile.hat_state.last_updated_at = ProfileFactory.UtcNowIso();
            }

            RefreshHatPassiveSummary(profile);

            if (profile.vault_state == null)
            {
                profile.vault_state = new VaultStateData();
            }

            profile.vault_state.treasure_progression_enabled = false;
            profile.vault_state.owned_treasure_count = 0;
            if (string.IsNullOrEmpty(profile.vault_state.status_note))
            {
                profile.vault_state.status_note = "Vault of Treasures shell only; Treasures are not implemented in Sprint 3.";
            }
        }

        public static bool ApplyMilestoneUnlocks(PlayerProfileData profile)
        {
            EnsureDefaults(profile);

            var changed = false;
            if (profile.strata_state != null &&
                profile.strata_state.loamwake != null &&
                profile.strata_state.loamwake.keeper_lw_001_defeated &&
                !HasHat(profile, FirstHatId))
            {
                profile.hat_state.unlocked_hats.Add(new HatUnlockData
                {
                    hat_id = FirstHatId,
                    unlocked = true,
                    unlocked_at = ProfileFactory.UtcNowIso(),
                    permanent_passive_active = true
                });

                if (string.IsNullOrEmpty(profile.hat_state.visible_hat_id))
                {
                    profile.hat_state.visible_hat_id = FirstHatId;
                }

                profile.vault_state.visible = true;
                profile.hat_state.last_updated_at = ProfileFactory.UtcNowIso();
                changed = true;
            }

            if (profile.strata_state != null &&
                profile.strata_state.loamwake != null &&
                profile.strata_state.loamwake.keeper_lw_001_defeated &&
                !profile.vault_state.visible)
            {
                profile.vault_state.visible = true;
                changed = true;
            }

            RefreshHatPassiveSummary(profile);
            return changed;
        }

        public static bool HasHat(PlayerProfileData profile, string hatId)
        {
            if (profile == null || profile.hat_state == null || profile.hat_state.unlocked_hats == null)
            {
                return false;
            }

            foreach (var hat in profile.hat_state.unlocked_hats)
            {
                if (hat != null && hat.unlocked && hat.hat_id == hatId)
                {
                    return true;
                }
            }

            return false;
        }

        public static FixtureInstanceData FindFirstFixture(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return null;
            }

            EnsureDefaults(profile);
            foreach (var fixture in profile.fixture_state.fixture_inventory)
            {
                if (fixture != null && fixture.fixture_id == FirstFixtureId)
                {
                    return fixture;
                }
            }

            return null;
        }

        public static bool IsFirstFixtureEquipped(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return false;
            }

            var fixture = FindFirstFixture(profile);
            return fixture != null && profile.fixture_state.equipped_fixture_instance_ids.Contains(fixture.instance_id);
        }

        public static int GetEquippedFixtureBonus(PlayerProfileData profile)
        {
            return IsFirstFixtureEquipped(profile) ? 2 : 0;
        }

        public static int GetHatExpeditionPowerBonus(PlayerProfileData profile)
        {
            return HasHat(profile, FirstHatId) ? 1 : 0;
        }

        public static int GetTotalExpeditionPowerBonus(PlayerProfileData profile)
        {
            return GetEquippedFixtureBonus(profile) + GetHatExpeditionPowerBonus(profile);
        }

        public static bool IsVaultShellOnly(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return !profile.vault_state.treasure_progression_enabled && profile.vault_state.owned_treasure_count == 0;
        }

        public static void RefreshHatPassiveSummary(PlayerProfileData profile)
        {
            if (profile == null || profile.hat_state == null)
            {
                return;
            }

            profile.hat_state.passive_summary = HasHat(profile, FirstHatId)
                ? FirstHatDisplayName + ": +1 expedition power permanent passive"
                : "No Hat passives active";
        }

        private static void EnforceFixtureCap(PlayerProfileData profile)
        {
            var cap = profile.account.fixture_cap;
            if (cap < 0)
            {
                profile.account.fixture_cap = 0;
                cap = 0;
            }

            while (profile.fixture_state.equipped_fixture_instance_ids.Count > cap)
            {
                profile.fixture_state.equipped_fixture_instance_ids.RemoveAt(profile.fixture_state.equipped_fixture_instance_ids.Count - 1);
            }
        }

        private static void RemoveInvalidEquippedIds(FixtureStateData fixtureState)
        {
            for (var i = fixtureState.equipped_fixture_instance_ids.Count - 1; i >= 0; i--)
            {
                var instanceId = fixtureState.equipped_fixture_instance_ids[i];
                if (!InventoryContains(fixtureState.fixture_inventory, instanceId))
                {
                    fixtureState.equipped_fixture_instance_ids.RemoveAt(i);
                }
            }
        }

        private static bool InventoryContains(IEnumerable<FixtureInstanceData> inventory, string instanceId)
        {
            if (string.IsNullOrEmpty(instanceId))
            {
                return false;
            }

            foreach (var fixture in inventory)
            {
                if (fixture != null && fixture.instance_id == instanceId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
