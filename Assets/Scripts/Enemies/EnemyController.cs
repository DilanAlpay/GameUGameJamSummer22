using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    NavMeshMover mover;
    Health health;
    //Fighter fighter;
    [SerializeField]LineOfSight vision;
    IMoverBrain moverBrain;

    public float susTime = 1f;
    public float timeSinceLastSawPlayer = Mathf.Infinity;

    public GameObjectEvent onAttackBehavior;
    public UnityEvent onLoseTarget;

    private void Awake()
    {
        mover = GetComponent<NavMeshMover>();

        health = GetComponent<Health>();
        //fighter = GetComponent<Fighter>();
        moverBrain = GetComponent<IMoverBrain>();
        if (moverBrain == null)
        {
            Debug.Log($"{name}: No movement pattern found. Using default behavior");
            moverBrain = gameObject.AddComponent<DumbBehavior>();
        }        
    }

    private void Update()
    {
        if (health.IsDead)
        {
            return;
        }

        GameObject target = vision.GetTarget();
        if (target != null)
        {
            AttackBehavior(target);
        }
        else if( timeSinceLastSawPlayer <= susTime ) //Suspicious behavior
        {
        
            SuspiciousBehavior();
        }
        else
        {
            MovementBehavior();
        }
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void MovementBehavior()
    {

        moverBrain.Move(mover);
        
    }


    private void SuspiciousBehavior()
    {
        onLoseTarget?.Invoke();
        mover.StopMoving();
    }

    private void AttackBehavior(GameObject target)
    {
        timeSinceLastSawPlayer = 0f;
        onAttackBehavior?.Invoke(target);
    }

}
