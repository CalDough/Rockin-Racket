using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenLoader : MonoBehaviour
{
    [SerializeField] GameObject splashScreen;
    [SerializeField] DialogueTrigger characterTrigger;

    private bool shownSplash = false;
    
    void Start()
    {
        splashScreen.SetActive(false);
    }

    public void button_LoadSplashScreen()
    {
        if (shownSplash)
        {
            button_toDialogue();
        }
        else
        {
            splashScreen.SetActive(true);
            shownSplash = true;
        }
    }

    public void button_toDialogue()
    {
        splashScreen.SetActive(false);
        characterTrigger.Button_StartDialogue();
    }

    void Update()
    {
        
    }
}
