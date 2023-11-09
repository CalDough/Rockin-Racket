using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
/*
    This script is a singleton which manages the raccoon band
    It has the 5 Band Positions which are set to each of the Raccoons
    Players will be able to view what instruments the Raccoons are good at and what equipment they have for each song
    If a song doesn't need all 5 raccoons, then a position can be disabled
    Current role of a position helps determine which instrument to display on scene

*/
public class BandManager : MonoBehaviour
{
    public BandPosition Manager = new BandPosition(); 
    public BandPosition AnimalOne = new BandPosition(); // Upfront, usually Vocalist
    public BandPosition AnimalTwo = new BandPosition();  // Right Side
    public BandPosition AnimalThree = new BandPosition(); // Left Side
    public BandPosition AnimalFour = new BandPosition(); //  In the Back, usually Drummer
    
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "BandData.json";

    public static BandManager Instance { get; private set; }

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

    public void SetPositionRole(BandPosition role, Attribute newRoleType)
    {
    }

    public List<Attribute> GetBandMemberInstruments(SongData songData)
    {
        // Define a new list to hold the results
        List<Attribute> instrumentList = new List<Attribute>();

        // Get the corresponding track data
        List<TrackData> trackDataList = new List<TrackData>
        {
            songData.Ace,
            songData.Haley,
            songData.Kurt,
            songData.MJ
        };

        // Loop over the track data
        foreach (TrackData track in trackDataList)
        {
        }

        // Return the final list of instruments
        return instrumentList;
    }

    //Function to handle showing the band with correct instruments for the current song State
    //

    public void SaveBand()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        var bandData = new BandData
        {
            Manager = this.Manager,
            AnimalOne = this.AnimalOne,
            AnimalTwo = this.AnimalTwo,
            AnimalThree = this.AnimalThree,
            AnimalFour = this.AnimalFour
        };

        string jsonData = JsonUtility.ToJson(bandData, prettyPrint: true);
        
        File.WriteAllText(saveFolderPath + saveFileName, jsonData);

        Debug.Log("Band saved to " + saveFolderPath + saveFileName);
    }

    public void LoadBand()
    {
        string filePath = Path.Combine(saveFolderPath, saveFileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            BandData loadedData = JsonUtility.FromJson<BandData>(jsonData);
            this.Manager = loadedData.Manager;
            this.AnimalOne = loadedData.AnimalOne;
            this.AnimalTwo = loadedData.AnimalTwo;
            this.AnimalThree = loadedData.AnimalThree;
            this.AnimalFour = loadedData.AnimalFour;

            Debug.Log("Band loaded from " + filePath);
        }
        else
        {
            Debug.Log("No saved band data found. Loading default band data.");
            LoadDefaultBand();
            // Here you can put your logic for initializing data when there's no saved file
        }
    }

    [System.Serializable]
    private class BandData
    {
        public BandPosition Manager;
        public BandPosition AnimalOne;
        public BandPosition AnimalTwo;
        public BandPosition AnimalThree;
        public BandPosition AnimalFour;
    }

    public void LoadDefaultBand()
    {


    }


}

[System.Serializable]
public class BandPosition 
{
    public bool IsActivePosition = true;  
}
