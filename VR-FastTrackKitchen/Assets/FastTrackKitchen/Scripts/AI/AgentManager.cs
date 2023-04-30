using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AgentManager : MonoBehaviour
{
    public GameObject AIToSpawn; // Assign the game object to spawn in the Inspector
    public float spawnCooldown = 1f; // Set the cooldown time in seconds
    public Transform parent;
    private float timeSinceLastSpawn = 0f;

    void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time - timeSinceLastSpawn > spawnCooldown)
        {
            // Spawn the object and reset the timeSinceLastSpawn variable
            Instantiate(AIToSpawn, transform.position, Quaternion.identity,parent);
            timeSinceLastSpawn = Time.time;
        }
    }
}