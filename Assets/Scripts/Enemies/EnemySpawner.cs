using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ObjectSpawner))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]ObjectSpawner objSpawner;
    [SerializeField] RandomSquare square;
    [SerializeField] float radius;

    public void SpawnEnemy()
    {
        Vector3 position;

        if (square != null)
        {
            position = square.GetRandomPoint();
        }
        else
        {
            NavMeshHelper.RandomPoint(transform.position, radius, out position);
        }
        objSpawner.SpawnObject(position);

    }


}
