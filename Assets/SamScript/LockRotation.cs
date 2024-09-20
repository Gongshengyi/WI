using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion _initialRotation;

    void Start()
    {
        // ��¼����ĳ�ʼ��ת
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        // �����������ת
        transform.rotation = _initialRotation;
    }
}
