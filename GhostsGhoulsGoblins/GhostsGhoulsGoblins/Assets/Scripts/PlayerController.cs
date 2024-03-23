using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

/// <summary>
/// issues: rotation of player clashes with dive rotation; seems to be only having prob when facing x, y, or z directly
/// need player rotation to only rotate the y axis.
///         landing return to 0 degrees rotation is having some issues
/// </summary>
public class PlayerController : MonoBehaviour
{
    private PlayerMove playerActions;
    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField] float playerSpeed = 5;
    [SerializeField] int jumpForce = 5;
    [SerializeField] int divefwdForce = 6;
    [SerializeField] float diveUpForce = 5;
    [SerializeField] float sensitivityValue = 40f;

    private float xDir;
    private bool isGrounded;
    [SerializeField] bool isDiving;
    Rigidbody rb;

    private float yRotate = 0f;


    private void Awake()
    {
        playerActions = new PlayerMove();
        playerActions.Enable();

    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
    }

    private void Update()
    {
        xDir = transform.forward.x;
        GroundCheck();
        MoveDirection();
        //PlayerWalk();
    }

    void PlayerWalk()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.x) * playerSpeed *Time.deltaTime;
    }

    private void FixedUpdate()
    {


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
            if(isDiving)
            {
                transform.Rotate(-90, 0, 0);
                isDiving= false;
            }
            transform.position = new Vector3(0, 3, 0);
        }
    }



    /// <summary>
    /// Temporary code allow player to adjust direction with mouse
    /// </summary>
    private void MoveDirection()
    {
        if (!isDiving)
        {
            yRotate += Input.GetAxis("Mouse X") * sensitivityValue;
            transform.localEulerAngles = new Vector3(xDir, yRotate, 0);
        }
    }
    /// <summary>
    /// temporary move code unitl input system used
    /// </summary>
    private void PlayerMovement()
    {

        if (!isDiving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += transform.forward * playerSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * playerSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.forward * playerSpeed * Time.deltaTime;
            }
                if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * playerSpeed * Time.deltaTime;
            }
        }

    }



    private void OnMoveForward()
    {
        if (isGrounded == true)
        {
            transform.position += transform.forward * playerSpeed * Time.deltaTime;
            Debug.Log("front");

        }
    }
    private void OnMoveLeft()
    {
        if (isGrounded == true)
        {
            transform.position -= transform.right * playerSpeed * Time.deltaTime;
            Debug.Log("left");

        }
    }
    private void OnMoveBack()
    {
        if (isGrounded == true)
        {
            transform.position -= transform.forward * playerSpeed * Time.deltaTime;
            Debug.Log("BAck");
        }
    }
    private void OnMoveRight()
    {
        if (isGrounded == true)
        {
            transform.position += transform.right * playerSpeed * Time.deltaTime;
            Debug.Log("right");

        }
    }



    /// <summary>
    /// Jump mechanic
    /// </summary>
    private void OnJump()
    {
        if(isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);       
        }
    }

    /// <summary>
    /// Dive while standing
    /// </summary>
    private void OnDive()
    {
        if (!isGrounded)
        {
            //null
        }
        if (isGrounded && !isDiving)
        {
            isDiving = true;
            GetComponent<Rigidbody>().AddForce(Vector3.up * diveUpForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(transform.forward * divefwdForce, ForceMode.Impulse);
            transform.Rotate(90, 0, 0);
        }
    }

    /// <summary>
    /// destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //if the obstacle you triggered has a tag "Enemy":
        if (other.transform.tag == "BreakableWall")
        {
            if (isDiving == true)
            {
                //destroys wall if diving
                Destroy(other.gameObject);
            }
        }
    }




}