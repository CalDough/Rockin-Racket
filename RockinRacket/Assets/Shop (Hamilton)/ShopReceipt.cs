using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopReceipt : MonoBehaviour
{
    public TMP_Text cartText;

    private List<ItemTest> selectedItems = new();
    private int cost;

    public void AddToCart(ItemTest item)
    {
        //item.AddToCart();
        selectedItems.Add(item);
        UpdateText();
    }
    public void RemoveFromCart(ItemTest item)
    {
        //item.RemoveFromCart();
        selectedItems.Remove(item);
        UpdateText();
    }
    public ItemTest[] GetItemsToBuy()
    {
        return selectedItems.ToArray();
    }
    public bool IsInCart(ItemTest item)
    {
        return selectedItems.Contains(item);
    }
    private void UpdateText()
    {
        StringBuilder stringBuilder = new();
        cost = 0;
        foreach (ItemTest item in selectedItems)
        {
            stringBuilder.Append(item.name);
            for (int i=0; i<20-item.name.Length; i++)
                stringBuilder.Append(".");
            stringBuilder.Append("$");
            stringBuilder.AppendLine(item.cost.ToString());
            cost += item.cost;
        }
        stringBuilder.AppendLine();
        if (cost > 0)
        {
            stringBuilder.Append("Total Cost: $");
            stringBuilder.Append(cost);
        }

        cartText.text = stringBuilder.ToString();
    }
    public void ResetReceipt()
    {
        selectedItems = new();
        UpdateText();
    }
}
