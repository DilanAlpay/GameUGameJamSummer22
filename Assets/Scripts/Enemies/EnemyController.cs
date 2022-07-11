using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour, IPausable
{
    public LayerMask targetLayers;

    NavMeshMover mover;
    Health health;
    Fighter fighter;
    [SerializeField]LineOfSight vision;
    IMoverBrain moverBrain;

    public float susTime = 1f;
    public float timeSinceLastSawPlayer = Mathf.Infinity;

    public GameObjectEvent onAttackBehavior;
    public UnityEvent onLoseTarget;

    public UnityEvent pause;
    public UnityEvent unpause;

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
        vision.SetTargetLayers(targetLayers);

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

    private void PatrolBehavior()
    {
        throw new NotImplementedException();
    }

    private void SuspiciousBehavior()
    {
        onLoseTarget?.Invoke();
        //fighter.LoseTarget();
        mover.StopMoving();
    }

    private void AttackBehavior(GameObject target)
    {
        timeSinceLastSawPlayer = 0f;
        //fighter.Attack(target);
        onAttackBehavior?.Invoke(target);
    }
    public void Pause()
    {
        mover.PauseMovement();
        pause?.Invoke();
    }

    public void Unpause()
    {
        unpause?.Invoke();
        mover.UnpauseMovement();
    }
}
