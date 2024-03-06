using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5;
    private PlayerMovement playerActions;
    //checks if player is on ground
    public bool isGrounded;
    public bool isDiving;
    public bool canJump;
    public bool canDive;
    private int jumpForce = 5;
    private int diveForce = 6;
    Rigidbody rb;

    //private float xRotate = 0f;
    private float yRotate = 0f;
    public float sensitivityValue = 40f;


    private void Awake()
    {
        playerActions = new PlayerMovement();
        playerActions.Enable();
        canDive = true;
    }



    private void Update()
    {
        GroundCheck();
        PlayerJump();
        DiveCheck();
        TurnPlayer();

    }



    private void TurnPlayer()
    {

        //xRotate += Input.GetAxis("Mouse Y") * sensitivityValue;
        yRotate += Input.GetAxis("Mouse X") * sensitivityValue;
        transform.localEulerAngles = new Vector3(0, yRotate, 0);

    }

    private void FixedUpdate()
    {
        Vector3 moveVec = playerActions.PlayerMoves.PlayerControls.ReadValue<Vector2>();
        GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0) * playerSpeed, ForceMode.Force);
    }

    //temporary moves
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            //player is on ground/platform
            isGrounded = true;
            if (!canDive && isGrounded)
            {
                transform.Rotate(-90, 0, 0);
                canDive = true;
                isDiving = false;
            }
        }
        else
        {
            //player is not on ground/platform
            isGrounded = false;
        }
    }

    private void PlayerJump()
    {
        //if player hits space and is on ground, then jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            canJump = true;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //sets jump to false one player is in the air
            canJump = false;

        }

    }

    private void DiveCheck()
    {
        //if player hits space and is on ground, then jump
        if (Input.GetKeyDown(KeyCode.E) && canDive == true)
        {
            if (!isGrounded)
            {
                PlayerDive();
            }
            else
            {
                canDive = false;
            }
        }
    }
    private void PlayerDive()
    {
        if (canDive)
        {
            isDiving = true;
            GetComponent<Rigidbody>().AddForce(Vector3.forward * diveForce, ForceMode.Impulse);
            transform.Rotate(90, 0, 0);
            canDive = false;
        }
    }

    /// <summary>
    /// Needs work, but intended to destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //if the obstacle you triggered has a tag "Enemy":
        if (other.transform.tag == "WeakWall")
        {

            if (isDiving == true)
            {
                Destroy(this.gameObject);
            }
        }
    }




}