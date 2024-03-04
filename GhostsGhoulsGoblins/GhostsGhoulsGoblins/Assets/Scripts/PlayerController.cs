using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5;
    private PlayerMovement playerActions;
    //checks if player is on ground
    private bool isGrounded;
    private bool canJump;
    private bool canDive;
    private int jumpForce = 5;
    Rigidbody rb;
    private void Awake()
    {
        playerActions = new PlayerMovement();
        playerActions.Enable();
       
    }

    //temporary moves
    private void groundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            //player is on ground/platform
            isGrounded = true;
        }
        else
        {
            //player is not on ground/platform
            isGrounded = false;
        }
    }

    private void Update()
    {
        groundCheck();
        playerJump();
        playerDive();
    }


    private void FixedUpdate()
    {
        Vector3 moveVec = playerActions.PlayerMoves.PlayerControls.ReadValue<Vector2>();
        GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0) * playerSpeed, ForceMode.Force);
    }


    private void playerJump()
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

    private void playerDive()
    {
        //if player hits space and is on ground, then jump
        if (Input.GetKeyDown(KeyCode.E))
        {
            canDive = true;

            GetComponent<Rigidbody>().AddForce(Vector3.forward * jumpForce, ForceMode.Impulse);
            //sets jump to false one player is in the air
            canDive = false;
            groundCheck();
            if (isGrounded)
            {
                canDive = true;
            }
        }

    }

}
