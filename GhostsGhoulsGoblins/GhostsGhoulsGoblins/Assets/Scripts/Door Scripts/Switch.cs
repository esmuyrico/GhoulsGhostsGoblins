using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 4/17/2024
// controls what happens when switches are activated

public class Switch : MonoBehaviour
{
    public Material activatedMaterial;
    private bool activated = false;

    [SerializeField]
    private GameObject doorController;

    private void Awake()
    {
        doorController.GetComponent<DoorController>().AddKeyToList(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !activated)
        {
            activated = true;
            transform.GetChild(0).GetComponent<Renderer>().material = activatedMaterial;
            doorController.GetComponent<DoorController>().KeyActivated();
        }
    }
}
