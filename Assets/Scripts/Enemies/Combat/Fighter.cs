using GameJam.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fighter : MonoBehaviour
{
    public Weapon defaultWeapon;

    public Weapon equippedWeapon;

    GameObject target;

    LoggerTag logtag = LoggerTag.Combat;
    public float AttackRange => equippedWeapon.attackRange;
    private void Start()
    {
        if(equippedWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
    }

    private void Update()
    {
        
    }


    private void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
        
    }

    public IEnumerator AttackCO(Transform target)
    {
        LoggerManager.i.Log($"{name} is attacking with {equippedWeapon.weaponName}!", logtag);

        yield return new WaitForSeconds(equippedWeapon.attackTime);
        if(equippedWeapon.type == WeaponType.Projectile)
        {
            Projectile proj = Instantiate(equippedWeapon.projectile, transform.position, transform.rotation);
            proj.SetTarget(target);
        }
    }

    public bool CanAttack(GameObject target)
    {
        if (target == null) return false;

        return IsInRange(target);
    }

    private bool IsInRange(GameObject target)
    {
        return target != null && Vector3.Distance(target.transform.position ,transform.position) <= AttackRange;
    }

    public void Attack(GameObject target)
    {

    }
}
