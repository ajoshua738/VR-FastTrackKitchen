using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Griddle : MonoBehaviour
{

  
    public float transitionTime = 10f;
    public float transitionTick = 1.0f;

    private Material material;
    private Renderer renderer;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
        StartCoroutine(LerpMaterial());

    }
    private IEnumerator LerpMaterial()
    {
        while (t < 1.0f)
        {
            yield return new WaitForSeconds(transitionTick);
            t += 0.1f;
            material.SetFloat("_Blend", t);
        }
  
      
    }
    // Update is called once per frame
    void Update()
    {
   
    }

  
}
