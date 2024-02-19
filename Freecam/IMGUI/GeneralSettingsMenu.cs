using System;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
public class GeneralSettingsMenu(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    private FreecamHostManager _freecamHostManager = null!;

    private bool _noHmdToggle;
    private string _speedString = string.Empty;
    private float _speedValue;
    private string _fastMultiplierString = string.Empty;
    private float _fastMultiplierValue;

    private void Awake()
    {
        _config = Config.Instance;

        _noHmdToggle = _config.NoHmd;
        _speedValue = _config.Speed;
        _fastMultiplierValue = _config.FastMultiplier;
    }

    private void Start()
    {
        _freecamHostManager = GetComponent<FreecamHostManager>();
    }

    private void OnGUI()
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
        _speedValue = GUI.HorizontalSlider(new Rect(15f, 130f, 190f, 20f), _speedValue, 0.1f, 40f);
        
        GUI.Label(new Rect(15f, 155f, 190f, 20f), "Fast Multiplier:");
        _fastMultiplierString = GUI.TextField(new Rect(150f, 155f, 60f, 20f), _fastMultiplierString);
        _fastMultiplierValue = GUI.HorizontalSlider(new Rect(15f, 180f, 190f, 20f), _fastMultiplierValue, 1.5f, 20f);
    }

    private void Update()
    {
        if (_noHmdToggle != _config.NoHmd)
        {
            _config.NoHmd = _noHmdToggle;
        }
        
        if (Math.Abs(_speedValue - _config.Speed) > 0.01f)
        {
            _config.Speed = _speedValue;
            _speedString = $"{_speedValue:F}";
        }
        
        if (float.TryParse(_speedString, out float speed))
        {
            _speedValue = speed;
        }
        else
        {
            _speedString = $"{_speedValue:F}";
        }
        
        if (Math.Abs(_fastMultiplierValue - _config.FastMultiplier) > 0.01f)
        {
            _config.FastMultiplier = _fastMultiplierValue;
            _fastMultiplierString = $"{_fastMultiplierValue:F}";
        }

        if (float.TryParse(_fastMultiplierString, out float fastMultiplier))
        {
            _fastMultiplierValue = fastMultiplier;
        }
        else
        {
            _fastMultiplierString = $"{_fastMultiplierValue:F}";
        }
    }
}