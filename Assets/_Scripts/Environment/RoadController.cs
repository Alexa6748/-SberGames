using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(float currentValue, float maxRotation)
    {
        float x = currentValue * maxRotation * Time.deltaTime;
        newPosition = Quaternion.AngleAxis(currentValue * maxRotation, (start.position - end.position)) * newPosition;
        Vector3 newCameraPosition = transform.position + newPosition;
        transform.eulerAngles = new Vector3(x, transform.eulerAngles.x, transform.eulerAngles.z);
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, 0.03f);
    }
}
