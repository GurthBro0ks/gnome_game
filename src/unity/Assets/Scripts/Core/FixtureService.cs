using System;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class FixtureService
    {
        public const int FirstFixtureMooncapCost = 80;
        public const int FirstFixtureTwineCost = 6;

        public bool UnlockFirstFixtureCap(PlayerProfileData profile, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            if (profile.account.fixture_cap >= 1)
            {
                status = "Fixture cap already unlocked";
                return false;
            }

            profile.account.fixture_cap = 1;
            profile.fixture_state.last_updated_at = ProfileFactory.UtcNowIso();
            status = "Fixture cap increased to 1";
            return true;
        }

        public bool CraftFirstFixture(PlayerProfileData profile, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            if (profile.account.fixture_cap < 1)
            {
                status = "Fixture cap is 0; unlock the first carry space";
                return false;
            }

            if (HasCraftedRecipe(profile, FixtureStateHelper.FirstFixtureRecipeId) ||
                FixtureStateHelper.FindFirstFixture(profile) != null)
            {
                status = "Root-Bitten Shovel Strap already crafted";
                return false;
            }

            if (profile.wallet.mooncaps < FirstFixtureMooncapCost)
            {
                status = "Need " + FirstFixtureMooncapCost + " Mooncaps";
                return false;
            }

            if (profile.wallet.loamwake_materials.tangled_root_twine < FirstFixtureTwineCost)
            {
                status = "Need " + FirstFixtureTwineCost + " Tangled Root Twine";
                return false;
            }

            profile.wallet.mooncaps -= FirstFixtureMooncapCost;
            profile.wallet.loamwake_materials.tangled_root_twine -= FirstFixtureTwineCost;
            profile.fixture_state.fixture_inventory.Add(new FixtureInstanceData
            {
                instance_id = "fixinst-" + Guid.NewGuid().ToString("N"),
                fixture_id = FixtureStateHelper.FirstFixtureId,
                upgrade_level = 0,
                acquired_at = ProfileFactory.UtcNowIso()
            });
            profile.fixture_state.crafted_recipe_ids.Add(FixtureStateHelper.FirstFixtureRecipeId);
            profile.fixture_state.last_updated_at = ProfileFactory.UtcNowIso();

            status = "Crafted Root-Bitten Shovel Strap";
            return true;
        }

        public bool EquipFirstFixture(PlayerProfileData profile, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            var fixture = FixtureStateHelper.FindFirstFixture(profile);
            if (fixture == null)
            {
                status = "Craft Root-Bitten Shovel Strap first";
                return false;
            }

            return EquipFixtureInstance(profile, fixture.instance_id, out status);
        }

        public bool EquipFixtureInstance(PlayerProfileData profile, string instanceId, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            var fixture = FindFixture(profile, instanceId);
            if (fixture == null)
            {
                status = "Fixture not owned";
                return false;
            }

            if (profile.fixture_state.equipped_fixture_instance_ids.Contains(fixture.instance_id))
            {
                status = GetFixtureDisplayName(fixture.fixture_id) + " already equipped";
                return false;
            }

            if (profile.fixture_state.equipped_fixture_instance_ids.Count >= profile.account.fixture_cap)
            {
                status = "Fixture cap full; unequip first";
                return false;
            }

            profile.fixture_state.equipped_fixture_instance_ids.Add(fixture.instance_id);
            profile.fixture_state.last_updated_at = ProfileFactory.UtcNowIso();
            status = "Equipped " + GetFixtureDisplayName(fixture.fixture_id) + GetEquipBonusSuffix(fixture.fixture_id);
            return true;
        }

        public bool UnequipFirstFixture(PlayerProfileData profile, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            var fixture = FixtureStateHelper.FindFirstFixture(profile);
            if (fixture == null || !profile.fixture_state.equipped_fixture_instance_ids.Contains(fixture.instance_id))
            {
                status = "Root-Bitten Shovel Strap is not equipped";
                return false;
            }

            profile.fixture_state.equipped_fixture_instance_ids.Remove(fixture.instance_id);
            profile.fixture_state.last_updated_at = ProfileFactory.UtcNowIso();
            status = "Unequipped Root-Bitten Shovel Strap";
            return true;
        }

        public bool SetFirstHatVisible(PlayerProfileData profile, out string status)
        {
            FixtureStateHelper.EnsureDefaults(profile);

            if (!FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId))
            {
                status = "Loamwake Dirt Cap is locked";
                return false;
            }

            if (profile.hat_state.visible_hat_id == FixtureStateHelper.FirstHatId)
            {
                status = "Loamwake Dirt Cap already visible";
                return false;
            }

            profile.hat_state.visible_hat_id = FixtureStateHelper.FirstHatId;
            profile.hat_state.last_updated_at = ProfileFactory.UtcNowIso();
            FixtureStateHelper.RefreshHatPassiveSummary(profile);
            status = "Loamwake Dirt Cap set as visible Hat";
            return true;
        }

        public string BuildPowerSummary(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return "Sprint 3 power summary\nNo profile loaded";
            }

            FixtureStateHelper.EnsureDefaults(profile);
            return "Sprint 3 power summary\n" +
                "Fixture bonus: +" + FixtureStateHelper.GetEquippedFixtureBonus(profile) + " expedition power\n" +
                "Hat passive: +" + FixtureStateHelper.GetHatExpeditionPowerBonus(profile) + " expedition power\n" +
                "Total Sprint 3 bonus: +" + FixtureStateHelper.GetTotalExpeditionPowerBonus(profile);
        }

        private static bool HasCraftedRecipe(PlayerProfileData profile, string recipeId)
        {
            return profile.fixture_state.crafted_recipe_ids != null &&
                profile.fixture_state.crafted_recipe_ids.Contains(recipeId);
        }

        private static FixtureInstanceData FindFixture(PlayerProfileData profile, string instanceId)
        {
            if (profile == null || profile.fixture_state == null || profile.fixture_state.fixture_inventory == null)
            {
                return null;
            }

            foreach (var fixture in profile.fixture_state.fixture_inventory)
            {
                if (fixture != null && fixture.instance_id == instanceId)
                {
                    return fixture;
                }
            }

            return null;
        }

        private static string GetFixtureDisplayName(string fixtureId)
        {
            return fixtureId == FixtureStateHelper.FirstFixtureId ? "Root-Bitten Shovel Strap" : fixtureId;
        }

        private static string GetEquipBonusSuffix(string fixtureId)
        {
            return fixtureId == FixtureStateHelper.FirstFixtureId ? " (+2 expedition power)" : "";
        }
    }
}
