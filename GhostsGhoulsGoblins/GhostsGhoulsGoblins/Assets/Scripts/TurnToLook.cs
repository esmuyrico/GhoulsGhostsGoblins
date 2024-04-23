using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToLook : MonoBehaviour
{

    [SerializeField] private Transform pointToLookAt;
    public Vector3 temp;

    // Update is called once per frame
    void Update()
    {
        // looks at the x and y position of the object to look at, and looks at the current height of the player
        Vector3 posToLookAt = new Vector3(pointToLookAt.position.x, transform.position.y, pointToLookAt.position.z);

        if (!gameObject.GetComponent<PlayerController>().isDiving)
        {
            transform.LookAt(posToLookAt);
            transform.Find("RotatePoint").localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.Find("RotatePoint").localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        }
        
        temp = Quaternion.Euler(0, transform.rotation.y, 0) * transform.forward;

        gameObject.GetComponent<PlayerController>().diveDirection = temp;
    } 
}
