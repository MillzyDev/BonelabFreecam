using HarmonyLib;
using SLZ.Marrow.Input;

namespace Freecam.HarmonyPatches;

// ReSharper disable once InconsistentNaming
[HarmonyPatch(typeof(XRHMD))]
[HarmonyPatch(nameof(XRHMD.IsUserPresent))]
internal static class XRHMD_Patch
{
    [HarmonyPostfix]
    private static void Postfix(bool __result)
    {
        __result = true;
    }
}