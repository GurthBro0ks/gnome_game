using System;

namespace GnomeGame.Data
{
    public static class ProfileFactory
    {
        public const int CurrentSaveVersion = 2;

        public static PlayerProfileData CreateDefault(string uid)
        {
            var now = UtcNowIso();

            return new PlayerProfileData
            {
                save_version = new SaveVersionData
                {
                    version = CurrentSaveVersion
                },
                account = new AccountData
                {
                    uid = string.IsNullOrEmpty(uid) ? CreateLocalUid() : uid,
                    display_name = "Local Gnome",
                    created_at = now,
                    last_login = now,
                    tutorial_state = "not_started"
                },
                wallet = new WalletData
                {
                    mooncaps = 0,
                    mushcaps = 0,
                    glowcaps = 0,
                    lucky_draws = 0,
                    treasure_tickets = 0,
                    echoes = 0,
                    echo_shards = 0,
                    elder_books = 0,
                    crack_coins = 0,
                    bronze_shovels = 0,
                    favor_marks = 0,
                    strata_seals = 0,
                    festival_marks = 0
                },
                burrow_state = new BurrowStateData
                {
                    burrow_level = 1,
                    expand_count = 0,
                    last_production_tick = now,
                    unlocked_rooms = new System.Collections.Generic.List<string>
                    {
                        "dewpond",
                        "mushpatch"
                    },
                    dewpond = new BurrowBuildingStateData
                    {
                        level = 1,
                        stored_output = 0,
                        storage_cap = 60
                    },
                    mushpatch = new BurrowBuildingStateData
                    {
                        level = 1,
                        stored_output = 0,
                        storage_cap = 60
                    },
                    rootmine = new RootmineStateData
                    {
                        level = 0,
                        unlocked = false,
                        status_note = "Locked until Burrow level 2"
                    }
                }
            };
        }

        public static string CreateLocalUid()
        {
            return "local-" + Guid.NewGuid().ToString("N");
        }

        public static string UtcNowIso()
        {
            return DateTime.UtcNow.ToString("o");
        }
    }
}
