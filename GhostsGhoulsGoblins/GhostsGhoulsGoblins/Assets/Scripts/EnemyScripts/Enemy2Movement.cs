using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Movement : MonoBehaviour
{

    //Waypoint variables
    public GameObject[] waypoints;
    private int currentPath = 0;
    [SerializeField] float walkSpeed = 2;
    private float waypointDistance = 1;

    //Chase player variables
    public GameObject playerLocation;
    [SerializeField] float alertSpeed = 4;
    private PlayerController _playerController;
    public Transform player;

    //spawngold variables
    public GameObject goldCoin;
    private Vector3 coinSpawn;


    //enemy type (for parent/child script)
    private float playerDistance = 1.3f;


    // Alert System:
    public float visionRange;
    public float visionAngle;
    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public bool enemyAlerted;
    public int health = 1;
    public GameObject swordPrefab;
    public bool canSwing = true;
    public float detectDistanceFromPlayer = 3;










    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        coinSpawn = transform.position;
    }

    void Update()
    {
        EnemyMove();
        DetectPlayer();

    }




    /// <summary>
    /// determines whether to follow path or chase player
    /// </summary>
    private void EnemyMove()
    {
        if (enemyAlerted == false)
        {
            MoveToWaypoint();
        }
        if (enemyAlerted == true)
        {
            ChasePlayer();
        }
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enemy moves to waypoints in the order of the list they're on 
    /// </summary>
    private void MoveToWaypoint()
    {
        if (Vector3.Distance(waypoints[currentPath].transform.position, transform.position) < waypointDistance)
        {
            currentPath++;
            if (currentPath >= waypoints.Length)
            {
                currentPath = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPath].transform.position, Time.deltaTime * walkSpeed);
    }


    private void DetectPlayer()
    {
        Vector3 playerTarget = (playerLocation.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) <= visionAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerLocation.transform.position);
            if (distanceToTarget <= visionRange)
            {
                Debug.Log("In Range");
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                {
                    enemyAlerted = true;
                    meleePlayer();
                }
            }
            if (distanceToTarget >= visionRange)
            {
                enemyAlerted = false;
            }
        }
        else
        {
            enemyAlerted = false;
        }
    }

    private void meleePlayer()
    {
        // Before if statement > Find the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.transform.position);

        // If statement > Check if the player is close enough (unity func find dist between 2 points / float for dist
        if (distanceToPlayer < detectDistanceFromPlayer)
        {
            transform.LookAt(player);
            Debug.Log("Kill you");
            StartCoroutine(swing());
        }
    }

    private IEnumerator swing()
    {
        Debug.Log(canSwing);
        if (canSwing)
        {
            canSwing = false;
            GameObject sword = Instantiate(swordPrefab, transform);
            StartCoroutine(sword.GetComponent<MeleeAttack>().swing());
            yield return new WaitForSeconds(1);
            canSwing = true;
        }
    }




    /// <summary>
    /// chases player to close distance
    /// </summary>
    private void ChasePlayer()
    {
        if (Vector3.Distance(playerLocation.transform.position, transform.position) >= playerDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLocation.transform.position, Time.deltaTime * alertSpeed);
        }
    }



    /// <summary>
    /// when player dives into enemy, the enemy drops a coin and dies
    /// function called in OnCollisionEnter
    /// </summary>
    private void EnemyDeath()
    {
        if (_playerController.isDiving == true)
        {
            Instantiate(goldCoin, coinSpawn, Quaternion.identity);
            Destroy(gameObject);
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyDeath();
        }
    }
}
