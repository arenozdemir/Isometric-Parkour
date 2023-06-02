using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] LayerMask obsticle;

    float value;
    
    bool isObsticle;
    bool isRotatingStarted;
    
    Quaternion beginRotation;
    private void Start()
    {
        beginRotation = transform.rotation;
    }
    private void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f, obsticle))
        {
            if (!isRotatingStarted)
            {
                isObsticle = true;
                isRotatingStarted = true;
            }
        }
        Rotation();
    }

    void Rotation()
    {
        if (isObsticle)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = beginRotation;
        }
    }
}
