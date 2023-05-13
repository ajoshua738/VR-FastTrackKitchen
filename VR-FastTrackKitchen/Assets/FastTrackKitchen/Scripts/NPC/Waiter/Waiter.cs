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
        agent.isStopped = false;
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

        if (isSendingFood && agent.remainingDistance <= 1.5f)
        {
            agent.isStopped = true;
            animator.SetBool("Walk", false);

            timer += Time.deltaTime;
            if (timer > 2)
            {
                returnToIdle = true;
                isSendingFood = false;
                timer = 0;
            }
          
        }

        if (returnToIdle)
        {

            animator.SetBool("Walk", true);
            agent.isStopped = false;
            agent.destination = idlePoint.position;

            if (agent.remainingDistance <= 1.0f)
            {
                animator.SetBool("Walk", false);
                returnToIdle = false;
                timer = 0.0f;
                sendPoint = null;
                hasSendPoint = false;
                waiterPlate.SetActive(false);
                agent.isStopped = true;
            }



        }

       


    }

    
}
