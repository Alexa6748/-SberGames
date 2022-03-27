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

    private bool startMove;
    private bool startRotate;

    void FixedUpdate()
    {
        LookMove();
    }

    private void LateUpdate()
    {
        LookRotation();
    }

    private void LookMove()
    {
        if (CheckInput(playerInput.actions["Move"], out Vector2 input))
        {
            OnMove?.Invoke(input);
            startMove = true;
        }
        else if (startMove)
        {
            OnMove?.Invoke(zero);
            startMove = false;
        }
    }

    private void LookRotation()
    {
        if (CheckInput(playerInput.actions["Rotation"], out Vector2 input))
        {
            OnRotate?.Invoke(input);
            startRotate = true;
        }
        else if (startRotate)
        {
            OnMove?.Invoke(zero);
            startRotate = false;
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
