using UnityEngine;

public class LeftHandActivator : MonoBehaviour
{
    public GameObject objectB; // ����B

    void Start()
    {
        // ȷ������B��ʼʱ�����ص�
        objectB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // �����ײ���Ƿ�Ϊ����
        if (other.CompareTag("LeftHand"))
        {
            objectB.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // ����뿪���Ƿ�Ϊ����
        if (other.CompareTag("LeftHand"))
        {
            objectB.SetActive(false);
        }
    }
}
