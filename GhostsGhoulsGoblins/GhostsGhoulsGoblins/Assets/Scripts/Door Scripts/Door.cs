using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Brough, Heath
    // 4/17/2024
    // Controls the opening of the Door

    private bool unlocked = false;

    [SerializeField]
    private GameObject doorController;

    private void Awake()
    {
        doorController.GetComponent<DoorController>().SetDoor(gameObject);
    }

    public IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(1f);
        GetRidOFDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && unlocked)
        {
            GetRidOFDoor();
        }
    }

    

    public void UnlockDoor()
    {
        unlocked = true;
    }

    // opens the door and reveals the next part
    private void GetRidOFDoor()
    {
        Destroy(gameObject);
    }
}
