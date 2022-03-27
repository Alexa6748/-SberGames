using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngleDet : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] float _x, _y, _z;
    bool ifInRegion = false;

    public void Update()
    {
        if (ifInRegion)
        {
            ChechCameraAngle();
        }
    }

    public void ChechCameraAngle()
    {
        if ((_x - _camera.transform.rotation.x) + (_y - _camera.transform.rotation.y) + (_z - _camera.transform.rotation.z) < 10 && (_x - _camera.transform.rotation.x) + (_y - _camera.transform.rotation.y) + (_z - _camera.transform.rotation.z) > -10)
        {
            ifInRegion = false;
            //анимация приближения дороги и отрубаем движение камеры, подрубаем движение и начинаем перемену гравитации
            Debug.Log("cmera ok");
        }
        else
        {
            Debug.Log("camera not ok");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        ifInRegion = true;
        //отрубить передвижение и врубить поворот камеры
    }
}

