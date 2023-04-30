using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    private int chairID;
    private static int nextID = 0;

    public bool isOccupied = false;

    public bool IsOccupied
    {
        get { return isOccupied; }
        set { isOccupied = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Awake()
    {
        chairID = nextID;
        nextID++;
    }
    public int GetChairID()
    {
        return chairID;
    }

}
