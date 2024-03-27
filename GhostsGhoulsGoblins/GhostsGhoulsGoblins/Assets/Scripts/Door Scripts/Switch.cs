using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField]
    private GameObject parentDoor;
    public Material activatedMaterial;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !activated)
        {
            parentDoor.GetComponent<Door>().IncreaseSwitchCount();
            activated = true;
            transform.GetChild(0).GetComponent<Renderer>().material = activatedMaterial;
        }
    }
}
