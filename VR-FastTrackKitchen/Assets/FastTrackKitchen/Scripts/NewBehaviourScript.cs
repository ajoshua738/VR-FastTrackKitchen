using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void LoadRestaurantScene()
    {
        AudioManager.instance.SetMusicClip(AudioManager.instance.gameBackground);
        SceneManager.LoadScene("Restaurant Song Test");
    }

    public Transform camTransform;

    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
    }
}
