using System;
using System.Globalization;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class BurrowProductionService
    {
        private const int DewpondSecondsPerUnitLevel1 = 3;
        private const int MushpatchSecondsPerUnitLevel1 = 10;
        private const int RootmineTwineSecondsPerUnitLevel1 = 20;
        private const int RootmineOreSecondsPerUnitLevel1 = 40;

        public bool ProcessProduction(PlayerProfileData profile, DateTime nowUtc)
        {
            if (profile == null)
            {
                return false;
            }

            BurrowStateHelper.EnsureDefaults(profile);

            var burrow = profile.burrow_state;
            var lastTick = ParseOrNow(burrow.last_production_tick, nowUtc);
            if (nowUtc <= lastTick)
            {
                return false;
            }

            var elapsedSeconds = Math.Max(0, (int)(nowUtc - lastTick).TotalSeconds);
            var dewpondSecondsPerUnit = GetDewpondSecondsPerUnit(burrow.dewpond.level);
            var mushpatchSecondsPerUnit = GetMushpatchSecondsPerUnit(burrow.mushpatch.level);
            var rootmineTwineSecondsPerUnit = GetRootmineTwineSecondsPerUnit(burrow.rootmine.level);
            var rootmineOreSecondsPerUnit = GetRootmineOreSecondsPerUnit(burrow.rootmine.level);
            var shouldAdvanceTick = elapsedSeconds >= dewpondSecondsPerUnit ||
                elapsedSeconds >= mushpatchSecondsPerUnit ||
                (burrow.rootmine.unlocked &&
                    (elapsedSeconds >= rootmineTwineSecondsPerUnit || elapsedSeconds >= rootmineOreSecondsPerUnit));
            var changed = false;

            changed |= Accumulate(burrow.dewpond, elapsedSeconds, dewpondSecondsPerUnit);
            changed |= Accumulate(burrow.mushpatch, elapsedSeconds, mushpatchSecondsPerUnit);
            changed |= AccumulateRootmine(burrow.rootmine, elapsedSeconds, rootmineTwineSecondsPerUnit, rootmineOreSecondsPerUnit);

            if (!shouldAdvanceTick && !changed)
            {
                return false;
            }

            burrow.last_production_tick = nowUtc.ToString("o");

            BurrowStateHelper.EnsureDefaults(profile);
            return true;
        }

        public int GetNextExpandCost(PlayerProfileData profile)
        {
            if (profile == null || profile.burrow_state == null)
            {
                return -1;
            }

            switch (profile.burrow_state.burrow_level)
            {
                case 1:
                    return 50;
                case 2:
                    return 150;
                default:
                    return -1;
            }
        }

        public static bool CanGather(BurrowBuildingStateData building)
        {
            return building != null && building.stored_output > 0;
        }

        public static bool CanGather(RootmineStateData rootmine)
        {
            return rootmine != null &&
                rootmine.unlocked &&
                (rootmine.stored_tangled_root_twine > 0 || rootmine.stored_crumbled_ore_chunk > 0);
        }

        private static bool Accumulate(BurrowBuildingStateData building, int elapsedSeconds, int secondsPerUnit)
        {
            if (building == null || elapsedSeconds <= 0 || secondsPerUnit <= 0)
            {
                return false;
            }

            building.storage_cap = BurrowStateHelper.GetStorageCap(building.level);
            var produced = elapsedSeconds / secondsPerUnit;
            if (produced <= 0)
            {
                return false;
            }

            var prior = building.stored_output;
            building.stored_output = Math.Min(building.storage_cap, building.stored_output + produced);
            return building.stored_output != prior;
        }

        private static bool AccumulateRootmine(RootmineStateData rootmine, int elapsedSeconds, int twineSecondsPerUnit, int oreSecondsPerUnit)
        {
            if (rootmine == null || !rootmine.unlocked || elapsedSeconds <= 0)
            {
                return false;
            }

            rootmine.material_storage_cap = BurrowStateHelper.GetRootmineMaterialStorageCap(rootmine.level);
            if (rootmine.material_storage_cap <= 0)
            {
                return false;
            }

            var priorTwine = rootmine.stored_tangled_root_twine;
            var priorOre = rootmine.stored_crumbled_ore_chunk;

            var producedTwine = twineSecondsPerUnit > 0 ? elapsedSeconds / twineSecondsPerUnit : 0;
            var producedOre = oreSecondsPerUnit > 0 ? elapsedSeconds / oreSecondsPerUnit : 0;

            rootmine.stored_tangled_root_twine = Math.Min(rootmine.material_storage_cap, rootmine.stored_tangled_root_twine + producedTwine);
            rootmine.stored_crumbled_ore_chunk = Math.Min(rootmine.material_storage_cap, rootmine.stored_crumbled_ore_chunk + producedOre);

            return rootmine.stored_tangled_root_twine != priorTwine ||
                rootmine.stored_crumbled_ore_chunk != priorOre;
        }

        private static int GetDewpondSecondsPerUnit(int level)
        {
            // TODO: Move prototype timing out of code once Burrow balance data exists.
            return DewpondSecondsPerUnitLevel1;
        }

        private static int GetMushpatchSecondsPerUnit(int level)
        {
            // TODO: Move prototype timing out of code once Burrow balance data exists.
            return MushpatchSecondsPerUnitLevel1;
        }

        private static int GetRootmineTwineSecondsPerUnit(int level)
        {
            return RootmineTwineSecondsPerUnitLevel1;
        }

        private static int GetRootmineOreSecondsPerUnit(int level)
        {
            return RootmineOreSecondsPerUnitLevel1;
        }

        private static DateTime ParseOrNow(string value, DateTime fallback)
        {
            DateTime parsed;
            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out parsed))
            {
                return parsed.ToUniversalTime();
            }

            return fallback;
        }
    }
}
