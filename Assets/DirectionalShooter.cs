using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooter : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileSpawn;
    [Range(0,180)]
    [SerializeField] float rotateStep;
    [Range(1,36)]
    [SerializeField] int numMultiShots = 8; 
    int offset = 0;
    private void Awake()
    {
        if(projectileSpawn == null)
        {
            projectileSpawn = transform;
        }
    }
    public void Shoot(float yAngle)
    {
        Projectile tempProjectile = Instantiate(projectile, projectileSpawn.position, Quaternion.Euler(0, yAngle, 0));
    }

    public void MultiShot()
    {

            for(int i = 0; i < 360; i += 360/numMultiShots)
            {
                Shoot(i+offset);
            }
            offset += 15;
            offset = offset % 360;

        
    }
}
