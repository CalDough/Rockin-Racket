using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrowdMember : MonoBehaviour
{
    public enum MoodState { Hyped, Frustrated, Pleased }

    [Header("Required Fields")]
    public GameObject trashPrefab;
    public GameObject thoughtBubble;

    [SerializeField] private MoodState currentMood = MoodState.Pleased;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] ParticleSystem goodParticles;
    [SerializeField] ParticleSystem badParticles; 

    [Header("Concert Variables")]
    [SerializeField] public int groupSize = 3;
    [SerializeField] private float concertRating = 5f;

    [Header("Private Variables")]
    [SerializeField] private bool wantsTShirt = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector2 movementRange = new Vector2(1f, 0f); 
    [SerializeField] private float moveSpeed = 1f; 

    [Header("Animation States")]
    [SerializeField] private string HypedAnimation = "Audience_Happy";
    [SerializeField] private string PleasedAnimation = "Audience_Excited";
    [SerializeField] private string FrustratedAnimation = "Audience_Normal";

    private GameObject currentTrash; // crowd members spawned trash, to be removed later depending on design change
    private Vector3 originalPosition;
    private Coroutine wantTShirtCoroutine;
    private Coroutine currentMoveCoroutine; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        StartCoroutine(RandomMovementRoutine());
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    void Update()
    {
        if (isJumping)
        {
            Jump();
            characterAnimator.Play(HypedAnimation); 
        }
        else
        {
            UpdateAnimationBasedOnMood();
        }
    }

    public void StartWantingShirts()
    {
        StartCoroutine( WantTShirtRoutine());
    }

    IEnumerator WantTShirtRoutine()
    {
        wantsTShirt = true;
        isJumping = true;
        thoughtBubble.SetActive(true);
        yield return new WaitForSeconds(20f); 

        if (wantsTShirt) 
        {
            badParticles.Play();
            if (concertRating > 5) 
            {
                UpdateConcertRating(-1); 
            }
            StopJumping();
        }
        wantTShirtCoroutine = null; 
    }

    public float GetConcertRating()
    {
        return concertRating;
    }

    public void UpdateConcertRating(float amount)
    {
        concertRating = Mathf.Clamp(concertRating + amount, 0, 10);
        UpdateAnimationBasedOnMood(); 
    }
    private void UpdateAnimationBasedOnMood()
    {
        if (concertRating <= 5)
        {
            currentMood = MoodState.Frustrated;
            characterAnimator.Play(FrustratedAnimation);
        }
        else
        {
            currentMood = MoodState.Pleased;
            characterAnimator.Play(PleasedAnimation);
        }
    } 
    
    private void StopAllCoroutinesOnEnd()
    {
        if (wantTShirtCoroutine != null)
        {
            StopCoroutine(wantTShirtCoroutine);
            wantTShirtCoroutine = null;
        }
        StopJumping();
    }

    void StopJumping()
    {
        wantsTShirt = false;
        isJumping = false;
        thoughtBubble.SetActive(false); 
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }

    public void ChangeMood(MoodState newMood)
    {
        currentMood = newMood;
    }

    public void ThrowTrash()
    {
        if(!CrowdController.Instance.CanSpawnTrash() )
        { return;}

        currentTrash = Instantiate(trashPrefab, transform.position, Quaternion.identity);
        currentTrash.layer = LayerMask.NameToLayer("Trash"); 
        Rigidbody2D trashRb = currentTrash.GetComponent<Rigidbody2D>();
        Collider2D trashCollider = currentTrash.GetComponent<Collider2D>();

        if (trashCollider != null)
        {
            trashCollider.enabled = false;
        }

        Vector2 throwDirection = GetRandomUpwardDirection();
        trashRb.AddForce(throwDirection * Random.Range(5f, 10f), ForceMode2D.Impulse);
        CrowdController.Instance.currentTrashCount++;
        StartCoroutine(ActivateTrashProjectile(currentTrash));
    }

    IEnumerator ActivateTrashProjectile(GameObject trash)
    {
        if (trash != null)
        {
            CrowdTrash crowdTrash = trash.GetComponent<CrowdTrash>();
            if (crowdTrash != null)
            {crowdTrash.IsProjectile = false;}
        }
        yield return new WaitForSeconds(0.3f);
        if (trash != null)
        {
            Collider2D trashCollider = trash.GetComponent<Collider2D>();
            if (trashCollider != null)
            {
                trashCollider.enabled = true; 
            }

            CrowdTrash crowdTrash = trash.GetComponent<CrowdTrash>();
            if (crowdTrash != null)
            {
                crowdTrash.IsProjectile = true;
            }
        }
    }

    Vector2 GetRandomUpwardDirection()
    {
        float angle = Random.Range(-45f, 45f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        /*
        if (collider.gameObject.CompareTag("Trash") && collider.gameObject != currentTrash)
        {
            Debug.Log("Trigger entered trash");
            CrowdTrash crowdTrash = collider.gameObject.GetComponent<CrowdTrash>();
            if (crowdTrash != null && crowdTrash.IsProjectile)
            {
                Debug.Log("Trigger destry trash");
                Destroy(collider.gameObject); 
                StartCoroutine(HandleTrashCollision());
            }
        }
        */
        if (collider.gameObject.CompareTag("TShirt"))
        {
            if (wantsTShirt)
            {
                UpdateConcertRating(CrowdController.Instance.tShirtRatingBonus);
                goodParticles.Play();
            }
            else
            {
                badParticles.Play();
            }
            StopJumping();
            Destroy(collider.gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 newTargetPosition = originalPosition - (transform.position - originalPosition);
            newTargetPosition.x = Mathf.Clamp(newTargetPosition.x, originalPosition.x - movementRange.x, originalPosition.x + movementRange.x);

            if (currentMoveCoroutine != null)
            {
                StopCoroutine(currentMoveCoroutine);
            }
            currentMoveCoroutine = StartCoroutine(MoveTowardsPosition(newTargetPosition));
        }
    }

    IEnumerator RandomMovementRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f)); 
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        float moveDistance = Random.Range(-movementRange.x, movementRange.x);
        Vector3 targetPosition = new Vector3(originalPosition.x + moveDistance, transform.position.y, transform.position.z);
        targetPosition.x = Mathf.Clamp(targetPosition.x, originalPosition.x - movementRange.x, originalPosition.x + movementRange.x);

        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
        }
        currentMoveCoroutine = StartCoroutine(MoveTowardsPosition(targetPosition));
    }

    IEnumerator MoveTowardsPosition(Vector3 targetPosition)
    {
        spriteRenderer.flipX = (targetPosition.x > transform.position.x);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    /*
    IEnumerator HandleTrashCollision()
    {
        concertRating = Mathf.Clamp(concertRating - concertRatingDecreaseForGettingHit, 0, 10);
        ShowAngryFace();
        yield return new WaitForSeconds(Random.Range(trashStickDurationMin, trashStickDurationMax));
        HideAngryFace();
        ResetTrashSpawnTimer(); 
        ThrowTrash(); 
    }
    */

    //WIP AREA
    private void ShowAngryFace()
    {
       
    }

    private void HideAngryFace()
    {

    }


    private void SubscribeEvents()
    {
        StateEvent.OnStateStart += HandleGameStateStart;
        StateEvent.OnStateEnd += HandleGameStateEnd;
    }

    private void UnsubscribeEvents()
    {
        StateEvent.OnStateStart -= HandleGameStateStart;
        StateEvent.OnStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                break;
            default:
                break;
        }
    }
    
    private void HandleGameStateEnd(object sender, StateEventArgs e)
    {
        switch(e.state.stateType)
        {
            case StateType.Song:
                StopAllCoroutinesOnEnd();
                break;
            default:
                break;
        }
    }
}
