using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class InputManager : MonoBehaviour
{  
    public static InputManager instance;

    public bool inputEnabled;

    public static InputManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one Input Manager scripts in this scene");
        }

        instance = this;
        inputEnabled = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.GetInstance().dialogueActive)
        {
            inputEnabled = false;
        }
        else
        {
            inputEnabled = true;
        }
        
        if (inputEnabled && Input.GetMouseButtonDown(0))
        {
            //Creates a Ray originating from the mousePosition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 10f, Color.green, 2f);
            //Creates a RayCastHit
            RaycastHit rch;
            //Checks if an object is hit by the raycast
            if (Physics.Raycast(ray, out rch))
            {
                Debug.Log("Something's being hit");
                DialogueTrigger dTrigger;

                if (rch.collider.gameObject.TryGetComponent<DialogueTrigger>(out dTrigger))
                {
                    Debug.Log("Detected Dialogue Trigger Component");
                    dTrigger.OnRayHit();
                }
                
            }
        }
    }
}
