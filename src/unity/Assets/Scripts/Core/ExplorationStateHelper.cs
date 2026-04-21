using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public static class ExplorationStateHelper
    {
        public static void EnsureDefaults(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return;
            }

            if (profile.wallet == null)
            {
                profile.wallet = new WalletData();
            }

            if (profile.wallet.loamwake_materials == null)
            {
                profile.wallet.loamwake_materials = new LoamwakeMaterialsData();
            }

            if (profile.strata_state == null)
            {
                profile.strata_state = new StrataStateData();
            }

            if (profile.strata_state.unlocked_strata_ids == null)
            {
                profile.strata_state.unlocked_strata_ids = new List<string>();
            }

            EnsureStratumUnlocked(profile.strata_state.unlocked_strata_ids, "loamwake");

            if (profile.strata_state.loamwake == null)
            {
                profile.strata_state.loamwake = new LoamwakeProgressData();
            }

            EnsureZone(ref profile.strata_state.loamwake.zone_lw_001_rootvine_shelf, "zone_lw_001_rootvine_shelf", true);
            EnsureZone(ref profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow, "zone_lw_002_mudpipe_hollow", false);
            EnsureZone(ref profile.strata_state.loamwake.zone_lw_003_glowroot_passage, "zone_lw_003_glowroot_passage", false);

            if (profile.strata_state.loamwake.last_exploration_result == null)
            {
                profile.strata_state.loamwake.last_exploration_result = new ExplorationResultData();
            }

            if (profile.strata_state.loamwake.field_returns == null)
            {
                profile.strata_state.loamwake.field_returns = new ExplorationResultData();
            }

            var loamwake = profile.strata_state.loamwake;
            if (loamwake.zone_lw_001_rootvine_shelf.first_clear)
            {
                loamwake.zone_lw_002_mudpipe_hollow.unlocked = true;
            }

            if (loamwake.zone_lw_002_mudpipe_hollow.first_clear)
            {
                loamwake.zone_lw_003_glowroot_passage.unlocked = true;
            }

            loamwake.keeper_lw_001_unlocked = loamwake.zone_lw_003_glowroot_passage.unlocked || loamwake.keeper_lw_001_unlocked;
        }

        private static void EnsureStratumUnlocked(ICollection<string> unlockedStrataIds, string stratumId)
        {
            if (!unlockedStrataIds.Contains(stratumId))
            {
                unlockedStrataIds.Add(stratumId);
            }
        }

        private static void EnsureZone(ref ZoneProgressData zone, string zoneId, bool unlockedByDefault)
        {
            if (zone == null)
            {
                zone = new ZoneProgressData();
            }

            if (string.IsNullOrEmpty(zone.zone_id))
            {
                zone.zone_id = zoneId;
            }

            if (!zone.first_clear && zone.clear_count < 0)
            {
                zone.clear_count = 0;
            }

            if (zone.zone_id == zoneId && zone.clear_count < 0)
            {
                zone.clear_count = 0;
            }

            if (unlockedByDefault)
            {
                zone.unlocked = true;
            }
        }
    }
}
