using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TShirtCannon;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;
using AYellowpaper.SerializedCollections;
using System.Xml;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;

public class MerchTableHandler : MonoBehaviour
{
    [SerializeField] GameStateData currentGameState;
    [SerializedDictionary("Merch Item", "Cursor Texture")]
    public SerializedDictionary<CustomerWants, Texture2D> cursorTextures;
    [SerializeField] UnityEngine.CursorMode cursorMode;
    [SerializedDictionary("Merch Item", "Customer Wants Icon")]
    public SerializedDictionary<CustomerWants, Sprite> customerWantIcons;
    [SerializedDictionary("Encoded Char", "Merch Item")]
    public SerializedDictionary<string, CustomerWants> wantsDecoder;
    [Header("UI Objects")]
    [SerializeField] RawImage cloudContainer;
    [SerializeField] GameObject[] ItemBoxes;

    private CustomerWants[] currentWants;
    private Dictionary<CustomerWants, bool> currentCustomerStatus = new Dictionary<CustomerWants, bool>();
    private bool isMerchTableActiveYet = false;


    // Start is called before the first frame update
    void Start()
    {
        MerchTableEvents.instance.e_sendCustomerData.AddListener(NewCustomer);
        OrderFulfilled();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeldMerchItem();

        UpdateCustomerCloudBox();


        //if (isMerchTableActiveYet)
        //{

        //}
    }

    /*
     * This method updates the customer cloud and checks to see if it has been fulfilled
     */
    private void UpdateCustomerCloudBox()
    {
        int i = 0;

        foreach (KeyValuePair<CustomerWants, bool> entry in currentCustomerStatus)
        {
            if (entry.Value == true)
            {
                ItemBoxes[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            i++;
        }

        //for (int i = 0; i < ItemBoxes.Length; i++)
        //{
        //    if (currentCustomerStatus.ElementAt(i).Value == true)
        //    {
        //        ItemBoxes[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //    }
        //}
    }

    /*
     * This method is called by the send customer data event and decodes the string to wants
     */
    private void NewCustomer(string encodedWants)
    {
        Debug.Log("New Customer method called");
        string[] decodedWants = encodedWants.Split(" ");
        Debug.Log("Finished decoding wants = " + decodedWants.Length);
        currentWants = new CustomerWants[decodedWants.Length];

        for (int i = 0; i < decodedWants.Length; i++)
        {
            if (wantsDecoder.ContainsKey(decodedWants[i]))
            {
                Debug.Log("Wants: " + decodedWants[i]);

                currentWants[i] = wantsDecoder[decodedWants[i]];
                currentCustomerStatus.Add(currentWants[i], false);
            }

        }

        Debug.Log("Dictionary filled in with wants");

        PopulateWantsInCloud();
    }

    /*
     * This method populates the customer wants cloud UI
     */
    private void PopulateWantsInCloud()
    {
        for (int i = 0; i < currentWants.Length; i++)
        {
            Debug.Log("Wants: " + currentWants[i]);
        }

        Debug.Log("Populating Wants In Cloud");
        if (currentWants.Length > ItemBoxes.Length)
        {
            Debug.Log("ItemBoxes Length: " + ItemBoxes.Length + " | Currentwants Length " + currentWants.Length);
            Debug.LogError("Item wants is greater than the alloted UI for the current customer's wants");
        }

        for (int i = 0; i < currentWants.Length; i++)
        {

            if (customerWantIcons.ContainsKey(currentWants[i]))
            {
                Debug.Log("i: " + i + " | ItemBoxes length: " + ItemBoxes.Length + " | Current wants length: " + currentWants.Length);
                ItemBoxes[i].gameObject.GetComponent<Image>().sprite = customerWantIcons[currentWants[i]];
            }
        }
        isMerchTableActiveYet = true;
    }

    public void OrderFulfilled()
    {
        currentCustomerStatus = new Dictionary<CustomerWants, bool>();
        Debug.Log("Order Fulfilled");

        for (int i = 0; i < ItemBoxes.Length; i++)
        {
            ItemBoxes[i].gameObject.GetComponent<Image>().sprite = null;
        }

        MerchTableEvents.instance.e_cueNextCustomer.Invoke();
    }

    private void UpdateHeldMerchItem()
    {
        Texture2D value;
        bool hasValue;

        switch (currentGameState.currentlyHeldObject)
        {
            case CustomerWants.None:
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                break;
            case CustomerWants.tshirt:
                hasValue = cursorTextures.TryGetValue(CustomerWants.tshirt, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            case CustomerWants.mug:
                hasValue = cursorTextures.TryGetValue(CustomerWants.mug, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            case CustomerWants.button:
                hasValue = cursorTextures.TryGetValue(CustomerWants.button, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            default:
                Debug.Log("<color=red>MISSING CURSOR TEXTURE MERCHTABLEHANDLER.CS </color>");
                break;
        }
    }
}
