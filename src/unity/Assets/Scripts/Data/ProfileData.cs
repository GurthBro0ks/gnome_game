using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GnomeGame.Data
{
    [Serializable]
    [DataContract]
    public class SaveVersionData
    {
        [DataMember(Order = 0)]
        public int version = 1;
    }

    [Serializable]
    [DataContract]
    public class AccountData
    {
        [DataMember(Order = 0)]
        public string uid = "";

        [DataMember(Order = 1)]
        public string display_name = "Local Gnome";

        [DataMember(Order = 2)]
        public string created_at = "";

        [DataMember(Order = 3)]
        public string last_login = "";

        [DataMember(Order = 4)]
        public string tutorial_state = "not_started";
    }

    [Serializable]
    [DataContract]
    public class WalletData
    {
        [DataMember(Order = 0)]
        public int mooncaps = 0;

        [DataMember(Order = 1)]
        public int mushcaps = 0;

        [DataMember(Order = 2)]
        public int glowcaps = 0;

        [DataMember(Order = 3)]
        public int lucky_draws = 0;

        [DataMember(Order = 4)]
        public int treasure_tickets = 0;

        [DataMember(Order = 5)]
        public int echoes = 0;

        [DataMember(Order = 6)]
        public int echo_shards = 0;

        [DataMember(Order = 7)]
        public int elder_books = 0;

        [DataMember(Order = 8)]
        public int crack_coins = 0;

        [DataMember(Order = 9)]
        public int bronze_shovels = 0;

        [DataMember(Order = 10)]
        public int favor_marks = 0;

        [DataMember(Order = 11)]
        public int strata_seals = 0;

        [DataMember(Order = 12)]
        public int festival_marks = 0;

        [DataMember(Order = 13)]
        public LoamwakeMaterialsData loamwake_materials = new LoamwakeMaterialsData();
    }

    [Serializable]
    [DataContract]
    public class LoamwakeMaterialsData
    {
        [DataMember(Order = 0)]
        public int tangled_root_twine = 0;

        [DataMember(Order = 1)]
        public int crumbled_ore_chunk = 0;

        [DataMember(Order = 2)]
        public int dull_glow_shard = 0;
    }

    [Serializable]
    [DataContract]
    public class BurrowBuildingStateData
    {
        [DataMember(Order = 0)]
        public int level = 1;

        [DataMember(Order = 1)]
        public int stored_output = 0;

        [DataMember(Order = 2)]
        public int storage_cap = 60;
    }

    [Serializable]
    [DataContract]
    public class RootmineStateData
    {
        [DataMember(Order = 0)]
        public int level = 0;

        [DataMember(Order = 1)]
        public bool unlocked = false;

        [DataMember(Order = 2)]
        public string status_note = "Locked until Burrow level 2";
    }

    [Serializable]
    [DataContract]
    public class BurrowStateData
    {
        [DataMember(Order = 0)]
        public int burrow_level = 1;

        [DataMember(Order = 1)]
        public int expand_count = 0;

        [DataMember(Order = 2)]
        public string last_production_tick = "";

        [DataMember(Order = 3)]
        public List<string> unlocked_rooms = new List<string>();

        [DataMember(Order = 4)]
        public BurrowBuildingStateData dewpond = new BurrowBuildingStateData();

        [DataMember(Order = 5)]
        public BurrowBuildingStateData mushpatch = new BurrowBuildingStateData();

        [DataMember(Order = 6)]
        public RootmineStateData rootmine = new RootmineStateData();
    }

    [Serializable]
    [DataContract]
    public class PlayerProfileData
    {
        [DataMember(Order = 0)]
        public SaveVersionData save_version = new SaveVersionData();

        [DataMember(Order = 1)]
        public AccountData account = new AccountData();

        [DataMember(Order = 2)]
        public WalletData wallet = new WalletData();

        [DataMember(Order = 3)]
        public BurrowStateData burrow_state = new BurrowStateData();

        [DataMember(Order = 4)]
        public StrataStateData strata_state = new StrataStateData();
    }

    [Serializable]
    [DataContract]
    public class MaterialRewardData
    {
        [DataMember(Order = 0)]
        public string material_id = "";

        [DataMember(Order = 1)]
        public int amount = 0;
    }

    [Serializable]
    [DataContract]
    public class ExplorationResultData
    {
        [DataMember(Order = 0)]
        public string last_zone_id = "";

        [DataMember(Order = 1)]
        public string route_id = "";

        [DataMember(Order = 2)]
        public string result = "";

        [DataMember(Order = 3)]
        public bool keeper_encounter = false;

        [DataMember(Order = 4)]
        public int mooncaps = 0;

        [DataMember(Order = 5)]
        public int mushcaps = 0;

        [DataMember(Order = 6)]
        public List<MaterialRewardData> material_rewards = new List<MaterialRewardData>();
    }

    [Serializable]
    [DataContract]
    public class ZoneProgressData
    {
        [DataMember(Order = 0)]
        public string zone_id = "";

        [DataMember(Order = 1)]
        public bool unlocked = false;

        [DataMember(Order = 2)]
        public bool first_clear = false;

        [DataMember(Order = 3)]
        public int clear_count = 0;
    }

    [Serializable]
    [DataContract]
    public class LoamwakeProgressData
    {
        [DataMember(Order = 0)]
        public ZoneProgressData zone_lw_001_rootvine_shelf = new ZoneProgressData();

        [DataMember(Order = 1)]
        public ZoneProgressData zone_lw_002_mudpipe_hollow = new ZoneProgressData();

        [DataMember(Order = 2)]
        public ZoneProgressData zone_lw_003_glowroot_passage = new ZoneProgressData();

        [DataMember(Order = 3)]
        public bool keeper_lw_001_unlocked = false;

        [DataMember(Order = 4)]
        public bool keeper_lw_001_defeated = false;

        [DataMember(Order = 5)]
        public ExplorationResultData last_exploration_result = new ExplorationResultData();

        [DataMember(Order = 6)]
        public ExplorationResultData field_returns = new ExplorationResultData();
    }

    [Serializable]
    [DataContract]
    public class StrataStateData
    {
        [DataMember(Order = 0)]
        public string current_stratum_id = "";

        [DataMember(Order = 1)]
        public List<string> unlocked_strata_ids = new List<string>();

        [DataMember(Order = 2)]
        public LoamwakeProgressData loamwake = new LoamwakeProgressData();
    }
}
