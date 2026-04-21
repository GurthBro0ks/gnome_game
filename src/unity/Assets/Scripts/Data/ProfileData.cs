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

        [DataMember(Order = 5)]
        public int fixture_cap = 0;
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

        [DataMember(Order = 14)]
        public int rootrail_parts = 0;

        [DataMember(Order = 15)]
        public int polishes = 0;

        [DataMember(Order = 16)]
        public int strain_seeds = 0;
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

        [DataMember(Order = 3)]
        public int stored_tangled_root_twine = 0;

        [DataMember(Order = 4)]
        public int stored_crumbled_ore_chunk = 0;

        [DataMember(Order = 5)]
        public int material_storage_cap = 30;
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

        [DataMember(Order = 5)]
        public FixtureStateData fixture_state = new FixtureStateData();

        [DataMember(Order = 6)]
        public HatStateData hat_state = new HatStateData();

        [DataMember(Order = 7)]
        public VaultStateData vault_state = new VaultStateData();

        [DataMember(Order = 8)]
        public SocialProgressData social_progress = new SocialProgressData();

        [DataMember(Order = 9)]
        public EventProgressData event_progress = new EventProgressData();

        [DataMember(Order = 10)]
        public CrackProgressData crack_progress = new CrackProgressData();

        [DataMember(Order = 11)]
        public CliqueProgressData clique_progress = new CliqueProgressData();
    }

    [Serializable]
    [DataContract]
    public class CrackProgressData
    {
        [DataMember(Order = 0)]
        public bool visible = false;

        [DataMember(Order = 1)]
        public bool unlocked = false;

        [DataMember(Order = 2)]
        public bool unlock_gate_met = false;

        [DataMember(Order = 3)]
        public string unlock_gate_note = "Prototype gate: reveal the Rootrail station";

        [DataMember(Order = 4)]
        public int current_depth = 0;

        [DataMember(Order = 5)]
        public int best_depth = 0;

        [DataMember(Order = 6)]
        public int probe_count = 0;

        [DataMember(Order = 7)]
        public int crack_coins_earned_total = 0;

        [DataMember(Order = 8)]
        public string reward_claim_summary = "No Crack Coins earned yet";

        [DataMember(Order = 9)]
        public string latest_result_summary = "No Crack probes yet";

        [DataMember(Order = 10)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class CliqueProgressData
    {
        [DataMember(Order = 0)]
        public bool visible = false;

        [DataMember(Order = 1)]
        public bool unlocked = false;

        [DataMember(Order = 2)]
        public bool unlock_gate_met = false;

        [DataMember(Order = 3)]
        public string unlock_gate_note = "Prototype gate: reveal the Rootrail station";

        [DataMember(Order = 4)]
        public string clique_name = "Rootrail Porch Circle";

        [DataMember(Order = 5)]
        public string player_role = "Founder";

        [DataMember(Order = 6)]
        public List<CliqueRosterEntryData> roster = new List<CliqueRosterEntryData>();

        [DataMember(Order = 7)]
        public bool local_stipend_claimed = false;

        [DataMember(Order = 8)]
        public string latest_result_summary = "No Clique actions yet";

        [DataMember(Order = 9)]
        public bool great_dispute_stub_only = true;

        [DataMember(Order = 10)]
        public bool networking_enabled = false;

        [DataMember(Order = 11)]
        public bool multiplayer_enabled = false;

        [DataMember(Order = 12)]
        public bool shared_state_enabled = false;

        [DataMember(Order = 13)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class CliqueRosterEntryData
    {
        [DataMember(Order = 0)]
        public string display_name = "";

        [DataMember(Order = 1)]
        public string role = "";

        [DataMember(Order = 2)]
        public string status = "";
    }

    [Serializable]
    [DataContract]
    public class EventProgressData
    {
        [DataMember(Order = 0)]
        public LuckyDrawWeekStateData lucky_draw_week = new LuckyDrawWeekStateData();
    }

    [Serializable]
    [DataContract]
    public class LuckyDrawWeekStateData
    {
        [DataMember(Order = 0)]
        public string event_id = "evt_luckydraw_001";

        [DataMember(Order = 1)]
        public bool active = false;

        [DataMember(Order = 2)]
        public bool unlocked = false;

        [DataMember(Order = 3)]
        public bool unlock_gate_met = false;

        [DataMember(Order = 4)]
        public string unlock_gate_note = "Prototype gate: unlock Greta through Burrow Post";

        [DataMember(Order = 5)]
        public string week_marker = "prototype-week-001";

        [DataMember(Order = 6)]
        public string started_at = "";

        [DataMember(Order = 7)]
        public bool weekly_claimed = false;

        [DataMember(Order = 8)]
        public int activity_progress = 0;

        [DataMember(Order = 9)]
        public bool activity_ticket_claimed = false;

        [DataMember(Order = 10)]
        public int pull_count = 0;

        [DataMember(Order = 11)]
        public int ladder_progress = 0;

        [DataMember(Order = 12)]
        public string latest_pull_result = "No pulls yet";

        [DataMember(Order = 13)]
        public List<LuckyDrawPullHistoryData> pull_history = new List<LuckyDrawPullHistoryData>();

        [DataMember(Order = 14)]
        public List<LuckyStallPurchaseCountData> stall_purchase_counts = new List<LuckyStallPurchaseCountData>();

        [DataMember(Order = 15)]
        public FestivalLedgerFreeLaneData festival_ledger = new FestivalLedgerFreeLaneData();

        [DataMember(Order = 16)]
        public bool paid_path_active = false;

        [DataMember(Order = 17)]
        public bool iap_enabled = false;

        [DataMember(Order = 18)]
        public string latest_result_summary = "";

        [DataMember(Order = 19)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class LuckyDrawPullHistoryData
    {
        [DataMember(Order = 0)]
        public int pull_number = 0;

        [DataMember(Order = 1)]
        public string pull_row_id = "";

        [DataMember(Order = 2)]
        public string reward_summary = "";

        [DataMember(Order = 3)]
        public string pulled_at = "";
    }

    [Serializable]
    [DataContract]
    public class LuckyStallPurchaseCountData
    {
        [DataMember(Order = 0)]
        public string stall_id = "";

        [DataMember(Order = 1)]
        public int purchase_count = 0;
    }

    [Serializable]
    [DataContract]
    public class FestivalLedgerFreeLaneData
    {
        [DataMember(Order = 0)]
        public int progress_points = 0;

        [DataMember(Order = 1)]
        public List<string> claimed_tier_ids = new List<string>();

        [DataMember(Order = 2)]
        public string latest_claim_summary = "No Festival Ledger rewards claimed";

        [DataMember(Order = 3)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class SocialProgressData
    {
        [DataMember(Order = 0)]
        public List<BurrowPostStateData> burrow_posts = new List<BurrowPostStateData>();

        [DataMember(Order = 1)]
        public List<DailyDutyStateData> daily_duties = new List<DailyDutyStateData>();

        [DataMember(Order = 2)]
        public GretaStateData greta = new GretaStateData();

        [DataMember(Order = 3)]
        public RootrailRevealStateData rootrail = new RootrailRevealStateData();

        [DataMember(Order = 4)]
        public string latest_result_summary = "";

        [DataMember(Order = 5)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class BurrowPostStateData
    {
        [DataMember(Order = 0)]
        public string post_id = "";

        [DataMember(Order = 1)]
        public string title = "";

        [DataMember(Order = 2)]
        public string state = "locked";

        [DataMember(Order = 3)]
        public bool unread = true;

        [DataMember(Order = 4)]
        public bool completed = false;

        [DataMember(Order = 5)]
        public bool reward_claimed = false;

        [DataMember(Order = 6)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class DailyDutyStateData
    {
        [DataMember(Order = 0)]
        public string duty_id = "";

        [DataMember(Order = 1)]
        public string title = "";

        [DataMember(Order = 2)]
        public int progress = 0;

        [DataMember(Order = 3)]
        public int target = 1;

        [DataMember(Order = 4)]
        public bool completed = false;

        [DataMember(Order = 5)]
        public bool reward_claimed = false;

        [DataMember(Order = 6)]
        public string reward_summary = "";

        [DataMember(Order = 7)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class GretaStateData
    {
        [DataMember(Order = 0)]
        public bool unlocked = false;

        [DataMember(Order = 1)]
        public int trust_level = 0;

        [DataMember(Order = 2)]
        public int trust_points = 0;

        [DataMember(Order = 3)]
        public bool intro_post_completed = false;

        [DataMember(Order = 4)]
        public bool first_followup_completed = false;

        [DataMember(Order = 5)]
        public bool rootrail_lead_completed = false;

        [DataMember(Order = 6)]
        public bool calling_unlocked = false;

        [DataMember(Order = 7)]
        public string last_interaction_at = "";
    }

    [Serializable]
    [DataContract]
    public class RootrailRevealStateData
    {
        [DataMember(Order = 0)]
        public bool revealed = false;

        [DataMember(Order = 1)]
        public bool station_visible = false;

        [DataMember(Order = 2)]
        public bool repair_progression_enabled = false;

        [DataMember(Order = 3)]
        public string station_id = "rtr_station_lw_001_loamwake_terminal";

        [DataMember(Order = 4)]
        public string current_step_id = "";

        [DataMember(Order = 5)]
        public bool repair_timer_started = false;

        [DataMember(Order = 6)]
        public bool forgotten_manual_discovered = false;

        [DataMember(Order = 7)]
        public string status_note = "Rootrail has not been revealed yet.";
    }

    [Serializable]
    [DataContract]
    public class FixtureInstanceData
    {
        [DataMember(Order = 0)]
        public string instance_id = "";

        [DataMember(Order = 1)]
        public string fixture_id = "";

        [DataMember(Order = 2)]
        public int upgrade_level = 0;

        [DataMember(Order = 3)]
        public string acquired_at = "";
    }

    [Serializable]
    [DataContract]
    public class FixtureStateData
    {
        [DataMember(Order = 0)]
        public List<FixtureInstanceData> fixture_inventory = new List<FixtureInstanceData>();

        [DataMember(Order = 1)]
        public List<string> equipped_fixture_instance_ids = new List<string>();

        [DataMember(Order = 2)]
        public List<string> crafted_recipe_ids = new List<string>();

        [DataMember(Order = 3)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class HatUnlockData
    {
        [DataMember(Order = 0)]
        public string hat_id = "";

        [DataMember(Order = 1)]
        public bool unlocked = false;

        [DataMember(Order = 2)]
        public string unlocked_at = "";

        [DataMember(Order = 3)]
        public bool permanent_passive_active = true;
    }

    [Serializable]
    [DataContract]
    public class HatStateData
    {
        [DataMember(Order = 0)]
        public List<HatUnlockData> unlocked_hats = new List<HatUnlockData>();

        [DataMember(Order = 1)]
        public string visible_hat_id = "";

        [DataMember(Order = 2)]
        public string passive_summary = "No Hat passives active";

        [DataMember(Order = 3)]
        public string last_updated_at = "";
    }

    [Serializable]
    [DataContract]
    public class VaultStateData
    {
        [DataMember(Order = 0)]
        public bool visible = false;

        [DataMember(Order = 1)]
        public bool treasure_progression_enabled = false;

        [DataMember(Order = 2)]
        public int owned_treasure_count = 0;

        [DataMember(Order = 3)]
        public string status_note = "Vault of Treasures shell only; Treasures are not implemented in Sprint 3.";
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
