using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // ������ B ����ײ�����������巢����ײʱ����
        if (collision.gameObject.CompareTag("TagA"))
        {
            // �����ײ�������Ƿ��� TagA ��ǩ
            gameObject.SetActive(false); // ʹ���� B ��ʧ
        }
    }

    // �����ʹ�õ��Ǵ�������Collider �� Is Trigger ����Ϊ true����ʹ�� OnTriggerEnter
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("TagA"))
    //    {
    //        // ��鴥���������Ƿ��� TagA ��ǩ
    //        gameObject.SetActive(false); // ʹ���� B ��ʧ
    //    }
    //}
}
