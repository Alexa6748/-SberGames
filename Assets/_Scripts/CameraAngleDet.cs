using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAngleDet : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] public float _x, _y, _z;
    [SerializeField] private RoadAnimatorControl roadAnim;
    [SerializeField] private Player player;
    private bool ifInRegion = false;

    public static UnityAction OnEnterRegion;

    public void Update()
    {
        if (ifInRegion)
        {
            ChechCameraAngle();
        }
    }

    public void ChechCameraAngle()
    {
        if (IsRightAngle(-20, 20))
        {
            ifInRegion = false;
            roadAnim.StartAnimation();
            player.SetState("OnGravityChange");
            Debug.Log("cmera ok");

        }
        else
        {
            Debug.Log(_camera.rotation.eulerAngles.y);
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
        return (_x - _camera.rotation.eulerAngles.x) + (_y - _camera.rotation.eulerAngles.y) + (_z - _camera.rotation.eulerAngles.z) < max
            && (_x - _camera.rotation.eulerAngles.x) + (_y - _camera.rotation.eulerAngles.y) + (_z - _camera.rotation.eulerAngles.z) > min;
    }
}

