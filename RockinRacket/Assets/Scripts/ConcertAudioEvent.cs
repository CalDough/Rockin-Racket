using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ConcertAudioEvent 
{
    public static event EventHandler<ConcertAudioEventArgs> OnAudioBroken; 
    public static event EventHandler<ConcertAudioEventArgs> OnAudioFixed; 
    
    public static event EventHandler<ConcertAudioEventArgs> OnConcertEnd; 

    public static void AudioBroken(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        OnAudioBroken?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void AudioFixed(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        OnAudioFixed?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void ConcertEnd()
    {
        OnConcertEnd?.Invoke(null, new ConcertAudioEventArgs( ));
    }
}

public class ConcertAudioEventArgs : EventArgs
{
    public MiniGame EventObject { get; private set; }
    public float BrokenValue { get; set; }
    public int ConcertPosition { get; set; }
    public bool AffectInstrument { get; set; }

    public ConcertAudioEventArgs()
    {

    }

    public ConcertAudioEventArgs(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        EventObject = eventData;
        BrokenValue = brokenValue;
        ConcertPosition = concertPosition;
        AffectInstrument = affectInstrument;
    }
}
