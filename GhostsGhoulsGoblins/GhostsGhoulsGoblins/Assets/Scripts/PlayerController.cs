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
    private void Awake()
    {
        playerActions = new PlayerMovement();
        playerActions.Enable();
    }

    //temporary moves
    private void PlayerMoves()
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
        //if player hits space and is on ground, then jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //jump is true if player hits space and is on ground
            canJump = true;
        }
    }

    


    private void FixedUpdate()
    {
        Vector3 moveVec = playerActions.PlayerMoves.PlayerControls.ReadValue<Vector2>();
        GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0) * playerSpeed, ForceMode.Force);
    }

}
