using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomList; 

    public int currentHub = 1;

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

    public static RoomManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("2 instances of room manager attempted to exist, destroying the new one");
            Destroy(this);
        }
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
        switch (hub)
        {
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

    public void setInteraction(int hub, string character)
    {
        switch (hub)
        {
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
    }

    public static RoomManager getInstance()
    {
        return instance;
    }
}
