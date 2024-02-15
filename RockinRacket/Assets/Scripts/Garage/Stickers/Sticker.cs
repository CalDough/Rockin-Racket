using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[System.Serializable]
public class Sticker : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    public Bandmate bandmate;
    private bool draggable = false;
    private Vector3 mouseDiff;

    private void Update()
    {
        if (draggable)
        {
            transform.position = new Vector3(Input.mousePosition.x + mouseDiff.x, Input.mousePosition.y + mouseDiff.y, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        mouseDiff = transform.position - Input.mousePosition;
        draggable = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        draggable = false;
        StickerSaver.UpdateSticker(bandmate, transform);
    }
    public void SetDraggable()
    {
        draggable = true;
        print("dragging");
    }
}
