using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // the total number of keys needed for the door to open
    [SerializeField]
    private int keysNeeded = 1;
    [SerializeField] private float currentKeys = 0;

    [SerializeField]
    private int switchesNeeded = 1;
    [SerializeField] private int currentSwitches = 0;

    [SerializeField] private GameObject ActivateOnOpen = null;

    private void Start()
    {
        ActivateOnOpen.SetActive(false);
    }

    public void IncreaseKeyCount()
    {
        currentKeys+=0.5f;
        Debug.Log("Player needs " + (keysNeeded - currentKeys) + " more keys");
    }

    public void IncreaseSwitchCount()
    {
        currentSwitches++;
        Debug.Log("Player needs " + (switchesNeeded - currentSwitches) + " more keys");
        if (currentSwitches == switchesNeeded)
        {
            StartCoroutine(OpenDoorWithDelay());
        }
    }

    private IEnumerator OpenDoorWithDelay()
    {
        yield return new WaitForSeconds(1f);
        GetRidOFDoor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentKeys >= keysNeeded)
        {
            GetRidOFDoor();
        }
    }

    // opens the door and reveals the next part
    private void GetRidOFDoor()
    {
        if (ActivateOnOpen != null)
        {
            ActivateOnOpen.SetActive(true);
        }
        Destroy(gameObject);
    }
}
