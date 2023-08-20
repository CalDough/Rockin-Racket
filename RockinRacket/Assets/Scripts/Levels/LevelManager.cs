using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is for a test feature for an overworld level system like in mario. Ignore for now.
*/
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

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
}
