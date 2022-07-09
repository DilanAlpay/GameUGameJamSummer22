using GameJam.Util;
using UnityEngine;

[RequireComponent(typeof(NavMeshMover),typeof(LineOfSight))]
public class EnemyBase : MonoBehaviour
{
    public Transform target;
    
    public float checkPositionTime = 0.25f;

    public LoggerTag logtag = LoggerTag.Enemy;
    protected NavMeshMover mover;
    protected Fighter fighter;
    protected LineOfSight los;
    protected WaitForSeconds delay;

    protected virtual void Awake()
    {
        mover = GetComponent<NavMeshMover>();
        los = GetComponent<LineOfSight>();
        fighter = GetComponent<Fighter>();
        los.SetTarget(target);
        delay = new WaitForSeconds(checkPositionTime);
    }


}