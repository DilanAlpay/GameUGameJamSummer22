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

    public IEnumerator Attack()
    {
        LoggerManager.i.Log($"{name} is attacking with {equippedWeapon.weaponName}!", logtag);

        yield return new WaitForSeconds(defaultWeapon.attackTime);
    }
}
