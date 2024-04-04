using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnToLook : MonoBehaviour
{

    [SerializeField] private Transform pointToLookAt;
    private Transform rotatePoint;
    public Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        rotatePoint = transform.Find("RotatePoint");
    }

    // Update is called once per frame
    void Update()
    {
        // looks at the x and y position of the object to look at, and looks at the current height of the player
        Vector3 posToLookAt = new Vector3(pointToLookAt.position.x, transform.position.y, pointToLookAt.position.z);

        // looks at the position to look at
        rotatePoint.LookAt(posToLookAt);
        temp = Quaternion.Euler(0, rotatePoint.rotation.y, 0) * rotatePoint.forward;


        gameObject.GetComponent<PlayerController>().diveDirection = temp;
    }
}
