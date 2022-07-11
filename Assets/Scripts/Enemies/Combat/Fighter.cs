using GameJam.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fighter : MonoBehaviour, IAction
{
    public bool isActive = true;
    [SerializeField] Transform projectileSpawn;
    public Weapon defaultWeapon;
    public Weapon equippedWeapon;
    

    float attackCooldown = 2f;
    float timeSinceLastAttack = Mathf.Infinity;

    GameObject _target;
    NavMeshMover mover; 
    LoggerTag logtag = LoggerTag.Combat;
    public float AttackRange => equippedWeapon.attackRange;
    private void Start()
    {
        mover = GetComponent<NavMeshMover>();
        if(equippedWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
        if(projectileSpawn == null)
        {
            projectileSpawn = transform;
        }
    }

    private void Update()
    {
        if (!isActive) return;
        timeSinceLastAttack += Time.deltaTime;
        if (_target == null) return;
        //if target is dead reset the target too

        if (!IsInRange(_target))
        {
            //check if target is dead
            mover.MoveToPosition(_target.transform.position);

            
        }
        else
        {
            mover.StopMoving();
            AttackBehavior();
        }
    }

    private void AttackBehavior()
    {
        transform.LookAt(_target.transform);
        if(timeSinceLastAttack > equippedWeapon.timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;
            TriggerAttack();
        }
    }

    private void TriggerAttack()
    {
        StartCoroutine(AttackCO(_target.transform));
    }

    private void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
        attackCooldown = equippedWeapon.timeBetweenAttacks;
        
    }

    public IEnumerator AttackCO(Transform target)
    {
        //Will replace with animation triggers?
        LoggerManager.i.Log($"{name} is attacking with {equippedWeapon.weaponName}!", logtag);
        if(equippedWeapon.type == WeaponType.Projectile)
        {
            Projectile proj = Instantiate(equippedWeapon.projectile, projectileSpawn.position, projectileSpawn.rotation);
            proj.SetTarget(target);
        }
        yield return null;
    }

    public bool CanAttack(GameObject target)
    {
        if (target == null) return false;

        return IsInRange(target);
    }

    public void LoseTarget()
    {
        _target = null;
    }
    private bool IsInRange(GameObject target)
    {
        return target != null && Vector3.Distance(target.transform.position, transform.position) <= AttackRange;
    }

    public void Attack(GameObject target)
    {
        GetComponent<ActionScheduler>()?.StartAction(this);
        _target = target;
    }

    public void CancelAction()
    {
        _target = null;
        mover.StopMoving();
    }
}
