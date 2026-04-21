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
            Vault
        }

        private ProfileService profileService;
        private SaveManager saveManager;
        private AuthManager authManager;

        private ScreenPage currentScreen = ScreenPage.Burrow;

        private GameObject burrowPage;
        private GameObject loamwakePage;
        private GameObject fixturePage;
        private GameObject vaultPage;

        private Text burrowWalletText;
        private Text burrowActionText;
        private Text dewpondText;
        private Text mushpatchText;
        private Text burrowStatusText;
        private Text rootmineText;
        private Text fixtureSummaryText;
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

        private Text enterLoamwakeButtonLabel;
        private Text ledgerhollowButtonLabel;
        private Text memoryFenButtonLabel;

        private Button dewpondGatherButton;
        private Button mushpatchGatherButton;
        private Button rootmineGatherButton;
        private Button expandButton;
        private Button openFixtureButton;
        private Button openVaultButton;
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

            BuildBurrowPage(font);
            BuildLoamwakePage(font);
            BuildFixturePage(font);
            BuildVaultPage(font);
        }

        private void BuildBurrowPage(Font font)
        {
            AddText(burrowPage.transform, font, "The Burrow", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            burrowWalletText = AddText(burrowPage.transform, font, "", 28, FontStyle.Bold, TextAnchor.MiddleLeft, 42f);
            burrowActionText = AddText(burrowPage.transform, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 52f);

            var dewpondCard = CreateCard(burrowPage.transform, "DewpondCard", 140f);
            dewpondText = AddText(dewpondCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 70f);
            Text unusedLabel;
            dewpondGatherButton = AddButton(dewpondCard.transform, font, "Gather Dewpond", OnGatherDewpondPressed, out unusedLabel);

            var mushpatchCard = CreateCard(burrowPage.transform, "MushpatchCard", 140f);
            mushpatchText = AddText(mushpatchCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 70f);
            mushpatchGatherButton = AddButton(mushpatchCard.transform, font, "Gather Mushpatch", OnGatherMushpatchPressed, out unusedLabel);

            var burrowCard = CreateCard(burrowPage.transform, "BurrowStatusCard", 150f);
            burrowStatusText = AddText(burrowCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 76f);
            expandButton = AddButton(burrowCard.transform, font, "Expand Burrow", OnExpandPressed, out expandButtonLabel);

            var rootmineCard = CreateCard(burrowPage.transform, "RootmineCard", 300f);
            rootmineText = AddText(rootmineCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 76f);
            rootmineGatherButton = AddButton(rootmineCard.transform, font, "Gather Rootmine", OnGatherRootminePressed, out rootmineGatherLabel);
            openFixtureButton = AddButton(rootmineCard.transform, font, "Fixture Workshop", OnOpenFixturePressed, out unusedLabel);
            openVaultButton = AddButton(rootmineCard.transform, font, "Vault of Treasures", OnOpenVaultPressed, out unusedLabel);

            var fixtureSummaryCard = CreateCard(burrowPage.transform, "FixtureSummaryCard", 106f);
            fixtureSummaryText = AddText(fixtureSummaryCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 62f);

            var strataGateCard = CreateCard(burrowPage.transform, "StrataGateCard", 194f);
            AddText(strataGateCard.transform, font, "Strata Gate", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            enterLoamwakeButton = AddButton(strataGateCard.transform, font, "Enter Loamwake", OnEnterLoamwakePressed, out enterLoamwakeButtonLabel);
            ledgerhollowButton = AddButton(strataGateCard.transform, font, "Ledgerhollow (Unavailable)", null, out ledgerhollowButtonLabel);
            memoryFenButton = AddButton(strataGateCard.transform, font, "Memory Fen (Unavailable)", null, out memoryFenButtonLabel);

            var returnsCard = CreateCard(burrowPage.transform, "FieldReturnsCard", 78f);
            fieldReturnsSnippetText = AddText(returnsCard.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 42f);

            var saveActionsCard = CreateCard(burrowPage.transform, "SaveActionsCard", 150f);
            AddText(saveActionsCard.transform, font, "Debug actions", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            AddButton(saveActionsCard.transform, font, "Force Save", OnForceSavePressed, out unusedLabel);
            AddButton(saveActionsCard.transform, font, "Reload Save", OnReloadPressed, out unusedLabel);

            var debugPanel = CreateCard(burrowPage.transform, "BurrowDebugPanel", 170f);
            AddText(debugPanel.transform, font, "Burrow Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            burrowDebugText = AddText(debugPanel.transform, font, "", 18, FontStyle.Normal, TextAnchor.UpperLeft, 104f);
        }

        private void BuildLoamwakePage(Font font)
        {
            var topNavCard = CreateCard(loamwakePage.transform, "LoamwakeTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            AddText(loamwakePage.transform, font, "Loamwake", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            loamwakeWalletText = AddText(loamwakePage.transform, font, "", 28, FontStyle.Bold, TextAnchor.MiddleLeft, 42f);
            loamwakeActionText = AddText(loamwakePage.transform, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 52f);

            var zone1Card = CreateCard(loamwakePage.transform, "Zone1Card", 220f);
            zone1Text = AddText(zone1Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone1SafeButton = AddButton(zone1Card.transform, font, "Safe Route", OnZone1SafePressed, out zone1SafeLabel);
            zone1RiskyButton = AddButton(zone1Card.transform, font, "Risky Route", OnZone1RiskyPressed, out zone1RiskyLabel);

            var zone2Card = CreateCard(loamwakePage.transform, "Zone2Card", 220f);
            zone2Text = AddText(zone2Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone2SafeButton = AddButton(zone2Card.transform, font, "Safe Route", OnZone2SafePressed, out zone2SafeLabel);
            zone2RiskyButton = AddButton(zone2Card.transform, font, "Risky Route", OnZone2RiskyPressed, out zone2RiskyLabel);

            var zone3Card = CreateCard(loamwakePage.transform, "Zone3Card", 220f);
            zone3Text = AddText(zone3Card.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 98f);
            zone3SafeButton = AddButton(zone3Card.transform, font, "Safe Route", OnZone3SafePressed, out zone3SafeLabel);
            zone3RiskyButton = AddButton(zone3Card.transform, font, "Risky Route", OnZone3RiskyPressed, out zone3RiskyLabel);

            var keeperCard = CreateCard(loamwakePage.transform, "KeeperCard", 176f);
            keeperText = AddText(keeperCard.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            keeperButton = AddButton(keeperCard.transform, font, "Challenge Keeper", OnKeeperPressed, out keeperButtonLabel);

            var returnsCard = CreateCard(loamwakePage.transform, "LoamwakeReturnsCard", 150f);
            loamwakeFieldReturnsText = AddText(returnsCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 104f);

            var navCard = CreateCard(loamwakePage.transform, "NavigationCard", 150f);
            AddText(navCard.transform, font, "Navigation", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            AddButton(navCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);
            AddButton(navCard.transform, font, "Force Save", OnForceSavePressed, out unusedLabel);

            var debugPanel = CreateCard(loamwakePage.transform, "LoamwakeDebugPanel", 240f);
            AddText(debugPanel.transform, font, "Loamwake Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            loamwakeDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 170f);
        }

        private void BuildFixturePage(Font font)
        {
            var topNavCard = CreateCard(fixturePage.transform, "FixtureTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            AddText(fixturePage.transform, font, "Fixture Workshop", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);
            fixtureWalletText = AddText(fixturePage.transform, font, "", 24, FontStyle.Bold, TextAnchor.MiddleLeft, 64f);
            fixtureActionText = AddText(fixturePage.transform, font, "", 22, FontStyle.Normal, TextAnchor.MiddleLeft, 48f);

            var craftCard = CreateCard(fixturePage.transform, "FirstFixtureCraftCard", 240f);
            fixtureCraftText = AddText(craftCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            unlockFixtureCapButton = AddButton(craftCard.transform, font, "Unlock First Fixture Cap", OnUnlockFixtureCapPressed, out unlockFixtureCapLabel);
            craftFirstFixtureButton = AddButton(craftCard.transform, font, "Craft Root-Bitten Shovel Strap", OnCraftFirstFixturePressed, out craftFirstFixtureLabel);

            var inventoryCard = CreateCard(fixturePage.transform, "FixtureInventoryCard", 240f);
            fixtureInventoryText = AddText(inventoryCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            equipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Equip First Fixture", OnEquipFirstFixturePressed, out equipFirstFixtureLabel);
            unequipFirstFixtureButton = AddButton(inventoryCard.transform, font, "Unequip First Fixture", OnUnequipFirstFixturePressed, out unequipFirstFixtureLabel);

            var hatCard = CreateCard(fixturePage.transform, "HatShellCard", 176f);
            fixtureHatText = AddText(hatCard.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 88f);
            setFirstHatVisibleButton = AddButton(hatCard.transform, font, "Set Visible Hat", OnSetFirstHatVisiblePressed, out setFirstHatVisibleLabel);

            var debugPanel = CreateCard(fixturePage.transform, "FixtureDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Fixture Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            fixtureDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
        }

        private void BuildVaultPage(Font font)
        {
            var topNavCard = CreateCard(vaultPage.transform, "VaultTopNavigationCard", 94f);
            Text unusedLabel;
            AddButton(topNavCard.transform, font, "Back to Burrow", OnBackToBurrowPressed, out unusedLabel);

            AddText(vaultPage.transform, font, "Vault of Treasures", 42, FontStyle.Bold, TextAnchor.MiddleLeft, 56f);

            var shellCard = CreateCard(vaultPage.transform, "VaultShellCard", 240f);
            vaultStatusText = AddText(shellCard.transform, font, "", 22, FontStyle.Normal, TextAnchor.UpperLeft, 196f);

            var debugPanel = CreateCard(vaultPage.transform, "VaultDebugPanel", 220f);
            AddText(debugPanel.transform, font, "Vault Debug / Status", 22, FontStyle.Bold, TextAnchor.MiddleLeft, 34f);
            vaultDebugText = AddText(debugPanel.transform, font, "", 20, FontStyle.Normal, TextAnchor.UpperLeft, 150f);
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

            burrowWalletText.text = "Mooncaps: " + snapshot.mooncaps + "   Mushcaps: " + snapshot.mushcaps +
                "   Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                "   Ore: " + profile.wallet.loamwake_materials.crumbled_ore_chunk;
            burrowActionText.text = profileService.LastActionStatus;

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

            fieldReturnsSnippetText.text = BuildFieldReturnsSnippet(loamwake.field_returns);

            burrowDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Last production tick: " + snapshot.last_production_tick + "\n" +
                "Active UID: " + snapshot.active_uid + "\n" +
                "Save path: " + snapshot.save_file_path;

            loamwakeWalletText.text =
                "Mushcaps: " + snapshot.mushcaps + "   Mooncaps: " + snapshot.mooncaps + "\n" +
                "Materials - Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                " / Ore: " + profile.wallet.loamwake_materials.crumbled_ore_chunk +
                " / Glow: " + profile.wallet.loamwake_materials.dull_glow_shard;
            loamwakeActionText.text = profileService.LastActionStatus;

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
                "First Keeper\n" +
                "Status: " + (loamwake.keeper_lw_001_defeated ? "Defeated" : (loamwake.keeper_lw_001_unlocked ? "Unlocked" : "Locked")) + "\n" +
                "Zone: Glowroot Passage\n" +
                "Auto-Clash threshold: 28";
            keeperButton.interactable = loamwake.keeper_lw_001_unlocked && !loamwake.keeper_lw_001_defeated && profile.wallet.mushcaps >= 2;
            keeperButtonLabel.text = loamwake.keeper_lw_001_defeated ? "Keeper Defeated" : "Challenge Keeper (2 Mushcaps)";

            loamwakeFieldReturnsText.text = BuildFieldReturnsDetail(loamwake.field_returns);

            loamwakeDebugText.text =
                "Auth state: " + snapshot.auth_state + "\n" +
                "Save state: " + snapshot.save_state + "\n" +
                "Current stratum: " + (string.IsNullOrEmpty(snapshot.current_stratum_id) ? "burrow" : snapshot.current_stratum_id) + "\n" +
                "Keeper defeated: " + loamwake.keeper_lw_001_defeated + "\n" +
                "Active UID: " + snapshot.active_uid + "\n" +
                "Save path: " + snapshot.save_file_path;

            fixtureWalletText.text =
                "Mooncaps: " + snapshot.mooncaps +
                "   Tangled Root Twine: " + profile.wallet.loamwake_materials.tangled_root_twine +
                "   Crumbled Ore Chunk: " + profile.wallet.loamwake_materials.crumbled_ore_chunk + "\n" +
                "Fixture cap: " + profile.fixture_state.equipped_fixture_instance_ids.Count + "/" + profile.account.fixture_cap;
            fixtureActionText.text = profileService.LastActionStatus;
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
                "Loamwake Dirt Cap: " + (FixtureStateHelper.HasHat(profile, FixtureStateHelper.FirstHatId) ? "Unlocked" : "Locked until Keeper defeat") + "\n" +
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

            enterLoamwakeButton.interactable = profileService.IsStratumSelectable(LoamwakeExplorationService.StratumId);
            enterLoamwakeButtonLabel.text = "Enter Loamwake";
            ledgerhollowButton.interactable = false;
            ledgerhollowButtonLabel.text = "Ledgerhollow (Unavailable)";
            memoryFenButton.interactable = false;
            memoryFenButtonLabel.text = "Memory Fen (Unavailable)";
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
            layout.childControlHeight = false;
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
            rect.sizeDelta = new Vector2(0f, 58f);

            var layout = buttonObject.AddComponent<LayoutElement>();
            layout.preferredHeight = 58f;

            labelText = AddText(buttonObject.transform, font, label, 22, FontStyle.Bold, TextAnchor.MiddleCenter, 58f);
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
