using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectileObject;
    private EnemyAlert _enemyAlert;
    public Transform projectileSpawn;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool canShoot;
    // Start is called before the first frame update
    void Update()
    {
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (_enemyAlert.enemyAlerted == true)
        {
            StartCoroutine(ShootProjectiles());
        }
    }

    IEnumerator  ShootProjectiles()
    {
        if (canShoot)
        {
            canShoot = false;
            var projectile = Instantiate(projectileObject, projectileSpawn.position, projectileSpawn.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileSpawn.up * projectileSpeed;
        }
        StartCoroutine(MoveDelay());
        yield return null;
    }

    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
