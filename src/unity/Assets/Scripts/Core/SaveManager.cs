using System;
using System.IO;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public class SaveManager
    {
        private const string SaveFileName = "profile.json";

        public SaveManager(string persistentDataPath)
        {
            PersistentDataPath = persistentDataPath;
            SaveFilePath = Path.Combine(persistentDataPath, SaveFileName);
        }

        public string PersistentDataPath { get; private set; }
        public string SaveFilePath { get; private set; }
        public PlayerProfileData Profile { get; private set; }
        public string LastSaveStatus { get; private set; } = "No save attempted";
        public string LastLoadStatus { get; private set; } = "No load attempted";

        public PlayerProfileData LoadOrCreateProfile(string uid)
        {
            Directory.CreateDirectory(PersistentDataPath);

            if (!File.Exists(SaveFilePath))
            {
                Profile = ProfileFactory.CreateDefault(uid);
                EnsureProfileShape(uid);
                SaveProfile("created default profile");
                LastLoadStatus = "No local save found; created default profile";
                return Profile;
            }

            try
            {
                var json = File.ReadAllText(SaveFilePath);
                Profile = ProfileJsonSerializer.FromJson(json);
                EnsureProfileShape(uid);
                Profile.account.last_login = ProfileFactory.UtcNowIso();
                CheckVersionAndMigrateIfNeeded();
                SaveProfile("loaded existing profile");
                LastLoadStatus = "Loaded existing profile";
            }
            catch (Exception exception)
            {
                BackupUnreadableSave();
                Profile = ProfileFactory.CreateDefault(uid);
                SaveProfile("recovered from unreadable save");
                LastLoadStatus = "Load failed; created default profile: " + exception.Message;
            }

            return Profile;
        }

        public bool SaveProfile(string reason)
        {
            if (Profile == null)
            {
                LastSaveStatus = "Save skipped; no profile loaded";
                return false;
            }

            try
            {
                Directory.CreateDirectory(PersistentDataPath);
                var json = ProfileJsonSerializer.ToJson(Profile);
                WriteAtomic(SaveFilePath, json);
                LastSaveStatus = "Saved " + ProfileFactory.UtcNowIso() + " (" + reason + ")";
                return true;
            }
            catch (Exception exception)
            {
                LastSaveStatus = "Save failed: " + exception.Message;
                return false;
            }
        }

        public bool MutateProfile(Action<PlayerProfileData> mutation, string reason)
        {
            if (Profile == null)
            {
                LastSaveStatus = "Mutation skipped; no profile loaded";
                return false;
            }

            mutation(Profile);
            TutorialService.SyncProgress(Profile);
            return SaveProfile(reason);
        }

        public bool MutateProfileIfChanged(Func<PlayerProfileData, bool> mutation, string reason)
        {
            if (Profile == null)
            {
                LastSaveStatus = "Mutation skipped; no profile loaded";
                return false;
            }

            var changed = mutation(Profile);
            if (!changed)
            {
                return false;
            }

            TutorialService.SyncProgress(Profile);
            return SaveProfile(reason);
        }

        public PlayerProfileData ReloadProfile()
        {
            if (!File.Exists(SaveFilePath))
            {
                LastLoadStatus = "Reload skipped; no save file exists";
                return Profile;
            }

            try
            {
                var json = File.ReadAllText(SaveFilePath);
                Profile = ProfileJsonSerializer.FromJson(json);
                EnsureProfileShape(Profile != null && Profile.account != null ? Profile.account.uid : "");
                CheckVersionAndMigrateIfNeeded();
                LastLoadStatus = "Reloaded save file";
            }
            catch (Exception exception)
            {
                LastLoadStatus = "Reload failed; keeping in-memory profile: " + exception.Message;
            }

            return Profile;
        }

        private void EnsureProfileShape(string uid)
        {
            if (Profile == null)
            {
                Profile = ProfileFactory.CreateDefault(uid);
            }

            if (Profile.save_version == null)
            {
                Profile.save_version = new SaveVersionData();
            }

            if (Profile.account == null)
            {
                Profile.account = new AccountData();
            }

            if (Profile.wallet == null)
            {
                Profile.wallet = new WalletData();
            }

            if (Profile.burrow_state == null)
            {
                Profile.burrow_state = new BurrowStateData();
            }

            BurrowStateHelper.EnsureDefaults(Profile);
            ExplorationStateHelper.EnsureDefaults(Profile);
            FixtureStateHelper.EnsureDefaults(Profile);
            FixtureStateHelper.ApplyMilestoneUnlocks(Profile);
            SocialProgressService.EnsureDefaults(Profile);
            LuckyDrawEventService.EnsureDefaults(Profile);
            new LuckyDrawEventService().SyncUnlock(Profile);
            CrackCliqueService.EnsureDefaults(Profile);
            new CrackCliqueService().SyncUnlocks(Profile);
            TutorialService.EnsureDefaults(Profile);
            TutorialService.SyncProgress(Profile);

            if (string.IsNullOrEmpty(Profile.account.uid))
            {
                Profile.account.uid = string.IsNullOrEmpty(uid) ? ProfileFactory.CreateLocalUid() : uid;
            }

            if (string.IsNullOrEmpty(Profile.account.display_name))
            {
                Profile.account.display_name = "Local Gnome";
            }

            if (string.IsNullOrEmpty(Profile.account.created_at))
            {
                Profile.account.created_at = ProfileFactory.UtcNowIso();
            }

            if (string.IsNullOrEmpty(Profile.account.tutorial_state))
            {
                Profile.account.tutorial_state = "not_started";
            }
        }

        private void CheckVersionAndMigrateIfNeeded()
        {
            if (Profile.save_version.version <= 0)
            {
                Profile.save_version.version = ProfileFactory.CurrentSaveVersion;
            }

            if (Profile.save_version.version < ProfileFactory.CurrentSaveVersion)
            {
                // Sprint 7 and earlier migrations are additive; rehydrate all prototype state on load.
                BurrowStateHelper.EnsureDefaults(Profile);
                ExplorationStateHelper.EnsureDefaults(Profile);
                FixtureStateHelper.EnsureDefaults(Profile);
                FixtureStateHelper.ApplyMilestoneUnlocks(Profile);
                SocialProgressService.EnsureDefaults(Profile);
                LuckyDrawEventService.EnsureDefaults(Profile);
                new LuckyDrawEventService().SyncUnlock(Profile);
                CrackCliqueService.EnsureDefaults(Profile);
                new CrackCliqueService().SyncUnlocks(Profile);
                TutorialService.EnsureDefaults(Profile);
                TutorialService.SyncProgress(Profile);
                Profile.save_version.version = ProfileFactory.CurrentSaveVersion;
            }
        }

        private void BackupUnreadableSave()
        {
            if (!File.Exists(SaveFilePath))
            {
                return;
            }

            var backupPath = SaveFilePath + ".unreadable." + DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            File.Copy(SaveFilePath, backupPath, true);
        }

        private static void WriteAtomic(string path, string contents)
        {
            var tempPath = path + ".tmp";
            File.WriteAllText(tempPath, contents);

            if (File.Exists(path))
            {
                try
                {
                    File.Replace(tempPath, path, null);
                    return;
                }
                catch
                {
                    File.Delete(path);
                }
            }

            File.Move(tempPath, path);
        }
    }
}
