using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandAnimationController : MonoBehaviour
{
    
    public BandRoleName bandName = BandRoleName.Default;
    
    [Header("Inspector Variables")] 
    [SerializeField] BandAudioController BandMember;
    [SerializeField] ParticleSystem badParticleEffect;
    [SerializeField] ParticleSystem goodParticleEffect;
    [SerializeField] Animator characterAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] List<Transform> targetPoints = new List<Transform>();

    [Header("Animation Names")] 
    [SerializeField] string playName = "Playing";
    [SerializeField] string idleName = "Idle"; 
    [SerializeField] string walkName = "Walk"; 

    
    [Header("Test Variables")] 
    [SerializeField] bool alwaysPlay = false;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = false; 






    public void PlayAnimation(string animationName)
    {
        if (characterAnimator == null)
        {
            Debug.LogWarning("animator component is not assigned");
            return;
        }

        if (characterAnimator.HasState(0, Animator.StringToHash(animationName)))
        {characterAnimator.Play(animationName);}

        else
        {Debug.LogWarning($"no animation named '{animationName}' found in the animator");}
    }

    public void StopAnimation()
    {
        characterAnimator.StopPlayback();
    }

    public void PlayProblemParticles()
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
        switch(e.stateType)
        {
            case GameModeType.SongIntro:
                MoveToTarget("Stage");
                break;
            case GameModeType.Song:
                MoveToTarget("Stage");
                PlayAnimation(playName); //For now ill always force the characters to play with this
                break;
            case GameModeType.SongOutro:
                MoveToTarget("Backstage");
                break;

            case GameModeType.IntermissionIntro:
                MoveToTarget("Backstage");
                break;
            case GameModeType.Intermission:
                MoveToTarget("Backstage");
                break;
            case GameModeType.IntermissionOutro:
                MoveToTarget("Stage");
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
                PlayAnimation(idleName);
                break;
            case GameModeType.Intermission:
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
        if (e.ConcertPosition == this.bandName)
        {
            PlayAnimation(playName);
        }
    }

    private void HandleAudioBroken(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.bandName)
        {
            PlayProblemParticles();
        }
    }

    private void HandleAudioFixed(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.bandName)
        {
            PlayFixedParticles();
        }
    }

    private void HandleConcertEnd(object sender, ConcertAudioEventArgs e)
    {
        StopAnimation();
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

        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(startPos, target.position);

        // threshold distance for animation
        float thresholdDistance = 0.5f; 

        if (journeyLength > thresholdDistance)
        {characterAnimator.Play(walkName);}
        else
        {characterAnimator.Play(idleName);}

        float journeyDuration = journeyLength / moveSpeed;
        float elapsedTime = 0f;

        // Determine if the target is to the left or right
        bool targetIsToRight = (target.position.x > startPos.x);
        spriteRenderer.flipX = !targetIsToRight;

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
