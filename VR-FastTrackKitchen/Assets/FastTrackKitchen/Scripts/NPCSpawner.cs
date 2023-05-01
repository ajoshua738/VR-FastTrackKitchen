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

    public float spawnTimer = 0;
    public float maxNPCs;
    public float totalNPCs;

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
       
        Instantiate(npc,spawnPoint.position,Quaternion.identity);
        spawnedNPCs.Add(npc);

        // Get a reference to the OrderManager script
        OrderManager orderManager = FindObjectOfType<OrderManager>();
        if (orderManager != null)
        {
            npc.GetComponent<NPCMovement>().onOrder.AddListener(orderManager.GenerateOrder);
        }
        else
        {
            Debug.LogError("OrderManager not found in scene!");
        }
        // Connect the onOrder event to the GenerateOrder method of the OrderManager script
        npc.GetComponent<NPCMovement>().onOrder.AddListener(orderManager.GenerateOrder);
    }

    

    // Update is called once per frame
    void Update()
    {
        
        if(spawnedNPCs.Count < totalNPCs && spawnedNPCs.Count < maxNPCs)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer > 4)
            {
                spawnTimer = 0;
                SpawnNPC();
            }
            
        }
    }
}
