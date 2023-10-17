using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandAnimationController : MonoBehaviour
{
    [SerializeField] BandRoleAudioController BandMember;
    
    public int ConcertPosition = 0;
    [SerializeField] ParticleSystem badParticleEffect;
    [SerializeField] ParticleSystem goodParticleEffect;
    [SerializeField] Animator characterAnimator;
    [SerializeField] string playingName = "InstrumentPlaying";
    [SerializeField] string idleName = "Idle"; 

    [SerializeField] bool alwaysPlay = false;

    public void PlayAnimation()
    {

        if (BandMember.isPlaying || BandMember.isSinging || alwaysPlay == true)
        {
            characterAnimator.Play(playingName);
            //Debug.Log($"Playing animation: {playingName}");
        }
        else
        {
            characterAnimator.Play(idleName);
            //Debug.Log($"Playing animation: {idleName}");
        }
    }

    public void StopAnimation()
    {
        characterAnimator.Play(idleName);
    }

    public void PlayEventParticles()
    {
        badParticleEffect.Play();
        StartCoroutine(StopParticleAfterSeconds(badParticleEffect, 3));
    }

    public void PlayFixedParticles()
    {
        goodParticleEffect.Play();
        StartCoroutine(StopParticleAfterSeconds(goodParticleEffect, 3));
    }

    private IEnumerator StopParticleAfterSeconds(ParticleSystem particleSystem, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        particleSystem.Stop();
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        //Testing animations with alwaysPlay bool
    
        switch(e.stateType)
        {
            case GameModeType.Song:
                if(alwaysPlay == true)
                {PlayAnimation();}
                break;
            default:
                break;
        }
        
    }
    
    public void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        StopAnimation();
    }

    private void Start()
    {
        ConcertAudioEvent.OnAudioBroken += HandleAudioBroken;
        ConcertAudioEvent.OnAudioFixed += HandleAudioFixed;
        ConcertAudioEvent.OnConcertEnd += HandleConcertEnd;
        
        ConcertAudioEvent.OnPlayingAudio += HandleAudioStart;
        
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
    }

    private void OnDestroy()
    {
        ConcertAudioEvent.OnAudioBroken -= HandleAudioBroken;
        ConcertAudioEvent.OnAudioFixed -= HandleAudioFixed;
        ConcertAudioEvent.OnConcertEnd -= HandleConcertEnd;
        
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }
    
    private void HandleAudioStart(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.ConcertPosition)
        {
            PlayAnimation();
        }
    }

    private void HandleAudioBroken(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.ConcertPosition)
        {
            PlayEventParticles();
        }
    }

    private void HandleAudioFixed(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.ConcertPosition)
        {
            PlayFixedParticles();
        }
    }

    private void HandleConcertEnd(object sender, ConcertAudioEventArgs e)
    {
        StopAnimation();
    }

    
}
