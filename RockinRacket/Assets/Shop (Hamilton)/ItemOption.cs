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
    public CatalogManager catalogManager;

    public ItemTest item;
    public Image Highlight;
    public Image ItemImage;
    public Image soldImage;

    private bool forSale;
    private bool equipped;
    //public bool IsEquipped() { return equipped; }

    private void Awake()
    {
        Show(false);
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
    // called from ShopCatalog
    public void SetItem(ItemTest item, bool inCart, bool forSale, bool equipped)
    {
        this.item = item;
        ItemImage.sprite = item.sprite;
        UpdateOption(forSale, inCart, equipped);
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
            catalogManager.ItemOptionPressed(item);
    }
    public void RemoveFromCart()
    {
        if (forSale)
        ItemImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void UpdateOption(bool inCart, bool forSale, bool equipped)
    {
        this.forSale = forSale;
        this.equipped = equipped;
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
        // check if it's a default item
        if (item.cost == 0)
            UpdateOption(false, true, true);
        else
            UpdateOption(false, true, false);

    }
}