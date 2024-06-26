using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    protected float health;
    protected bool dead;
    public float startingHealth;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
       
        health = startingHealth;
    }
    public void TakeHit(float damage, RaycastHit hit)
    {

        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {

            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        OnDeath?.Invoke();
        GameObject.Destroy(gameObject);

    }
}
