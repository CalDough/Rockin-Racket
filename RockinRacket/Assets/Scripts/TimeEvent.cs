using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
    This script is for events related to pausing/closing the game, and resetting levels. 
    Other scripts that are affected by these events should subscribe to this script or call the script at appropriate times.
*/

public static class TimeEvents 
{
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;
    public static event Action OnLevelReset;
    public static event Action OnSaveAndClose;
    
    public static void GamePaused()
    {
        Debug.Log("Game Paused");
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

    public static void GameResumed()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }

    public static void LevelReset()
    {
        Debug.Log("Level Restarted");
        OnLevelReset?.Invoke();
    }
    
    public static void GameSaveAndQuit()
    {
        Debug.Log("Save And Quit");
        OnSaveAndClose?.Invoke();
    }
    
}
