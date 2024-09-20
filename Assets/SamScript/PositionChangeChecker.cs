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
        // ��¼����A�ĳ�ʼλ��
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

            // λ�÷����仯����������A
            gameObject.SetActive(false);
        }
    }
}
