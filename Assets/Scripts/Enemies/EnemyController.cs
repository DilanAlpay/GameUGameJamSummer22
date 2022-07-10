using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPausable
{
    public LayerMask targetLayers;

    NavMeshMover mover;
    Health health;
    Fighter fighter;
    [SerializeField]LineOfSight vision;
    [SerializeField] GameObject target;

    public enum EnemyType
    {
        Dumb,
        Guard,
        Patrol
    }

    public EnemyType enemyType;
    private Vector3 startPosition;

    public float susTime = 1f;
    public float timeSinceLastSawPlayer = Mathf.Infinity;

    private void Awake()
    {
        mover = GetComponent<NavMeshMover>();
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        vision.SetTargetLayers(targetLayers);
        startPosition = transform.position;
    }

    private void Update()
    {
        if (health.IsDead)
        {
            return;
        }

        target = vision.GetTarget();
        if (target != null)
        {
            Debug.Log("I have a target!");
            AttackBehavior();
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
        
        if(enemyType == EnemyType.Dumb)
        {
            
            mover.StopMoving();
        }
        else if(enemyType == EnemyType.Guard)
        {
            if(Vector3.Distance(transform.position, startPosition) >= 1f)
                mover.MoveToPosition(startPosition, ()=>Debug.Log("Hide"));
        }
        else if(enemyType == EnemyType.Patrol)
        {
            mover.MoveToPosition(startPosition, () => Debug.Log("Patrol"));
        }
        
    }

    private void PatrolBehavior()
    {
        throw new NotImplementedException();
    }

    private void SuspiciousBehavior()
    {
        fighter.LoseTarget();
        mover.StopMoving();
    }

    private void AttackBehavior()
    {
        timeSinceLastSawPlayer = 0f;
        fighter.Attack(target);
    }
    public void Pause()
    {
        mover.PauseMovement();
        fighter.isActive = false;
    }

    public void Unpause()
    {
        fighter.isActive = true;
        mover.UnpauseMovement();
    }
}
