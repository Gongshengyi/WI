using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCloud : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞体的 Tag 是否为 "cloud"
        if (other.CompareTag("cloud"))
        {
            Destroy(other.gameObject); // 销毁碰撞体所在的物体
        }
    }

}
