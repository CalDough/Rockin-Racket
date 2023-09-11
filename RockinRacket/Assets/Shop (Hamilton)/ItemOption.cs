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

    private bool isBought;

    //[SerializeField] private Graphic graphic;

    private void Awake()
    {
        //Highlight.sprite = item.Sprite;
        ItemImage.sprite = item.Sprite;
        Highlight.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isBought)
            //print("Mouse Enter " + item.ItemName);
            Highlight.color = new Color(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isBought)
            //print("Mouse Exit " + item.ItemName);
            Highlight.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isBought)
            //print("Mouse Down " + item.ItemName);
            //selection.setFixed(true);
            selection.SelectItem(this);
    }

    public void setBought(bool bought)
    {
        isBought = bought;
        ItemImage.color = new Color(0, 0, 0, 255);
    }
}