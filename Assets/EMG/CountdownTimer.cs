using System.Collections;
using UnityEngine;
using UnityEngine.UI; // ������ʾ����ʱ�ı�

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 5f; // ����ʱʱ�䣨�룩
    public Text countdownText; // UI Text ���������ʾ����ʱ
    public GameObject objectToActivate; // ����ʱ�����󼤻������
    public GameObject endtext;
    //public GameObject objectB; // ����B
    public GameObject Anim1; // Anim1
    public GameObject Anim2;

    private float currentTime;
    private bool timerActive = true;

    void Start()
    {
        currentTime = countdownTime;
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // ��ʼʱ��������
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        if (endtext != null)
        {
            endtext.SetActive(false); // ��ʼʱ����endtext
        }


        if (Anim1 != null)
        {
            Anim1.SetActive(true); // ȷ������B��ʼʱ�Ǽ����
        }

        if (Anim2 != null)
        {
            Anim2.SetActive(true); // ȷ������B��ʼʱ�Ǽ����
        }
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime; // ��ȥʱ��
            UpdateCountdownText();

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerActive = false;
                OnTimerEnd();
            }
        }
    }

    void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // ��ʾʣ��ʱ��
        }
    }

    void OnTimerEnd()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // ��������
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        StartCoroutine(HandleEndOfTimer());
    }

    IEnumerator HandleEndOfTimer()
    {
        if (endtext != null)
        {
            endtext.SetActive(true);
        }

        // �ȴ� 3 ��
        yield return new WaitForSeconds(3f);

        if (endtext != null)
        {
            endtext.SetActive(false);
        }

        //if (objectB != null)
        //{
            //objectB.SetActive(false); // ��������B
        //}

        if (Anim1 != null)
        {
            Anim1.SetActive(false); // ȷ������B��ʼʱ�Ǽ����
        }

        if (Anim2 != null)
        {
            Anim2.SetActive(false); // ȷ������B��ʼʱ�Ǽ����
        }
    }
}
