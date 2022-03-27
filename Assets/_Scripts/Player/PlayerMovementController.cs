using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateController;


public class PlayerMovementController : MonoBehaviour
{
    private Player player;
    private CharacterController characterController;
    private Transform camera;

    Vector3 moveTo;

    private float smoothTime = 0.01f;
    private Vector2 input;
    Vector2 zero = Vector2.zero;

    void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        camera = Camera.main.transform;

        PlayerInputController.OnMove += Move;        
    }
    private void FixedUpdate()
    {
        transform.up = PlayerGravity.NormalDirection;
    }

    void Move(Vector2 input)
    {
        if (player.CurrentState.IsMovementEnabled)
        {
            CalculateMoveToVector(input);
        }
        else
        {
            moveTo = Vector3.zero;
        }
        Move();
    }

    private void Move()
    {
        characterController.Move(moveTo, player.CurrentState.PlayerSpeed);
    }

    private void CalculateMoveToVector(Vector2 input)
    {
        Vector3 forward = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
        moveTo = forward * input.y + camera.right * input.x;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.Project(camera.forward, Vector3.forward),
            smoothTime);
    }
}