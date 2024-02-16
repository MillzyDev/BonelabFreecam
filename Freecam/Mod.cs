using Freecam.Configuration;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using Unity.XR.MockHMD;
using UnityEngine;
using UnityEngine.XR.Management;

namespace Freecam
{
    public sealed class Mod : MelonMod
    {
        private static void SwapXRLoader()
        {
            List<XRLoader> loaders = XRGeneralSettings.Instance.Manager.loaders;
            
            var mockHmdLoader = ScriptableObject.CreateInstance<MockHMDLoader>();
            mockHmdLoader.name = "Freecam Override";
            
            loaders.Clear();
            loaders.Add(mockHmdLoader);
        }
        
        public override void OnInitializeMelon()
        {
            PatchManager.Instance.InstallFreecamPatches();

            if (Config.Instance.NoHmd)
            {
                SwapXRLoader();
            }
        }

        public override void OnApplicationQuit()
        {
            PatchManager.Instance.UninstallFreecamPatches();
            Config.Instance.Save();
        }
    }
}