using System.Collections.Generic;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class CrackCliqueService
    {
        private const int CrackDepthPerProbe = 5;
        private const int CrackCoinRewardPerProbe = 3;
        private const int CliqueStipendFavorMarks = 5;

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

            if (profile.crack_progress == null)
            {
                profile.crack_progress = new CrackProgressData();
            }

            if (profile.clique_progress == null)
            {
                profile.clique_progress = new CliqueProgressData();
            }

            var crack = profile.crack_progress;
            if (string.IsNullOrEmpty(crack.unlock_gate_note))
            {
                crack.unlock_gate_note = "Prototype gate: reveal the Rootrail station";
            }

            if (string.IsNullOrEmpty(crack.reward_claim_summary))
            {
                crack.reward_claim_summary = "No Crack Coins earned yet";
            }

            if (string.IsNullOrEmpty(crack.latest_result_summary))
            {
                crack.latest_result_summary = "No Crack probes yet";
            }

            var clique = profile.clique_progress;
            if (string.IsNullOrEmpty(clique.unlock_gate_note))
            {
                clique.unlock_gate_note = "Prototype gate: reveal the Rootrail station";
            }

            if (string.IsNullOrEmpty(clique.clique_name))
            {
                clique.clique_name = "Rootrail Porch Circle";
            }

            if (string.IsNullOrEmpty(clique.player_role))
            {
                clique.player_role = "Founder";
            }

            if (string.IsNullOrEmpty(clique.latest_result_summary))
            {
                clique.latest_result_summary = "No Clique actions yet";
            }

            if (clique.roster == null)
            {
                clique.roster = new List<CliqueRosterEntryData>();
            }

            EnsureRosterEntry(clique, "You", "Founder", "Holding the charter stub");
            EnsureRosterEntry(clique, "Greta", "Steward", "Watching the old track");
            EnsureRosterEntry(clique, "Mossvane", "Burrowmate", "Placeholder roster entry");

            clique.great_dispute_stub_only = true;
            clique.networking_enabled = false;
            clique.multiplayer_enabled = false;
            clique.shared_state_enabled = false;
        }

        public bool SyncUnlocks(PlayerProfileData profile)
        {
            EnsureDefaults(profile);

            var gateMet = IsRootrailRevealGateMet(profile);
            var changed = false;

            changed |= SyncCrack(profile.crack_progress, gateMet);
            changed |= SyncClique(profile.clique_progress, gateMet);

            return changed;
        }

        public bool ProbeCrack(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            SyncUnlocks(profile);

            var crack = profile.crack_progress;
            if (!crack.unlocked)
            {
                status = "The Crack is locked until the Rootrail station reveal";
                return false;
            }

            crack.probe_count += 1;
            crack.current_depth += CrackDepthPerProbe;
            if (crack.current_depth > crack.best_depth)
            {
                crack.best_depth = crack.current_depth;
            }

            profile.wallet.crack_coins += CrackCoinRewardPerProbe;
            crack.crack_coins_earned_total += CrackCoinRewardPerProbe;
            crack.reward_claim_summary = "Earned " + crack.crack_coins_earned_total + " Crack Coins across " + crack.probe_count + " probes";
            crack.latest_result_summary = "Probe " + crack.probe_count + " reached depth " + crack.current_depth + " and found +" + CrackCoinRewardPerProbe + " Crack Coins";
            crack.last_updated_at = ProfileFactory.UtcNowIso();

            status = crack.latest_result_summary;
            return true;
        }

        public bool ClaimCliqueStipend(PlayerProfileData profile, out string status)
        {
            EnsureDefaults(profile);
            SyncUnlocks(profile);

            var clique = profile.clique_progress;
            if (!clique.unlocked)
            {
                status = "Clique shell is locked until the Rootrail station reveal";
                return false;
            }

            if (clique.local_stipend_claimed)
            {
                status = "Local Clique stipend already claimed";
                return false;
            }

            profile.wallet.favor_marks += CliqueStipendFavorMarks;
            clique.local_stipend_claimed = true;
            clique.latest_result_summary = "Claimed local Clique stipend: +" + CliqueStipendFavorMarks + " Favor Marks";
            clique.last_updated_at = ProfileFactory.UtcNowIso();
            status = clique.latest_result_summary;
            return true;
        }

        public static bool IsCrackVisible(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return profile.crack_progress.visible;
        }

        public static bool IsCliqueVisible(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return profile.clique_progress.visible;
        }

        public static bool IsGreatDisputeStubOnly(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            return profile.clique_progress.great_dispute_stub_only;
        }

        public static bool HasNoNetworkingOrSharedState(PlayerProfileData profile)
        {
            EnsureDefaults(profile);
            var clique = profile.clique_progress;
            return !clique.networking_enabled && !clique.multiplayer_enabled && !clique.shared_state_enabled;
        }

        private static bool SyncCrack(CrackProgressData crack, bool gateMet)
        {
            var changed = false;

            if (gateMet && !crack.unlock_gate_met)
            {
                crack.unlock_gate_met = true;
                changed = true;
            }

            if (crack.unlock_gate_met && !crack.visible)
            {
                crack.visible = true;
                changed = true;
            }

            if (crack.visible && !crack.unlocked)
            {
                crack.unlocked = true;
                crack.latest_result_summary = "The Crack opened as a prototype probe layer";
                changed = true;
            }

            if (changed)
            {
                crack.last_updated_at = ProfileFactory.UtcNowIso();
            }

            return changed;
        }

        private static bool SyncClique(CliqueProgressData clique, bool gateMet)
        {
            var changed = false;

            if (gateMet && !clique.unlock_gate_met)
            {
                clique.unlock_gate_met = true;
                changed = true;
            }

            if (clique.unlock_gate_met && !clique.visible)
            {
                clique.visible = true;
                changed = true;
            }

            if (clique.visible && !clique.unlocked)
            {
                clique.unlocked = true;
                clique.latest_result_summary = "Clique shell unlocked locally; no shared state active";
                changed = true;
            }

            if (changed)
            {
                clique.last_updated_at = ProfileFactory.UtcNowIso();
            }

            return changed;
        }

        private static bool IsRootrailRevealGateMet(PlayerProfileData profile)
        {
            return profile != null &&
                profile.social_progress != null &&
                profile.social_progress.rootrail != null &&
                profile.social_progress.rootrail.revealed &&
                profile.social_progress.rootrail.station_visible;
        }

        private static void EnsureRosterEntry(CliqueProgressData clique, string displayName, string role, string status)
        {
            for (var i = 0; i < clique.roster.Count; i++)
            {
                var entry = clique.roster[i];
                if (entry != null && entry.display_name == displayName)
                {
                    if (string.IsNullOrEmpty(entry.role))
                    {
                        entry.role = role;
                    }

                    if (string.IsNullOrEmpty(entry.status))
                    {
                        entry.status = status;
                    }

                    return;
                }
            }

            clique.roster.Add(new CliqueRosterEntryData
            {
                display_name = displayName,
                role = role,
                status = status
            });
        }
    }
}
