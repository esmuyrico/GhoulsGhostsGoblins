using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Last Edited 4/22/2024
// holds a function to store a point when the player hits the object

public class PitEdge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit");
            // stores the last point that the player was when they leave the ground
            Vector3 lastGroundPoint = transform.position;
            //Debug.Log(lastGroundPoint);
            other.GetComponent<Checkpoints>().StoreLastGroundPoint(lastGroundPoint);
        }
    }
}
