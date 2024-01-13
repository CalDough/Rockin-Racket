using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;
using AYellowpaper.SerializedCollections;
using System.Xml;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;
using UnityEngine.ProBuilder.Shapes;

public class MerchTableHandler : MonoBehaviour
{
    [SerializeField] GameStateData currentGameState;
    [SerializedDictionary("Merch Item", "Cursor Texture")]
    public SerializedDictionary<CustomerWants, Texture2D> cursorTextures;
    [SerializeField] UnityEngine.CursorMode cursorMode;
    [SerializedDictionary("Merch Item", "Customer Wants Icon")]
    public SerializedDictionary<CustomerWants, UnityEngine.Sprite> customerWantIcons;
    [SerializedDictionary("Encoded Char", "Merch Item")]
    public SerializedDictionary<string, CustomerWants> wantsDecoder;
    [Header("UI Objects")]
    [SerializeField] GameObject itemContainer;
    [SerializeField] GameObject customerThinkingBox;
    [SerializeField] GameObject[] ItemBoxes;
    public RectTransform destination;
    [SerializeField] int customerThinkingTime;
    [SerializeField] GameObject moneyText;
    [SerializeField] int moneyTextTimer;
    [SerializeField] int moneyIncrementAmount;

    private CustomerWants[] currentWants;
    private Dictionary<CustomerWants, bool> currentCustomerStatus = new Dictionary<CustomerWants, bool>();
    private int totalMoney;
    //private bool isMerchTableActiveYet = false;

    void Start()
    {
        MerchTableEvents.instance.e_sendCustomerData.AddListener(NewCustomer);

        totalMoney = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * This method updates the customer cloud when the player drops an item on the customer bag
     */
    public void UpdateCustomerCloud(CustomerWants draggedWant)
    {
        //Debug.Log("Dragged want is: " + draggedWant);
        if (currentCustomerStatus.ContainsKey(draggedWant))
        {
            //Debug.Log("Setting dictionary value to true");
            currentCustomerStatus[draggedWant] = true;
        }

        int j = 0;

        foreach (KeyValuePair<CustomerWants, bool> entry in currentCustomerStatus)
        {
            if (entry.Value == true)
            {
                ItemBoxes[j].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            j++;
        }

        int i = 0;
        bool isCustomerDone = true;

        foreach (KeyValuePair<CustomerWants, bool> entry in currentCustomerStatus)
        {
            if (entry.Value == false)
            {
                isCustomerDone = false;
            }
            i++;
        }

        if (isCustomerDone)
        {
            OrderFulfilled();
        }
    }

    /*
     * This method is called by the send customer data event and decodes the string to wants
     */
    private void NewCustomer(string encodedWants)
    {
        customerThinkingBox.gameObject.SetActive(true);
        string[] decodedWants = encodedWants.Split(" ");
        currentWants = new CustomerWants[decodedWants.Length];

        for (int i = 0; i < decodedWants.Length; i++)
        {
            if (wantsDecoder.ContainsKey(decodedWants[i]))
            {
                currentWants[i] = wantsDecoder[decodedWants[i]];
                currentCustomerStatus.Add(currentWants[i], false);
            }
        }
        StartCoroutine(CustomerThinkingTimer(customerThinkingTime));
    }

    IEnumerator CustomerThinkingTimer(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        Debug.Log("Customer is ready to order");
        itemContainer.SetActive(true);
        customerThinkingBox.gameObject.SetActive(false);
        PopulateWantsInCloud();
    }

    /*
     * This method populates the customer wants cloud UI
     */
    private void PopulateWantsInCloud()
    {
        if (currentWants.Length > ItemBoxes.Length)
        {
            Debug.LogError("Item wants is greater than the alloted UI for the current customer's wants");
        }

        for (int i = 0; i < currentWants.Length; i++)
        {
            if (customerWantIcons.ContainsKey(currentWants[i]))
            {
                ItemBoxes[i].gameObject.GetComponent<Image>().sprite = customerWantIcons[currentWants[i]];
            }
        }
    }

    public void OrderFulfilled()
    {
        currentCustomerStatus = new Dictionary<CustomerWants, bool>();
        //Debug.Log("Order Fulfilled");

        for (int i = 0; i < ItemBoxes.Length; i++)
        {
            ItemBoxes[i].gameObject.GetComponent<Image>().sprite = null;
            ItemBoxes[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        itemContainer.SetActive(false);
        moneyText.SetActive(true);
        StartCoroutine(MoneyTextRemovalTimer(moneyTextTimer));
        totalMoney += moneyIncrementAmount;
        GameManager.Instance.IncrementMoney(moneyIncrementAmount);
        MerchTableEvents.instance.e_cueNextCustomer.Invoke();
    }

    public bool CheckIfIsRequired(CustomerWants draggedWant)
    {
        if (currentWants.Contains(draggedWant))
        {
            return true;
        }
        return false;
    }

    IEnumerator MoneyTextRemovalTimer(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        moneyText.SetActive(false);
    }
}
