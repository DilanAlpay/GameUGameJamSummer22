using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : MonoBehaviour,IMoverBrain
{
    Vector3 guardPos;
    [SerializeField] float acceptableDist = 1f;
    private void Awake()
    {
       guardPos = transform.position;
    }

    public void Move(NavMeshMover mover)
    {
        if (IsAtTarget(mover, guardPos))
        {
            mover.StopMoving();
            return;
        }

        mover.MoveToPosition(guardPos);
    }

    public bool IsAtTarget(NavMeshMover mover, Vector3 targetPos)
    {
        return Vector3.Distance(mover.transform.position, targetPos) <= acceptableDist;
    }
}
