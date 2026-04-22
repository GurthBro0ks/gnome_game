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

            var canvasObject = new GameObject("Sprint2Canvas", typeof(RectTransform));
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
            var headerCard = CreateCard(burrowPage.transform, "BurrowFixedHeader", 420f);
            AddText(headerCard.transform, font, "The Burrow", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            burrowWalletText = AddText(headerCard.transform, font, "", 23, FontStyle.Bold, TextAnchor.MiddleLeft, 64f);
            burrowActionText = AddText(headerCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.MiddleLeft, 88f);

            Text unusedLabel;
            var primaryRow = CreateButtonRow(headerCard.transform, "BurrowPrimaryActionRow");
            enterLoamwakeButton = AddButton(primaryRow.transform, font, "Enter Loamwake", OnEnterLoamwakePressed, out enterLoamwakeButtonLabel);
            openBurrowPostsButton = AddButton(primaryRow.transform, font, "Burrow Post", OnOpenBurrowPostsPressed, out unusedLabel);
            openDailyDutiesButton = AddButton(primaryRow.transform, font, "Daily Duties", OnOpenDailyDutiesPressed, out unusedLabel);

            var systemsRow = CreateButtonRow(headerCard.transform, "BurrowSystemsActionRow");
            openRootrailButton = AddButton(systemsRow.transform, font, "Rootrail Station", OnOpenRootrailPressed, out unusedLabel);
            openLuckyDrawButton = AddButton(systemsRow.transform, font, "Lucky Draw Week", OnOpenLuckyDrawPressed, out openLuckyDrawLabel);
            openFixtureButton = AddButton(systemsRow.transform, font, "Fixture Workshop", OnOpenFixturePressed, out unusedLabel);

            var shellRow = CreateButtonRow(headerCard.transform, "BurrowShellActionRow");
            openVaultButton = AddButton(shellRow.transform, font, "Vault of Treasures", OnOpenVaultPressed, out unusedLabel);
            ledgerhollowButton = AddButton(shellRow.transform, font, "Ledgerhollow (Unavailable)", OnOpenCrackPressed, out ledgerhollowButtonLabel);
            memoryFenButton = AddButton(shellRow.transform, font, "Memory Fen (Unavailable)", OnOpenCliquePressed, out memoryFenButtonLabel);

            var body = CreateScrollBody(burrowPage.transform, "BurrowScrollableBody");

            var dewpondCard = CreateCard(body, "DewpondCard", 130f);
            dewpondText = AddText(dewpondCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 62f);
            dewpondGatherButton = AddButton(dewpondCard.transform, font, "Gather Dewpond", OnGatherDewpondPressed, out unusedLabel);

            var mushpatchCard = CreateCard(body, "MushpatchCard", 130f);
            mushpatchText = AddText(mushpatchCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 62f);
            mushpatchGatherButton = AddButton(mushpatchCard.transform, font, "Gather Mushpatch", OnGatherMushpatchPressed, out unusedLabel);

            var burrowCard = CreateCard(body, "BurrowStatusCard", 160f);
            burrowStatusText = AddText(burrowCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            expandButton = AddButton(burrowCard.transform, font, "Expand Burrow", OnExpandPressed, out expandButtonLabel);

            var rootmineCard = CreateCard(body, "RootmineCard", 180f);
            rootmineText = AddText(rootmineCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 108f);
            rootmineGatherButton = AddButton(rootmineCard.transform, font, "Gather Rootmine", OnGatherRootminePressed, out rootmineGatherLabel);

            var fixtureSummaryCard = CreateCard(body, "FixtureSummaryCard", 126f);
            fixtureSummaryText = AddText(fixtureSummaryCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 86f);

            var socialCard = CreateCard(body, "Sprint4SocialCard", 150f);
            socialSummaryText = AddText(socialCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 112f);

            var strataGateCard = CreateCard(body, "StrataGateCard", 58f);
            AddText(strataGateCard.transform, font, "Strata Gate", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);

            var returnsCard = CreateCard(body, "FieldReturnsCard", 96f);
            fieldReturnsSnippetText = AddText(returnsCard.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 62f);

            var debugPanel = CreateCard(body, "BurrowDebugPanel", 180f);
            AddText(debugPanel.transform, font, "Burrow Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            AddButton(debugPanel.transform, font, "Reset Guide", OnResetTutorialGuidePressed, out unusedLabel);
            burrowDebugText = AddText(debugPanel.transform, font, "", 17, FontStyle.Normal, TextAnchor.UpperLeft, 62f);
        }

        private void BuildLoamwakePage(Font font)
        {
            var topNavCard = CreateCard(loamwakePage.transform, "LoamwakeTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(loamwakePage.transform, "LoamwakeScrollableBody");

            AddText(body, font, "Loamwake", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            loamwakeWalletText = AddText(body, font, "", 28, FontStyle.Bold, TextAnchor.MiddleLeft, 42f);
            loamwakeActionText = AddText(body, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 72f);

            var zone1Card = CreateCard(body, "Zone1Card", 220f);
            zone1Text = AddText(zone1Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone1SafeButton = AddButton(zone1Card.transform, font, "Safe Route", OnZone1SafePressed, out zone1SafeLabel);
            zone1RiskyButton = AddButton(zone1Card.transform, font, "Risky Route", OnZone1RiskyPressed, out zone1RiskyLabel);

            var zone2Card = CreateCard(body, "Zone2Card", 220f);
            zone2Text = AddText(zone2Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone2SafeButton = AddButton(zone2Card.transform, font, "Safe Route", OnZone2SafePressed, out zone2SafeLabel);
            zone2RiskyButton = AddButton(zone2Card.transform, font, "Risky Route", OnZone2RiskyPressed, out zone2RiskyLabel);

            var zone3Card = CreateCard(body, "Zone3Card", 220f);
            zone3Text = AddText(zone3Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone3SafeButton = AddButton(zone3Card.transform, font, "Safe Route", OnZone3SafePressed, out zone3SafeLabel);
            zone3RiskyButton = AddButton(zone3Card.transform, font, "Risky Route", OnZone3RiskyPressed, out zone3RiskyLabel);

            var keeperCard = CreateCard(body, "KeeperCard", 176f);
            keeperText = AddText(keeperCard.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            keeperButton = AddButton(keeperCard.transform, font, "Challenge Warden", OnKeeperPressed, out keeperButtonLabel);

            var returnsCard = CreateCard(body, "LoamwakeReturnsCard", 150f);
            loamwakeFieldReturnsText = AddText(returnsCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 104f);

            var navCard = CreateCard(body, "NavigationCard", 150f);
            AddText(navCard.transform, font, "Navigation", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            AddButton(navCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);
            AddButton(navCard.transform, font, "Force Save", OnForceSavePressed, out unusedLabel);

            var debugPanel = CreateCard(body, "LoamwakeDebugPanel", 240f);
            AddText(debugPanel.transform, font, "Loamwake Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            loamwakeDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 170f);
        }

        private void BuildFixturePage(Font font)
        {
            var topNavCard = CreateCard(fixturePage.transform, "FixtureTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(fixturePage.transform, "FixtureScrollableBody");

            AddText(body, font, "Fixture Workshop", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            fixtureWalletText = AddText(body, font, "", 24, FontStyle.Bold, TextAnchor.MiddleLeft, 64f);
            fixtureActionText = AddText(body, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 72f);

            var craftCard = CreateCard(body, "FirstFixtureCraftCard", 260f);
            fixtureCraftText = AddText(craftCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 108f);
            unlockFixtureCapButton = AddButton(craftCard.transform, font, "Unlock First Fixture Cap", OnUnlockFixtureCapPressed, out unlockFixtureCapLabel);
            craftFirstFixtureButton = AddButton(craftCard.transform, font, "Craft Root-Bitten Shovel Strap", OnCraftFirstFixturePressed, out craftFirstFixtureLabel);

            var inventoryCard = CreateCard(body, "FixtureInventoryCard", 260f);
            fixtureInventoryText = AddText(inventoryCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 108f);
            equipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Equip First Fixture", OnEquipFirstFixturePressed, out equipFirstFixtureLabel);
            unequipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Unequip First Fixture", OnUnequipFirstFixturePressed, out unequipFirstFixtureLabel);

            var hatCard = CreateCard(body, "HatShellCard", 196f);
            fixtureHatText = AddText(hatCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 108f);
            setFirstHatVisibleButton = AddButton(hatCard.transform, font, "Set Visible Hat", OnSetFirstHatVisiblePressed, out setFirstHatVisibleLabel);

            var debugPanel = CreateCard(body, "FixtureDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Fixture Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            fixtureDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildVaultPage(Font font)
        {
            var topNavCard = CreateCard(vaultPage.transform, "VaultTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(vaultPage.transform, "VaultScrollableBody");

            AddText(body, font, "Vault of Treasures", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var shellCard = CreateCard(body, "VaultShellCard", 240f);
            vaultStatusText = AddText(shellCard.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 196f);

            var debugPanel = CreateCard(body, "VaultDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Vault Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            vaultDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildBurrowPostsPage(Font font)
        {
            var topNavCard = CreateCard(burrowPostsPage.transform, "BurrowPostsTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(burrowPostsPage.transform, "BurrowPostsScrollableBody");

            AddText(body, font, "Burrow Post", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var postCard = CreateCard(body, "BurrowPostsCard", 310f);
            burrowPostsStatusText = AddText(postCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 148f);
            readUtilityPostButton = AddButton(postCard.transform, font, "Read Root Twine Run", OnReadUtilityPostPressed, out readUtilityPostLabel);
            readGretaIntroPostButton = AddButton(postCard.transform, font, "Read Greta Intro Post", OnReadGretaIntroPostPressed, out readGretaIntroPostLabel);

            var gretaCard = CreateCard(body, "GretaFollowupCard", 174f);
            AddText(gretaCard.transform, font, "Greta", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            completeGretaFollowupButton = AddButton(gretaCard.transform, font, "Complete Greta Follow-Up", OnCompleteGretaFollowupPressed, out completeGretaFollowupLabel);
            completeRootrailRevealButton = AddButton(gretaCard.transform, font, "Follow Greta to the Old Track", OnCompleteRootrailRevealPressed, out completeRootrailRevealLabel);

            var debugPanel = CreateCard(body, "BurrowPostsDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Burrow Post Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            burrowPostsDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildDailyDutiesPage(Font font)
        {
            var topNavCard = CreateCard(dailyDutiesPage.transform, "DailyDutiesTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(dailyDutiesPage.transform, "DailyDutiesScrollableBody");

            AddText(body, font, "Daily Duties", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var dutiesCard = CreateCard(body, "DailyDutiesCard", 360f);
            dailyDutiesStatusText = AddText(dutiesCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 316f);

            var debugPanel = CreateCard(body, "DailyDutiesDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Daily Duties Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            dailyDutiesDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildRootrailPage(Font font)
        {
            var topNavCard = CreateCard(rootrailPage.transform, "RootrailTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(rootrailPage.transform, "RootrailScrollableBody");

            AddText(body, font, "Rootrail Station", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var stationCard = CreateCard(body, "RootrailStationShellCard", 320f);
            rootrailStatusText = AddText(stationCard.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 276f);

            var debugPanel = CreateCard(body, "RootrailDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Rootrail Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            rootrailDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildLuckyDrawPage(Font font)
        {
            var topNavCard = CreateCard(luckyDrawPage.transform, "LuckyDrawTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(luckyDrawPage.transform, "LuckyDrawScrollableBody");

            AddText(body, font, "Lucky Draw Week", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var statusCard = CreateCard(body, "LuckyDrawStatusCard", 310f);
            luckyDrawStatusText = AddText(statusCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 148f);
            claimLuckyDrawWeeklyButton = AddButton(statusCard.transform, font, "Claim Weekly Lucky Draw", OnClaimLuckyDrawWeeklyPressed, out claimLuckyDrawWeeklyLabel);
            pullLuckyDrawButton = AddButton(statusCard.transform, font, "Pull Lucky Draw", OnPullLuckyDrawPressed, out pullLuckyDrawLabel);

            var ledgerCard = CreateCard(body, "FestivalLedgerFreeLaneCard", 270f);
            luckyDrawLedgerText = AddText(ledgerCard.transform, font, "", 19, FontStyle.Normal, TextAnchor.UpperLeft, 198f);
            claimFestivalLedgerButton = AddButton(ledgerCard.transform, font, "Claim Festival Ledger Reward", OnClaimFestivalLedgerPressed, out claimFestivalLedgerLabel);

            var stallCard = CreateCard(body, "LuckyStallCard", 390f);
            luckyDrawStallText = AddText(stallCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 166f);
            buyMooncapDrawButton = AddButton(stallCard.transform, font, "Buy Mooncap Lucky Draw", OnBuyMooncapDrawPressed, out buyMooncapDrawLabel);
            buyMaterialCacheButton = AddButton(stallCard.transform, font, "Buy Material Cache", OnBuyMaterialCachePressed, out buyMaterialCacheLabel);
            buyPolishBundleButton = AddButton(stallCard.transform, font, "Buy Polish Bundle", OnBuyPolishBundlePressed, out buyPolishBundleLabel);
            buyFestivalExchangeButton = AddButton(stallCard.transform, font, "Buy Festival Marks Exchange", OnBuyFestivalExchangePressed, out buyFestivalExchangeLabel);

            var debugPanel = CreateCard(body, "LuckyDrawDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Lucky Draw / Stall Debug", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            luckyDrawDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildCrackPage(Font font)
        {
            var topNavCard = CreateCard(crackPage.transform, "CrackTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(crackPage.transform, "CrackScrollableBody");

            AddText(body, font, "The Crack", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var statusCard = CreateCard(body, "CrackProbeCard", 390f);
            crackStatusText = AddText(statusCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 266f);
            probeCrackButton = AddButton(statusCard.transform, font, "Probe the Crack", OnProbeCrackPressed, out probeCrackLabel);

            var debugPanel = CreateCard(body, "CrackDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Crack Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            crackDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildCliquePage(Font font)
        {
            var topNavCard = CreateCard(cliquePage.transform, "CliqueTopNavigationCard", 84f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            var body = CreateScrollBody(cliquePage.transform, "CliqueScrollableBody");

            AddText(body, font, "Clique", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var statusCard = CreateCard(body, "CliqueShellCard", 520f);
            cliqueStatusText = AddText(statusCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 396f);
            claimCliqueStipendButton = AddButton(statusCard.transform, font, "Claim Local Clique Stipend", OnClaimCliqueStipendPressed, out claimCliqueStipendLabel);

            var debugPanel = CreateCard(body, "CliqueDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Clique Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            cliqueDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
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
                "Level: " + burrow.rootmine.level + "   Storage cap: " + burrow.rootmine.material_storage_cap + "\n" +
                "Stored: " + burrow.rootmine.stored_tangled_root_twine + " Twine / " +
                burrow.rootmine.stored_crumbled_ore_chunk + " Ore\n" +
                burrow.rootmine.status_note;

            fixtureSummaryText.text =
                "Fixture / Hat summary\n" +
                "Equipped: " + profile.fixture_state.equipped_fixture_instance_ids.Count + "/" + profile.account.fixture_cap +
                "   Owned Fixtures: " + profile.fixture_state.fixture_inventory.Count + "\n" +
                "Visible Hat: " + (string.IsNullOrEmpty(profile.hat_state.visible_hat_id) ? "None" : FixtureStateHelper.FirstHatDisplayName) +
                "   Power bonus: +" + FixtureStateHelper.GetTotalExpeditionPowerBonus(profile);

            socialSummaryText.text =
                "Greta: " + (profile.social_progress.greta.unlocked ? "Unlocked" : "Locked") +
                "   Trust: " + profile.social_progress.greta.trust_level +
                "   Rootrail: " + (profile.social_progress.rootrail.revealed ? "Revealed" : "Hidden") + "\n" +
                (luckyDrawVisible
                    ? "Lucky Draw: tickets " + profile.wallet.lucky_draws + ", pulls " + luckyDraw.pull_count
                    : "Lucky Draw hidden until Greta unlock") + "\n" +
                "Crack: " + (crack.visible ? "Depth " + crack.best_depth + ", Coins " + profile.wallet.crack_coins : "Hidden until Rootrail reveal") +
                "   Clique: " + (clique.visible ? clique.player_role + ", Favor " + profile.wallet.favor_marks : "Hidden until Rootrail reveal");

            fieldReturnsSnippetText.text = BuildFieldReturnsSnippet(loamwake.field_returns);

            burrowDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Guide step: " + profile.tutorial_progress.current_step_id + "\n" +
                "Guide completed: " + profile.tutorial_progress.completed_step_ids.Count;

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
                "First Warden: The Mudgrip\n" +
                "Status: " + (loamwake.keeper_lw_001_defeated ? "Defeated" : (loamwake.keeper_lw_001_unlocked ? "Unlocked" : "Locked")) + "\n" +
                "Zone: Mudpipe Hollow\n" +
                "Auto-Clash threshold: 28";
            keeperButton.interactable = loamwake.keeper_lw_001_unlocked && !loamwake.keeper_lw_001_defeated && profile.wallet.mushcaps >= 2;
            keeperButtonLabel.text = loamwake.keeper_lw_001_defeated ? "Warden Defeated" : "Challenge The Mudgrip (2 Mushcaps)";

            loamwakeFieldReturnsText.text = BuildFieldReturnsDetail(loamwake.field_returns);

            loamwakeDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Current stratum: " + (string.IsNullOrEmpty(snapshot.current_stratum_id) ? "burrow" : snapshot.current_stratum_id) + "\n" +
                "The Mudgrip defeated: " + loamwake.keeper_lw_001_defeated + "\n" +
                "Active UID: " + snapshot.active_uid + "\n" +
                "Save path: " + snapshot.save_file_path;

            fixtureWalletText.text =
                "Mooncaps: " + snapshot.mooncaps +
                "   Tangled Root Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                "   Crumbled Ore Chunk: " + profile.wallet.loamwake_materials.crumbled_ore_chunk + "\n" +
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
                "Hat Collection shell\n" +
                "Loamwake Dirt Cap: " + (FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId) ? "Unlocked" : "Locked until The Mudgrip is defeated") + "\n" +
                "Visible Hat: " + (string.IsNullOrEmpty(profile.hat_state.visible_hat_id) ? "None" : FixtureStateHelper.FirstHatDisplayName) + "\n" +
                profile.hat_state.passive_summary;
            fixtureDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Fixture recipe IDs: " + profile.fixture_state.crafted_recipe_ids.Count + "\n" +
                "Fixture cap model: ordered list, no rigid slots\n" +
                "Save path: " + snapshot.save_file_path;

            vaultStatusText.text =
                "Vault state: " + (profile.vault_state.visible ? "Visible shell" : "Locked shell") + "\n" +
                profile.vault_state.status_note + "\n\n" +
                "Treasures not implemented in Sprint 3.\n" +
                "Set / Polish / Strengthen / Attune are intentionally absent.";
            vaultDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Treasure progression enabled: " + profile.vault_state.treasure_progression_enabled + "\n" +
                "Owned treasure count: " + profile.vault_state.owned_treasure_count + "\n" +
                "Save path: " + snapshot.save_file_path;

            burrowPostsStatusText.text = BuildBurrowPostsStatus(profile);
            burrowPostsDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Latest social result: " + profile.social_progress.latest_result_summary + "\n" +
                "Greta first follow-up: " + profile.social_progress.greta.first_followup_completed + "\n" +
                "Save path: " + snapshot.save_file_path;

            dailyDutiesStatusText.text = BuildDailyDutiesStatus(profile);
            dailyDutiesDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Daily Duty count: " + profile.social_progress.daily_duties.Count + "\n" +
                "Latest social result: " + profile.social_progress.latest_result_summary + "\n" +
                "Save path: " + snapshot.save_file_path;

            rootrailStatusText.text =
                "Rootrail Terminal - Loamwake Station\n" +
                "Reveal state: " + (profile.social_progress.rootrail.revealed ? "Revealed" : "Hidden") + "\n" +
                "Station shell: " + (profile.social_progress.rootrail.station_visible ? "Visible" : "Locked") + "\n" +
                "Repair progression: " + (profile.social_progress.rootrail.repair_progression_enabled ? "Enabled" : "Not implemented in Sprint 4") + "\n" +
                "Forgotten Manual: Loamwake Terminal Routing Manual - Not in Codex\n" +
                profile.social_progress.rootrail.status_note;
            rootrailDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Repair timer started: " + profile.social_progress.rootrail.repair_timer_started + "\n" +
                "Current repair step: " + (string.IsNullOrEmpty(profile.social_progress.rootrail.current_step_id) ? "none" : profile.social_progress.rootrail.current_step_id) + "\n" +
                "Rootrail Parts: " + profile.wallet.rootrail_parts;

            luckyDrawStatusText.text =
                "Lucky Draw Week: Loose Lots\n" +
                "State: " + (luckyDraw.active ? "Active" : (luckyDraw.unlocked ? "Unlocked" : "Locked/hidden")) + "\n" +
                "Gate: " + luckyDraw.unlock_gate_note + " - " + (luckyDraw.unlock_gate_met ? "met" : "not met") + "\n" +
                "Lucky Draw tickets: " + profile.wallet.lucky_draws + "   Pulls: " + luckyDraw.pull_count + "\n" +
                "Weekly claim: " + (luckyDraw.weekly_claimed ? "claimed" : "ready") +
                "   Activity source: " + (luckyDraw.activity_ticket_claimed ? "claimed" : luckyDraw.activity_progress + "/3") + "\n" +
                "Latest pull: " + luckyDraw.latest_pull_result;
            luckyDrawLedgerText.text = LuckyDrawEventService.BuildLedgerSummary(profile);
            luckyDrawStallText.text =
                LuckyDrawEventService.BuildStallSummary(profile) + "\n" +
                "Mooncaps: " + profile.wallet.mooncaps +
                "   Festival Marks: " + profile.wallet.festival_marks +
                "   Polishes: " + profile.wallet.polishes;
            luckyDrawDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Week marker: " + luckyDraw.week_marker + "\n" +
                "Paid path active: " + luckyDraw.paid_path_active + "   IAP enabled: " + luckyDraw.iap_enabled + "\n" +
                "Latest event result: " + luckyDraw.latest_result_summary;

            crackStatusText.text =
                "Endless descent prototype shell\n" +
                "State: " + (crack.unlocked ? "Unlocked" : (crack.visible ? "Visible shell" : "Locked/hidden")) + "\n" +
                "Gate: " + crack.unlock_gate_note + " - " + (crack.unlock_gate_met ? "met" : "not met") + "\n" +
                "Current depth: " + crack.current_depth + "   Best depth: " + crack.best_depth + "\n" +
                "Probe count: " + crack.probe_count + "   Crack Coins: " + profile.wallet.crack_coins + "\n" +
                "Reward summary: " + crack.reward_claim_summary + "\n" +
                "Latest result: " + crack.latest_result_summary + "\n" +
                "Only Probe the Crack is implemented in this sprint.";
            crackDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Visible: " + crack.visible + "   Unlocked: " + crack.unlocked + "\n" +
                "Latest Crack result: " + crack.latest_result_summary + "\n" +
                "Save path: " + snapshot.save_file_path;

            cliqueStatusText.text =
                "Local social shell\n" +
                "State: " + (clique.unlocked ? "Unlocked" : (clique.visible ? "Visible shell" : "Locked/hidden")) + "\n" +
                "Gate: " + clique.unlock_gate_note + " - " + (clique.unlock_gate_met ? "met" : "not met") + "\n" +
                "Clique name: " + clique.clique_name + "\n" +
                "Your role: " + clique.player_role + "   Favor Marks: " + profile.wallet.favor_marks + "\n" +
                "Clique Rolls:\n" + BuildCliqueRosterSummary(clique) +
                "Great Dispute: stub notice only; no gameplay is implemented.\n" +
                "Latest result: " + clique.latest_result_summary;
            cliqueDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Networking enabled: " + clique.networking_enabled + "\n" +
                "Multiplayer enabled: " + clique.multiplayer_enabled + "   Shared state enabled: " + clique.shared_state_enabled + "\n" +
                "Great Dispute stub only: " + clique.great_dispute_stub_only;

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
                displayName + "\n" +
                "Status: " + (unlocked ? "Unlocked" : "Locked") + "\n" +
                "Difficulty: " + difficulty + "\n" +
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
                return "Field Returns\nNo Loamwake runs recorded yet.";
            }

            return "Field Returns\n" +
                result.last_zone_id + " via " + result.route_id + "\n" +
                "Result: " + result.result + "   Mooncaps: +" + result.mooncaps + "   Mushcaps: +" + result.mushcaps;
        }

        private static string BuildFieldReturnsDetail(ExplorationResultData result)
        {
            if (result == null || string.IsNullOrEmpty(result.last_zone_id))
            {
                return "Latest result\nNo Loamwake exploration recorded yet.";
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

            return "Latest result\n" +
                "Zone: " + result.last_zone_id + "\n" +
                "Route: " + result.route_id + "   Result: " + result.result + "\n" +
                "Mooncaps: +" + result.mooncaps + "   Mushcaps: +" + result.mushcaps + "\n" +
                materialLine;
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
            image.color = new Color(0.08f, 0.1f, 0.09f, 0.96f);

            var layout = page.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(18, 18, 18, 18);
            layout.spacing = 8;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return page;
        }

        private static GameObject CreateCard(Transform parent, string name, float height)
        {
            var card = CreatePanel(parent, name, new Color(0.14f, 0.18f, 0.16f, 1f));
            var rect = card.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, height);

            var layoutElement = card.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = height;

            var layout = card.AddComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(12, 12, 12, 12);
            layout.spacing = 6;
            layout.childControlWidth = true;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childForceExpandHeight = false;

            return card;
        }

        private static GameObject CreateButtonRow(Transform parent, string name)
        {
            var row = new GameObject(name, typeof(RectTransform));
            row.transform.SetParent(parent, false);

            var rect = row.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 48f);

            var layoutElement = row.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 48f;

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
            var scrollObject = CreatePanel(parent, name, new Color(0.1f, 0.12f, 0.11f, 0.35f));
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
            layout.spacing = 8;
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
            var buttonObject = CreatePanel(parent, label, new Color(0.18f, 0.23f, 0.2f, 1f));
            var button = buttonObject.AddComponent<Button>();
            if (onClick != null)
            {
                button.onClick.AddListener(onClick);
            }

            var colors = button.colors;
            colors.highlightedColor = new Color(0.24f, 0.32f, 0.27f, 1f);
            colors.pressedColor = new Color(0.12f, 0.16f, 0.14f, 1f);
            colors.disabledColor = new Color(0.14f, 0.16f, 0.15f, 0.8f);
            button.colors = colors;

            var rect = buttonObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0f, 48f);

            var layout = buttonObject.AddComponent<LayoutElement>();
            layout.preferredHeight = 48f;

            labelText = AddText(buttonObject.transform, font, label, 20, FontStyle.Bold, TextAnchor.MiddleCenter, 48f);
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
