using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;
    // Start is called before the first frame update
    void Start()
    {
        menu.transform.position = head.position + new Vector3(head.forward.x, 0.25f, head.forward.z).normalized * spawnDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0.25f, head.forward.z).normalized * spawnDistance;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
    }

    public void resumeButton()
    {
        menu.SetActive(!menu.activeSelf);
        menu.transform.position = head.position + new Vector3(head.forward.x, 0.25f, head.forward.z).normalized * spawnDistance;
    }
}
