using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
internal sealed class LayerSettingsMenu(IntPtr ptr) : MonoBehaviour(ptr), INotifyPropertyChanged
{
    private int _cullingMask;
    private Vector2 _scrollPosition = new(0f, 0f);
    private string[] _layerNames = null!;

    public int CullingMask
    {
        get => _cullingMask;
        set => SetField(ref _cullingMask, value);
    }

    private void Awake()
    {
        var config = Config.Instance;

        _cullingMask = config.CullingMask;

        List<string> layerNames = [];
        for (int layer = 0; layer < 32; layer++)
        {
            layerNames.Add(LayerMask.LayerToName(layer));
            MelonLogger.Msg($"Layer is empty str: {LayerMask.LayerToName(layer) == string.Empty}");
        }

        _layerNames = layerNames.ToArray();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(420f, 40f, 200f, 825f), "Layers");
        /* _scrollPosition =
            GUI.BeginScrollView(new Rect(425f, 60f, 190f, 290f), _scrollPosition, new Rect(0f, 0f, 185f, 960f));
            */
        
        for (int layer = 0; layer < 32; layer++)
        {
            int currentLayer = 1 << layer;
            bool toggleEnabled = GUI.Toggle(
                new Rect(425f, 65f + 25f * layer, 180f, 20f), 
                (_cullingMask & currentLayer) == currentLayer, 
                _layerNames[layer]);

            if (toggleEnabled)
            {
                CullingMask |= currentLayer;
            }
            else
            {
                CullingMask &= ~currentLayer;
            }
        }
        
        //GUI.EndScrollView();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}