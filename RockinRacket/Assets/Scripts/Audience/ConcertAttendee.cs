using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConcertAttendee : Attendee
{
    public enum MoodState { Excited, Neutral, Requesting, Caught, Displeased }
    public enum RequestableItem { RedShirt, WhiteShirt,  BlackShirt }

    [Header("Events")]
    public UnityEvent onItemUnfulfilledEvent;
    public UnityEvent onItemFulfilledEvent;
    public UnityEvent onEnterConcert;

    [Header("Score Variables")]
    public int ScoreBonus = 10;
    public int ScorePenalty = -5;

    [Header("Movement Variables")]
    [SerializeField] private Vector3 defaultLocation;
    [SerializeField] private Vector2 movementRange = new Vector2(-1f, 1f); 
    [SerializeField] private Vector2 moveSpeed = new Vector2(1f, 3f); 
    [SerializeField] private float minTrashForce = 5f;
    [SerializeField] private float maxTrashForce = 10f;
    public bool isInConcert = false;

    [Header("Mood Variables")]
    public MoodState currentMood = MoodState.Neutral;
    public RequestableItem wantedItem = RequestableItem.RedShirt;
    [SerializeField] private CrowdTrash[] Trash; 
    [SerializeField] private ThoughtBubble thoughtBubble; 

    //0 = min, 90 = max
    public int MoodScore = 50;

    //How long before an item is wanted
    public float itemWaitMin = 15f;
    public float itemWaitMax = 30f;
    //How long they will have their thought bubble up
    public float itemPatienceMin = 15f;
    public float itemPatienceMax = 25f;

    public float trashMinAngle = 0f;
    public float trashMaxAngle = 90f;

    public float jumpStrength = 5f; 

    [Header("Appearance Variables")]
    //public Sprite[] appearanceVariations;
    //private SpriteRenderer sr;
    public Collider2D solidCollider2D;
    [SerializeField] SpriteRenderer Clothes;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem frustratedParticles;
    [SerializeField] ParticleSystem pleasedParticles; 
    [SerializeField] ParticleSystem hypedParticles; 

    [SerializeField] private string CheerAudioPath = "Audience_Cheer";
    [SerializeField] private string BooAudioPath = "Audience_Boo";

    [SerializeField] private string RequestAnimation = "Audience_Request";
    [SerializeField] private string CatchAnimation = "Audience_Catch";
    [SerializeField] private string ExcitedAnimation = "Audience_Excited";
    [SerializeField] private string NeutralAnimation = "Audience_Neutral";
    [SerializeField] private string DispleasedAnimation = "Audience_Displeased";

    private Coroutine ShirtCoroutine;
    private Coroutine LerpAttendeeCoroutine;
    private Coroutine JumpCoroutine;
    private Coroutine MoveCoroutine;
    private Coroutine AnimationHandlerCoroutine;

    public override void Init()
    {
        ConcertEvents.instance.e_SongStarted.AddListener(StartAttendeeMovement);
        ConcertEvents.instance.e_SongEnded.AddListener(StopAttendeeMovement);
    }

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();        
        SubscribeEvents();
        currentMoodRating = 50;
        defaultLocation = this.gameObject.transform.position;
        //StartMoveCoroutine();


    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(2f, 5f); 
            yield return new WaitForSeconds(waitTime);

            float targetDistance = Random.Range(movementRange.x, movementRange.y);
            Vector3 targetPosition = new Vector3(defaultLocation.x + targetDistance, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

            float speed = Mathf.Lerp(moveSpeed.x, moveSpeed.y, currentMoodRating / 100f);
            StartLerp(transform.position, targetPosition, speed);
        }
    }

    //Have to use custom lerp with speed variable since it makes movement more natural with custom timing
    public void StartLerp(Vector3 start, Vector3 end, float speed)
    {
        if (LerpAttendeeCoroutine != null)
        {
            StopCoroutine(LerpAttendeeCoroutine);
        }

        LerpAttendeeCoroutine = StartCoroutine(LerpAttendee(start, end, speed));
    }

    private IEnumerator LerpAttendee(Vector3 start, Vector3 end, float speed)
    {
        solidCollider2D.isTrigger = true;
        this.rb.bodyType = RigidbodyType2D.Kinematic;
        float journeyLength = Vector3.Distance(start, end);
        float journeyDuration = journeyLength / speed;
        float elapsedTime = 0;

        while (elapsedTime < journeyDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsedTime / journeyDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        EndLerp();
    }

    private void SubscribeEvents()
    {
        MinigameEvents.OnMinigameFail += HandleEventFail;
        MinigameEvents.OnMinigameComplete += HandleEventComplete;
    }

    private void UnsubscribeEvents()
    {
        MinigameEvents.OnMinigameFail -= HandleEventFail;
        MinigameEvents.OnMinigameComplete -= HandleEventComplete;
    }

    private void SetMood(MoodState mood, bool temporary = false, float duration = 0f)
    {
        if (temporary)
        {
            if(AnimationHandlerCoroutine != null)
            {
                StopCoroutine(AnimationHandlerCoroutine);
            }
            AnimationHandlerCoroutine = StartCoroutine(RevertMoodAfterDelay(duration));
        }
        currentMood = mood;
        switch (mood)
        {
            case MoodState.Excited:
                anim.Play(ExcitedAnimation);
                break;
            case MoodState.Neutral:
                anim.Play(NeutralAnimation);
                break;
            case MoodState.Requesting:
                anim.Play(RequestAnimation);
                break;
            case MoodState.Caught:
                anim.Play(CatchAnimation);
                break;
            case MoodState.Displeased:
                anim.Play(DispleasedAnimation);
                break;
        }
    }

    private IEnumerator RevertMoodAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CalculateMoodstate(0);
    }

    public void CalculateMoodstate(int moodValueChange)
    {
        MoodScore += moodValueChange;
        MoodScore = Mathf.Clamp(MoodScore,0,100);

        if(MoodScore >= 70)
        {
            SetMood(MoodState.Excited);
        }
        else if(MoodScore < 30)
        {
            SetMood(MoodState.Displeased);
        }
        else
        {
            SetMood(MoodState.Neutral);
        }
    }

    public override void TriggerAttendeeHappyEffect()
    {
        if(currentMood == MoodState.Excited || currentMood == MoodState.Neutral)
        {
            hypedParticles.Play();
        }
        else
        {
            pleasedParticles.Play();
        }
    }

    public override void TriggerAttendeeAngryEffect( )
    {
        frustratedParticles.Play();
    }

    public override void RandomizeAppearance()
    {
        SpriteRenderer spriteRenderer = Clothes;
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.HSVToRGB(Random.value, 0.65f, 1f); 

            Color.RGBToHSV(spriteRenderer.color, out float H, out float S, out float V);
            H += Random.Range(-0.1f, 0.1f); 
            H = Mathf.Repeat(H, 1); 
            S = Mathf.Clamp(S * 0.65f, 0.5f, 0.6f); 
            spriteRenderer.color = Color.HSVToRGB(H, S, V);
        }

        float scaleModifier = 1 + Random.Range(-0.05f, 0.1f); 
        this.gameObject.transform.localScale *= scaleModifier;
    }

    protected override void EndLerp()
    {
        solidCollider2D.isTrigger = false;
        this.rb.bodyType = RigidbodyType2D.Dynamic;
        if(!isInConcert)
        {
            onEnterConcert.Invoke();
            isInConcert = true;
        }
    }

    IEnumerator RequestItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(itemWaitMin, itemWaitMax));
            StartJumpCoroutine();
            wantedItem = TShirtCannon.Instance.SelectRandomShirtType();
            Debug.Log("Attendee wants " + wantedItem);
            thoughtBubble.ShowItemThought(wantedItem);

            float itemPatienceTimer = Random.Range(itemPatienceMin, itemPatienceMax);
            //thoughtBubble.StartTimer(itemPatienceTimer);
            yield return new WaitForSeconds(itemPatienceTimer);
            StopJumpCoroutine();
            if (thoughtBubble.IsOpenedBubble)
            {
                Debug.Log("Attendee Did not get any item");
                OnItemUnfulfilled();
                TriggerAttendeeAngryEffect();
                CalculateMoodstate(-10);
                SetMood(MoodState.Displeased, true, 5);
                thoughtBubble.HideItemThought(wantedItem);

            }
            
        }
    }

    public void OnItemObtained()
    {
        TriggerAttendeeHappyEffect();
        CalculateMoodstate(10);
        SetMood(MoodState.Caught, true, 5);
        onItemFulfilledEvent.Invoke();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus);
        FMODUnity.RuntimeManager.PlayOneShot(CheerAudioPath);
    }

    public void OnItemUnfulfilled()
    {
        TriggerAttendeeAngryEffect();
        SetMood(MoodState.Displeased, true, 5);
        onItemUnfulfilledEvent.Invoke();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScorePenalty);
        CreateTrash();
        FMODUnity.RuntimeManager.PlayOneShot(BooAudioPath);
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            anim.Play(RequestAnimation);
            Jump();
            yield return new WaitForSeconds(.7f);
            anim.Play(ExcitedAnimation);
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

    public void Jump()
    {
        if (rb != null)
        {
            rb.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
        }
    }

    public void CreateTrash()
    {
        if (Trash.Length > 0)
        {
            int randomIndex = Random.Range(0, Trash.Length);
            CrowdTrash trashObject = Instantiate(Trash[randomIndex], transform.position, Quaternion.identity);
            Rigidbody2D trashRb = trashObject.GetComponent<Rigidbody2D>();
            if (trashRb != null)
            {

                float angle = UnityEngine.Random.Range(trashMinAngle, trashMaxAngle);
                Vector2 forceDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

                float forceMagnitude = Random.Range(minTrashForce, maxTrashForce);
                trashRb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Collider Entered" + collider.name);
        if (collider.gameObject.CompareTag("TShirt"))
        {
            Shirt shirt = collider.gameObject.GetComponent<Shirt>();
            if (shirt != null)
            {
                HandleShirtReceived(shirt);
            }

            Destroy(collider.gameObject);
        }
    }

    private void HandleShirtReceived(Shirt shirt)
    {
        RequestableItem receivedShirtType = shirt.ShirtType;
        StopJumpCoroutine();
        if (thoughtBubble.IsOpenedBubble && receivedShirtType == wantedItem)
        {
            OnItemObtained();
        }
        else
        {
            OnItemUnfulfilled();
        }
        if(thoughtBubble.IsOpenedBubble)
        {thoughtBubble.HideItemThought(wantedItem);}
    }

    public void StartAttendeeMovement()
    {
        StartItemCoroutine();
        StartMoveCoroutine();
    }

    public void StopAttendeeMovement()
    {
        StopAllCoroutinesForState();
    }

    public void HandleEventFail(object sender, GameEventArgs e)
    {
        CalculateMoodstate(-10);
        CreateTrash();
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        CalculateMoodstate(5);
    }

    private void StopAllCoroutinesForState()
    {
        StopItemCoroutine();

        if (LerpAttendeeCoroutine != null)
        {
            StopCoroutine(LerpAttendeeCoroutine);
            LerpAttendeeCoroutine = null;
        }

        StopJumpCoroutine();

        if (MoveCoroutine != null)
        {
            StopCoroutine(MoveCoroutine);
            MoveCoroutine = null;
        }
        thoughtBubble.HideItemThought(wantedItem);
    }

    public void StartItemCoroutine()
    {
        //Debug.Log("Attendee starting item coroutine");
        ShirtCoroutine = StartCoroutine(RequestItemRoutine());
    }

    public void StopItemCoroutine()
    {
        if (ShirtCoroutine != null)
        {
            StopCoroutine(ShirtCoroutine);
            ShirtCoroutine = null;
        }
    }

    public void StartJumpCoroutine()
    {
        //Debug.Log("Attendee starting jump coroutine");
        JumpCoroutine = StartCoroutine(JumpRoutine());
    }

    public void StopJumpCoroutine()
    {
        if (JumpCoroutine != null)
        {
            StopCoroutine(JumpCoroutine);
            JumpCoroutine = null;
        }
    }

    public void StartMoveCoroutine()
    {
        //Debug.Log("Attendee starting move coroutine");
        MoveCoroutine = StartCoroutine(RandomMovementRoutine());
    }


    void OnDrawGizmos()
    {
        Vector3 directionMinAngle = Quaternion.Euler(0, 0, trashMinAngle) * transform.right;
        Vector3 directionMaxAngle = Quaternion.Euler(0, 0, trashMaxAngle) * transform.right;

 
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, directionMinAngle * 5);
        Gizmos.DrawRay(transform.position, directionMaxAngle * 5); 

        DrawGizmoArc(transform.position, trashMinAngle, trashMaxAngle, 5); 
    }

    void DrawGizmoArc(Vector3 center, float startAngle, float endAngle, float radius)
    {
        int steps = 20; 
        float angleStep = (endAngle - startAngle) / steps;

        Vector3 previousPoint = center + Quaternion.Euler(0, 0, startAngle) * Vector3.right * radius;
        for (int i = 1; i <= steps; i++)
        {
            Vector3 newDirection = Quaternion.Euler(0, 0, startAngle + angleStep * i) * Vector3.right;
            Vector3 newPoint = center + newDirection * radius;
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = newPoint;
        }
    }

}
