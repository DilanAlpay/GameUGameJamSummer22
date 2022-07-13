using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour, IMoverBrain
{
    [SerializeField] float distToWaypoint = 1f;
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    int currWaypoint = 0;

    public bool IsAtTarget(NavMeshMover mover, Vector3 targetPos)
    {
        
        return Vector3.Distance(mover.transform.position, targetPos) <= distToWaypoint;
    }

    public void Move(NavMeshMover mover)
    {
        Vector3 targetPos = waypoints[currWaypoint].position;
        mover.MoveToPosition(targetPos);
        if (IsAtTarget(mover, targetPos))
        {
            currWaypoint++;
            if(currWaypoint >= waypoints.Count)
            {
                currWaypoint = 0;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints.Count == 0) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(waypoints[0].position, 0.3f);
        for (int i = 1; i < waypoints.Count; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.3f);
            Gizmos.DrawLine(waypoints[i].position, waypoints[i - 1].position);
            
        }
        Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
       

    }
}
