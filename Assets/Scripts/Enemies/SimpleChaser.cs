using GameJam.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMeshMover))]
public class SimpleChaser : MonoBehaviour
{
    public Transform target;
    public float checkPositionTime = 0.25f;

    
    NavMeshMover mover;
    Fighter fighter;
    LineOfSight los;
    bool isChasing;
    WaitForSeconds delay;

    LoggerTag logtag = LoggerTag.Enemy;
    private void Awake()
    {
        mover = GetComponent<NavMeshMover>();
        los = GetComponent<LineOfSight>();
        fighter = GetComponent<Fighter>();
        los.SetTarget(target);
        delay = new WaitForSeconds(checkPositionTime);
    }


    void Start()
    {
        
    }

    IEnumerator Chase()
    {
        while (isChasing)
        {

            mover.MoveToTarget(target);
            if (Vector3.Distance(transform.position, target.position) <= fighter.AttackRange)
            {
                    mover.CancelAction();
                    
                    LoggerManager.i.Log("Attacking time!", logtag);
                    yield return new WaitForSeconds(1f);
                    mover.ResetMoving();
                    
            }
            else
                yield return delay;
        }

            
        
    }
    public void StartChasing()
    {
        LoggerManager.i.Log("starting to chase", logtag);
        isChasing = true;
        StartCoroutine(Chase());
        mover.StartMoving();
    }



    public void StopChasing()
    {
        isChasing = false;
        mover.StopMoving();

    }
}
