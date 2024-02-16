using Freecam.Configuration;
using MelonLoader;

namespace Freecam
{
    public sealed class Mod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            PatchManager.Instance.InstallFreecamPatches();
        }

        public override void OnApplicationQuit()
        {
            PatchManager.Instance.UninstallFreecamPatches();
            Config.Instance.Save();
        }
    }
}