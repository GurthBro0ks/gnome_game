using GnomeGame.UI;
using UnityEngine;

namespace GnomeGame.Core
{
    public class AppBootstrap : MonoBehaviour
    {
        [Header("Sprint 0 Auth Stub")]
        [SerializeField]
        private bool auth_enabled = false;

        [SerializeField]
        private bool simulate_auth_failure = false;

        private AuthManager authManager;
        private SaveManager saveManager;
        private ProfileService profileService;

        private void Awake()
        {
            EnsureCamera();

            authManager = new AuthManager();
            saveManager = new SaveManager(Application.persistentDataPath);
            profileService = new ProfileService();

            authManager.Initialize(Application.persistentDataPath, auth_enabled, simulate_auth_failure);

            var profile = saveManager.LoadOrCreateProfile(authManager.ActiveUid);
            authManager.AdoptProfileUidIfFallback(profile.account.uid);

            profileService.Initialize(saveManager);
            CreateDebugUi();

            Debug.Log("[Sprint0] Boot complete. " + authManager.StatusMessage);
            Debug.Log("[Sprint0] Save path: " + saveManager.SaveFilePath);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus && saveManager != null)
            {
                saveManager.SaveProfile("application pause");
            }
        }

        private void OnApplicationQuit()
        {
            if (saveManager != null)
            {
                saveManager.SaveProfile("application quit");
            }
        }

        private void CreateDebugUi()
        {
            var uiObject = new GameObject("BurrowDebugUI");
            uiObject.transform.SetParent(transform, false);

            var ui = uiObject.AddComponent<BurrowDebugUI>();
            ui.Initialize(profileService, saveManager, authManager);
        }

        private static void EnsureCamera()
        {
            if (Camera.main != null)
            {
                return;
            }

            var cameraObject = new GameObject("Main Camera");
            var camera = cameraObject.AddComponent<Camera>();
            camera.tag = "MainCamera";
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.05f, 0.07f, 0.06f, 1f);
        }
    }
}
