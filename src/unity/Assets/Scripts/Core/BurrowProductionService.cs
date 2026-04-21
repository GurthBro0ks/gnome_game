using System;
using System.Globalization;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class BurrowProductionService
    {
        private const int DewpondSecondsPerUnitLevel1 = 3;
        private const int MushpatchSecondsPerUnitLevel1 = 10;

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
            var shouldAdvanceTick = elapsedSeconds >= dewpondSecondsPerUnit || elapsedSeconds >= mushpatchSecondsPerUnit;
            var changed = false;

            changed |= Accumulate(burrow.dewpond, elapsedSeconds, dewpondSecondsPerUnit);
            changed |= Accumulate(burrow.mushpatch, elapsedSeconds, mushpatchSecondsPerUnit);

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
