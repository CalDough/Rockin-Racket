using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
    This script is sorta a singleton. It really should not be or it should be put outside of a canvas element and upon loading a scene finds
    the object that has the transition animation. I was too lazy to do that, so if you get errors when scene swapping, they can be ignored for now.
    When changing scenes, This script should be called with the correct  scene name or build index.
*/

public class UserInterfaceManager : MonoBehaviour
{
    
    public Animator transition;
    public float transitionTime = 1f;
    public string loadingSceneName = "LoadingScene";
    public UIState CurrentState = UIState.Main;
    //public MenuState CurrentState = MenuState.MainMenu;
    public static UserInterfaceManager Instance { get; private set; }

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
    
    void OnEnable()
    {
        UIEvent.UserInterfaceChanged += HandleUIChanged;
    }

    void OnDisable()
    {
        UIEvent.UserInterfaceChanged -= HandleUIChanged;
    }

    void HandleUIChanged(object sender, UIEventArgs args)
    {
        
        CurrentState = args.CurrentState;
        
    }

    public void SwitchSceneIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneIndexAsync(sceneIndex));
    }

    public void InvokeUIChanged(UIState newState)
    {
        UIEvent.UIChanged(newState);
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {

        if(transition != null)
        {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        }
         // Load the loading scene synchronously first
        SceneManager.LoadScene(loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Here you can update UI for loading progress with asyncOperation.progress
            yield return null;
        }
    }

    private IEnumerator LoadSceneIndexAsync(int sceneName)
    {
        if(transition != null)
        {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        }
         // Load the loading scene synchronously first
        SceneManager.LoadScene(loadingSceneName);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            // Here you can update UI for loading progress with asyncOperation.progress
            yield return null;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
