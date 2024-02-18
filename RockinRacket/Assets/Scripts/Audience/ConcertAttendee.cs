using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConcertAttendee : Attendee
{
    public enum MoodState { Hyped, Frustrated, Pleased }
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
    public MoodState currentMood = MoodState.Pleased;
    public RequestableItem wantedItem = RequestableItem.RedShirt;
    [SerializeField] private CrowdTrash[] Trash; 
    [SerializeField] private ThoughtBubble thoughtBubble; 
    public int maxMoodRating = 100;
    public int minMoodRating = 0;

    public int itemMoodBonus = 25;
    public int itemMoodNegative = -10;
    public int minigameMoodBonus = 15;
    public int minigameMoodNegative = -15;

    public int hypeMoodThreshold = 80;
    public int pleasedMoodThreshold = 45;

    public float itemWaitMin = 15f;
    public float itemWaitMax = 30f;

    public float itemPatienceMin = 15f;
    public float itemPatienceMax = 25f;

    public float trashMinAngle = 0f;
    public float trashMaxAngle = 90f;

    public float jumpStrength = 5f; 

    [Header("Appearance Variables")]
    //public Sprite[] appearanceVariations;
    //private SpriteRenderer sr;
    public Collider2D solidCollider2D;
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ParticleSystem frustratedParticles;
    [SerializeField] ParticleSystem pleasedParticles; 
    [SerializeField] ParticleSystem hypedParticles; 

    [SerializeField] private string HypedAnimation = "Audience_Excited";
    [SerializeField] private string PleasedAnimation = "Audience_Happy";
    [SerializeField] private string FrustratedAnimation = "Audience_Normal";

    private Coroutine ShirtCoroutine;
    private Coroutine LerpAttendeeCoroutine;
    private Coroutine JumpCoroutine;
    private Coroutine MoveCoroutine;

    public override void Init()
    {
 
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

        ConcertEvents.instance.e_ConcertStarted.AddListener(StartAttendeeMovement);
        ConcertEvents.instance.e_ConcertEnded.AddListener(StopAttendeeMovement);
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void Update()
    {
        
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

    public void CalculateAttendeeMoodstate(int moodValueChange)
    {
        currentMoodRating += moodValueChange;
        
        if(currentMoodRating >= hypeMoodThreshold)
        {
            SetMood(MoodState.Hyped);
        }
        else if(currentMoodRating >= pleasedMoodThreshold)
        {
            SetMood(MoodState.Pleased);
        }
        else
        {
            SetMood(MoodState.Frustrated);
        }
    }

    private void SetMood(MoodState mood)
    {
        currentMood = mood;
        switch (mood)
        {
            case MoodState.Hyped:
                anim.Play(HypedAnimation);
                break;
            case MoodState.Pleased:
                anim.Play(PleasedAnimation);
                break;
            case MoodState.Frustrated:
                anim.Play(FrustratedAnimation);
                break;
        }
    }

    public override void TriggerAttendeeHappyEffect()
    {
        if(currentMood == MoodState.Hyped)
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
        sr.sprite = appearanceVariations[Random.Range(0, appearanceVariations.Length)];
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
                CalculateAttendeeMoodstate(itemMoodNegative);
                thoughtBubble.HideItemThought(wantedItem);

            }
            
        }
    }

    public void OnItemObtained()
    {
        TriggerAttendeeHappyEffect();
        CalculateAttendeeMoodstate(itemMoodBonus);
        onItemFulfilledEvent.Invoke();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus);
    }

    public void OnItemUnfulfilled()
    {
        TriggerAttendeeAngryEffect();
        CalculateAttendeeMoodstate(itemMoodNegative);
        onItemUnfulfilledEvent.Invoke();
        ConcertEvents.instance.e_ScoreChange.Invoke(ScorePenalty);
        CreateTrash();
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            anim.Play(HypedAnimation);
            Jump();
            yield return new WaitForSeconds(.7f);
            anim.Play(PleasedAnimation);
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
            Debug.Log("Creating trash object index: " + randomIndex);
            CrowdTrash trashObject = Instantiate(Trash[randomIndex], transform.position, Quaternion.identity);
            Rigidbody2D trashRb = trashObject.GetComponent<Rigidbody2D>();
            trashObject.gameObject.layer = LayerMask.NameToLayer("Trash"); 
            if (trashRb != null)
            {
                Vector2 forceDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
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
        CalculateAttendeeMoodstate(minigameMoodNegative);
        CreateTrash();
    }

    public void HandleEventComplete(object sender, GameEventArgs e)
    {
        CalculateAttendeeMoodstate(minigameMoodBonus);
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
}
