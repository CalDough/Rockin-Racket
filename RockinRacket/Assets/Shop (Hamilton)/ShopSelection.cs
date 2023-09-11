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

    //private bool isFixed;


    public void SelectItem(ItemTest item)
    {
        print("HIT");
        image.sprite = item.Sprite;
        nameText.text = item.ItemName;
        descriptionText.text = item.Description;
        buyText.text = "$" + item.Cost.ToString();
    }
    public void DeselectItem()
    {
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
