using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class LuckyDrawEventService
    {
        public const string EventId = "evt_luckydraw_001";
        public const string PrototypeWeekMarker = "prototype-week-001";
        public const string WeeklyClaimSourceId = "floor_lw_001_weekly_claim";
        public const string ActivitySourceId = "floor_lw_002_activity_milestone";

        public const string StallMooncapDrawId = "stall_lucky_001_mooncap_draw";
        public const string StallMaterialCacheId = "stall_lucky_002_material_cache";
        public const string StallPolishBundleId = "stall_lucky_003_polish_bundle";
        public const string StallFestivalExchangeId = "stall_lucky_004_festival_exchange";

        private const int ActivityTicketTarget = 3;

        private static readonly LedgerTier[] LedgerTiers =
        {
            new LedgerTier("ledger_lucky_001_first_pull", 1, "First Pull Bonus"),
            new LedgerTier("ledger_lucky_002_three_pulls", 3, "Material Bundle"),
            new LedgerTier("ledger_lucky_003_five_pulls", 5, "Polish Cache"),
            new LedgerTier("ledger_lucky_004_eight_pulls", 8, "Festival Marks")
        };

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

            if (profile.event_progress == null)
            {
                profile.event_progress = new EventProgressData();
            }

            if (profile.event_progress.lucky_draw_week == null)
            {
                profile.event_progress.lucky_draw_week = new LuckyDrawWeekStateData();
            }

            var state = profile.event_progress.lucky_draw_week;
            if (string.IsNullOrEmpty(state.event_id))
            {
                state.event_id = EventId;
            }

            if (string.IsNullOrEmpty(state.week_marker))
            {
                state.week_marker = PrototypeWeekMarker;
            }

            if (string.IsNullOrEmpty(state.unlock_gate_note))
            {
                state.unlock_gate_note = "Prototype gate: unlock Greta through Burrow Post";
            }

            if (string.IsNullOrEmpty(state.latest_pull_result))
            {
                state.latest_pull_result = "No pulls yet";
            }

            if (state.pull_history == null)
            {
                state.pull_history = new List<LuckyDrawPullHistoryData>();
            }

            if (state.stall_purchase_counts == null)
            {
                state.stall_purchase_counts = new List<LuckyStallPurchaseCountData>();
            }

            if (state.festival_ledger == null)
            {
                state.festival_ledger = new FestivalLedgerFreeLaneData();
            }

            if (state.festival_ledger.claimed_tier_ids == null)
            {
                state.festival_ledger.claimed_tier_ids = new List<string>();
            }

            if (string.IsNullOrEmpty(state.festival_ledger.latest_claim_summary))
            {
                state.festival_ledger.latest_claim_summary = "No Festival Ledger rewards claimed";
            }

            state.paid_path_active = false;
            state.iap_enabled = false;
            state.ladder_progress = state.pull_count;
            state.festival_ledger.progress_points = state.pull_count;
        }

        public bool SyncUnlock(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            var state = profile.event_progress.lucky_draw_week;
            var gateMet = IsPrototypeGateMet(profile);
            var changed = false;

            if (gateMet && !state.unlock_gate_met)
            {
                state.unlock_gate_met = true;
                changed = true;
            }

            if (state.unlock_gate_met && !state.unlocked)
            {
                state.unlocked = true;
                state.active = true;
                if (string.IsNullOrEmpty(state.started_at))
                {
                    state.started_at = ProfileFactory.UtcNowIso();
                }

                SetResult(state, "Lucky Draw Week unlocked through Greta");
                changed = true;
            }

            if (state.unlocked && !state.active)
            {
                state.active = true;
                changed = true;
            }

            return changed;
        }

        public bool ClaimWeeklyTicket(PlayerProfileData profile, out string status)
        {
            if (!RequireActive(profile, out status))
            {
                return false;
            }

            var state = profile.event_progress.lucky_draw_week;
            if (state.weekly_claimed)
            {
                status = "Weekly Lucky Draw already claimed";
                return false;
            }

            profile.wallet.lucky_draws += 1;
            state.weekly_claimed = true;
            SetResult(state, "Claimed weekly free Lucky Draw (+1)");
            status = state.latest_result_summary;
            return true;
        }

        public bool RecordFreeActivity(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            SyncUnlock(profile);

            var state = profile.event_progress.lucky_draw_week;
            if (!state.active || state.activity_ticket_claimed)
            {
                return false;
            }

            state.activity_progress = System.Math.Min(ActivityTicketTarget, state.activity_progress + 1);
            if (state.activity_progress >= ActivityTicketTarget)
            {
                state.activity_ticket_claimed = true;
                profile.wallet.lucky_draws += 1;
                SetResult(state, "Activity milestone complete: +1 Lucky Draw");
            }
            else
            {
                SetResult(state, "Lucky Draw activity progress " + state.activity_progress + "/" + ActivityTicketTarget);
            }

            return true;
        }

        public bool Pull(PlayerProfileData profile, out string status)
        {
            if (!RequireActive(profile, out status))
            {
                return false;
            }

            if (profile.wallet.lucky_draws < 1)
            {
                status = "Need 1 Lucky Draw ticket";
                return false;
            }

            var state = profile.event_progress.lucky_draw_week;
            profile.wallet.lucky_draws -= 1;

            var rowIndex = state.pull_count % 5;
            string rowId;
            string rewardSummary;

            if (rowIndex == 0)
            {
                rowId = "pull_lw_003_mooncaps";
                profile.wallet.mooncaps += 90;
                rewardSummary = "+90 Mooncaps";
            }
            else if (rowIndex == 1)
            {
                rowId = "pull_lw_001_loam_bundle";
                profile.wallet.loamwake_materials.tangled_root_twine += 4;
                profile.wallet.loamwake_materials.crumbled_ore_chunk += 2;
                rewardSummary = "+4 Twine, +2 Ore";
            }
            else if (rowIndex == 2)
            {
                rowId = "pull_lw_005_festival_marks";
                profile.wallet.festival_marks += 12;
                rewardSummary = "+12 Festival Marks";
            }
            else if (rowIndex == 3)
            {
                rowId = "pull_lw_004_polish";
                profile.wallet.polishes += 3;
                rewardSummary = "+3 Polishes";
            }
            else
            {
                rowId = "pull_lw_002_mid_materials";
                profile.wallet.loamwake_materials.dull_glow_shard += 2;
                profile.wallet.mushcaps += 1;
                rewardSummary = "+2 Dull Glow Shards, +1 Mushcap";
            }

            state.pull_count += 1;
            state.ladder_progress = state.pull_count;
            state.festival_ledger.progress_points = state.pull_count;
            state.latest_pull_result = "Pull " + state.pull_count + " (" + rowId + "): " + rewardSummary;
            state.pull_history.Add(new LuckyDrawPullHistoryData
            {
                pull_number = state.pull_count,
                pull_row_id = rowId,
                reward_summary = rewardSummary,
                pulled_at = ProfileFactory.UtcNowIso()
            });

            while (state.pull_history.Count > 5)
            {
                state.pull_history.RemoveAt(0);
            }

            SetResult(state, state.latest_pull_result);
            status = state.latest_result_summary;
            return true;
        }

        public bool ClaimNextLedgerReward(PlayerProfileData profile, out string status)
        {
            if (!RequireActive(profile, out status))
            {
                return false;
            }

            var state = profile.event_progress.lucky_draw_week;
            for (var i = 0; i < LedgerTiers.Length; i++)
            {
                var tier = LedgerTiers[i];
                if (state.pull_count >= tier.threshold && !IsLedgerTierClaimed(state, tier.tierId))
                {
                    GrantLedgerTier(profile, tier, out status);
                    return true;
                }
            }

            status = "No Festival Ledger free-lane reward ready";
            return false;
        }

        public bool PurchaseStallItem(PlayerProfileData profile, string stallId, out string status)
        {
            if (!RequireActive(profile, out status))
            {
                return false;
            }

            if (stallId == StallMooncapDrawId)
            {
                return PurchaseMooncapDraw(profile, out status);
            }

            if (stallId == StallMaterialCacheId)
            {
                return PurchaseMaterialCache(profile, out status);
            }

            if (stallId == StallPolishBundleId)
            {
                return PurchasePolishBundle(profile, out status);
            }

            if (stallId == StallFestivalExchangeId)
            {
                return PurchaseFestivalExchange(profile, out status);
            }

            status = "Unknown Lucky Stall item";
            return false;
        }

        public static bool IsEventVisible(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return profile.event_progress.lucky_draw_week.unlocked &&
                profile.event_progress.lucky_draw_week.active;
        }

        public static string BuildLedgerSummary(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            var state = profile.event_progress.lucky_draw_week;
            var value = "Festival Ledger free lane - pulls " + state.pull_count + "\n";
            for (var i = 0; i < LedgerTiers.Length; i++)
            {
                var tier = LedgerTiers[i];
                value += tier.displayName + " (" + tier.threshold + "): " +
                    (IsLedgerTierClaimed(state, tier.tierId) ? "claimed" : (state.pull_count >= tier.threshold ? "ready" : "locked")) + "\n";
            }

            value += state.festival_ledger.latest_claim_summary;
            return value;
        }

        public static string BuildStallSummary(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return "Lucky Stall\n" +
                "One Lucky Draw: 250 Mooncaps, remaining " + GetRemainingPurchases(profile, StallMooncapDrawId, 1) + "/1\n" +
                "Loamwake Material Cache: 2 Lucky Draws, remaining " + GetRemainingPurchases(profile, StallMaterialCacheId, 2) + "/2\n" +
                "Polish Bundle: 1 Lucky Draw, remaining " + GetRemainingPurchases(profile, StallPolishBundleId, 2) + "/2\n" +
                "Festival Marks Exchange: 20 Festival Marks, remaining " + GetRemainingPurchases(profile, StallFestivalExchangeId, 1) + "/1";
        }

        public static int GetRemainingPurchases(PlayerProfileData profile, string stallId, int limit)
        {
            EnsureDefaults(profile);
            return System.Math.Max(0, limit - GetPurchaseCount(profile.event_progress.lucky_draw_week, stallId));
        }

        private static bool RequireActive(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            var service = new LuckyDrawEventService();
            service.SyncUnlock(profile);
            if (!profile.event_progress.lucky_draw_week.active)
            {
                status = "Lucky Draw Week is hidden until Greta is unlocked";
                return false;
            }

            status = "";
            return true;
        }

        private static bool PurchaseMooncapDraw(PlayerProfileData profile, out string status)
        {
            const int limit = 1;
            const int cost = 250;
            var state = profile.event_progress.lucky_draw_week;
            if (GetPurchaseCount(state, StallMooncapDrawId) >= limit)
            {
                status = "Mooncap Lucky Draw sold out this prototype week";
                return false;
            }

            if (profile.wallet.mooncaps < cost)
            {
                status = "Need 250 Mooncaps";
                return false;
            }

            profile.wallet.mooncaps -= cost;
            profile.wallet.lucky_draws += 1;
            IncrementPurchase(state, StallMooncapDrawId);
            SetResult(state, "Lucky Stall: bought 1 Lucky Draw for 250 Mooncaps");
            status = state.latest_result_summary;
            return true;
        }

        private static bool PurchaseMaterialCache(PlayerProfileData profile, out string status)
        {
            const int limit = 2;
            const int cost = 2;
            var state = profile.event_progress.lucky_draw_week;
            if (GetPurchaseCount(state, StallMaterialCacheId) >= limit)
            {
                status = "Material Cache sold out this prototype week";
                return false;
            }

            if (profile.wallet.lucky_draws < cost)
            {
                status = "Need 2 Lucky Draws";
                return false;
            }

            profile.wallet.lucky_draws -= cost;
            profile.wallet.loamwake_materials.tangled_root_twine += 8;
            profile.wallet.loamwake_materials.crumbled_ore_chunk += 5;
            profile.wallet.loamwake_materials.dull_glow_shard += 2;
            IncrementPurchase(state, StallMaterialCacheId);
            SetResult(state, "Lucky Stall: bought Loamwake Material Cache");
            status = state.latest_result_summary;
            return true;
        }

        private static bool PurchasePolishBundle(PlayerProfileData profile, out string status)
        {
            const int limit = 2;
            const int cost = 1;
            var state = profile.event_progress.lucky_draw_week;
            if (GetPurchaseCount(state, StallPolishBundleId) >= limit)
            {
                status = "Polish Bundle sold out this prototype week";
                return false;
            }

            if (profile.wallet.lucky_draws < cost)
            {
                status = "Need 1 Lucky Draw";
                return false;
            }

            profile.wallet.lucky_draws -= cost;
            profile.wallet.polishes += 6;
            IncrementPurchase(state, StallPolishBundleId);
            SetResult(state, "Lucky Stall: bought Polish Bundle (+6 Polishes)");
            status = state.latest_result_summary;
            return true;
        }

        private static bool PurchaseFestivalExchange(PlayerProfileData profile, out string status)
        {
            const int limit = 1;
            const int cost = 20;
            var state = profile.event_progress.lucky_draw_week;
            if (GetPurchaseCount(state, StallFestivalExchangeId) >= limit)
            {
                status = "Festival Marks Exchange sold out this prototype week";
                return false;
            }

            if (profile.wallet.festival_marks < cost)
            {
                status = "Need 20 Festival Marks";
                return false;
            }

            profile.wallet.festival_marks -= cost;
            profile.wallet.lucky_draws += 1;
            IncrementPurchase(state, StallFestivalExchangeId);
            SetResult(state, "Lucky Stall: exchanged 20 Festival Marks for 1 Lucky Draw");
            status = state.latest_result_summary;
            return true;
        }

        private static void GrantLedgerTier(PlayerProfileData profile, LedgerTier tier, out string status)
        {
            var state = profile.event_progress.lucky_draw_week;
            if (tier.tierId == "ledger_lucky_001_first_pull")
            {
                profile.wallet.mooncaps += 80;
                status = "Claimed First Pull Bonus: +80 Mooncaps";
            }
            else if (tier.tierId == "ledger_lucky_002_three_pulls")
            {
                profile.wallet.loamwake_materials.tangled_root_twine += 4;
                profile.wallet.loamwake_materials.crumbled_ore_chunk += 2;
                status = "Claimed Material Bundle: +4 Twine, +2 Ore";
            }
            else if (tier.tierId == "ledger_lucky_003_five_pulls")
            {
                profile.wallet.polishes += 4;
                profile.wallet.mooncaps += 100;
                status = "Claimed Polish Cache: +4 Polishes, +100 Mooncaps";
            }
            else
            {
                profile.wallet.festival_marks += 20;
                status = "Claimed Festival Marks: +20 Festival Marks";
            }

            state.festival_ledger.claimed_tier_ids.Add(tier.tierId);
            state.festival_ledger.latest_claim_summary = status;
            state.festival_ledger.last_updated_at = ProfileFactory.UtcNowIso();
            SetResult(state, status);
        }

        private static bool IsPrototypeGateMet(PlayerProfileData profile)
        {
            SocialProgressService.EnsureDefaults(profile);
            return profile.social_progress.greta.unlocked;
        }

        private static bool IsLedgerTierClaimed(LuckyDrawWeekStateData state, string tierId)
        {
            return state.festival_ledger.claimed_tier_ids != null &&
                state.festival_ledger.claimed_tier_ids.Contains(tierId);
        }

        private static int GetPurchaseCount(LuckyDrawWeekStateData state, string stallId)
        {
            if (state == null || state.stall_purchase_counts == null)
            {
                return 0;
            }

            foreach (var purchase in state.stall_purchase_counts)
            {
                if (purchase != null && purchase.stall_id == stallId)
                {
                    return purchase.purchase_count;
                }
            }

            return 0;
        }

        private static void IncrementPurchase(LuckyDrawWeekStateData state, string stallId)
        {
            foreach (var purchase in state.stall_purchase_counts)
            {
                if (purchase != null && purchase.stall_id == stallId)
                {
                    purchase.purchase_count += 1;
                    return;
                }
            }

            state.stall_purchase_counts.Add(new LuckyStallPurchaseCountData
            {
                stall_id = stallId,
                purchase_count = 1
            });
        }

        private static void SetResult(LuckyDrawWeekStateData state, string result)
        {
            state.latest_result_summary = result;
            state.last_updated_at = ProfileFactory.UtcNowIso();
        }

        private sealed class LedgerTier
        {
            public readonly string tierId;
            public readonly int threshold;
            public readonly string displayName;

            public LedgerTier(string activeTierId, int activeThreshold, string activeDisplayName)
            {
                tierId = activeTierId;
                threshold = activeThreshold;
                displayName = activeDisplayName;
            }
        }
    }
}
