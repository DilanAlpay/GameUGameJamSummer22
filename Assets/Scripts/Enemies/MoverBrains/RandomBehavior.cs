using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RandomBehavior : MonoBehaviour, IMoverBrain
{
    [SerializeField] float distToTarget = 1f;
    [SerializeField] RandomSquare square;
    [SerializeField] UnityEvent startedMoving;
    [SerializeField] UnityEvent arrivedAtTarget;
    [SerializeField] float waitTime = 3f;

    [SerializeField] float radius = 10;
    float counter = Mathf.Infinity;
    bool hasArrived = true;
    Vector3 targetPosition = new Vector3();

    public bool isAtTarget => Vector3.Distance(transform.position, targetPosition) < distToTarget;
    private void Awake() => targetPosition = transform.position;

    private void Update()
    {
        Move(GetComponent<NavMeshMover>());
    }
    public bool IsAtTarget(NavMeshMover mover, Vector3 targetPos)
    {
        float v = Vector3.Distance(mover.transform.position, targetPos);
        bool isAtTarget = v <= distToTarget;
        
        if(!hasArrived && isAtTarget)
        {
            
            counter = 0;
            mover.StopMoving();
            arrivedAtTarget?.Invoke();
            hasArrived = true;
        }
        return isAtTarget;
    }

    public void Move(NavMeshMover mover)
    {
        if (IsAtTarget(mover, targetPosition))
        {

            if (counter >= waitTime)
                MoveToRandomPosition(mover);
            counter += Time.deltaTime;
        }
    }

    public void MoveToRandomPosition(NavMeshMover mover)
    {

        startedMoving?.Invoke();

        hasArrived = false;

        for (int i = 0; i < 20; i++) {
            Vector3 randomPoint = new Vector3();
            if(square)
                randomPoint = square.GetRandomPoint();
            else
            {
                NavMeshHelper.RandomPoint(transform.position, radius, out randomPoint);
            }
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                targetPosition = hit.position;
                break;            
            }
        }
       
        mover.MoveToPosition(targetPosition);

    }

   
}
