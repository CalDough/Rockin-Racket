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
    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private Image highlightImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image soldImage;
    [SerializeField] private GameObject equipImageObject;

    private ItemTest item;
    private bool forSale;
    private bool equipped;
    public ItemTest GetItem() { return item; }

    private void Awake()
    {
        Show(false);
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
    // called from ShopCatalog
    public void SetItem(ItemTest item, bool forSale, bool inCart, bool equipped)
    {
        this.item = item;
        itemImage.sprite = item.sprite;
        UpdateOption(forSale, inCart, equipped);
        Show(true);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (forSale)
            highlightImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (forSale)
            highlightImage.color = new Color(1f, 1f, 1f, 0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //if (forSale)
            catalogManager.ItemOptionPressed(item);
    }
    public void RemoveFromCart()
    {
        if (forSale)
        itemImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void UpdateOption(bool forSale, bool inCart, bool equipped)
    {
        equipImageObject.SetActive(false);
        this.forSale = forSale;
        this.equipped = equipped;
        if (forSale)
        {
            soldImage.color = new Color(1f, 1f, 1f, 0f);
            itemImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            soldImage.color = new Color(1f, 1f, 1f, 1f);
            itemImage.color = new Color(1f, 1f, 1f, .7f);
            if (equipped)
                equipImageObject.SetActive(true);
        }
        if (inCart)
            itemImage.color = new Color(1f, 1f, 1f, .7f);
    }
    public void ResetItem()
    {
        print("hit!");
        // check if it's a default item
        if (item.cost == 0)
            UpdateOption(true, false, true);
        else
            UpdateOption(true, false, false);
    }
}