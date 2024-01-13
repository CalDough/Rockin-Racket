using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MinigameEvents 
{
    public static event EventHandler<GameEventArgs> OnMinigameStart; 
    public static event EventHandler<GameEventArgs> OnMinigameFail; 
    public static event EventHandler<GameEventArgs> OnMinigameCancel; 
    public static event EventHandler<GameEventArgs> OnMinigameComplete; 

    public static void EventStart(MinigameController eventData)
    {
        OnMinigameStart?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventFail(MinigameController eventData)
    {
        OnMinigameFail?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventCancel(MinigameController eventData)
    {
        OnMinigameCancel?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventComplete(MinigameController eventData)
    {
        OnMinigameComplete?.Invoke(null, new GameEventArgs(eventData));
    }

}

public class GameEventArgs : EventArgs
{
    public MinigameController EventObject { get; private set; }

    public GameEventArgs(MinigameController eventData)
    {
        EventObject = eventData;
    }
}