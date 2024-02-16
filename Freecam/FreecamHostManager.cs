using System;
using UnityEngine;

namespace Freecam;

internal sealed class FreecamHostManager(IntPtr ptr) : MonoBehaviour(ptr)
{
    public static void CreateFreecam(Transform playerHead)
    {
        var freecamHost = new GameObject("FreecamHost");
        freecamHost.AddComponent<FreecamHostManager>();

        Transform hostTransform = freecamHost.transform;
        hostTransform.position = playerHead.position;
        hostTransform.rotation = playerHead.rotation;

        var freecamObject = new GameObject("Freecam");
        freecamObject.transform.SetParent(hostTransform, false);

        var camera = freecamObject.AddComponent<Camera>();
        camera.cameraType = CameraType.Game;
        // TODO: Add camera control behaviour
    }
}