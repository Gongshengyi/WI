using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // �����ײ��� Tag �Ƿ�Ϊ "cloud"
        if (other.CompareTag("cloud"))
        {
            Destroy(other.gameObject); // ������ײ�����ڵ�����
        }
    }

}
