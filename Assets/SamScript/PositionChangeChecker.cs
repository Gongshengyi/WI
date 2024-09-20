using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PositionChangeChecker : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody rb;
    void Start()
    {
        // 记录物体A的初始位置
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (initialPosition != Vector3.zero)
        {
            return;
        }

        if (initialPosition != Vector3.zero && transform.position != initialPosition)
        {
            Debug.Log("Position changed");

            // 位置发生变化后，隐藏物体A
            gameObject.SetActive(false);
        }
    }
}
