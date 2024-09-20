using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour
{
    public Transform teleportDestination;  // ����Ŀ�ĵ�
    public float delay = 1f;  // �����ӳ�ʱ�䣨�룩

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // �����봥��������Ƿ������
        {
            StartCoroutine(TeleportAfterDelay(other.gameObject));
        }
    }

    private IEnumerator TeleportAfterDelay(GameObject player)
    {
        yield return new WaitForSeconds(delay);  // �ȴ�ָ����ʱ��
        player.transform.position = teleportDestination.position;  // �������
    }
}
