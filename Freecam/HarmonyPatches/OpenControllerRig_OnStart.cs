using HarmonyLib;
using SLZ.Rig;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(OpenControllerRig))]
[HarmonyPatch(nameof(OpenControllerRig.OnStart))]
internal static class OpenControllerRig_OnStart
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(OpenControllerRig __instance)
    {
        // The game won't unpause unless its already paused (slz tryna make my life difficult smh)
        __instance._isControllerRigPaused = true;
    }
}