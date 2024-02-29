using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 5;
    private PlayerMovement playerActions;

    private void Awake()
    {
        playerActions = new PlayerMovement();
        playerActions.Enable();
    }

    private void FixedUpdate()
    {
        Vector3 moveVec = playerActions.PlayerMoves.PlayerControls.ReadValue<Vector2>();
        GetComponent<Rigidbody>().AddForce(new Vector3(moveVec.x, 0) * playerSpeed, ForceMode.Force);
    }

}
