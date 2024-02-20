using System;
using Freecam.Configuration;
using Freecam.IMGUI;
using MelonLoader;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;
using UnityEngine;

namespace Freecam;

[RegisterTypeInIl2Cpp]
internal sealed class FreecamHostManager(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    private GameObject _freecamObject = null!;
    private RigManager _rigManager = null!;
    private FreecamMenu _menu = null!;
    
    public static void CreateFreecam(RigManager rigManager)
    {
        var freecamHost = new GameObject("FreecamHost");
        var instance = freecamHost.AddComponent<FreecamHostManager>();
        instance._rigManager = rigManager;

        Transform hostTransform = freecamHost.transform;
        hostTransform.position = rigManager.ControllerRig.m_head.position;

        var freecamObject = new GameObject("Freecam");
        freecamObject.transform.SetParent(hostTransform, false);
        freecamObject.transform.localPosition = new Vector3(0f, 0f, 0f);

        var camera = freecamObject.AddComponent<Camera>();
        camera.cameraType = CameraType.SceneView; // this allows us to change the FOV and avoid foveated rendering

        _ = freecamObject.AddComponent<FreecamController>();

        var config = Config.Instance;
        
        var configMenu = freecamHost.AddComponent<FreecamMenu>();
        configMenu.enabled = config.ShowConfigMenu;
    }

    private void Awake()
    {
        _config = Config.Instance;
    }

    private void Start()
    {
        _freecamObject = GetComponentInChildren<FreecamController>().gameObject;
        _menu = GetComponent<FreecamMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFreecam();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleMenu();
        }
    }

    public void ToggleFreecam()
    {
        bool newFreecamActivity = !_config.FreecamEnabled;
        _freecamObject.SetActive(newFreecamActivity);
        _config.FreecamEnabled = newFreecamActivity;
        
        Control_Player controlPlayer = _rigManager.uiRig.controlPlayer;
        DataManager.Settings._spectatorSettings._spectatorCameraMode = SpectatorCameraMode.Passthrough;
        controlPlayer.UpdateSpectator();
    }

    private void ToggleMenu()
    {
        bool menuEnabled = !_config.ShowConfigMenu;
        _menu.enabled = menuEnabled;
        _config.ShowConfigMenu = menuEnabled;
    }
}