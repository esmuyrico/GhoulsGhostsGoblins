using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private int keysNeeded = 1;
    private int currentKeys = 0;

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
        if (currentKeys == keysNeeded)
        {
            StartCoroutine(OpenDoor());
            Debug.Log("Door Opened");
        }
        else
        {
            Debug.Log("Player needs " + (keysNeeded - currentKeys));
        }
    }

    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
