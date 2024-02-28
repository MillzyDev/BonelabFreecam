using System;
using System.ComponentModel;
using Freecam.Configuration;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
public class FreecamMenu(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    private Camera _camera = null!;
    
    private FreecamController _freecamController = null!;
    private GeneralSettingsMenu _generalSettingsMenu = null!;
    private CameraSettingsMenu _cameraSettingsMenu = null!;
    private LayerSettingsMenu _layerSettingsMenu = null!;
    
    private void Awake()
    {
        _config = Config.Instance;
        
        _freecamController = GetComponentInChildren<FreecamController>();
        
        _generalSettingsMenu = gameObject.AddComponent<GeneralSettingsMenu>();
        _cameraSettingsMenu = gameObject.AddComponent<CameraSettingsMenu>();
        _layerSettingsMenu = gameObject.AddComponent<LayerSettingsMenu>();

        _generalSettingsMenu.PropertyChanged += PropertyChanged;
        _cameraSettingsMenu.PropertyChanged += PropertyChanged;
    }

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10f, 10f, 600f, 25f) ,"Freecam Menu (F1 to Show/Hide)");
    }

    private void OnEnable()
    {
        _generalSettingsMenu.enabled = true;
        _cameraSettingsMenu.enabled = true;
    }

    private void OnDisable()
    {
        SaveValues();
        
        _generalSettingsMenu.enabled = false;
        _cameraSettingsMenu.enabled = false;
    }

    private void PropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        ApplyValues();
    }

    private void ApplyValues()
    {
        _freecamController.Speed = _generalSettingsMenu.Speed;
        _freecamController.FastMultiplier = _generalSettingsMenu.FastMultiplier;
        _freecamController.CameraSensitivity = _generalSettingsMenu.CameraSensitivity;
        
        _camera.fieldOfView = _cameraSettingsMenu.FieldOfView;
        _camera.nearClipPlane = _cameraSettingsMenu.NearClip;

        _camera.cullingMask = _layerSettingsMenu.CullingMask;
    }

    private void SaveValues()
    {
        _config.NoHmd = _generalSettingsMenu.NoHmd;
        _config.Speed = _generalSettingsMenu.Speed;
        _config.FastMultiplier = _generalSettingsMenu.FastMultiplier;
        _config.CameraSensitivity = _generalSettingsMenu.CameraSensitivity;

        _config.FieldOfView = _cameraSettingsMenu.FieldOfView;
        _config.NearClip = _cameraSettingsMenu.NearClip;

        _config.CullingMask = _layerSettingsMenu.CullingMask;
    }
}