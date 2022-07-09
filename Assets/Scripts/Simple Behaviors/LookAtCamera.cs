using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [Header("Will use MainCamera if left blank")]
    [SerializeField] Camera cam;
    
    
    [SerializeField] bool rotateYAxisOnly;
    // Start is called before the first frame update

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if(!rotateYAxisOnly)
            transform.LookAt(cam.gameObject.transform);
        else
        {
            Vector3 worldPosition = new Vector3(cam.gameObject.transform.position.x, transform.position.y, cam.gameObject.transform.position.z);
            transform.LookAt(worldPosition);
        }
    }

}
