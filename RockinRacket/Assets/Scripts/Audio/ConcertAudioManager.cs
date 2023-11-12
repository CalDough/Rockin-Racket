using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
/*
This class manages all the songs played during the concert as well as manages the broken levels of each member
It gets the path of the current song from the  StateManager.Instance.CurrentState.Song.FMODMultiTrack.PrimaryTrackPath;
Then it uses that to access the FMOD Multi track with each Logic Track to access the "BrokenValue" parameter

*/
public class ConcertAudioManager : MonoBehaviour
{

    public static ConcertAudioManager Instance { get; private set; }
    public StudioEventEmitter ConcertAudioEmitterPrefab; //If the emitter is not set we make one.
    private StudioEventEmitter ConcertAudioEmitterInstance;
    private FMOD.Studio.EventInstance ConcertAudioInstance;    
    public string ConcertAudioPath;
    //public string parameterName = "BrokenValue";
    [Header("Test Variables")] 
    public bool isPlaying = false;
    private List<string> logicTracks = new List<string> { "Guitar", "Bass", "Drums" };

    [Header("Debug Variables")]
    public bool debugUpdateAudio = false; 

    [Header("Band Variables")] 
    public bool AceAvailable = true;    
    [Range(-0,5)]public float AceBrokenValue = 0;

    public bool MJAvailable = true;
    [Range(-0,5)]public float MJBrokenValue = 0;

    public bool HaleyAvailable = true;
    [Range(-0,5)]public float HaleyBrokenValue = 0;

    public bool KurtAvailable = true;
    [Range(-0,5)]public float KurtBrokenValue = 0;

    public bool SpeakersAvailable = false;
    [Range(-0,5)]public float SpeakersBrokenValue = 0;

    public bool HarveyAvailable = false;
    [Range(-0,5)]public float HarveyBrokenValue = 0;




    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        SubscribeEvents();
    }
    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    void Update()
    {
        if (debugUpdateAudio)
        {
            UpdateFMODParameters();
        }
    }

    private void UpdateFMODParameters()
    {
        SetInstrumentBrokenValue("Voice", HaleyBrokenValue);
        SetInstrumentBrokenValue("Guitar", KurtBrokenValue);
        SetInstrumentBrokenValue("Drums", AceBrokenValue);
        SetInstrumentBrokenValue("Bass", MJBrokenValue);
        SetInstrumentBrokenValue("Speakers", SpeakersBrokenValue);
    }

    public void SetInstrumentVolume(string instrumentName, float volume)
    {
        ConcertAudioInstance.setParameterByName(instrumentName + "Volume", volume);
    }

    public void SetInstrumentBrokenValue(string instrumentName, float brokenValue)
    {
        ConcertAudioInstance.setParameterByName(instrumentName + "BrokenValue", brokenValue);
    }

    public float GetInstrumentBrokenValue(string instrumentName)
    {
        ConcertAudioInstance.getParameterByName(instrumentName + "BrokenValue", out float value);
        return value;
    }

    public void PauseConcert()
    {
        if (ConcertAudioEmitterInstance != null)
        {ConcertAudioEmitterInstance.EventInstance.setPaused(true);}
    }
    
    public void ResumeConcert()
    {
        if (ConcertAudioEmitterInstance != null)
        {ConcertAudioEmitterInstance.EventInstance.setPaused(false);}
    }

    public void StopSounds()
    {
        if (ConcertAudioEmitterInstance != null)
        {
            ConcertAudioEmitterInstance.Stop();
            Destroy(ConcertAudioEmitterInstance.gameObject);
        }

        this.isPlaying = false;
    }

    string BandMemberToInstrument(BandRoleName bandName)
    {
        switch(bandName)
        {
            case BandRoleName.Haley:
                return "Voice";
            case BandRoleName.Kurt:
                return "Guitar";
            case BandRoleName.Ace:
                return "Drums";
            case BandRoleName.MJ:
                return "Bass";
            case BandRoleName.Speakers:
                return "Speakers";
            default:
                Debug.Log("Trying to affect band member not on list: " + bandName);
                return "";
        }
    }

    void StartConcertAudio()
    {
        
        if (ConcertAudioEmitterPrefab == null)
        {return;}

        if(StateManager.Instance.CurrentState.Song == null)
        {return;}
        ConcertAudioPath = StateManager.Instance.CurrentState.Song.FMODMultiTrack.PrimaryTrackPath;


        if (!string.IsNullOrEmpty(ConcertAudioPath) && DoesEventExist(ConcertAudioPath))
        {
            ConcertAudioEmitterInstance = Instantiate(ConcertAudioEmitterPrefab, transform);
            ConcertAudioEmitterInstance.EventReference = RuntimeManager.PathToEventReference(ConcertAudioPath);
            ConcertAudioEmitterInstance.Play();
            ConcertAudioInstance = ConcertAudioEmitterInstance.EventInstance;
            this.isPlaying = true;
            //ConcertAudioEvent.PlayingAudio(this.bandName);
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

    public List<BandRoleName> AvailableConcertMembers()
    {
        List<BandRoleName> availableMembers = new List<BandRoleName>();

        if (AceAvailable) availableMembers.Add(BandRoleName.Ace);
        if (MJAvailable) availableMembers.Add(BandRoleName.MJ);
        if (HaleyAvailable) availableMembers.Add(BandRoleName.Haley);
        if (KurtAvailable) availableMembers.Add(BandRoleName.Kurt);
        if (SpeakersAvailable) availableMembers.Add(BandRoleName.Speakers);
        if (HarveyAvailable) availableMembers.Add(BandRoleName.Harvey);

        return availableMembers;
    }


    void SubscribeEvents()
    {
        TimeEvents.OnGamePaused += PauseConcert;    
        TimeEvents.OnGameResumed += ResumeConcert;  
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;

        ConcertAudioEvent.OnAudioBroken += AudioBroken;
        ConcertAudioEvent.OnAudioFixed += AudioFixed;

        ConcertAudioEvent.OnConcertEnd += ConcertEnd;

    }

    void UnsubscribeEvents()
    {
        TimeEvents.OnGamePaused -= PauseConcert; 
        TimeEvents.OnGameResumed -= ResumeConcert; 
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;

        ConcertAudioEvent.OnAudioBroken -= AudioBroken;
        ConcertAudioEvent.OnAudioFixed -= AudioFixed;
        
        ConcertAudioEvent.OnConcertEnd -= ConcertEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StartConcertAudio();
                break;
            case StateType.Intermission:
                break;
            default:
                break;
        }
    }
    
    public void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StopSounds();
                break;
            case StateType.Intermission:
                break;
            default:
                break;
        }
    }

    public void AudioBroken(object sender, ConcertAudioEventArgs e)
    {
        if(e.AffectInstrument)
        {
            string instrumentName = BandMemberToInstrument(e.ConcertPosition);
            UpdateBrokenValue(instrumentName, e.BrokenValue);
            SetInstrumentBrokenValue(instrumentName, GetBrokenValue(instrumentName));
        }
        else
        {

        }
    }

    public void AudioFixed(object sender, ConcertAudioEventArgs e)
    {
        if(e.AffectInstrument)
        {
            string instrumentName = BandMemberToInstrument(e.ConcertPosition);
            UpdateBrokenValue(instrumentName, -e.BrokenValue); // Assuming negative value fixes the instrument
            SetInstrumentBrokenValue(instrumentName, GetBrokenValue(instrumentName));
        }
        else
        {

        }
    }

    public void ConcertEnd(object sender, ConcertAudioEventArgs e)
    {
        this.StopSounds();
    }

    private void CheckAndPrintTrackStatus()
    {
        foreach (var track in logicTracks)
        {
            ConcertAudioInstance.getParameterByName(track + "Volume", out float volume);
            if (volume > 0)
            {
                Debug.Log(track + " is playing.");
            }
            else
            {
                Debug.Log(track + " is not playing.");
            }
        }
    }

    private void UpdateBrokenValue(string instrumentName, float delta)
    {
        switch(instrumentName)
        {
            case "Voice":
                HaleyBrokenValue = Mathf.Clamp(HaleyBrokenValue + delta, 0, 5);
                break;
            case "Guitar":
                KurtBrokenValue = Mathf.Clamp(KurtBrokenValue + delta, 0, 5);
                break;
            case "Drums":
                AceBrokenValue = Mathf.Clamp(AceBrokenValue + delta, 0, 5);
                break;
            case "Bass":
                MJBrokenValue = Mathf.Clamp(MJBrokenValue + delta, 0, 5);
                break;
            case "Speakers":
                SpeakersBrokenValue = Mathf.Clamp(SpeakersBrokenValue + delta, 0, 5);
                break;
        }
    }

    private float GetBrokenValue(string instrumentName)
    {
        switch(instrumentName)
        {
            case "Voice": return HaleyBrokenValue;
            case "Guitar": return KurtBrokenValue;
            case "Drums": return AceBrokenValue;
            case "Bass": return MJBrokenValue;
            case "Speakers": return SpeakersBrokenValue;
            default: return 0;
        }
    }
}
