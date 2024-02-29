using Freecam.Configuration;
using HarmonyLib;
using SLZ.Rig;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(OpenControllerRig))]
[HarmonyPatch(nameof(OpenControllerRig.OnEarlyUpdate))]
internal static class OpenControllerRig_OnEarlyUpdate
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static bool Prefix(OpenControllerRig __instance)
    {
        // The game won't unpause unless its already paused (slz tryna make my life difficult smh)
        if (!Config.Instance.NoHmd)
            return true;
        
        __instance._isControllerRigPaused = true;
        return true;
    }
}