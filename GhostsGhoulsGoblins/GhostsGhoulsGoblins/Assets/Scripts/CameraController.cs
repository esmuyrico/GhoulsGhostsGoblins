using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    //follow player
    public GameObject player;
    private Vector3 offset;

    //camera turn
    private float yRotate = 0f;
    private float xRotate = 0f;
    public float sensitivityValue = 40f;


    // Start is called before the first frame update
    void Start()
    {
        //camera starts following player
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //CameraControl();

    }

    private void CameraControl()
    {
        //xRotate += Input.GetAxis("Mouse Y") * sensitivityValue;
        yRotate += Input.GetAxis("Mouse X") * sensitivityValue;
        transform.localEulerAngles = new Vector3(0, yRotate, 0);
    }




    // Update checks to make sure player moved in fram before following player
    void LateUpdate()
    {
        //keeps following player
        transform.position = player.transform.position + offset;
    }
}
