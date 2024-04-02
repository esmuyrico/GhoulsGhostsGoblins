using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAlert : MonoBehaviour
{
    public GameObject playerLoc;
    public float visionRange;
    public float visionAngle;
    public LayerMask Player;
    public LayerMask obstacleMask;
    public bool enemyAlerted;
    public Transform player;
    private void Update()
    {
        DetectPlayer();
        shootPlayer();
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
    private void shootPlayer()
    {
        if (enemyAlerted)
        {
            transform.LookAt(player);
        }
    }
}