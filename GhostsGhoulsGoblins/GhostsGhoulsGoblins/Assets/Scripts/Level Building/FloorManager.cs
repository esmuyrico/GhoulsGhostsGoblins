using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] private BuildingFloorBehavior[] floors = new BuildingFloorBehavior[3];

    public static bool wantToPause = false;
    // adds a floor to the list of floors
    public void addFloor(BuildingFloorBehavior floor, int floorNumber)
    {
        if (floors[floorNumber] == null)
        {
            // if the index is not already filled, add it to the list at that index
            floors[floorNumber] = floor;
        }
    }

    public void EnteredFloor(int floorNumber)
    {
        if (floorNumber == 0)
        {
            // entered the first floor
            // deactivate the second and third floor
            floors[1].SetFloorActive(false);
            floors[2].SetFloorActive(false);
        }
        else
        {
            // entered the second floor
            // activate the second floor
            floors[1].SetFloorActive(true);
            floors[2].SetFloorActive(false);
        }
    }

    // Activates all floors 
    private void LeftBuilding()
    {
        floors[0].SetFloorActive(true);
        floors[1].SetFloorActive(true);
        floors[2].SetFloorActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // player left the building
            Debug.Log("left building");
            if (wantToPause)
            {
                UnityEditor.EditorApplication.isPaused = true;
            }
            LeftBuilding();
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("want to pause"))
        {
           // wantToPause = !wantToPause;
        }

    }
}
