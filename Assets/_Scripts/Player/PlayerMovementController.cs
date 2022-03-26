using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] [Range(0, 10f)] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -12;

    private CharacterController characterController;
    private Transform camera;

    private PlayerInput playerInput;
    private float speed;
    private bool groundedPlayer;
    private Vector3 gravity;

    Vector3 moveInput;
    Vector3 moveTo;
    Vector3 zero = Vector3.zero;
    private float smoothTime = 0.01f;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();
        camera = Camera.main.transform;

        speed = playerSpeed * Time.deltaTime;
    }



    void Update()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && gravity.y < 0)
        {
            gravity.y = 0f;
        }
        
        gravity.y += gravityValue * Time.deltaTime;
        moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 forward = Vector3.Project(camera.forward, Vector3.forward);
        moveTo = Vector3.Project(camera.forward, Vector3.forward) * moveInput.y + camera.right * moveInput.x;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.Project(camera.forward, Vector3.forward),
            smoothTime);

        characterController.Move(moveTo * speed);
        characterController.Move(gravity * Time.deltaTime);
    }
}