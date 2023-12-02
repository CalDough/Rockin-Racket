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

    private Bandmate currentBandmate;
    private bool moneyAnim;
    private readonly float MoneyColorAnimTime = .5f;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }
    // called by shopmanager at start, then when items bought
    public void UpdateMoneyText() { moneyText.text = "$" + shopManager.Money; }

    public void ItemOptionPressed(ItemTest item)
    {
        shopSelection.SelectItem(item);
    }

    // called from receipt when items bought
    public void BuyBtnPressed()
    {
        ItemTest[] itemsToBuy = shopReceipt.GetItemsToBuy();
        int cost = 0;
        // TODO: check if you have enough money
        if (itemsToBuy.Length > 0)
        {
            foreach (ItemTest item in itemsToBuy)
                cost += item.cost;

            if (cost <= shopManager.Money)
            {
                shopManager.Money -= cost;
                UpdateMoneyText();
                ItemInventory.AddItems(shopReceipt.GetItemsToBuy());
                shopManager.Bought = true;
                shopReceipt.ResetReceipt();
                shopCatalog.UpdateItemOptions(shopReceipt);
                shopSelection.UpdateText();
            }
            else if (!moneyAnim)
                StartCoroutine(NotEnoughMoneyAnim());
        }
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

    private IEnumerator NotEnoughMoneyAnim()
    {
        moneyAnim = true;
        Color normalColor = new(1f, 1f, 1f);
        Color goalColor = new(1f, .5f, .5f);
        float counter = 0;

        // choose the portion of the animation that is leadup vs retract
        float firstHalf = MoneyColorAnimTime * .5f;
        float secondHalf = MoneyColorAnimTime * .5f;

        while (counter < firstHalf)
        {
            counter += Time.deltaTime;
            moneyText.color = Color.Lerp(normalColor, goalColor, counter / firstHalf);
            yield return null;
        }
        counter = 0;
        while (counter < secondHalf)
        {
            counter += Time.deltaTime;
            moneyText.color = Color.Lerp(goalColor, normalColor, counter / secondHalf);
            yield return null;
        }
        moneyAnim = false;
    }
}
