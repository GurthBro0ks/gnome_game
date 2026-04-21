using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class SocialProgressService
    {
        public const string UtilityPostId = "post_lw_001_root_twine_run";
        public const string GretaIntroPostId = "post_lw_005_greta_intro";
        public const string DewpondDutyId = "dty_daily_gather_dewpond";
        public const string ExploreDutyId = "dty_daily_explore_loamwake";
        public const string FixtureDutyId = "dty_daily_fixture_check";
        public const string GretaConfidantId = "cnf_001_placeholder_greta";
        public const string RootrailStationId = "rtr_station_lw_001_loamwake_terminal";

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

            if (profile.social_progress == null)
            {
                profile.social_progress = new SocialProgressData();
            }

            if (profile.social_progress.burrow_posts == null)
            {
                profile.social_progress.burrow_posts = new List<BurrowPostStateData>();
            }

            if (profile.social_progress.daily_duties == null)
            {
                profile.social_progress.daily_duties = new List<DailyDutyStateData>();
            }

            if (profile.social_progress.greta == null)
            {
                profile.social_progress.greta = new GretaStateData();
            }

            if (profile.social_progress.rootrail == null)
            {
                profile.social_progress.rootrail = new RootrailRevealStateData();
            }

            EnsurePost(profile, UtilityPostId, "Root Twine Run", "available");
            EnsurePost(profile, GretaIntroPostId, "Rail Scratches on the Board", IsGretaIntroPostUnlocked(profile) ? "available" : "locked");

            EnsureDailyDuty(profile, DewpondDutyId, "Gather Dewpond", 1, "Reward: 15 Mooncaps");
            EnsureDailyDuty(profile, ExploreDutyId, "Explore Loamwake", 1, "Reward: 20 Mooncaps");
            EnsureDailyDuty(profile, FixtureDutyId, "Check the First Fixture", 1, "Reward: 2 Rootrail Parts");

            RefreshAvailability(profile);

            if (string.IsNullOrEmpty(profile.social_progress.last_updated_at))
            {
                profile.social_progress.last_updated_at = ProfileFactory.UtcNowIso();
            }
        }

        public static void RefreshAvailability(PlayerProfileData profile)
        {
            if (profile == null || profile.social_progress == null)
            {
                return;
            }

            var gretaPost = GetPost(profile, GretaIntroPostId);
            if (gretaPost != null && !gretaPost.completed)
            {
                gretaPost.state = IsGretaIntroPostUnlocked(profile) ? "available" : "locked";
            }

            var rootrail = profile.social_progress.rootrail;
            if (rootrail.revealed)
            {
                rootrail.station_visible = true;
                rootrail.repair_progression_enabled = false;
                rootrail.current_step_id = "";
                rootrail.repair_timer_started = false;
                rootrail.status_note = "Old track exposed near the Burrow. Station shell visible; find the Forgotten Manual before repairs.";
            }
            else
            {
                rootrail.station_visible = false;
                rootrail.repair_progression_enabled = false;
                rootrail.current_step_id = "";
                rootrail.repair_timer_started = false;
                rootrail.status_note = "Rootrail has not been revealed yet.";
            }
        }

        public bool ReadUtilityPost(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            var post = GetPost(profile, UtilityPostId);
            if (post.completed)
            {
                status = "Root Twine Run already read";
                return false;
            }

            post.unread = false;
            post.completed = true;
            post.reward_claimed = true;
            post.state = "completed";
            post.last_updated_at = ProfileFactory.UtcNowIso();
            profile.wallet.mooncaps += 15;
            SetResult(profile, "Read Root Twine Run; gained 15 Mooncaps");
            status = profile.social_progress.latest_result_summary;
            return true;
        }

        public bool ReadGretaIntroPost(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            var post = GetPost(profile, GretaIntroPostId);
            if (post.state == "locked")
            {
                status = "Greta intro Post appears after first Rootvine Shelf clear";
                return false;
            }

            if (post.completed)
            {
                status = "Greta intro Post already completed";
                return false;
            }

            post.unread = false;
            post.completed = true;
            post.reward_claimed = true;
            post.state = "completed";
            post.last_updated_at = ProfileFactory.UtcNowIso();

            var greta = profile.social_progress.greta;
            greta.unlocked = true;
            greta.intro_post_completed = true;
            greta.last_interaction_at = ProfileFactory.UtcNowIso();
            profile.wallet.mooncaps += 40;

            SetResult(profile, "Greta unlocked through Burrow Post; gained 40 Mooncaps");
            status = profile.social_progress.latest_result_summary;
            return true;
        }

        public bool CompleteGretaFirstFollowup(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            var greta = profile.social_progress.greta;
            if (!greta.unlocked)
            {
                status = "Greta is locked";
                return false;
            }

            if (greta.first_followup_completed)
            {
                status = "Greta's first follow-up already completed";
                return false;
            }

            greta.first_followup_completed = true;
            greta.trust_points += 1;
            greta.trust_level = 1;
            greta.last_interaction_at = ProfileFactory.UtcNowIso();
            profile.wallet.mooncaps += 80;
            profile.wallet.loamwake_materials.tangled_root_twine += 4;
            SetResult(profile, "Completed Greta follow-up; Trust is now 1 and gained 80 Mooncaps");
            status = profile.social_progress.latest_result_summary;
            return true;
        }

        public bool RevealRootrail(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            var greta = profile.social_progress.greta;
            if (!greta.first_followup_completed)
            {
                status = "Complete Greta's first follow-up before the Rootrail reveal";
                return false;
            }

            if (!HasZone2Clear(profile))
            {
                status = "Survey Mudpipe Hollow before the Rootrail reveal";
                return false;
            }

            if (profile.social_progress.rootrail.revealed)
            {
                status = "Rootrail station shell already revealed";
                return false;
            }

            greta.rootrail_lead_completed = true;
            greta.last_interaction_at = ProfileFactory.UtcNowIso();
            profile.social_progress.rootrail.revealed = true;
            profile.social_progress.rootrail.station_visible = true;
            profile.social_progress.rootrail.repair_progression_enabled = false;
            profile.social_progress.rootrail.current_step_id = "";
            profile.social_progress.rootrail.repair_timer_started = false;
            profile.social_progress.rootrail.status_note = "Old track exposed near the Burrow. Station shell visible; repairs are not implemented in Sprint 4.";
            profile.wallet.mooncaps += 80;
            profile.wallet.rootrail_parts += 5;
            SetResult(profile, "Rootrail revealed as an old station shell; gained 5 Rootrail Parts");
            status = profile.social_progress.latest_result_summary;
            return true;
        }

        public void RecordDewpondGather(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            IncrementDuty(profile, DewpondDutyId);
        }

        public void RecordLoamwakeExplore(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            IncrementDuty(profile, ExploreDutyId);
        }

        public void RecordFixtureProgress(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            IncrementDuty(profile, FixtureDutyId);
        }

        public static bool IsRootrailRevealOnly(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            var rootrail = profile.social_progress.rootrail;
            return rootrail.revealed &&
                rootrail.station_visible &&
                !rootrail.repair_progression_enabled &&
                !rootrail.repair_timer_started &&
                string.IsNullOrEmpty(rootrail.current_step_id);
        }

        public static BurrowPostStateData GetPost(PlayerProfileData profile, string postId)
        {
            if (profile == null || profile.social_progress == null || profile.social_progress.burrow_posts == null)
            {
                return null;
            }

            foreach (var post in profile.social_progress.burrow_posts)
            {
                if (post != null && post.post_id == postId)
                {
                    return post;
                }
            }

            return null;
        }

        public static DailyDutyStateData GetDailyDuty(PlayerProfileData profile, string dutyId)
        {
            if (profile == null || profile.social_progress == null || profile.social_progress.daily_duties == null)
            {
                return null;
            }

            foreach (var duty in profile.social_progress.daily_duties)
            {
                if (duty != null && duty.duty_id == dutyId)
                {
                    return duty;
                }
            }

            return null;
        }

        private static void IncrementDuty(PlayerProfileData profile, string dutyId)
        {
            var duty = GetDailyDuty(profile, dutyId);
            if (duty == null || duty.completed)
            {
                return;
            }

            duty.progress = System.Math.Min(duty.target, duty.progress + 1);
            duty.last_updated_at = ProfileFactory.UtcNowIso();
            if (duty.progress >= duty.target)
            {
                duty.completed = true;
                duty.reward_claimed = true;
                GrantDutyReward(profile, dutyId);
            }
        }

        private static void GrantDutyReward(PlayerProfileData profile, string dutyId)
        {
            if (dutyId == DewpondDutyId)
            {
                profile.wallet.mooncaps += 15;
                SetResult(profile, "Daily Duty complete: Gather Dewpond (+15 Mooncaps)");
                return;
            }

            if (dutyId == ExploreDutyId)
            {
                profile.wallet.mooncaps += 20;
                SetResult(profile, "Daily Duty complete: Explore Loamwake (+20 Mooncaps)");
                return;
            }

            if (dutyId == FixtureDutyId)
            {
                profile.wallet.rootrail_parts += 2;
                SetResult(profile, "Daily Duty complete: Check the First Fixture (+2 Rootrail Parts)");
            }
        }

        private static void EnsurePost(PlayerProfileData profile, string postId, string title, string initialState)
        {
            var post = GetPost(profile, postId);
            if (post != null)
            {
                if (string.IsNullOrEmpty(post.title))
                {
                    post.title = title;
                }

                return;
            }

            profile.social_progress.burrow_posts.Add(new BurrowPostStateData
            {
                post_id = postId,
                title = title,
                state = initialState,
                unread = initialState == "available",
                completed = false,
                reward_claimed = false,
                last_updated_at = ProfileFactory.UtcNowIso()
            });
        }

        private static void EnsureDailyDuty(PlayerProfileData profile, string dutyId, string title, int target, string rewardSummary)
        {
            var duty = GetDailyDuty(profile, dutyId);
            if (duty != null)
            {
                return;
            }

            profile.social_progress.daily_duties.Add(new DailyDutyStateData
            {
                duty_id = dutyId,
                title = title,
                progress = 0,
                target = target,
                completed = false,
                reward_claimed = false,
                reward_summary = rewardSummary,
                last_updated_at = ProfileFactory.UtcNowIso()
            });
        }

        private static bool IsGretaIntroPostUnlocked(PlayerProfileData profile)
        {
            return profile != null &&
                profile.strata_state != null &&
                profile.strata_state.loamwake != null &&
                profile.strata_state.loamwake.zone_lw_001_rootvine_shelf != null &&
                profile.strata_state.loamwake.zone_lw_001_rootvine_shelf.first_clear;
        }

        private static bool HasZone2Clear(PlayerProfileData profile)
        {
            return profile != null &&
                profile.strata_state != null &&
                profile.strata_state.loamwake != null &&
                profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow != null &&
                profile.strata_state.loamwake.zone_lw_002_mudpipe_hollow.first_clear;
        }

        private static void SetResult(PlayerProfileData profile, string result)
        {
            profile.social_progress.latest_result_summary = result;
            profile.social_progress.last_updated_at = ProfileFactory.UtcNowIso();
        }
    }
}
