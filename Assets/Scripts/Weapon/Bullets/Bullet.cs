using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10f;
    public float bulletDamageMultiplier = 1f;
    private TrailRenderer trailRenderer;
    public float trailDuration = 0.2f; // Adjust as needed

    private void Awake()
    {
        // Destroy the bullet after 5 seconds
        Destroy(gameObject, 5f);

        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }
    private void Start()
    {
        // Enable the Trail Renderer when the bullet is spawned
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
            StartCoroutine(DisableTrailAfterDuration(trailDuration));
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        _other.GetComponent<Entity>()?.TakeBulletDamage(this);
        Destroy(gameObject);
    }

    private IEnumerator DisableTrailAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Disable the Trail Renderer after the specified duration
        if (trailRenderer != null)
        {
            trailRenderer.emitting = false;
        }
    }

}
