using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage;

    public void DealDamage(GameObject health)
    {
        Debug.Log($"Deal {damage} to {health}");
    }
}
