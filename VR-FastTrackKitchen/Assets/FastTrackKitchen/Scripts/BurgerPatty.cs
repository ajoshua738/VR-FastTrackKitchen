using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerPatty : MonoBehaviour
{

    //Collision handlng
    [SerializeField] private float raycastDistance = .1f;
    [SerializeField] private  LayerMask collisionLayerMask; 
    private RaycastHit hitInfo;
    private bool isCooking; // to check if the patty already placed on the griddle once
    private bool cookingCoroutineStarted;
    private bool isCooked;
    private bool overcookingCoroutineStarted;

    private XRGrabInteractable xrGrab;
   

    [SerializeField] private InteractionLayerMask cookedPatty;
    //Material lerping handling
    [SerializeField] private float cookTime = 10.0f;
    private float increment;
    [SerializeField] private float burnTime = 5.0f;
    private bool isBurnt;

    

    private AudioSource cookingSound;
    private Material material;
    private Renderer renderer;

    [SerializeField] private GameObject indgredientSocket;

    // Start is called before the first frame update
    void Start()
    {
     
        indgredientSocket.SetActive(false);
        isCooking = false;
        cookingCoroutineStarted = false;
        isCooked = false;
        overcookingCoroutineStarted = false;
        isBurnt = false;
        cookingSound = GetComponent<AudioSource>();
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        //value to increment each second until burger is cooked
        increment = 1.0f / cookTime;
        xrGrab = GetComponent<XRGrabInteractable>();
    }

   
    private IEnumerator OverCooking()
    {
        float t = 0.0f;
        while (t < burnTime)
        {
            if (isCooking)
            {
                
                yield return new WaitForSeconds(1.0f); //waits 1 second
                t += 1.0f;
                Debug.Log(t);
            }
            else
            {
                
                yield return null; // pause the coroutine until isCooking is true again
            }
        }
        Debug.Log("burnt");
        isBurnt = true;
        StopCoroutine(OverCooking());
    }
    private IEnumerator CookBurger()
    {

        float t = 0.0f;
        cookingSound.Play();
        while (t < 1.0f)
        {
            if (isCooking)
            {
               
                yield return new WaitForSeconds(1.0f); //waits 1 second
                t += increment;
                Debug.Log(t);
                material.SetFloat("_Blend", t);
            }
            else
            {
               

                yield return null; // pause the coroutine until isCooking is true again
            }


        }

        StopCoroutine(CookBurger());
        isCooked = true;
        xrGrab.interactionLayers = cookedPatty;
        indgredientSocket.SetActive(true);
      


    }


  

    // Update is called once per frame
    void Update()
    {
      
        

        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, raycastDistance, collisionLayerMask))
        {
           isCooking=true;
        }
        else
        {
            isCooking = false;
        }
        if (!isCooked && isCooking && !cookingCoroutineStarted)
        {
            StartCoroutine(CookBurger());
            cookingCoroutineStarted = true;
          
        }
        if(isCooked && isCooking && !overcookingCoroutineStarted)
        {
            StartCoroutine(OverCooking());
            overcookingCoroutineStarted = true;
           
        }

        if(isCooking && !isBurnt)
        {
            if (!cookingSound.isPlaying)
            {
                cookingSound.Play();
            }
        }
        else
        {
            cookingSound.Stop();
        }
        

    }
}
