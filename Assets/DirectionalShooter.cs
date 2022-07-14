using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooter : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileSpawn;
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
        Projectile tempProjectile = Instantiate(projectile, transform.position, Quaternion.Euler(0, yAngle, 0));
    }

    public void EightShot()
    {

            for(int i = 0; i < 360; i += 45)
            {
                Shoot(i+offset);
            }
            offset += 15;
            offset = offset % 45;

        
    }
}
