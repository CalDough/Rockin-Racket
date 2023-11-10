using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class Battery : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    [SerializeField] private GameObject positionHolder;
    public bool canDrag = true;
    public bool IsPlaced { get; private set; } = false;

    public MicFixing micFixing;
    private RectTransform rectTransform;
    private Canvas canvas; 

    void Start()
    {
        canvas = GetComponentInParent<Canvas>(); 
        rectTransform = GetComponent<RectTransform>();
        this.gameObject.transform.position = positionHolder.transform.position;
        startPosition = this.gameObject.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsPlaced || !canDrag) return;
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsPlaced || !canDrag) return;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out position);
        rectTransform.anchoredPosition = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsPlaced || !canDrag) return;

        foreach (BatterySlot slot in micFixing.batterySlots)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(slot.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera))
            {
                if (slot.IsValidAndEmpty())
                {
                    this.canDrag = false;
                    slot.MarkAsOccupied();
                    StartCoroutine(LerpToPosition(slot.myRect.position, 1.0f)); 

                    break;
                }
            }
        }
    }

    private IEnumerator LerpToPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        IsPlaced = true;
        micFixing.CheckAllBatteriesPlaced();
    }

    public void ResetPosition()
    {
        this.gameObject.transform.position = positionHolder.transform.position;
        this.canDrag = true;
        IsPlaced = false;
    }
}