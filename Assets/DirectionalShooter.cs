using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalShooter : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileSpawn;
    private void Awake()
    {
        if(projectileSpawn == null)
        {
            projectileSpawn = transform;
        }
    }
    public void Shoot(float yAngle);
}
