using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  This class handles saving and loading logic for the main menu
 * 
 */

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject newGameOverwritePopUp;
    [SerializeField] Button continueButton;

    [Header("Script References")]
    [SerializeField] GameLoadHandler gameLoadHandler;
    [SerializeField] SceneLoader sceneLoader;

    [Header("Transition Data")]
    [SerializeField] TransitionData tutorialTransition;
    [SerializeField] TransitionData hubTransition;


    private void Start()
    {
        if (!GameManager.Instance.hasStartedGame)
        {
            continueButton.interactable = false;
        }
    }

    public void OnNewGameClick()
    {
        if (GameManager.Instance.hasStartedGame)
        {
            Debug.Log("Save Game Found, activating overwrite popup");
            newGameOverwritePopUp.SetActive(true);
        }
        else
        {
            NewGameInitialize();
        }
    }

    public void NewGameInitialize()
    {
        sceneLoader.SwitchScene(tutorialTransition);
        gameLoadHandler.NewGame();
    }

    public void OnContinueGameClick()
    {
        if (GameManager.Instance.hasStartedGame)
        {
            gameLoadHandler.Load();
            sceneLoader.SwitchScene(hubTransition);
        }
    }

    public void OnQuitGameClick()
    {
        gameLoadHandler.SaveAndExit();
        sceneLoader.ExitGame();
    }

    public void DeactivateOverwritePopup()
    {
        newGameOverwritePopUp.SetActive(false);
    }
}
