using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesManager : MonoBehaviour
{
    // Main Variables of all enemies
    // Rank indicates how strong an enemy is and the other variables values change according to rank.
    [SerializeField] internal int rank;

    // HP , Damage & Speed
    internal int health, minHealth, maxHealth, damage, defaultSpeed = 5, speed = 5;
    internal int defaultMaxHealth = 50, defaultDamage = 5;

    public Vector3 initialPosition;
    public Quaternion initialRotation;
    // public int enemyHealth { get { return health; } }

    // Bool variables for states
    public bool isDying, isAttacking, isMoving;

    // Player variables
    [Tooltip("Introduce here player GameObject with Player stats Script")]
    [SerializeField] internal PlayerController playerStats;
    [SerializeField] internal GameObject playerGO;

    // AI variables
    internal NavMeshAgent navMesh;

    // Moving and Position variables
    internal Vector3 actualPosition, previousPosition;
    internal Animator enemyAnimator;
    internal Rigidbody rb;


    // Detection variables
    [SerializeField] internal bool playerDetected;

    // Time control variables
    internal float coolDown, timer;

    internal virtual void Start()
    {
        playerStats = FindObjectOfType<PlayerController>();

        //Set main Stats according to enemy Rank
        minHealth = 0;
        maxHealth = defaultMaxHealth * rank;
        health = maxHealth;
        damage = defaultDamage * rank;

        // Assign NavMeshAgent component to variable
        navMesh = GetComponent<NavMeshAgent>();

        // Assign Rigidbody
        rb = GetComponent<Rigidbody>();
        // Initial Position
        initialPosition = rb.transform.position;
        initialRotation = rb.transform.rotation;

        // Set player detection bool variable to false
        playerDetected = false;
    }

    internal void EnemyHealthControl()
    {
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
        else if(health <= minHealth)
        {
            isAttacking = false;
            isMoving = false;
            isDying = true;
            health = minHealth;
        }
    }


}
