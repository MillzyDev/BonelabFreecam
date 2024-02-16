﻿using System;
using MelonLoader;
using UnityEngine;

namespace Freecam;

[RegisterTypeInIl2Cpp]
internal sealed class FreecamController(IntPtr ptr) : MonoBehaviour(ptr)
{
    private Vector3 _lastMousePosition = Vector3.zero;

    private void Update()
    {
        float speed = 10f * Time.deltaTime;

        // fast mode
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 10f;
        }

        Transform cameraTransform = transform;

        // wasd controls
        if (Input.GetKey(KeyCode.W))
        {
            cameraTransform.position += cameraTransform.forward * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraTransform.position += cameraTransform.right * (-1f * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraTransform.position += cameraTransform.forward * (-1f * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraTransform.position += cameraTransform.right * speed;
        }
        
        // up and down
        if (Input.GetKey(KeyCode.Space))
        {
            cameraTransform.position += cameraTransform.up * speed;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            cameraTransform.position += cameraTransform.up * (-1f * speed);
        }
        
        // looking around
        if (Input.GetMouseButton(1)) // right click
        {
            Vector3 mouseDisplacement = Input.mousePosition - _lastMousePosition;

            Vector3 localEulerAngles = transform.localEulerAngles;
            float thetaX = localEulerAngles.y + mouseDisplacement.x * 0.3f;
            float thetaY = localEulerAngles.x - mouseDisplacement.y * 0.3f;

            cameraTransform.localEulerAngles = new Vector3(thetaY, thetaX, 0f);
        }

        _lastMousePosition = Input.mousePosition;
    }
}