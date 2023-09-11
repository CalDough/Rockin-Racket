using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ItemOption : MonoBehaviour, 
IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public ShopSelection selection;

    public ItemTest item;
    public Image Highlight;
    public Image ItemImage;

    //[SerializeField] private Graphic graphic;

    private void Awake()
    {
        //Highlight.sprite = item.Sprite;
        ItemImage.sprite = item.Sprite;
        Highlight.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Mouse Enter " + item.ItemName);
        Highlight.color = new Color(255, 255, 255, 255);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //print("Mouse Exit " + item.ItemName);
        Highlight.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("Mouse Down " + item.ItemName);
        //selection.setFixed(true);
        selection.SelectItem(item);
    }
}