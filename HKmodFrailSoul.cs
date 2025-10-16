using BepInEx;
using BepInEx.Logging;
using GlobalEnums;
using HarmonyLib;
using UnityEngine;

namespace HKmodFrailSoul
{

    [BepInPlugin("com.github.hkmod.frailsoul", "Frail Soul", "1.0.0")]
    public class HKmodFrailSoulPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Frail Soul mod loaded.");
            Harmony.CreateAndPatchAll(typeof(HKmodFrailSoulPlugin), null);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerData), "TakeHealth")]
        private static void TakeHealthPostfix(PlayerData __instance)
        {
            if (__instance.permadeathMode == PermadeathModes.On)
            {
                __instance.health = 0;
            }
        }

        [HarmonyPatch(typeof(GameManager), "PlayerDead")]
        [HarmonyPrefix]
        private static void Patch_PlayerDead_Prefix(GameManager __instance)
        {
            if (__instance.playerData.permadeathMode == PermadeathModes.Dead)
            {
                __instance.playerData.permadeathMode = PermadeathModes.On;
            }
        }
    }
}
