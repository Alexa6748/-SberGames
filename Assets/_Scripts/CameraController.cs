using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] [Range(0, 1)] private float sensitive;

    private float smoothTime = 0.01f;
    private Vector3 zero = Vector3.zero;
    private float speed;
    private Vector3 rotateInput;

    void Start()
    {
        speed = rotateSpeed * Time.deltaTime;
    }

    void LateUpdate()
    {
        rotateInput = playerInput.actions["Rotation"].ReadValue<Vector2>();
        Debug.Log(rotateInput);
        offset = Quaternion.AngleAxis(rotateSpeed, SwapXY(rotateInput)) * offset;
        Vector3 newCameraPosition = playerInput.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothTime);
        transform.LookAt(playerInput.transform.position);
        
    }

    public static Vector2 SwapXY(Vector2 vector2)
    {
        Vector2 newVector2;
        newVector2.x = vector2.y;
        newVector2.y = vector2.x;
        return newVector2;
    }
}
