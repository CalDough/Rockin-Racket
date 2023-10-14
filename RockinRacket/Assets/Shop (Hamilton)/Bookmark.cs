using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Bookmark : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    public Color color;
    public string category;

    void Start()
    {
        image.color = color;
        text.text = category;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HIT!");
    }
}
