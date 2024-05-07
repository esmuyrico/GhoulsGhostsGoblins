using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public int health = 1;
    public GameObject swordPrefab;
    public bool canSwing = true;
    public float detectDistanceFromPlayer = 3;



    private void Update()
    {
        DetectPlayer();
    }



    private void DetectPlayer()
    {
        Vector3 playerTarget = (playerLoc.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) <= visionAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, playerLoc.transform.position);
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
        float distanceToPlayer = Vector3.Distance(transform.position, playerLoc.transform.position);

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
        // Debug.Log(canSwing);
        if (canSwing)
        {
            canSwing = false;
            GameObject sword = Instantiate(swordPrefab, transform);
            StartCoroutine(sword.GetComponent<MeleeAttack>().swing());
            yield return new WaitForSeconds(1);
            canSwing = true;
        }
    }

    /* // Alex said comment this out
     * private void OnGUI()
    {
        if (GUILayout.Button("swing"))
        {
            StartCoroutine(swing());
        }
    } */
}
