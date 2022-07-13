using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] LineOfSight vision;
    [SerializeField] Weapon weapon;
    [SerializeField] Transform spawnPoint;
    [SerializeField] bool yMove;
    [SerializeField] bool resetCounterOnActivate;
    float timeSinceLastShot = Mathf.Infinity;
    [SerializeField]bool isActive = true;

    private void Awake()
    {
        if(spawnPoint == null)
        {
            spawnPoint = transform;
        }
    }

    private void Update()
    {
        if (!isActive) return;
        if(timeSinceLastShot >= weapon.timeBetweenAttacks)
        {
            if(FireTurret())
                timeSinceLastShot = 0;
            
        }
        timeSinceLastShot += Time.deltaTime;

    }

    private bool FireTurret()
    {
        
        GameObject target = vision.GetTarget();
        if (target != null)
        {
            Projectile projectile = Instantiate(weapon.projectile, spawnPoint.position,spawnPoint.rotation);
            projectile.SetTarget(target.transform, yMove);
            return true;
        }

        return false;
        
    }

    public void Activate()
    {
        isActive = true;
        if (resetCounterOnActivate)
            timeSinceLastShot = weapon.timeBetweenAttacks/2;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
