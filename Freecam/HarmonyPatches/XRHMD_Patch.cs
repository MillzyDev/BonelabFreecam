using HarmonyLib;
using MelonLoader;
using SLZ.Marrow.Input;

namespace Freecam.HarmonyPatches;

// ReSharper disable once InconsistentNaming
[HarmonyPatch(typeof(XRHMD))]
[HarmonyPatch(nameof(XRHMD.IsUserPresent), MethodType.Getter)]
internal static class XRHMD_Patch
{
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    // ReSharper disable once UnusedMember.Local
    [HarmonyPrefix]
    private static bool Prefix(ref bool __result)
    {
        __result = true;
        return false;
    }
}