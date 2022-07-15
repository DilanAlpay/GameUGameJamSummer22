using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] NavMeshMover mover;
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
        Vector3 scale = Vector3.one;
        scale.x = (left ? -1 : 1); 
        _facingLeft = left;
        transform.localScale = scale;
    }
}
