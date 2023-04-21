using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IngredientRemoveHoverMesh : MonoBehaviour
{
    [SerializeField] private GameObject ingredientSocket;
    private XRSocketInteractor xrSocketInteractor;

    private void Awake()
    {
       
        xrSocketInteractor = ingredientSocket.GetComponent<XRSocketInteractor>();
    }
    // Start is called before the first frame update
    void Start()
    {
        xrSocketInteractor.interactableCantHoverMeshMaterial = null;
    }

   
}
