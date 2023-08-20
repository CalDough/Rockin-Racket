using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script goes unused right now, its for testing. It will most likely be deleted.
*/
public class UIEvent 
{
    public static event EventHandler<UIEventArgs> UserInterfaceChanged;

    public static void UIChanged(UIState newState)
    {
        UserInterfaceChanged?.Invoke(null, new UIEventArgs(newState));
    }
}

public class UIEventArgs : EventArgs
{
    public UIState CurrentState { get; private set; }

    public UIEventArgs(UIState newState)
    {
        CurrentState = newState;
    }
}
[Serializable]
public enum UIState
{
    VenueSelection,
    SongSelection,
    Main,
    Shop,
    Band,
    Setting,
    Gameplay,
    Inventory,
    Overworld,
    Cutscene
}