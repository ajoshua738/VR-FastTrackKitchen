using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : Food
{
    // Constructor with optional ingredient parameters
    public Burger(bool hasLettuce = true, bool hasCheese = true, bool hasTomato = true, bool hasOnion = true)
    {
        foodName = "Burger";

        ingredientRequirements = new Dictionary<string, bool>()
        {
            { "Lettuce", hasLettuce },
            { "Cheese", hasCheese },
            { "Tomato", hasTomato },
            { "Onion", hasOnion },
            { "Patty", true },
            { "Top Bun", true },
            { "Bottom Bun", true }
        };
    }
    //public Dictionary<string, bool> GetIngredientRequirements()
    //{
    //    return ingredientRequirements;
    //}

    // Method to randomly generate a Burger order with different ingredient requirements
    public static Burger GenerateRandomOrder()
    {
        // Generate random values for each ingredient requirement
        bool hasLettuce = Random.value > 0.5f;
        bool hasCheese = Random.value > 0.5f;
        bool hasTomato = Random.value > 0.5f;
        bool hasOnion = Random.value > 0.5f;

        // Create a new Burger object with the randomly generated ingredient requirements
        return new Burger(hasLettuce, hasCheese, hasTomato, hasOnion);
    }

}
