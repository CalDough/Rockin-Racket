using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ink.Runtime;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    public TextAsset MainDialogueFile;
    //I was thinking that there might be a global ink file with boolean variables or other things that scripts can check for conditions
    
    void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(this);
        } 
    }

    //probably return like ink file or something
    public bool GetDialogue()
    {
        return true;
    }
    
}