using GameJam.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Weapon defaultWeapon;

    public Weapon equippedWeapon;

    LoggerTag logtag = LoggerTag.Combat;
    public float AttackRange => equippedWeapon.attackRange;
    private void Start()
    {
        if(equippedWeapon == null)
        {
            EquipWeapon(defaultWeapon);
        }
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
}
