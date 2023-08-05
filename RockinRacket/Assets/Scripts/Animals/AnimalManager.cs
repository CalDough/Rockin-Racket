using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class AnimalManager : MonoBehaviour
{
    private string saveFolderPath = "Player/SaveFiles/";
    private string saveFileName = "AnimalData.json";

    public List<AnimalData> animalScriptables;
    public List<Animal> AllAnimals;
    public List<Animal> CarryOverAttendees;
    public List<Animal> PotentialAttendees;
    
    //Attendees for current concert
    public List<Animal> Attendees;


    public int ticketCost = 5;

    public static AnimalManager Instance { get; private set; }

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

    public void SaveAnimals()
    {
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        var allData = new AllAnimalData
        {
            allAnimals = AllAnimals,
            carryOverAttendees = CarryOverAttendees,
            potentialAttendees = PotentialAttendees,
        };

        string jsonData = JsonUtility.ToJson(allData, prettyPrint: true);
        
        File.WriteAllText(saveFolderPath + saveFileName, jsonData);

        Debug.Log("Animals saved to " + saveFolderPath + saveFileName);
    }

    public void LoadAnimals()
    {
        string filePath = Path.Combine(saveFolderPath, saveFileName);

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            AllAnimalData loadedData = JsonUtility.FromJson<AllAnimalData>(jsonData);
            AllAnimals = loadedData.allAnimals;
            CarryOverAttendees = loadedData.carryOverAttendees;
            PotentialAttendees = loadedData.potentialAttendees;
            Debug.Log("Data loaded from " + filePath);
        }
        else
        {
            Debug.Log("No saved animal data found. Loading default animal data.");
            AllAnimals = new List<Animal>();
            foreach (var animalData in animalScriptables)
            {
                var animal = new Animal();
                animal.Load(animalData);
                AllAnimals.Add(animal);
            }
        }
    }

    public void LoadNewAnimals()
    {
        
        Debug.Log("Loading default animal data.");
        AllAnimals = new List<Animal>();
        foreach (var animalData in animalScriptables)
        {
            var animal = new Animal();
            animal.Load(animalData);
            AllAnimals.Add(animal);
        }
        
    }

    public Animal FindAnimalByID(int id)
    {
        foreach (Animal animal in AllAnimals)
        {
            if (animal.ID == id)
            {
                return animal;
            }
        }
        Debug.LogWarning("No Animal with ID " + id + " was found.");
        return null;
    }

    public void InitializeAttendees()
    {
        Attendees.Clear();

        // Add setGuests from the ModSelectedVenue to the Attendees list
        foreach (int guestID in GameStateManager.Instance.SelectedVenue.setGuests)
        {
            Animal guest = FindAnimalByID(guestID);
            if (guest != null)
            {
                AnimalManager.Instance.Attendees.Add(guest);
            }
        }

        // Loop through CarryOverAttendees and add them to the Attendees list based on carryOverChance
        foreach (Animal carryOverAttendee in CarryOverAttendees)
        {
            int roll = UnityEngine.Random.Range(0, 100);
            if (roll <= carryOverAttendee.CarryOverChance)
            {
                Attendees.Add(carryOverAttendee);
            }
        }

        // Loop through AllAnimals and add them to the Attendees list based on interest
        foreach (Animal animal in AllAnimals)
        {
            if (animal.BandInterest >= 0) // Animal has positive interest
            {
                if (ticketCost <= animal.Frugality) // Price is within frugalness
                {
                    int roll = UnityEngine.Random.Range(0, 100);
                    if (roll <= animal.BandInterest)
                    {
                        Attendees.Add(animal);
                    }
                }
                else // Price is above frugalness
                {
                    int roll = UnityEngine.Random.Range(0, 100);
                    // Adjust the chance based on how much above frugalness the price is
                    // example: cost 100, frugalness 25, interest 100
                    // 100 *(75/100)
                    // =75, so we need a roll greater than 75 for the animal to ignore
                    // may want to cap ticket cost at >200 to 100
                    if (roll <= animal.BandInterest * (1.0f - ((ticketCost - animal.Frugality) / 100.0f)))
                    {
                        Attendees.Add(animal);
                    }
                }
            }
            else if (animal.BandInterest < 0 && ticketCost == 0) // Animal has negative interest, but will attend anyways 50% chance
            {
                int roll = UnityEngine.Random.Range(0, 100);
                if (roll <= 50)
                {
                    Attendees.Add(animal);
                }
            }
        } 
        //This function might be crazy and will have to check a variety of conditions for unlocking animals who might appear
        // might check for items owned, fame status, or whatever else for the attendees left in the potential attendees list
        
        CheckPotentialAttendees();
    }

       
   

    public void CheckPotentialAttendees()
    {

    }

    [System.Serializable]
    private class AllAnimalData
    {
        public List<Animal> allAnimals;
        public List<Animal> carryOverAttendees;
        public List<Animal> potentialAttendees;
    }
}
