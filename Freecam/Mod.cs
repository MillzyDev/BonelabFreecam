using Freecam.Configuration;
using MelonLoader;

namespace Freecam
{
    public class Mod : MelonMod
    {
        public override void OnApplicationQuit()
        {
            Config.Instance.Save();
        }
    }
}