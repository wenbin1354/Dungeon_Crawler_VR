using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity
{
    private Animator anim;
    private AudioSource audioSource;
    [SerializeField] private AudioClip onHitSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    protected override void Die()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }

    public override void TakeBulletDamage(Bullet _bullet)
    {
        base.TakeBulletDamage(_bullet);
        // play on hit sound audio clip
        audioSource.PlayOneShot(onHitSound);
        // move back a bit base on bullet direction
        transform.position -= _bullet.transform.forward * 0.1f;
    }
}
