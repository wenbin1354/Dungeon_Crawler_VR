using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float leftAndRightEdge = 8f;
    [SerializeField]
    private float changeDirInterval = 2f; // Time interval for changing direction
    private float timeSinceLastDirChange;
    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        // Initialize the timer
        timeSinceLastDirChange = 0f;
    }

    void Update()
    {
        anim.SetFloat("Speed", speed);
        
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing direction based on time interval
        timeSinceLastDirChange += Time.deltaTime;
        if (timeSinceLastDirChange >= changeDirInterval)
        {
            ChangeDirection();
            timeSinceLastDirChange = 0f; // Reset the timer
        }

        // Check left and right edges
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    void ChangeDirection()
    {
        // Randomly change direction
        speed *= -1;
    }
}
