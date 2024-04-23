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
    [SerializeField] float maxSpeed = 4;

    [SerializeField] float jumpForce = 2f;
    [SerializeField] int divefwdForce = 7;
    [SerializeField] float diveUpForce = 16;
    Rigidbody rb;
    [SerializeField] float airDrag = 2.9f;
    [SerializeField] float groundDrag = 3.4f;

    // Respawning Variables
    //public bool isInvincible;
    //private Vector3 previousPos;

    // Checks for if the player hadDived or hasJumped
    public bool hasDived;
    public bool hasJumped;

    //ground/dive check variables
    private float xDir;
    public bool isGrounded;
    public bool isDiving;
    private bool canDive;
    [SerializeField] float DiveDelayTime = 1.25f;

    public Vector3 diveDirection { get; set; }

    //variables for checking if walking on ground
    private bool feetOnGround;
    RaycastHit feetFloor;
    Collider feetCollider;
    private float floorToFeet = .5f;


    //JUMP VARIABLES
    public int jumpAmt;
    [SerializeField] int jumpOneForce = 4;
    [SerializeField] int jumpTwoForce = 3;


    private void Awake()
    {
        playerActions = new PlayerMove();
        playerActions.Enable();
    }

    private void Start()
    {
        //previousPos = transform.position;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        feetCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        canDive = true;
    }

    private void Update()
    {
        xDir = transform.forward.x;
        GroundCheck();
        BackupGroundCheck();
        PlayerMovement();
        feetCollider = GetComponent<Collider>();
        SpeedRegulator();

    }
    private void SpeedRegulator()
    {
        // if on ground
        if (isGrounded)
        {
            playerSpeed = 14;
            jumpAmt = 0;
            rb.drag = groundDrag;
        }
        //if in air
        if (!isGrounded)
        {
            rb.drag = airDrag;
            playerSpeed = maxSpeed / 2;
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
            GetComponent<Rigidbody>().AddForce(Vector3.forward * playerSpeed, ForceMode.Force);

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
        if (!isDiving && !(jumpAmt == 2))
        {
            if (canDive)
            {
                canDive = false;
                GetComponent<Rigidbody>().AddForce(Vector3.up * diveUpForce, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(diveDirection * divefwdForce, ForceMode.Impulse);
                //delay a sec or 2
                StartCoroutine(DiveCheckDelay());
                StartCoroutine(DiveAbilityDelay());
            }
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
    /// After being used, the dive will not be able to be used until 
    /// the time delay (currently 1.5 sec) is finished
    /// </summary>
    /// <returns></returns>
    IEnumerator DiveAbilityDelay()
    {
        yield return new WaitForSeconds(DiveDelayTime);
        canDive = true;
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
        //dive into anything (except keys, pitedges, coins and checkpoints) ends dive
        if (!(other.transform.tag == "PitEdge") && !(other.transform.tag == "Coin") && !(other.transform.tag == "Key") && !(other.transform.tag == "CheckPoint"))
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