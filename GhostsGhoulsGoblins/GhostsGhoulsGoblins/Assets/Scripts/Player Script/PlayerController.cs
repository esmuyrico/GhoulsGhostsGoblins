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
    private bool m_HitDetect;
    private float xDir;
    private bool isGrounded;
    [SerializeField] bool isDiving;
    Rigidbody rb;
    RaycastHit touchFloor;
    Collider m_Collider;
    private float yRotate = 0f;
    public float m_MaxDistance;

    private void Awake()
    {
        playerActions = new PlayerMove();
        playerActions.Enable();

    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");


        m_MaxDistance = 1f;
        m_Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        xDir = transform.forward.x;
        GroundCheck();
        MoveDirection();
        //PlayerWalk();
        PlayerMovement();
        m_Collider = GetComponent<Collider>();

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
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale * 0.5f, -transform.up, out touchFloor, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
            Debug.Log("Hit : " + touchFloor.collider.name);

            if (isDiving)
            {
                transform.Rotate(-90, 0, 0);
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
    void OnDrawGizmos()
    {
        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * touchFloor.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * touchFloor.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
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



    private void OnMoveForward()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.forward * playerSpeed * Time.deltaTime;
        }
    }
    private void OnMoveLeft()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        }
    }
    private void OnMoveBack()
    {
        if (isDiving == false)
        {
            transform.position += Vector3.back * playerSpeed * Time.deltaTime;

        }
    }
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

        //if the obstacle you triggered has a tag "Enemy":
        if (other.transform.tag == "Enemy")
        {
            if (isDiving == true)
            {
                //destroys wall if diving
                Destroy(other.gameObject);
            }
        }

    }




}