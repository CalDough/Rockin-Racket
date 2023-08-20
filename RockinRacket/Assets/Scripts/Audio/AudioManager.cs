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
    public SongData songData;

    [SerializeField] List<StudioEventEmitter> ConcertSounds = new List<StudioEventEmitter>();
    [SerializeField] List<GameObject> SoundObjects = new List<GameObject>();
    
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

        TimeEvents.OnGamePaused += PauseConcert;    
        TimeEvents.OnGameResumed += ResumeConcert;  
    }

    void OnDestroy()
    {
        TimeEvents.OnGamePaused -= PauseConcert; 
        TimeEvents.OnGameResumed -= ResumeConcert; 
    }
    
    public void CleanUp()
    {
        StopConcert();
        ConcertSounds.Clear();
        SoundObjects.Clear();
    }

    public void CreateSoundObjects()
    {
        int numBandPositions = 5;
        int numRoles = 2;  // Primary and Secondary

        // Check if SoundObjects list has the right number of objects
        if (SoundObjects.Count != numBandPositions * numRoles)
        {
            // Clear the lists if not of the right length
            foreach(GameObject go in SoundObjects)
            {
                Destroy(go);
            }
            SoundObjects.Clear();
            ConcertSounds.Clear();

            // Find or create the AudioSources parent object
            GameObject audioSourcesParent = GameObject.Find("AudioSources");
            if (audioSourcesParent == null)
            {
                audioSourcesParent = new GameObject("AudioSources");
            }

            for (int i = 0; i < numBandPositions; i++)
            {
                for (int j = 0; j < numRoles; j++)
                {
                    // Create a new GameObject for this track
                    GameObject newGO = new GameObject("FMOD_SoundObject_" + (i + 1) + (j == 0 ? "_Primary" : "_Secondary"));

                    // Set its parent to the AudioSources parent
                    newGO.transform.parent = audioSourcesParent.transform;

                    // Add the FMOD Studio Event Emitter component to the GameObject
                    StudioEventEmitter eventEmitter = newGO.AddComponent<StudioEventEmitter>();
                    ConcertSounds.Add(eventEmitter);

                    newGO.SetActive(false);
                    SoundObjects.Add(newGO);
                }
            }
        }
        else
        {
            // If the list has the right length ensure all objects are disabled and event emitters' EventReferences are null
            for (int i = 0; i < numBandPositions * numRoles; i++)
            {
                SoundObjects[i].SetActive(false);
                ConcertSounds[i].EventReference.Path = null;
            }
        }
    }

    public void EnableSoundObjectsBasedOnBandPositions(SongData songData)
    {
        BandManager bandManager = BandManager.Instance;
        // Get the band positions
        List<BandPosition> bandPositions = new List<BandPosition>
        {
            bandManager.AnimalOne,
            bandManager.AnimalTwo,
            bandManager.AnimalThree,
            bandManager.AnimalFour
        };

        // Get the corresponding track data
        List<TrackData> trackDataList = new List<TrackData>
        {
            songData.PositionOne,
            songData.PositionTwo,
            songData.PositionThree,
            songData.PositionFour
        };

        // Loop over the band positions
        for (int i = 0; i < bandPositions.Count; i++)
        {
        // Get the GameObjects corresponding to this band position
        GameObject primaryGO = SoundObjects[2 * i];
        GameObject secondaryGO = SoundObjects[2 * i + 1];

        // Check if the band position is not empty
        if (bandPositions[i].IsActivePosition == true)
        {
            // Check if the track paths are not null or empty before setting the EventReference
            if (!string.IsNullOrEmpty(trackDataList[i].PrimaryTrackPath))
            {
                primaryGO.SetActive(true);
                primaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(trackDataList[i].PrimaryTrackPath);
            }
            else
            {
                primaryGO.SetActive(false);
            }

            if (!string.IsNullOrEmpty(trackDataList[i].SecondaryTrackPath))
            {
                secondaryGO.SetActive(true);
                secondaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(trackDataList[i].SecondaryTrackPath);
            }
            else
            {
                secondaryGO.SetActive(false);
            }
        }
        else
        {
            // Disable the GameObjects since they are not playing sounds
            primaryGO.SetActive(false);
            secondaryGO.SetActive(false);
        }
        }

        // Handle the background sound separately
        GameObject backgroundPrimaryGO = SoundObjects[8];
        GameObject backgroundSecondaryGO = SoundObjects[9];

        if (!string.IsNullOrEmpty(songData.Background.PrimaryTrackPath))
        {
            backgroundPrimaryGO.SetActive(true);
            backgroundPrimaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(songData.Background.PrimaryTrackPath);
        }
        else
        {backgroundPrimaryGO.SetActive(false);}

        if (!string.IsNullOrEmpty(songData.Background.SecondaryTrackPath))
        {
            backgroundSecondaryGO.SetActive(true);
            backgroundSecondaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(songData.Background.SecondaryTrackPath);
        }
        else
        {backgroundSecondaryGO.SetActive(false);}
    }

    public void EnableSoundObjects()
    {

        // Get the corresponding track data
        List<TrackData> trackDataList = new List<TrackData>
        {
            songData.PositionOne,
            songData.PositionTwo,
            songData.PositionThree,
            songData.PositionFour
        };

        // Loop over the band positions
        for (int i = 0; i < 4; i++)
        {
            // Get the GameObjects corresponding to this band position
            GameObject primaryGO = SoundObjects[2 * i];
            GameObject secondaryGO = SoundObjects[2 * i + 1];

            // Check if the band position is not empty
            
            
                // Check if the track paths are not null or empty before setting the EventReference
                if (!string.IsNullOrEmpty(trackDataList[i].PrimaryTrackPath))
                {
                    primaryGO.SetActive(true);
                    primaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(trackDataList[i].PrimaryTrackPath);
                }
                else
                {
                    primaryGO.SetActive(false);
                }

                if (!string.IsNullOrEmpty(trackDataList[i].SecondaryTrackPath))
                {
                    secondaryGO.SetActive(true);
                    secondaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(trackDataList[i].SecondaryTrackPath);
                }
                else
                {
                    secondaryGO.SetActive(false);
                }
        }

        // Handle the background sound separately
        GameObject backgroundPrimaryGO = SoundObjects[8];
        GameObject backgroundSecondaryGO = SoundObjects[9];

        if (!string.IsNullOrEmpty(songData.Background.PrimaryTrackPath))
        {
            backgroundPrimaryGO.SetActive(true);
            backgroundPrimaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(songData.Background.PrimaryTrackPath);
        }
        else
        {
            backgroundPrimaryGO.SetActive(false);
        }

        if (!string.IsNullOrEmpty(songData.Background.SecondaryTrackPath))
        {
            backgroundSecondaryGO.SetActive(true);
            backgroundSecondaryGO.GetComponent<StudioEventEmitter>().EventReference = FMODUnity.EventReference.Find(songData.Background.SecondaryTrackPath);
        }
        else
        {
            backgroundSecondaryGO.SetActive(false);
        }
    }

    public void StartConcert()
    {
        if(GameStateManager.Instance.CurrentGameState.Song != null)
        {
            EnableSoundObjectsBasedOnBandPositions(GameStateManager.Instance.CurrentGameState.Song);
        }
        foreach(StudioEventEmitter emitter in this.ConcertSounds)
        {
            emitter.Play();
        }
    }

    public void StopConcert()
    {
        foreach(StudioEventEmitter emitter in this.ConcertSounds)
        {
            emitter.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
    
    public void PauseConcert()
    {
        foreach(StudioEventEmitter emitter in this.ConcertSounds)
        {
            emitter.EventInstance.setPaused(true);
        }
    }

    public void ResumeConcert()
    {
        foreach(StudioEventEmitter emitter in this.ConcertSounds)
        {
            emitter.EventInstance.setPaused(false);
        }
    }

    



}
