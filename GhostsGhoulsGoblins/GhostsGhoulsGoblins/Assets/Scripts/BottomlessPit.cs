using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Brough, Heath
// Last Edited 4/22/2024
// holds a function to damage the player when they fall

public class BottomlessPit : MonoBehaviour
{
    // player needs to store where it last was when it stops touching the ground 
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Player"))
        {
            // function to deal damage to player and respawn them at the edge of the pit
            other.GetComponent<Checkpoints>().FellOff();
            
        }*/
    }
}
