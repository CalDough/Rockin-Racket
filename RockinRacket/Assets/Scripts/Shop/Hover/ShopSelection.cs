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
    private Bandmate bandmate;

    public Item GetSelectedItem() { return selectedItem; }

    //private void Start()
    //{
    //    UpdateText();
    //}

    public void SelectItem(Item itemTest)
    {
        selectedItem = itemTest;
        UpdateText();
    }

    private void UpdateText()
    {
        // default values
        nameText.text = $"{bandmate}'s Page";
        descriptionText.text = $"A collection of items hand picked for {bandmate}";

        if (selectedItem != null)
        {
            nameText.text = selectedItem.name;
            descriptionText.text = selectedItem.description;
        }
    }

    // called by catalogManager when bookmark pressed
    public void ResetSelection(Bandmate bandmate)
    {
        selectedItem = null;
        this.bandmate = bandmate;
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
