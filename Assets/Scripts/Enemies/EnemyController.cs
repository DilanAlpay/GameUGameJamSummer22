using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask targetLayers;

    NavMeshMover mover;
    Health health;
    Fighter fighter;


    private void Awake()
    {
        mover = GetComponent<NavMeshMover>();
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
    }

    private void Update()
    {
        if (health.IsDead) return;

        
    }


}
