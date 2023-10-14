using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Bookmark : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BookmarkManager bookmarkManager;
    [SerializeField] private Image image;
    [SerializeField] private Image block;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Color color;
    [SerializeField] private string category;

    void Start()
    {
        image.color = color;
        text.text = category;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bookmarkManager.SelectBookmark(this);
    }
    public void Open()
    {
        block.color = new Color(0, 0, 0, 0);
    }
    public void Close()
    {
        block.color = new Color(0, 0, 0, 255);
    }
}
