using System;
using Freecam.Configuration;
using MelonLoader;
using SLZ.Rig;
using UnityEngine;

namespace Freecam;

[RegisterTypeInIl2Cpp]
internal sealed class FreecamHostManager(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Config _config = null!;
    private GameObject _freecamObject = null!;
    private RigManager _rigManager = null!;
    
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
        camera.cameraType = CameraType.Game;

        _ = freecamObject.AddComponent<FreecamController>();
    }

    private void Awake()
    {
        _config = Config.Instance;
    }

    private void Start()
    {
        _freecamObject = GetComponentInChildren<FreecamController>().gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFreecam();
        }
    }

    private void ToggleFreecam()
    {
        bool newFreecamActivity = !_config.FreecamEnabled;
        _freecamObject.SetActive(newFreecamActivity);
        _config.FreecamEnabled = newFreecamActivity;
    }
}