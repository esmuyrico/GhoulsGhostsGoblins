using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{    
    //player movement variables
    private PlayerMove playerActions;
    PlayerInput playerInput;
    InputAction moveAction;
    [SerializeField] float playerSpeed = 3.8f;
    [SerializeField] int jumpForce = 5;
    [SerializeField] int divefwdForce = 6;
    [SerializeField] float diveUpForce = 5;
    [SerializeField] float sensitivityValue = 40f;
    Rigidbody rb;
    private float yRotate = 0f;


    // Respawning Variables
    public bool isInvincible;
    private Vector3 previousPos;

    //private Vector3 startPos;

    //ground/dive check variables
    private float xDir;
    public bool isGrounded;
    public bool isDiving;
    public Vector3 diveDirection { get; set; }

    //variables for checking if walking on ground
    private bool feetOnGround;
    RaycastHit feetFloor;
    Collider feetCollider;
    private float floorToFeet = .5f;


    private void Awake()
    {
        playerActions = new PlayerMove();
        playerActions.Enable();
    }

    private void Start()
    {
        previousPos = transform.position;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        feetCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xDir = transform.forward.x;
        GroundCheck();
        BackupGroundCheck();
        MoveDirection();
        PlayerMovement();
        feetCollider = GetComponent<Collider>();
        //  faceCollider = GetComponent<Collider>();
    }






    /// <summary>
    /// Checks if the player is on the ground, falling or in the air.
    /// Respawns player if fall off map.
    /// </summary>
    private void GroundCheck()
    {
        //Checks if standing on ground
        feetOnGround = Physics.BoxCast(feetCollider.bounds.center, transform.localScale * 0.5f, -transform.up, out feetFloor, transform.rotation, floorToFeet);
        if (feetOnGround)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //respawns if falls too far
        if (transform.position.y < -2)
        {
            if (isDiving)
            {
                isDiving = false;
            }
            transform.position = new Vector3(-1.3f, 20, 1.1f);
        }
    }
    private void BackupGroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
            isGrounded = true;
        else
            isGrounded = false;
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
    /// Player movement checking if player can move
    /// </summary>
    private void PlayerMovement()
    {
        if (!isDiving)
        {
            if (Input.GetKey(KeyCode.W))
            {
                OnMoveForward();
            }
            if (Input.GetKey(KeyCode.A))
            {
                OnMoveLeft();
            }
            if (Input.GetKey(KeyCode.S))
            {
                OnMoveBack();
            }
            if (Input.GetKey(KeyCode.D))
            {
                OnMoveRight();
            }
        }
    }


    /// <summary>
    /// Moves player forward
    /// </summary>
    private void OnMoveForward()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }
    }
    /// <summary>
    /// Moves player left
    /// </summary>
    private void OnMoveLeft()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
    }
    /// <summary>
    /// Moves player back
    /// </summary>
    private void OnMoveBack()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.back * playerSpeed * Time.deltaTime;

        }
    }
    /// <summary>
    /// Moves player right
    /// </summary>
    private void OnMoveRight()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Jump mechanic
    /// </summary>
    private void OnJump()
    {
        if (isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Dive feature
    /// </summary>
    private void OnDive()
    {
        if (!isGrounded)
        {
            //null
        }
        if (isGrounded && !isDiving)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * diveUpForce, ForceMode.Impulse);
            //code that works w/o point to click
            //GetComponent<Rigidbody>().AddForce(Vector3.forward * divefwdForce, ForceMode.Impulse);
            //code that works with point to click
            GetComponent<Rigidbody>().AddForce(diveDirection * divefwdForce, ForceMode.Impulse);
            //delay a sec or 2
            StartCoroutine(DiveCheckDelay());
            //if no contact: is diving = true


        }
    }

    private void OnEnemy()
    {
        //make sure player is upright
        //attach to enemy
        //set a bool or circumstance connecting to OnDive funnction saying that player can (smaller) dive off in oposite direction from enemy

    }

    /// <summary>
    /// if player hits face on ground, player will be considered grounded and will return to stand/walking
    /// </summary>
    private void FinishDive()
    {
        transform.Rotate(-90, 0, 0);

        isDiving = false;
        //isGrounded = true;

    }
    /// <summary>
    /// waits to check if player is on ground after diving
    /// </summary>
    /// <returns></returns>
    IEnumerator DiveCheckDelay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.Rotate(90, 0, 0);
        isDiving = true;

    }

    /// <summary>
    /// destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            if (isDiving == true)
            {
                //FinishDive();
                Debug.Log("First?");

            }
        }

        if (isDiving == true)
        {
            FinishDive();
            Debug.Log("last?");
        }

        if (other.transform.tag == "Untagged")
        {
            isGrounded = true;

            if (isDiving == true)
            {
                //destroys wall if diving
                //transform.Rotate(-90, 0, 0);
                isDiving = false;
            }
        }

        if (other.transform.gameObject)
        {
            if (isDiving == true)
            {
                isGrounded = true;
                isDiving = false;
            }
        }

        if (other.transform.tag == "Enemy")
        {
            Debug.Log("hitenemy");
        }

        if (other.transform.tag == "BreakableWall")
        {
            Destroy(other.transform.gameObject);
        }
    }

}