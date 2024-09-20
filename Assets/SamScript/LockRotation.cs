using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion _initialRotation;

    void Start()
    {
        // 记录物体的初始旋转
        _initialRotation = transform.rotation;
    }

    void Update()
    {
        // 锁定物体的旋转
        transform.rotation = _initialRotation;
    }
}
