using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCLeave : MonoBehaviour
{


    public NavMeshAgent agent;
    public Transform leavePos;
    public Animator animator;
    // public bool leaveCalled;
    public GameObject d;

    void Start()
    {
        leavePos= GameObject.FindGameObjectWithTag("LeavePosition").transform;
        Debug.Log(leavePos.position);
        //leaveCalled = false;
        Leave();
      
       
      
    }

    public void Leave()
    {
        agent.isStopped = false;
        animator.SetBool("Walk", true);
        animator.SetBool("Sit", false);
        agent.SetDestination(leavePos.position);
        Transform seatPos = NPCMovement.instance.occupiedSeats[0];
        NPCMovement.instance.availableSeats.Add(seatPos);
        NPCMovement.instance.occupiedSeats.RemoveAt(0);
        //Plate.instance.DestroyObject();
        
        GameObject plateObject = OrderManager.instance.platePositionList[0];
        d = plateObject;

        DestroyPlate destroyPlateScript = plateObject.GetComponent<DestroyPlate>();

        if (destroyPlateScript != null)
        {
            destroyPlateScript.DeleteCollectedObjects();
        }
        OrderManager.instance.platePositionList.RemoveAt(0);





    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.remainingDistance);
        //if (leavePos != null && !leaveCalled)
        //{
        //    Leave();

        //}

        if (agent.remainingDistance <= 1.0f)
        {
            NPCSpawner.instance.spawnedNPCs.RemoveAt(0);
            Destroy(gameObject);
        }


        //agent.destination = leavePos.position;
    }

  


}
