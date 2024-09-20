using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractionController : MonoBehaviour
{
    public float followRadius = 5; // default is distance of 5
    public float waitToFollow = 3; // default is wait 3 seconds
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private float waitTimer; // we will use this to count how long the agent has waited
    private bool hasWavedGoodbye;

    //add in the boolean for waving here


    // Start is called before the first frame update
    void Start()
    {
        // we have hard-coded this Player tag name here you can adapt it to be more flexible if you want (like in the LookAtController)
        player = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        waitTimer = 0;
        hasWavedGoodbye = true; // we only want to wave goodbye once the agent has followed us

        // we will set out hasWavedGoodbye boolen to true here because we want to wave goodbye only once the agent has followed us first

    }

    // Update is called once per frame
    void Update()
    {
        //we check the distance between our nav agent and our player
        float distance = Vector3.Distance(player.transform.position,transform.position);

        if (distance <= followRadius) 
        {
            Debug.Log("Close enough to follow");
            // Follow player logic here
            if (distance <= agent.stoppingDistance) 
            {
                animator.SetBool("Walking", false);
                waitTimer = 0;
                
            }
            else
            {
                waitTimer += Time.deltaTime;
                
                Debug.Log(waitTimer);

                if (waitTimer >= waitToFollow)
                {
                    hasWavedGoodbye = false; // once the agent has followed us we want them to wave goodbye if we escape


                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);



                    animator.SetBool("Walking", true);
                    

                    //move your agent SetDestination line of code & the animator SetBool functionality that sets Walking to true in here
                }
            }
    
        }       
                
         else
         {
            if (hasWavedGoodbye == false)
            {
                agent.isStopped = true;
                animator.SetBool("Walking", false);         
                DoWave();
         


            }
                        
         }       
           
    }

    private void DoWave()
    {
        waitTimer = 0;
        hasWavedGoodbye = true;
        animator.SetTrigger("Wave");
        Debug.Log("Set trigger");
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
}