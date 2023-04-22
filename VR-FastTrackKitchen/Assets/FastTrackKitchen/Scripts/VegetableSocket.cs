using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VegetableSocket : MonoBehaviour
{

    public XRSocketInteractor socketInteractor;
    public VegetableCutting vegetable;
    // Start is called before the first frame update
    void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        
        socketInteractor.onSelectEntered.AddListener(OnSocketed);
       
        
    }

    private void OnSocketed(XRBaseInteractable interactable)
    {
        // Get the collider of the interactable object
        Collider collider = interactable.GetComponent<Collider>();
        vegetable = interactable.GetComponent<VegetableCutting>();
        // Set the collider as a trigger collider
        collider.isTrigger = true;
        vegetable.isCuttable = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
