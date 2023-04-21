using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{

    private Food mainMeal; // The main meal of the order

    public Order(Food mainMeal)
    {
        this.mainMeal = mainMeal;
    }

    public string GetMainMealName()
    {
        return mainMeal.GetName();
    }

    public void GenerateOrder()
    {
        if (mainMeal is Burger burger)
        {
            Burger burgera = Burger.GenerateRandomOrder();
        }

        Debug.Log("Order for " + GetMainMealName() + ":\n" + mainMeal.ToString());
    }

    private void Start()
    {
        GenerateOrder();
    }
}
