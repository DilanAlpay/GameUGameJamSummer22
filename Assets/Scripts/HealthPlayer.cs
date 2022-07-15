using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class HealthPlayer : Health
{
    public UnityEvent<GameObject> onTakeDamagePlayer;

    protected override void ChangeHealth(int amount, GameObject source = null)
    {
        if (isDead) return;
        //We are still temporarily invincible and cannot take damage
        if ((isInvincible || Time.time < iFinish) && amount < 0) return;

        int prevHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > prevHealth)
        {
            onHeal?.Invoke();
        }

        if (currentHealth < prevHealth)
        {
            onTakeDamagePlayer.Invoke(source);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
