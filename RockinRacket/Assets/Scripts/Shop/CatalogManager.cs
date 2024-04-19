using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    recieves button input from resetBtn, cartBtn, buyBtn
    takes calls from ItemOptions when they are clicked to pass to ShopSelection
    passes information to ShopSelection, ShopReceipt, and ShopCatalog
*/

public class CatalogManager : MonoBehaviour
{
    [SerializeField] private ShopSelection shopSelection;
    [SerializeField] private ShopReceipt shopReceipt;
    [SerializeField] private ShopCatalog shopCatalog;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopAudio shopAudio;
    [SerializeField] private TMP_Text moneyText;

    public Bandmate CurrentBandmate { get; private set; }
    private bool moneyAnim;
    private readonly float MoneyColorAnimTime = .5f;
    private bool startingPage = true;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }
    // called by shopmanager at start, then when items bought
    public void UpdateMoneyText() { moneyText.text = "$" + GameManager.Instance.globalMoney; print(GameManager.Instance.globalMoney); }

    // THESE ARE CALLED BY ITEM OPTION
    public void SelectItem(Item item)
    {
        shopSelection.SelectItem(item);
    }
    public void CartItem(Item item)
    {
        AddToCart(item);
    }
    public void EquipItem(Item item)
    {
        Equip(item);
    }


    // called from receipt when items bought
    public void BuyBtnPressed()
    {
        Item[] itemsToBuy = shopReceipt.GetItemsToBuy();
        int cost = 0;
        if (itemsToBuy.Length > 0)
        {
            foreach (Item item in itemsToBuy)
                cost += item.cost;

            if (cost <= GameManager.Instance.globalMoney)
            {
                GameManager.Instance.globalMoney -= cost;
                UpdateMoneyText();
                ItemInventory.AddItems(shopReceipt.GetItemsToBuy());
                shopManager.Bought = true;
                shopReceipt.ResetReceipt();
                shopCatalog.UpdateItemOptions(shopReceipt);
                //shopSelection.UpdateText();
                shopAudio.PlayCheckout();
            }
            else if (!moneyAnim)
                StartCoroutine(NotEnoughMoneyAnim());
        }
    }

    public void CartBtnPressed()
    {
        Item currentItem = shopSelection.GetSelectedItem();
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
        Item currentItem = shopSelection.GetSelectedItem();
        ItemInventory.EquipItem(CurrentBandmate, currentItem);
        shopCatalog.UpdateItemOptions(shopReceipt);
    }

    //public void ResetBtnPressed()
    //{
    //    ItemInventory.ResetInventory();
    //    shopSelection.ResetSelection();
    //    shopReceipt.ResetReceipt();
    //    shopCatalog.UpdateItemOptions(shopReceipt);
    //}

    // called by BookmarkManager on start
    public void InitBookmarks(Bandmate bandmate)
    {
        CurrentBandmate = bandmate;
        shopSelection.ResetSelection(bandmate);
        shopCatalog.DisplayItemsByBandmate(ItemInventory.GetItemsByBandmate(bandmate), shopReceipt);
    }
    // called by BookmarkManager when bookmark pressed
    public void BookmarkPressed(Bandmate bandmate)
    {
        if (CurrentBandmate != bandmate)
        {
            CurrentBandmate = bandmate;
            shopSelection.ResetSelection(bandmate);
            shopCatalog.DisplayItemsByBandmate(ItemInventory.GetItemsByBandmate(bandmate), shopReceipt);
            shopAudio.PlayPageTurn();
        }
        
    }
    // called by itemOption when mouse exits
    public void ResetSelection()
    {
        shopSelection.ResetSelection();
    }

    private IEnumerator NotEnoughMoneyAnim()
    {
        moneyAnim = true;
        Color normalColor = new(1f, 1f, 1f);
        Color goalColor = new(1f, .1f, .1f);
        Vector3 startSize = new(1f, 1f, 1f);
        Vector3 goalSize = new(1.3f, 1.3f, 1f);
        float counter = 0;

        // choose the portion of the animation that is leadup vs retract
        float firstHalf = MoneyColorAnimTime * .5f;
        float secondHalf = MoneyColorAnimTime * .5f;

        while (counter < firstHalf)
        {
            counter += Time.deltaTime;
            moneyText.color = Color.Lerp(normalColor, goalColor, counter / firstHalf);
            moneyText.transform.localScale = Vector3.Lerp(startSize, goalSize, counter / firstHalf);
            yield return null;
        }
        counter = 0;
        while (counter < secondHalf)
        {
            counter += Time.deltaTime;
            moneyText.color = Color.Lerp(goalColor, normalColor, counter / secondHalf);
            moneyText.transform.localScale = Vector3.Lerp(goalSize, startSize, counter / secondHalf);
            yield return null;
        }
        moneyAnim = false;
    }

    private void AddToCart(Item item)
    {
        if (!shopReceipt.IsInCart(item))
        {
            shopReceipt.AddToCart(item);
        }
        else
        {
            shopReceipt.RemoveFromCart(item);
        }
        //shopCatalog.UpdateItemOptions(shopReceipt);
    }
    private void Equip(Item item)
    {
        ItemInventory.EquipItem(CurrentBandmate, item);
        shopCatalog.UpdateItemOptions(shopReceipt);
    }
}
