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

    //external scripts
    private EnemyShoot _enemyShoot;
    private PlayerController _playerController;

    //spawngold variables
    public GameObject goldCoin;
    public Vector3 coinSpawn;


    //enemy type (for parent/child script)
    public bool meleeEnemy;
    private float playerDistance;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _enemyShoot = FindObjectOfType<EnemyShoot>();
        EnemyType();
        coinSpawn = transform.position;
    }

    void Update()
    {
        EnemyMove();

    }

    /// <summary>
    /// sets the enemy type 
    /// </summary>
    public virtual void EnemyType()
    {
        meleeEnemy = false;
        if (meleeEnemy == true)
        {
            playerDistance = 2;
        }
        if (meleeEnemy == false)
        {
            playerDistance = 5;
        }
    }


    /// <summary>
    /// determines whether to follow path or chase player
    /// </summary>
    private void EnemyMove()
    {
        if (_enemyShoot.enemyAlerted == false)
        {
            MoveToWaypoint();
        }
        if (_enemyShoot.enemyAlerted == true)
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
    /// when player dives into enemy, the enemy drops a coin and dies
    /// function called in OnCollisionEnter
    /// </summary>
    private void EnemyDeath()
    {
        //Once dive is fix, uncomment code

        //if (_playerController.isDiving == true)
        //{
            Debug.Log("yay its working");
            //spawn coin
            Instantiate(goldCoin, coinSpawn, Quaternion.identity);
            //spawnGold.GetComponent<Rigidbody>().velocity = coinSpawn.forward * 1;
            //destroys enemy if diving
            Destroy(gameObject);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {




        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("HITPLAYERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
            EnemyDeath();
        }
    }
}