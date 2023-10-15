using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ConcertAudioEvent 
{
    
    public static event EventHandler<ConcertAudioEventArgs> OnPlayingAudio; 
    public static event EventHandler<ConcertAudioEventArgs> OnAudioBroken; 
    public static event EventHandler<ConcertAudioEventArgs> OnAudioFixed; 
    
    public static event EventHandler<ConcertAudioEventArgs> OnRequestBandPlayers; 
    public static event EventHandler<ConcertAudioEventArgs> OnSendBandPlayers; 
    
    public static event EventHandler<ConcertAudioEventArgs> OnConcertEnd; 

    public static void AudioBroken(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        OnAudioBroken?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void AudioFixed(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        OnAudioFixed?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void PlayingAudio(int concertPosition)
    {
        OnPlayingAudio?.Invoke(null, new ConcertAudioEventArgs(  concertPosition));
    }

    public static void ConcertEnd()
    {
        OnConcertEnd?.Invoke(null, new ConcertAudioEventArgs( ));
    }

    public static void RequestBandPlayers()
    {
        OnRequestBandPlayers?.Invoke(null, new ConcertAudioEventArgs( ));
    }

    public static void SendBandPlayers(BandRoleAudioController bandRoleAudioPlayer)
    {
        OnSendBandPlayers?.Invoke(null, new ConcertAudioEventArgs( bandRoleAudioPlayer ));
    }
}

public class ConcertAudioEventArgs : EventArgs
{
    public MiniGame EventObject { get; private set; }
    public BandRoleAudioController BandRoleAudioPlayer { get; private set; }
    public float BrokenValue { get; set; }
    public int ConcertPosition { get; set; }
    public bool AffectInstrument { get; set; }

    public ConcertAudioEventArgs()
    {

    }

    public ConcertAudioEventArgs(int concertPosition)
    {
        ConcertPosition = concertPosition;
    }

    public ConcertAudioEventArgs(BandRoleAudioController bandRoleAudioPlayer)
    {
        BandRoleAudioPlayer = bandRoleAudioPlayer;
    }

    public ConcertAudioEventArgs(MiniGame eventData, float brokenValue, int concertPosition, bool affectInstrument)
    {
        EventObject = eventData;
        BrokenValue = brokenValue;
        ConcertPosition = concertPosition;
        AffectInstrument = affectInstrument;
    }
}
