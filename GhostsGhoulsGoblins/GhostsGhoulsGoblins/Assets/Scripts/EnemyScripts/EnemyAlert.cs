using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAlert : MonoBehaviour
{
    public GameObject playerLoc;
    public float visionRange;
    public float visionAngle;
    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public bool enemyAlerted;
    public Transform player;

    //if player is x distance to enemy, alert

    //if player is y distance to enemy, passive
    private void Start()
    {

    }
    private void Update()
    {
        DetectPlayer();
        meleePlayer();
    }
    /// <summary>
    /// if player is in range, shoots at player
    /// </summary>
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


    /// <summary>
    /// shoots at player if alerted
    /// </summary>
    private void meleePlayer()
    {
        if (enemyAlerted == true)
        {
            transform.LookAt(player);
            Debug.Log("Kill you");
        }
    }



}