using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour
{
    public Transform teleportDestination;  // 传送目的地
    public float delay = 1f;  // 传送延迟时间（秒）

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 检查进入触发区域的是否是玩家
        {
            StartCoroutine(TeleportAfterDelay(other.gameObject));
        }
    }

    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        yield return new WaitForSeconds(delay);  // 等待指定的时间
        player.transform.position = teleportDestination.position;  // 传送玩家
    }
}
