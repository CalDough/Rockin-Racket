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
        (string, int) result = DialogueLoader.GetDialogueFile("Ace");
        Debug.Log("Successfully found file: " + result.Item1);
        Debug.Log("Hub: " + result.Item2);
    }
}
