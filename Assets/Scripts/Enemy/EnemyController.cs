using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float leftAndRightEdge = 8f;
    [SerializeField] private float changeDirInterval = 2f; // Time interval for changing direction
    [SerializeField] private bool startMoving = false;
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
        if (!startMoving)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }

        anim.SetFloat("Speed", Mathf.Abs(speed));

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

    public void StartMoving()
    {
        startMoving = !startMoving;
    }

    public void SetSpeedZero()
    {
        speed = 0f;
    }
}
