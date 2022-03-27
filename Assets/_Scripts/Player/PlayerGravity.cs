using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerStateController;


public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private float gravityValue = -12;
    public static Vector3 NormalDirection { get; set; } = Vector3.up;

    private bool groundedPlayer;
    private Vector3 gravity;
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void FixedUpdate()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && gravity.y < 0)
        {
            gravity = Vector3.zero;
        }

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        characterController.ApplyGravity(NormalDirection, Time.deltaTime * gravityValue);
    }
}
