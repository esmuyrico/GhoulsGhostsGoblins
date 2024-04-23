using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Last Edited 4/22/2024
// holds functions for storing checkpoints and points right before the edge of a pit

public class Checkpoints : Singleton<Checkpoints>
{
    public Vector3 currentCheckpoint;
    public Vector3 _lastGroundPoint;

    bool alreadyFell = false;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = transform.position;
        
    }

    public void StoreLastGroundPoint(Vector3 lastGroundPoint)
    {
        //Debug.Log("storing last ground point");
        _lastGroundPoint = lastGroundPoint;
    }

    public void GoLastGroundPoint()
    {
        //Debug.Log("going to last ground point");
        transform.position = _lastGroundPoint + Vector3.up;

        alreadyFell = false;
    }

    public void GoToCheckPoint()
    {
        //Debug.Log("going to checkpoint");
        transform.position = currentCheckpoint;
        alreadyFell = false;
    }

    public void ReachedCheckpoint(Vector3 checkPointPos)
    {
        //Debug.Log("reached checkpoint");
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
            GoLastGroundPoint();
        }
    }
}
