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
     * In the Start method we are adding listeners for any relevant UI events
     */
    private void Start()
    {
        MerchTableEvents.instance.e_itemDeposited.AddListener(PurchaseableItemFulfilled);
    }

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
     * The following method removes items from the Customer Wants box as they are fulfilled
     */
    public void PurchaseableItemFulfilled(Sprite itemSprite)
    {
        for (int i = 0; i < keyItemSprites.Length; i++)
        {
            if (itemSprite == keyItemSprites[i].sprite)
            {
                keyItemSprites[i].gameObject.SetActive(false);
            }
        }

        if (!keyItemSprites[0].gameObject.activeSelf && !keyItemSprites[1].gameObject.activeSelf && !keyItemSprites[2].gameObject.activeSelf && !keyItemSprites[3].gameObject.activeSelf)
        {
            //merchTableClass.TriggerNextCustomer();
            Debug.Log("Customer Fulfilled");
            visualContainer.SetActive(false);

        }
    }
}
