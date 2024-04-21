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
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce = 0.5f;
    [SerializeField] int divefwdForce = 6;
    [SerializeField] float diveUpForce = 5;
    [SerializeField] float sensitivityValue = 40f;
    Rigidbody rb;
    private float yRotate = 0f;


    // Respawning Variables
    public bool isInvincible;
    private Vector3 previousPos;

    // Checks for if the player hadDived or hasJumped
    public bool hasDived;
    public bool hasJumped;

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


    //JUMP VARIABLES
    public int jumpAmt;
    //private bool hasJumpedOnce;
    //private bool hasJumpedTwice;
    [SerializeField] int jumpOneForce = 3;
    [SerializeField] int jumpTwoForce = 2;


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
        SpeedRegulator();

    }

    private void SpeedRegulator()
    {


        // if on ground
        if (isGrounded)
        {
            playerSpeed = 3f;
            jumpAmt = 0;
        }
        //if in air
        if (!isGrounded)
        {




            playerSpeed = 2;
            switch (jumpAmt)
            {
                case 1:
                    playerSpeed = 2;
                    break;
                case 2:
                    playerSpeed = 1;
                    break;
                default: break;
            }
        }
    }




    /// <summary>
    /// Checks if the player is on the ground, falling or in the air.
    /// Respawns player if fall off map.
    /// </summary>
    private void GroundCheck()
    {
        // Code here for checking if standing on ground
        feetOnGround = Physics.BoxCast(feetCollider.bounds.center, transform.localScale * 0.5f, -transform.up, out feetFloor, transform.rotation, floorToFeet);
        if (feetOnGround)
        {
            isGrounded = true;
            // Set hasJumped and hasDived to false
        }
        else
        {
            isGrounded = false;
        }
        if (transform.position.y < -6)
        {
            transform.position = new Vector3(.2f, 19.81f, .3f);
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
            //transform.position += transform.forward * playerSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(transform.forward * playerSpeed, ForceMode.Force);

        }
    }
    /// <summary>
    /// Moves player left
    /// </summary>
    private void OnMoveLeft()
    {
        if (isDiving == false)
        {
            //transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(Vector3.left * playerSpeed, ForceMode.Force);
        }
    }
    /// <summary>
    /// Moves player back
    /// </summary>
    private void OnMoveBack()
    {
        if (isDiving == false)
        {
            //transform.position += Vector3.back * playerSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(Vector3.back * playerSpeed, ForceMode.Force);
        }
    }
    /// <summary>
    /// Moves player right
    /// </summary>
    private void OnMoveRight()
    {
        if (isDiving == false)
        {
            //transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            GetComponent<Rigidbody>().AddForce(Vector3.right * playerSpeed, ForceMode.Force);
        }
    }

    /// <summary>
    /// Jump mechanic
    /// </summary>
    private void OnJump()
    {
        //first jump
        if (isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpOneForce, ForceMode.Impulse);
            StartCoroutine(JumpCheckDelay());
        }
        if (isDiving && (jumpAmt == 1))
        {
            jumpAmt++;
            FinishDive();
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpTwoForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Dive feature
    /// </summary>
    private void OnDive()
    {
        if (!isDiving && !(jumpAmt ==2))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * diveUpForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(diveDirection * divefwdForce, ForceMode.Impulse);
            //delay a sec or 2
            StartCoroutine(DiveCheckDelay());
            //StartCoroutine(DiveCheckDelay());

        }
    }

    /// <summary>
    /// if player hits face on ground, player will be considered grounded and will return to stand/walking
    /// </summary>
    private void FinishDive()
    {
        hasDived = true;
        transform.Rotate(-90, 0, 0);
        isDiving = false;

    }

    /// <summary>
    /// waits to check if player is on ground after diving
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpTwoDelay()
    {
        yield return new WaitForSeconds(0.3f);
        jumpAmt = 1;
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
        jumpAmt = 1;

    }

    /// <summary>
    /// waits to check if player is on ground after diving
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpCheckDelay()
    {
        yield return new WaitForSeconds(0.1f);
        jumpAmt = 1;
    }

    /// <summary>
    /// destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isDiving == true)
            FinishDive();

        //dive into anything ends dive
        if (other.transform.gameObject)
        {
            if (isDiving == true)
                FinishDive();
        }

        //Breakable Walls
        if (other.transform.tag == "BreakableWall")
        {
            if (isDiving == true)
            {
                FinishDive();
                Destroy(other.transform.gameObject);
            }
        }
    }
}