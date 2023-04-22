using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class VegetableCutting : MonoBehaviour
{
    
    public GameObject vegetablePrefab;
    public int hitsToDestroy = 0;
    public int hits;
    public bool isCuttable { get; set; }
    public string knifeTag;
    


    private void Start()
    {
        isCuttable = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (isCuttable && other.gameObject.tag == knifeTag)
        {
            hits++;


            if (hits >= hitsToDestroy)
            {
                Instantiate(vegetablePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }


    



}
