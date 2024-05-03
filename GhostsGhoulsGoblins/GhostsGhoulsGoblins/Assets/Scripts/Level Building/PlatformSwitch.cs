using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour
{
    [SerializeField] private Transform movingPlatform;
    private bool activated = false;

    private void Start()
    {
        // deactivate the platform in start
        movingPlatform.Find("MovingPlatformCollider").gameObject.GetComponent<MovingPlatform>().Activate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load<Material>("PlatformSwitchActivated");
            movingPlatform.Find("MovingPlatformCollider").gameObject.GetComponent<MovingPlatform>().Activate();
        }
    }
}
