using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;

    //spawngold
    public GameObject goldCoin;
    public Transform coinSpawn;


    void Start()
    {
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (goingLeft)
        {
            if (transform.position.x <= leftPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else
        {
            
            if (transform.position.x >= rightPos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }



    private void EnemyDeath()
    {
        Debug.Log("HitPlayer");
        //Once dive is fix, uncomment code

        //if (_playerController.isDiving == true)
        //{
        //Instantiate(goldCoin, coinSpawn, Quaternion.identity);

        //spawn coin
        //var WWay = Instantiate(WWayPrefab, RBottomSpawn.position, RBottomSpawn.rotation * Quaternion.Euler(0f, -90f, 0f));
        //WWay.GetComponent<Rigidbody>().velocity = RBottomSpawn.up;

        var spawnGold = Instantiate(goldCoin, coinSpawn.position, coinSpawn.rotation);
        spawnGold.GetComponent<Rigidbody>().velocity = coinSpawn.forward * 1;
        //destroys enemy if diving
        Destroy(gameObject);

        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            EnemyDeath();
        }
    }

}
