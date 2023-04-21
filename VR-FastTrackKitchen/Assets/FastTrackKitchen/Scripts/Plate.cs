using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public List<IngredientSO> ingredientsOnPlate;

    private void Awake()
    {
        ingredientsOnPlate = new List<IngredientSO>();
    }



    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            IngredientSO ingredientSO = ingredient.GetIngredientSO();
            if (ingredientSO != null)
            {
                ingredientsOnPlate.Add(ingredientSO);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            IngredientSO ingredientSO = ingredient.GetIngredientSO();
            if (ingredientSO != null)
            {
                ingredientsOnPlate.Remove(ingredientSO);
            }
        }
    }

    private void Update()
    {
        Debug.Log(ingredientsOnPlate);
    }
}
