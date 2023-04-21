using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CookBurgerPatty : MonoBehaviour
{
   
    
  

    //Time it takes to cook the burger
    public float cookTime = 10.0f;
    float increment;
    public float burnTime = 5.0f;
    
  

    private AudioSource cookingSound;
    private Material material;
    private Renderer renderer;
  

    bool isCooked = false;
    bool isBurned = false;


    

    // Start is called before the first frame update
    void Start()
    {
        cookingSound = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        //value to increment each second until burger is cooked
        increment = 1.0f / cookTime;


      


    }

   

 
    private IEnumerator CookBurger()
    {

            float t = 0.0f;
            cookingSound.Play();
            while (t < 1.0f)
            {
                yield return new WaitForSeconds(1.0f); //waits 1 second
                t += increment;
                Debug.Log(t);
                material.SetFloat("_Blend", t);
            }

        StopCoroutine(CookBurger());    
        StartCoroutine(OverCooking());
       
    }

    private IEnumerator OverCooking()
    {

        float t = 0.0f;
       
        while (t < burnTime)
        {
            yield return new WaitForSeconds(1.0f); //waits 1 second
            t += 1.0f;
            Debug.Log(t);
            
        }
        Debug.Log("burnt");
        StopCoroutine(OverCooking());
    }
    



    public void Cooking()
    {
       
       StartCoroutine(CookBurger());
        
       
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
      

        
            
     
    }

    

  
}
