using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Created 11/13/23
// last modified 11/14/23
// moves an object around the game based on where the mouse is on the screen

public class PointToLookAt : MonoBehaviour
{
    public Vector3 worldPosition;
    public Vector3 screenPostion;

    public Vector3 playerPos;

    // Plane used for finding the position on screen to look at
    Plane plane = new Plane(Vector3.down, 1.5f);

    // Update is called once per frame
    void Update()
    {
        screenPostion = Input.mousePosition;
        worldPosition = new Vector3(0, 0, 0);

        Ray ray = Camera.main.ScreenPointToRay(screenPostion);

        // checks if the raycast hits the plane and returns the position it hits
        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        // find the delta from the launchpos to the mousPos3D
        Vector3 mouseDelta = worldPosition - new Vector3(playerPos.x, 0, playerPos.z);
/*
        // Limit mouseDelta to radious of slingshot sphere collider
        float maxMagnitude = 5;
        float minMagnitude = 4;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
*/
        mouseDelta = new Vector3(mouseDelta.x, 1.5f, mouseDelta.z);
        transform.position = mouseDelta;
/*
        if (Vector3.Distance(playerPos, transform.position) < minMagnitude)
        {
            // mouseDelta.Normalize();
            //  mouseDelta *= minMagnitude;
            Debug.Log("too close");
            Vector3 direction = transform.position - playerPos;
            transform.position = direction.normalized * minMagnitude;
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
        // Debug.Log(mouseDelta);
*/
    }
}
