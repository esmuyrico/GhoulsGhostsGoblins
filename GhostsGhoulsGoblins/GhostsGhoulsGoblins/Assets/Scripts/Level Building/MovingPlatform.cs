using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    upDown,
    forwardsBackwards,
    LeftRight
}

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Direction direction;
    [SerializeField] private bool requiresActivation;
    [SerializeField] private int speed = 5;
    [SerializeField] private bool activated = false;

    // true if going right, forward, or up
    // false if going left, back, or down
    private bool progressing;

    // startPoint should always have the lower x for leftRight, y for upDown, and z for forwardsBackwards
    [SerializeField] private Transform startPoint;
    // startPoint should always have the higher x for leftRight, y for upDown, and z for forwardsBackwards
    [SerializeField] private Transform endPoint;

    [SerializeField] private bool platformInBuilding = false;

    public Vector3 startPos;
    public Vector3 endPos;
    [SerializeField] private Transform platformRef;

    private GameObject target = null;
    private Vector3 offset;


    private void Awake()
    {
        startPos = startPoint.position;
        endPos = endPoint.position;
        activated = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint.position;
        target = null;
    }

    public void Activate()
    {
        activated = !activated;
    }

    private void Move()
    {
        if (activated)
        {
            switch (direction)
            {
                case Direction.upDown:
                    MoveDownUp();
                    break;
                case Direction.forwardsBackwards:
                    MoveBackForward();
                    break;
                case Direction.LeftRight:
                    MoveLeftRight();
                    break;
                default:
                    break;
            }
        }
    }

    private void MoveLeftRight()
    {
        // going left
        if (progressing)
        {
            // reached the start go right
            if (transform.position.x < startPoint.position.x)
            {
                progressing = !progressing;
            }
            else
            {
                // go left
                transform.Translate(Vector3.left * speed * Time.deltaTime);

            }
        }
        // going right
        else
        {
            // reached the end, go left
            if (transform.position.x > endPoint.position.x)
            {
                progressing = !progressing;
            }
            else
            {
                // go right
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    private void MoveDownUp()
    {
        // going left
        if (progressing)
        {
            // reached the start go right
            if (transform.position.y < startPoint.position.y)
            {
                progressing = !progressing;
            }
            else
            {
                // go left
                transform.Translate(Vector3.down * speed * Time.deltaTime);

            }
        }
        // going right
        else
        {
            // reached the end, go left
            if (transform.position.y > endPoint.position.y)
            {
                progressing = !progressing;
            }
            else
            {
                // go right
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
        }
    }

    private void MoveBackForward()
    {
        // going left
        if (progressing)
        {
            // reached the start go forward
            if (transform.position.z < startPoint.position.z)
            {
                progressing = !progressing;
            }
            else
            {
                // go left
                transform.Translate(Vector3.back * speed * Time.deltaTime);

            }
        }
        // going right
        else
        {
            // reached the end, go back
            if (transform.position.z > endPoint.position.z)
            {
                progressing = !progressing;
            }
            else
            {
                // go right
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (transform.position.y > other.transform.position.y && transform.parent.transform.Find("CrushPoint") != null)
            {
                other.transform.position = transform.parent.transform.Find("CrushPoint").position;
            }
            target = other.gameObject;
            offset = target.transform.position - transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        target = null;
    }

    void LateUpdate()
    {
        Move();
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}
