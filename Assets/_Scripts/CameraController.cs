using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PlayerInput playerInput;

    private float smoothTime = 0.01f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(
            playerInput.actions["Look"].ReadValue<Vector2>().x * rotateSpeed, Vector3.up) * offset;
        Vector3 newCameraPosition = playerInput.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothTime);
        transform.LookAt(playerInput.transform.position);
    }
}
