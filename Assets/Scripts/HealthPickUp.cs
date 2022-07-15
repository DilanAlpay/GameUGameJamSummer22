using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] int healAmount = 1;
    private void OnTriggerEnter(Collider other)
    {
        HealthPlayer health = other.GetComponent<HealthPlayer>();
        
        if (!health || health.AtMax) return;

        health.Heal(1);
        Destroy(gameObject);
    }
}
