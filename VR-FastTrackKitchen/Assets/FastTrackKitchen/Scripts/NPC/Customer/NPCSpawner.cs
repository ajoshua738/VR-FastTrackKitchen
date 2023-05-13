using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public List<Transform> availableSeats;
    public List<Transform> occupiedSeats;
    public List<GameObject> spawnedNPCs;
    public List<GameObject> NPCs;
    public List<Transform> spawnPoints;

    float timer = 0;
    public float spawnTimer = 10.0f;
    public float maxNPCs; //overall max number of npcs that should spawn in a level
    public float totalNPCs; // total npcs that can be in the scene
    public float npcCount = 0;

    public static NPCSpawner instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        availableSeats = new List<Transform>();   
        occupiedSeats = new List<Transform>();
        spawnedNPCs = new List<GameObject>();

        availableSeats = GameObject.FindGameObjectsWithTag("SeatPosition").Select(c => c.transform).ToList();
        


    }
   

    public void SpawnNPC()
    {
        
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject npc = NPCs[Random.Range(0, NPCs.Count)];

        GameObject npcInstance = Instantiate(npc,spawnPoint.position,Quaternion.identity);
        spawnedNPCs.Add(npcInstance);
        npcCount++;
      

       
    }

    

    // Update is called once per frame
    void Update()
    {
        
        if(spawnedNPCs.Count < totalNPCs && npcCount < maxNPCs)
        {
            timer += Time.deltaTime;
            if(timer > spawnTimer)
            {
                timer = 0;
                SpawnNPC();
            }
            
        }
    }
}
