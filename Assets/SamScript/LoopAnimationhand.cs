using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimationhand : MonoBehaviour
{
    private Animator animator;
    private float waitTime = 1f;
    private float timer = 0f;
    private bool isWaiting = false;
    private bool playAnimation1 = true; // ���Ʋ����ĸ�����

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        if (!isWaiting)
        {
            // �жϵ�ǰ����״̬
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            // �����ǰ���Ŷ���1
            if (playAnimation1 && currentState.IsName("grab") && currentState.normalizedTime >= 1.0f)
            {
                // ����1������ϣ���ʼ�ȴ�
                isWaiting = true;
                timer = 0f;
            }
            //�����ǰ���Ŷ���2
            else if (!playAnimation1 && currentState.IsName("Arm bend down") && currentState.normalizedTime >= 1.0f)
            {
                // ����2������ϣ���ʼ�ȴ�
                isWaiting = true;
                timer = 0f;
            }
        }
        else
        {
            // �ȴ���ʱ
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                isWaiting = false;

                // �л�����
                if (playAnimation1)
                {
                    animator.Play("Arm bend down");
                }
                else
                {
                    animator.Play("Arm bending gesture");
                }

                // �л����ű�־
                playAnimation1 = !playAnimation1;
            }
        }
    }
}
