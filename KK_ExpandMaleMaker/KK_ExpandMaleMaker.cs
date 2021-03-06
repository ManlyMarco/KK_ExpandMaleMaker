using BepInEx;
using HarmonyLib;
using UnityEngine;
using KKAPI.Chara;
using System.Linq;
using BepInEx.Harmony;
using BepInEx.Configuration;

namespace KK_ExpandMaleMaker {

    [BepInPlugin(GUID, Name, Version)]
    [BepInDependency("marco.kkapi")]
    public class KK_ExpandMaleMaker : BaseUnityPlugin {

        public const string Name = "Expand Male Maker";
        public const string GUID = "kokaiinum.KKExpandMaleMaker";
        public const string Version = "1.0";

        internal static MonoBehaviour instance;

        internal static ConfigEntry<bool> heightEnabled;
        internal static ConfigEntry<bool> hairEnabled;
        internal static ConfigEntry<bool> compatabilityMode;

        private void Awake() {
            heightEnabled = Config.AddSetting("Config", "Edit Male Height", true, new ConfigDescription("If the male height can be edited. \nRequires passing a loading screen to take effect. \nDisabling this will also prevent any male cards from\nbeing loaded with edited heights.", null, new ConfigurationManagerAttributes { Order = 3 }));

            hairEnabled = Config.AddSetting("Config", "Edit Male Underhair", true, new ConfigDescription("If the male underhair can be edited. \n Requires passing a loading screen to take effect.", null, new ConfigurationManagerAttributes { Order = 2 }));

            compatabilityMode = Config.AddSetting("Config", "Compatability Mode", true, new ConfigDescription("When enabled, male characters not specifically saved with edited\nheights will be defaulted to 60 (the default behaviour for the game).", null, new ConfigurationManagerAttributes { IsAdvanced = true, Order = 1 }));

            instance = this;

            Hooks.isDark = typeof(ChaControl).GetProperties(AccessTools.all).Any(p => p.Name == "exType");

            CharacterApi.RegisterExtraBehaviour<MaleHeightCompatabilityController>(GUID);

            HarmonyWrapper.PatchAll(typeof(Hooks));
        }
    }

}

