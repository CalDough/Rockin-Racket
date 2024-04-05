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

    private Item selectedItem;

    public Item GetSelectedItem() { return selectedItem; }

    private void Start()
    {
        UpdateText();
    }

    public void SelectItem(Item itemTest)
    {
        selectedItem = itemTest;
        UpdateText();
    }

    private void UpdateText()
    {
        // default values
        nameText.text = "~Name~";
        descriptionText.text = "~Description~";

        if (selectedItem != null)
        {
            //bool isInCart = shopReceipt.IsInCart(selectedItem);
            //bool isBought = ItemInventory.ContainsItem(selectedItem);
            //bool isEquipped = ItemInventory.IsEquipped(selectedItem);
            //cartBtn.SetActive(!isBought);
            //equipBtn.SetActive(isBought && !isEquipped);
            nameText.text = selectedItem.name;
            descriptionText.text = selectedItem.description;
            //costText.text = "Item Cost";

            //if (!isBought)
            //{
            //    costText.text = "$" + selectedItem.cost.ToString();
            //    if (isInCart)
            //        cartButtonText.text = "Remove From Cart";
            //    else
            //        cartButtonText.text = "Add To Cart";
            //}
        }
        else
        {
            //cartBtn.SetActive(false);
            //equipBtn.SetActive(false);
            //costText.text = "";
        }
    }

    // called by catalogManager when bookmark pressed
    public void ResetSelection()
    {
        selectedItem = null;
        UpdateText();
    }

    // called by ItemModifiers on mouse enter
    public void ShowModifier(bool isScore, Bandmate bandmate)
    {
        if (isScore)
        {
            nameText.text = "Score Booster!";
            descriptionText.text = $"This item increases your score from {bandmate}'s minigames\n";
        }
        else
        {
            nameText.text = "Complex Instrument";
            descriptionText.text = $"This item makes {bandmate}'s minigames more difficult!\n";
        }
    }
    // called by ItemModifiers on mouse exit
    public void StopShowingModifiers()
    {
        UpdateText();
    }
}
