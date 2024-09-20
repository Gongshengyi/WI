using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Move Speed
    public float moveInterval = 2.0f; // movement interval
    public float moveRange = 1.0f; // Range of movement
    public Animator animator; // Invoke the Animator component

    private Vector3 targetPosition; // target location
    private bool isMoving = false; // move or not

    void Start()
    {
        // Ensure the Animator component is correctly assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Initialise to Idle animation
        animator.SetBool("isWalking", false);

        // Starting a random move 
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Randomly generated target locations
            float randomX = Random.Range(-moveRange, moveRange);
            float randomY = Random.Range(-moveRange, moveRange);
            targetPosition = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

            // Setting the animation to walk
            animator.SetBool("isWalking", true);

            // Start moving
            isMoving = true;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Stop moving, switch back to Idle
            isMoving = false;
            animator.SetBool("isWalking", false);

            // Wait a random period of time before moving
            yield return new WaitForSeconds(moveInterval);
        }
    }
}
