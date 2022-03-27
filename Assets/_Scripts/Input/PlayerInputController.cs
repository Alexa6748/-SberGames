using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public static UnityAction<Vector2> OnMove;

    public static UnityAction<Vector2> OnRotate;

    Vector2 zero = Vector2.zero;

    void Update()
    {
        LookMove();
        LookRotation();
    }

    private void LookMove()
    {
        if (CheckInput(playerInput.actions["Move"], out Vector2 input))
        {
            Debug.Log("move");
            OnMove?.Invoke(input);
        }
    }

    private void LookRotation()
    {
        if (CheckInput(playerInput.actions["Rotation"], out Vector2 input))
        {
            Debug.Log("rot");
            OnRotate?.Invoke(input);
        }
    }

    bool CheckInput(InputAction action, out Vector2 input)
    {
        input = action.ReadValue<Vector2>();
        if (input == zero)
            return false;
        return true;
    }
}
