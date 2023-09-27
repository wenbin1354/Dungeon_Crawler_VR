using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    protected override void Die()
    {
        anim.SetBool("isDead", true);
        Destroy(gameObject, 2f);
    }
}
