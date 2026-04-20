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
        public int mooncaps;

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
                mooncaps = profile != null && profile.wallet != null ? profile.wallet.mooncaps : 0
            };
        }
    }
}
