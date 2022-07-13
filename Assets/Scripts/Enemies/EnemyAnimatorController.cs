using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] NavMeshMover mover;
    [SerializeField] bool isFacingLeftToStart = true;
    private bool _facingLeft = true;

    private void Awake()
    {
        mover = GetComponentInParent<NavMeshMover>();
    }

    private void Update()
    {
        Vector3 direction = mover.GetMoveDirection();
        if(direction.x > 0 && _facingLeft)
        {
            Flip(true);
        }
        else if(direction.y < 0 && !_facingLeft)
        {
            Flip(false);
        }
    }

    private void Flip(bool left)
    {
        foreach(Transform child in transform)
        {
            child.localEulerAngles = Vector3.up * (left ? 180 : 0);
            _facingLeft = left;
        }
    }
}
