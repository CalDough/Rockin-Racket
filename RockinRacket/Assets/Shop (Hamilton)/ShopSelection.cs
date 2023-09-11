using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelection : MonoBehaviour
{
    public Image image;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text buyText;
    public Sprite defaultSprite;

    private ItemOption selectedItemOption;


    public void SelectItem(ItemOption itemOption)
    {
        selectedItemOption = itemOption;
        image.sprite = itemOption.item.Sprite;
        nameText.text = itemOption.item.ItemName;
        descriptionText.text = itemOption.item.Description;
        buyText.text = "$" + itemOption.item.Cost.ToString();
    }
    public void BuyItem()
    {
        selectedItemOption.setBought(true);
        image.sprite = defaultSprite;
        nameText.text = "Item Name";
        descriptionText.text = "Item Description";
        buyText.text = "";
    }

    //public bool IsFixed()
    //{
    //    return isFixed;
    //}
    //public void setFixed(bool fix) {
    //    isFixed = fix;
    //}
}
