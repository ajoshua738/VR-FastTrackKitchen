using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TableBell : MonoBehaviour
{
   
    public UnityEvent onPress;
    float timer = 0.0f;
  
  
    AudioSource sound;
    bool canBePressed;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        canBePressed = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (canBePressed)
        {
            canBePressed = false;
            sound.Play();
            onPress.Invoke();
          

        }
    }

   


    // Update is called once per frame
    void Update()
    {
       
        timer += Time.deltaTime;
        if(timer > 3.0f)
        {
            canBePressed = true;
            timer = 0.0f;
           
        }
    }
}
