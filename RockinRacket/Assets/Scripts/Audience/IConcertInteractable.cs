using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConcertInteractable
{
    void ClickInteraction();
    void OnDragStart(Vector2 point);
    void OnDrag(Vector2 point);
    void OnDragEnd(Vector2 point);
}
