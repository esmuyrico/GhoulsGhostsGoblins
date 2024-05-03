using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextLevel : MonoBehaviour
{
    private bool locked = true;
    [SerializeField] Transform levelTwoTeleportPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GhoulDialogue.Instance.choiceMade)
        {
            other.transform.position = levelTwoTeleportPoint.position;
        }
    }
}
