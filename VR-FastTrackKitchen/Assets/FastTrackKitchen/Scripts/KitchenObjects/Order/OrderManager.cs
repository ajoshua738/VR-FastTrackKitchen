using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class OrderManager : MonoBehaviour
{
    //Order Related Parameters
    [SerializeField] private RecipeListSO recipeListSO;
    public List<OrderSO> newOrderSOList;
    public List<OrderSO> completedOrderSOList;
    [SerializeField] private float orderTime = 60.0f;

   
   
   
   
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
    public AudioSource customerLeave;

    //public Transform sendPos;

    public List<GameObject> platePositionList;


    //Level Completion stuff
    public GameObject levelScreen;
    public int mistakes = 0;
    public float levelTimer = 0.0f;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text mistakesText;
    public TMP_Text gradeText;



    private void Awake()
    {
        instance = this;
        newOrderSOList = new List<OrderSO>();
        platePositionList = new List<GameObject>();
        
    }

    void Start()
    {
        levelScreen.SetActive(false);
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
        //sendPos = platePos;
        
       
        newOrderSOList.Add(newOrderSO);

        Debug.Log(newOrderSO.recipeSO.recipeName);
        orderID++;
        newOrderSound.Play();

    }

    //Called when user ready to serve
    public void ConfirmScore(bool correctOrder)
    {
        //Happy - >= 80
        //OK - >= 50
        //Upset >= 20
        //Angry == 0

        //float scoreHappy;
        //float scoreOK;
        //float scoreUpset;
        //float scoreAngry;
        float leaveTime = 5.0f;
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
            leaveTime = 0.0f;

        }

        if (correctOrder)
        {
            newOrderSOList[0].isCorrectOrder = true;
        }
        else
        {
            newOrderSOList[0].isCorrectOrder = false;
        }

        StartCoroutine(LeaveTimer(leaveTime));

        
        
        //NPCMovement.instance.Leave(leaveTime);

        completedOrderSOList.Add(newOrderSOList[0]);
       
  

    }

    

    public IEnumerator LeaveTimer(float timer)
    {
        GameObject firstNPC = NPCSpawner.instance.spawnedNPCs[0];
        NPCMovement npcMove = firstNPC.GetComponent<NPCMovement>();
        NPCLeave npcLeave = firstNPC.GetComponent<NPCLeave>();
        yield return new WaitForSeconds(timer);
        //npcMove.isSeated = false;

        npcMove.enabled = false;
        npcLeave.enabled = true;
    }

    //To display in UI/Update
    void CalculateCustomerSatisfaction()
    {
        float timeLeft = newOrderSOList[0].orderTime;
        float maxTime = orderTime;
        float satisfactionPercentage = timeLeft / maxTime;
        float leaveTime = 5.0f;


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
            leaveTime = 0.0f;
            StartCoroutine(LeaveTimer(leaveTime));
            customerLeave.Play();


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
                ConfirmScore(true); //Correct order
                SendOrder();
            }
            else
            {
                ConfirmScore(false); //Wrong order (wrong requirements)
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
        levelTimer += Time.deltaTime;

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

    public void CalculateScore()
    {
        float score = 0.0f;
        string grade = "A";
        foreach (OrderSO order in completedOrderSOList)
        {
            if(order.customerSatisfaction == 1.0f)
            {
                score += 50.0f;
            }
            else if(order.customerSatisfaction == 0.69f)
            {
                score += 25.0f;
            }
            else if (order.customerSatisfaction == 0.44f)
            {
                score += 00.0f;
            }
            else
            {
                score -= 50.0f;
            }


            if (order.isCorrectOrder)
            {
                score += 25.0f;
            }
            else
            {
                score -= 25.0f;
                mistakes++;
            }
        }

        score -= mistakes * 25.0f;


        if (score >= 375.0f)
        {
            grade = "A";
        }
        else if (score >= 225.0f && score < 375.0f)
        {
            grade = "B";
        }
        else if (score >= 75.0f && score < 225.0f)
        {
            grade = "C";
        }
        else if (score >= 0.0f && score < 75.0f)
        {
            grade = "D";
        }
        else
        {
            grade = "FAIL";
        }


        Debug.Log("Score : " + score);
        timeText.text = levelTimer.ToString("F0");
        scoreText.text = ""+score;
        mistakesText.text = "" + mistakes;
        gradeText.text = "" + grade;
        levelScreen.SetActive(true);
    }

    public void FoodUIManager()
    {
        OrderSO firstOrder = newOrderSOList[0];
        if (newOrderSOList.Count > 0)
        {


            orderText.text = "Order ID : " + firstOrder.orderID + "\n"
                             + "Recipe: " + firstOrder.recipeSO.recipeName + "\n"
                             + "Satisfaction: " + "\n"
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
