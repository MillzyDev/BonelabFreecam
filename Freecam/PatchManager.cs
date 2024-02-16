using System;
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
    }

    public void UninstallFreecamPatches()
    {
        _harmony.UnpatchSelf();
    }
}