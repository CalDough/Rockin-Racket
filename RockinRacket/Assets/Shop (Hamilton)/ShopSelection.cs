using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelection : MonoBehaviour
{
    public Receipt receipt;
    public Sprite defaultSprite;

    public Image image;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text cartButtonText;

    private ItemOption selectedItemOption;

    public void SelectItem(ItemOption itemOption)
    {
        selectedItemOption = itemOption;
        UpdateSelection();
    }
    public void CartBtnPressed()
    {
        if (receipt.IsInCart(selectedItemOption))
            receipt.RemoveFromCart(selectedItemOption);
        else
            receipt.AddToCart(selectedItemOption);
        UpdateSelection();
    }
    public void UpdateSelection()
    {
        image.sprite = selectedItemOption.item.sprite;
        nameText.text = selectedItemOption.item.itemName;
        descriptionText.text = selectedItemOption.item.description;
        costText.text = "$" + selectedItemOption.item.cost.ToString();
        if (receipt.IsInCart(selectedItemOption))
            cartButtonText.text = "Remove From Cart";
        else
            cartButtonText.text = "Add To Cart";
    }
}
