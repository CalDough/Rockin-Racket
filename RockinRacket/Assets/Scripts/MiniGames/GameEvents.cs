using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents 
{
    public static event EventHandler<GameEventArgs> OnEventStart; //Starting of an event
    public static event EventHandler<GameEventArgs> OnEventFail; //Fail of an event
    public static event EventHandler<GameEventArgs> OnEventMiss; //Miss of an event, due to game state change
    public static event EventHandler<GameEventArgs> OnEventCancel; //Cancelation of an event, such as items used
    public static event EventHandler<GameEventArgs> OnEventComplete; //Successful events
    public static event EventHandler<GameEventArgs> OnEventOpen;
    public static event EventHandler<GameEventArgs> OnEventClose;

    public static void EventStart(MiniGame eventData)
    {
        OnEventStart?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventFail(MiniGame eventData)
    {
        OnEventFail?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventCancel(MiniGame eventData)
    {
        OnEventCancel?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventMiss(MiniGame eventData)
    {
        OnEventMiss?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventComplete(MiniGame eventData)
    {
        OnEventComplete?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventOpened(MiniGame eventData)
    {
        OnEventOpen?.Invoke(null, new GameEventArgs(eventData));
    }

    public static void EventClosed(MiniGame eventData)
    {
        OnEventClose?.Invoke(null, new GameEventArgs(eventData));
    }
}

public class GameEventArgs : EventArgs
{
    public MiniGame EventObject { get; private set; }
    public float Duration { get; set; }

    public GameEventArgs(MiniGame eventData)
    {
        EventObject = eventData;
    }

    public GameEventArgs(MiniGame eventData, float duration)
    {
        Duration = duration;
    }
}