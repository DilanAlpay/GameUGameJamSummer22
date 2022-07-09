using GameJam.Util;
using UnityEngine;

[RequireComponent(typeof(NavMeshMover),typeof(LineOfSight))]
public class EnemyBase : MonoBehaviour
{
    public LayerMask targetLayers;
    
    public float checkPositionTime = 0.25f;

    public LoggerTag logtag = LoggerTag.Enemy;
    protected NavMeshMover mover;
    protected Fighter fighter;
    protected LineOfSight los;
    protected WaitForSeconds delay;
    protected Transform target;

    protected virtual void Awake()
    {
        mover = GetComponent<NavMeshMover>();
        los = GetComponent<LineOfSight>();
        fighter = GetComponent<Fighter>();
        los.SetTargetLayers(targetLayers);
        delay = new WaitForSeconds(checkPositionTime);
    }

    public void SetTarget(Transform tar)
    {

        target = tar;
    }
}