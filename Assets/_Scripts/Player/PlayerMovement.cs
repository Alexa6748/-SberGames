using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 5f)] private float playerSpeed = 4;

    private CharacterController characterController;
    private Transform camera;

    private PlayerInput playerInput;
    private float speed;
    Vector3 moveInput;
    Vector3 moveTo;
    private float smoothTime = 0.01f;
    Vector3 zero = Vector3.zero;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        camera = Camera.main.transform;
    }



    void FixedUpdate()
    {
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        if (moveInput != zero)
        {
            Vector3 forward = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
            moveTo = forward * moveInput.y + camera.right * moveInput.x;
            transform.forward = Vector3.Lerp(transform.forward, Vector3.Project(camera.forward, Vector3.forward),
                smoothTime);

            Move();
        }

        transform.up = PlayerState.CurrentState.NormalDirection;
    }

    private void Move()
    {
        characterController.Move(moveTo, playerSpeed);
    }
}