using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon",menuName = "Enemies/Create Weapon") ]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public WeaponType type = WeaponType.Melee;
    public GameObject projectile;
    public float attackRange;

    public float attackTime;
}

public enum WeaponType
{
    Melee,
    Projectile
}