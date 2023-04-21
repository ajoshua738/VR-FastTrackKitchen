using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SpawnIngredient : MonoBehaviour
{

    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private float raycastDistance = 1.0f;
    private RaycastHit hitInfo;

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform spawnPoint;
    // Update is called once per frame
    void Update()
    {
       
        if(!Physics.Raycast(transform.position, transform.up, out hitInfo, raycastDistance, collisionLayerMask))
        {
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }
   
}
