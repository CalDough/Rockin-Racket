using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Bookmark : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private BookmarkPair bookmarkPair;
    [SerializeField] private Image image;
    [SerializeField] private Image block;
    [SerializeField] private TMP_Text text;
    //private Color color;

    public void Initialize(Color color, Bandmate itemType, bool show)
    {
        //this.color = color;
        image.color = color;
        this.text.text = itemType.ToString();
        Show(show);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        bookmarkPair.BookmarkSelected();
    }
    public void Open()
    {
        block.color = new Color(0, 0, 0, 0);
    }
    public void Close()
    {
        block.color = new Color(0, 0, 0, 255);
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
