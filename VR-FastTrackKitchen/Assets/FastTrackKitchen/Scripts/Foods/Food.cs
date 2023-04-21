using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    
    
    protected Dictionary<string, bool> ingredientRequirements;
    protected string foodName;
    // Get the name of the food
    public string GetName()
    {
        return foodName;
    }

    // Get the preparation time for the food
    

    // Get the ingredient requirements for the food
    public Dictionary<string, bool> GetIngredientRequirements()
    {
        
        return ingredientRequirements;
    }

}
