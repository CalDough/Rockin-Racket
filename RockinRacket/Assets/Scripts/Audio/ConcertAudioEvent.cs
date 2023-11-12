using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
All Audio Events that affect the band member go through this class.
When sending a BrokenValue, leave it as positive values only.
The instruments broken range is 0-5, 0 being the least broken and 5 being the most broken.
This class could be combined with the Mini-game Concert Events

*/
public static class ConcertAudioEvent 
{
    
    public static event EventHandler<ConcertAudioEventArgs> OnPlayingAudio; 
    public static event EventHandler<ConcertAudioEventArgs> OnAudioBroken; 
    public static event EventHandler<ConcertAudioEventArgs> OnAudioFixed; 
    
    //public static event EventHandler<ConcertAudioEventArgs> OnRequestBandPlayers; 
    //public static event EventHandler<ConcertAudioEventArgs> OnSendBandPlayers; 
    
    public static event EventHandler<ConcertAudioEventArgs> OnConcertEnd; 

    public static void AudioBroken(MiniGame eventData, float brokenValue, BandRoleName concertPosition, bool affectInstrument)
    {
        OnAudioBroken?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void AudioFixed(MiniGame eventData, float brokenValue, BandRoleName concertPosition, bool affectInstrument)
    {
        OnAudioFixed?.Invoke(null, new ConcertAudioEventArgs( eventData,  brokenValue,  concertPosition,  affectInstrument));
    }

    public static void PlayingAudio(BandRoleName concertPosition)
    {
        OnPlayingAudio?.Invoke(null, new ConcertAudioEventArgs(  concertPosition));
    }

    public static void ConcertEnd()
    {
        OnConcertEnd?.Invoke(null, new ConcertAudioEventArgs( ));
    }
    /*
    public static void RequestBandPlayers()
    {
        OnRequestBandPlayers?.Invoke(null, new ConcertAudioEventArgs( ));
    }

    public static void SendBandPlayers(BandAudioController bandAudioPlayer)
    {
        OnSendBandPlayers?.Invoke(null, new ConcertAudioEventArgs( bandAudioPlayer ));
    }
    */
}

public class ConcertAudioEventArgs : EventArgs
{
    public MiniGame EventObject { get; private set; }
    //public BandAudioController BandAudioPlayer { get; private set; }
    public float BrokenValue { get; set; }
    public BandRoleName ConcertPosition { get; set; }
    public bool AffectInstrument { get; set; }

    public ConcertAudioEventArgs()
    {

    }

    public ConcertAudioEventArgs(BandRoleName concertPosition)
    {
        ConcertPosition = concertPosition;
    }
    /*
    public ConcertAudioEventArgs(BandAudioController bandRoleAudioPlayer)
    {
        BandAudioPlayer = bandRoleAudioPlayer;
    }
    */
    public ConcertAudioEventArgs(MiniGame eventData, float brokenValue, BandRoleName concertPosition, bool affectInstrument)
    {
        EventObject = eventData;
        BrokenValue = brokenValue;
        ConcertPosition = concertPosition;
        AffectInstrument = affectInstrument;
    }
}
