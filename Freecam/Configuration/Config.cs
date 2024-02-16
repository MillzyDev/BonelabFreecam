using System;
using MelonLoader;

namespace Freecam.Configuration;

internal sealed class Config
{
    private static readonly Lazy<Config> s_lazy = new(() => new Config());

    private readonly MelonPreferences_Category _config;
    private readonly MelonPreferences_Entry<bool> _freecamEnabled;
    private readonly MelonPreferences_Entry<bool> _noHmd;
    
    private Config()
    {
        _config = MelonPreferences.CreateCategory("Freecam");
        _freecamEnabled = _config.CreateEntry("bFreecamEnabled", false);
        _noHmd = _config.CreateEntry("bNoHmd", true);
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

    public bool NoHmd
    {
        get => _noHmd.Value;
        set => _noHmd.Value = value;
    }

    public void Save()
    {
        _config.SaveToFile();
    }
}