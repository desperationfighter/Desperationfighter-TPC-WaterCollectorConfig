using HarmonyLib;
using SpaceCraft;
using static UnityEngine.Awaitable;
using System.Collections.ObjectModel;

namespace WaterCollectorConfig.Patches
{
    [HarmonyPatch(typeof(MachineGenerator))]
    [HarmonyPatch(nameof(MachineGenerator.SetGeneratorInventory))]
    static class MachineGenerator_SetGeneratorInventory_Patch
    {
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

            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Instance Name> {__instance.name}");
            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - before");

            if (__instance.name.Contains("WaterCollector1"))
            {
                Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Set SkyWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.SkyWaterCollector_Interval.Value;
            }
            else if(__instance.name.Contains("WaterCollector2"))
            {
                Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Set LakeWaterCollector_Interval Intervall");
                __instance.spawnEveryXSec = Plugin.LakeWaterCollector_Interval.Value;
            }

            Plugin.MyDebugLogger($"prefix (MachineGenerator.SetGeneratorInventory) : Intervall Spawn set to - {__instance.spawnEveryXSec} - after");
        }
    }
}
