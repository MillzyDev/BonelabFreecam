using HarmonyLib;
using SLZ.Rig;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(OpenControllerRig))]
[HarmonyPatch(nameof(OpenControllerRig.OnStart))]
internal static class OpenControllerRig_Patch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(OpenControllerRig __instance)
    {
        __instance._isControllerRigPaused = true;
    }
}