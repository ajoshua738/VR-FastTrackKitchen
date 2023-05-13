using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlate : MonoBehaviour
{
   
    public static DestroyPlate instance;
   


    public List<GameObject> collectedObjects = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entered object is a GameObject
        GameObject enteredObject = other.gameObject;
        if (enteredObject != null)
        {
            if (enteredObject.CompareTag("Plate"))
            {
                collectedObjects.Add(enteredObject);
            }
            if (enteredObject.CompareTag("BurgerPatty"))
            {
                collectedObjects.Add(enteredObject);
            }
            if (enteredObject.CompareTag("Ingredient"))
            {
                collectedObjects.Add(enteredObject);
            }
            // Add the entered object to the list of collected objects
            

            // You can perform additional actions or logic here if needed
        }
    }

    public void DeleteCollectedObjects()
    {
        foreach (GameObject obj in collectedObjects)
        {
            Destroy(obj);
        }

        // Clear the collectedObjects list
        collectedObjects.Clear();
    }
}
