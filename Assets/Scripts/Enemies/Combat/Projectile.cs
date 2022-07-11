using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPausable
{
    [SerializeField] bool isHoming;
    Bullet bullet;
    Transform target;
    
    bool isPaused;

    private void Awake()
    {
        bullet = GetComponent<Bullet>();
    }
    public void SetTarget(Transform newTarget, float damage = 0)
    {
        target = newTarget;
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
        return new Vector3(target.position.x, transform.position.y, target.position.z);
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
