using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerGravity),
    typeof(PlayerMovementController), 
    typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStateController controller;

    public PlayerState CurrentState => controller.CurrentState;

    private void Start()
    {
        SetState("Idle");
    }

    public void SetState(string stateName) => controller.SetCurrentState(stateName);

    public bool HasState(string stateName) => controller.IsCurrentState(stateName);
}
