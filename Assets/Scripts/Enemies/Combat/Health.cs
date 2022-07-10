using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth;

    
    [SerializeField] UnityEvent onTakeDamage;
    [SerializeField] UnityEvent onHeal;
    [SerializeField] UnityEvent onDie;

    int currentHealth;
    bool isDead;
    public bool IsDead => isDead;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, GameObject source = null)
    {
        ChangeHealth(-damage, source);
    }

    public void Heal(int amount, GameObject source = null)
    {
        ChangeHealth(amount, source);
    }

    void ChangeHealth(int amount, GameObject source = null)
    {
        if (isDead) return;
        int prevHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth > prevHealth)
        {
            onHeal?.Invoke();
        }

        if (currentHealth < prevHealth)
        {
            onTakeDamage?.Invoke();
        }

        if(currentHealth <= 0)
        {
            Die();
        }

    }


    public void Die()
    {
        if (isDead) return;
        isDead = true;
        onDie?.Invoke();
    }


}
