using HarmonyLib;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(RigManager))]
[HarmonyPatch(nameof(RigManager.Start))]
internal static class RigManager_Start
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(RigManager __instance)
    {
        // Switch spectator camera to passthrough so the spectator camera can work.
        Control_Player controlPlayer = __instance.uiRig.controlPlayer;
        DataManager.Settings._spectatorSettings._spectatorCameraMode = SpectatorCameraMode.Passthrough;
        controlPlayer.UpdateSpectator();
        
        FreecamHostManager.CreateFreecam(__instance.ControllerRig.m_head);
    }
}