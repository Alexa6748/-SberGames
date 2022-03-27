using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerState", menuName = "ScriptableObjects/PlayerState", order = 51)]
public class PlayerState : State
{
    [SerializeField]
    public bool IsFirstCameraActive = true;

    [SerializeField]
    public bool IsCameraControlsEnabled = false;

    [SerializeField]
    public bool IsMovementEnabled = true;

    [SerializeField]
    public string AnimationTrigger { get; set; }

    [SerializeField]
    public bool UseGravity = true;

    [SerializeField]

    public float PlayerSpeed = 4;
}
