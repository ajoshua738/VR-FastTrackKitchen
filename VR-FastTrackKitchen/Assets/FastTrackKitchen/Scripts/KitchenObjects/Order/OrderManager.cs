using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private RecipeListSO recipeListSO;
  
    public List<OrderSO> newOrderSOList;
    [SerializeField] private float minTime = 30.0f;
    [SerializeField] private float maxTime = 180.0f;

    private float spawnOrderTimer = 0;
    private float spawnOrderTimerMax = 4f;
    private int ordersMax = 4;

    //public Plate plate;
    public Plate plateObject;


    private void Awake()
    {
        newOrderSOList = new List<OrderSO>();
    }
    private void OnTriggerEnter(Collider other)
    {
        plateObject = other.gameObject.GetComponent<Plate>();
    }
    private void OnTriggerExit(Collider other)
    {
        plateObject = null;
    }

    void GenerateOrder()
    {
        OrderSO newOrderSO = ScriptableObject.CreateInstance<OrderSO>();
        RecipeSO randomRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        newOrderSO.orderTime = Random.Range(minTime, maxTime);
        newOrderSO.customerSatisfaction = 1.0f;
        newOrderSO.recipeSO = randomRecipeSO;
        newOrderSOList.Add(newOrderSO);

        Debug.Log(newOrderSO.recipeSO.recipeName);

    }

    public void CheckOrder()
    {
        if (newOrderSOList.Count > 0 && plateObject.ingredientsOnPlate.Count == newOrderSOList[0].recipeSO.ingredientsSOList.Count)
        {
            bool hasAllIngredients = true;
            foreach (IngredientSO ingredientSO in newOrderSOList[0].recipeSO.ingredientsSOList)
            {
                if (!plateObject.ingredientsOnPlate.Contains(ingredientSO))
                {
                    hasAllIngredients = false;
                    break;
                }
            }

            if (hasAllIngredients)
            {
                newOrderSOList.RemoveAt(0);
                hasAllIngredients = false;

            }
        }
    }
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        spawnOrderTimer -= Time.deltaTime;
        if(spawnOrderTimer <= 0f)
        {
            spawnOrderTimer = spawnOrderTimerMax;

            if(newOrderSOList.Count < ordersMax)
            {
                GenerateOrder();
            }
        }


      


  
    }
}
