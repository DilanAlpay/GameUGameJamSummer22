using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage;

    public void DealDamage(GameObject health)
    {
        Health targetHealth = health.GetComponent<Health>();
        if(targetHealth != null)
        {
            targetHealth.TakeDamage(damage, this.gameObject);
        }
   
    }
}
