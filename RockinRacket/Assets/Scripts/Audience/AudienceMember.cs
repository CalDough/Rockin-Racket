using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is to be used in conjunction with the Audience Prefab and the T-Shirt Cannon minigame and may reference values from
 * Animal.cs and AnimalManager.cs
 */

public class AudienceMember : MonoBehaviour
{
    [SerializeField] private AudienceRow currentRow;

    [SerializeField] private Animator characterAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem goodParticles;
    [SerializeField] ParticleSystem badParticles; 
    [SerializeField] private float moveSpeed = 5f;
    private bool isMoving = false;
    [SerializeField] private float moodRandomizationDuration = 30f;
    private float moveInterval = 5f;

    private AudienceController audienceController;
    [SerializeField] private AudienceHypeState currentHypeState;
    [SerializeField] private AudienceComfortState currentComfortState;

    public bool IsMoodRandomized = false;
    
    private Coroutine MoveCoroutine;

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
        switch (currentHypeState)
        {
            case AudienceHypeState.HighHype:
                characterAnimator.Play("Audience_Happy");
                break;

            case AudienceHypeState.MidHype:
                characterAnimator.Play("Audience_Excited");
                break;

            case AudienceHypeState.LowHype:
                characterAnimator.Play("Audience_Normal");
                break;
        }
        if(currentComfortState == AudienceComfortState.LowComfort)
        {
            characterAnimator.Play("Audience_Normal");
        }

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
                MinigameStatusManager.Instance.AddMinigameVariables(100,10);
                this.currentComfortState = AudienceComfortState.HighComfort;
                break;

            case TShirtCannon.PressureState.Weak:
                characterAnimator.Play("Audience_Excited");
                MinigameStatusManager.Instance.AddMinigameVariables(50,10);
                this.currentComfortState = AudienceComfortState.MidComfort;
                break;

            case TShirtCannon.PressureState.Bad:
                characterAnimator.Play("Audience_Normal");
                badParticles.Play();
                break;
        }
    }

    public void RandomizeMood()
    {
        if (IsMoodRandomized) return;

        IsMoodRandomized = true;
        this.currentComfortState = AudienceComfortState.LowComfort;

        StartCoroutine(ResetMoodAfterDelay());
    }

    private IEnumerator ResetMoodAfterDelay()
    {
        yield return new WaitForSeconds(moodRandomizationDuration);
        ResetMood();
    }

    public void ResetMood()
    {
        if (!IsMoodRandomized) return;

        IsMoodRandomized = false;
        if(audienceController != null)
        {
            UpdateState(audienceController.currentHypeState, audienceController.currentComfortState);
        }
        
    }
    
    public void EnterConcert()
    {
        MoveCoroutine = StartCoroutine(MoveRoutine());
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
        //float thresholdDistance = 0.5f;

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
        isMoving = true;

        // flip sprite based on direction
        spriteRenderer.flipX = (target.x > transform.position.x);

        float journeyDuration = Vector3.Distance(target, transform.position) / moveSpeed;
        float elapsedTime = 0f;

        Vector3 startPos = transform.position;

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
        
        Destroy(this.gameObject);
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
                UpdateBehavior();
                MoveCoroutine = StartCoroutine(MoveRoutine());
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
                if(MoveCoroutine != null)
                {
                    StopCoroutine(MoveCoroutine);
                }
                break;
            default:
                break;
        }
    }

    public void SetRow(AudienceRow row)
    {
        currentRow = row;
        MoveToRandomPositionInRow();
    }

    private void MoveToRandomPositionInRow()
    {
        if (currentRow != null && !isMoving)
        {
            Vector3 newPosition = currentRow.GetRandomPosition();
            StartCoroutine(MoveToPosition(newPosition));
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (currentRow != null && !isMoving)
            {
                Vector3 newPosition = currentRow.GetRandomPosition();
                yield return StartCoroutine(MoveToPosition(newPosition));
            }
            yield return new WaitForSeconds(moveInterval);
        }
    }

    //This isnt working yet
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Audience"))
        {
            AttemptToMoveToNewSpot();
        }
    }

    private void AttemptToMoveToNewSpot()
    {
        Debug.Log("Too Close, Moving Away");
        if (isMoving) return;

        if (Random.value > 0.5f)
        {
            MoveToRandomPositionInRow();
        }
        else
        {
            MoveToRandomRow();
        }
    }

    private void MoveToRandomRow()
    {
        if (audienceController != null)
        {
            AudienceRow newRow = audienceController.GetRandomRow();
            if (newRow != null && newRow != currentRow)
            {
                currentRow = newRow;
                MoveToRandomPositionInRow();
            }
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