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
    private float _fastMultiplier;
    
    private void Awake()
    {
        _config = Config.Instance;

        _noHmdToggle = _config.NoHmd;
        _speedSlider = _config.Speed;
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
        _fastMultiplier = GUI.HorizontalSlider(new Rect(15f, 180f, 190f, 20f), _fastMultiplier, 1.5f, 20f);
    }

    private void OnCameraGUI()
    {
        GUI.Box(new Rect(215f, 40f, 200f, 300f), "Camera");
    }

    private void LateUpdate()
    {
        if (_noHmdToggle != _config.NoHmd)
        {
            _config.NoHmd = _noHmdToggle;
        }
        
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

        if (Math.Abs(_fastMultiplier - _config.FastMultiplier) > 0.01f)
        {
            _config.FastMultiplier = _fastMultiplier;
            _fastMultiplierString = $"{_fastMultiplier:F}";
        }

        if (float.TryParse(_fastMultiplierString, out float fastMultiplier))
        {
            _fastMultiplier = fastMultiplier;
        }
        else
        {
            _fastMultiplierString = $"{_fastMultiplier:F}";
        }
    }
}