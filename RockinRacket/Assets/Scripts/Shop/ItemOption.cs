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
    //[SerializeField] private Image highlightImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image soldImage;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject equipImageObject;

    private Item item;
    private bool forSale;
    private bool inCart;
    private bool equipped;
    private string costString;
    public Item GetItem() { return item; }

    private void Awake()
    {
        Show(false);
        soldImage.color = new Color(1f, 0f, 0f, 0f);
    }
    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
    // called from ShopCatalog
    public void SetItem(Item item, bool forSale, bool inCart, bool equipped)
    {
        this.item = item;
        itemImage.sprite = item.sprite;
        UpdateItem(forSale, inCart, equipped);
        Show(true);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        catalogManager.SelectItem(item);
        itemImage.sprite = item.selectedSprite;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        catalogManager.ResetSelection();
        if (!inCart)
            itemImage.sprite = item.sprite;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (forSale)
        {
            if (inCart)
            {
                inCart = false;
            }
            else
            {
                inCart = true;
            }
            catalogManager.CartItem(item);
        }
        else
        {
            catalogManager.EquipItem(item);
        }
            
    }
    private void RemoveFromCart()
    {
        if (inCart)
        {
            inCart = false;
            Display();
        }
        //itemImage.color = new Color(1f, 1f, 1f, 1f);
    }
    public void Display()
    {
        equipImageObject.SetActive(false);
        if (forSale)
        {
            //soldImage.color = new Color(1f, 0f, 0f, 0f);
            costString = $"${item.cost}";
            costText.fontStyle = FontStyles.Normal;
            costText.fontSize = 72;
            //itemImage.color = new Color(1f, 1f, 1f, 1f);
            if (inCart)
                itemImage.sprite = item.selectedSprite;
            else
                itemImage.sprite = item.sprite;
        }
        else
        {
            //soldImage.color = new Color(1f, 0f, 0f, 1f);
            costString = "Owned";
            costText.fontStyle = FontStyles.Underline;
            costText.fontSize = 54;
            //itemImage.color = new Color(1f, 1f, 1f, .7f);
            if (equipped)
                equipImageObject.SetActive(true);
            itemImage.sprite = item.sprite;
        }
        

        costText.text = costString;
    }
    public void UpdateItem(bool forSale, bool inCart, bool equipped)
    {
        this.forSale = forSale;
        this.inCart = inCart;
        this.equipped = equipped;
        Display();
    }
    public void ResetItem()
    {
        print("hit!");
        // check if it's a default item
        if (item.cost == 0)
            UpdateItem(true, false, true);
        else
            UpdateItem(true, false, false);
    }
}