using Freecam.Configuration;
using HarmonyLib;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(RigManager))]
[HarmonyPatch(nameof(RigManager.Start))]
internal static class RigManager_Start
{
    private static readonly Config s_config = Config.Instance;
    
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(RigManager __instance)
    {
        if (s_config.FreecamEnabled)
        {
            // Switch spectator camera to passthrough, if its enabled initially
            Control_Player controlPlayer = __instance.uiRig.controlPlayer;
            DataManager.Settings._spectatorSettings._spectatorCameraMode = SpectatorCameraMode.Passthrough;
            controlPlayer.UpdateSpectator();
        }
        
        FreecamHostManager.CreateFreecam(__instance);
    }
}