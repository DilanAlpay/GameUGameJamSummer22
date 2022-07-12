using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour,IAction
{
    public float maxSpeed = 3;
    float currentSpeed;
    NavMeshAgent agent;
    Action onArrived;
    bool isPaused;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = maxSpeed;
        currentSpeed = maxSpeed;
    }

    private void Update()
    {
        if (isPaused) return;
        if(Vector3.Distance(transform.position,agent.destination) <= 1)
        {
            onArrived?.Invoke();
            onArrived = null;
        }
    }

    public void PauseMovement()
    {
        isPaused = true;
        agent.speed = 0;

    }

    public void UnpauseMovement()
    {
        isPaused = false;
        agent.speed = maxSpeed;
    }

    public void MoveToPosition(Vector3 position, Action onArrivedAction = null)
    {
        StartMoving();
        agent.SetDestination(position);
        onArrived = onArrivedAction;
    }

    public void MoveToTarget(Transform target, Action onArrivedAction = null)
    {
        if(target != null)
            MoveToPosition(target.position, onArrivedAction);
    }


    public void StartMoveAction(Vector3 position, Action onArrivedAction = null)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        MoveToPosition(position, onArrivedAction);
     
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void StartMoving()
    {
        agent.isStopped = false;
    }

    public void ResetMoving()
    {
        StartMoving();
        MoveToTarget(transform);
    }
    public void CancelAction()
    {
        StopMoving();
    }
}
