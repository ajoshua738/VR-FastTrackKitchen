using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    public TMP_Text orderText;
    public float timePerOrder; //Time given for each order
    public float timeBetweenOrders; //Time between each new order


    public float timeRemaining; //Time remaining to complete the current order

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = timePerOrder;
    }

    // Update is called once per frame
    void Update()
    {
        orderText.text = "Order time : " + timeRemaining;
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            orderText.text = "Order failed";

        }

    }

    public void GenerateOrder()
    {

    }
}
