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

    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       
        if(!Physics.Raycast(transform.position, transform.up, out hitInfo, raycastDistance, collisionLayerMask) && timer > 2.0f)
        {
            timer = 0.0f;
            Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        }
    }
   
}
