using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAngleDet : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _x, _y, _z;
    [SerializeField] private RoadAnimatorControl roadAnim;
    private Player player;
    private bool ifInRegion = false;

    public static UnityAction OnEnterRegion;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    public void Update()
    {
        if (ifInRegion)
        {
            ChechCameraAngle();
        }
    }

    public void ChechCameraAngle()
    {
        if (IsRightAngle(-10, 10))
        {
            ifInRegion = false;
            roadAnim.StartAnimation();
            player.SetState("OnGravityChange");
            Debug.Log("cmera ok");

        }
        else
        {
            Debug.Log("camera not ok");
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            ifInRegion = true;
            player.SetState("OnRoadEnd");
        }        
    }

    public bool IsRightAngle(float min, float max)
    {
        return (_x - _camera.rotation.x) + (_y - _camera.rotation.y) + (_z - _camera.rotation.z) < max
            && (_x - _camera.rotation.x) + (_y - _camera.rotation.y) + (_z - _camera.rotation.z) > min;
    }
}

