using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSelection : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text costText;
    public TMP_Text cartButtonText;

    private ItemTest selectedItem;

    public ItemTest GetSelectedItem() { return selectedItem; }

    public void SelectItem(ItemTest itemTest, bool isIncart)
    {
        selectedItem = itemTest;
        UpdateText(isIncart);
    }
    private void UpdateText(bool isIncart)
    {
        if (selectedItem == null)
        {
            nameText.text = "Item Name";
            descriptionText.text = "Item Description";
            costText.text = "Item Cost";
            cartButtonText.text = "Add To Cart";
            return;
        }
        nameText.text = selectedItem.name;
        descriptionText.text = selectedItem.description;
        costText.text = "$" + selectedItem.cost.ToString();
        if (isIncart)
            cartButtonText.text = "Remove From Cart";
        else
            cartButtonText.text = "Add To Cart";
    }
    public void ResetSelection()
    {
        selectedItem = new();
        UpdateText(false);
    }
}
