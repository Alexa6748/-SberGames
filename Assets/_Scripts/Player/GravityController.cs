using UnityEngine;
using UnityEngine.Events;

public class GravityController : MonoBehaviour
{
    private Player player;
    [SerializeField] private Transform startGravityChange;
    [SerializeField] private Transform finishGravityChange;
    [SerializeField] private RoadController road;

    private Vector3 gravityChangedDirection;
    private float maxDistance;

    private bool started;

    public static UnityAction OnGravityChange; 
    
    private void Start()
    {
        gravityChangedDirection = finishGravityChange.position - startGravityChange.position;
        maxDistance = gravityChangedDirection.magnitude;
        player = GetComponent<Player>();
    }

    public void StartGravityChange()
    {
        started = true;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out player))
        {
            if (player.HasState("Idle1"))
            {
                player.SetState("OnGravityChange");
            }
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
            PlayerGravity.NormalDirection = Vector3.Lerp(PlayerGravity.NormalDirection, normalVector, 0.5f);
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
            if (player.CurrentState.IsFirstCameraActive)
            {
                player.SetState("Idle1");
            }
            else
            {
                player.SetState("Idle");
            }
            started = false;
            GetComponent<Collider>().enabled = false;
        }
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(startGravityChange.position, finishGravityChange.position);
    }
}
