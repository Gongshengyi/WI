using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class CustomGrabbable : Grabbable
{
    private bool isBeingHeld = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void ProcessPointerEvent(PointerEvent evt)
    {
        base.ProcessPointerEvent(evt);

        // Check if the event type is Select or Unselect
        switch (evt.Type)
        {
            case PointerEventType.Select:
                isBeingHeld = true;
                break;
            case PointerEventType.Unselect:
                isBeingHeld = false;
                break;
        }
    }

    private void Update()
    {
        if (isBeingHeld)
        {
            LimitMovementToYAxis();
        }
    }

    private void LimitMovementToYAxis()
    {
        if (GrabPoints.Count > 0)
        {
            // Assuming the grab point we use is always the first one
            Pose grabPointPose = GrabPoints[0];
            Vector3 grabPointPosition = grabPointPose.position;

            // Set the new position of the object, keeping X and Z positions the same, only Y changes
            transform.position = new Vector3(transform.position.x, grabPointPosition.y, transform.position.z);
        }
    }
}
