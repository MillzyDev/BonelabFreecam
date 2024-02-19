using System;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
internal sealed class CameraSettingsMenu(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    
    private string _fieldOfViewString = string.Empty;
    private float _fieldOfViewValue;
    private string _nearClipString = string.Empty;
    private float _nearClipValue;

    private void Awake()
    {
        _config = Config.Instance;
        
        _fieldOfViewValue = _config.FieldOfView;
        _nearClipValue = _config.NearClip;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(215f, 40f, 200f, 300f), "Camera");
        
        GUI.Label(new Rect(220f, 60f, 190f, 20f), "Field of View:");
        _fieldOfViewString = GUI.TextField(new Rect(350f, 60f, 50f, 20f), _fieldOfViewString);
        _fieldOfViewValue = GUI.HorizontalSlider(new Rect(220f, 85f, 190f, 20f), _fieldOfViewValue, 60f, 120f);
        
        GUI.Label(new Rect(220f, 125f, 190f, 20f), "Near Clip Distance:");
        _nearClipString = GUI.TextField(new Rect(350f, 125f, 50f, 20f), _nearClipString);
        _nearClipValue = GUI.HorizontalSlider(new Rect(220f, 150f, 190f, 20f), _nearClipValue, 0.01f, 0.5f);
    }

    private void Update()
    {
        #region Field of View
        if (Math.Abs(_fieldOfViewValue - _config.FieldOfView) > 0.01f)
        {
            _config.FieldOfView = (int)_fieldOfViewValue;
            _fieldOfViewString = $"{_fieldOfViewValue:0F}";
        }

        if (int.TryParse(_fieldOfViewString, out int fieldOfView))
        {
            _fieldOfViewValue = fieldOfView;
        }
        else
        {
            _fieldOfViewString = $"{_fieldOfViewValue:00}";
        }
        #endregion

        #region Near Clip
        if (Math.Abs(_fieldOfViewValue - _config.NearClip) > 0.001)
        {
            _config.NearClip = _nearClipValue;
            _nearClipString = $"{_nearClipValue:F}";
        }

        if (float.TryParse(_nearClipString, out float nearClip))
        {
            _nearClipValue = nearClip;
        }
        else
        {
            _nearClipString = $"{_nearClipValue:F}";
        }
        #endregion
    }
}