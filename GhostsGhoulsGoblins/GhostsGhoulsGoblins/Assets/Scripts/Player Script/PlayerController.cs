using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;


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

    public Vector3 diveDirection { get; set; }

    // Respawning Variables
    public int lives = 3;
    public int fallDepth;
    private Vector3 startPos;
    public GameObject canvas;
    public bool isInvincible;

    private float xDir;
    public bool isGrounded;
    public bool isDiving;
    Rigidbody rb;

    private float yRotate = 0f;

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
        startPos = transform.position;
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
        transform.position = (startPos);

        feetCollider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(gameObject);
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

    // This will bring the player to the start position and take lives away
    private void Respawn()
    {
        // When the player is respawned, this will start the Blink Coroutine.
        StartCoroutine(Blink());
        lives--;
        transform.position = startPos;
        // If the player's lives are less than or equal to 0, then disable control.
        if (lives <= 0)
        {
            this.enabled = false;
        }
    }

    // This will cause the player to blink (Invincibility Frame)
    private IEnumerator Blink()
    {
        isInvincible = true;
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        // Once the For loop above is finished, this will turn it back on.
        GetComponent<MeshRenderer>().enabled = true;
        isInvincible = false;
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
                //isDiving = false;
            }
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
                //transform.Rotate(-90, 0, 0);
                isDiving = false;
            }
            transform.position = new Vector3(-1.3f, 20, 1.1f);
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

        //if the obstacle you triggered has a tag "Enemy":
        if (other.transform.gameObject)
        {
            if (isDiving == true)
            {
                //destroys wall if diving
                //transform.Rotate(-90, 0, 0);
                isGrounded = true;
                isDiving = false;
                Respawn();
            }
        }

        if (other.transform.tag == "Enemy")
        {
            Debug.Log("hitenemy");
            //Destroy(other.transform.gameObject);
        }

    }

}