using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float rotateSpeed = 2.0f;

    [SerializeField] private Transform cameraTransform;

    private PlayerInput playerInput;
    private float speed;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        speed = playerSpeed * Time.deltaTime;
    }

    void Update()
    {
        Vector3 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 moveTo = new Vector3(moveInput.x, 0, moveInput.y);

        transform.Translate(moveTo * speed);
        
        Vector3 rotateInput = playerInput.actions["Look"].ReadValue<Vector2>();

        transform.Rotate(Vector3.up * rotateSpeed * rotateInput.x);
    }
}