using System;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class ProfileService
    {
        private SaveManager saveManager;
        private BurrowProductionService burrowProductionService;
        private Func<DateTime> utcNowProvider;

        public event Action ProfileChanged;

        public PlayerProfileData Profile => saveManager != null ? saveManager.Profile : null;
        public string LastActionStatus { get; private set; } = "Ready";

        public void Initialize(SaveManager activeSaveManager, BurrowProductionService activeBurrowProductionService, Func<DateTime> activeUtcNowProvider)
        {
            saveManager = activeSaveManager;
            burrowProductionService = activeBurrowProductionService;
            utcNowProvider = activeUtcNowProvider ?? (() => DateTime.UtcNow);
            RaiseProfileChanged();
        }

        public bool ProcessBurrowProduction()
        {
            if (saveManager == null || burrowProductionService == null)
            {
                return false;
            }

            var changed = saveManager.MutateProfileIfChanged(
                profile => burrowProductionService.ProcessProduction(profile, utcNowProvider()),
                "processed burrow production");

            if (changed)
            {
                LastActionStatus = "Burrow output updated";
                RaiseProfileChanged();
            }

            return changed;
        }

        public bool GatherDewpond()
        {
            return GatherBuildingOutput("dewpond");
        }

        public bool GatherMushpatch()
        {
            return GatherBuildingOutput("mushpatch");
        }

        public bool ExpandBurrow()
        {
            if (saveManager == null || burrowProductionService == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    BurrowStateHelper.EnsureDefaults(profile);

                    var cost = burrowProductionService.GetNextExpandCost(profile);
                    if (cost < 0)
                    {
                        LastActionStatus = "Expand stubbed beyond Burrow level 3";
                        return false;
                    }

                    if (profile.wallet.mooncaps < cost)
                    {
                        LastActionStatus = "Expand blocked: need " + cost + " Mooncaps";
                        return false;
                    }

                    profile.wallet.mooncaps -= cost;
                    profile.burrow_state.burrow_level += 1;
                    profile.burrow_state.expand_count += 1;
                    BurrowStateHelper.EnsureDefaults(profile);

                    LastActionStatus = "Expanded the Burrow to level " + profile.burrow_state.burrow_level;
                    return true;
                },
                "expanded burrow");

            RaiseProfileChanged();
            return saved;
        }

        public void ForceSave()
        {
            if (saveManager == null)
            {
                return;
            }

            saveManager.SaveProfile("manual force save");
            RaiseProfileChanged();
        }

        public void ReloadSave()
        {
            if (saveManager == null)
            {
                return;
            }

            saveManager.ReloadProfile();
            ProcessBurrowProduction();
            LastActionStatus = "Reloaded save";
            RaiseProfileChanged();
        }

        public int GetNextExpandCost()
        {
            if (burrowProductionService == null)
            {
                return -1;
            }

            return burrowProductionService.GetNextExpandCost(Profile);
        }

        private bool GatherBuildingOutput(string buildingId)
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    BurrowStateHelper.EnsureDefaults(profile);

                    BurrowBuildingStateData building;
                    bool isMooncaps;
                    string label;

                    if (!TryGetBuilding(profile, buildingId, out building, out isMooncaps, out label))
                    {
                        LastActionStatus = "Unknown Burrow building";
                        return false;
                    }

                    if (!BurrowProductionService.CanGather(building))
                    {
                        LastActionStatus = label + " has nothing ready to gather";
                        return false;
                    }

                    var amount = building.stored_output;
                    building.stored_output = 0;

                    if (isMooncaps)
                    {
                        profile.wallet.mooncaps += amount;
                    }
                    else
                    {
                        profile.wallet.mushcaps += amount;
                    }

                    LastActionStatus = "Gathered " + amount + " " + (isMooncaps ? "Mooncaps" : "Mushcaps") + " from " + label;
                    return true;
                },
                "gathered " + buildingId + " output");

            RaiseProfileChanged();
            return saved;
        }

        private static bool TryGetBuilding(PlayerProfileData profile, string buildingId, out BurrowBuildingStateData building, out bool isMooncaps, out string label)
        {
            building = null;
            isMooncaps = false;
            label = "";

            if (profile == null || profile.burrow_state == null)
            {
                return false;
            }

            if (buildingId == "dewpond")
            {
                building = profile.burrow_state.dewpond;
                isMooncaps = true;
                label = "Dewpond";
                return true;
            }

            if (buildingId == "mushpatch")
            {
                building = profile.burrow_state.mushpatch;
                isMooncaps = false;
                label = "Mushpatch";
                return true;
            }

            return false;
        }

        private void RaiseProfileChanged()
        {
            var handler = ProfileChanged;
            if (handler != null)
            {
                handler.Invoke();
            }
        }
    }
}
