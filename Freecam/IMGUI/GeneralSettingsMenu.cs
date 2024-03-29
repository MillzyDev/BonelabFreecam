﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
internal sealed class GeneralSettingsMenu(IntPtr ptr) : MonoBehaviour(ptr), INotifyPropertyChanged
{
    private Config _config = null!;
    private string _fastMultiplierString = string.Empty;
    private float _fastMultiplierValue;
    private string _cameraSensitivityString = string.Empty;
    private float _cameraSensitivityValue;
    private FreecamHostManager _freecamHostManager = null!;
    private LayerSettingsMenu _layerSettingsMenu = null!;

    private bool _noHmdToggle;
    private string _speedString = string.Empty;
    private float _speedValue;

    public bool NoHmd
    {
        get => _noHmdToggle;
        set => SetField(ref _noHmdToggle, value);
    }

    public float Speed
    {
        get => _speedValue;
        set
        {
            SetField(ref _speedValue, value);
            _speedString = value.ToString("F");
        }
    }

    public float FastMultiplier
    {
        get => _fastMultiplierValue;
        set
        {
            SetField(ref _fastMultiplierValue, value);
            _fastMultiplierString = value.ToString("F");
        }
    }

    public float CameraSensitivity
    {
        get => _cameraSensitivityValue;
        set
        {
            SetField(ref _cameraSensitivityValue, value);
            _cameraSensitivityString = value.ToString("F");
        }
    }

    private string SpeedString
    {
        set
        {
            if (!SetField(ref _speedString, value, dontFire: true)) return;

            if (float.TryParse(value, out float speed)) Speed = speed;
        }
    }

    private string FastMultiplierString
    {
        set
        {
            if (!SetField(ref _fastMultiplierString, value, dontFire: true)) return;

            if (float.TryParse(value, out float fastMultiplier)) FastMultiplier = fastMultiplier;
        }
    }

    private string CameraSensitivityString
    {
        set
        {
            if (!SetField(ref _cameraSensitivityString, value, dontFire: true)) return;

            if (float.TryParse(value, out float cameraSensitivity)) CameraSensitivity = cameraSensitivity;
        }
    }

    private void Awake()
    {
        _config = Config.Instance;

        _noHmdToggle = _config.NoHmd;
        _speedValue = _config.Speed;
        _fastMultiplierValue = _config.FastMultiplier;
        _cameraSensitivityValue = _config.CameraSensitivity;
    }

    private void Start()
    {
        _freecamHostManager = GetComponent<FreecamHostManager>();
        _layerSettingsMenu = GetComponent<LayerSettingsMenu>();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10f, 40f, 200f, 300f), "General");

        bool freecamEnabled = _config.FreecamEnabled;
        if (GUI.Button(new Rect(15f, 60f, 190f, 20f), freecamEnabled ? "Disable Freecam (F)" : "Enable Freecam (F)"))
        {
            _freecamHostManager.ToggleFreecam();
        }

        NoHmd = GUI.Toggle(new Rect(15f, 85f, 190f, 20f), _noHmdToggle, "No HMD Mode");

        GUI.Label(new Rect(15f, 110f, 190f, 20f), "Speed:");
        SpeedString = GUI.TextField(new Rect(150f, 110f, 60f, 20f), _speedString);
        Speed = GUI.HorizontalSlider(new Rect(15f, 130f, 190f, 20f), _speedValue, 0.1f, 40f);

        GUI.Label(new Rect(15f, 155f, 190f, 20f), "Fast Multiplier:");
        FastMultiplierString = GUI.TextField(new Rect(150f, 155f, 60f, 20f), _fastMultiplierString);
        FastMultiplier = GUI.HorizontalSlider(new Rect(15f, 180f, 190f, 20f), _fastMultiplierValue, 1.5f, 20f);
        
        GUI.Label(new Rect(15f, 200f, 190f, 20f), "Camera Sensitivity");
        CameraSensitivityString = GUI.TextField(new Rect(150f, 200f, 60f, 20f), _cameraSensitivityString);
        CameraSensitivity = GUI.HorizontalSlider(new Rect(15f, 225f, 190f, 20f), _cameraSensitivityValue, 0.01f, 1f);

        if (GUI.Button(new Rect(15f, 250f, 190f, 20f), "Show All Layers"))
        {
            _layerSettingsMenu.CullingMask = -1;
        }

        if (GUI.Button(new Rect(15f, 275f, 190f, 20f), "Hide All Layers"))
        {
            _layerSettingsMenu.CullingMask = 0;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, bool dontFire = false,
        [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;

        if (!dontFire)
            OnPropertyChanged(propertyName);

        return true;
    }
}