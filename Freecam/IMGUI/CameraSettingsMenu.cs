using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
internal sealed class CameraSettingsMenu(IntPtr ptr) : MonoBehaviour(ptr), INotifyPropertyChanged
{
    private string _fieldOfViewString = string.Empty;
    private float _fieldOfViewValue;
    private string _nearClipString = string.Empty;
    private float _nearClipValue;

    public float FieldOfView
    {
        get => _fieldOfViewValue;
        set
        {
            SetField(ref _fieldOfViewValue, value);
            _fieldOfViewString = value.ToString("F");
        }
    }

    public float NearClip
    {
        get => _nearClipValue;
        set { 
            SetField(ref _nearClipValue, value);
            _nearClipString = value.ToString("F");
        }
    }

    private string FieldOfViewString
    {
        set
        {
            if (!SetField(ref _fieldOfViewString, value, dontFire: true)) return;

            if (float.TryParse(value, out float fieldOfView)) FieldOfView = fieldOfView;
        }
    }

    private string NearClipString
    {
        set
        {
            if (!SetField(ref _nearClipString, value, dontFire: true)) return;

            if (float.TryParse(value, out float nearClip)) NearClip = nearClip;
        }
    }

    private void Awake()
    {
        var config = Config.Instance;
        _fieldOfViewValue = config.FieldOfView;
        _nearClipValue = config.NearClip;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(215f, 40f, 200f, 300f), "Camera");
        
        GUI.Label(new Rect(220f, 60f, 190f, 20f), "Field of View:");
        FieldOfViewString = GUI.TextField(new Rect(350f, 60f, 50f, 20f), _fieldOfViewString);
        FieldOfView = GUI.HorizontalSlider(new Rect(220f, 85f, 190f, 20f), _fieldOfViewValue, 60f, 120f);
        
        GUI.Label(new Rect(220f, 125f, 190f, 20f), "Near Clip Distance:");
        NearClipString = GUI.TextField(new Rect(350f, 125f, 50f, 20f), _nearClipString);
        NearClip = GUI.HorizontalSlider(new Rect(220f, 150f, 190f, 20f), _nearClipValue, 0.01f, 0.5f);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, bool dontFire = false, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        
        if (!dontFire)
            OnPropertyChanged(propertyName);
        
        return true;
    }
}