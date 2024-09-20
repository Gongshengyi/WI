using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CanvasDisplayController : MonoBehaviour
{
    public CanvasGroup canvasGroup; // cite Canvas Group

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XR Origin")) // Check trigger collision with XR Origin
        {
            canvasGroup.alpha = 1; // show dialog UI
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("XR Origin"))
        {
            canvasGroup.alpha = 0; // hide dialog UI
        }
    }
}
