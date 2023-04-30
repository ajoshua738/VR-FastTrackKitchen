using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AIControl : MonoBehaviour
{
    private ChairManager currentChair = null;
    private List<ChairManager> availableChairs = new List<ChairManager>();
    public NavMeshAgent agent;
    private ChairManager[] chairs;

    public float cooldownTime = 5f;
    private bool isWaiting = true;

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        // Find all chairs in the scene
        chairs = FindObjectsOfType<ChairManager>();

        // Add all unoccupied chairs to the list of available chairs
        foreach (ChairManager chair in chairs)
        {
            if (!chair.IsOccupied)
            {
                availableChairs.Add(chair);
                isWaiting = false;
            }
        }
    }

    private void Update()
    {
        if (isWaiting)
        {
            foreach (ChairManager chair in chairs)
            {
                if (!chair.IsOccupied)
                {
                    availableChairs.Add(chair);
                    isWaiting = false;
                }
            }
        }
        if (currentChair == null)
        {
            foreach (ChairManager chair in availableChairs)
            {
                if (!chair.IsOccupied)
                {
                    currentChair = chair;
                    currentChair.IsOccupied = true;
                    availableChairs.Remove(chair);
                    Debug.Log(gameObject.name + " has claimed a chair.");
                    gameObject.GetComponent<AIControl>().agent.SetDestination(chair.transform.position);
                    StartCoroutine(Cooldown());
                    break;
                }
            }
        }
    }

    void OnDestroy()
    {
        // If the AI is occupying a chair, release it
        if (currentChair != null)
        {
            currentChair.IsOccupied = false;
            availableChairs.Add(currentChair);
            Debug.Log(gameObject.name + " has released the chair.");
        }
    }
}