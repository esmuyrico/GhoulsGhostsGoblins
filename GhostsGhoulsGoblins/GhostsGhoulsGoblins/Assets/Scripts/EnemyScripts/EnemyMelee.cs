using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    // Alert System:
    public GameObject playerLoc;
    public float visionRange;
    public float visionAngle;
    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public bool enemyAlerted;
    public Transform player;

    // Melee System:
    //public int attack;

    private void Start()
    {

    }

    private void Update()
    {
        DetectPlayer();
        meleePlayer();
    }

    private void DetectPlayer()
    {
        Vector3 playerTarget = (playerLoc.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) <= visionAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerLoc.transform.position);
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

    private void meleePlayer()
    {
        if (enemyAlerted == true)
        {
            transform.LookAt(player);
            Debug.Log("Kill you");
            // Melee function goes here <<<<<
            //attack/player
        }
    }
}
