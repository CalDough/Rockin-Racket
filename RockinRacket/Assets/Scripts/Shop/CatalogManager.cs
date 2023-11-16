using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    recieves button input from resetBtn, cartBtn, buyBtn
    takes calls from ItemOptions when they are clicked to pass to ShopSelection
    passes information to ShopSelection, ShopReceipt, and ShopCatalog
    handles saving and loading with ItemInventory
*/

public class CatalogManager : MonoBehaviour
{
    [SerializeField] private ShopSelection shopSelection;
    [SerializeField] private ShopReceipt shopReceipt;
    [SerializeField] private ShopCatalog shopCatalog;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }

    public void ItemOptionPressed(ItemTest item)
    {
        shopSelection.SelectItem(item, shopReceipt.IsInCart(item));
    }

    public void BuyBtnPressed()
    {
        shopSelection.ResetSelection();
        ItemInventory.AddItems(shopReceipt.GetItemsToBuy());
        shopReceipt.ResetReceipt();
        shopCatalog.UpdateItemOptions(shopReceipt);
    }
    public void CartBtnPressed()
    {
        ItemTest currentItem = shopSelection.GetSelectedItem();
        if (!shopReceipt.IsInCart(currentItem))
        {
            shopReceipt.AddToCart(currentItem);
        }
        else
        {
            shopReceipt.RemoveFromCart(currentItem);
        }
        shopCatalog.UpdateItemOptions(shopReceipt);
    }

    public void ResetBtnPressed()
    {
        ItemInventory.ResetInventory();
        shopSelection.ResetSelection();
        shopReceipt.ResetReceipt();
        shopCatalog.UpdateItemOptions(shopReceipt);
    }

    public void BookmarkPressed(Bandmate bandmate)
    {
        shopSelection.ResetSelection();
        shopCatalog.DisplayItemsByBandmate(ItemInventory.GetItemsByBandmate(bandmate), shopReceipt);
    }
}
