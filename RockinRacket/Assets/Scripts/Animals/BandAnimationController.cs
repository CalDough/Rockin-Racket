using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BandAnimationController : MonoBehaviour
{
    
    public BandRoleName bandName = BandRoleName.Default;
    
    [Header("Inspector Variables")] 
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

    [Header("Particle Effect Controls")]
    [SerializeField] private bool reduceNoiseStrength = false;
    [SerializeField] private float particleStrength = 2f;
    [SerializeField] private float particleStrengthReduction = .004f;

    [Header("Move and Play Settings")]
    [SerializeField] private bool moveAndPlay = false;
    [SerializeField] private Vector3 moveAndPlayRange = new Vector3(.5f, 1f, .5f);  
    [SerializeField] private Vector3 originalPosition;

    private void Start()
    {
        ConcertAudioEvent.OnAudioBroken += HandleAudioBroken;
        ConcertEvents.instance.e_ConcertStarted.AddListener(StartMovementAnimation);
        ConcertEvents.instance.e_ConcertEnded.AddListener(StopMovementAnimation);
    }

    private void FixedUpdate()
    {
        if (reduceNoiseStrength)
        {
            ReduceMusicParticleEffectNoiseStrength();
        }
    }

    private void ReduceMusicParticleEffectNoiseStrength()
    {
        var noise = musicParticleEffect.noise;
        noise.strength = Mathf.Max(0, noise.strength.constant - particleStrengthReduction);
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
        //MoveToTarget("Stage");
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


    private void OnDestroy()
    {
        ConcertAudioEvent.OnAudioBroken -= HandleAudioBroken;
        //ConcertAudioEvent.OnAudioFixed -= HandleAudioFixed;
        //ConcertAudioEvent.OnConcertEnd -= HandleConcertEnd;
    }

    //private void HandleAudioStart(object sender, ConcertAudioEventArgs e)
    //{
    //    if (e.ConcertPosition == this.bandName)
    //    {
    //        PlayAnimation(playName);
    //    }
    //}

    private void HandleAudioBroken(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.bandName)
        {
            PlayProblemParticles();
            var noise = musicParticleEffect.noise; 
            noise.strength = particleStrength;

        }
    }

    private void HandleAudioFixed(object sender, ConcertAudioEventArgs e)
    {
        if (e.ConcertPosition == this.bandName)
        {
            PlayFixedParticles();
            var noise = musicParticleEffect.noise; 
            noise.strength = 0;
        }
    }

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
        // you'll have to ignore this code, i think there were plans for an animation where the characters move while playing, so
        // thats why the code looks odd
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
