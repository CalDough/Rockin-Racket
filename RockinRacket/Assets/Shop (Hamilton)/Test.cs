using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public void Save()
    {
        int hub = 1;
        int fame = 12;
        int money = 120;
        GameSaver.SaveStats(hub, fame, money);
        GameSaver.Save();
    }
    public void Load()
    {
        GameSaver.Load();
        GameSaver.PrintStats();
    }
    public void LoadDialogue()
    {
        string[] result = DialogueLoader.GetDialogueFile("Ace");
        Debug.Log("Successfully found file: " + result[0]);
        Debug.Log("Hub: " + result[1]);
    }
}
