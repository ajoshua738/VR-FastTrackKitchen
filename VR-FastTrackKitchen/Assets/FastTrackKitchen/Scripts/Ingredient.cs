using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientSO ingredientSO;

    public IngredientSO GetIngredientSO()
    {
        return ingredientSO;
    }
}
