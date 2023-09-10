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
    public int MainID = 0;
    public GameObject MenuUI;
    public InputActionAsset actionAsset;
    private InputAction pauseAction;
    public void NewData()
    {
        AnimalManager.Instance.LoadNewAnimals();
        BandManager.Instance.LoadDefaultBand();
        InventoryManager.Instance.LoadDefaultItems();
        GameManager.Instance.NewGame();







    }

    public void LoadAllData()
    {
        AnimalManager.Instance.LoadAnimals();
        BandManager.Instance.LoadBand();
        InventorySaver.Instance.LoadInventory();
        GameManager.Instance.LoadGame();







    }

    public void SaveAllData()
    {
        AnimalManager.Instance.SaveAnimals();
        BandManager.Instance.SaveBand();
        InventorySaver.Instance.SaveInventory();
        GameManager.Instance.SaveGame();
        









    }

    public void SaveAllDataAndExit()
    {
        AnimalManager.Instance.SaveAnimals();
        BandManager.Instance.SaveBand();
        InventorySaver.Instance.SaveInventory();
        GameManager.Instance.SaveGame();
        








        Application.Quit();
    }



    private void Awake()
    {
        // Assuming you have an action map named "Menu" and a Pause action within it.
        var menuActionMap = actionAsset.FindActionMap("PauseMenu");
        pauseAction = menuActionMap.FindAction("Pause");
        
        pauseAction.performed += _ => ToggleMenu(); 
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    public void OpenMenu()
    {
        TimeEvents.GamePaused();
        if(MenuUI != null)
        {
        MenuUI.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        TimeEvents.GameResumed();
        if(MenuUI != null)
        {
        MenuUI.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        CustomSceneEvent.CustomTransitionCalled(1);
        CloseMenu();
        if(GameStateManager.Instance != null)
        {
            if( GameStateManager.Instance.ConcertActive)
            {GameStateManager.Instance.EndConcertEarly();}
        }

            
    }

    public void ToggleMenu()
    {
        if(MenuUI == null)
        {return;}

        if (MenuUI.activeSelf)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
    
}
