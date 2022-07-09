using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public float height;
    public float thickness = 3;

    public void Throw(Vector3 d, float t)
    {
        transform.parent = null;
        StartCoroutine(MoveToPoint(d, t));
    }


    IEnumerator MoveToPoint(Vector3 dest, float time)
    {
        Vector3 start = transform.position;
        float elapsed = 0;

        while(elapsed < time)
        {
            transform.position = MathParabola.Parabola(start, dest, height, elapsed/time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = dest;
    }


}
