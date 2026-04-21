using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public static class BurrowStateHelper
    {
        public static void EnsureDefaults(PlayerProfileData profile)
        {
            if (profile == null)
            {
                return;
            }

            if (profile.burrow_state == null)
            {
                profile.burrow_state = new BurrowStateData();
            }

            if (profile.burrow_state.unlocked_rooms == null)
            {
                profile.burrow_state.unlocked_rooms = new List<string>();
            }

            if (profile.burrow_state.dewpond == null)
            {
                profile.burrow_state.dewpond = new BurrowBuildingStateData();
            }

            if (profile.burrow_state.mushpatch == null)
            {
                profile.burrow_state.mushpatch = new BurrowBuildingStateData();
            }

            if (profile.burrow_state.rootmine == null)
            {
                profile.burrow_state.rootmine = new RootmineStateData();
            }

            if (profile.burrow_state.dewpond.level <= 0)
            {
                profile.burrow_state.dewpond.level = 1;
            }

            if (profile.burrow_state.mushpatch.level <= 0)
            {
                profile.burrow_state.mushpatch.level = 1;
            }

            profile.burrow_state.dewpond.storage_cap = GetStorageCap(profile.burrow_state.dewpond.level);
            profile.burrow_state.mushpatch.storage_cap = GetStorageCap(profile.burrow_state.mushpatch.level);
            profile.burrow_state.rootmine.material_storage_cap = GetRootmineMaterialStorageCap(profile.burrow_state.rootmine.level);

            if (string.IsNullOrEmpty(profile.burrow_state.last_production_tick))
            {
                profile.burrow_state.last_production_tick = ProfileFactory.UtcNowIso();
            }

            EnsureRoom(profile.burrow_state.unlocked_rooms, "dewpond");
            EnsureRoom(profile.burrow_state.unlocked_rooms, "mushpatch");

            if (profile.burrow_state.burrow_level >= 2)
            {
                profile.burrow_state.rootmine.unlocked = true;
                if (profile.burrow_state.rootmine.level <= 0)
                {
                    profile.burrow_state.rootmine.level = 1;
                }

                profile.burrow_state.rootmine.status_note = "Unlocked";
                profile.burrow_state.rootmine.material_storage_cap = GetRootmineMaterialStorageCap(profile.burrow_state.rootmine.level);
                EnsureRoom(profile.burrow_state.unlocked_rooms, "rootmine");
            }
            else
            {
                profile.burrow_state.rootmine.unlocked = false;
                profile.burrow_state.rootmine.level = 0;
                profile.burrow_state.rootmine.status_note = "Locked until Burrow level 2";
                profile.burrow_state.rootmine.stored_tangled_root_twine = 0;
                profile.burrow_state.rootmine.stored_crumbled_ore_chunk = 0;
                profile.burrow_state.rootmine.material_storage_cap = GetRootmineMaterialStorageCap(0);
            }
        }

        public static int GetStorageCap(int buildingLevel)
        {
            // TODO: Replace with balance data when Burrow upgrade tuning moves out of prototype constants.
            return 60 + (buildingLevel - 1) * 30;
        }

        public static int GetRootmineMaterialStorageCap(int rootmineLevel)
        {
            if (rootmineLevel <= 0)
            {
                return 0;
            }

            return 30 + (rootmineLevel - 1) * 15;
        }

        private static void EnsureRoom(ICollection<string> rooms, string roomId)
        {
            if (!rooms.Contains(roomId))
            {
                rooms.Add(roomId);
            }
        }
    }
}
