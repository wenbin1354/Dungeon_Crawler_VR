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
    public float damageMultiplier = 1f;

    public bool useHealthBar = false;

    [SerializeField] private HealthBar healthBar;
    // events
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDeath;

    private void Awake()
    {

    }

    private void Start()
    {
        currentHealth = maxHealth;
        if (useHealthBar)
        {
            healthBar = GetComponentInChildren<HealthBar>();
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            // Debug.Log("Health Bar Found");
        }
    }

    public virtual void TakeDamage(Entity _attacker)
    {

        if (currentHealth - _attacker.damage * _attacker.damageMultiplier > 0)
        {
            currentHealth -= _attacker.damage * _attacker.damageMultiplier;
            // damaged animation
            OnTakeDamage?.Invoke(_attacker.transform);
            // Debug.Log("Took Damage");
        }
        else
        {
            currentHealth = 0;
            Die();
            OnDeath?.Invoke();
        }

        if (useHealthBar)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

            if (currentHealth == 0)
            {
                StartCoroutine(DisableHealthBarAfterDelay(1f));
            }
        }
    }

    public virtual void TakeBulletDamage(Bullet _bullet)
    {
        if (currentHealth - _bullet.bulletDamage * _bullet.bulletDamageMultiplier > 0)
        {
            currentHealth -= _bullet.bulletDamage * _bullet.bulletDamageMultiplier;
            // damaged animation
            OnTakeDamage?.Invoke(_bullet.transform);
            // Debug.Log("Took Damage");
        }
        else
        {
            currentHealth = 0;
            Die();
            OnDeath?.Invoke();
        }

        if (useHealthBar)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

            if (currentHealth == 0)
            {
                StartCoroutine(DisableHealthBarAfterDelay(1f));
            }
        }
    }

    private void OnTriggerStay(Collider _other)
    {
        _other.GetComponent<Entity>()?.TakeDamage(this);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator DisableHealthBarAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Disable the health bar after the delay
        healthBar.gameObject.SetActive(false);
    }

}