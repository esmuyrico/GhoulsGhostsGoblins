using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 4/17/2024
// controls what happens when keys are picked up

public class PickUpKeys : MonoBehaviour
{
    [SerializeField]
    private GameObject doorController;

    private void Awake()
    {
        doorController.GetComponent<DoorController>().AddKeyToList(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorController.GetComponent<DoorController>().KeyActivated();
            gameObject.SetActive(false);
        }
    }
}
