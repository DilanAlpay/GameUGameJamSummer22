using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPausable
{
    [SerializeField] bool isHoming;
    [SerializeField] bool canMoveY = false;
    [SerializeField] bool useOffset;
    [SerializeField] float offset = 0.7f;
    Bullet bullet;
    Transform target;
    
    bool isPaused;

    private void Awake()
    {
        bullet = GetComponent<Bullet>();
    }
    public void SetTarget(Transform newTarget, bool yMove = false, float damage = 0)
    {
        target = newTarget;
        canMoveY = yMove;
        transform.LookAt(GetAimLocation());
        
    }

    private void Update()
    {
        if (isPaused) return;

        //fix this part eventually
        if (bullet != null)
        {
            if (isHoming)
                bullet.UpdateHomingPosition(GetAimLocation());
            else
                bullet.UpdatePosition();
        }

    }

    Vector3 GetAimLocation()
    {
        float ypos = canMoveY ? target.position.y : transform.position.y;
        if (useOffset) ypos += offset;
        return new Vector3(target.position.x, ypos, target.position.z);
    }

    public void Pause()
    {
        isPaused = true;
        
    }

    public void Unpause()
    {
        isPaused = false;
    }


}
