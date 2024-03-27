using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // the total number of keys needed for the door to open
    [SerializeField]
    private int keysNeeded = 1;
    private int currentKeys = 0;

    [SerializeField]
    private int switchesNeeded = 1;
    private int currentSwitches = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseKeyCount()
    {
        currentKeys++;
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentKeys == keysNeeded)
        {
            Destroy(gameObject);
        }
    }
}
