using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] LayerMask obsticle;
    private Transform oldObsticle;
    private void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f, obsticle))
        {
            oldObsticle = hit.transform;
            SetObjectTransparency(hit.transform.GetComponent<MeshRenderer>().material, 0.3f);
        }
        else
        {
            if (oldObsticle != null)
            {
                SetObjectTransparency(oldObsticle.GetComponent<MeshRenderer>().material, 1);
                oldObsticle = null;
            }
        }
    }
    private void SetObjectTransparency(Material objectMaterial, float transparency)
    {
        Color color = objectMaterial.color;
        color.a = transparency;
        objectMaterial.color = color;
    }

}
