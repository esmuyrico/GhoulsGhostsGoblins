using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -6f);
    [SerializeField] private Vector3 cameraAngle = new Vector3(50, 0, 0);
    private float cameraSpeed = 50;

    private bool cameraMoving = false;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = new Vector3(0, 10, -6f);
        cameraAngle = new Vector3(50, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerTransform.position + offset;
        transform.rotation = Quaternion.Euler(cameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position - offset != playerTransform.position)
            transform.position = playerTransform.position + offset;
    }

    private IEnumerator SmoothMoveCamera()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = playerTransform.position + offset;
        cameraMoving = true;
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPos, endPos);

        float percentComplete = 0;
        while (percentComplete < 1)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * cameraSpeed;
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            percentComplete = fractionOfJourney;
            yield return null;
        }

        cameraMoving = false;
    }
}
