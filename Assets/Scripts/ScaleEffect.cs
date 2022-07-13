using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField] Transform effectedObj;
    [SerializeField] float maxScale;
    float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = Vector3.Distance(transform.position, effectedObj.position);
    }

    // Update is called once per frame
    void Update()
    {
        effectedObj.transform.localScale = maxScale*Vector3.one*(1- Vector3.Distance(transform.position,effectedObj.position)/maxDistance);
    }
}
