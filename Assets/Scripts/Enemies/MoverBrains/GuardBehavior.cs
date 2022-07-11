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
        float v = Vector3.Distance(mover.transform.position, guardPos);
        if (v <= acceptableDist)
        {
            mover.StopMoving();
            return;
        }

        mover.MoveToPosition(guardPos);
    }
}
