using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKeys : MonoBehaviour
{
    [SerializeField]
    private GameObject parentDoor;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentDoor.GetComponent<Door>().IncreaseKeyCount();
            Destroy(gameObject);
        }
    }
}
