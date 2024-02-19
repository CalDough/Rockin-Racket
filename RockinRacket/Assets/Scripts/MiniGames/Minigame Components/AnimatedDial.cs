using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class AnimatedDial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform dialBase;
    public RectTransform circularArrow;
    public Animator anim;
    public bool isMatched = false;

    [SerializeField] private float currentAngle = 0f;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool isLocked = false;
    [SerializeField] public float matchingThreshold = 5f; 
    [SerializeField] private float markerAngle; 



    public delegate void DialMatchedEventHandler();
    public event DialMatchedEventHandler OnDialMatched;
    public string dialClickSoundEvent = "";

    void OnDrawGizmos() 
    {
        Vector3 dialPosition = dialBase.position; 
        Gizmos.color = Color.green;
        Vector3 currentDirection = Quaternion.Euler(0, 0, currentAngle) * Vector3.up * 100; 
        Gizmos.DrawLine(dialPosition, dialPosition + currentDirection);

        Gizmos.color = Color.red;

        Vector3 markerDirection = Quaternion.Euler(0, 0, markerAngle) * Vector3.up * 100; 
        Gizmos.DrawLine(dialPosition, dialPosition + markerDirection);
    }

    public void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dialClickSoundEvent);
    }

    void Start()
    {
        CheckForMatchingAngle();
        RandomizeDial();
        anim.Play("ShowMarker");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Dial OnPointerDown");
        if (!isLocked)
        {
            isDragging = true;
            //Debug.Log("Started Dragging Dial");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        //Debug.Log("Stopped Dragging Dial");
        CheckForMatchingAngle();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dial OnDrag");
        if (isDragging && !isLocked)
        {
            currentAngle = CalculateAngle(eventData.position);
            UpdateDialPosition(currentAngle);
            CheckForMatchingAngle();
        }
    }

    private void UpdateDialPosition(float angle)
    {

        dialBase.localEulerAngles = new Vector3(0, 0, angle);

    }

    private float CalculateAngle(Vector2 dragPosition)
    {
        Vector2 direction = dragPosition - (Vector2)dialBase.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; 
        return angle;
    }


    private void CheckForMatchingAngle() 
    {
        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, markerAngle)) <= matchingThreshold && !isLocked) {
            isLocked = true; 
            
            anim.Play("HideMarker");
            StartCoroutine(LerpToMatchAngle(markerAngle));
        }
    }

    public void RandomizeDial() 
    {
        markerAngle = Random.Range(0, 360);
        circularArrow.localEulerAngles = new Vector3(0, 0, markerAngle);

        float dialStartAngle;

        do {dialStartAngle = Random.Range(0, 360);} 
        while (Mathf.Abs(Mathf.DeltaAngle(dialStartAngle, markerAngle)) < matchingThreshold + 15); 

        currentAngle = dialStartAngle;
        UpdateDialPosition(currentAngle);

        isLocked = false;
    }


    private IEnumerator LerpToMatchAngle(float targetAngle) 
    {
        float duration = 0.25f;
        float time = 0;
        float startAngle = currentAngle;

        while (time < duration) {
            time += Time.deltaTime;
            float t = time / duration;
            currentAngle = Mathf.LerpAngle(startAngle, targetAngle, t);
            dialBase.localEulerAngles = new Vector3(0, 0, currentAngle);
            yield return null;
        }
        PlaySound();
        DialMatched();
        isMatched = true; 
    }
    public void DialMatched()
    {
        Debug.Log("Dial: Dial Matched");
        OnDialMatched?.Invoke();
    }
}