using Freecam.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using SLZ.Marrow.Input;

namespace Freecam.HarmonyPatches;

// ReSharper disable once UnusedType.Global
internal static class XRApi_InitializeXRLoader
{
    private static readonly Config s_config = Config.Instance;
    
    // manually patched, since a single patch is not compatible for both the steam and oculus versions
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public static bool Prefix(ref bool __result)
    {
        if (!s_config.NoHmd)
        {
            return true;
        }

        __result = true;
        return false;
    }
}