using System;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class ProfileService
    {
        private SaveManager saveManager;
        private BurrowProductionService burrowProductionService;
        private LoamwakeExplorationService loamwakeExplorationService;
        private FixtureService fixtureService = new FixtureService();
        private SocialProgressService socialProgressService = new SocialProgressService();
        private Func<DateTime> utcNowProvider;

        public event Action ProfileChanged;

        public PlayerProfileData Profile => saveManager != null ? saveManager.Profile : null;
        public string LastActionStatus { get; private set; } = "Ready";

        public void Initialize(
            SaveManager activeSaveManager,
            BurrowProductionService activeBurrowProductionService,
            LoamwakeExplorationService activeLoamwakeExplorationService,
            Func<DateTime> activeUtcNowProvider)
        {
            saveManager = activeSaveManager;
            burrowProductionService = activeBurrowProductionService;
            loamwakeExplorationService = activeLoamwakeExplorationService;
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

        public bool GatherRootmine()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    BurrowStateHelper.EnsureDefaults(profile);
                    ExplorationStateHelper.EnsureDefaults(profile);
                    FixtureStateHelper.EnsureDefaults(profile);

                    var rootmine = profile.burrow_state.rootmine;
                    if (!rootmine.unlocked)
                    {
                        LastActionStatus = "Rootmine locked until Burrow level 2";
                        return false;
                    }

                    if (!BurrowProductionService.CanGather(rootmine))
                    {
                        LastActionStatus = "Rootmine has no materials ready";
                        return false;
                    }

                    var twine = rootmine.stored_tangled_root_twine;
                    var ore = rootmine.stored_crumbled_ore_chunk;
                    rootmine.stored_tangled_root_twine = 0;
                    rootmine.stored_crumbled_ore_chunk = 0;
                    profile.wallet.loamwake_materials.tangled_root_twine += twine;
                    profile.wallet.loamwake_materials.crumbled_ore_chunk += ore;

                    LastActionStatus = "Gathered Rootmine materials: +" + twine + " Twine, +" + ore + " Ore";
                    return true;
                },
                "gathered rootmine materials");

            RaiseProfileChanged();
            return saved;
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

        public bool EnterLoamwake()
        {
            if (saveManager == null || loamwakeExplorationService == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile => loamwakeExplorationService.SetCurrentStratum(profile, LoamwakeExplorationService.StratumId),
                "entered loamwake");

            if (saved)
            {
                LastActionStatus = "Entered Loamwake";
            }
            else if (Profile != null && loamwakeExplorationService.IsStratumSelectable(Profile, LoamwakeExplorationService.StratumId))
            {
                LastActionStatus = "Loamwake ready";
            }

            RaiseProfileChanged();
            return saved;
        }

        public bool ReturnToBurrow()
        {
            if (saveManager == null || Profile == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    ExplorationStateHelper.EnsureDefaults(profile);
                    if (string.IsNullOrEmpty(profile.strata_state.current_stratum_id))
                    {
                        return false;
                    }

                    profile.strata_state.current_stratum_id = "";
                    return true;
                },
                "returned to burrow");

            LastActionStatus = "Returned to the Burrow";
            RaiseProfileChanged();
            return saved;
        }

        public bool ExploreLoamwakeZone(string zoneId, string routeId)
        {
            if (saveManager == null || loamwakeExplorationService == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    ExplorationResultData result;
                    string status;
                    var changed = loamwakeExplorationService.ExploreZone(profile, zoneId, routeId, out result, out status);
                    if (changed)
                    {
                        socialProgressService.RecordLoamwakeExplore(profile);
                        SocialProgressService.EnsureDefaults(profile);
                    }

                    LastActionStatus = status;
                    return changed;
                },
                "explored loamwake zone");

            RaiseProfileChanged();
            return saved;
        }

        public bool ChallengeLoamwakeKeeper()
        {
            if (saveManager == null || loamwakeExplorationService == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    ExplorationResultData result;
                    string status;
                    var changed = loamwakeExplorationService.ChallengeKeeper(profile, out result, out status);
                    if (changed && FixtureStateHelper.ApplyMilestoneUnlocks(profile))
                    {
                        status += "; Loamwake Dirt Cap unlocked";
                    }

                    LastActionStatus = status;
                    return changed;
                },
                "challenged loamwake keeper");

            RaiseProfileChanged();
            return saved;
        }

        public bool UnlockFirstFixtureCap()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    string status;
                    var changed = fixtureService.UnlockFirstFixtureCap(profile, out status);
                    LastActionStatus = status;
                    return changed;
                },
                "unlocked first fixture cap");

            RaiseProfileChanged();
            return saved;
        }

        public bool CraftFirstFixture()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    string status;
                    var changed = fixtureService.CraftFirstFixture(profile, out status);
                    if (changed)
                    {
                        socialProgressService.RecordFixtureProgress(profile);
                    }

                    LastActionStatus = status;
                    return changed;
                },
                "crafted first fixture");

            RaiseProfileChanged();
            return saved;
        }

        public bool EquipFirstFixture()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    string status;
                    var changed = fixtureService.EquipFirstFixture(profile, out status);
                    if (changed)
                    {
                        socialProgressService.RecordFixtureProgress(profile);
                    }

                    LastActionStatus = status;
                    return changed;
                },
                "equipped first fixture");

            RaiseProfileChanged();
            return saved;
        }

        public bool UnequipFirstFixture()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    string status;
                    var changed = fixtureService.UnequipFirstFixture(profile, out status);
                    LastActionStatus = status;
                    return changed;
                },
                "unequipped first fixture");

            RaiseProfileChanged();
            return saved;
        }

        public bool SetFirstHatVisible()
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    string status;
                    var changed = fixtureService.SetFirstHatVisible(profile, out status);
                    LastActionStatus = status;
                    return changed;
                },
                "set first hat visible");

            RaiseProfileChanged();
            return saved;
        }

        public string BuildPowerSummary()
        {
            return fixtureService.BuildPowerSummary(Profile);
        }

        public bool ReadUtilityBurrowPost()
        {
            return MutateSocialProgress(socialProgressService.ReadUtilityPost, "read utility burrow post");
        }

        public bool ReadGretaIntroPost()
        {
            return MutateSocialProgress(socialProgressService.ReadGretaIntroPost, "read greta intro burrow post");
        }

        public bool CompleteGretaFirstFollowup()
        {
            return MutateSocialProgress(socialProgressService.CompleteGretaFirstFollowup, "completed greta first follow-up");
        }

        public bool RevealRootrailStation()
        {
            return MutateSocialProgress(socialProgressService.RevealRootrail, "revealed rootrail station");
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

                    if (buildingId == "dewpond")
                    {
                        socialProgressService.RecordDewpondGather(profile);
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

        public bool IsStratumSelectable(string stratumId)
        {
            return loamwakeExplorationService != null &&
                Profile != null &&
                loamwakeExplorationService.IsStratumSelectable(Profile, stratumId);
        }

        private delegate bool SocialMutation(PlayerProfileData profile, out string status);

        private bool MutateSocialProgress(SocialMutation mutation, string reason)
        {
            if (saveManager == null)
            {
                return false;
            }

            var saved = saveManager.MutateProfileIfChanged(
                profile =>
                {
                    SocialProgressService.EnsureDefaults(profile);
                    string status;
                    var changed = mutation(profile, out status);
                    LastActionStatus = status;
                    return changed;
                },
                reason);

            RaiseProfileChanged();
            return saved;
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
