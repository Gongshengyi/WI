using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYController : MonoBehaviour
{
    public GameObject idleObject;  // 
    public GameObject walkObject;  //
    public GameObject attackObject; // 

    public float moveInterval = 2.0f; // movement interval
    public float moveRange = 1.0f; // Range of movement
    public float moveSpeed = 2.0f; // Speed

    private Animator idleAnimator; // idle Animator
    private Vector3 targetPosition; // target location
    private bool isMoving = false; // moving or not

    void Start()
    {
        // initialisation state, show Idle objects, hide other objects
        idleObject.SetActive(true);
        walkObject.SetActive(false);
        attackObject.SetActive(false); // 

        idleAnimator = idleObject.GetComponent<Animator>();

        // Make sure the Idle animation is playing
        PlayIdleAnimation();

        // Starting a randomly moving
        StartCoroutine(MoveRoutine());
    }

    void PlayIdleAnimation()
    {
        idleAnimator.Play("Idle");
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Make sure to play the full Idle animation
            float idleAnimationLength = idleAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(idleAnimationLength);

            // Randomly generated target locations
            float randomX = Random.Range(-moveRange, moveRange);
            float randomY = Random.Range(-moveRange, moveRange);
            targetPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

            // Switch to Walking
            isMoving = true;
            idleObject.SetActive(false);
            walkObject.SetActive(true);

            // Move to target position
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Stop moving, switch back to Idle
            isMoving = false;
            walkObject.SetActive(false);
            idleObject.SetActive(true);

            // Wait a while before moving
            yield return new WaitForSeconds(moveInterval);
        }
    }
}
