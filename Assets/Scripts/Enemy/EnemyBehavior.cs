using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float speed;

    //Patrol Values
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public int timeBetweenPatrols;

    //Attack Values
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animation controls
    private Animator animator;
    public bool isAttacking;
    public bool isHit;
    public int attackPattern;

    //Game controls
    public int maxHealth, currentHealth, lightDamage, heavyDamage;
    public bool staggered;

    private void Awake()
    {
        player = GameObject.Find("Camera Offset").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Check ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInSightRange = Mathf.Abs((player.position - transform.position).magnitude) < sightRange;
        
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //playerInSightRange = Mathf.Abs((player.position - transform.position).magnitude) < attackRange;

        //float distanceFromPlayer = Mathf.Abs((player.position - transform.position).magnitude);

        if(currentHealth > 0 && staggered == false)
        {
            if (!playerInSightRange && !playerInAttackRange) Patrol();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange) AttackPlayer();
        }
        else if(currentHealth > 0 && staggered == true)
        {
            agent.destination = transform.position;
        }
        else
        {
            kill();
        }
        

        //isHit = true;

        //Update Anims
        if (!(playerInSightRange && playerInAttackRange))
            attackPattern = 0;
        animator.SetInteger("Attack", attackPattern);
        speed = agent.velocity.magnitude;
        animator.SetFloat("Velocity", agent.velocity.magnitude);
        if (isHit == true)
        {
            animator.SetBool("isHit", true);
            Invoke(nameof(resetHit), 1);
        }
        else
        {
            animator.SetBool("isHit", false);
        }
        if (currentHealth <= 0)
        {
            kill();
        }
        

    }

    void resetHit()
    {
        isHit = false;
    }

    void resetStaggered()
    {
        staggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "playerAttack")
        {
            isHit = true;
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                kill();
            }
            staggered = true;
            Invoke(nameof(resetStaggered), 1);
        }
    }

    private void Patrol()
    {


        agent.speed = 1;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Destination reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            Invoke(nameof(ResetWalkpoint), timeBetweenPatrols);
        }


    }

    private void ResetWalkpoint()
    {
        walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = 2;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Stop enemy from moving
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            transform.LookAt(player);
            attackPattern = 1;
            animator.SetInteger("Attack", attackPattern);
            Invoke(nameof(ResetStance), float.Parse(".5"));
            //Attack code here

            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetStance()
    {
        attackPattern = 0;
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        attackPattern = 0;
    }

    private void kill()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("isDead", true);
    }
}
