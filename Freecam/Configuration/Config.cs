﻿using System;
using MelonLoader;

namespace Freecam.Configuration;

internal sealed class Config
{
    private static readonly Lazy<Config> s_lazy = new(() => new Config());

    private readonly MelonPreferences_Category _config;
    private readonly MelonPreferences_Entry<bool> _freecamEnabled;
    private readonly MelonPreferences_Entry<bool> _noHmd;
    private readonly MelonPreferences_Entry<bool> _showConfigMenu;
    private readonly MelonPreferences_Entry<float> _speed;
    private readonly MelonPreferences_Entry<float> _fastMultiplier;
    private readonly MelonPreferences_Entry<float> _cameraSensitivity;
    private readonly MelonPreferences_Entry<float> _fieldOfView;
    private readonly MelonPreferences_Entry<float> _nearClip;
    private readonly MelonPreferences_Entry<int> _cullingMask;
    
    private Config()
    {
        _config = MelonPreferences.CreateCategory("Freecam");
        
        _freecamEnabled = _config.CreateEntry("bFreecamEnabled", false);
        _noHmd = _config.CreateEntry("bNoHmd", true);
        _showConfigMenu = _config.CreateEntry("bShowConfigMenu", true);
        _speed = _config.CreateEntry("fNormalSpeed", 10f);
        _fastMultiplier = _config.CreateEntry("fFastMultiplier", 10f);
        _cameraSensitivity = _config.CreateEntry("fLookSensitivity", 0.3f);
        _fieldOfView = _config.CreateEntry("fFieldOfView", 90f);
        _nearClip = _config.CreateEntry("fNearClip", 0.3f);
        _cullingMask = _config.CreateEntry("iCullingMask", -1);
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

    public bool ShowConfigMenu
    {
        get => _showConfigMenu.Value;
        set => _showConfigMenu.Value = value;
    }

    public float Speed
    {
        get => _speed.Value;
        set => _speed.Value = value;
    }

    public float FastMultiplier
    {
        get => _fastMultiplier.Value;
        set => _fastMultiplier.Value = value;
    }

    public float CameraSensitivity
    {
        get => _cameraSensitivity.Value;
        set => _cameraSensitivity.Value = value;
    }

    public float FieldOfView
    {
        get => _fieldOfView.Value;
        set => _fieldOfView.Value = value;
    }

    public float NearClip
    {
        get => _nearClip.Value;
        set => _nearClip.Value = value;
    }

    public int CullingMask
    {
        get => _cullingMask.Value;
        set => _cullingMask.Value = value;
    }

    public void Save()
    {
        _config.SaveToFile();
    }
}