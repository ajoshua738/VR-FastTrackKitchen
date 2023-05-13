using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public List<OrderSO> orders;
    public static ScoreManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        orders = new List<OrderSO>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
