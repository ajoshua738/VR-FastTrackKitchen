using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SetPlateParent : MonoBehaviour
{
   
    private bool alreadyHasParent = false;
    [SerializeField] private float raycastDistance = 2.0f;
    [SerializeField] private LayerMask collisionLayerMask;
    private RaycastHit hitInfo;
    GameObject child;
    GameObject parent;

   

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.up, out hitInfo, raycastDistance, collisionLayerMask))
        {
           
            hitInfo.transform.SetParent(transform, true);
            
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.SetParent(null);
            }
        }

    }

  




}
