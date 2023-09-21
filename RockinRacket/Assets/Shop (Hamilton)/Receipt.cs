using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Receipt : MonoBehaviour
{
    public TMP_Text cartText;

    private List<ItemOption> selectedItemOptions = new();
    private int cost;

    public void AddToCart(ItemOption item)
    {
        item.AddToCart();
        selectedItemOptions.Add(item);
        UpdateText();
    }
    public void RemoveFromCart(ItemOption item)
    {
        item.RemoveFromCart();
        selectedItemOptions.Remove(item);
        UpdateText();
    }
    public void BuyItems()
    {
        foreach (ItemOption item in selectedItemOptions)
        {
            item.BuyItem();
        }
        selectedItemOptions.Clear();
        UpdateText();
        ItemInventory.Save();
    }
    public bool IsInCart(ItemOption item)
    {
        return selectedItemOptions.Contains(item);
    }
    private void UpdateText()
    {
        StringBuilder stringBuilder = new();
        cost = 0;
        foreach (ItemOption itemOption in selectedItemOptions)
        {
            stringBuilder.Append(itemOption.item.itemName);
            for (int i=0; i<20-itemOption.item.itemName.Length; i++)
                stringBuilder.Append(".");
            stringBuilder.Append("$");
            stringBuilder.AppendLine(itemOption.item.cost.ToString());
            cost += itemOption.item.cost;
        }
        stringBuilder.AppendLine();
        stringBuilder.Append("Total Cost: $");
        stringBuilder.Append(cost);

        cartText.text = stringBuilder.ToString();
    }
}
