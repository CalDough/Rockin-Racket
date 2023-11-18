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
    private Coroutine tShirtRoutine;

    [SerializeField] private float jumpForce = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        tShirtRoutine = StartCoroutine(WantTShirtRoutine());
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
            yield return new WaitForSeconds(Random.Range(10f, 30f));
            wantsTShirt = true;
            isJumping = true;
            thoughtBubble.SetActive(true); 
            yield return new WaitForSeconds(20f); 
            StopJumping();
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
        GameObject trash = Instantiate(trashPrefab, transform.position, Quaternion.identity);
        Rigidbody2D trashRb = trash.GetComponent<Rigidbody2D>();
        Vector2 throwDirection = GetRandomUpwardDirection();
        trashRb.AddForce(throwDirection * Random.Range(5f, 10f), ForceMode2D.Impulse);
    }

    Vector2 GetRandomUpwardDirection()
    {
        float angle = Random.Range(-45f, 45f) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trash"))
        {
            StartCoroutine(HandleTrashCollision(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("TShirt"))
        {
            if (wantsTShirt)
            {
                concertRating = Mathf.Clamp(concertRating + 1, 0, 10);
                StopCoroutine(tShirtRoutine);
                StopJumping();
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator HandleTrashCollision(GameObject trash)
    {
        CrowdTrash crowdTrash = trash.GetComponent<CrowdTrash>();
        if (crowdTrash && !crowdTrash.IsProjectile)
        {
            crowdTrash.StickToMember(gameObject);
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            crowdTrash.MakeProjectile();
            ThrowTrash(); 
        }
    }
}