using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Vector3 currentCheckpoint;
    public Vector3 _lastGroundPoint;

    public int health;
    private int maxHealth = 60;

    bool alreadyFell = false;
    // Start is called before the first frame update
    void Start()
    {
        health = 60;
        currentCheckpoint = transform.position;
        UIManager.Instance.UpdateHealth(health, maxHealth);
        UIManager.Instance.UpdateGold(0);
    }

    public void StoreLastGroundPoint(Vector3 lastGroundPoint)
    {
        Debug.Log("storing last ground point");
        _lastGroundPoint = lastGroundPoint;
    }

    public void GoLastGroundPoint()
    {
        Debug.Log("going to last ground point");
        transform.position = _lastGroundPoint;
        alreadyFell = false;
    }

    public void GoToCheckPoint()
    {
        Debug.Log("going to checkpoint");
        transform.position = currentCheckpoint;
        alreadyFell = false;
    }

    public void ReachedCheckpoint(Vector3 checkPointPos)
    {
        Debug.Log("reached checkpoint");
        currentCheckpoint = checkPointPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            ReachedCheckpoint(other.transform.position);
        }
        if (other.CompareTag("GameEnd"))
        {
            Application.Quit();
        }
    }

    public void FellOff()
    {
        if (!alreadyFell)
        {
            alreadyFell = true;
            Debug.Log("fell");
            health -= 5;
            if (health == 0)
            {
                health = maxHealth;
                GoToCheckPoint();
            }
            else
            {
                GoLastGroundPoint();
            }
            UIManager.Instance.UpdateHealth(health, maxHealth);
        }
    }
}
