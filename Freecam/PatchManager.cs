using System;
using Freecam.HarmonyPatches;
using HarmonyLib;
using Il2CppSystem.Reflection;
using SLZ.Marrow.Input;
using BindingFlags = System.Reflection.BindingFlags;
using MethodInfo = System.Reflection.MethodInfo;

namespace Freecam;

internal sealed class PatchManager
{
    private static readonly Lazy<PatchManager> s_lazy = new(() => new PatchManager());

    private readonly HarmonyLib.Harmony _harmony;

    private PatchManager()
    {
        _harmony = new HarmonyLib.Harmony(BuildInfo.Id);
    }

    public static PatchManager Instance
    {
        get => s_lazy.Value;
    }

    public void InstallFreecamPatches()
    {
        _harmony.PatchAll();
        PatchXRApi();
    }

    public void UninstallFreecamPatches()
    {
        _harmony.UnpatchSelf();
    }

    private void PatchXRApi()
    {
        Type xrApi = typeof(XRApi);

        Type targetType = xrApi.GetNestedType("__c__DisplayClass50_0");
        MethodInfo targetMethod;
        if (targetType == null)
        {
            targetType = xrApi.GetNestedType("__c");
            targetMethod = targetType.GetMethod("_Initialize_b__45_0")!;
        }
        else
        {
            targetMethod = targetType.GetMethod("_InitializeXRLoader_b__0")!;
        }
        
        MethodInfo prefix = typeof(XRApi_InitializeXRLoader).GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public)!;
        _harmony.Patch(targetMethod, prefix: new HarmonyMethod(prefix));
    }
}