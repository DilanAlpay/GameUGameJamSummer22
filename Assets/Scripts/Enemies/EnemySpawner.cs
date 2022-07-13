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

        Vector3 position;
        NavMeshHelper.RandomPoint(transform.position, radius, out position);

        objSpawner.SpawnObject(position);

    }


}
