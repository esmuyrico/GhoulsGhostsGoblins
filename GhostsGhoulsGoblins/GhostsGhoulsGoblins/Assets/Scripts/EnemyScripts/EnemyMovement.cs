using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
    private float playerDistance = 5;
    public Transform player;

    //spawngold variables
    public GameObject goldCoin;
    public Vector3 coinSpawn;

    //Shoot variables
    public GameObject projectileObject;
    public Transform projectileSpawn;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool canShoot;

    //alert variables
    public float visionRange;
    public float visionAngle;
    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public bool enemyAlerted;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        canShoot = true;
        coinSpawn = transform.position;
    }

    void Update()
    {
        EnemyMove();
        DetectPlayer();
        shootPlayer();
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
    /// <summary>
    /// if player is in range, shoots at player
    /// </summary>
    private void DetectPlayer()
    {
        Vector3 playerTarget = (playerLocation.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) <= visionAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerLocation.transform.position);
            if (distanceToTarget <= visionRange)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                {
                    enemyAlerted = true;
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

    /// <summary>
    /// chases player
    /// </summary>
    private void ChasePlayer()
    {
        if (Vector3.Distance(playerLocation.transform.position, transform.position) >= playerDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerLocation.transform.position, Time.deltaTime * alertSpeed);
        }
    }

    /// <summary>
    /// shoots at player if alerted
    /// </summary>
    private void shootPlayer()
    {
        if (enemyAlerted == true)
        {
            transform.LookAt(player);
            if (canShoot == true)
            {
                StartCoroutine(ShootProjectiles());
            }
        }
    }

    /// <summary>
    /// Shoots projectile
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootProjectiles()
    {
        if (canShoot == true)
        {
            canShoot = false;
            var projectile = Instantiate(projectileObject, projectileSpawn.position, projectileSpawn.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawn.forward * projectileSpeed;
        }
        StartCoroutine(ShootDelay());
        yield return null;
    }

    /// <summary>
    /// Delays shooting time
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootDelay()
    {
        float timeBetween = 4f;
        yield return new WaitForSeconds(timeBetween);
        canShoot = true;
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