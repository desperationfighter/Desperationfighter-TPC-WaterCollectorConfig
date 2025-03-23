using HarmonyLib;
using SpaceCraft;

namespace WaterCollectorConfig.Patches
{
    [HarmonyPatch(typeof(MachineGenerator))]
    [HarmonyPatch(nameof(MachineGenerator.SetGeneratorInventory))]
    static class MachineGenerator_SetGeneratorInventory_Patch
    {
        //normally you should only need post or prefix however i do both as on longterm runs you need to wait first cycle to adapt the new setting thats why i also use prefix. Maybe a bit ressource hungry???
        [HarmonyPostfix]
        static void Postfix(MachineGenerator __instance)
        {
            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Instance Name> WaterCollector1(Clone)[/ Debug]
            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to -150 - before[/ Debug]
            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Instance Name> WaterCollector2(Clone)[/ Debug]
            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to -100 - before[/ Debug]

            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Instance Name> WaterLifeCollector1(Clone)[/ Debug]
            //[Info: Water Collector Config][Water Collector Config][Debug] prefix(MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to -60 - before[/ Debug]

            if (!Plugin.ModisActive.Value) return;
            if (!__instance.name.Contains("Water")) return;

            Plugin.MyDebugLogger($"Postfix (MachineGenerator.SetGeneratorInventory) : Instance Name> {__instance.name}");
            Plugin.MyDebugLogger($"Postfix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - before");

            if (__instance.name.Contains("WaterCollector1"))
            {
                Plugin.MyDebugLogger($"Postfix (MachineGenerator.SetGeneratorInventory) : Set SkyWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.SkyWaterCollector_Interval.Value;
            }
            else if(__instance.name.Contains("WaterCollector2"))
            {
                Plugin.MyDebugLogger($"Postfix (MachineGenerator.SetGeneratorInventory) : Set LakeWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.LakeWaterCollector_Interval.Value;
            }

            Plugin.MyDebugLogger($"Postfix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - after");
        }

        [HarmonyPrefix]
        static bool Prefix(MachineGenerator __instance)
        {
            if (!Plugin.ModisActive.Value) return true;
            if (!__instance.name.Contains("Water")) return true;

            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Instance Name> {__instance.name}");
            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - before");

            if (__instance.name.Contains("WaterCollector1"))
            {
                Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Set SkyWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.SkyWaterCollector_Interval.Value;
            }
            else if (__instance.name.Contains("WaterCollector2"))
            {
                Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Set LakeWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.LakeWaterCollector_Interval.Value;
            }

            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - after");

            return true;
        }
    }
}
