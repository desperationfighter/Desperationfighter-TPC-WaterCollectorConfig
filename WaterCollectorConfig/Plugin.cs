using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace WaterCollectorConfig
{
    [BepInPlugin(GUID,Name,Version)]
    public class Plugin : BaseUnityPlugin
    {
        public new static ManualLogSource Logger { get; private set; }
        public const string GUID = "Desperationfighter.TPC.WaterCollectorConfig";
        public const string Name = "Water Collector Config";
        public const string Version = "1.0.0.0"; //Remmber to Update Assembly Version too !

        public static ConfigEntry<bool> ModisActive;
        public static ConfigEntry<int> SkyWaterCollector_Interval;
        public static ConfigEntry<int> LakeWaterCollector_Interval;
        public static ConfigEntry<bool> Debuglogging;

        private void Awake()
        {
            ModisActive = Config.Bind("1_General", "ModisActive", true, "Set if the Mod should running or not. If you don't want to remove Files or for Later Ingame Menu. Please reload your Savegame after Change as there are Setting that only apply once when World is loaded up.");
            SkyWaterCollector_Interval = Config.Bind("2_General-Config", "SkyWaterCollector_Interval", 150, "Affect Tier 1 Sky Water Collector -> Default value in Game = 150; Time span for Generating a Waterbottle");
            LakeWaterCollector_Interval = Config.Bind("2_General-Config", "LakeWaterCollector_Interval", 100, "Affect Tier 2 Lake Water Collector -> Default value in Game = 100; Time span for Generating a Waterbottle");
            Debuglogging = Config.Bind("9_Advanced", "Debuglogging", false, "Enables Debug Logging. Should be only activated when you know what you do.");

            // set project-scoped logger instance
            Logger = base.Logger;

            // register harmony patches, if there are any
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            Logger.LogInfo($"Plugin {GUID} is loaded!");
        }

        public static void MyDebugLogger(string message)
        { 
            if(Debuglogging.Value)
            {
                Logger.LogDebug( $"[{Name}][Debug] : {message} [/Debug]" );
            }
        }
    }
}
