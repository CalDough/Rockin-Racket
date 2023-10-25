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
        role.CurrentRole = newRoleType;
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
            if (track.PrimaryRole != Attribute.Vocal)
            {
                // If the primary role is not Vocal, add it to the list
                instrumentList.Add(track.PrimaryRole);
            }
            else if (track.SecondaryRole != Attribute.Vocal)
            {
                // If the primary role is Vocal but the secondary role is not, add the secondary role to the list
                instrumentList.Add(track.SecondaryRole);
            }
            else
            {
                // If both roles are Vocal, add Vocal to the list
                instrumentList.Add(Attribute.Vocal);
            }
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
        Debug.Log("Loading default band");
        Manager = new BandPosition(); 
        Manager.CurrentRole = Attribute.Management;
        Manager.IsActivePosition = true;
        Manager.Skills.Add(new Skill(Attribute.Brass, 25));
        Manager.Skills.Add(new Skill(Attribute.Keyboard, 25));
        Manager.Skills.Add(new Skill(Attribute.Management, 50));
        Manager.Skills.Add(new Skill(Attribute.Percussion, 25));
        Manager.Skills.Add(new Skill(Attribute.Repair, 25));
        Manager.Skills.Add(new Skill(Attribute.Strings, 25));
        Manager.Skills.Add(new Skill(Attribute.Vocal, 25));
        Manager.Skills.Add(new Skill(Attribute.Woodwind, 25));

        AnimalOne = new BandPosition(); // Upfront, usually Vocalist
        AnimalOne.CurrentRole = Attribute.Vocal;
        AnimalOne.IsActivePosition = true;
        AnimalOne.Skills.Add(new Skill(Attribute.Brass, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Keyboard, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Management, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Percussion, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Repair, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Strings, 25));
        AnimalOne.Skills.Add(new Skill(Attribute.Vocal, 50));
        AnimalOne.Skills.Add(new Skill(Attribute.Woodwind, 25));

        AnimalTwo = new BandPosition();  // Right Side
        AnimalTwo.CurrentRole = Attribute.Strings;
        AnimalTwo.IsActivePosition = true;
        AnimalTwo.Skills.Add(new Skill(Attribute.Brass, 25));
        AnimalTwo.Skills.Add(new Skill(Attribute.Keyboard, 45));
        AnimalTwo.Skills.Add(new Skill(Attribute.Management, 25));
        AnimalTwo.Skills.Add(new Skill(Attribute.Percussion, 25));
        AnimalTwo.Skills.Add(new Skill(Attribute.Repair, 25));
        AnimalTwo.Skills.Add(new Skill(Attribute.Strings, 50));
        AnimalTwo.Skills.Add(new Skill(Attribute.Vocal, 25));
        AnimalTwo.Skills.Add(new Skill(Attribute.Woodwind, 25));

        AnimalThree = new BandPosition(); // Left Side
        AnimalThree.CurrentRole = Attribute.Strings;
        AnimalThree.IsActivePosition = true;
        AnimalThree.Skills.Add(new Skill(Attribute.Brass, 25));
        AnimalThree.Skills.Add(new Skill(Attribute.Keyboard, 50));
        AnimalThree.Skills.Add(new Skill(Attribute.Management, 25));
        AnimalThree.Skills.Add(new Skill(Attribute.Percussion, 25));
        AnimalThree.Skills.Add(new Skill(Attribute.Repair, 25));
        AnimalThree.Skills.Add(new Skill(Attribute.Strings, 45));
        AnimalThree.Skills.Add(new Skill(Attribute.Vocal, 25));
        AnimalThree.Skills.Add(new Skill(Attribute.Woodwind, 25));
        
        AnimalFour = new BandPosition(); 
        AnimalFour.CurrentRole = Attribute.Percussion;
        AnimalFour.IsActivePosition = true;
        AnimalFour.Skills.Add(new Skill(Attribute.Brass, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Keyboard, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Management, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Percussion, 50));
        AnimalFour.Skills.Add(new Skill(Attribute.Repair, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Strings, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Vocal, 25));
        AnimalFour.Skills.Add(new Skill(Attribute.Woodwind, 25));




    }


}

[System.Serializable]
public class BandPosition 
{
    public BandPosition()
    {
        CurrentRole = Attribute.Management;
    }

    public bool IsActivePosition = true;  
    public Attribute CurrentRole;     
    public List<Skill> Skills = new List<Skill>(); 
}
