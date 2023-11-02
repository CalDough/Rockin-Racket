using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BandAudioController : MonoBehaviour
{
    
    public BandRoleName bandName = BandRoleName.Default;
    
    [Header("FMOD Prefabs")]
    public StudioEventEmitter instrumentEmitterPrefab;
    public StudioEventEmitter voiceEmitterPrefab;

    private StudioEventEmitter instrumentEmitterInstance;
    private StudioEventEmitter voiceEmitterInstance;
    
    private FMOD.Studio.EventInstance instrumentInstance;


    private FMOD.Studio.EventInstance voiceInstance;


    public string instrumentEvent;
    public string voiceEvent;
    public string parameterName = "BrokenValue";

    [Header("Test Variables")] 
    public bool isPlaying = false;
    public bool isSinging = false;

    public float HypeGeneration = 0;

    [Range(-0,5)]
    public float instrumentBrokenValue = 0;
    [Range(-0,5)]
    public float voiceBrokenValue = 0;


    public void ResetAudio()
    {
        StopSounds();
        this.instrumentEvent = "";
        this.voiceEvent = "";
        this.instrumentBrokenValue = 0;
        this.voiceBrokenValue = 0;
    }

    public void FixedUpdate()
    {
        SetInstrumentBrokeLevel();
        SetVoiceBrokeLevel();
    }

    
    public void StartSounds()
    {
        instrumentEvent = "";
        voiceEvent = "";
        if (instrumentEmitterInstance != null)
        { Destroy(instrumentEmitterInstance.gameObject);}
        if (voiceEmitterInstance != null) 
        {Destroy(voiceEmitterInstance.gameObject);}

        if(GameStateManager.Instance.CurrentGameState.Song == null)
        {return;}
        
        switch(bandName)
        {
            case BandRoleName.Haley:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.Haley.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.Haley.SecondaryTrackPath;
                break;
            case BandRoleName.Kurt:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.Kurt.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.Kurt.SecondaryTrackPath;
                break;
            case BandRoleName.Ace:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.Ace.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.Ace.SecondaryTrackPath;
                break;
            case BandRoleName.MJ:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.MJ.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.MJ.SecondaryTrackPath;
                break;
            case BandRoleName.Speakers:
                this.instrumentEvent = GameStateManager.Instance.CurrentGameState.Song.Speakers.PrimaryTrackPath;
                this.voiceEvent = GameStateManager.Instance.CurrentGameState.Song.Speakers.SecondaryTrackPath;
                break;
            default:
                Debug.Log("Trying to affect band member not on list: " + bandName);
                break;
        }

        if (!string.IsNullOrEmpty(instrumentEvent) && DoesEventExist(instrumentEvent))
        {
            instrumentEmitterInstance = Instantiate(instrumentEmitterPrefab, transform);
            instrumentEmitterInstance.EventReference = RuntimeManager.PathToEventReference(instrumentEvent);
            instrumentEmitterInstance.Play();
            instrumentInstance = instrumentEmitterInstance.EventInstance;
            this.isPlaying = true;
            ConcertAudioEvent.PlayingAudio(this.bandName);
        }

        if (!string.IsNullOrEmpty(voiceEvent) && DoesEventExist(voiceEvent))
        {
            voiceEmitterInstance = Instantiate(voiceEmitterPrefab, transform);
            voiceEmitterInstance.EventReference = RuntimeManager.PathToEventReference(voiceEvent);
            voiceEmitterInstance.Play();
            voiceInstance = voiceEmitterInstance.EventInstance;
            isSinging = true;
            ConcertAudioEvent.PlayingAudio(this.bandName);
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
        if (instrumentEmitterInstance != null)
        {
            instrumentEmitterInstance.Stop();
            Destroy(instrumentEmitterInstance.gameObject);
        }
        if (voiceEmitterInstance != null)
        {
            voiceEmitterInstance.Stop();
            Destroy(voiceEmitterInstance.gameObject);
        }

        this.isPlaying = false;
        this.isSinging = false;
    }
    
    public void PauseConcert()
    {
        if (voiceEmitterInstance != null)
        {voiceEmitterInstance.EventInstance.setPaused(true);}
        if (instrumentEmitterInstance != null)
        {instrumentEmitterInstance.EventInstance.setPaused(true);}
    }

    public void ResumeConcert()
    {
        if (voiceEmitterInstance != null)
        {voiceEmitterInstance.EventInstance.setPaused(false);}
        if (instrumentEmitterInstance != null)
        {instrumentEmitterInstance.EventInstance.setPaused(false);}
    }

    public void SetInstrumentBrokeLevel()
    {
        if (instrumentInstance.isValid())
        {instrumentInstance.setParameterByName(parameterName, instrumentBrokenValue);}
    }

    public void SetVoiceBrokeLevel()
    {
        if (voiceInstance.isValid())
        {voiceInstance.setParameterByName(parameterName, voiceBrokenValue);}
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

        ConcertAudioEvent.OnRequestBandPlayers += SendToRequester;
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
        ConcertAudioEvent.OnRequestBandPlayers -= SendToRequester;

    }

    public void SendToRequester(object sender, ConcertAudioEventArgs e)
    {
        ConcertAudioEvent.SendBandPlayers(this);
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
        if(e.ConcertPosition != this.bandName)
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
        if(e.ConcertPosition != this.bandName)
        {return;}

        if(e.AffectInstrument)
        {
            instrumentBrokenValue -= e.BrokenValue;
            SetInstrumentBrokeLevel();
            //ResyncAudio();
        }
        else
        {
            voiceBrokenValue -= e.BrokenValue;
            SetInstrumentBrokeLevel();
        }
    }

    public void ResyncAudio()
    {
        /*
        int position;
        instrumentEmitter.EventInstance.getTimelinePosition(out position);
        int playbackPosition = (int)(GameStateManager.Instance.levelTime * 1000);
        Debug.Log("Audio Being Resynced old "+ position +"/ new "+ playbackPosition);
        instrumentEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instrumentEmitter.EventInstance.start();
        instrumentEmitter.EventInstance.setTimelinePosition(playbackPosition);
        */
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
        //Debug.Log(parameterCount);
    }

}
