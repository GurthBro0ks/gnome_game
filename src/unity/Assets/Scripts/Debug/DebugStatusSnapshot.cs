using GnomeGame.Core;
using GnomeGame.Data;

namespace GnomeGame.Debugging
{
    public class DebugStatusSnapshot
    {
        public string auth_state;
        public string save_state;
        public string active_uid;
        public string save_file_path;
        public string last_production_tick;
        public string current_stratum_id;
        public int mooncaps;
        public int mushcaps;

        public static DebugStatusSnapshot From(AuthManager authManager, SaveManager saveManager, PlayerProfileData profile)
        {
            return new DebugStatusSnapshot
            {
                auth_state = authManager != null ? authManager.StatusMessage : "Auth not initialized",
                save_state = saveManager != null
                    ? saveManager.LastLoadStatus + " | " + saveManager.LastSaveStatus
                    : "Save not initialized",
                active_uid = profile != null && profile.account != null ? profile.account.uid : "",
                save_file_path = saveManager != null ? saveManager.SaveFilePath : "",
                last_production_tick = profile != null && profile.burrow_state != null ? profile.burrow_state.last_production_tick : "",
                current_stratum_id = profile != null && profile.strata_state != null ? profile.strata_state.current_stratum_id : "",
                mooncaps = profile != null && profile.wallet != null ? profile.wallet.mooncaps : 0,
                mushcaps = profile != null && profile.wallet != null ? profile.wallet.mushcaps : 0
            };
        }
    }
}
