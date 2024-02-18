using System;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
public class ConfigMenu(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    private FreecamHostManager _freecamHostManager = null!;

    private bool _noHmdToggle;
    private string _speedString = string.Empty;
    private float _speedSlider;
    private string _fastMultiplierString = string.Empty;
    private float _fastMultiplierSlider;
    private string _fieldOfViewString = string.Empty;
    private float _fieldOfViewSlider;
    private string _nearClipString = string.Empty;
    private float _nearClipSlider;
    
    private void Awake()
    {
        _config = Config.Instance;

        _noHmdToggle = _config.NoHmd;
        _speedSlider = _config.Speed;
        _fastMultiplierSlider = _config.FastMultiplier;
        _fieldOfViewSlider = _config.FieldOfView;
        _nearClipSlider = _config.NearClip;
    }

    private void Start()
    {
        _freecamHostManager = GetComponent<FreecamHostManager>();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10f, 10f, 600f, 25f) ,"Freecam Menu (F1 to Show/Hide)");
        
        OnGeneralGUI();
        OnCameraGUI();
    }

    private void OnGeneralGUI()
    {
        GUI.Box(new Rect(10f, 40f, 200f, 300f), "General");

        bool freecamEnabled = _config.FreecamEnabled;
        if (GUI.Button(new Rect(15f, 60f, 190f, 20f), freecamEnabled ? "Disable Freecam (F)" : "Enable Freecam (F)"))
        {
            _freecamHostManager.ToggleFreecam();
        }

        _noHmdToggle = GUI.Toggle(new Rect(15f, 85f, 190f, 20f), _noHmdToggle, "No HMD Mode");

        GUI.Label(new Rect(15f, 110f, 190f, 20f), "Speed:");
        _speedString = GUI.TextField(new Rect(150f, 110f, 60f, 20f), _speedString);
        _speedSlider = GUI.HorizontalSlider(new Rect(15f, 130f, 190f, 20f), _speedSlider, 0.1f, 40f);
        
        GUI.Label(new Rect(15f, 155f, 190f, 20f), "Fast Multiplier:");
        _fastMultiplierString = GUI.TextField(new Rect(150f, 155f, 60f, 20f), _fastMultiplierString);
        _fastMultiplierSlider = GUI.HorizontalSlider(new Rect(15f, 180f, 190f, 20f), _fastMultiplierSlider, 1.5f, 20f);
    }

    private void OnCameraGUI()
    {
        GUI.Box(new Rect(215f, 40f, 200f, 300f), "Camera");
        
        GUI.Label(new Rect(220f, 60f, 190f, 20f), "Field of View:");
        _fieldOfViewString = GUI.TextField(new Rect(350f, 60f, 50f, 20f), _fieldOfViewString);
        _fieldOfViewSlider = GUI.HorizontalSlider(new Rect(220f, 85f, 190f, 20f), _fieldOfViewSlider, 60f, 120f);
        
        GUI.Label(new Rect(220f, 125f, 190f, 20f), "Near Clip Distance:");
        _nearClipString = GUI.TextField(new Rect(350f, 125f, 50f, 20f), _nearClipString);
        _nearClipSlider = GUI.HorizontalSlider(new Rect(220f, 150f, 190f, 20f), _nearClipSlider, 0.01f, 0.5f);
    }

    private void LateUpdate()
    {
        #region No HMD
        if (_noHmdToggle != _config.NoHmd)
        {
            _config.NoHmd = _noHmdToggle;
        }
        #endregion
        
        #region Speed
        if (Math.Abs(_speedSlider - _config.Speed) > 0.01f)
        {
            _config.Speed = _speedSlider;
            _speedString = $"{_speedSlider:F}";
        }
        
        if (float.TryParse(_speedString, out float speed))
        {
            _speedSlider = speed;
        }
        else
        {
            _speedString = $"{_speedSlider:F}";
        }
        #endregion

        #region Fast Multiplier
        if (Math.Abs(_fastMultiplierSlider - _config.FastMultiplier) > 0.01f)
        {
            _config.FastMultiplier = _fastMultiplierSlider;
            _fastMultiplierString = $"{_fastMultiplierSlider:F}";
        }

        if (float.TryParse(_fastMultiplierString, out float fastMultiplier))
        {
            _fastMultiplierSlider = fastMultiplier;
        }
        else
        {
            _fastMultiplierString = $"{_fastMultiplierSlider:F}";
        }
        #endregion

        #region Field of View
        if (Math.Abs(_fieldOfViewSlider - _config.FieldOfView) > 0.01f)
        {
            _config.FieldOfView = (int)_fieldOfViewSlider;
            _fieldOfViewString = $"{_fieldOfViewSlider:0F}";
        }

        if (int.TryParse(_fieldOfViewString, out int fieldOfView))
        {
            _fieldOfViewSlider = fieldOfView;
        }
        else
        {
            _fieldOfViewString = $"{_fieldOfViewSlider:00}";
        }
        #endregion

        #region Near Clip
        if (Math.Abs(_nearClipSlider - _config.NearClip) > 0.001)
        {
            _config.NearClip = _nearClipSlider;
            _nearClipString = $"{_nearClipSlider:F}";
        }

        if (float.TryParse(_nearClipString, out float nearClip))
        {
            _nearClipSlider = nearClip;
        }
        else
        {
            _nearClipString = $"{_nearClipSlider:F}";
        }
        #endregion
    }
}