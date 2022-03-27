using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    private Player player;
    [SerializeField] private Transform startGravityChange;
    [SerializeField] private Transform finishGravityChange;
    [SerializeField] private RoadController road;

    private Vector3 gravityChangedDirection;
    private float maxDistance;

    private bool started;
    
    private void Start()
    {
        gravityChangedDirection = finishGravityChange.position - startGravityChange.position;
        maxDistance = gravityChangedDirection.magnitude;
        player = GetComponent<Player>();
    }

    private void StartGravityChange()
    {
        started = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out player))
        {
            player.CurrentState.IsFirstCameraActive = !player.CurrentState.IsFirstCameraActive;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (started && collider.TryGetComponent(out player))
        {
            Vector3 directionToEnd = player.transform.position - finishGravityChange.position;
            
            float distanceToEnd = Vector3.Project(directionToEnd, gravityChangedDirection).magnitude;
            Vector3 normalVector = startGravityChange.up * (1 - distanceToEnd / maxDistance) 
                + finishGravityChange.up * distanceToEnd / maxDistance;
            PlayerGravity.NormalDirection = normalVector;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out player))
        {
            Vector3 directionToEnd = player.transform.position - finishGravityChange.position;

            float distanceToEnd = Vector3.Project(directionToEnd, gravityChangedDirection).magnitude;
            Vector3 normalVector = finishGravityChange.up * distanceToEnd / maxDistance;
            PlayerGravity.NormalDirection = normalVector;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(startGravityChange.position, finishGravityChange.position);
    }
}
