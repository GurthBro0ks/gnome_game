using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class LoamwakeExplorationService
    {
        public const string StratumId = "loamwake";
        public const string KeeperId = "keeper_lw_001";
        public const string SafeRouteId = "safe_route";
        public const string RiskyRouteId = "risky_route";

        public sealed class ZoneDefinition
        {
            public string zone_id;
            public string display_name;
            public int difficulty;
            public int zone_order;
        }

        private static readonly ZoneDefinition[] ZoneDefinitions =
        {
            new ZoneDefinition
            {
                zone_id = "zone_lw_001_rootvine_shelf",
                display_name = "Rootvine Shelf",
                difficulty = 8,
                zone_order = 1
            },
            new ZoneDefinition
            {
                zone_id = "zone_lw_002_mudpipe_hollow",
                display_name = "Mudpipe Hollow",
                difficulty = 15,
                zone_order = 2
            },
            new ZoneDefinition
            {
                zone_id = "zone_lw_003_glowroot_passage",
                display_name = "Glowroot Passage",
                difficulty = 22,
                zone_order = 3
            }
        };

        public IEnumerable<ZoneDefinition> GetZoneDefinitions()
        {
            return ZoneDefinitions;
        }

        public bool IsStratumSelectable(PlayerProfileData profile, string stratumId)
        {
            ExplorationStateHelper.EnsureDefaults(profile);
            return profile.strata_state.unlocked_strata_ids.Contains(stratumId);
        }

        public bool SetCurrentStratum(PlayerProfileData profile, string stratumId)
        {
            ExplorationStateHelper.EnsureDefaults(profile);
            if (!IsStratumSelectable(profile, stratumId))
            {
                return false;
            }

            if (profile.strata_state.current_stratum_id == stratumId)
            {
                return false;
            }

            profile.strata_state.current_stratum_id = stratumId;
            return true;
        }

        public string GetZoneDisplayName(string zoneId)
        {
            var definition = FindZone(zoneId);
            return definition != null ? definition.display_name : zoneId;
        }

        public int GetRouteCost(string routeId)
        {
            return routeId == RiskyRouteId ? 2 : 1;
        }

        public int GetRouteBonus(string routeId)
        {
            return routeId == SafeRouteId ? 3 : 0;
        }

        public int GetZoneClearBonus(PlayerProfileData profile)
        {
            ExplorationStateHelper.EnsureDefaults(profile);
            var loamwake = profile.strata_state.loamwake;
            var firstClearCount = 0;

            if (loamwake.zone_lw_001_rootvine_shelf.first_clear)
            {
                firstClearCount += 1;
            }

            if (loamwake.zone_lw_002_mudpipe_hollow.first_clear)
            {
                firstClearCount += 1;
            }

            if (loamwake.zone_lw_003_glowroot_passage.first_clear)
            {
                firstClearCount += 1;
            }

            return firstClearCount * 5;
        }

        public int GetExpeditionPower(PlayerProfileData profile, string routeId)
        {
            ExplorationStateHelper.EnsureDefaults(profile);
            return (profile.burrow_state != null ? profile.burrow_state.burrow_level : 1) * 10 +
                GetZoneClearBonus(profile) +
                GetRouteBonus(routeId);
        }

        public bool ExploreZone(PlayerProfileData profile, string zoneId, string routeId, out ExplorationResultData result, out string status)
        {
            result = null;
            status = "";

            ExplorationStateHelper.EnsureDefaults(profile);

            if (!IsStratumSelectable(profile, StratumId))
            {
                status = "Loamwake is not selectable";
                return false;
            }

            var zone = GetZoneProgress(profile, zoneId);
            var definition = FindZone(zoneId);
            if (zone == null || definition == null)
            {
                status = "Unknown Loamwake zone";
                return false;
            }

            if (!zone.unlocked)
            {
                status = definition.display_name + " is locked";
                return false;
            }

            var cost = GetRouteCost(routeId);
            if (profile.wallet.mushcaps < cost)
            {
                status = "Need " + cost + " Mushcaps";
                return false;
            }

            profile.wallet.mushcaps -= cost;
            SetCurrentStratum(profile, StratumId);

            var expeditionPower = GetExpeditionPower(profile, routeId);
            var success = expeditionPower >= definition.difficulty;

            result = BuildZoneResult(definition, routeId, success);
            ApplyRewards(profile, result);

            if (success)
            {
                zone.clear_count += 1;
                if (!zone.first_clear)
                {
                    zone.first_clear = true;
                    UnlockNextZone(profile, zoneId);
                }
            }

            StoreResult(profile, result);
            status = definition.display_name + " " + (success ? "cleared" : "failed");
            return true;
        }

        public bool ChallengeKeeper(PlayerProfileData profile, out ExplorationResultData result, out string status)
        {
            result = null;
            status = "";

            ExplorationStateHelper.EnsureDefaults(profile);

            var loamwake = profile.strata_state.loamwake;
            if (!loamwake.keeper_lw_001_unlocked)
            {
                status = "Keeper is locked";
                return false;
            }

            if (loamwake.keeper_lw_001_defeated)
            {
                status = "Keeper already defeated";
                return false;
            }

            const int keeperCost = 2;
            if (profile.wallet.mushcaps < keeperCost)
            {
                status = "Need 2 Mushcaps";
                return false;
            }

            profile.wallet.mushcaps -= keeperCost;
            SetCurrentStratum(profile, StratumId);

            var keeperDifficulty = 28;
            var expeditionPower = (profile.burrow_state != null ? profile.burrow_state.burrow_level : 1) * 10 +
                GetZoneClearBonus(profile);
            var success = expeditionPower >= keeperDifficulty;

            result = BuildKeeperResult(success);
            ApplyRewards(profile, result);

            if (success)
            {
                loamwake.keeper_lw_001_defeated = true;
            }

            StoreResult(profile, result);
            status = success ? "Keeper defeated" : "Keeper resisted the clash";
            return true;
        }

        private static ExplorationResultData BuildZoneResult(ZoneDefinition definition, string routeId, bool success)
        {
            var result = new ExplorationResultData
            {
                last_zone_id = definition.zone_id,
                route_id = routeId,
                result = success ? "success" : "fail",
                keeper_encounter = false,
                material_rewards = new List<MaterialRewardData>()
            };

            if (success)
            {
                switch (definition.zone_id)
                {
                    case "zone_lw_001_rootvine_shelf":
                        result.mooncaps = routeId == RiskyRouteId ? 16 : 10;
                        result.mushcaps = 0;
                        result.material_rewards.Add(new MaterialRewardData
                        {
                            material_id = "tangled_root_twine",
                            amount = routeId == RiskyRouteId ? 2 : 1
                        });
                        break;
                    case "zone_lw_002_mudpipe_hollow":
                        result.mooncaps = routeId == RiskyRouteId ? 24 : 16;
                        result.mushcaps = routeId == RiskyRouteId ? 1 : 0;
                        result.material_rewards.Add(new MaterialRewardData
                        {
                            material_id = "crumbled_ore_chunk",
                            amount = routeId == RiskyRouteId ? 2 : 1
                        });
                        break;
                    case "zone_lw_003_glowroot_passage":
                        result.mooncaps = routeId == RiskyRouteId ? 36 : 24;
                        result.mushcaps = routeId == RiskyRouteId ? 1 : 0;
                        result.material_rewards.Add(new MaterialRewardData
                        {
                            material_id = "dull_glow_shard",
                            amount = routeId == RiskyRouteId ? 2 : 1
                        });
                        break;
                }
            }
            else
            {
                result.mooncaps = routeId == RiskyRouteId ? 3 : 2;
                result.mushcaps = 0;
            }

            return result;
        }

        private static ExplorationResultData BuildKeeperResult(bool success)
        {
            var result = new ExplorationResultData
            {
                last_zone_id = "zone_lw_003_glowroot_passage",
                route_id = "keeper_clash",
                result = success ? "success" : "fail",
                keeper_encounter = true,
                material_rewards = new List<MaterialRewardData>()
            };

            if (success)
            {
                result.mooncaps = 60;
                result.mushcaps = 1;
                result.material_rewards.Add(new MaterialRewardData
                {
                    material_id = "dull_glow_shard",
                    amount = 3
                });
            }
            else
            {
                result.mooncaps = 5;
            }

            return result;
        }

        private static void ApplyRewards(PlayerProfileData profile, ExplorationResultData result)
        {
            if (profile == null || result == null)
            {
                return;
            }

            profile.wallet.mooncaps += result.mooncaps;
            profile.wallet.mushcaps += result.mushcaps;

            foreach (var reward in result.material_rewards)
            {
                switch (reward.material_id)
                {
                    case "tangled_root_twine":
                        profile.wallet.loamwake_materials.tangled_root_twine += reward.amount;
                        break;
                    case "crumbled_ore_chunk":
                        profile.wallet.loamwake_materials.crumbled_ore_chunk += reward.amount;
                        break;
                    case "dull_glow_shard":
                        profile.wallet.loamwake_materials.dull_glow_shard += reward.amount;
                        break;
                }
            }
        }

        private static void StoreResult(PlayerProfileData profile, ExplorationResultData result)
        {
            profile.strata_state.loamwake.last_exploration_result = CloneResult(result);
            profile.strata_state.loamwake.field_returns = CloneResult(result);
        }

        private static ExplorationResultData CloneResult(ExplorationResultData source)
        {
            var clone = new ExplorationResultData
            {
                last_zone_id = source.last_zone_id,
                route_id = source.route_id,
                result = source.result,
                keeper_encounter = source.keeper_encounter,
                mooncaps = source.mooncaps,
                mushcaps = source.mushcaps,
                material_rewards = new List<MaterialRewardData>()
            };

            foreach (var reward in source.material_rewards)
            {
                clone.material_rewards.Add(new MaterialRewardData
                {
                    material_id = reward.material_id,
                    amount = reward.amount
                });
            }

            return clone;
        }

        private static void UnlockNextZone(PlayerProfileData profile, string clearedZoneId)
        {
            var loamwake = profile.strata_state.loamwake;

            if (clearedZoneId == "zone_lw_001_rootvine_shelf")
            {
                loamwake.zone_lw_002_mudpipe_hollow.unlocked = true;
                return;
            }

            if (clearedZoneId == "zone_lw_002_mudpipe_hollow")
            {
                loamwake.zone_lw_003_glowroot_passage.unlocked = true;
                loamwake.keeper_lw_001_unlocked = true;
            }
        }

        private static ZoneProgressData GetZoneProgress(PlayerProfileData profile, string zoneId)
        {
            var loamwake = profile.strata_state.loamwake;
            switch (zoneId)
            {
                case "zone_lw_001_rootvine_shelf":
                    return loamwake.zone_lw_001_rootvine_shelf;
                case "zone_lw_002_mudpipe_hollow":
                    return loamwake.zone_lw_002_mudpipe_hollow;
                case "zone_lw_003_glowroot_passage":
                    return loamwake.zone_lw_003_glowroot_passage;
                default:
                    return null;
            }
        }

        private static ZoneDefinition FindZone(string zoneId)
        {
            foreach (var zone in ZoneDefinitions)
            {
                if (zone.zone_id == zoneId)
                {
                    return zone;
                }
            }

            return null;
        }
    }
}
