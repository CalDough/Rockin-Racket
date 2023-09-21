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
        //Highlight.color = new Color(1f, 1f, 1f, 0f);
        UpdateIsSold();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (forSale)
            Highlight.color = new Color(1f, 1f, 1f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (forSale)
            Highlight.color = new Color(1f, 1f, 1f, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (forSale)
            selection.SelectItem(this);
    }
    public void AddToCart()
    {
        ItemImage.color = new Color(1f, 1f, 1f, .7f);
    }
    public void RemoveFromCart()
    {
        if (forSale)
        ItemImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void BuyItem()
    {
        this.forSale = false;
        // add item to inventory
        Inventory.AddItem(item);
        UpdateIsSold();
    }
    public void UpdateIsSold()
    {
        forSale = !Inventory.ContainsItem(item);
        if (forSale)
            soldImage.color = new Color(1f, 1f, 1f, 0f);
        else
            soldImage.color = new Color(1f, 1f, 1f, 1f);
    }
}