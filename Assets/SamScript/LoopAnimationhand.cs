using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopAnimationhand : MonoBehaviour
{
    private Animator animator;
    private float waitTime = 1f;
    private float timer = 0f;
    private bool isWaiting = false;
    private bool playAnimation1 = true; // 控制播放哪个动画

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
            // 判断当前动画状态
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            // 如果当前播放动画1
            if (playAnimation1 && currentState.IsName("grab") && currentState.normalizedTime >= 1.0f)
            {
                // 动画1播放完毕，开始等待
                isWaiting = true;
                timer = 0f;
            }
            //如果当前播放动画2
            else if (!playAnimation1 && currentState.IsName("Arm bend down") && currentState.normalizedTime >= 1.0f)
            {
                // 动画2播放完毕，开始等待
                isWaiting = true;
                timer = 0f;
            }
        }
        else
        {
            // 等待计时
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                isWaiting = false;

                // 切换动画
                if (playAnimation1)
                {
                    animator.Play("Arm bend down");
                }
                else
                {
                    animator.Play("Arm bending gesture");
                }

                // 切换播放标志
                playAnimation1 = !playAnimation1;
            }
        }
    }
}
