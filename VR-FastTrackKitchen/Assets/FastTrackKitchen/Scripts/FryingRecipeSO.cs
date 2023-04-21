using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    [SerializeField] private IngredientSO input;
    [SerializeField] private IngredientSO output;
    [SerializeField] private float fryingTimerMax;

}
