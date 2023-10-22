using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MoodEvent 
{
    
    public static event EventHandler<MoodEventArgs> OnComfortChange; 
    public static event EventHandler<MoodEventArgs> OnHypeChange; 
    public static event EventHandler<MoodEventArgs> OnHypeAndComfortChange; 

    public static void HypeChange(AudienceHypeState newHypeState)
    {
        OnHypeChange?.Invoke(null, new MoodEventArgs(newHypeState));
    }

    public static void ComfortChange(AudienceComfortState newComfortState)
    {
        OnComfortChange?.Invoke(null, new MoodEventArgs(  newComfortState));
    }

    public static void HypeAndComfortChange(AudienceHypeState newHypeState, AudienceComfortState newComfortState)
    {
        OnHypeAndComfortChange?.Invoke(null, new MoodEventArgs( newHypeState, newComfortState));
    }
}

public class MoodEventArgs : EventArgs
{

    public AudienceHypeState CurrentHypeState;
    public AudienceComfortState CurrentComfortState;

    public MoodEventArgs()
    {

    }

    public MoodEventArgs(AudienceHypeState currentHypeState)
    {
        CurrentHypeState = currentHypeState;
    }

    public MoodEventArgs( AudienceHypeState currentHypeState, AudienceComfortState currentComfortState)
    {
        CurrentHypeState = currentHypeState;
        CurrentComfortState = currentComfortState;
    }

    public MoodEventArgs(AudienceComfortState currentComfortState)
    {
        CurrentComfortState = currentComfortState;
    }
}
