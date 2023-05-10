using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class OrderSO : ScriptableObject
{
    public float orderTime;
    public float customerSatisfaction = 1.0f;
    public RecipeSO recipeSO;
    public int orderID;
    public Transform platePosition;
}
