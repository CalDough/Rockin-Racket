using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelection : MonoBehaviour
{
    public ShopReceipt receipt;

    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text cartButtonText;

    private ItemTest selectedItem;

    public void SelectItem(ItemTest itemTest)
    {
        selectedItem = itemTest;
        UpdateSelection();
    }
    public void CartBtnPressed()
    {
        if (receipt.IsInCart(selectedItem))
            receipt.RemoveFromCart(selectedItem);
        else
            receipt.AddToCart(selectedItem);
        UpdateSelection();
    }
    public void UpdateSelection()
    {
        if (selectedItem == null)
        {
            nameText.text = "Item Name";
            descriptionText.text = "Item Description";
            costText.text = "Item Cost";
            cartButtonText.text = "Add To Cart";
            return;
        }
        nameText.text = selectedItem.itemName;
        descriptionText.text = selectedItem.description;
        costText.text = "$" + selectedItem.cost.ToString();
        if (receipt.IsInCart(selectedItem))
            cartButtonText.text = "Remove From Cart";
        else
            cartButtonText.text = "Add To Cart";
    }
    public void Reset()
    {
        selectedItem = new();
        UpdateSelection();
    }
}
