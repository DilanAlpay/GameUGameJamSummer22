using UnityEngine;

public interface IMoverBrain
{
   public void Move(NavMeshMover mover);
   public bool IsAtTarget(NavMeshMover mover, Vector3 targetPos);
}
