using UnityEngine;

public class DumbBehavior : MonoBehaviour, IMoverBrain
{
    public void Move(NavMeshMover mover)
    {
        mover.StopMoving();
    }
}