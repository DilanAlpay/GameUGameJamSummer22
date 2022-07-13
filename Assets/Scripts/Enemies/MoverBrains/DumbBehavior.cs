using UnityEngine;

public class DumbBehavior : MonoBehaviour, IMoverBrain
{
    public bool IsAtTarget(NavMeshMover mover, Vector3 targetPos)
    {
        return Vector3.Distance(mover.transform.position, targetPos) <= 1f;
    }

    public void Move(NavMeshMover mover)
    {
        mover.StopMoving();
    }
}