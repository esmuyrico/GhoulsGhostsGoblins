using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{


    //YT VIDE WAYPOINNTS TUTORIAL
    public GameObject[] waypoints;
    private int current = 0;
    [SerializeField] float walkSpeed = 1;
    private float waypointDistance = 1;









    public PlayerController _playerController;

    //spawngold variables
    public GameObject goldCoin;
    public Transform coinSpawn;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();

    }

    void Update()
    {
        //PathDirection();
        //MoveToWaypoint();
    }

    /// <summary>
    /// Enemy moves to waypoints in the order of the list they're on 
    /// </summary>
    private void MoveToWaypoint()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointDistance)
        {
            current++;
            if(current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * walkSpeed);
    }


    /// <summary>
    /// when player dives into enemy, the enemy drops a coin and dies
    /// function called in OnCollisionEnter
    /// </summary>
    private void EnemyDeath()
    {
        Debug.Log("HitPlayer");
        //Once dive is fix, uncomment code

        if (_playerController.isDiving == true)
        {
            //Instantiate(goldCoin, coinSpawn, Quaternion.identity);

            //spawn coin
            //var WWay = Instantiate(WWayPrefab, RBottomSpawn.position, RBottomSpawn.rotation * Quaternion.Euler(0f, -90f, 0f));
            //WWay.GetComponent<Rigidbody>().velocity = RBottomSpawn.up;

            var spawnGold = Instantiate(goldCoin, coinSpawn.position, coinSpawn.rotation);
            spawnGold.GetComponent<Rigidbody>().velocity = coinSpawn.forward * 1;
            //destroys enemy if diving
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
