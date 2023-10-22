using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is to be used in conjunction with the Audience Prefab and the T-Shirt Cannon minigame and may reference values from
 * Animal.cs and AnimalManager.cs
 */

public class AudienceMember : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem goodParticles;
    [SerializeField] ParticleSystem badParticles; 
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = false;

    private AudienceController audienceController;
    [SerializeField] private AudienceHypeState currentHypeState;
    [SerializeField] private AudienceComfortState currentComfortState;

    public bool IsMoodRandomized = false;
    

    public void Init(AudienceController controller)
    {
        audienceController = controller;
        UpdateState(controller.currentHypeState, controller.currentComfortState);
    }

    public void UpdateState(AudienceHypeState newHypeState, AudienceComfortState newComfortState)
    {
        currentHypeState = newHypeState;
        currentComfortState = newComfortState;

        UpdateBehavior();
    }

    private void UpdateBehavior()
    {
    }


    private void Start()
    {
        SubscribeEvents();
        characterAnimator = GetComponent<Animator>();
    }

    //Reacts to the TCM mini-game and if hit by a Tshirt, it will make them either more comfortable or happy
    public void PlayTCMReaction(TShirtCannon.PressureState pressure)
    {
        switch (pressure)
        {
            case TShirtCannon.PressureState.Good:
                characterAnimator.Play("Audience_Happy");
                goodParticles.Play();
                break;

            case TShirtCannon.PressureState.Weak:
                characterAnimator.Play("Audience_Excited");
                break;

            case TShirtCannon.PressureState.Bad:
                characterAnimator.Play("Audience_Normal");
                badParticles.Play();
                break;
        }
    }

    public void MoveToConcertSpot(Vector3 target)
    {
        StartCoroutine(MoveToPosition(target));
    }

    public void ExitConcert(Vector3 target)
    {
        StartCoroutine(MoveToExit(target));
    }
    private IEnumerator MoveToExit(Vector3 target)
    {
        if (isMoving) yield break;
        isMoving = true;

        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(transform.position, target);
        float thresholdDistance = 0.5f;

        //characterAnimator.Play(journeyLength > thresholdDistance ? "MoveAnimation" : "IdleAnimation");

        float journeyDuration = journeyLength / moveSpeed;
        float elapsedTime = 0f;
        spriteRenderer.flipX = (target.x > startPos.x);

        while (elapsedTime < journeyDuration)
        {
            elapsedTime += Time.deltaTime;
            float fractionOfJourney = elapsedTime / journeyDuration;
            transform.position = Vector3.Lerp(startPos, target, fractionOfJourney);
            yield return null;
        }

        transform.position = target;
        isMoving = false;

        OnArrival();
    }


    private IEnumerator MoveToPosition(Vector3 target)
    {
        if (isMoving) yield break;
        isMoving = true;

        Vector3 startPos = transform.position;
        float journeyLength = Vector3.Distance(transform.position, target);
        float thresholdDistance = 0.5f;

        //characterAnimator.Play(journeyLength > thresholdDistance ? "MoveAnimation" : "IdleAnimation");

        float journeyDuration = journeyLength / moveSpeed;
        float elapsedTime = 0f;
        spriteRenderer.flipX = (target.x > startPos.x);

        while (elapsedTime < journeyDuration)
        {
            elapsedTime += Time.deltaTime;
            float fractionOfJourney = elapsedTime / journeyDuration;
            transform.position = Vector3.Lerp(startPos, target, fractionOfJourney);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }

    private void OnArrival()
    {
        
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;
        MoodEvent.OnHypeAndComfortChange += HandleNewHypeAndComfortChange;
    }

    private void HandleNewHypeAndComfortChange(object sender, MoodEventArgs e)
    {
        if(IsMoodRandomized){return;}

        this.currentComfortState = e.CurrentComfortState;
        this.currentHypeState = e.CurrentHypeState;
        UpdateBehavior();
    }

    private void UnsubscribeEvents()
    {
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
        MoodEvent.OnHypeAndComfortChange -= HandleNewHypeAndComfortChange;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {
            case GameModeType.Song:
                break;
            default:
                break;
        }
    }
}

public enum AudienceHypeState
{
    LowHype,
    MidHype,
    HighHype
}

public enum AudienceComfortState
{
    LowComfort,
    MidComfort,
    HighComfort
}