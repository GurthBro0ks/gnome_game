using GnomeGame.Core;
using GnomeGame.Debugging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GnomeGame.UI
{
    public class BurrowDebugUI : MonoBehaviour
    {
        private ProfileService profileService;
        private SaveManager saveManager;
        private AuthManager authManager;

        private Text mooncapsText;
        private Text statusText;

        public void Initialize(ProfileService activeProfileService, SaveManager activeSaveManager, AuthManager activeAuthManager)
        {
            profileService = activeProfileService;
            saveManager = activeSaveManager;
            authManager = activeAuthManager;

            BuildUi();
            profileService.ProfileChanged += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            if (profileService != null)
            {
                profileService.ProfileChanged -= Refresh;
            }
        }

        private void BuildUi()
        {
            EnsureEventSystem();

            var font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (font == null)
            {
                font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }

            var canvasObject = new GameObject("Sprint0Canvas", typeof(RectTransform));
            canvasObject.transform.SetParent(transform, false);

            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);
            scaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();

            var root = CreatePanel(canvasObject.transform, "DebugStatusPanel", new Color(0.08f, 0.1f, 0.09f, 0.96f));
            var rootRect = root.GetComponent<RectTransform>();
            rootRect.anchorMin = new Vector2(0.05f, 0.08f);
            rootRect.anchorMax = new Vector2(0.95f, 0.92f);
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            var layout = root.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(28, 28, 28, 28);
            layout.spacing = 18;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            AddText(root.transform, font, "The Burrow", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 58);
            mooncapsText = AddText(root.transform, font, "Mooncaps: 0", 30, FontStyle.Bold, TextAnchor.MiddleLeft, 48);

            AddButton(root.transform, font, "Gather +10 Mooncaps", () => profileService.GatherMooncaps(10));
            AddButton(root.transform, font, "Force Save", () => profileService.ForceSave());
            AddButton(root.transform, font, "Reload Save", () => profileService.ReloadSave());

            statusText = AddText(root.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 360);
        }

        private void Refresh()
        {
            var snapshot = DebugStatusSnapshot.From(authManager, saveManager, profileService.Profile);

            mooncapsText.text = "Mooncaps: " + snapshot.mooncaps;
            statusText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Active UID: " + snapshot.active_uid + "\n" +
                "Save file path: " + snapshot.save_file_path;
        }

        private static GameObject CreatePanel(Transform parent, string name, Color color)
        {
            var panel = new GameObject(name, typeof(RectTransform));
            panel.transform.SetParent(parent, false);

            var image = panel.AddComponent<Image>();
            image.color = color;

            return panel;
        }

        private static Text AddText(Transform parent, Font font, string value, int size, FontStyle style, TextAnchor alignment, float height)
        {
            var textObject = new GameObject(value, typeof(RectTransform));
            textObject.transform.SetParent(parent, false);

            var text = textObject.AddComponent<Text>();
            text.text = value;
            text.font = font;
            text.fontSize = size;
            text.fontStyle = style;
            text.alignment = alignment;
            text.color = Color.white;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            var rect = textObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, height);

            var layout = textObject.AddComponent<LayoutElement>();
            layout.preferredHeight = height;

            return text;
        }

        private static void AddButton(Transform parent, Font font, string label, UnityEngine.Events.UnityAction onClick)
        {
            var buttonObject = CreatePanel(parent, label, new Color(0.18f, 0.23f, 0.2f, 1f));
            buttonObject.name = label;

            var button = buttonObject.AddComponent<Button>();
            button.onClick.AddListener(onClick);

            var colors = button.colors;
            colors.highlightedColor = new Color(0.24f, 0.32f, 0.27f, 1f);
            colors.pressedColor = new Color(0.12f, 0.16f, 0.14f, 1f);
            button.colors = colors;

            var rect = buttonObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 72f);

            var layout = buttonObject.AddComponent<LayoutElement>();
            layout.preferredHeight = 72f;

            var labelText = AddText(buttonObject.transform, font, label, 26, FontStyle.Bold, TextAnchor.MiddleCenter, 72f);
            var labelRect = labelText.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;
        }

        private static void EnsureEventSystem()
        {
            if (FindObjectOfType<EventSystem>() != null)
            {
                return;
            }

            var eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }
    }
}
