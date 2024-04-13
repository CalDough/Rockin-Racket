using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
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
    private List<string> logicTracks = new List<string> { "Guitar", "Bass", "Drums", "Vocals" };

    [Header("Debug Variables")]
    public bool debugUpdateAudio = false; 
    [SerializeField] private float brokenValueReduction = .004f;

    [Header("Band Variables")] 
    public bool AceAvailable = true;    
    [Range(-0,5)]public float AceBrokenValue = 0;

    public bool MJAvailable = true;
    [Range(-0,5)]public float MJBrokenValue = 0;

    public bool HaleyAvailable = true;
    [Range(-0,5)]public float HaleyBrokenValue = 0;

    public bool KurtAvailable = true;
    [Range(-0,5)]public float KurtBrokenValue = 0;

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
            ConcertEvents.instance.e_SongStarted.AddListener(StartConcertAudio);
        }

    }

    void Start()
    {
        //SubscribeEvents();
        SceneManager.sceneUnloaded += OnSceneChange;
        TimeEvents.OnGamePaused += PauseConcert;
        TimeEvents.OnGameResumed += ResumeConcert;
        ConcertAudioEvent.OnAudioBroken += AudioBroken;
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        StopSounds();
    }

    private void OnSceneChange(Scene arg0)
    {
        StopSounds();
        Debug.Log("Stopping Audio");
    }
    
    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChange;
        //UnsubscribeEvents();
        TimeEvents.OnGamePaused -= PauseConcert;
        TimeEvents.OnGameResumed -= ResumeConcert;
        ConcertAudioEvent.OnAudioBroken -= AudioBroken;
    }

    void Update()
    {
        if (debugUpdateAudio)
        {
            UpdateFMODParameters();
        }
    }

    void FixedUpdate()
    {
        ReduceBrokenValues();
    }

    private void UpdateFMODParameters()
    {
        SetInstrumentBrokenValue("Vocals", HaleyBrokenValue);
        SetInstrumentBrokenValue("Guitar", KurtBrokenValue);
        SetInstrumentBrokenValue("Drums", AceBrokenValue);
        SetInstrumentBrokenValue("Bass", MJBrokenValue);
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
        ConcertAudioInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ConcertAudioInstance.release();
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
                return "Vocals";
            case BandRoleName.Kurt:
                return "Guitar";
            case BandRoleName.Ace:
                return "Drums";
            case BandRoleName.MJ:
                return "Bass";
            case BandRoleName.Harvey:
                return "Keyboard";
            default:
                Debug.Log("Trying to affect band member not on list: " + bandName);
                return "";
        }
    }

    void StartConcertAudio()
    {
        
        if (ConcertAudioEmitterPrefab == null)
        {return;}

        //if(StateManager.Instance.CurrentState.Song == null)
        //{return;}
        //ConcertAudioPath = StateManager.Instance.CurrentState.Song.FMODMultiTrack.PrimaryTrackPath;
        ConcertAudioPath = ConcertController.instance.currentSong.FMODMultiTrack.PrimaryTrackPath;


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
        if (HarveyAvailable) availableMembers.Add(BandRoleName.Harvey);

        return availableMembers;
    }


    public void AudioBroken(object sender, ConcertAudioEventArgs e)
    {
        string instrumentName = BandMemberToInstrument(e.ConcertPosition);
        UpdateBrokenValue(e.ConcertPosition, e.StressFactor);
        SetInstrumentBrokenValue(instrumentName, GetBrokenValue(instrumentName));
    }


    public void AudioFixed(object sender, ConcertAudioEventArgs e)
    {
        string instrumentName = BandMemberToInstrument(e.ConcertPosition);
        UpdateBrokenValue(e.ConcertPosition, -e.StressFactor); 
        SetInstrumentBrokenValue(instrumentName, GetBrokenValue(instrumentName));
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

    private void UpdateBrokenValue(BandRoleName concertPosition, float delta)
    {
        switch(concertPosition)
        {
            case BandRoleName.Haley:
                HaleyBrokenValue = Mathf.Clamp(HaleyBrokenValue + delta, 0, 5);
                break;
            case BandRoleName.Kurt:
                KurtBrokenValue = Mathf.Clamp(KurtBrokenValue + delta, 0, 5);
                break;
            case BandRoleName.Ace:
                AceBrokenValue = Mathf.Clamp(AceBrokenValue + delta, 0, 5);
                break;
            case BandRoleName.MJ:
                MJBrokenValue = Mathf.Clamp(MJBrokenValue + delta, 0, 5);
                break;
            case BandRoleName.Harvey:
                HarveyBrokenValue = Mathf.Clamp(HarveyBrokenValue + delta, 0, 5);
                break;
            default:
                break;
        }
    }

    private float GetBrokenValue(string instrumentName)
    {
        switch(instrumentName)
        {
            case "Vocals": return HaleyBrokenValue;
            case "Guitar": return KurtBrokenValue;
            case "Drums": return AceBrokenValue;
            case "Bass": return MJBrokenValue;
            default: return 0;
        }
    }

    private void ReduceBrokenValues()
    {
        if (AceAvailable && AceBrokenValue > 0)
        {
            AceBrokenValue = Mathf.Max(0, AceBrokenValue - brokenValueReduction);
            SetInstrumentBrokenValue("Drums", AceBrokenValue);
        }

        if (MJAvailable && MJBrokenValue > 0)
        {
            MJBrokenValue = Mathf.Max(0, MJBrokenValue - brokenValueReduction);
            SetInstrumentBrokenValue("Bass", MJBrokenValue);
        }

        if (HaleyAvailable && HaleyBrokenValue > 0)
        {
            HaleyBrokenValue = Mathf.Max(0, HaleyBrokenValue - brokenValueReduction);
            SetInstrumentBrokenValue("Vocals", HaleyBrokenValue);
        }

        if (KurtAvailable && KurtBrokenValue > 0)
        {
            KurtBrokenValue = Mathf.Max(0, KurtBrokenValue - brokenValueReduction);
            SetInstrumentBrokenValue("Guitar", KurtBrokenValue);
        }

        if (HarveyAvailable && HarveyBrokenValue > 0)
        {
            HarveyBrokenValue = Mathf.Max(0, HarveyBrokenValue - brokenValueReduction);
            //SetInstrumentBrokenValue("HarveyInstrument", HarveyBrokenValue);
        }
    }
}
