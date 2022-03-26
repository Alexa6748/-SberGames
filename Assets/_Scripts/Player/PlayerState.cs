using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState CurrentState;

    private void Awake()
    {
        if (CurrentState == null)
            CurrentState = this;
    }
    public Vector3 NormalDirection { get; set; } = Vector3.up;

    public bool IsFirstCameraActive { get; set; } = true;
    public bool IsCameraControlsEnabled { get; set; } = false;

    public static Action<PlayerState> onGravityChange;
}
