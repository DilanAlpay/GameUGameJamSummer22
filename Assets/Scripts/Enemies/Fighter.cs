using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Weapon defaultWeapon;

    public Weapon equippedWeapon;


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
        
        yield return new WaitForSeconds(defaultWeapon.attackTime);
    }
}
