using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BandAudioController : MonoBehaviour
{
    
    public BandRoleName bandName = BandRoleName.Default;
    
    [Header("Inspector Variables")] 
    private FMOD.Studio.EventInstance instrumentInstance;
    public StudioEventEmitter instrumentEmitter;

    private FMOD.Studio.EventInstance voiceInstance;
    public StudioEventEmitter voiceEmitter;

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
        if (!string.IsNullOrEmpty(instrumentEvent))
        {
            if(DoesEventExist(instrumentEvent))
            {
                Debug.Log("Playing "+instrumentEvent);
                instrumentEmitter.EventReference = FMODUnity.RuntimeManager.PathToEventReference(instrumentEvent);
                //FMODUnity.EventReference.Find(instrumentEvent);
                instrumentEmitter.Play();
                instrumentInstance = instrumentEmitter.EventInstance;
                
                PrintEventParameters(instrumentEvent);
                this.isPlaying = true;
                ConcertAudioEvent.PlayingAudio(this.bandName);
            }
        }

        if (!string.IsNullOrEmpty(voiceEvent))
        {
            if(DoesEventExist(voiceEvent))
            {
                voiceEmitter.EventReference = FMODUnity.RuntimeManager.PathToEventReference(voiceEvent);
                voiceEmitter.Play();
                voiceInstance = voiceEmitter.EventInstance;
                PrintEventParameters(voiceEvent);
                isSinging = true;
                ConcertAudioEvent.PlayingAudio(this.bandName);
            }
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
        
        this.isPlaying = false;
        this.isSinging = false;
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
        int position;
        instrumentEmitter.EventInstance.getTimelinePosition(out position);
        int playbackPosition = (int)(GameStateManager.Instance.levelTime * 1000);
        Debug.Log("Audio Being Resynced old "+ position +"/ new "+ playbackPosition);
        instrumentEmitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instrumentEmitter.EventInstance.start();
        instrumentEmitter.EventInstance.setTimelinePosition(playbackPosition);

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
