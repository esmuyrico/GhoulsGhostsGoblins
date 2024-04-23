using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// 4/17/2024
// Manages the interactions between the keys, switches, and doors

public class DoorController : MonoBehaviour
{
    private List<GameObject> _keys = new List<GameObject>();
    [SerializeField] private List<GameObject> _Locks = new List<GameObject>();
    private GameObject _door;
    private int keysActivated = 0;
    [SerializeField] [Tooltip("DO NOT CHANGE")] private bool isKeyDoor;

    private void Start()
    {
        SetUpDisplayKeysOnDoor();
    }

    public void AddKeyToList(GameObject key)
    {
        _keys.Add(key);
       // Debug.Log("Key added. keys: " + _keys.Count);
    }

    public void SetDoor(GameObject door)
    {
        _door = door;
    }

    public void KeyActivated()
    {
        RemoveLock();
        keysActivated++;
        if (keysActivated >= _keys.Count)
        {
            if (isKeyDoor)
            {
                _door.GetComponent<Door>().UnlockDoor();
                Debug.Log("door unlocked");
            }
            else
            {
                StartCoroutine(_door.GetComponent<Door>().OpenDoorWithDelay());
                Debug.Log("door opening soon");
            }
        }
        // reflect number of keys on the door
    }

    private void SetUpDisplayKeysOnDoor()
    {
        Vector3 doorPos = _door.transform.position;
    }
    
    public void addLockToList(GameObject lockToAdd)
    {
        _Locks.Add(lockToAdd);
    }

    private void RemoveLock()
    {
        if (_Locks.Count != 0)
        {
            _Locks[keysActivated].SetActive(false);
        }
    }
}
