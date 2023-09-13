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
    public Image soldImage;

    private bool forSale;

    private void Awake()
    {
        ItemImage.sprite = item.sprite;
        Highlight.color = new Color(255, 255, 255, 0);
        forSale = true;
        UpdateIsSold();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (forSale)
            Highlight.color = new Color(255, 255, 255, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (forSale)
            Highlight.color = new Color(255, 255, 255, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (forSale)
            selection.SelectItem(this);
    }
    public void AddToCart()
    {
        // TODO does this work?
        ItemImage.color = new Color(255, 255, 255, 100);
    }
    public void RemoveFromCart()
    {
        ItemImage.color = new Color(255, 255, 255, 255);
    }
    public void BuyItem()
    {
        this.forSale = false;
        UpdateIsSold();
    }
    public void UpdateIsSold()
    {
        if (forSale)
            soldImage.color = new Color(255, 255, 255, 0);
        else
            soldImage.color = new Color(255, 255, 255, 255);
    }
}