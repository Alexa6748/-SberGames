using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), 
    typeof(PlayerMovementController), 
    typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerStateController controller;

    public PlayerState CurrentState => controller.CurrentState;
    public PlayerInput PlayerInput => playerInput;

    private void Start()
    {
        SetState("Idle");
        Debug.Log(CurrentState);
    }

    public void SetState(string stateName) => controller.SetCurrentState(stateName);

    public bool HasState(string stateName) => controller.IsCurrentState(stateName);
}
