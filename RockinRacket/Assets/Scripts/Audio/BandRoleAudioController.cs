using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BandRoleAudioController : MonoBehaviour
{


    private FMOD.Studio.EventInstance instrumentInstance;
    public StudioEventEmitter instrumentEmitter;

    private FMOD.Studio.EventInstance voiceInstance;
    public StudioEventEmitter voiceEmitter;

    public string instrumentEvent;
    public string voiceEvent;

    public int ConcertPosition = 0;

    public string parameterName = "BrokenValue";

    [Range(-0,5)]
    public float instrumentBrokenValue = 0;
    
    [Range(-0,5)]
    public float voiceBrokenValue = 0;


    public void ResetAudio()
    {
        this.instrumentEvent = "";
        this.voiceEvent = "";
        this.instrumentBrokenValue = 0;
        this.voiceBrokenValue = 0;
    }

    public void Update()
    {
        SetInstrumentBrokeLevel();
        SetVoiceBrokeLevel();
    }

    public void SetInstrumentBrokeLevel()
    {
        if (instrumentInstance.isValid())
        {
            instrumentInstance.setParameterByName(parameterName, instrumentBrokenValue);
        }
        
    }

    public void SetVoiceBrokeLevel()
    {
        if (voiceInstance.isValid())
        {
            voiceInstance.setParameterByName(parameterName, voiceBrokenValue);
        }
    }

    public void StartSounds()
    {
        instrumentEvent = "";
        voiceEvent = "";
        if(GameStateManager.Instance.CurrentGameState.Song == null)
        {return;}
        
        switch(ConcertPosition)
        {
            case 1:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.PositionOne.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.PositionOne.SecondaryTrackPath;
                break;
            case 2:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.PositionTwo.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.PositionTwo.SecondaryTrackPath;
                break;
            case 3:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.PositionThree.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.PositionThree.SecondaryTrackPath;
                break;
            case 4:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.PositionFour.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.PositionFour.SecondaryTrackPath;
                break;
            case 5:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.Background.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.Background.SecondaryTrackPath;
                break;
            default:
                break;
        }
        if (!string.IsNullOrEmpty(instrumentEvent))
        {
            if(DoesEventExist(instrumentEvent))
            {
                instrumentEmitter.EventReference = FMODUnity.EventReference.Find(instrumentEvent);
                instrumentEmitter.Play();
                instrumentInstance = instrumentEmitter.EventInstance;
                
                PrintEventParameters(instrumentEvent);
            }
        }
        else
        {
            instrumentEmitter.EventReference.Path = null;
        }

        if (!string.IsNullOrEmpty(voiceEvent))
        {
            if(DoesEventExist(voiceEvent))
            {
                voiceEmitter.EventReference = FMODUnity.EventReference.Find(voiceEvent);
                voiceEmitter.Play();
                voiceInstance = voiceEmitter.EventInstance;
                PrintEventParameters(voiceEvent);
            }
        }
        else
        {
            voiceEmitter.EventReference.Path = null;
        }
        
    }

    public bool DoesEventExist(string eventName)
    {
        EventDescription eventDescription;
        bool eventExists = RuntimeManager.StudioSystem.getEvent(eventName, out eventDescription) == FMOD.RESULT.OK;
        if (eventExists && eventDescription.isValid())
        {
            return true;
        }
        return false;
    }

    public void StopSounds()
    {
        voiceEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instrumentEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    
    public void PauseConcert()
    {
        voiceEmitter.EventInstance.setPaused(true);
        instrumentEmitter.EventInstance.setPaused(true);
    }

    public void ResumeConcert()
    {
        voiceEmitter.EventInstance.setPaused(false);
        instrumentEmitter.EventInstance.setPaused(false);
    }

    

    void Start()
    {
        TimeEvents.OnGamePaused += PauseConcert;    
        TimeEvents.OnGameResumed += ResumeConcert;  
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
        ConcertAudioEvent.OnAudioBroken += AudioBroken;
        ConcertAudioEvent.OnAudioFixed += AudioFixed;
        
        ConcertAudioEvent.OnConcertEnd += ConcertEnd;
    }

    void OnDestroy()
    {
        TimeEvents.OnGamePaused -= PauseConcert; 
        TimeEvents.OnGameResumed -= ResumeConcert; 
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
        ConcertAudioEvent.OnAudioBroken -= AudioBroken;
        ConcertAudioEvent.OnAudioFixed -= AudioFixed;
        
        ConcertAudioEvent.OnConcertEnd -= ConcertEnd;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                StartSounds();
                break;
            case GameModeType.Intermission:
                break;
            default:
                break;
        }
    }
    
    public void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                StopSounds();
                ResetAudio();
                break;
            case GameModeType.Intermission:
                break;
            default:
                break;
        }
    }

    public void AudioBroken(object sender, ConcertAudioEventArgs e)
    {
        if(e.ConcertPosition != this.ConcertPosition)
        {return;}

        if(e.AffectInstrument)
        {
            instrumentBrokenValue += e.BrokenValue;
            SetInstrumentBrokeLevel();
        }
        else
        {
            voiceBrokenValue += e.BrokenValue;
            SetInstrumentBrokeLevel();
        }
    }

    public void AudioFixed(object sender, ConcertAudioEventArgs e)
    {
        if(e.ConcertPosition != this.ConcertPosition)
        {return;}

        if(e.AffectInstrument)
        {
            instrumentBrokenValue -= e.BrokenValue;
            SetInstrumentBrokeLevel();
        }
        else
        {
            voiceBrokenValue -= e.BrokenValue;
            SetInstrumentBrokeLevel();
        }
    }

    public void ConcertEnd(object sender, ConcertAudioEventArgs e)
    {
        this.StopSounds();
    }
    
    void PrintEventParameters(string eventName)
    {
        EventDescription eventDescription;
        if (RuntimeManager.StudioSystem.getEvent(eventName, out eventDescription) != FMOD.RESULT.OK)
        {
            Debug.LogError("Failed to get event description.");
            return;
        }

        int parameterCount;
        eventDescription.getParameterDescriptionCount(out parameterCount);
        Debug.Log(parameterCount);
    }

}
