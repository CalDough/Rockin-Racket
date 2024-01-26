using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BandAnimationController : MonoBehaviour
{
    
    public BandRoleName bandName = BandRoleName.Default;
    
    [Header("Inspector Variables")] 
    [SerializeField] BandAudioController BandMember;
    [SerializeField] ParticleSystem badParticleEffect;
    [SerializeField] ParticleSystem goodParticleEffect;
    [SerializeField] ParticleSystem musicParticleEffect;
    [SerializeField] Animator characterAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] List<Transform> targetPoints = new List<Transform>();

    [Header("Animation Names")] 
    [SerializeField] string playName = "Playing";
    [SerializeField] string idleName = "Idle"; 
    //[SerializeField] string walkName = "Walk"; 

    
    [Header("Test Variables")] 
    //[SerializeField] bool alwaysPlay = false;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = false; 

    [Header("Move and Play Settings")]
    [SerializeField] private bool moveAndPlay = false;
    [SerializeField] private Vector3 moveAndPlayRange = new Vector3(.5f, 1f, .5f);  
    [SerializeField] private Vector3 originalPosition;

    private void Start()
    {
        ConcertEvents.instance.e_ConcertStarted.AddListener(StartMovementAnimation);
        ConcertEvents.instance.e_ConcertEnded.AddListener(StopMovementAnimation);
    }

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

    public void StartMovementAnimation()
    {
        MoveToTarget("Stage");
        PlayAnimation(playName); //For now i'll always force the characters to play with this
        musicParticleEffect.Play();
        if (moveAndPlay)
        {
            StartCoroutine(MoveAndPlayRoutine());
        }
    }

    public void StopMovementAnimation()
    {
        PlayAnimation(idleName);
        musicParticleEffect.Stop(); 
    }

    //public void HandleGameStateStart(object sender, StateEventArgs e)
    //{

    //    switch(e.state.stateType)
    //    {
    //        case StateType.SongIntro:
    //            MoveToTarget("Stage");
    //            break;
    //        case StateType.Song:
    //            MoveToTarget("Stage");
    //            PlayAnimation(playName); //For now i'll always force the characters to play with this
    //            musicParticleEffect.Play();
    //            if(moveAndPlay)
    //            {
    //                StartCoroutine(MoveAndPlayRoutine());
    //            }
    //            break;
    //        case StateType.SongOutro:
    //            if(StateManager.Instance.GetNextStateType() == StateType.IntermissionIntro)
    //            {MoveToTarget("Backstage");}
    //            break;
    //        case StateType.IntermissionIntro:
    //            MoveToTarget("Backstage");
    //            break;
    //        case StateType.Intermission:
    //            MoveToTarget("Backstage");
    //            break;
    //        case StateType.IntermissionOutro:
    //            MoveToTarget("Stage");
    //            break;
    //        default:
    //            break;
    //    }

    //}

    //public void HandleGameStateEnd(object sender, StateEventArgs e)
    //{
    //    StopAnimation();
    //    switch(e.state.stateType)
    //    {
    //        case StateType.Song:
    //            PlayAnimation(idleName);
    //            musicParticleEffect.Stop(); 
    //            break;
    //        case StateType.Intermission:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //private void Start()
    //{
    //    //ConcertAudioEvent.OnAudioBroken += HandleAudioBroken;
    //    //ConcertAudioEvent.OnAudioFixed += HandleAudioFixed;
    //    //ConcertAudioEvent.OnConcertEnd += HandleConcertEnd;

    //    //ConcertAudioEvent.OnPlayingAudio += HandleAudioStart;

    //    //StateEvent.OnStateStart += HandleGameStateStart;
    //    //StateEvent.OnStateEnd += HandleGameStateEnd;
    //}

    //private void OnDestroy()
    //{
    //    //ConcertAudioEvent.OnAudioBroken -= HandleAudioBroken;
    //    //ConcertAudioEvent.OnAudioFixed -= HandleAudioFixed;
    //    //ConcertAudioEvent.OnConcertEnd -= HandleConcertEnd;

    //    //StateEvent.OnStateStart -= HandleGameStateStart;
    //    //StateEvent.OnStateEnd -= HandleGameStateEnd;
    //}

    //private void HandleAudioStart(object sender, ConcertAudioEventArgs e)
    //{
    //    if (e.ConcertPosition == this.bandName)
    //    {
    //        PlayAnimation(playName);
    //    }
    //}

    //private void HandleAudioBroken(object sender, ConcertAudioEventArgs e)
    //{
    //    if (e.ConcertPosition == this.bandName)
    //    {
    //        PlayProblemParticles();
    //        var noise = musicParticleEffect.noise; 
    //        noise.strength = 3;
    //    }
    //}

    //private void HandleAudioFixed(object sender, ConcertAudioEventArgs e)
    //{
    //    if (e.ConcertPosition == this.bandName)
    //    {
    //        PlayFixedParticles();
    //        var noise = musicParticleEffect.noise; 
    //        noise.strength = 0;
    //    }
    //}

    //private void HandleConcertEnd(object sender, ConcertAudioEventArgs e)
    //{
    //    StopAnimation();
    //}


    public void MoveToTarget(string targetName)
    {
        Transform target = targetPoints.Find(t => t.name == targetName);
        originalPosition = target.position;
        if(target != null && !isMoving) 
        {
            StartCoroutine(MoveTo(target.position));
        }
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(transform.position, target);

        // threshold distance for animation
        float thresholdDistance = 0.5f; 

        if (journeyLength > thresholdDistance)
        {characterAnimator.Play(idleName);}
        else
        {characterAnimator.Play(idleName);}

        float journeyDuration = journeyLength / moveSpeed;
        float elapsedTime = 0f;

        // determine if the target is to the left or right
        bool targetIsToRight = (target.x > startPos.x);
        spriteRenderer.flipX = !targetIsToRight;

        while (elapsedTime < journeyDuration)
        {
            elapsedTime += Time.deltaTime;
            float fractionOfJourney = elapsedTime / journeyDuration;

            transform.position = Vector3.Lerp(startPos, target, fractionOfJourney);
            yield return null;
        }

        transform.position = target;
        //characterAnimator.Play(idleName);
        isMoving = false;
    }

    private IEnumerator MoveAndPlayRoutine()
    {
        //Debug.Log("MoveAndPlayRoutine started!");
        yield return new WaitForSeconds(0.5f);

        while(true)
        {
            if(characterAnimator.GetCurrentAnimatorStateInfo(0).IsName(playName))
            {
                Vector3 randomPosition = new Vector3(originalPosition.x + Random.Range(-moveAndPlayRange.x, moveAndPlayRange.x), originalPosition.y, originalPosition.z + Random.Range(-moveAndPlayRange.z, moveAndPlayRange.z));
                
                yield return MoveTo(randomPosition);
                characterAnimator.Play(playName);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
