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
    [SerializeField] string walkName = "Walk"; 

    [SerializeField] bool alwaysPlay = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] List<Transform> targetPoints = new List<Transform>();
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = false; 

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
        // Testing animations with alwaysPlay bool, if enabled the band member always uses animations
    
        switch(e.stateType)
        {
            case GameModeType.Song:
                if(alwaysPlay == true)
                {PlayAnimation();}
                break;
            case GameModeType.Intermission:
                OnIntermissionStart();
                break;
            default:
                break;
        }
        
    }
    
    public void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        StopAnimation();
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            case GameModeType.Intermission:
                OnIntermissionEnd();
                break;
            default:
                break;
        }
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

    private void OnIntermissionStart()
    {
        StopAnimation();
        // preferably we play a walking animaton to show them leaving the stage
        MoveToTarget("Backstage");
    }
    private void OnIntermissionEnd()
    {
        StopAnimation();
        // preferably we play a walking animaton to show them entering the stage
        // might need to add state for stage entry or some wacky logic here
        MoveToTarget("Stage");
    }

    public void MoveToTarget(string targetName)
    {
        // find the target in the list by name
        Transform target = targetPoints.Find(t => t.name == targetName);
        if(target != null && !isMoving) 
        {
            StartCoroutine(MoveTo(target));
        }
    }

    private IEnumerator MoveTo(Transform target)
    {
        isMoving = true;
        //characterAnimator.Play(walkName);

        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(startPos, target.position);
        float journeyDuration = journeyLength / moveSpeed; 
        float elapsedTime = 0f; 

        // determine if the target is to the left or right to flip sprite
        bool targetIsToRight = (target.position.x < startPos.x);
        spriteRenderer.flipX = targetIsToRight;

        while (elapsedTime < journeyDuration)
        {
            elapsedTime += Time.deltaTime;
            float fractionOfJourney = elapsedTime / journeyDuration;

            transform.position = Vector3.Lerp(startPos, target.position, fractionOfJourney);
            yield return null;
        }

        transform.position = target.position;
        characterAnimator.Play(idleName);
        isMoving = false;
    }
}
