using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 
*/

public class ShopSelection : MonoBehaviour
{
    [SerializeField] private ShopReceipt shopReceipt;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject cartBtn;
    [SerializeField] private TMP_Text cartButtonText;
    [SerializeField] private GameObject equipBtn;
    [SerializeField] private TMP_Text equipButtonText;

    private Item selectedItem;

    public Item GetSelectedItem() { return selectedItem; }

    public void SelectItem(Item itemTest)
    {
        selectedItem = itemTest;
        UpdateText();
    }

    public void UpdateText()
    {
        // default values
        nameText.text = "Item Name";
        descriptionText.text = "Item Description";
        costText.text = "Item Cost";
        cartButtonText.text = "Cart";
        equipButtonText.text = "Equip";

        if (selectedItem != null)
        {
            bool isInCart = shopReceipt.IsInCart(selectedItem);
            bool isBought = ItemInventory.ContainsItem(selectedItem);
            bool isEquipped = ItemInventory.IsEquipped(selectedItem);
            cartBtn.SetActive(!isBought);
            equipBtn.SetActive(isBought && !isEquipped);
            nameText.text = selectedItem.name;
            descriptionText.text = selectedItem.description;
            costText.text = "Item Cost";

            if (!isBought)
            {
                costText.text = "$" + selectedItem.cost.ToString();
                if (isInCart)
                    cartButtonText.text = "Remove From Cart";
                else
                    cartButtonText.text = "Add To Cart";
            }
        }
        else
        {
            cartBtn.SetActive(false);
            equipBtn.SetActive(false);
            costText.text = "";
        }
    }

    // called by catalogManager when 
    public void ResetSelection()
    {
        selectedItem = null;
        UpdateText();
    }
}
