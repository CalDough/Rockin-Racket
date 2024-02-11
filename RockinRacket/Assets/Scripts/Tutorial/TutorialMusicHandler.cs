using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class TutorialMusicHandler : MonoBehaviour
{

    public static TutorialMusicHandler instance;

    public StudioEventEmitter ConcertAudioEmitterPrefab;
    private StudioEventEmitter ConcertAudioEmitterInstance;
    private FMOD.Studio.EventInstance ConcertAudioInstance;    

    [Header("Current Song Details")]
    public bool afterIntermission;
    public bool isMusicPlaying;
    public string audioPath;
    public float songTimer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void StartLoopingAudio()
    {
        if (ConcertAudioEmitterPrefab == null)
        {Debug.Log("Audio Emitter is null"); return;}

        if (!string.IsNullOrEmpty(audioPath) && DoesEventExist(audioPath))
        {
            ConcertAudioEmitterInstance = Instantiate(ConcertAudioEmitterPrefab, transform);
            ConcertAudioEmitterInstance.EventReference = RuntimeManager.PathToEventReference(audioPath);
            ConcertAudioEmitterInstance.Play();
            ConcertAudioInstance = ConcertAudioEmitterInstance.EventInstance;
            this.isMusicPlaying = true;
        }
    }

    public bool DoesEventExist(string eventName)
    {
        EventDescription eventDescription;
        bool eventExists = RuntimeManager.StudioSystem.getEvent(eventName, out eventDescription) == FMOD.RESULT.OK;
        if (eventExists && eventDescription.isValid())
        {return true;}
        return false;
    }

    public void PauseAudio()
    {
        if (ConcertAudioEmitterInstance != null)
        {ConcertAudioEmitterInstance.EventInstance.setPaused(true);}
    }
    
    public void ResumeAudio()
    {
        if (ConcertAudioEmitterInstance != null)
        {ConcertAudioEmitterInstance.EventInstance.setPaused(false);}
    }

    public void StopAudio()
    {
        if (ConcertAudioEmitterInstance != null)
        {
            ConcertAudioEmitterInstance.Stop();
            Destroy(ConcertAudioEmitterInstance.gameObject);
        }

        this.isMusicPlaying = false;
    }
}
