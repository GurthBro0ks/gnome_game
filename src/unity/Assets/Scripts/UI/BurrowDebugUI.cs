using GnomeGame.Core;
using GnomeGame.Debugging;
using GnomeGame.Data;
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

        private Text walletText;
        private Text actionText;
        private Text dewpondText;
        private Text mushpatchText;
        private Text burrowStatusText;
        private Text rootmineText;
        private Text debugText;

        private Button dewpondGatherButton;
        private Button mushpatchGatherButton;
        private Button expandButton;
        private Text expandButtonLabel;

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

            var canvasObject = new GameObject("Sprint1Canvas", typeof(RectTransform));
            canvasObject.transform.SetParent(transform, false);

            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);
            scaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();

            var root = CreatePanel(canvasObject.transform, "BurrowRoot", new Color(0.08f, 0.1f, 0.09f, 0.96f));
            var rootRect = root.GetComponent<RectTransform>();
            rootRect.anchorMin = new Vector2(0.04f, 0.04f);
            rootRect.anchorMax = new Vector2(0.96f, 0.96f);
            rootRect.offsetMin = Vector2.zero;
            rootRect.offsetMax = Vector2.zero;

            var rootLayout = root.AddComponent<VerticalLayoutGroup>();
            rootLayout.padding = new RectOffset(28, 28, 28, 28);
            rootLayout.spacing = 16;
            rootLayout.childControlWidth = true;
            rootLayout.childControlHeight = false;
            rootLayout.childForceExpandWidth = true;
            rootLayout.childForceExpandHeight = false;

            AddText(root.transform, font, "The Burrow", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 60f);
            walletText = AddText(root.transform, font, "", 28, FontStyle.Bold, TextAnchor.MiddleLeft, 44f);
            actionText = AddText(root.transform, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 58f);

            var dewpondCard = CreateCard(root.transform, "DewpondCard");
            dewpondText = AddText(dewpondCard.transform, font, "", 24, FontStyle.Normal, TextAnchor.UpperLeft, 120f);
            Text unusedButtonLabel;
            dewpondGatherButton = AddButton(dewpondCard.transform, font, "Gather Dewpond", () => profileService.GatherDewpond(), out unusedButtonLabel);

            var mushpatchCard = CreateCard(root.transform, "MushpatchCard");
            mushpatchText = AddText(mushpatchCard.transform, font, "", 24, FontStyle.Normal, TextAnchor.UpperLeft, 120f);
            mushpatchGatherButton = AddButton(mushpatchCard.transform, font, "Gather Mushpatch", () => profileService.GatherMushpatch(), out unusedButtonLabel);

            var burrowCard = CreateCard(root.transform, "BurrowStatusCard");
            burrowStatusText = AddText(burrowCard.transform, font, "", 24, FontStyle.Normal, TextAnchor.UpperLeft, 120f);
            expandButton = AddButton(burrowCard.transform, font, "Expand Burrow", () => profileService.ExpandBurrow(), out expandButtonLabel);

            var rootmineCard = CreateCard(root.transform, "RootmineCard");
            rootmineText = AddText(rootmineCard.transform, font, "", 24, FontStyle.Normal, TextAnchor.UpperLeft, 100f);

            var saveActionsCard = CreateCard(root.transform, "SaveActionsCard");
            AddText(saveActionsCard.transform, font, "Debug actions", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 36f);
            AddButton(saveActionsCard.transform, font, "Force Save", () => profileService.ForceSave(), out unusedButtonLabel);
            AddButton(saveActionsCard.transform, font, "Reload Save", () => profileService.ReloadSave(), out unusedButtonLabel);

            var debugPanel = CreatePanel(root.transform, "DebugPanel", new Color(0.13f, 0.15f, 0.14f, 1f));
            var debugLayout = debugPanel.AddComponent<VerticalLayoutGroup>();
            debugLayout.padding = new RectOffset(20, 20, 20, 20);
            debugLayout.spacing = 10;
            debugLayout.childControlWidth = true;
            debugLayout.childControlHeight = false;
            debugLayout.childForceExpandWidth = true;
            debugLayout.childForceExpandHeight = false;

            var debugRect = debugPanel.GetComponent<RectTransform>();
            debugRect.sizeDelta = new Vector2(0f, 270f);

            var debugElement = debugPanel.AddComponent<LayoutElement>();
            debugElement.preferredHeight = 270f;

            AddText(debugPanel.transform, font, "Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 36f);
            debugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 210f);
        }

        private void Refresh()
        {
            var profile = profileService.Profile;
            if (profile == null || profile.burrow_state == null || profile.wallet == null)
            {
                return;
            }

            var snapshot = DebugStatusSnapshot.From(authManager, saveManager, profile);
            var burrow = profile.burrow_state;
            var nextExpandCost = profileService.GetNextExpandCost();

            walletText.text = "Mooncaps: " + snapshot.mooncaps + "   Mushcaps: " + snapshot.mushcaps;
            actionText.text = profileService.LastActionStatus;

            dewpondText.text =
                "Dewpond\n" +
                "Level: " + burrow.dewpond.level + "\n" +
                "Stored output: " + burrow.dewpond.stored_output + " Mooncaps\n" +
                "Storage cap: " + burrow.dewpond.storage_cap;

            mushpatchText.text =
                "Mushpatch\n" +
                "Level: " + burrow.mushpatch.level + "\n" +
                "Stored output: " + burrow.mushpatch.stored_output + " Mushcaps\n" +
                "Storage cap: " + burrow.mushpatch.storage_cap;

            burrowStatusText.text =
                "Burrow status\n" +
                "Burrow level: " + burrow.burrow_level + "\n" +
                "Expand count: " + burrow.expand_count + "\n" +
                "Next expand cost: " + (nextExpandCost > 0 ? nextExpandCost + " Mooncaps" : "Sprint 1 stub after level 3") + "\n" +
                (nextExpandCost > 0 && profile.wallet.mooncaps < nextExpandCost
                    ? "Blocked: need " + nextExpandCost + " Mooncaps"
                    : "Ready");

            rootmineText.text =
                "Rootmine\n" +
                "Status: " + (burrow.rootmine.unlocked ? "Unlocked" : "Locked") + "\n" +
                "Level: " + burrow.rootmine.level + "\n" +
                burrow.rootmine.status_note;

            debugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Last production tick: " + snapshot.last_production_tick + "\n" +
                "Active UID: " + snapshot.active_uid + "\n" +
                "Save file path: " + snapshot.save_file_path;

            dewpondGatherButton.interactable = burrow.dewpond.stored_output > 0;
            mushpatchGatherButton.interactable = burrow.mushpatch.stored_output > 0;
            expandButton.interactable = nextExpandCost > 0 && profile.wallet.mooncaps >= nextExpandCost;
            expandButtonLabel.text = nextExpandCost > 0 ? "Expand Burrow (" + nextExpandCost + " Mooncaps)" : "Expand Stubbed";
        }

        private static GameObject CreateCard(Transform parent, string name)
        {
            var card = CreatePanel(parent, name, new Color(0.14f, 0.18f, 0.16f, 1f));
            var rect = card.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 220f);

            var layoutElement = card.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 220f;

            var layout = card.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(20, 20, 20, 20);
            layout.spacing = 12;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return card;
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
            var textObject = new GameObject("Text", typeof(RectTransform));
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

        private static Button AddButton(Transform parent, Font font, string label, UnityEngine.Events.UnityAction onClick, out Text labelText)
        {
            var buttonObject = CreatePanel(parent, label, new Color(0.18f, 0.23f, 0.2f, 1f));
            var button = buttonObject.AddComponent<Button>();
            button.onClick.AddListener(onClick);

            var colors = button.colors;
            colors.highlightedColor = new Color(0.24f, 0.32f, 0.27f, 1f);
            colors.pressedColor = new Color(0.12f, 0.16f, 0.14f, 1f);
            colors.disabledColor = new Color(0.14f, 0.16f, 0.15f, 0.8f);
            button.colors = colors;

            var rect = buttonObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 70f);

            var layout = buttonObject.AddComponent<LayoutElement>();
            layout.preferredHeight = 70f;

            labelText = AddText(buttonObject.transform, font, label, 24, FontStyle.Bold, TextAnchor.MiddleCenter, 70f);
            var labelRect = labelText.GetComponent<RectTransform>();
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;

            return button;
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
