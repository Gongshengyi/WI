using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 当物体 B 的碰撞体与其他物体发生碰撞时触发
        if (collision.gameObject.CompareTag("TagA"))
        {
            // 检查碰撞的物体是否有 TagA 标签
            gameObject.SetActive(false); // 使物体 B 消失
        }
    }

    // 如果你使用的是触发器（Collider 的 Is Trigger 设置为 true），使用 OnTriggerEnter
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("TagA"))
    //    {
    //        // 检查触发的物体是否有 TagA 标签
    //        gameObject.SetActive(false); // 使物体 B 消失
    //    }
    //}
}
