using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chord : MonoBehaviour
{
    public Vector2 StringStart;
    public Vector2 StringEnd;
    
    public Vector2 GetWorldPosition(Vector2 offset)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        return rectTransform.anchoredPosition + offset;
    }
}
