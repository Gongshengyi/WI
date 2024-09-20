using UnityEngine;

public class LeftHandActivator : MonoBehaviour
{
    public GameObject objectB; // 物体B

    void Start()
    {
        // 确保物体B开始时是隐藏的
        objectB.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // 检查碰撞的是否为左手
        if (other.CompareTag("LeftHand"))
        {
            objectB.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 检查离开的是否为左手
        if (other.CompareTag("LeftHand"))
        {
            objectB.SetActive(false);
        }
    }
}
