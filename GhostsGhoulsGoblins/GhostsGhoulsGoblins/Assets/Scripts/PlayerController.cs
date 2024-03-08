using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerActions;

    [SerializeField] float playerSpeed = 5;
    [SerializeField] int jumpForce = 5;
    [SerializeField] int divefwdForce = 6;
    [SerializeField] float diveUpForce = 5;


    //checks if player is on ground
    private bool isGrounded;
    private bool isDiving;
    private bool canDive;
    Rigidbody rb;




    private void Awake()
    {
        playerActions = new PlayerMovement();
        playerActions.Enable();
        canDive = true;

    }



    private void Update()
    {
        GroundCheck();
        PlayerMove();
        DiveCheck();
        TurnPlayer();

    }



    private void TurnPlayer()
    {



    }

    private void FixedUpdate()
    {
        //Vector3 moveVec = playerActions.PlayerMoves.PlayerControls.ReadValue<Vector2>();
        //GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0) * playerSpeed, ForceMode.Force);
    }
    


    /// <summary>
    /// Checks if the player is on the ground, falling or in the air.
    /// Respawns player if fall off map.
    /// </summary>
    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            //player is on ground/platform
            isGrounded = true;

            if ((Physics.Raycast(transform.position, Vector3.down, .5f)) && isDiving)
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
        //if player falls, respawns player
        if(transform.position.y < -2)
        {
            transform.position = new Vector3(0, 3, 0);
        }
    }



    /// <summary>
    /// Temporary code to get player moving and jumping
    /// </summary>
    private void PlayerMove()
    {
        //move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
        //move back
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * playerSpeed * Time.deltaTime;
        }
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
        //player jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }



    }



    /// <summary>
    /// Checks if player can dive
    /// </summary>
    private void DiveCheck()
    {
        //if player hits space and is on ground, then jump
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (!isGrounded)
            {
                //
            }
            if (isGrounded)
            {
                FloorDive();
            }
        }
    }
    /// <summary>
    /// Dive while standing
    /// </summary>
    private void FloorDive()
    {
            isDiving = true;
            canDive = false;
            transform.Rotate(90, 0, 0);
            GetComponent<Rigidbody>().AddForce(Vector3.forward * diveForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

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