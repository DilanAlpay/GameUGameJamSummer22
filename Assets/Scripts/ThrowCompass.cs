using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCompass : MonoBehaviour
{
    public Throwable target;

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = target.transform.position - transform.position;
        float angle = 180 + Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.up * (angle));
    }
}
