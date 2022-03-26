using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] [Range(0, 1)] private float sensitive;

    [SerializeField] private CinemachineVirtualCamera camera1;
    [SerializeField] private CinemachineVirtualCamera camera2;

    private float smoothTime = 0.01f;
    private Vector3 zero = Vector3.zero;
    private float speed;
    private Vector3 rotateInput;
    private PlayerState currentState;

    void Start()
    {
        speed = rotateSpeed * Time.deltaTime;
        currentState = playerInput.GetComponent<PlayerState>();
        PlayerState.onGravityChange += (currentState) =>
        {
            if (currentState.IsFirstCameraActive)
            {
                BlendToFirstCamera();
            }
            else
            {
                BlendToSecondCamera();
            }
        };

    }

    void LateUpdate()
    {
        if (currentState.IsCameraControlsEnabled)
        {
            RotateCamera();
        }
    }

    private void RotateCamera()
    {
        rotateInput = playerInput.actions["Rotation"].ReadValue<Vector2>();
        offset = Quaternion.AngleAxis(rotateSpeed, SwapXY(rotateInput)) * offset;
        Vector3 newCameraPosition = playerInput.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothTime);

        Vector3 lookAt = (playerInput.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookAt, playerInput.transform.up);
    }

    public static Vector2 SwapXY(Vector2 vector2)
    {
        Vector2 newVector2;
        newVector2.x = vector2.y;
        newVector2.y = vector2.x;
        return newVector2;
    }

    void BlendToSecondCamera()
    {
        camera2.Priority = 12;
    }

    void BlendToFirstCamera()
    {
        camera2.Priority = 10;
    }
}
