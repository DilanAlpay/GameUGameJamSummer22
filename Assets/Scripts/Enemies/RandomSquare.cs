using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSquare : MonoBehaviour, IRandomPoint
{
    public float minX, maxX, minZ, maxZ;
    public float y = 0f;
    public Vector3 GetRandomPoint()
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 bottomLeft = new Vector3(minX, y, minZ);
        Vector3 topLeft = new Vector3(minX, y, maxZ);
        Vector3 topRight = new Vector3(maxX, y, maxZ);
        Vector3 bottomRight = new Vector3(maxX, y, minZ);
        Gizmos.DrawSphere(bottomRight, 1f);
        Gizmos.DrawSphere(bottomLeft, 1f);
        Gizmos.DrawSphere(topLeft, 1f);
        Gizmos.DrawSphere(topRight, 1f);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
