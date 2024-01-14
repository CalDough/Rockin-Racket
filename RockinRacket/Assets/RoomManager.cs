using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> roomList; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
