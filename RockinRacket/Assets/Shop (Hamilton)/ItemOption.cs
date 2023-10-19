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
    private bool inCart;
    private bool equipped;
    public bool IsEquipped() { return equipped; }

    private void Awake()
    {
        Show(false);
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    public void SetItem(ItemTest item, bool forSale, bool inCart)
    {
        this.item = item;
        ItemImage.sprite = item.sprite;
        UpdateOption(forSale, inCart);
        Show(true);
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
            selection.SelectItem(item);
    }
    public void RemoveFromCart()
    {
        if (forSale)
        ItemImage.color = new Color(1f, 1f, 1f, 1f);
    }
    //public void BuyItem()
    //{
    //    //forSale = false;
    //    // add item to inventory
    //    //ItemInventory.AddItem(item);
    //    //UpdateOption();
    //}
    // TODO
    public void EquipItem()
    {

    }
    public void UpdateOption(bool forSale, bool inCart)
    {
        this.forSale = forSale;
        this.inCart = inCart;
        if (forSale)
        {
            soldImage.color = new Color(1f, 1f, 1f, 0f);
            ItemImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            soldImage.color = new Color(1f, 1f, 1f, 1f);
            ItemImage.color = new Color(1f, 1f, 1f, .7f);
        }
        if (inCart)
            ItemImage.color = new Color(1f, 1f, 1f, .7f);
    }
    public void ResetItem()
    {
        UpdateOption(true, false);
    }
}