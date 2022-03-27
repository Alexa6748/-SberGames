using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    bool lastStateOnRoadEnd;

    private void Start()
    {
        PlayerStateController.OnStateChange += () =>
        {
            if (player.HasState("OnRoadEnd"))
            {
                EnableCameraControl();
                lastStateOnRoadEnd = true;
            }
            else
            {
                DisableCameraControl();
            }            
            
            if (player.HasState("OnGravityChange"))
            {
                BlendBetweenCameras();
            }
        };

        GravityController.OnGravityChange += BlendBetweenCameras;
    }

    private void DisableCameraControl()
    {
        freeLookCamera.Priority = camera1.Priority - 2;
    }

    private void EnableCameraControl()
    {
        freeLookCamera.Priority = camera1.Priority + 2;
    }

    void BlendBetweenCameras()
    {
        if (camera2.Priority > camera1.Priority)
        {
            camera2.Priority = camera1.Priority - 1;
        }
        else
        {
            camera2.Priority = camera1.Priority + 1;
        }
    }
}
