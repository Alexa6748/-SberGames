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

    private void Start()
    {
        PlayerStateController.OnStateChange += () =>
        {
            if (player.HasState("OnRoadEnd"))
            {
                EnableCameraControl();
            }
            else if (!player.HasState("Idle"))
            {
                DisableCameraControl();
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
        if (player.CurrentState.IsFirstCameraActive)
        {
            camera2.Priority = camera1.Priority - 1;
        }
        else
        {
            camera2.Priority = camera1.Priority + 1;
        }
    }
}
