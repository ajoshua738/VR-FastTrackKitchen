using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentManager : MonoBehaviour
{
    public GameObject[] seats;
    GameObject[] agents;
    // Use this for initialization
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < agents.Length; i++)
        {
            agents[i].GetComponent<AIControl>().agent.SetDestination(seats[i].transform.position);
        }
    }
}