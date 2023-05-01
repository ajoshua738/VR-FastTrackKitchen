using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    //Order Related Parameters
    [SerializeField] private RecipeListSO recipeListSO;
    public List<OrderSO> newOrderSOList;
    public List<OrderSO> completedOrderSOList;
    [SerializeField] private float orderTime = 60.0f;

    private float spawnOrderTimer = 0;
    private float spawnOrderTimerMax = 4f;
   
    [SerializeField] private int maximumOrders = 4;
    [SerializeField] private int maxOrdersList = 1;
    private int orderCount = 0;
    private int orderID = 0;
    //--------------------------------------------------------


    //Plate related parameters
    public Plate plateObjectScript;
    public GameObject plateObject;
    bool isPlateInTrigger;
    public Transform sendPosition;


    //GUI
    public TMP_Text orderText;
    public Image orderImage;

    private void Awake()
    {
        newOrderSOList = new List<OrderSO>();
    }

    void Start()
    {
        isPlateInTrigger = false;
    }

    public void SendOrder()
    {
        plateObject.transform.position = sendPosition.position;
    }
    public void GenerateOrder()
    {
        OrderSO newOrderSO = ScriptableObject.CreateInstance<OrderSO>();
        RecipeSO randomRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        newOrderSO.orderID = orderID;
        newOrderSO.orderTime = orderTime;
        newOrderSO.customerSatisfaction = 1.0f;
        newOrderSO.recipeSO = randomRecipeSO;
        newOrderSOList.Add(newOrderSO);

        Debug.Log(newOrderSO.recipeSO.recipeName);
        orderID++;

    }

    //Called when user ready to serve
    public void ConfirmScore()
    {
        //Happy - >= 80
        //OK - >= 50
        //Upset >= 20
        //Angry == 0

        //float scoreHappy;
        //float scoreOK;
        //float scoreUpset;
        //float scoreAngry;

        float timeLeft = newOrderSOList[0].orderTime;
        float maxTime = orderTime;

        float satisfactionPercentage = timeLeft / maxTime;

        if (satisfactionPercentage >= 0.7f)
        {
            newOrderSOList[0].customerSatisfaction = 1.0f;
        }
        else if (satisfactionPercentage >= 0.45f && satisfactionPercentage < 0.7f)
        {
            newOrderSOList[0].customerSatisfaction = 0.69f;
        }
        else if (satisfactionPercentage >= 0.01f && satisfactionPercentage < 0.45f)
        {
            newOrderSOList[0].customerSatisfaction = 0.44f;
        }
        else
        {
            newOrderSOList[0].customerSatisfaction = 0.0f;

        }

        completedOrderSOList.Add(newOrderSOList[0]);
        newOrderSOList.RemoveAt(0);
  

    }

    //To display in UI/Update
    void CalculateCustomerSatisfaction()
    {
        float timeLeft = newOrderSOList[0].orderTime;
        float maxTime = orderTime;
        float satisfactionPercentage = timeLeft / maxTime;

        if (satisfactionPercentage >= 0.6f)
        {
            newOrderSOList[0].customerSatisfaction = 1.0f;
        }
        else if (satisfactionPercentage >= 0.3f && satisfactionPercentage < 0.6f)
        {
            newOrderSOList[0].customerSatisfaction = 0.69f;
        }
        else if (satisfactionPercentage > 0.2f && satisfactionPercentage < 0.3f)
        {
            newOrderSOList[0].customerSatisfaction = 0.44f;
        }
        else
        {
            newOrderSOList[0].customerSatisfaction = 0.0f;
            completedOrderSOList.Add(newOrderSOList[0]);
            newOrderSOList.RemoveAt(0);
        }




        Debug.Log("Order Time : " + timeLeft);
        Debug.Log("Customer Satisfaction : " + satisfactionPercentage);

    }

    public void CheckOrder()
    {
        if (newOrderSOList.Count > 0 && plateObjectScript.ingredientsOnPlate.Count == newOrderSOList[0].recipeSO.ingredientsSOList.Count)
        {
            bool hasAllIngredients = true;
            foreach (IngredientSO ingredientSO in newOrderSOList[0].recipeSO.ingredientsSOList)
            {
                if (!plateObjectScript.ingredientsOnPlate.Contains(ingredientSO))
                {
                    hasAllIngredients = false;
                    break;
                }
            }

            if (hasAllIngredients)
            {
                ConfirmScore(); //Correct order
                SendOrder();
            }
            else
            {
                ConfirmScore(); //Wrong order (wrong requirements)
                SendOrder();
            }
        }
    }


    


    private void OnTriggerEnter(Collider other)
    {
        if (!isPlateInTrigger)
        {
            plateObjectScript = other.GetComponentInChildren<Plate>();
            if (plateObjectScript != null)
            {
                plateObject = other.gameObject;
                isPlateInTrigger = true;
            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == plateObject)
        {
            isPlateInTrigger = false;
            plateObject = null;
            plateObjectScript = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnOrderTimer -= Time.deltaTime;

        if (orderCount < maximumOrders)
        {
            
            if (spawnOrderTimer <= 0f)
            {
                spawnOrderTimer = spawnOrderTimerMax;

                if (newOrderSOList.Count < maxOrdersList)
                {
                    orderCount++;
                    //GenerateOrder();
                }
            }
        }


        // decrease orderTime for each OrderSO in the list
        foreach (OrderSO order in newOrderSOList)
        {
            order.orderTime -= Time.deltaTime;
            if (order.orderTime <= 0.0f)
            {
                newOrderSOList.RemoveAt(0);
            }
        }

        

        if (newOrderSOList.Count > 0)
        {
            CalculateCustomerSatisfaction();
            OrderSO firstOrder = newOrderSOList[0];
            orderText.text = "Order ID : "+firstOrder.orderID+"\n"
                            +"Recipe: " + firstOrder.recipeSO.recipeName + "\n"
                            + "Satisfaction: " + firstOrder.customerSatisfaction.ToString("0.00") + "\n"
                            + "Time: " + firstOrder.orderTime.ToString("0.0");
            orderImage.sprite = firstOrder.recipeSO.recipeSprite;
        }
        else
        {
            orderText.text = null;
            orderImage.sprite = null;
        }

    }
}
