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
    


    //GUI
    public TMP_Text orderText;
    public Image orderFoodImage;
    public Image orderEmojiImage;
    public List<Sprite> customerEmoji;

    public static OrderManager instance;

    public AudioSource newOrderSound;

   
  
    private void Awake()
    {
        instance = this;
        newOrderSOList = new List<OrderSO>();
        
    }

    void Start()
    {
        isPlateInTrigger = false;
      
    }

    public void SendOrder()
    {
        plateObject.transform.position = newOrderSOList[0].platePosition.position;
        newOrderSOList.RemoveAt(0);
    }
    public void GenerateOrder(Transform platePos)
    {
        OrderSO newOrderSO = ScriptableObject.CreateInstance<OrderSO>();
        RecipeSO randomRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
        newOrderSO.orderID = orderID;
        newOrderSO.orderTime = orderTime;
        newOrderSO.customerSatisfaction = 1.0f;
        newOrderSO.recipeSO = randomRecipeSO;
        
        newOrderSO.platePosition = platePos;
    
        
       
        newOrderSOList.Add(newOrderSO);

        Debug.Log(newOrderSO.recipeSO.recipeName);
        orderID++;
        newOrderSound.Play();

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

        if (satisfactionPercentage > 0.7f)
        {
            newOrderSOList[0].customerSatisfaction = 1.0f;
        }
        else if (satisfactionPercentage >= 0.4f)
        {
            newOrderSOList[0].customerSatisfaction = 0.69f;
        }
        else if (satisfactionPercentage > 0.0f)
        {
            newOrderSOList[0].customerSatisfaction = 0.44f;
        }
        else
        {
            newOrderSOList[0].customerSatisfaction = 0.0f;

        }

        completedOrderSOList.Add(newOrderSOList[0]);
       
  

    }

    //To display in UI/Update
    void CalculateCustomerSatisfaction()
    {
        float timeLeft = newOrderSOList[0].orderTime;
        float maxTime = orderTime;
        float satisfactionPercentage = timeLeft / maxTime;

        if (satisfactionPercentage > 0.7f)
        {
            newOrderSOList[0].customerSatisfaction = 1.0f;
            
        }
        else if (satisfactionPercentage >= 0.4f)
        {
            newOrderSOList[0].customerSatisfaction = 0.69f;
           
        }
        else if (satisfactionPercentage > 0.0f)
        {
            newOrderSOList[0].customerSatisfaction = 0.44f;
            
        }
        else
        {
           
            newOrderSOList[0].customerSatisfaction = 0.0f;
            completedOrderSOList.Add(newOrderSOList[0]);
            newOrderSOList.RemoveAt(0);
        }




        //Debug.Log("Order Time : " + timeLeft);
        //Debug.Log("Customer Satisfaction : " + satisfactionPercentage);

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
        //spawnOrderTimer -= Time.deltaTime;

        //if (orderCount < maximumOrders)
        //{

        //    if (spawnOrderTimer <= 0f)
        //    {
        //        spawnOrderTimer = spawnOrderTimerMax;

        //        if (newOrderSOList.Count < maxOrdersList)
        //        {
        //            orderCount++;
        //            GenerateOrder();
        //        }
        //    }
        //}


        // decrease orderTime for each OrderSO in the list
        //foreach (OrderSO order in newOrderSOList)
        //{
        //    order.orderTime -= Time.deltaTime;

        //}

        if(newOrderSOList.Count > 0)
        {
            newOrderSOList[0].orderTime -= Time.deltaTime;
        }
        

        

        if (newOrderSOList.Count > 0)
        {
            CalculateCustomerSatisfaction();
           
         
            
            FoodUIManager();


        }
      

    }

    public void FoodUIManager()
    {
        OrderSO firstOrder = newOrderSOList[0];
        if (newOrderSOList.Count > 0)
        {


            orderText.text = "Order ID : " + firstOrder.orderID + "\n"
                             + "Recipe: " + firstOrder.recipeSO.recipeName + "\n"
                             + "Satisfaction: " + firstOrder.customerSatisfaction.ToString("0.00") + "\n"
                             + "Time: " + firstOrder.orderTime.ToString("0.0");
            orderFoodImage.sprite = firstOrder.recipeSO.recipeSprite;


            if (firstOrder.customerSatisfaction == 1.0f)
            {
                orderEmojiImage.sprite = customerEmoji[0];
            }
            else if (firstOrder.customerSatisfaction == 0.69f)
            {
                orderEmojiImage.sprite = customerEmoji[1];
            }
            else if (firstOrder.customerSatisfaction == 0.44f)
            {
                orderEmojiImage.sprite = customerEmoji[2];
            }
            else
            {
                orderEmojiImage.sprite = customerEmoji[3];
            }
        }
        else
        {
            orderText.text = null;
            orderFoodImage.sprite = null;
            orderEmojiImage.sprite = null;
        }
        
    }
}
