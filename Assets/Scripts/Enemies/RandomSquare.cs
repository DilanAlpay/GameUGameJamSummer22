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
        return new Vector3(transform.position.x+x, y, transform.position.zz);
    }

    private void OnDrawGizmosSelected()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Vector3 bottomLeft = new Vector3(x+minX, y, z+minZ);
        Vector3 topLeft = new Vector3(x+minX, y, z+maxZ);
        Vector3 topRight = new Vector3(x+maxX, y, z+maxZ);
        Vector3 bottomRight = new Vector3(x + maxX, y, z+minZ);
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
