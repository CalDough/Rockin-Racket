using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RoomManager : MonoBehaviour
{
    private readonly string filePath = "Player/SaveFiles/";
    private readonly string fileName = "Dialogue.json";

    [SerializeField] private List<GameObject> roomList; 
    private GameManager gameManager;

    public int currentHub = 0;
    
    // Hub 1 Dialogue Interacted Booleans
    private bool hub0_Ace_Interacted = false;
    private bool hub0_MJ_Interacted = false;
    private bool hub0_Kurt_Interacted = false;
    private bool hub0_Haley_Interacted = false;

    // Hub 1 Dialogue Interacted Booleans
    private bool hub1_Ace_Interacted = false;
    private bool hub1_MJ_Interacted = false;
    private bool hub1_Kurt_Interacted = false;
    private bool hub1_Haley_Interacted = false;

    // Hub 2 Dialogue Interacted Booleans
    private bool hub2_Ace_Interacted = false;
    private bool hub2_MJ_Interacted = false;
    private bool hub2_Kurt_Interacted = false;
    private bool hub2_Haley_Interacted = false;

    // Hub 3 Dialogue Interacted Booleans
    private bool hub3_Ace_Interacted = false;
    private bool hub3_MJ_Interacted = false;
    private bool hub3_Kurt_Interacted = false;
    private bool hub3_Haley_Interacted = false;

    // Hub 4 Dialogue Interacted Booleans
    private bool hub4_Ace_Interacted = false;
    private bool hub4_MJ_Interacted = false;
    private bool hub4_Kurt_Interacted = false;
    private bool hub4_Haley_Interacted = false;

    private static RoomManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            instance.LoadDialogue();
            instance.StartingRoom();
        }
        else
        {
            Debug.Log("2 instances of room manager attempted to exist, destroying the new one");
            Destroy(this);
        }
    }

    void Start()
    {
        Debug.Log("Current Hub: " + currentHub);
    }

    private void SetHubNumber()
    {
        gameManager = GameManager.Instance;

        if (gameManager.CompletedLevelOne)
        {
            currentHub = 1;
        }
        else
        {
            currentHub = 0;
        }
        if (gameManager.CompletedLevelTwo)
        {
            currentHub = 2;
        }
        if (gameManager.CompletedLevelThree)
        {
            currentHub = 3;
        }
        
        if (gameManager.CompletedLevelFour)
        {
            currentHub = 4;
        }
    }

    private void StartingRoom()
    {
        foreach (GameObject room in roomList)
        {
            room.SetActive(false);
        }
        GoToRoom("Garage");
    }

    public void GoToRoom(String roomName)
    {
        if (roomName != "")
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                GameObject currentRoom = roomList[i];
                if (currentRoom.name == roomName)
                {
                    currentRoom.SetActive(true);
                }
                else
                {
                    currentRoom.SetActive(false);
                }
            }
        }
    }

    public bool CheckIfInteracted(int hub, string character)
    {
        SetHubNumber();

        switch (hub)
        {
            case 0:
                switch(character)
                {
                    case "Ace":
                        return hub0_Ace_Interacted;
                    case "Haley":
                        return hub0_Haley_Interacted;
                    case "Kurt":
                        return hub0_Kurt_Interacted;
                    case "MJ":
                        return hub0_MJ_Interacted;
                    default:
                        Debug.Log("You have entered an invalid character, bool is automatically false");
                        return false;
                }
            case 1:
                switch(character)
                {
                    case "Ace":
                        return hub1_Ace_Interacted;
                    case "Haley":
                        return hub1_Haley_Interacted;
                    case "Kurt":
                        return hub1_Kurt_Interacted;
                    case "MJ":
                        return hub1_MJ_Interacted;
                    default:
                        Debug.Log("You have entered an invalid character, bool is automatically false");
                        return false;
                }
            case 2:
                switch(character)
                {
                    case "Ace":
                        return hub2_Ace_Interacted;
                    case "Haley":
                        return hub2_Haley_Interacted;
                    case "Kurt":
                        return hub2_Kurt_Interacted;
                    case "MJ":
                        return hub2_MJ_Interacted;
                    default:
                        Debug.Log("You have entered an invalid character, bool is automatically false");
                        return false;
                }
            case 3:
                switch(character)
                {
                    case "Ace":
                        return hub3_Ace_Interacted;
                    case "Haley":
                        return hub3_Haley_Interacted;
                    case "Kurt":
                        return hub3_Kurt_Interacted;
                    case "MJ":
                        return hub3_MJ_Interacted;
                    default:
                        Debug.Log("You have entered an invalid character, bool is automatically false");
                        return false;
                }
            case 4:
                switch(character)
                {
                    case "Ace":
                        return hub4_Ace_Interacted;
                    case "Haley":
                        return hub4_Haley_Interacted;
                    case "Kurt":
                        return hub4_Kurt_Interacted;
                    case "MJ":
                        return hub4_MJ_Interacted;
                    default:
                        Debug.Log("You have entered an invalid character, bool is automatically false");
                        return false;
                }
            default:
                Debug.Log("You have entered an invalid hub, bool is automatically false");
                return false;
        }
    }

    public void SetInteraction(int hub, string character)
    {
        switch (hub)
        {
            case 0:
                switch(character)
                {
                    case "Ace":
                        hub0_Ace_Interacted = true;
                        break;
                    case "Haley":
                        hub0_Haley_Interacted = true;
                        break;
                    case "Kurt":
                        hub0_Kurt_Interacted = true;
                        break;
                    case "MJ":
                        hub0_MJ_Interacted = true;
                        break;
                    default:
                        Debug.Log("You have entered an invalid character, no value updated");
                        break;
                }
                break;
            case 1:
                switch(character)
                {
                    case "Ace":
                        hub1_Ace_Interacted = true;
                        break;
                    case "Haley":
                        hub1_Haley_Interacted = true;
                        break;
                    case "Kurt":
                        hub1_Kurt_Interacted = true;
                        break;
                    case "MJ":
                        hub1_MJ_Interacted = true;
                        break;
                    default:
                        Debug.Log("You have entered an invalid character, no value updated");
                        break;
                }
                break;
            case 2:
                switch(character)
                {
                    case "Ace":
                        hub2_Ace_Interacted = true;
                        break;
                    case "Haley":
                        hub2_Haley_Interacted = true;
                        break;
                    case "Kurt":
                        hub2_Kurt_Interacted = true;
                        break;
                    case "MJ":
                        hub2_MJ_Interacted = true;
                        break;
                    default:
                        Debug.Log("You have entered an invalid character, no value updated");
                        break;
                }
                break;
            case 3:
                switch(character)
                {
                    case "Ace":
                        hub3_Ace_Interacted = true;
                        break;
                    case "Haley":
                        hub3_Haley_Interacted = true;
                        break;
                    case "Kurt":
                        hub3_Kurt_Interacted = true;
                        break;
                    case "MJ":
                        hub3_MJ_Interacted = true;
                        break;
                    default:
                        Debug.Log("You have entered an invalid character, no value updated");
                        break;
                }
                break;
            case 4:
                switch(character)
                {
                    case "Ace":
                        hub4_Ace_Interacted = true;
                        break;
                    case "Haley":
                        hub4_Haley_Interacted = true;
                        break;
                    case "Kurt":
                        hub4_Kurt_Interacted = true;
                        break;
                    case "MJ":
                        hub4_MJ_Interacted = true;
                        break;
                    default:
                        Debug.Log("You have entered an invalid character, no value updated");
                        break;
                }
                break;
            default:
                Debug.Log("You have entered an invalid hub, no value updated");
                break;
        }
        SaveDialogue();
    }

    public static RoomManager GetInstance()
    {
        return instance;
    }

    //
    // -- SAVING AND LOADING BELOW --
    //

    public void SaveDialogue()
    {
        Directory.CreateDirectory(filePath);

        var dialogueData = new DialogueData
        {
            hub0_Ace_Interacted = hub0_Ace_Interacted,
            hub0_MJ_Interacted = hub0_MJ_Interacted,
            hub0_Kurt_Interacted = hub0_Kurt_Interacted,
            hub0_Haley_Interacted = hub0_Haley_Interacted,

            hub1_Ace_Interacted = hub1_Ace_Interacted,
            hub1_MJ_Interacted = hub1_MJ_Interacted,
            hub1_Kurt_Interacted = hub1_Kurt_Interacted,
            hub1_Haley_Interacted = hub1_Haley_Interacted,

            hub2_Ace_Interacted = hub2_Ace_Interacted,
            hub2_MJ_Interacted = hub2_MJ_Interacted,
            hub2_Kurt_Interacted = hub2_Kurt_Interacted,
            hub2_Haley_Interacted = hub2_Haley_Interacted,

            hub3_Ace_Interacted = hub3_Ace_Interacted,
            hub3_MJ_Interacted = hub3_MJ_Interacted,
            hub3_Kurt_Interacted = hub3_Kurt_Interacted,
            hub3_Haley_Interacted = hub3_Haley_Interacted,

            hub4_Ace_Interacted = hub4_Ace_Interacted,
            hub4_MJ_Interacted = hub4_MJ_Interacted,
            hub4_Kurt_Interacted = hub4_Kurt_Interacted,
            hub4_Haley_Interacted = hub4_Haley_Interacted
        };

        string jsonData = JsonUtility.ToJson(dialogueData, prettyPrint: true);

        File.WriteAllText(filePath + fileName, jsonData);
    }

    public void LoadDialogue()
    {
        string filePathAndName = filePath + fileName;
        if (File.Exists(filePathAndName))
        {
            string jsonData = File.ReadAllText(filePath);
            DialogueData loadedData = JsonUtility.FromJson<DialogueData>(jsonData);

            hub0_Ace_Interacted = loadedData.hub0_Ace_Interacted;
            hub0_MJ_Interacted = loadedData.hub0_MJ_Interacted;
            hub0_Kurt_Interacted = loadedData.hub0_Kurt_Interacted;
            hub0_Haley_Interacted = loadedData.hub0_Haley_Interacted;


            hub1_Ace_Interacted = loadedData.hub1_Ace_Interacted;
            hub1_MJ_Interacted = loadedData.hub1_MJ_Interacted;
            hub1_Kurt_Interacted = loadedData.hub1_Kurt_Interacted;
            hub1_Haley_Interacted = loadedData.hub1_Haley_Interacted;

            hub2_Ace_Interacted = loadedData.hub2_Ace_Interacted;
            hub2_MJ_Interacted = loadedData.hub2_MJ_Interacted;
            hub2_Kurt_Interacted = loadedData.hub2_Kurt_Interacted;
            hub2_Haley_Interacted = loadedData.hub2_Haley_Interacted;

            hub3_Ace_Interacted = loadedData.hub3_Ace_Interacted;
            hub3_MJ_Interacted = loadedData.hub3_MJ_Interacted;
            hub3_Kurt_Interacted = loadedData.hub3_Kurt_Interacted;
            hub3_Haley_Interacted = loadedData.hub3_Haley_Interacted;

            hub4_Ace_Interacted = loadedData.hub4_Ace_Interacted;
            hub4_MJ_Interacted = loadedData.hub4_MJ_Interacted;
            hub4_Kurt_Interacted = loadedData.hub4_Kurt_Interacted;
            hub4_Haley_Interacted = loadedData.hub4_Haley_Interacted;

            Debug.Log("Dialogue successfully loaded");
        }
        else
        {
            ResetDialogue();
            Debug.Log("No saved dialogue data found. Loading default dialogueData data.");
        }
    }

    public void ResetDialogue()
    {
        hub0_Ace_Interacted = false;
        hub0_MJ_Interacted = false;
        hub0_Kurt_Interacted = false;
        hub0_Haley_Interacted = false;
        
        hub1_Ace_Interacted = false;
        hub1_MJ_Interacted = false;
        hub1_Kurt_Interacted = false;
        hub1_Haley_Interacted = false;

        hub2_Ace_Interacted = false;
        hub2_MJ_Interacted = false;
        hub2_Kurt_Interacted = false;
        hub2_Haley_Interacted = false;

        hub3_Ace_Interacted = false;
        hub3_MJ_Interacted = false;
        hub3_Kurt_Interacted = false;
        hub3_Haley_Interacted = false;

        hub4_Ace_Interacted = false;
        hub4_MJ_Interacted = false;
        hub4_Kurt_Interacted = false;
        hub4_Haley_Interacted = false;
    }

    // used to save conversations
    private class DialogueData
    {
        // Hub 2 Dialogue Interacted Booleans
        public bool hub0_Ace_Interacted = false;
        public bool hub0_MJ_Interacted = false;
        public bool hub0_Kurt_Interacted = false;
        public bool hub0_Haley_Interacted = false;

        // Hub 1 Dialogue Interacted Booleans
        public bool hub1_Ace_Interacted = false;
        public bool hub1_MJ_Interacted = false;
        public bool hub1_Kurt_Interacted = false;
        public bool hub1_Haley_Interacted = false;

        // Hub 2 Dialogue Interacted Booleans
        public bool hub2_Ace_Interacted = false;
        public bool hub2_MJ_Interacted = false;
        public bool hub2_Kurt_Interacted = false;
        public bool hub2_Haley_Interacted = false;

        // Hub 3 Dialogue Interacted Booleans
        public bool hub3_Ace_Interacted = false;
        public bool hub3_MJ_Interacted = false;
        public bool hub3_Kurt_Interacted = false;
        public bool hub3_Haley_Interacted = false;

        // Hub 4 Dialogue Interacted Booleans
        public bool hub4_Ace_Interacted = false;
        public bool hub4_MJ_Interacted = false;
        public bool hub4_Kurt_Interacted = false;
        public bool hub4_Haley_Interacted = false;
    }
}