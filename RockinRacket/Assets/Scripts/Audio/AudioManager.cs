using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
/*
    This script is a singleton which manages the audio across scenes and in the concert
    It should be recoded. It takes data from Band Manager and the Concerts Current track and sets FMOD emitters to play sounds
    Upon leaving a concert it destroys the Sound Objects.

*/
public class AudioManager : MonoBehaviour
{
    
    public static AudioManager Instance { get; private set; }

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
