using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateController;

public class CameraController : MonoBehaviour
{
    private Player player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;
    [SerializeField] private CinemachineBrain cinemachineBrain;

    private float smoothTime = 0.01f;

    private void Start()
    {
        PlayerStateController.OnStateChange += () =>
        {
            if (player.HasState("OnRoadEnd"))
            {
                EnableCameraControl();
            }
            else
            {
                cinemachineBrain.enabled = true;
            }
        };
        player = GetComponent<Player>();

        PlayerInputController.OnRotate += RotateCamera;
    }

    private void EnableCameraControl()
    {
        cinemachineBrain.enabled = false;
    }

    private void DisableCameraControl()
    {
        cinemachineBrain.enabled = true;
    }


    private void RotateCamera(Vector2 input)
    {
        if (player.HasState("OnRoadEnd"))
        {
            offset = Quaternion.AngleAxis(rotateSpeed, SwapXY(input)) * offset;
            Vector3 newCameraPosition = playerInput.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothTime);

            Vector3 lookAt = (playerInput.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookAt, playerInput.transform.up);
        }        
    }

    public static Vector2 SwapXY(Vector2 vector2)
    {
        Vector2 newVector2;
        newVector2.x = vector2.y;
        newVector2.y = vector2.x;
        return newVector2;
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
