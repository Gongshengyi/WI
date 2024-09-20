using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraTarget : MonoBehaviour
{
    public Transform target; // Reference to the target character's Transform
    public float smoothSpeed = 0.125f; // Adjust this value to control the smoothness of the camera follow

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position;

            // Use Mathf.SmoothDamp to smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Make the camera look at the target
            transform.LookAt(target);
        }
    }
}
