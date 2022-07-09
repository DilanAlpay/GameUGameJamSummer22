using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMover : MonoBehaviour,IAction
{
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    public void MoveToPosition(Vector3 position)
    {
        StartMoving();
        agent.SetDestination(position);
    }

    public void MoveToTarget(Transform target)
    {
        MoveToPosition(target.position);
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
