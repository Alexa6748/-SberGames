using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStateController", menuName = "ScriptableObjects/PlayerStateController", order = 51)]
public class PlayerStateController : ScriptableObject
{
    [SerializeField] public List<PlayerState> states = new List<PlayerState>();
    
    [SerializeField] public PlayerState CurrentState;

    public static UnityAction OnStateChange;

    public PlayerState this[string stateName]
    {
        get => states.Find(State => State.name == stateName);
    }

    public void SetCurrentState(string stateName)
    {
        CurrentState = this[stateName];
        OnStateChange?.Invoke();
    }

    public bool IsCurrentState(params string[] stateNameList)
    {
        foreach (string stateName in stateNameList)
        {
            if (this[stateName] == CurrentState)
            {
                return true;
            }
        }
        return false;
    }
}
