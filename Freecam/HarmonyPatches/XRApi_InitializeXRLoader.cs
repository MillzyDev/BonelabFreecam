using Freecam.Configuration;
using HarmonyLib;
using SLZ.Marrow.Input;

namespace Freecam.HarmonyPatches;

[HarmonyPatch(typeof(XRApi.__c__DisplayClass50_0))]
[HarmonyPatch(nameof(XRApi.__c__DisplayClass50_0._InitializeXRLoader_b__0))]
internal static class XRApi_InitializeXRLoader
{
    private static readonly Config s_config = Config.Instance;
    
    [HarmonyPrefix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static bool Prefix(ref bool __result)
    {
        if (!s_config.NoHmd)
        {
            return true;
        }

        __result = true;
        return false;
    }
}