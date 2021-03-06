using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth;

    [SerializeField] int size;
    [SerializeField] UnityEvent onTakeDamage;
    [SerializeField] protected UnityEvent onHeal;
    [SerializeField] protected UnityEvent onDie;
    [SerializeField] GameEvent hitBySmallBubble;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected float iFrames;

    protected bool isDead;
    protected bool isInvincible;
    public int HP => currentHealth;
    public bool IsDead => isDead;
    public UnityEvent Death => onDie;
    public bool AtMax { get { return currentHealth == maxHealth; } }

    /// <summary>
    /// Time when the iFrames run out
    /// </summary>
    protected float iFinish;
    
    private void Start()
    {
        currentHealth = maxHealth;        
    }

    public void Squish(int bubbleSize, GameObject source = null)
    {
        if (bubbleSize > size)
        {
            TakeDamage(maxHealth, source);
        }
        else
        {
            hitBySmallBubble?.Call();
        }
    }

    public virtual void TakeDamage(int damage, GameObject source = null)
    {
        ChangeHealth(-damage, source);
    }

    public void Heal(int amount, GameObject source = null)
    {
        ChangeHealth(amount, source);
    }
    
    public void Revive()
    {
        isDead = false;
    }

    protected virtual void ChangeHealth(int amount, GameObject source = null)
    {
        if (isDead) return;

        //We are still temporarily invincible and cannot take damage
        if ((isInvincible || Time.time < iFinish) && amount < 0) return;
        

        int prevHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if(currentHealth > prevHealth)
        {
            onHeal?.Invoke();
        }

        if (currentHealth < prevHealth)
        {
            iFinish = Time.time + iFrames;
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
        GetComponent<ActionScheduler>()?.CancelCurrentAction();
        onDie?.Invoke();
    }

    public void SetInvincible()
    {
        isInvincible = true;
    }

    public void SetNotInvincible()
    {
        isInvincible = false;
    }

}
