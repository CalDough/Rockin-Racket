using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrowdMember : MonoBehaviour
{
    public enum MoodState { Hyped, Frustrated, Pleased }
    public GameObject trashPrefab;
    public GameObject thoughtBubble;

    [SerializeField] private MoodState currentMood = MoodState.Pleased;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;
    [SerializeField] private float concertRating = 5f;
    [SerializeField] private bool wantsTShirt = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float tshirtIntervalMin = 20f;
    [SerializeField] private float tshirtIntervalMax = 40f;
    [SerializeField] private float trashSpawnIntervalMin = 20f;
    [SerializeField] private float trashSpawnIntervalMax = 40f;
    [SerializeField] private float trashStickDurationMin = 1f;
    [SerializeField] private float trashStickDurationMax = 3.5f;
    [SerializeField] private float concertRatingIncreaseForThrowingTrash = 1f;
    [SerializeField] private float concertRatingDecreaseForGettingHit = 2f;

    private Coroutine tShirtRoutine;
    private Coroutine trashSpawnRoutine;

    [SerializeField] private GameObject currentTrash; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tShirtRoutine = StartCoroutine(WantTShirtRoutine());
        trashSpawnRoutine = StartCoroutine(TrashSpawnRoutine());
    }

    void Update()
    {
        if (isJumping)
        {
            Jump();
        }
    }

    IEnumerator WantTShirtRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(tshirtIntervalMin, tshirtIntervalMax));
            wantsTShirt = true;
            isJumping = true;
            thoughtBubble.SetActive(true); 
            yield return new WaitForSeconds(20f); 
            StopJumping();
        }
    }

    IEnumerator TrashSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(trashSpawnIntervalMin, trashSpawnIntervalMax));
            ThrowTrash();
        }
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
        else if (collider.gameObject.CompareTag("TShirt"))
        {
            if (wantsTShirt)
            {
                concertRating = Mathf.Clamp(concertRating + 1, 0, 10);
                StopCoroutine(tShirtRoutine);
                StopJumping();
            }
            Destroy(collider.gameObject);
        }
    }

    IEnumerator HandleTrashCollision()
    {
        concertRating = Mathf.Clamp(concertRating - concertRatingDecreaseForGettingHit, 0, 10);
        ShowAngryFace();
        yield return new WaitForSeconds(Random.Range(trashStickDurationMin, trashStickDurationMax));
        HideAngryFace();
        ResetTrashSpawnTimer(); 
        ThrowTrash(); 
    }

    private void ShowAngryFace()
    {
       
    }

    private void HideAngryFace()
    {

    }

    private void ResetTrashSpawnTimer()
    {
        if (trashSpawnRoutine != null)
        {
            StopCoroutine(trashSpawnRoutine);
        }
        trashSpawnRoutine = StartCoroutine(TrashSpawnRoutine());
    }
}
