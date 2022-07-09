using GameJam.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleChaser : EnemyBase
{
    bool isChasing;
    Vector3 startPos;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        startPos = transform.position;
    }
    IEnumerator Chase()
    {
        while (isChasing)
        {

            mover.MoveToTarget(target);
            if (Vector3.Distance(transform.position, target.position) <= fighter.AttackRange)
            {
                    mover.CancelAction();
                    yield return fighter.Attack();
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


    }

    public void ReturnToStartingPosition()
    {
        LoggerManager.i.Log("Go home buddy",logtag);
        isChasing = false;
        mover.MoveToPosition(startPos);
    }
}
