using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.IO;

namespace NoExistentialCrisis
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "InfernoDragon0.cotl.NoExistentialCrisis";
        public const string PluginName = "NoExistentialCrisis";
        public const string PluginVer = "1.0.0";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;
        internal static ConfigEntry<float> cursedChance;
        internal static ConfigEntry<float> crisisChance;

        private void Awake()
        {
            Plugin.Log = base.Logger;
            PluginPath = Path.GetDirectoryName(Info.Location);

            cursedChance = Config.Bind("NoExistentialCrisis", "CursedChance", 0.0f, "Chance for the follower to be cursed upon resurrection. 0.0 for never, 1.0 for always.");
            crisisChance = Config.Bind("NoExistentialCrisis", "CrisisChance", 0.0f, "Chance for the follower to have an existential crisis upon resurrection. 0.0 for never, 1.0 for always.");

        }

        private void OnEnable()
        {
            Harmony.PatchAll();
            Logger.LogInfo($"Loaded {PluginName}!");
        }

        private void OnDisable()
        {
            Harmony.UnpatchSelf();
            Logger.LogInfo($"Unloaded {PluginName}!");
        }
    }
}