using System.Collections;
using UnityEngine;
using UnityEngine.UI; // 用于显示倒计时文本

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 5f; // 倒计时时间（秒）
    public Text countdownText; // UI Text 组件用于显示倒计时
    public GameObject objectToActivate; // 倒计时结束后激活的物体
    public GameObject endtext;
    //public GameObject objectB; // 物体B
    public GameObject Anim1; // Anim1
    public GameObject Anim2;

    private float currentTime;
    private bool timerActive = true;

    void Start()
    {
        currentTime = countdownTime;
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // 初始时隐藏物体
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        if (endtext != null)
        {
            endtext.SetActive(false); // 初始时隐藏endtext
        }


        if (Anim1 != null)
        {
            Anim1.SetActive(true); // 确保物体B初始时是激活的
        }

        if (Anim2 != null)
        {
            Anim2.SetActive(true); // 确保物体B初始时是激活的
        }
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime; // 减去时间
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
            countdownText.text = Mathf.Ceil(currentTime).ToString(); // 显示剩余时间
        }
    }

    void OnTimerEnd()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // 激活物体
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

        // 等待 3 秒
        yield return new WaitForSeconds(3f);

        if (endtext != null)
        {
            endtext.SetActive(false);
        }

        //if (objectB != null)
        //{
            //objectB.SetActive(false); // 隐藏物体B
        //}

        if (Anim1 != null)
        {
            Anim1.SetActive(false); // 确保物体B初始时是激活的
        }

        if (Anim2 != null)
        {
            Anim2.SetActive(false); // 确保物体B初始时是激活的
        }
    }
}
