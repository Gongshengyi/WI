using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothInteraction : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Apply a force to the Rigidbody (simulate wind, for example)
        rb.AddForce(Vector3.forward * 10f);
    }
}
