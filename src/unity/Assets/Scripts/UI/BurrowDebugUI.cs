using GnomeGame.Core;
using GnomeGame.Data;
using GnomeGame.Debugging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GnomeGame.UI
{
    public class BurrowDebugUI : MonoBehaviour
    {
        private enum ScreenPage
        {
            Burrow,
            Loamwake,
            FixtureWorkshop,
            Vault,
            BurrowPosts,
            DailyDuties,
            Rootrail,
            LuckyDrawWeek,
            Crack,
            Clique
        }

        private ProfileService profileService;
        private SaveManager saveManager;
        private AuthManager authManager;

        private ScreenPage currentScreen = ScreenPage.Burrow;
        private bool debugExpanded;

        private GameObject burrowPage;
        private GameObject loamwakePage;
        private GameObject fixturePage;
        private GameObject vaultPage;
        private GameObject burrowPostsPage;
        private GameObject dailyDutiesPage;
        private GameObject rootrailPage;
        private GameObject luckyDrawPage;
        private GameObject crackPage;
        private GameObject cliquePage;

        private Text burrowWalletText;
        private Text burrowActionText;
        private Text dewpondText;
        private Text mushpatchText;
        private Text burrowStatusText;
        private Text rootmineText;
        private Text fixtureSummaryText;
        private Text socialSummaryText;
        private Text fieldReturnsSnippetText;
        private Text burrowDebugText;

        private Text loamwakeWalletText;
        private Text loamwakeActionText;
        private Text zone1Text;
        private Text zone2Text;
        private Text zone3Text;
        private Text keeperText;
        private Text loamwakeFieldReturnsText;
        private Text loamwakeDebugText;
        private Text fixtureWalletText;
        private Text fixtureActionText;
        private Text fixtureCraftText;
        private Text fixtureInventoryText;
        private Text fixtureHatText;
        private Text fixtureDebugText;
        private Text vaultStatusText;
        private Text vaultDebugText;
        private Text burrowPostsStatusText;
        private Text burrowPostsDebugText;
        private Text dailyDutiesStatusText;
        private Text dailyDutiesDebugText;
        private Text rootrailStatusText;
        private Text rootrailDebugText;
        private Text luckyDrawStatusText;
        private Text luckyDrawLedgerText;
        private Text luckyDrawStallText;
        private Text luckyDrawDebugText;
        private Text crackStatusText;
        private Text crackDebugText;
        private Text cliqueStatusText;
        private Text cliqueDebugText;

        private Text enterLoamwakeButtonLabel;
        private Text ledgerhollowButtonLabel;
        private Text memoryFenButtonLabel;

        private Button dewpondGatherButton;
        private Button mushpatchGatherButton;
        private Button rootmineGatherButton;
        private Button expandButton;
        private Button openFixtureButton;
        private Button openVaultButton;
        private Button openBurrowPostsButton;
        private Button openDailyDutiesButton;
        private Button openRootrailButton;
        private Button openLuckyDrawButton;
        private Button enterLoamwakeButton;
        private Button ledgerhollowButton;
        private Button memoryFenButton;
        private Button zone1SafeButton;
        private Button zone1RiskyButton;
        private Button zone2SafeButton;
        private Button zone2RiskyButton;
        private Button zone3SafeButton;
        private Button zone3RiskyButton;
        private Button keeperButton;
        private Button unlockFixtureCapButton;
        private Button craftFirstFixtureButton;
        private Button equipFirstFixtureButton;
        private Button unequipFirstFixtureButton;
        private Button setFirstHatVisibleButton;
        private Button readUtilityPostButton;
        private Button readGretaIntroPostButton;
        private Button completeGretaFollowupButton;
        private Button completeRootrailRevealButton;
        private Button claimLuckyDrawWeeklyButton;
        private Button pullLuckyDrawButton;
        private Button claimFestivalLedgerButton;
        private Button buyMooncapDrawButton;
        private Button buyMaterialCacheButton;
        private Button buyPolishBundleButton;
        private Button buyFestivalExchangeButton;
        private Button probeCrackButton;
        private Button claimCliqueStipendButton;

        private Text expandButtonLabel;
        private Text rootmineGatherLabel;
        private Text zone1SafeLabel;
        private Text zone1RiskyLabel;
        private Text zone2SafeLabel;
        private Text zone2RiskyLabel;
        private Text zone3SafeLabel;
        private Text zone3RiskyLabel;
        private Text keeperButtonLabel;
        private Text unlockFixtureCapLabel;
        private Text craftFirstFixtureLabel;
        private Text equipFirstFixtureLabel;
        private Text unequipFirstFixtureLabel;
        private Text setFirstHatVisibleLabel;
        private Text readUtilityPostLabel;
        private Text readGretaIntroPostLabel;
        private Text completeGretaFollowupLabel;
        private Text completeRootrailRevealLabel;
        private Text openLuckyDrawLabel;
        private Text claimLuckyDrawWeeklyLabel;
        private Text pullLuckyDrawLabel;
        private Text claimFestivalLedgerLabel;
        private Text buyMooncapDrawLabel;
        private Text buyMaterialCacheLabel;
        private Text buyPolishBundleLabel;
        private Text buyFestivalExchangeLabel;
        private Text probeCrackLabel;
        private Text claimCliqueStipendLabel;

        private GameObject burrowDebugPanel;
        private GameObject loamwakeDebugPanel;
        private GameObject fixtureDebugPanel;
        private GameObject vaultDebugPanel;
        private GameObject burrowPostsDebugPanel;
        private GameObject dailyDutiesDebugPanel;
        private GameObject rootrailDebugPanel;
        private GameObject luckyDrawDebugPanel;
        private GameObject crackDebugPanel;
        private GameObject cliqueDebugPanel;

        private Button debugToggleBurrowButton;
        private Text debugToggleBurrowLabel;

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

            var canvasObject = new GameObject("Phase4ACanvas", typeof(RectTransform));
            canvasObject.transform.SetParent(transform, false);

            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080f, 1920f);
            scaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();

            burrowPage = CreatePage(canvasObject.transform, "BurrowPage");
            loamwakePage = CreatePage(canvasObject.transform, "LoamwakePage");
            fixturePage = CreatePage(canvasObject.transform, "FixtureWorkshopPage");
            vaultPage = CreatePage(canvasObject.transform, "VaultPage");
            burrowPostsPage = CreatePage(canvasObject.transform, "BurrowPostsPage");
            dailyDutiesPage = CreatePage(canvasObject.transform, "DailyDutiesPage");
            rootrailPage = CreatePage(canvasObject.transform, "RootrailPage");
            luckyDrawPage = CreatePage(canvasObject.transform, "LuckyDrawWeekPage");
            crackPage = CreatePage(canvasObject.transform, "CrackPage");
            cliquePage = CreatePage(canvasObject.transform, "CliquePage");

            BuildBurrowPage(font);
            BuildLoamwakePage(font);
            BuildFixturePage(font);
            BuildVaultPage(font);
            BuildBurrowPostsPage(font);
            BuildDailyDutiesPage(font);
            BuildRootrailPage(font);
            BuildLuckyDrawPage(font);
            BuildCrackPage(font);
            BuildCliquePage(font);
        }

        private void BuildBurrowPage(Font font)
        {
            Text unusedLabel;

            var headerCard = CreateCard(burrowPage.transform, "BurrowFixedHeader", 460f);
            AddSectionTitle(headerCard.transform, font, "The Burrow");
            burrowWalletText = AddText(headerCard.transform, font, "", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            burrowActionText = AddText(headerCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.MiddleLeft, 56f);

            var navRow1 = CreateButtonRow(headerCard.transform, "BurrowNavRow1", 52f);
            enterLoamwakeButton = AddButton(navRow1.transform, font, "Enter Loamwake", OnEnterLoamwakePressed, out enterLoamwakeButtonLabel);
            openBurrowPostsButton = AddButton(navRow1.transform, font, "Burrow Post", OnOpenBurrowPostsPressed, out unusedLabel);
            openDailyDutiesButton = AddButton(navRow1.transform, font, "Daily Duties", OnOpenDailyDutiesPressed, out unusedLabel);

            var navRow2 = CreateButtonRow(headerCard.transform, "BurrowNavRow2", 52f);
            openRootrailButton = AddButton(navRow2.transform, font, "Rootrail Station", OnOpenRootrailPressed, out unusedLabel);
            openLuckyDrawButton = AddButton(navRow2.transform, font, "Lucky Draw Week", OnOpenLuckyDrawPressed, out openLuckyDrawLabel);
            openFixtureButton = AddButton(navRow2.transform, font, "Fixture Workshop", OnOpenFixturePressed, out unusedLabel);

            var navRow3 = CreateButtonRow(headerCard.transform, "BurrowNavRow3", 52f);
            openVaultButton = AddButton(navRow3.transform, font, "Vault of Treasures", OnOpenVaultPressed, out unusedLabel);
            ledgerhollowButton = AddButton(navRow3.transform, font, "Ledgerhollow (Unavailable)", OnOpenCrackPressed, out ledgerhollowButtonLabel);
            memoryFenButton = AddButton(navRow3.transform, font, "Memory Fen (Unavailable)", OnOpenCliquePressed, out memoryFenButtonLabel);

            var debugToggleRow = CreateButtonRow(headerCard.transform, "DebugToggleRow", 44f);
            debugToggleBurrowButton = AddButton(debugToggleRow.transform, font, "Debug: Show", OnToggleDebugPressed, out debugToggleBurrowLabel);

            var body = CreateScrollBody(burrowPage.transform, "BurrowScrollableBody");

            var dewpondCard = CreateCard(body, "DewpondCard", 140f);
            AddSectionTitle(dewpondCard.transform, font, "Dewpond");
            dewpondText = AddText(dewpondCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 54f);
            dewpondGatherButton = AddButton(dewpondCard.transform, font, "Gather Dewpond", OnGatherDewpondPressed, out unusedLabel);

            var mushpatchCard = CreateCard(body, "MushpatchCard", 140f);
            AddSectionTitle(mushpatchCard.transform, font, "Mushpatch");
            mushpatchText = AddText(mushpatchCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 54f);
            mushpatchGatherButton = AddButton(mushpatchCard.transform, font, "Gather Mushpatch", OnGatherMushpatchPressed, out unusedLabel);

            var burrowCard = CreateCard(body, "BurrowStatusCard", 160f);
            AddSectionTitle(burrowCard.transform, font, "Burrow Status");
            burrowStatusText = AddText(burrowCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 66f);
            expandButton = AddButton(burrowCard.transform, font, "Expand Burrow", OnExpandPressed, out expandButtonLabel);

            var rootmineCard = CreateCard(body, "RootmineCard", 160f);
            AddSectionTitle(rootmineCard.transform, font, "Rootmine");
            rootmineText = AddText(rootmineCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 76f);
            rootmineGatherButton = AddButton(rootmineCard.transform, font, "Gather Rootmine", OnGatherRootminePressed, out rootmineGatherLabel);

            var fixtureSummaryCard = CreateCard(body, "FixtureSummaryCard", 100f);
            AddSectionTitle(fixtureSummaryCard.transform, font, "Fixture / Hat Summary");
            fixtureSummaryText = AddText(fixtureSummaryCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 48f);

            var socialCard = CreateCard(body, "SocialSummaryCard", 120f);
            AddSectionTitle(socialCard.transform, font, "Social Progress");
            socialSummaryText = AddText(socialCard.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 70f);

            var returnsCard = CreateCard(body, "FieldReturnsCard", 100f);
            AddSectionTitle(returnsCard.transform, font, "Field Returns");
            fieldReturnsSnippetText = AddText(returnsCard.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 52f);

            burrowDebugPanel = CreateCard(body, "BurrowDebugPanel", 260f);
            AddSectionTitle(burrowDebugPanel.transform, font, "Debug / Status");
            AddButton(burrowDebugPanel.transform, font, "Reset Guide", OnResetTutorialGuidePressed, out unusedLabel);
            AddButton(burrowDebugPanel.transform, font, "Force Save", OnForceSavePressed, out unusedLabel);
            AddButton(burrowDebugPanel.transform, font, "Reload Save", OnReloadPressed, out unusedLabel);
            burrowDebugText = AddText(burrowDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 80f);
            burrowDebugPanel.SetActive(false);
        }

        private void BuildLoamwakePage(Font font)
        {
            CreateNavHeader(loamwakePage.transform, font, "Loamwake", out var _);

            var body = CreateScrollBody(loamwakePage.transform, "LoamwakeScrollableBody");

            loamwakeWalletText = AddText(body, font, "", 24, FontStyle.Bold, TextAnchor.MiddleLeft, 40f);
            loamwakeActionText = AddText(body, font, "", 18, FontStyle.Normal, TextAnchor.MiddleLeft, 60f);

            var zone1Card = CreateCard(body, "Zone1Card", 250f);
            AddSectionTitle(zone1Card.transform, font, "Rootvine Shelf");
            zone1Text = AddText(zone1Card.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 80f);
            var z1Row = CreateButtonRow(zone1Card.transform, "Zone1RouteRow", 48f);
            zone1SafeButton = AddButton(z1Row.transform, font, "Safe Route", OnZone1SafePressed, out zone1SafeLabel);
            zone1RiskyButton = AddButton(z1Row.transform, font, "Risky Route", OnZone1RiskyPressed, out zone1RiskyLabel);

            var zone2Card = CreateCard(body, "Zone2Card", 250f);
            AddSectionTitle(zone2Card.transform, font, "Mudpipe Hollow");
            zone2Text = AddText(zone2Card.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 80f);
            var z2Row = CreateButtonRow(zone2Card.transform, "Zone2RouteRow", 48f);
            zone2SafeButton = AddButton(z2Row.transform, font, "Safe Route", OnZone2SafePressed, out zone2SafeLabel);
            zone2RiskyButton = AddButton(z2Row.transform, font, "Risky Route", OnZone2RiskyPressed, out zone2RiskyLabel);

            var zone3Card = CreateCard(body, "Zone3Card", 250f);
            AddSectionTitle(zone3Card.transform, font, "Glowroot Passage");
            zone3Text = AddText(zone3Card.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 80f);
            var z3Row = CreateButtonRow(zone3Card.transform, "Zone3RouteRow", 48f);
            zone3SafeButton = AddButton(z3Row.transform, font, "Safe Route", OnZone3SafePressed, out zone3SafeLabel);
            zone3RiskyButton = AddButton(z3Row.transform, font, "Risky Route", OnZone3RiskyPressed, out zone3RiskyLabel);

            var keeperCard = CreateCard(body, "KeeperCard", 200f);
            AddSectionTitle(keeperCard.transform, font, "First Warden: The Mudgrip");
            keeperText = AddText(keeperCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 90f);
            keeperButton = AddButton(keeperCard.transform, font, "Challenge Warden", OnKeeperPressed, out keeperButtonLabel);

            var returnsCard = CreateCard(body, "LoamwakeReturnsCard", 160f);
            AddSectionTitle(returnsCard.transform, font, "Field Returns");
            loamwakeFieldReturnsText = AddText(returnsCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 100f);

            loamwakeDebugPanel = CreateCard(body, "LoamwakeDebugPanel", 260f);
            AddSectionTitle(loamwakeDebugPanel.transform, font, "Debug / Status");
            loamwakeDebugText = AddText(loamwakeDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 180f);
            loamwakeDebugPanel.SetActive(false);
        }

        private void BuildFixturePage(Font font)
        {
            CreateNavHeader(fixturePage.transform, font, "Fixture Workshop", out var _);

            var body = CreateScrollBody(fixturePage.transform, "FixtureScrollableBody");

            fixtureWalletText = AddText(body, font, "", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 48f);
            fixtureActionText = AddText(body, font, "", 18, FontStyle.Normal, TextAnchor.MiddleLeft, 60f);

            var craftCard = CreateCard(body, "FirstFixtureCraftCard", 280f);
            AddSectionTitle(craftCard.transform, font, "Craft First Fixture");
            fixtureCraftText = AddText(craftCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 100f);
            unlockFixtureCapButton = AddButton(craftCard.transform, font, "Unlock First Fixture Cap", OnUnlockFixtureCapPressed, out unlockFixtureCapLabel);
            craftFirstFixtureButton = AddButton(craftCard.transform, font, "Craft Root-Bitten Shovel Strap", OnCraftFirstFixturePressed, out craftFirstFixtureLabel);

            var inventoryCard = CreateCard(body, "FixtureInventoryCard", 280f);
            AddSectionTitle(inventoryCard.transform, font, "Fixture Inventory");
            fixtureInventoryText = AddText(inventoryCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 100f);
            equipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Equip First Fixture", OnEquipFirstFixturePressed, out equipFirstFixtureLabel);
            unequipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Unequip First Fixture", OnUnequipFirstFixturePressed, out unequipFirstFixtureLabel);

            var hatCard = CreateCard(body, "HatShellCard", 220f);
            AddSectionTitle(hatCard.transform, font, "Hat Collection");
            fixtureHatText = AddText(hatCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 100f);
            setFirstHatVisibleButton = AddButton(hatCard.transform, font, "Set Visible Hat", OnSetFirstHatVisiblePressed, out setFirstHatVisibleLabel);

            fixtureDebugPanel = CreateCard(body, "FixtureDebugPanel", 220f);
            AddSectionTitle(fixtureDebugPanel.transform, font, "Debug / Status");
            fixtureDebugText = AddText(fixtureDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            fixtureDebugPanel.SetActive(false);
        }

        private void BuildVaultPage(Font font)
        {
            CreateNavHeader(vaultPage.transform, font, "Vault of Treasures", out var _);

            var body = CreateScrollBody(vaultPage.transform, "VaultScrollableBody");

            var shellCard = CreateCard(body, "VaultShellCard", 280f);
            AddSectionTitle(shellCard.transform, font, "Treasure Vault");
            vaultStatusText = AddText(shellCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 200f);

            vaultDebugPanel = CreateCard(body, "VaultDebugPanel", 220f);
            AddSectionTitle(vaultDebugPanel.transform, font, "Debug / Status");
            vaultDebugText = AddText(vaultDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            vaultDebugPanel.SetActive(false);
        }

        private void BuildBurrowPostsPage(Font font)
        {
            CreateNavHeader(burrowPostsPage.transform, font, "Burrow Post", out var _);

            var body = CreateScrollBody(burrowPostsPage.transform, "BurrowPostsScrollableBody");

            var postCard = CreateCard(body, "BurrowPostsCard", 340f);
            AddSectionTitle(postCard.transform, font, "Available Posts");
            burrowPostsStatusText = AddText(postCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            readUtilityPostButton = AddButton(postCard.transform, font, "Read Root Twine Run", OnReadUtilityPostPressed, out readUtilityPostLabel);
            readGretaIntroPostButton = AddButton(postCard.transform, font, "Read Greta Intro Post", OnReadGretaIntroPostPressed, out readGretaIntroPostLabel);

            var gretaCard = CreateCard(body, "GretaFollowupCard", 200f);
            AddSectionTitle(gretaCard.transform, font, "Greta");
            completeGretaFollowupButton = AddButton(gretaCard.transform, font, "Complete Greta Follow-Up", OnCompleteGretaFollowupPressed, out completeGretaFollowupLabel);
            completeRootrailRevealButton = AddButton(gretaCard.transform, font, "Follow Greta to the Old Track", OnCompleteRootrailRevealPressed, out completeRootrailRevealLabel);

            burrowPostsDebugPanel = CreateCard(body, "BurrowPostsDebugPanel", 220f);
            AddSectionTitle(burrowPostsDebugPanel.transform, font, "Debug / Status");
            burrowPostsDebugText = AddText(burrowPostsDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            burrowPostsDebugPanel.SetActive(false);
        }

        private void BuildDailyDutiesPage(Font font)
        {
            CreateNavHeader(dailyDutiesPage.transform, font, "Daily Duties", out var _);

            var body = CreateScrollBody(dailyDutiesPage.transform, "DailyDutiesScrollableBody");

            var dutiesCard = CreateCard(body, "DailyDutiesCard", 400f);
            AddSectionTitle(dutiesCard.transform, font, "Active Duties");
            dailyDutiesStatusText = AddText(dutiesCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 340f);

            dailyDutiesDebugPanel = CreateCard(body, "DailyDutiesDebugPanel", 220f);
            AddSectionTitle(dailyDutiesDebugPanel.transform, font, "Debug / Status");
            dailyDutiesDebugText = AddText(dailyDutiesDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            dailyDutiesDebugPanel.SetActive(false);
        }

        private void BuildRootrailPage(Font font)
        {
            CreateNavHeader(rootrailPage.transform, font, "Rootrail Station", out var _);

            var body = CreateScrollBody(rootrailPage.transform, "RootrailScrollableBody");

            var stationCard = CreateCard(body, "RootrailStationShellCard", 360f);
            AddSectionTitle(stationCard.transform, font, "Loamwake Terminal");
            rootrailStatusText = AddText(stationCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 280f);

            rootrailDebugPanel = CreateCard(body, "RootrailDebugPanel", 220f);
            AddSectionTitle(rootrailDebugPanel.transform, font, "Debug / Status");
            rootrailDebugText = AddText(rootrailDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            rootrailDebugPanel.SetActive(false);
        }

        private void BuildLuckyDrawPage(Font font)
        {
            CreateNavHeader(luckyDrawPage.transform, font, "Lucky Draw Week", out var _);

            var body = CreateScrollBody(luckyDrawPage.transform, "LuckyDrawScrollableBody");

            var statusCard = CreateCard(body, "LuckyDrawStatusCard", 340f);
            AddSectionTitle(statusCard.transform, font, "Loose Lots");
            luckyDrawStatusText = AddText(statusCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            claimLuckyDrawWeeklyButton = AddButton(statusCard.transform, font, "Claim Weekly Lucky Draw", OnClaimLuckyDrawWeeklyPressed, out claimLuckyDrawWeeklyLabel);
            pullLuckyDrawButton = AddButton(statusCard.transform, font, "Pull Lucky Draw", OnPullLuckyDrawPressed, out pullLuckyDrawLabel);

            var ledgerCard = CreateCard(body, "FestivalLedgerFreeLaneCard", 300f);
            AddSectionTitle(ledgerCard.transform, font, "Festival Ledger");
            luckyDrawLedgerText = AddText(ledgerCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 200f);
            claimFestivalLedgerButton = AddButton(ledgerCard.transform, font, "Claim Festival Ledger Reward", OnClaimFestivalLedgerPressed, out claimFestivalLedgerLabel);

            var stallCard = CreateCard(body, "LuckyStallCard", 420f);
            AddSectionTitle(stallCard.transform, font, "Lucky Stall");
            luckyDrawStallText = AddText(stallCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 170f);
            buyMooncapDrawButton = AddButton(stallCard.transform, font, "Buy Mooncap Lucky Draw", OnBuyMooncapDrawPressed, out buyMooncapDrawLabel);
            buyMaterialCacheButton = AddButton(stallCard.transform, font, "Buy Material Cache", OnBuyMaterialCachePressed, out buyMaterialCacheLabel);
            buyPolishBundleButton = AddButton(stallCard.transform, font, "Buy Polish Bundle", OnBuyPolishBundlePressed, out buyPolishBundleLabel);
            buyFestivalExchangeButton = AddButton(stallCard.transform, font, "Buy Festival Marks Exchange", OnBuyFestivalExchangePressed, out buyFestivalExchangeLabel);

            luckyDrawDebugPanel = CreateCard(body, "LuckyDrawDebugPanel", 220f);
            AddSectionTitle(luckyDrawDebugPanel.transform, font, "Debug / Status");
            luckyDrawDebugText = AddText(luckyDrawDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            luckyDrawDebugPanel.SetActive(false);
        }

        private void BuildCrackPage(Font font)
        {
            CreateNavHeader(crackPage.transform, font, "The Crack", out var _);

            var body = CreateScrollBody(crackPage.transform, "CrackScrollableBody");

            var statusCard = CreateCard(body, "CrackProbeCard", 420f);
            AddSectionTitle(statusCard.transform, font, "Endless Descent");
            crackStatusText = AddText(statusCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 280f);
            probeCrackButton = AddButton(statusCard.transform, font, "Probe the Crack", OnProbeCrackPressed, out probeCrackLabel);

            crackDebugPanel = CreateCard(body, "CrackDebugPanel", 220f);
            AddSectionTitle(crackDebugPanel.transform, font, "Debug / Status");
            crackDebugText = AddText(crackDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            crackDebugPanel.SetActive(false);
        }

        private void BuildCliquePage(Font font)
        {
            CreateNavHeader(cliquePage.transform, font, "Clique", out var _);

            var body = CreateScrollBody(cliquePage.transform, "CliqueScrollableBody");

            var statusCard = CreateCard(body, "CliqueShellCard", 560f);
            AddSectionTitle(statusCard.transform, font, "Local Clique");
            cliqueStatusText = AddText(statusCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 420f);
            claimCliqueStipendButton = AddButton(statusCard.transform, font, "Claim Local Clique Stipend", OnClaimCliqueStipendPressed, out claimCliqueStipendLabel);

            cliqueDebugPanel = CreateCard(body, "CliqueDebugPanel", 220f);
            AddSectionTitle(cliqueDebugPanel.transform, font, "Debug / Status");
            cliqueDebugText = AddText(cliqueDebugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
            cliqueDebugPanel.SetActive(false);
        }

        private void Refresh()
        {
            var profile = profileService.Profile;
            if (profile == null || profile.burrow_state == null || profile.wallet == null)
            {
                return;
            }

            if (profile.strata_state != null && profile.strata_state.current_stratum_id == LoamwakeExplorationService.StratumId)
            {
                currentScreen = ScreenPage.Loamwake;
            }

            UpdatePageVisibility();

            var snapshot = DebugStatusSnapshot.From(authManager, saveManager, profile);
            var burrow = profile.burrow_state;
            var loamwake = profile.strata_state.loamwake;
            var nextExpandCost = profileService.GetNextExpandCost();
            FixtureStateHelper.EnsureDefaults(profile);
            SocialProgressService.EnsureDefaults(profile);
            LuckyDrawEventService.EnsureDefaults(profile);
            CrackCliqueService.EnsureDefaults(profile);
            var luckyDraw = profile.event_progress.lucky_draw_week;
            var luckyDrawVisible = LuckyDrawEventService.IsEventVisible(profile);
            var crack = profile.crack_progress;
            var clique = profile.clique_progress;
            var guide = profileService.BuildTutorialGuidance();

            burrowWalletText.text = "Mooncaps: " + snapshot.mooncaps + "   Mushcaps: " + snapshot.mushcaps +
                "   Lucky Draws: " + profile.wallet.lucky_draws +
                "   Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                "   Ore: " + profile.wallet.loamwake_materials.crumbled_ore_chunk;
            burrowActionText.text = guide + "\nLast action: " + profileService.LastActionStatus;

            dewpondText.text =
                "Level: " + burrow.dewpond.level +
                "   Stored: " + burrow.dewpond.stored_output + "/" + burrow.dewpond.storage_cap + " Mooncaps";

            mushpatchText.text =
                "Level: " + burrow.mushpatch.level +
                "   Stored: " + burrow.mushpatch.stored_output + "/" + burrow.mushpatch.storage_cap + " Mushcaps";

            burrowStatusText.text =
                "Burrow level: " + burrow.burrow_level +
                "   Expand count: " + burrow.expand_count + "\n" +
                "Next cost: " + (nextExpandCost > 0 ? nextExpandCost + " Mooncaps" : "Stub after level 3") + "\n" +
                (nextExpandCost > 0 && profile.wallet.mooncaps < nextExpandCost
                    ? "Blocked: need " + nextExpandCost + " Mooncaps"
                    : "Ready to expand");

            rootmineText.text =
                "Status: " + (burrow.rootmine.unlocked ? "Unlocked" : "Locked") +
                "   Level: " + burrow.rootmine.level +
                "   Cap: " + burrow.rootmine.material_storage_cap + "\n" +
                "Stored: " + burrow.rootmine.stored_tangled_root_twine + " Twine / " +
                burrow.rootmine.stored_crumbled_ore_chunk + " Ore\n" +
                burrow.rootmine.status_note;

            fixtureSummaryText.text =
                "Equipped: " + profile.fixture_state.equipped_fixture_instance_ids.Count + "/" + profile.account.fixture_cap +
                "   Owned: " + profile.fixture_state.fixture_inventory.Count +
                "   Hat: " + (string.IsNullOrEmpty(profile.hat_state.visible_hat_id) ? "None" : FixtureStateHelper.FirstHatDisplayName) +
                "   Power: +" + FixtureStateHelper.GetTotalExpeditionPowerBonus(profile);

            socialSummaryText.text =
                "Greta: " + (profile.social_progress.greta.unlocked ? "Unlocked" : "Locked") +
                "   Trust: " + profile.social_progress.greta.trust_level +
                "   Rootrail: " + (profile.social_progress.rootrail.revealed ? "Revealed" : "Hidden") + "\n" +
                (luckyDrawVisible
                    ? "Lucky Draw: tickets " + profile.wallet.lucky_draws + ", pulls " + luckyDraw.pull_count
                    : "Lucky Draw hidden until Greta unlock") + "\n" +
                "Crack: " + (crack.visible ? "Depth " + crack.best_depth + ", Coins " + profile.wallet.crack_coins : "Hidden") +
                "   Clique: " + (clique.visible ? clique.player_role + ", Favor " + profile.wallet.favor_marks : "Hidden");

            fieldReturnsSnippetText.text = BuildFieldReturnsSnippet(loamwake.field_returns);

            burrowDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Guide: " + profile.tutorial_progress.current_step_id +
                "   Steps done: " + profile.tutorial_progress.completed_step_ids.Count;

            loamwakeWalletText.text =
                "Mushcaps: " + snapshot.mushcaps + "   Mooncaps: " + snapshot.mooncaps + "\n" +
                "Materials - Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                " / Ore: " + profile.wallet.loamwake_materials.crumbled_ore_chunk +
                " / Glow: " + profile.wallet.loamwake_materials.dull_glow_shard;
            loamwakeActionText.text = guide + "\nLast action: " + profileService.LastActionStatus;

            UpdateZoneCard(
                loamwake.zone_lw_001_rootvine_shelf,
                "Rootvine Shelf",
                8,
                zone1Text,
                zone1SafeButton,
                zone1RiskyButton,
                zone1SafeLabel,
                zone1RiskyLabel,
                profile.wallet.mushcaps);
            UpdateZoneCard(
                loamwake.zone_lw_002_mudpipe_hollow,
                "Mudpipe Hollow",
                15,
                zone2Text,
                zone2SafeButton,
                zone2RiskyButton,
                zone2SafeLabel,
                zone2RiskyLabel,
                profile.wallet.mushcaps);
            UpdateZoneCard(
                loamwake.zone_lw_003_glowroot_passage,
                "Glowroot Passage",
                22,
                zone3Text,
                zone3SafeButton,
                zone3RiskyButton,
                zone3SafeLabel,
                zone3RiskyLabel,
                profile.wallet.mushcaps);

            keeperText.text =
                "Status: " + (loamwake.keeper_lw_001_defeated ? "Defeated" : (loamwake.keeper_lw_001_unlocked ? "Unlocked" : "Locked")) + "\n" +
                "Zone: Mudpipe Hollow   Auto-Clash threshold: 28";
            keeperButton.interactable = loamwake.keeper_lw_001_unlocked && !loamwake.keeper_lw_001_defeated && profile.wallet.mushcaps >= 2;
            keeperButtonLabel.text = loamwake.keeper_lw_001_defeated ? "Warden Defeated" : "Challenge The Mudgrip (2 Mushcaps)";

            loamwakeFieldReturnsText.text = BuildFieldReturnsDetail(loamwake.field_returns);

            loamwakeDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Stratum: " + (string.IsNullOrEmpty(snapshot.current_stratum_id) ? "burrow" : snapshot.current_stratum_id) + "\n" +
                "Mudgrip defeated: " + loamwake.keeper_lw_001_defeated +
                "   UID: " + snapshot.active_uid +
                "   Path: " + snapshot.save_file_path;

            fixtureWalletText.text =
                "Mooncaps: " + snapshot.mooncaps +
                "   Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                "   Ore: " + profile.wallet.loamwake_materials.crumbled_ore_chunk + "\n" +
                "Fixture cap: " + profile.fixture_state.equipped_fixture_instance_ids.Count + "/" + profile.account.fixture_cap;
            fixtureActionText.text = guide + "\nLast action: " + profileService.LastActionStatus;
            fixtureCraftText.text =
                "Root-Bitten Shovel Strap\n" +
                "Recipe: 6 Tangled Root Twine + 80 Mooncaps\n" +
                "Effect while equipped: +2 expedition power\n" +
                "Crafted: " + (FixtureStateHelper.FindFirstFixture(profile) != null);
            fixtureInventoryText.text =
                "Owned Fixtures: " + profile.fixture_state.fixture_inventory.Count + "\n" +
                "Equipped ordered list: " + BuildEquippedFixtureList(profile) + "\n" +
                profileService.BuildPowerSummary();
            fixtureHatText.text =
                "Loamwake Dirt Cap: " + (FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId) ? "Unlocked" : "Locked until The Mudgrip is defeated") + "\n" +
                "Visible Hat: " + (string.IsNullOrEmpty(profile.hat_state.visible_hat_id) ? "None" : FixtureStateHelper.FirstHatDisplayName) + "\n" +
                profile.hat_state.passive_summary;
            fixtureDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Recipe IDs: " + profile.fixture_state.crafted_recipe_ids.Count +
                "   Model: ordered list, no rigid slots\n" +
                "Path: " + snapshot.save_file_path;

            vaultStatusText.text =
                "Vault state: " + (profile.vault_state.visible ? "Visible shell" : "Locked shell") + "\n" +
                profile.vault_state.status_note + "\n\n" +
                "Treasures not implemented in Sprint 3.\n" +
                "Set / Polish / Strengthen / Attune are intentionally absent.";
            vaultDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Treasure progression: " + profile.vault_state.treasure_progression_enabled +
                "   Owned: " + profile.vault_state.owned_treasure_count + "\n" +
                "Path: " + snapshot.save_file_path;

            burrowPostsStatusText.text = BuildBurrowPostsStatus(profile);
            burrowPostsDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Latest social: " + profile.social_progress.latest_result_summary + "\n" +
                "Greta follow-up: " + profile.social_progress.greta.first_followup_completed +
                "   Path: " + snapshot.save_file_path;

            dailyDutiesStatusText.text = BuildDailyDutiesStatus(profile);
            dailyDutiesDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Duty count: " + profile.social_progress.daily_duties.Count +
                "   Latest social: " + profile.social_progress.latest_result_summary + "\n" +
                "Path: " + snapshot.save_file_path;

            rootrailStatusText.text =
                "Rootrail Terminal - Loamwake Station\n" +
                "Reveal: " + (profile.social_progress.rootrail.revealed ? "Revealed" : "Hidden") +
                "   Station: " + (profile.social_progress.rootrail.station_visible ? "Visible" : "Locked") + "\n" +
                "Repair: " + (profile.social_progress.rootrail.repair_progression_enabled ? "Enabled" : "Not implemented") + "\n" +
                "Forgotten Manual: Loamwake Terminal Routing Manual - Not in Codex\n" +
                profile.social_progress.rootrail.status_note;
            rootrailDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Timer started: " + profile.social_progress.rootrail.repair_timer_started + "\n" +
                "Step: " + (string.IsNullOrEmpty(profile.social_progress.rootrail.current_step_id) ? "none" : profile.social_progress.rootrail.current_step_id) +
                "   Parts: " + profile.wallet.rootrail_parts;

            luckyDrawStatusText.text =
                "State: " + (luckyDraw.active ? "Active" : (luckyDraw.unlocked ? "Unlocked" : "Locked")) +
                "   Gate: " + luckyDraw.unlock_gate_note + " - " + (luckyDraw.unlock_gate_met ? "met" : "not met") + "\n" +
                "Tickets: " + profile.wallet.lucky_draws + "   Pulls: " + luckyDraw.pull_count + "\n" +
                "Weekly: " + (luckyDraw.weekly_claimed ? "claimed" : "ready") +
                "   Activity: " + (luckyDraw.activity_ticket_claimed ? "claimed" : luckyDraw.activity_progress + "/3") + "\n" +
                "Latest pull: " + luckyDraw.latest_pull_result;
            luckyDrawLedgerText.text = LuckyDrawEventService.BuildLedgerSummary(profile);
            luckyDrawStallText.text =
                LuckyDrawEventService.BuildStallSummary(profile) + "\n" +
                "Mooncaps: " + profile.wallet.mooncaps +
                "   Festival Marks: " + profile.wallet.festival_marks +
                "   Polishes: " + profile.wallet.polishes;
            luckyDrawDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Week: " + luckyDraw.week_marker + "\n" +
                "Paid: " + luckyDraw.paid_path_active +
                "   IAP: " + luckyDraw.iap_enabled +
                "   Latest: " + luckyDraw.latest_result_summary;

            crackStatusText.text =
                "State: " + (crack.unlocked ? "Unlocked" : (crack.visible ? "Visible" : "Locked")) +
                "   Gate: " + crack.unlock_gate_note + " - " + (crack.unlock_gate_met ? "met" : "not met") + "\n" +
                "Depth: " + crack.current_depth + "   Best: " + crack.best_depth +
                "   Probes: " + crack.probe_count + "   Coins: " + profile.wallet.crack_coins + "\n" +
                "Rewards: " + crack.reward_claim_summary + "\n" +
                "Latest: " + crack.latest_result_summary + "\n" +
                "Only Probe the Crack is implemented in this sprint.";
            crackDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Visible: " + crack.visible +
                "   Unlocked: " + crack.unlocked + "\n" +
                "Latest: " + crack.latest_result_summary +
                "   Path: " + snapshot.save_file_path;

            cliqueStatusText.text =
                "State: " + (clique.unlocked ? "Unlocked" : (clique.visible ? "Visible" : "Locked")) +
                "   Gate: " + clique.unlock_gate_note + " - " + (clique.unlock_gate_met ? "met" : "not met") + "\n" +
                "Name: " + clique.clique_name +
                "   Role: " + clique.player_role +
                "   Favor: " + profile.wallet.favor_marks + "\n" +
                "Clique Rolls:\n" + BuildCliqueRosterSummary(clique) +
                "Great Dispute: stub notice only; no gameplay implemented.\n" +
                "Latest: " + clique.latest_result_summary;
            cliqueDebugText.text =
                "Auth: " + snapshot.auth_state +
                "   Save: " + snapshot.save_state +
                "   Networking: " + clique.networking_enabled + "\n" +
                "Multiplayer: " + clique.multiplayer_enabled +
                "   Shared: " + clique.shared_state_enabled +
                "   Dispute stub: " + clique.great_dispute_stub_only;

            dewpondGatherButton.interactable = burrow.dewpond.stored_output > 0;
            mushpatchGatherButton.interactable = burrow.mushpatch.stored_output > 0;
            rootmineGatherButton.interactable = BurrowProductionService.CanGather(burrow.rootmine);
            rootmineGatherLabel.text = burrow.rootmine.unlocked ? "Gather Rootmine Materials" : "Rootmine Locked";
            expandButton.interactable = nextExpandCost > 0 && profile.wallet.mooncaps >= nextExpandCost;
            expandButtonLabel.text = nextExpandCost > 0 ? "Expand Burrow (" + nextExpandCost + " Mooncaps)" : "Expand Stubbed";

            var firstFixture = FixtureStateHelper.FindFirstFixture(profile);
            var firstFixtureEquipped = FixtureStateHelper.IsFirstFixtureEquipped(profile);
            unlockFixtureCapButton.interactable = profile.account.fixture_cap < 1;
            unlockFixtureCapLabel.text = profile.account.fixture_cap < 1 ? "Unlock First Fixture Cap" : "First Cap Unlocked";
            craftFirstFixtureButton.interactable = profile.account.fixture_cap >= 1 &&
                firstFixture == null &&
                profile.wallet.mooncaps >= FixtureService.FirstFixtureMooncapCost &&
                profile.wallet.loamwake_materials.tangled_root_twine >= FixtureService.FirstFixtureTwineCost;
            craftFirstFixtureLabel.text = firstFixture == null ? "Craft Root-Bitten Shovel Strap" : "Fixture Crafted";
            equipFirstFixtureButton.interactable = firstFixture != null &&
                !firstFixtureEquipped &&
                profile.fixture_state.equipped_fixture_instance_ids.Count < profile.account.fixture_cap;
            equipFirstFixtureLabel.text = firstFixtureEquipped ? "First Fixture Equipped" : "Equip First Fixture";
            unequipFirstFixtureButton.interactable = firstFixtureEquipped;
            unequipFirstFixtureLabel.text = firstFixtureEquipped ? "Unequip First Fixture" : "Nothing Equipped";
            setFirstHatVisibleButton.interactable = FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId) &&
                profile.hat_state.visible_hat_id != FixtureStateHelper.FirstHatId;
            setFirstHatVisibleLabel.text = FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId)
                ? "Set Loamwake Dirt Cap Visible"
                : "Hat Locked";

            var utilityPost = SocialProgressService.GetPost(profile, SocialProgressService.UtilityPostId);
            var gretaPost = SocialProgressService.GetPost(profile, SocialProgressService.GretaIntroPostId);
            readUtilityPostButton.interactable = utilityPost != null && !utilityPost.completed && utilityPost.state != "locked";
            readUtilityPostLabel.text = utilityPost != null && utilityPost.completed ? "Root Twine Run Completed" : "Read Root Twine Run";
            readGretaIntroPostButton.interactable = gretaPost != null && !gretaPost.completed && gretaPost.state != "locked";
            readGretaIntroPostLabel.text = gretaPost != null && gretaPost.completed
                ? "Greta Intro Completed"
                : (gretaPost != null && gretaPost.state == "locked" ? "Greta Intro Locked" : "Read Greta Intro Post");
            completeGretaFollowupButton.interactable = profile.social_progress.greta.unlocked && !profile.social_progress.greta.first_followup_completed;
            completeGretaFollowupLabel.text = profile.social_progress.greta.first_followup_completed
                ? "Greta Follow-Up Completed"
                : "Complete Greta Follow-Up";
            completeRootrailRevealButton.interactable = profile.social_progress.greta.first_followup_completed &&
                loamwake.zone_lw_002_mudpipe_hollow.first_clear &&
                !profile.social_progress.rootrail.revealed;
            completeRootrailRevealLabel.text = profile.social_progress.rootrail.revealed
                ? "Rootrail Revealed"
                : "Follow Greta to the Old Track";
            openRootrailButton.interactable = profile.social_progress.rootrail.station_visible;
            openLuckyDrawButton.gameObject.SetActive(luckyDrawVisible);
            openLuckyDrawButton.interactable = luckyDrawVisible;
            openLuckyDrawLabel.text = "Lucky Draw Week";

            claimLuckyDrawWeeklyButton.interactable = luckyDrawVisible && !luckyDraw.weekly_claimed;
            claimLuckyDrawWeeklyLabel.text = luckyDraw.weekly_claimed ? "Weekly Claim Used" : "Claim Weekly Lucky Draw";
            pullLuckyDrawButton.interactable = luckyDrawVisible && profile.wallet.lucky_draws >= 1;
            pullLuckyDrawLabel.text = profile.wallet.lucky_draws >= 1 ? "Pull (1 Lucky Draw)" : "Need 1 Lucky Draw";
            claimFestivalLedgerButton.interactable = luckyDrawVisible && HasClaimableLedgerReward(profile);
            claimFestivalLedgerLabel.text = HasClaimableLedgerReward(profile) ? "Claim Ready Ledger Reward" : "No Ledger Reward Ready";
            buyMooncapDrawButton.interactable = luckyDrawVisible &&
                profile.wallet.mooncaps >= 250 &&
                LuckyDrawEventService.GetRemainingPurchases(profile, LuckyDrawEventService.StallMooncapDrawId, 1) > 0;
            buyMooncapDrawLabel.text = "Mooncap Draw (250 Mooncaps)";
            buyMaterialCacheButton.interactable = luckyDrawVisible &&
                profile.wallet.lucky_draws >= 2 &&
                LuckyDrawEventService.GetRemainingPurchases(profile, LuckyDrawEventService.StallMaterialCacheId, 2) > 0;
            buyMaterialCacheLabel.text = "Material Cache (2 Lucky Draws)";
            buyPolishBundleButton.interactable = luckyDrawVisible &&
                profile.wallet.lucky_draws >= 1 &&
                LuckyDrawEventService.GetRemainingPurchases(profile, LuckyDrawEventService.StallPolishBundleId, 2) > 0;
            buyPolishBundleLabel.text = "Polish Bundle (1 Lucky Draw)";
            buyFestivalExchangeButton.interactable = luckyDrawVisible &&
                profile.wallet.festival_marks >= 20 &&
                LuckyDrawEventService.GetRemainingPurchases(profile, LuckyDrawEventService.StallFestivalExchangeId, 1) > 0;
            buyFestivalExchangeLabel.text = "Festival Exchange (20 Marks)";

            enterLoamwakeButton.interactable = profileService.IsStratumSelectable(LoamwakeExplorationService.StratumId);
            enterLoamwakeButtonLabel.text = "Enter Loamwake";
            ledgerhollowButton.interactable = crack.visible;
            ledgerhollowButtonLabel.text = crack.visible ? "The Crack" : "Ledgerhollow (Unavailable)";
            memoryFenButton.interactable = clique.visible;
            memoryFenButtonLabel.text = clique.visible ? "Clique" : "Memory Fen (Unavailable)";
            probeCrackButton.interactable = crack.unlocked;
            probeCrackLabel.text = crack.unlocked ? "Probe the Crack" : "Crack Locked";
            claimCliqueStipendButton.interactable = clique.unlocked && !clique.local_stipend_claimed;
            claimCliqueStipendLabel.text = clique.local_stipend_claimed ? "Local Stipend Claimed" : "Claim Local Clique Stipend";

            UpdateDebugPanelVisibility();
        }

        private void UpdateZoneCard(
            ZoneProgressData zone,
            string displayName,
            int difficulty,
            Text zoneText,
            Button safeButton,
            Button riskyButton,
            Text safeLabel,
            Text riskyLabel,
            int currentMushcaps)
        {
            var unlocked = zone != null && zone.unlocked;
            var clearCount = zone != null ? zone.clear_count : 0;
            var firstClear = zone != null && zone.first_clear;

            zoneText.text =
                "Status: " + (unlocked ? "Unlocked" : "Locked") +
                "   Difficulty: " + difficulty + "\n" +
                "First clear: " + firstClear + "   Clears: " + clearCount;

            safeButton.interactable = unlocked && currentMushcaps >= 1;
            riskyButton.interactable = unlocked && currentMushcaps >= 2;
            safeLabel.text = unlocked ? "Safe Route (1 Mushcap)" : "Safe Route (Locked)";
            riskyLabel.text = unlocked ? "Risky Route (2 Mushcaps)" : "Risky Route (Locked)";
        }

        private void OnGatherDewpondPressed()
        {
            profileService.GatherDewpond();
        }

        private void OnGatherMushpatchPressed()
        {
            profileService.GatherMushpatch();
        }

        private void OnGatherRootminePressed()
        {
            profileService.GatherRootmine();
        }

        private void OnExpandPressed()
        {
            profileService.ExpandBurrow();
        }

        private void OnOpenFixturePressed()
        {
            currentScreen = ScreenPage.FixtureWorkshop;
            Refresh();
        }

        private void OnOpenVaultPressed()
        {
            currentScreen = ScreenPage.Vault;
            Refresh();
        }

        private void OnOpenBurrowPostsPressed()
        {
            currentScreen = ScreenPage.BurrowPosts;
            Refresh();
        }

        private void OnOpenDailyDutiesPressed()
        {
            currentScreen = ScreenPage.DailyDuties;
            Refresh();
        }

        private void OnOpenRootrailPressed()
        {
            currentScreen = ScreenPage.Rootrail;
            Refresh();
        }

        private void OnOpenLuckyDrawPressed()
        {
            currentScreen = ScreenPage.LuckyDrawWeek;
            Refresh();
        }

        private void OnOpenCrackPressed()
        {
            currentScreen = ScreenPage.Crack;
            Refresh();
        }

        private void OnOpenCliquePressed()
        {
            currentScreen = ScreenPage.Clique;
            Refresh();
        }

        private void OnEnterLoamwakePressed()
        {
            profileService.EnterLoamwake();
            currentScreen = ScreenPage.Loamwake;
            Refresh();
        }

        private void OnBackToBurrowPressed()
        {
            profileService.ReturnToBurrow();
            currentScreen = ScreenPage.Burrow;
            Refresh();
        }

        private void OnUnlockFixtureCapPressed()
        {
            profileService.UnlockFirstFixtureCap();
        }

        private void OnCraftFirstFixturePressed()
        {
            profileService.CraftFirstFixture();
        }

        private void OnEquipFirstFixturePressed()
        {
            profileService.EquipFirstFixture();
        }

        private void OnUnequipFirstFixturePressed()
        {
            profileService.UnequipFirstFixture();
        }

        private void OnSetFirstHatVisiblePressed()
        {
            profileService.SetFirstHatVisible();
        }

        private void OnReadUtilityPostPressed()
        {
            profileService.ReadUtilityBurrowPost();
        }

        private void OnReadGretaIntroPostPressed()
        {
            profileService.ReadGretaIntroPost();
        }

        private void OnCompleteGretaFollowupPressed()
        {
            profileService.CompleteGretaFirstFollowup();
        }

        private void OnCompleteRootrailRevealPressed()
        {
            profileService.RevealRootrailStation();
        }

        private void OnClaimLuckyDrawWeeklyPressed()
        {
            profileService.ClaimLuckyDrawWeeklyTicket();
        }

        private void OnPullLuckyDrawPressed()
        {
            profileService.PullLuckyDraw();
        }

        private void OnClaimFestivalLedgerPressed()
        {
            profileService.ClaimFestivalLedgerReward();
        }

        private void OnBuyMooncapDrawPressed()
        {
            profileService.BuyLuckyStallMooncapDraw();
        }

        private void OnBuyMaterialCachePressed()
        {
            profileService.BuyLuckyStallMaterialCache();
        }

        private void OnBuyPolishBundlePressed()
        {
            profileService.BuyLuckyStallPolishBundle();
        }

        private void OnBuyFestivalExchangePressed()
        {
            profileService.BuyLuckyStallFestivalExchange();
        }

        private void OnProbeCrackPressed()
        {
            profileService.ProbeCrack();
        }

        private void OnClaimCliqueStipendPressed()
        {
            profileService.ClaimCliqueStipend();
        }

        private void OnZone1SafePressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.SafeRouteId);
        }

        private void OnZone1RiskyPressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_001_rootvine_shelf", LoamwakeExplorationService.RiskyRouteId);
        }

        private void OnZone2SafePressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.SafeRouteId);
        }

        private void OnZone2RiskyPressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_002_mudpipe_hollow", LoamwakeExplorationService.RiskyRouteId);
        }

        private void OnZone3SafePressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_003_glowroot_passage", LoamwakeExplorationService.SafeRouteId);
        }

        private void OnZone3RiskyPressed()
        {
            profileService.ExploreLoamwakeZone("zone_lw_003_glowroot_passage", LoamwakeExplorationService.RiskyRouteId);
        }

        private void OnKeeperPressed()
        {
            profileService.ChallengeLoamwakeKeeper();
        }

        private void OnForceSavePressed()
        {
            profileService.ForceSave();
        }

        private void OnResetTutorialGuidePressed()
        {
            profileService.ResetTutorialProgress();
        }

        private void OnReloadPressed()
        {
            profileService.ReloadSave();
        }

        private void OnToggleDebugPressed()
        {
            debugExpanded = !debugExpanded;
            UpdateDebugPanelVisibility();
        }

        private void UpdateDebugPanelVisibility()
        {
            debugToggleBurrowLabel.text = debugExpanded ? "Debug: Hide" : "Debug: Show";
            SetDebugPanelActive(burrowDebugPanel);
            SetDebugPanelActive(loamwakeDebugPanel);
            SetDebugPanelActive(fixtureDebugPanel);
            SetDebugPanelActive(vaultDebugPanel);
            SetDebugPanelActive(burrowPostsDebugPanel);
            SetDebugPanelActive(dailyDutiesDebugPanel);
            SetDebugPanelActive(rootrailDebugPanel);
            SetDebugPanelActive(luckyDrawDebugPanel);
            SetDebugPanelActive(crackDebugPanel);
            SetDebugPanelActive(cliqueDebugPanel);
        }

        private void SetDebugPanelActive(GameObject panel)
        {
            if (panel != null)
            {
                panel.SetActive(debugExpanded);
            }
        }

        private void UpdatePageVisibility()
        {
            burrowPage.SetActive(currentScreen == ScreenPage.Burrow);
            loamwakePage.SetActive(currentScreen == ScreenPage.Loamwake);
            fixturePage.SetActive(currentScreen == ScreenPage.FixtureWorkshop);
            vaultPage.SetActive(currentScreen == ScreenPage.Vault);
            burrowPostsPage.SetActive(currentScreen == ScreenPage.BurrowPosts);
            dailyDutiesPage.SetActive(currentScreen == ScreenPage.DailyDuties);
            rootrailPage.SetActive(currentScreen == ScreenPage.Rootrail);
            luckyDrawPage.SetActive(currentScreen == ScreenPage.LuckyDrawWeek);
            crackPage.SetActive(currentScreen == ScreenPage.Crack);
            cliquePage.SetActive(currentScreen == ScreenPage.Clique);
        }

        private static string BuildBurrowPostsStatus(PlayerProfileData profile)
        {
            return "Available Burrow Posts\n" +
                FormatPost(SocialProgressService.GetPost(profile, SocialProgressService.UtilityPostId)) + "\n" +
                FormatPost(SocialProgressService.GetPost(profile, SocialProgressService.GretaIntroPostId)) + "\n\n" +
                "Greta: " + (profile.social_progress.greta.unlocked ? "Unlocked" : "Locked") +
                "   Trust: " + profile.social_progress.greta.trust_level + "\n" +
                "Rootrail reveal gate: Greta follow-up + Mudpipe Hollow first clear";
        }

        private static string BuildDailyDutiesStatus(PlayerProfileData profile)
        {
            var value = "Daily Duties prototype loop\n";
            for (var i = 0; i < profile.social_progress.daily_duties.Count; i++)
            {
                value += FormatDuty(profile.social_progress.daily_duties[i]) + "\n";
            }

            value += "\nRewards auto-claim when progress reaches target.";
            return value;
        }

        private static string FormatPost(BurrowPostStateData post)
        {
            if (post == null)
            {
                return "Missing Post state";
            }

            return post.title + " - " + post.state + (post.unread ? " / unread" : " / read");
        }

        private static string FormatDuty(DailyDutyStateData duty)
        {
            if (duty == null)
            {
                return "Missing Duty state";
            }

            return duty.title + ": " + duty.progress + "/" + duty.target +
                " - " + (duty.completed ? "completed" : "active") +
                " - " + duty.reward_summary;
        }

        private static string BuildCliqueRosterSummary(CliqueProgressData clique)
        {
            if (clique == null || clique.roster == null || clique.roster.Count == 0)
            {
                return "No local roster entries.\n";
            }

            var value = "";
            for (var i = 0; i < clique.roster.Count; i++)
            {
                var entry = clique.roster[i];
                if (entry == null)
                {
                    continue;
                }

                value += "- " + entry.display_name + " / " + entry.role + " / " + entry.status + "\n";
            }

            return value;
        }

        private static bool HasClaimableLedgerReward(PlayerProfileData profile)
        {
            if (profile == null || profile.event_progress == null || profile.event_progress.lucky_draw_week == null)
            {
                return false;
            }

            var state = profile.event_progress.lucky_draw_week;
            return (state.pull_count >= 1 && !state.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_001_first_pull")) ||
                (state.pull_count >= 3 && !state.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_002_three_pulls")) ||
                (state.pull_count >= 5 && !state.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_003_five_pulls")) ||
                (state.pull_count >= 8 && !state.festival_ledger.claimed_tier_ids.Contains("ledger_lucky_004_eight_pulls"));
        }

        private static string BuildEquippedFixtureList(PlayerProfileData profile)
        {
            if (profile == null || profile.fixture_state == null || profile.fixture_state.equipped_fixture_instance_ids == null ||
                profile.fixture_state.equipped_fixture_instance_ids.Count == 0)
            {
                return "empty";
            }

            var value = "";
            for (var i = 0; i < profile.fixture_state.equipped_fixture_instance_ids.Count; i++)
            {
                if (i > 0)
                {
                    value += ", ";
                }

                value += (i + 1) + ": " + GetFixtureDisplayName(profile, profile.fixture_state.equipped_fixture_instance_ids[i]);
            }

            return value;
        }

        private static string GetFixtureDisplayName(PlayerProfileData profile, string instanceId)
        {
            if (profile == null || profile.fixture_state == null || profile.fixture_state.fixture_inventory == null)
            {
                return instanceId;
            }

            foreach (var fixture in profile.fixture_state.fixture_inventory)
            {
                if (fixture != null && fixture.instance_id == instanceId)
                {
                    return fixture.fixture_id == FixtureStateHelper.FirstFixtureId
                        ? "Root-Bitten Shovel Strap"
                        : fixture.fixture_id;
                }
            }

            return instanceId;
        }

        private static string BuildFieldReturnsSnippet(ExplorationResultData result)
        {
            if (result == null || string.IsNullOrEmpty(result.last_zone_id))
            {
                return "No Loamwake runs recorded yet.";
            }

            return result.last_zone_id + " via " + result.route_id + "\n" +
                "Result: " + result.result + "   Mooncaps: +" + result.mooncaps + "   Mushcaps: +" + result.mushcaps;
        }

        private static string BuildFieldReturnsDetail(ExplorationResultData result)
        {
            if (result == null || string.IsNullOrEmpty(result.last_zone_id))
            {
                return "No Loamwake exploration recorded yet.";
            }

            var materialLine = "Materials: none";
            if (result.material_rewards != null && result.material_rewards.Count > 0)
            {
                materialLine = "Materials: ";
                for (var i = 0; i < result.material_rewards.Count; i++)
                {
                    var reward = result.material_rewards[i];
                    if (i > 0)
                    {
                        materialLine += ", ";
                    }

                    materialLine += reward.material_id + " +" + reward.amount;
                }
            }

            return "Zone: " + result.last_zone_id + "\n" +
                "Route: " + result.route_id + "   Result: " + result.result + "\n" +
                "Mooncaps: +" + result.mooncaps + "   Mushcaps: +" + result.mushcaps + "\n" +
                materialLine;
        }

        private void CreateNavHeader(Transform parent, Font font, string pageTitle, out Button backButton)
        {
            var navCard = CreateCard(parent, pageTitle + "NavHeader", 96f);
            backButton = AddButton(navCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out var _);
            AddText(navCard.transform, font, pageTitle, 32, FontStyle.Bold, TextAnchor.MiddleLeft, 40f);
        }

        private static void AddSectionTitle(Transform parent, Font font, string title)
        {
            AddText(parent, font, title, 20, FontStyle.Bold, TextAnchor.MiddleLeft, 30f);
        }

        private static GameObject CreatePage(Transform parent, string name)
        {
            var page = new GameObject(name, typeof(RectTransform));
            page.transform.SetParent(parent, false);

            var rect = page.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.04f, 0.04f);
            rect.anchorMax = new Vector2(0.96f, 0.96f);
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            var image = page.AddComponent<Image>();
            image.color = new Color(0.06f, 0.08f, 0.07f, 0.98f);

            var layout = page.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(14, 14, 14, 14);
            layout.spacing = 10;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return page;
        }

        private static GameObject CreateCard(Transform parent, string name, float height)
        {
            var card = CreatePanel(parent, name, new Color(0.12f, 0.16f, 0.14f, 1f));
            var rect = card.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, height);

            var layoutElement = card.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = height;

            var layout = card.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(14, 14, 10, 10);
            layout.spacing = 6;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return card;
        }

        private static GameObject CreateButtonRow(Transform parent, string name, float height)
        {
            var row = new GameObject(name, typeof(RectTransform));
            row.transform.SetParent(parent, false);

            var rect = row.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, height);

            var layoutElement = row.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = height;

            var layout = row.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = 8;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = true;

            return row;
        }

        private static Transform CreateScrollBody(Transform parent, string name)
        {
            var scrollObject = CreatePanel(parent, name, new Color(0.08f, 0.1f, 0.09f, 0.3f));
            var scrollRectTransform = scrollObject.GetComponent<RectTransform>();

            var layoutElement = scrollObject.AddComponent<LayoutElement>();
            layoutElement.flexibleHeight = 1f;
            layoutElement.minHeight = 180f;

            var scrollRect = scrollObject.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.movementType = ScrollRect.MovementType.Clamped;
            scrollRect.scrollSensitivity = 36f;

            var viewportObject = CreatePanel(scrollObject.transform, name + "Viewport", new Color(0f, 0f, 0f, 0f));
            var viewportRect = viewportObject.GetComponent<RectTransform>();
            viewportRect.anchorMin = Vector2.zero;
            viewportRect.anchorMax = Vector2.one;
            viewportRect.offsetMin = Vector2.zero;
            viewportRect.offsetMax = Vector2.zero;
            viewportObject.AddComponent<RectMask2D>();

            var contentObject = new GameObject(name + "Content", typeof(RectTransform));
            contentObject.transform.SetParent(viewportObject.transform, false);
            var contentRect = contentObject.GetComponent<RectTransform>();
            contentRect.anchorMin = new Vector2(0f, 1f);
            contentRect.anchorMax = new Vector2(1f, 1f);
            contentRect.pivot = new Vector2(0.5f, 1f);
            contentRect.offsetMin = Vector2.zero;
            contentRect.offsetMax = Vector2.zero;

            var layout = contentObject.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(0, 0, 0, 0);
            layout.spacing = 10;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            var fitter = contentObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            scrollRect.viewport = viewportRect;
            scrollRect.content = contentRect;

            scrollRectTransform.sizeDelta = Vector2.zero;
            return contentObject.transform;
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
            var buttonObject = CreatePanel(parent, label, new Color(0.16f, 0.22f, 0.18f, 1f));
            var button = buttonObject.AddComponent<Button>();
            if (onClick != null)
            {
                button.onClick.AddListener(onClick);
            }

            var colors = button.colors;
            colors.highlightedColor = new Color(0.22f, 0.3f, 0.25f, 1f);
            colors.pressedColor = new Color(0.1f, 0.14f, 0.12f, 1f);
            colors.disabledColor = new Color(0.12f, 0.14f, 0.13f, 0.7f);
            button.colors = colors;

            var rect = buttonObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 44f);

            var layout = buttonObject.AddComponent<LayoutElement>();
            layout.preferredHeight = 44f;

            labelText = AddText(buttonObject.transform, font, label, 18, FontStyle.Bold, TextAnchor.MiddleCenter, 44f);
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
