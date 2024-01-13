using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * This class controls the UI elements associated with the Merch Table minigame. 
 * 
 * Here are the following elements that this class controls:
 *      - Displaying Customers wants (activation, deactivation, updating values)
 *      - Displaying Customer cash reward
 * 
 */


public class MerchTableUIHandler : MonoBehaviour
{
    [Header("Customer Information Box")]
    [SerializeField] private GameObject visualContainer;
    [SerializeField] private Image[] keyItemSprites;
    private List<PurchaseableItem> currentItemsList;

    [Header("Draggable Objected Related References")]
    public RectTransform destination;

    [Header("MerchTable Class Variables")]
    public MerchTable merchTableClass;


    /*
     * The following method activates the Customer Wants box and updates it with new information
     */
    public void ActivateAndUpdateCustomerWants(List<PurchaseableItem> customerWants)
    {
        visualContainer.SetActive(true);

        currentItemsList = customerWants;

        for (int i = 0; i < keyItemSprites.Length; i++)
        {
            if (i < customerWants.Count)
            {
                keyItemSprites[i].gameObject.SetActive(true);
                PurchaseableItem item = customerWants[i];

                keyItemSprites[i].sprite = item.itemIcon;
            }
            else
            {
                keyItemSprites[i].gameObject.SetActive(false);
            }
        }
    }

    /*
     * The following method deactivates the customer want item box when the current customer leaves
     */
    public void EndCurrentCustomer()
    {
        visualContainer.SetActive(false);
    }

    public bool CheckIfIsRequired(int id)
    {
        for (int i = 0; i < currentItemsList.Count; i++)
        {
            if (currentItemsList[i].itemID == id)
            {
                return true;
            }
        }

        return false;
    }

    public void CustomerWantFulfilled(PurchaseableItem fulfilledItem)
    {
        for (int i = 0; i < keyItemSprites.Length; i++)
        {
            if (keyItemSprites[i].sprite == fulfilledItem.itemIcon)
            {
                keyItemSprites[i].gameObject.SetActive(false);
            }
        }

        // To be replaced, temporary
        if (keyItemSprites[0].gameObject.activeSelf == false && keyItemSprites[1].gameObject.activeSelf == false && keyItemSprites[2].gameObject.activeSelf == false && keyItemSprites[3].gameObject.activeSelf == false)
        {
            merchTableClass.TriggerNextCustomer();
        }
    }

}
