using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThroughWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wall.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
