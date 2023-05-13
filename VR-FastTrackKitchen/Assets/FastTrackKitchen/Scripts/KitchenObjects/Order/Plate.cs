using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public List<IngredientSO> ingredientsOnPlate;

    public bool destroy = false;

    public static Plate instance;

    private void Awake()
    {
        instance = this;
        ingredientsOnPlate = new List<IngredientSO>();
    }

   

    public void DestroyObject()
    {
        for(int i =0; i < ingredientsOnPlate.Count; i++)
        {
            ingredientsOnPlate.RemoveAt(i);
        }
        destroy = true;

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
        if (destroy)
        {
            Destroy(gameObject);
        }
    }
}
