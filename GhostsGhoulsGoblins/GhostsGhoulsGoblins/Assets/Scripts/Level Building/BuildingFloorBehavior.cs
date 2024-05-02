using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFloorBehavior : MonoBehaviour
{
    [SerializeField] private int floorNumber = 0;
    [SerializeField] private FloorManager floorManager;

    public int FloorNumber
    {
        get { return floorNumber; }
    }

    private void Awake()
    {
        addFloorToList();
    }
    public void addFloorToList()
    {
        floorManager.addFloor(this, floorNumber);
    }

    public void SetFloorActive(bool tf)
    {
        transform.GetChild(0).gameObject.SetActive(tf);
        Debug.Log("Floor number: " + floorNumber + "set to " + tf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            floorManager.EnteredFloor(floorNumber);
    }
}
