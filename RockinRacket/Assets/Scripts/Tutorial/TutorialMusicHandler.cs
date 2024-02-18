using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
using System;

public class TutorialMusicHandler : MonoBehaviour
{

    public static TutorialMusicHandler instance;

    public StudioEventEmitter ConcertAudioEmitterPrefab;
    private StudioEventEmitter ConcertAudioEmitterInstance;
    private FMOD.Studio.EventInstance ConcertAudioInstance;    

    [Header("Current Song Details")]
    public bool isMusicPlaying;
    public string loopingAudioPath;
    public string postIntermissionAudioPath;
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

    public void Start()
    {
        ConcertEvents.instance.e_SongStarted.AddListener(SelectAudio);
        TimeEvents.OnGamePaused += PauseAudio;
        TimeEvents.OnGameResumed += ResumeAudio;        
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
        TimeEvents.OnGamePaused -= PauseAudio;
        TimeEvents.OnGameResumed -= ResumeAudio;
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        StopAudio();
    }

    public void SelectAudio()
    {
        if(TutorialManager.Instance.afterIntermission)
        {
            StartAudio(postIntermissionAudioPath);
            StartCoroutine(AudioTimer(songTimer));
        }
        else
        {
            StartAudio(loopingAudioPath);
        }
    }

    public void StartAudio(string audioPath)
    {
        if (ConcertAudioEmitterPrefab == null)
        {
            Debug.Log("Audio Emitter is null");
            return;
        }

        if (!string.IsNullOrEmpty(audioPath) && DoesEventExist(audioPath))
        {
            ConcertAudioEmitterInstance = Instantiate(ConcertAudioEmitterPrefab, transform);
            ConcertAudioEmitterInstance.EventReference = RuntimeManager.PathToEventReference(audioPath);
            ConcertAudioEmitterInstance.Play();
            ConcertAudioInstance = ConcertAudioEmitterInstance.EventInstance;
            this.isMusicPlaying = true;
        }
    }

    private IEnumerator AudioTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        isMusicPlaying = false;
        Debug.Log("Post intermission audio has finished.");
        StopAudio();
        ConcertEvents.instance.e_SongEnded.Invoke();
        ConcertEvents.instance.e_ConcertEnded.Invoke();
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
