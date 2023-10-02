using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [Header("Basic Stats")]
    public float maxHealth;
    public float currentHealth;
    public float damage;

    // events
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Entity attacker)
    {

        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
            // damaged animation
            OnTakeDamage?.Invoke(attacker.transform);
            // Debug.Log("Took Damage");
        }
        else
        {
            currentHealth = 0;
            Die();
            OnDeath?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Entity>()?.TakeDamage(this);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}