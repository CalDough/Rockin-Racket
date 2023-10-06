using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Object References")]
    [SerializeField] Image trashImage;
    public Vector3 startPosition;

    CanvasGroup cGroup;


    private void Start()
    {
        startPosition = transform.position;
        cGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        trashImage.raycastTarget = false;
        cGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("END DRAG DRAGGABLe");
        //transform.position = startPosition;
        trashImage.raycastTarget = true;
        cGroup.blocksRaycasts = true;
    }
}
