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
    [SerializeField] private ShopAudio shopAudio;

    private Bandmate currentBandmate;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }

    public void ItemOptionPressed(ItemTest item)
    {
        shopSelection.SelectItem(item);
    }

    // called from receipt when items bought
    public void BuyBtnPressed()
    {
        ItemInventory.AddItems(shopReceipt.GetItemsToBuy());
        shopReceipt.ResetReceipt();
        shopCatalog.UpdateItemOptions(shopReceipt);
        shopSelection.UpdateText();
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

    public void EquipBtnPressed()
    {
        ItemTest currentItem = shopSelection.GetSelectedItem();
        ItemInventory.EquipItem(currentBandmate, currentItem);
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
        shopAudio.PlayFlipPage();
        currentBandmate = bandmate;
        shopSelection.ResetSelection();
        shopCatalog.DisplayItemsByBandmate(ItemInventory.GetItemsByBandmate(bandmate), shopReceipt);
    }
}
