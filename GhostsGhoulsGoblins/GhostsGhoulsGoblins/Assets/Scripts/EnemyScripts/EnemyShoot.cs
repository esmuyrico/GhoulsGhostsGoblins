using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    //Shoot variables
    public GameObject projectileObject;
    public Transform projectileSpawn;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool canShoot;

    //alert variables
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
        canShoot = true;
    }
    private void Update()
    {
        DetectPlayer();
        shootPlayer();
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
}