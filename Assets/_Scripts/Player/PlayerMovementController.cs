using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateController;


public class PlayerMovementController : MonoBehaviour
{
    private Player player;
    private CharacterController characterController;
    private Transform camera;

    Vector3 moveInput;
    Vector3 moveTo;

    private float smoothTime = 0.01f;

    void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        camera = Camera.main.transform;

        PlayerInputController.OnMove += Move;
    }

    void Move(Vector2 input)
    {
        if (player.CurrentState.IsMovementEnabled)
        {
            CalculateMoveToVector();
            Move();
            transform.up = PlayerGravity.NormalDirection;
        }        
    }

    private void Move()
    {
        characterController.Move(moveTo, player.CurrentState.PlayerSpeed);
    }

    private void CalculateMoveToVector()
    {
        Vector3 forward = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
        moveTo = forward * moveInput.y + camera.right * moveInput.x;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.Project(camera.forward, Vector3.forward),
            smoothTime);
    }
}