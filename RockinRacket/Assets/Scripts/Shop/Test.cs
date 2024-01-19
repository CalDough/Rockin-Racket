using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour, IPointerDownHandler
{
    public GameLoadHandler gameLoadHandler;

    public void Save()
    {
        gameLoadHandler.Save();
    }
    public void Load()
    {
        gameLoadHandler.Load();
    }
    public void NewGame()
    {
        gameLoadHandler.NewGame();
    }
    public void AddMoney()
    {
        GameManager.Instance.globalMoney += 100;
    }
    public void LoadDialogue()
    {
        (string, int) result = DialogueLoader.GetDialogueFile("Ace");
        Debug.Log("Successfully found file: " + result.Item1);
        Debug.Log("Hub: " + result.Item2);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HIT!");
    }

    public void getEquipped()
    {
        print("MJ: " + ItemInventory.GetBandmateEquippedItem(Bandmate.MJ).name);
        print("Kurt: " + ItemInventory.GetBandmateEquippedItem(Bandmate.Kurt).name);
        print("Ace: " + ItemInventory.GetBandmateEquippedItem(Bandmate.Ace).name);
        print("Haley: " + ItemInventory.GetBandmateEquippedItem(Bandmate.Haley).name);
    }
}
