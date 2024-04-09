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

    [SerializeField] float playerSpeed = 3.8f;
    [SerializeField] int jumpForce = 5;
    [SerializeField] int divefwdForce = 6;
    [SerializeField] float diveUpForce = 5;
    [SerializeField] float sensitivityValue = 40f;
    [SerializeField] float PleaseWorkFloat;


    public Vector3 diveDirection { get; set; }


    private float xDir;
    public bool isGrounded;
    public bool isDiving;
    Rigidbody rb;

    private float yRotate = 0f;

    private Vector3 startPos;


    //variables for checking if walking on ground
    private bool feetOnGround;
    RaycastHit feetFloor;
    Collider feetCollider;
    private float floorToFeet = .5f;

    //variables for dive landing
    public bool faceOnGround;
    RaycastHit faceFloor;
    Collider faceCollider;
    public float floorToFace;

    //variables for dive targeting
    private bool enemyInRange;
    RaycastHit diveTarget;
    Collider frontCollider;
    //public float goblinToEnemy;
    private void Awake()
    {
        playerActions = new PlayerMove();
        playerActions.Enable();

    }

    private void Start()
    {
        startPos = transform.position;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        transform.position = (startPos);

        feetCollider = GetComponent<Collider>();
        faceCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        xDir = transform.forward.x;
        GroundCheck();
        BackupGroundCheck();
        MoveDirection();
        //PlayerWalk();
        PlayerMovement();
        feetCollider = GetComponent<Collider>();
        faceCollider = GetComponent<Collider>();
        //OnDrawGizmos();

    }

    void PlayerWalk()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.x) * playerSpeed * Time.deltaTime;
    }

    private void BackupGroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
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
            //Debug.Log("Hit : " + feetFloor.collider.name);
            if (isDiving)
            {
                //transform.Rotate(-90, 0, 0);
                isDiving = false;
            }
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }





        //if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        //{
        //player is on ground/platform
        //isGrounded = true;
        //if ((Physics.Raycast(transform.position, Vector3.down, .5f)) && isDiving)
        //{ 
        //transform.Rotate(-90, 0, 0);
        //isDiving = false;
        //}
        //}
        //else
        //{
        //player is not on ground/platform
        //isGrounded = false;
        //}
        //if player falls, respawns player
        if (transform.position.y < -2)
        {
            if (isDiving)
            {
                transform.Rotate(-90, 0, 0);
                isDiving = false;
            }
            transform.position = new Vector3(-1.3f, 20, 1.1f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (faceOnGround)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * faceFloor.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * faceFloor.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * floorToFace);
            Gizmos.DrawRay(transform.position, transform.forward * floorToFace);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * floorToFace, transform.localScale);
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
            isDiving = true;
            GetComponent<Rigidbody>().AddForce(Vector3.up * diveUpForce, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce(diveDirection * divefwdForce, ForceMode.Impulse);
            //transform.Rotate(90, 0, 0);
            StartCoroutine(GroundCheckDelay());

        }
    }
    /// <summary>
    /// if player hits face on ground, player will be considered grounded and will return to stand/walking
    /// </summary>
    private void FinishDive()
    {
        faceOnGround = Physics.BoxCast(faceCollider.bounds.center, transform.localScale * PleaseWorkFloat, transform.forward, out faceFloor, transform.rotation, floorToFace);

        if (isDiving && faceOnGround)
        {
            Debug.Log("Face Hit : " + faceFloor.collider.name);
            //transform.Rotate(-90, 0, 0);
            isGrounded = true;
            isDiving = false;
        }
    }
    /// <summary>
    /// waits to check if player is on ground after diving
    /// </summary>
    /// <returns></returns>
    IEnumerator GroundCheckDelay()
    {
        yield return new WaitForSeconds(.9f);
        FinishDive();
    }



    /// <summary>
    /// destroy wall when player dives into it
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
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


        //if the obstacle you triggered has a tag "Enemy":
        if (other.transform.gameObject)
        {
            if (isDiving == true)
            {
                //destroys wall if diving
                //transform.Rotate(-90, 0, 0);
                Debug.Log("Hit Something");
                isGrounded = true;
                isDiving = false;
            }
        }
    }
}