using System;
using System.IO;
using GnomeGame.Data;

namespace GnomeGame.Core
{
    public enum AuthInitState
    {
        NotStarted,
        Disabled,
        Fallback,
        Failed
    }

    public class AuthManager
    {
        private const string LocalUidFileName = "local_uid.txt";

        private string persistentDataPath = "";

        public AuthInitState State { get; private set; } = AuthInitState.NotStarted;
        public string ActiveUid { get; private set; } = "";
        public string StatusMessage { get; private set; } = "Auth not initialized";
        public bool IsFallback => State == AuthInitState.Disabled || State == AuthInitState.Fallback || State == AuthInitState.Failed;

        public void Initialize(string appPersistentDataPath, bool authEnabled, bool simulateAuthFailure)
        {
            persistentDataPath = appPersistentDataPath;

            try
            {
                if (!authEnabled)
                {
                    UseLocalFallback(AuthInitState.Disabled, "Auth disabled; using local fallback UID");
                    return;
                }

                if (simulateAuthFailure)
                {
                    throw new InvalidOperationException("Simulated auth failure");
                }

                UseLocalFallback(AuthInitState.Fallback, "Firebase/Auth unavailable in Sprint 0; using local fallback UID");
            }
            catch (Exception exception)
            {
                UseLocalFallback(AuthInitState.Failed, "Auth failed; using local fallback UID: " + exception.Message);
            }
        }

        public void AdoptProfileUidIfFallback(string profileUid)
        {
            if (!IsFallback || string.IsNullOrEmpty(profileUid) || profileUid == ActiveUid)
            {
                return;
            }

            ActiveUid = profileUid;
            WriteLocalUid(profileUid);
            StatusMessage += " (adopted saved profile UID)";
        }

        private void UseLocalFallback(AuthInitState state, string message)
        {
            State = state;
            ActiveUid = ReadOrCreateLocalUid();
            StatusMessage = message;
        }

        private string ReadOrCreateLocalUid()
        {
            var path = GetLocalUidPath();

            if (File.Exists(path))
            {
                var uid = File.ReadAllText(path).Trim();
                if (!string.IsNullOrEmpty(uid))
                {
                    return uid;
                }
            }

            var createdUid = ProfileFactory.CreateLocalUid();
            WriteLocalUid(createdUid);
            return createdUid;
        }

        private void WriteLocalUid(string uid)
        {
            Directory.CreateDirectory(persistentDataPath);
            File.WriteAllText(GetLocalUidPath(), uid);
        }

        private string GetLocalUidPath()
        {
            return Path.Combine(persistentDataPath, LocalUidFileName);
        }
    }
}
