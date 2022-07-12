using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObjectSpawner))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]ObjectSpawner objSpawner;

    [SerializeField] float radius;

    public void SpawnEnemy()
    {
        Debug.Log("hi mom");
        Vector3 position;
        RandomPoint(transform.position, radius, out position);

        objSpawner.SpawnObject(position);

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

}
