using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBehavior : MonoBehaviour
{
    [SerializeField] private GameObject DoorController;

    private void Awake()
    {
        DoorController.GetComponent<DoorController>().addLockToList(gameObject);
    }
}
