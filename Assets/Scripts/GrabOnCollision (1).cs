using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabOnCollision : MonoBehaviour
{
    public Transform handTransform;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            other.transform.SetParent(handTransform);
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            other.transform.SetParent(null);
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
