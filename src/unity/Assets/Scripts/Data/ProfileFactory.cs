using System;

namespace GnomeGame.Data
{
    public static class ProfileFactory
    {
        public const int CurrentSaveVersion = 3;

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
                    festival_marks = 0,
                    loamwake_materials = new LoamwakeMaterialsData
                    {
                        tangled_root_twine = 0,
                        crumbled_ore_chunk = 0,
                        dull_glow_shard = 0
                    }
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
                },
                strata_state = new StrataStateData
                {
                    current_stratum_id = "",
                    unlocked_strata_ids = new System.Collections.Generic.List<string>
                    {
                        "loamwake"
                    },
                    loamwake = new LoamwakeProgressData
                    {
                        zone_lw_001_rootvine_shelf = new ZoneProgressData
                        {
                            zone_id = "zone_lw_001_rootvine_shelf",
                            unlocked = true,
                            first_clear = false,
                            clear_count = 0
                        },
                        zone_lw_002_mudpipe_hollow = new ZoneProgressData
                        {
                            zone_id = "zone_lw_002_mudpipe_hollow",
                            unlocked = false,
                            first_clear = false,
                            clear_count = 0
                        },
                        zone_lw_003_glowroot_passage = new ZoneProgressData
                        {
                            zone_id = "zone_lw_003_glowroot_passage",
                            unlocked = false,
                            first_clear = false,
                            clear_count = 0
                        },
                        keeper_lw_001_unlocked = false,
                        keeper_lw_001_defeated = false,
                        last_exploration_result = new ExplorationResultData(),
                        field_returns = new ExplorationResultData()
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
