using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] NavMeshMover mover;
    [SerializeField] bool isFacingLeftToStart = true;
    private void Update()
    {
        Vector3 direction = mover.GetMoveDirection();
        if(direction.x < 0)
        {
            spriteRenderer.flipX = !isFacingLeftToStart;
        }
        else
        {
            spriteRenderer.flipX = isFacingLeftToStart;
        }
    }
}
