using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class NPCMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    //Animation
    public Animator animator;
    public List<Transform> availableSeats;
    public List<Transform> occupiedSeats;
    private Transform seatPos;

    public string chairTag;

    

    public bool isSeated = false;
    public Transform platePos;
   
 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        availableSeats = NPCSpawner.instance.availableSeats;
        SetPosition();

      

    }

    // Update is called once per frame
    void Update()
    {
        if (isSeated)
        {
            transform.position = seatPos.position;
            transform.rotation = seatPos.rotation;
        }
    }

    public void SetPosition()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Sit", false);
        int index = Random.Range(0, availableSeats.Count);
        agent.SetDestination(availableSeats[index].position);
        seatPos = availableSeats[index];
        // Remove the selected seat from availableSeats list in NPCMovement
        availableSeats.RemoveAt(index);
        //Debug.Log("Chair name : "+seatPos.gameObject.name);

        // Remove the selected seat from availableSeats list in NPCSpawner
        //NPCSpawner.instance.availableSeats.Remove(availableSeats[index]);

    }



    private void OnTriggerEnter(Collider other)
    {
       

        if (other.gameObject.GetInstanceID() == seatPos.gameObject.GetInstanceID())
        {
            
            Debug.Log("IN TRIGGER ENTER");
            agent.isStopped = true;
            animator.SetBool("Walk", false);
            animator.SetBool("Sit", true);
            transform.position = seatPos.position;
            transform.rotation = seatPos.rotation;
            isSeated = true;
            // Call the onOrder event

            for (int i = 0; i < seatPos.childCount; i++)
            {
                Transform child = seatPos.GetChild(i);
                if (child.CompareTag("PlatePosition"))
                {
                    platePos = child;
                    break;
                }
            }

           

            StartCoroutine(Order());


        }

      
    }

    IEnumerator Order()
    {
        yield return new WaitForSeconds(3); // Wait for 3 seconds
        OrderManager.instance.GenerateOrder(platePos);
        
       

    }

}
