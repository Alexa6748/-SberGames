using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    [SerializeField] private Transform startGravityChange;
    [SerializeField] private Transform finishGravityChange;
    [SerializeField] private RoadController road;

    private PlayerGravity player;
    private Vector3 gravityChangedDirection;
    private float maxDistance;
    
    private void Start()
    {
        
        gravityChangedDirection = finishGravityChange.position - startGravityChange.position;
        maxDistance = gravityChangedDirection.magnitude;
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out player))
        {
            PlayerState.CurrentState.IsFirstCameraActive = !PlayerState.CurrentState.IsFirstCameraActive;
            PlayerState.onGravityChange?.Invoke(PlayerState.CurrentState);
            player.GetComponent<Rigidbody>().freezeRotation = false;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.TryGetComponent(out player))
        {
            Vector3 directionToEnd = player.transform.position - finishGravityChange.position;
            
            float distanceToEnd = Vector3.Project(directionToEnd, gravityChangedDirection).magnitude;
            Vector3 normalVector = finishGravityChange.up * distanceToEnd / maxDistance;
            PlayerState.CurrentState.NormalDirection = normalVector;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(startGravityChange.position, finishGravityChange.position);
    }
}
