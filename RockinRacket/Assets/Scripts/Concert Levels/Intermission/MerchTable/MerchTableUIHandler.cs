using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

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
    public int pricePerObject;

    [Header("Draggable Objected Related References")]
    public RectTransform destination;
    public TMP_Text counterText;

    [Header("MerchTable Class Variables")]
    public MerchTable merchTableClass;

    [Header("Debug Mode")]
    public bool isInTutorial;

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
    public void PurchaseableItemFulfilled(string name)
    {

        for (int i = 0; i < currentItemsList.Count; i++)
        {
            if (currentItemsList[i].itemName == name)
            {
                currentItemsList.Remove(currentItemsList[i]);
                break;
            }
        }

        ActivateAndUpdateCustomerWants(currentItemsList);

        if (!keyItemSprites[0].gameObject.activeSelf && !keyItemSprites[1].gameObject.activeSelf && !keyItemSprites[2].gameObject.activeSelf && !keyItemSprites[3].gameObject.activeSelf)
        {
            visualContainer.SetActive(false);
            merchTableClass.TriggerNextCustomer(true);
            Debug.Log("<color=green>Customer Fulfilled</color>");
            GameManager.Instance.currentConcertData.localMoney += pricePerObject;
            ConcertEvents.instance.e_TriggerSound.Invoke("checkOut");
            UpdateCustomerCount();
        }
    }

    /*
     *  This method updates the remaining customer count
     */
    public void UpdateCustomerCount()
    {
        if (!isInTutorial)
        {
            counterText.text = $"Remaining Customers:\n{merchTableClass.ReturnCurrentCustomerCount()}";
        }
    }
}
