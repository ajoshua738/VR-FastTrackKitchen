using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Waiter : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator animator;
    public Transform idlePoint;
    public Transform sendPoint;

    public UnityEvent onSendOrder;
    public bool hasSendPoint;
    public bool isSendingFood;
    public bool returnToIdle;
    float timer =0.0f;
    public GameObject waiterPlate;

    // Start is called before the first frame update
    void Start()
    {
        isSendingFood = false;
        hasSendPoint = false;
        returnToIdle = false;
        waiterPlate.SetActive(false);
        
    }

    public void MoveToCustomer()
    {
        //sendPoint.position = OrderManager.instance.newOrderSOList[0].platePosition.position;
        animator.SetBool("Carry", true);
        animator.SetBool("Walk", true);
        agent.destination = sendPoint.position;
        isSendingFood = true;
        waiterPlate.SetActive(true);

    }



    // Update is called once per frame
    void Update()
    {
        if (!hasSendPoint)
        {
            if(OrderManager.instance.newOrderSOList.Count > 0)
            {
                sendPoint = OrderManager.instance.newOrderSOList[0].platePosition;
                hasSendPoint = true;
            }
        }

        if (isSendingFood && agent.remainingDistance <=agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Carry", false);
            returnToIdle = true;
            isSendingFood = false;
        }

        if (returnToIdle)
        {
            timer += Time.deltaTime;
            if(timer > 2)
            {
                agent.isStopped = false;
                agent.destination = idlePoint.position;
               
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    returnToIdle = false;
                    timer = 0.0f;
                    sendPoint = null;
                    hasSendPoint = false;
                    waiterPlate.SetActive(false);
                }
            }

           
           
           
            
        }

       


    }

    
}
