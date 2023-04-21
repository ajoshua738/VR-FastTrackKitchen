using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TableBell : MonoBehaviour
{
   
    public UnityEvent onPress;
    float timer = 0;
    GameObject hands;
  
    AudioSource sound;
    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!isPressed)
        {
           
           
            sound.Play();
            onPress.Invoke();
            isPressed = true;

        }
    }

   
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            isPressed = false;
        }
    }
}
