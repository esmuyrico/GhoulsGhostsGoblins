using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitEdge : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit");
            // stores the last point that the player was when they leave the ground
            Vector3 lastGroundPoint = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            Debug.Log(lastGroundPoint);
            other.GetComponent<Checkpoints>().StoreLastGroundPoint(lastGroundPoint);
        }
    }
}
