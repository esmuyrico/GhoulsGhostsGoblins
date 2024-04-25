using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    upDown,
    forwardsBackwards,
    LeftRight
}

public enum MoveBehavior
{
    pingPong,
    resetAfterMove
}

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private Direction direction;
    [SerializeField] private MoveBehavior moveBehavior;
    [SerializeField] private bool requiresActivation;
    [SerializeField] private int speed = 5;
    private bool activated;

    //           true  false
    private bool leftOrRight;
    private bool upOrDown;
    private bool forwardsOrBack;

    // startPoint should always have the lower x for leftRight, y for upDown, and z for forwardsBackwards
    [SerializeField] private Transform startPoint;
    // startPoint should always have the higher x for leftRight, y for upDown, and z for forwardsBackwards
    [SerializeField] private Transform endPoint;

    public Vector3 startPos;
    public Vector3 endPos;
    [SerializeField] private Transform platformRef;

    private GameObject target = null;
    private Vector3 offset;


    private void Awake()
    {
        startPos = startPoint.position;
        endPos = endPoint.position;
        if (requiresActivation)
            activated = false;
        else
            activated = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint.position;
        target = null;
    }

    private void Move()
    {
        // going left
        if (leftOrRight)
        {
            // reached the start go right
            if (transform.position.x < startPoint.position.x)
            {
                leftOrRight = !leftOrRight;
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
                leftOrRight = !leftOrRight;
            }
            else
            {
                // go right
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        target = other.gameObject;
        offset = target.transform.position - transform.position;

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
