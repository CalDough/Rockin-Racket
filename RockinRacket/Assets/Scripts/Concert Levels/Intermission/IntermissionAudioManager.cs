using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

/*
 *  This script handles the audio playing for intermission
 * 
 */


public class IntermissionAudioManager : MonoBehaviour
{
    public StudioEventEmitter checkOut;
    public StudioEventEmitter itemClick;

    private void Start()
    {
        ConcertEvents.instance.e_TriggerSound.AddListener(PlaySound);
    }

    /*
     * This method is called by event to play the given sound
     */
    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "checkOut":
                checkOut.Play(); break;
            case "itemClick":
                itemClick.Play(); break;
            default:
                break;
        }
    }
}
