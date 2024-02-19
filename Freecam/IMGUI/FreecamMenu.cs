using System;
using MelonLoader;
using UnityEngine;

namespace Freecam.IMGUI;

[RegisterTypeInIl2Cpp]
public class FreecamMenu(IntPtr ptr) : MonoBehaviour(ptr)
{
    private GeneralSettingsMenu _generalSettingsMenu = null!;
    private CameraSettingsMenu _cameraSettingsMenu = null!;
    
    private void Awake()
    {
        _generalSettingsMenu = gameObject.AddComponent<GeneralSettingsMenu>();
        _cameraSettingsMenu = gameObject.AddComponent<CameraSettingsMenu>();
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
        _generalSettingsMenu.enabled = false;
        _cameraSettingsMenu.enabled = false;
    }
}