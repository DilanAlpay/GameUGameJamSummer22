using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    public LayerMask ground;
    private float _offset = 0.05f;
    // Update is called once per frame
    void Update()
    {
        CastShadow();
    }


    private void CastShadow()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ground))
        {
            transform.GetChild(0).position = hit.point + Vector3.up * _offset;

        }
    }
}
