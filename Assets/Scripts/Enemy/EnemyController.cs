using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool startMoving = false;
    private Animator anim;
    private Transform player; // Reference to the player's transform

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
    }

    void Update()
    {
        if (!startMoving)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Move the enemy toward the player
        transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);

        // Rotate the enemy to face the player (optional)
        transform.LookAt(player);

        anim.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void StartMoving()
    {
        startMoving = !startMoving;
    }

    public void SetSpeedZero()
    {
        speed = 0f;
    }
}