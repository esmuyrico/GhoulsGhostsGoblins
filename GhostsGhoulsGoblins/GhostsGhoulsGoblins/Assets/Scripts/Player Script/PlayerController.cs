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
    private float playerSpeed;
    private float airSpeed = 20;
    private float walkSpeed = 31;
    private float jumpWait = .3f;

    private int divefwdForce = 16;
    private float diveUpForce = 8;
    Rigidbody rb;
    private float airDrag = 2.9f;
    private float groundDrag = 2f;

    // Checks for if the player hadDived or hasJumped
    private bool hasDived;
    private bool hasJumped;
    private bool canInterruptDive;


    //ground/dive check variables
    private float xDir;
    public bool isGrounded;
    public bool isDiving;
    private bool canDive;
    private float DiveDelayTime = .1f;
    private float fallSpeed = 98f;

    public Vector3 diveDirection { get; set; }

    //JUMP VARIABLES
    private int jumpAmt;
    private int jumpOneForce = 10;
    private int jumpTwoForce = 7;

    //RAMP VARIABLES
    private float slopeAngle = 40;
    private RaycastHit slopeHit;
    private float rampSpeed = 10f;



    private bool OnSlope()
    {
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.15f + .2f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < slopeAngle && angle != 0;
            }
        return false;
    }

    private Vector3 SlopeLeft()
    {
        return Vector3.ProjectOnPlane(Vector3.left, slopeHit.normal).normalized;
    }
    private Vector3 SlopeRight()
    {
        return Vector3.ProjectOnPlane(Vector3.right, slopeHit.normal).normalized;
    }
    private Vector3 SlopeForward()
    {
        return Vector3.ProjectOnPlane(Vector3.forward, slopeHit.normal).normalized;
    }
    private Vector3 SlopeBack()
    {
        return Vector3.ProjectOnPlane(Vector3.back, slopeHit.normal).normalized;
    }

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
        rb = GetComponent<Rigidbody>();
        canDive = true;
    }


    private void FixedUpdate()
    {

        rb.useGravity = !OnSlope();
        xDir = transform.forward.x;
        GroundCheck();
        PlayerMovement();
        SpeedRegulator();

    }
    private void SpeedRegulator()
    {
        // if on ground
        if (isGrounded)
        {
            playerSpeed = walkSpeed;
            jumpAmt = 0;
            rb.drag = groundDrag;
        }
        //if in air
        if (!isGrounded)
        {
            rb.drag = airDrag;
            playerSpeed = airSpeed;
        }
    }





    private void GroundCheck()
    {

        if ((Physics.Raycast(transform.position + new Vector3(.2f, 0f, .2f), Vector3.down, 1.15f))
            || (Physics.Raycast(transform.position + new Vector3(-.2f, 0f, .2f), Vector3.down, 1.15f))
            || (Physics.Raycast(transform.position + new Vector3(-.2f, 0f, .2f), Vector3.down, 1.15f))
            || (Physics.Raycast(transform.position + new Vector3(.2f, 0f, -.2f), Vector3.down, 1.15f)))
            {
            isGrounded = true;

        }

        else
            isGrounded = false;


    }

    private void WallStick()
    {
        if (isGrounded)
        {
            GetComponent<Collider>().material.dynamicFriction = 0;
            GetComponent<Collider>().material.staticFriction = 0;
            // Access the physics material of the collider
            PhysicMaterial physicsMaterial = GetComponent<Collider>().material;
            // Set the friction to 0 (no friction)
            //physicsMaterial.friction = 0f;
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
            if (OnSlope())
            {

                rb.AddForce(SlopeForward() * (playerSpeed - rampSpeed), ForceMode.Force);

            }
            if (!OnSlope())
            {
                GetComponent<Rigidbody>().AddForce(Vector3.forward * playerSpeed, ForceMode.Force);

            }
        }
    }
    /// <summary>
    /// Moves player left
    /// </summary>
    private void OnMoveLeft()
    {
        if (OnSlope())
        {

          rb.AddForce(SlopeLeft() * (playerSpeed - rampSpeed), ForceMode.Force);

        }
        if (!OnSlope())
        {
        GetComponent<Rigidbody>().AddForce(Vector3.left * playerSpeed, ForceMode.Force);
        }
    }
    /// <summary>
    /// Moves player back
    /// </summary>
    private void OnMoveBack()
    {
        if (OnSlope())
        {
            rb.AddForce(SlopeBack() * (playerSpeed - rampSpeed), ForceMode.Force);
        }
        if (!OnSlope())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.back * playerSpeed, ForceMode.Force);
        }
    }
    /// <summary>
    /// Moves player right
    /// </summary>
    private void OnMoveRight()
    {
        if (OnSlope())
        {

            rb.AddForce(SlopeRight() * (playerSpeed - rampSpeed), ForceMode.Force);
        }
        if (!OnSlope())
        {
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
            if (canInterruptDive)
            {
                jumpAmt++;
                FinishDive();
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpTwoForce, ForceMode.Impulse);
            }
 
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
                canInterruptDive = false;
                StartCoroutine(JumpTwoDelay());
                UIManager.Instance.UpdateDiveCharge(0);
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
    /// prevents player from spamming the second jump with the first jump and dive
    /// to get a higher jump
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpTwoDelay()
    {
        yield return new WaitForSeconds(jumpWait);
        canInterruptDive = true;
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
        for (float i = 0; i < DiveDelayTime; i += DiveDelayTime/10)
        {
            UIManager.Instance.UpdateDiveCharge(i / DiveDelayTime);
            yield return new WaitForSeconds(DiveDelayTime/10);
        }
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
    /// if player hits enemy, player will wait less than a second to return to not diving
    /// </summary>
    IEnumerator HitEnemyDelay()
    {
        yield return new WaitForSeconds(.1f);
        hasDived = true;
        transform.Rotate(-90, 0, 0);
        isDiving = false;
    }

    /// <summary>
    /// destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //dive into anything (except keys, pitedges, coins and checkpoints) ends dive
        if (!(other.transform.tag == "PitEdge")
            && !(other.transform.tag == "Coin")
            && !(other.transform.tag == "Key")
            && !(other.transform.tag == "CheckPoint")
            && !(other.transform.tag == "Enemy"))
        {
            if (isDiving == true)
                FinishDive();
        }


        if (other.transform.tag == "Enemy")
        {
           //HitEnemyDelay();
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