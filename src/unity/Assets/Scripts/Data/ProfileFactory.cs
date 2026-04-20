using System;

namespace GnomeGame.Data
{
    public static class ProfileFactory
    {
        public const int CurrentSaveVersion = 1;

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
                    dewpond_level = 1,
                    mushpatch_level = 1,
                    rootmine_level = 0,
                    tickworks_level = 0,
                    expand_count = 0
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
