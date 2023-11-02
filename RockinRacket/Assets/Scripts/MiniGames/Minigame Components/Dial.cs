using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using TMPro;

using FMODUnity;

public class Dial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public TextMeshProUGUI angleText;
    public RectTransform center;
    public RectTransform handle;

    public bool allowLooping = false;

    public RectTransform marker; // Marker UI object
    public float matchingThreshold = 5f; // The range within which the dial's angle should match the marker's angle
    public float markerAngle; // The angle at which the marker is set
    public bool MatchingAngle { get; private set; } 

    public float minValue = 0;
    public float maxValue = 100;
    public float currentValue;
    public float startAngle = 0;
    public float endAngle = 360;
    public float radius = 100; // Set the radius for the circular slider
    public float maxRateOfChange = 10f; // Set the max rate of angle change in degrees per frame
    
    private bool isDragging;
    private RectTransform rectTransform;
    private Vector2 centerPosition;
    public float currentAngle;
    
    public bool lockable = true;
    public bool isLocked = false;
    public delegate void DialMatchedEventHandler();
    public event DialMatchedEventHandler OnDialMatched;
    public string dialClickSoundEvent = "";

    public float soundStartVolume = 1;

    public void PlaySound()
    {
        if (!string.IsNullOrEmpty(dialClickSoundEvent))
        {
            FMOD.Studio.EventInstance soundInstance = RuntimeManager.CreateInstance(dialClickSoundEvent);
            soundInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            soundInstance.setVolume(soundStartVolume);
            soundInstance.start();
            soundInstance.release();
        }
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        centerPosition = rectTransform.rect.center;
        currentAngle = startAngle;
        UpdateHandle(currentAngle);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isLocked)
        {return;}
        
        if (isDragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
            float targetAngle = Mathf.Atan2(localPoint.y - centerPosition.y, localPoint.x - centerPosition.x) * Mathf.Rad2Deg;
            if (targetAngle < 0) targetAngle += 360;

            float angleDifference = Mathf.DeltaAngle(currentAngle % 360, targetAngle);
            angleDifference = Mathf.Clamp(angleDifference, -maxRateOfChange, maxRateOfChange);

            currentAngle += angleDifference;

            if (allowLooping)
            {
                if (currentAngle > endAngle) currentAngle = startAngle + (currentAngle - endAngle);
                else if (currentAngle < startAngle) currentAngle = endAngle - (startAngle - currentAngle);
            }
            else
            {
                currentAngle = Mathf.Clamp(currentAngle, startAngle, endAngle);
            }

            UpdateHandle(currentAngle % 360);

            currentValue = Mathf.Lerp(minValue, maxValue, (currentAngle - startAngle) / (endAngle - startAngle));
        }
    }

    private void UpdateHandle(float angle)
    {
        
        Vector2 newPos = new Vector2(centerPosition.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius, centerPosition.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
        handle.localPosition = newPos;

        Vector2 directionToCenter = centerPosition - newPos;
        float rotationAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;

        handle.localEulerAngles = new Vector3(0, 0, rotationAngle - 90);
        center.localEulerAngles = new Vector3(0, 0, angle);
        
        angleText.text = Mathf.RoundToInt(angle).ToString();

        CheckMatchingAngle();
    }   


    private void OnDrawGizmos()
    {
        if (rectTransform != null)
        {
            Vector2 worldCenter = rectTransform.TransformPoint(centerPosition);
            
            // Draw line and sphere for start angle
            float startAngleInRadians = startAngle * Mathf.Deg2Rad;
            Vector2 worldStartHandlePos = worldCenter + new Vector2(Mathf.Cos(startAngleInRadians), Mathf.Sin(startAngleInRadians)) * radius;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(worldCenter, worldStartHandlePos);
            Gizmos.DrawSphere(worldStartHandlePos, 5);
            
            // Draw line and sphere for end angle
            float endAngleInRadians = endAngle * Mathf.Deg2Rad;
            Vector2 worldEndHandlePos = worldCenter + new Vector2(Mathf.Cos(endAngleInRadians), Mathf.Sin(endAngleInRadians)) * radius;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(worldCenter, worldEndHandlePos);
            Gizmos.DrawSphere(worldEndHandlePos, 5);
            
            // Draw line and sphere for current angle
            float currentAngleInRadians = currentAngle * Mathf.Deg2Rad;
            Vector2 worldCurrentHandlePos = worldCenter + new Vector2(Mathf.Cos(currentAngleInRadians), Mathf.Sin(currentAngleInRadians)) * radius;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(worldCenter, worldCurrentHandlePos);
            Gizmos.DrawSphere(worldCurrentHandlePos, 5);
        }
    }

    public void SetMarkerAngle(float angle)
    {
        markerAngle = angle;
        Vector2 newPos = new Vector2(centerPosition.x + Mathf.Cos(angle * Mathf.Deg2Rad) * radius, centerPosition.y + Mathf.Sin(angle * Mathf.Deg2Rad) * radius);
        marker.localPosition = newPos;
        marker.gameObject.SetActive(true);

        // Make the marker face the center
        Vector2 directionToCenter = centerPosition - newPos;
        float rotationAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        marker.localEulerAngles = new Vector3(0, 0, rotationAngle - 90);
    }

    private void CheckMatchingAngle()
    {
        if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, markerAngle)) <= matchingThreshold)
        {
            if(lockable && isLocked != true)
            {
                isLocked = true;
                StartCoroutine(LerpToMarkerAngle());
            }
            else
            {  
                MatchingAngle = true;
                OnDialMatched?.Invoke(); 
            }
        }
        else
        {
            MatchingAngle = false;
        }
    }

    private IEnumerator LerpToMarkerAngle()
    {
        float duration = .25f;
        float elapsed = 0f; 

        float startAngle = currentAngle; 

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            
            currentAngle = Mathf.LerpAngle(startAngle, markerAngle, t);

            UpdateHandle(currentAngle % 360);
            yield return null;
        }

        currentAngle = markerAngle; 
        UpdateHandle(currentAngle % 360);
        MatchingAngle = true;
        OnDialMatched?.Invoke(); 
        PlaySound();
    }
}