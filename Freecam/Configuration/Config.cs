using System;
using MelonLoader;

namespace Freecam.Configuration;

internal sealed class Config
{
    private static readonly Lazy<Config> s_lazy = new(() => new Config());

    private readonly MelonPreferences_Category _config;
    private readonly MelonPreferences_Entry<bool> _freecamEnabled;
    
    private Config()
    {
        _config = MelonPreferences.CreateCategory("Freecam");
        _freecamEnabled = _config.CreateEntry("bFreecamEnabled", false);
    }

    public static Config Instance
    {
        get => s_lazy.Value;
    }

    public bool FreecamEnabled
    {
        get => _freecamEnabled.Value;
        set => _freecamEnabled.Value = value;
    }

    public void Save()
    {
        _config.SaveToFile();
    }
}