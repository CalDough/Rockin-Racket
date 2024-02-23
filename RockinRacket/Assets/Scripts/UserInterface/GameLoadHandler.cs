using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
/*
    This script is a test UI script the Pause Menu and Main Menu
*/
public class GameLoadHandler : MonoBehaviour
{
    //public PauseManager pauseManager;

    private int currentSceneIndex;
    //private static Stack<int> sceneIndexHistory = new();

    private void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    public void NewGame()
    {
        //AnimalManager.Instance.LoadNewAnimals();
        //BandManager.Instance.LoadDefaultBand();
        //InventoryManager.Instance.LoadDefaultItems();
        GameManager.Instance.NewGame();
        ItemInventory.ResetInventory();
        StickerSaver.Reset();
    }

    public void SaveAndExit()
    {
        GameManager.Instance.SaveGame();
        ItemInventory.Save();
        StickerSaver.SaveStickerData();
        Application.Quit();
    }
    public void Save()
    {
        GameManager.Instance.SaveGame();
        ItemInventory.Save();
        StickerSaver.SaveStickerData();
    }

    public void Load()
    {
        GameManager.Instance.LoadGame();
        ItemInventory.Load();
        StickerSaver.LoadStickerData();
    }

    //public void GoBackScene()
    //{
    //    int nextScene = (int)SceneIndex.MainMenu;
    //    if (sceneIndexHistory.Count != 0)
    //        nextScene = sceneIndexHistory.Pop();
    //    if (currentSceneIndex != nextScene)
    //        SetScene(nextScene);
    //    else if (currentSceneIndex == (int)SceneIndex.MainMenu)
    //        SetScene((int)SceneIndex.StartMenu);
    //}

    // for TESTING
    public void RandomScene()
    {
        SwitchToScene(new System.Random().Next(1, 7));
    }

    public void SwitchToScene(int sceneIndex)
    {
        print("Switching from scene: " + currentSceneIndex + " to: " + sceneIndex);
        if (currentSceneIndex != sceneIndex)
        {
            if (sceneIndex != (int)SceneIndex.ConcertDefault)
            {
                print("current: " + currentSceneIndex);
                print("goal: " + sceneIndex);
                currentSceneIndex = sceneIndex;
                SetScene(currentSceneIndex);
            }
        }
        //Debug.Log("Current Index: " + currentSceneIndex + "  ||  " + "Next Index: " + sceneIndex + "  ||  " + "history size: " + sceneIndexHistory.Count.ToString());
    }

    public void OpenStartMenu() { SwitchToScene((int)SceneIndex.StartMenu); }
    public void OpenMainMenu() { SwitchToScene((int)SceneIndex.Garage); }
    public void OpenVenueSelection() { SwitchToScene((int)SceneIndex.VenueSelection); }
    public void OpenBandManagement() { SwitchToScene((int)SceneIndex.BandManagement); }
    public void OpenShop() { SwitchToScene((int)SceneIndex.Shop); }
    public void OpenSettings() { SwitchToScene((int)SceneIndex.SettingsMenu); }
    public void StartLevelOne() { SwitchToScene((int)SceneIndex.Cinematic1); }

    //private void PrintSceneIndexHistory()
    //{
    //    string print = "Current Index: " + currentSceneIndex;
    //    foreach (int sceneIndex in sceneIndexHistory.ToArray())
    //        print += "Stack Index: " + sceneIndex + "\n";
    //    Debug.Log(print);
    //}

    //private void AddSceneIndexToHistory(int sceneIndex)
    //{
    //    // do nothing if we already have that scene at the top of the stack
    //    //if (sceneIndexHistory.Count != 0)
    //    //    if (sceneIndex == sceneIndexHistory.Peek())
    //    //        return;
    //    // else add it to the stack
    //    sceneIndexHistory.Push(sceneIndex);
    //}

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetScene(int sceneIndex)
    {
        CustomSceneEvent.CustomTransitionCalled(sceneIndex);
        if(StateManager.Instance != null)
        {
            if (StateManager.Instance.concertIsActive) {StateManager.Instance.EndConcertEarly();}
        }
    }
}
