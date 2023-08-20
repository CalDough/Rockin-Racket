using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
/*
    This script is a singleton which manages the Story of the game. Either the ink file will contain the boolean data for what
    cutscenes, progress, and dialogue have been activated or this script will. 
    Other Managers and scripts will be able to access this manager to obtain a bool whether an story event has happened or can happen.
    This script can also contain an GameEventContainer for concertevents that relate to the story.
    Most interaction to ink/story elements will occur through this script.

*/
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