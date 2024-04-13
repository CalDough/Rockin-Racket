using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/*
 * This class is the main embodiment of the merch table. It controls all of the logical related to the timing of the customers. For logic related to the UI,
 * please refer to the MerchTableUIHandler.cs class
 * 
 * 
 */

public class MerchTable : MonoBehaviour
{
    [Header("Current Instance Stats")]
    [SerializeField] private int startingCustomerCount;
    [SerializeField] private int currentCustomerCount;
    [SerializeField] private float totalMoneyEarned;
    private Queue<GameObject> customerQueue = new Queue<GameObject>();

    [Header("Location Variables")]
    public Transform customerSpawnPosition;
    public Transform customerAtMerchTablePosition;
    public Transform customerOffscreenDeathPosition;

    [Header("Object/Prefab References")]
    public GameObject customerPrefab;
    public GameObject[] customerPrefabVariations;
    [SerializeField] private MerchTableUIHandler merchTableUIHandler;
    [SerializeField] private RectTransform itemDestination;
    private GameObject currentCustomer;
    [SerializeField] private IntermissionController intermissionController;

    [Header("Purchaseable Item Details")]
    public Vector2 minMaxItemPurchaseAmounts;
    [SerializeField] private Sprite tShirtSprite;
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private Sprite posterSprite;
    [SerializeField] private GameObject tShirtPrefab;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject posterPrefab;
    private PurchaseableItem tShirt;
    private PurchaseableItem button;
    private PurchaseableItem poster;
    private List<PurchaseableItem> masterItemList = new List<PurchaseableItem>();

    [Header("Merch Box Initialzation References")]
    public MerchBox tShirtMerchBox;
    public MerchBox buttonMerchBox;
    public MerchBox posterMerchBox;

    [Header("Debug Data")]
    public Button initializeMerchTableManually;
    public bool AllCustomersFulfilled = false;

    /*
     * In Awake, we are initializing classes for each of our purchaseable items and our merch boxes
     */
    private void Awake()
    {
        tShirt = new PurchaseableItem(1, "Tshirt", tShirtSprite, tShirtPrefab);
        button = new PurchaseableItem(2, "Button", buttonSprite, buttonPrefab);
        poster = new PurchaseableItem(3, "Poster", posterSprite, posterPrefab);
        masterItemList.Add(tShirt);
        masterItemList.Add(button);
        masterItemList.Add(poster);

        tShirtMerchBox.Init(tShirt, "Tshirt", itemDestination);
        buttonMerchBox.Init(button, "Button", itemDestination);
        posterMerchBox.Init(poster, "Poster", itemDestination);

        // Temporary Debug Code
        initializeMerchTableManually.onClick.AddListener(() => InitalizeMerchTable(5));
    }

    /*
     * In Start we are adding event listeners for any MTCustomer specific events
     */
    private void Start()
    {
        MerchTableEvents.instance.e_customerHasArrived.AddListener(ActivateCustomerUI);
    }

    /*
     * This method is initializing the merch table for the current intermission
     */
    public void InitalizeMerchTable(int numCustomersFromConcertPerformance)
    {
        startingCustomerCount = numCustomersFromConcertPerformance;
        currentCustomerCount = startingCustomerCount;

        SpawnCustomers(startingCustomerCount);

        merchTableUIHandler.UpdateCustomerCount();
    }

    /*
     * This method spawns the customers and enqueues them in the master customer queue
     */
    private void SpawnCustomers(int numCustomers)
    {
        for (int i = 0; i < numCustomers; i++)
        {
            GameObject curSpawnedPrefab = Instantiate(customerPrefabVariations[Random.Range(0, customerPrefabVariations.Length)], customerSpawnPosition.position, Quaternion.identity);
            //curSpawnedPrefab.GetComponent<MTCustomer>().RandomizeAppearance();
            customerQueue.Enqueue(curSpawnedPrefab);
        }

        // After we spawn the customers we can trigger the first customer
        TriggerNextCustomer(false);
    }

    /*
     * This method triggers a new customer. Here is what happens when this method is called:
     *      - The top customer on the queue is popped off and set to lerp towards the shop point
     *      - The customer wants box is activated once the customer has arrived at their destintation
     *      - After the customer arrives, the player is able to drop items into their bag
     *      - This method communicates with the MerchTableUIHandler class
     */
    public void TriggerNextCustomer(bool preExistingCustomer)
    {
        if (preExistingCustomer)
        {
            currentCustomer.GetComponent<MTCustomer>().MarkCustomerForDestruction();
            currentCustomer.GetComponent<MTCustomer>().StartLerp(customerAtMerchTablePosition, customerOffscreenDeathPosition);
        }

        if (customerQueue.Count > 0)
        {
            GameObject curCustomer = customerQueue.Dequeue();
            curCustomer.GetComponent<MTCustomer>().StartLerp(customerSpawnPosition, customerAtMerchTablePosition);
            currentCustomer = curCustomer;
        }
        else
        {
            Debug.Log("<color=green> All Customers Fulfilled at Merch Stand </color>");
            AllCustomersFulfilled = true;
            intermissionController.TransitionBackToConcert();
        }
    }

    /*
     * This method randomly generates a new list of purchaseable items that the current customer wants. This data
     * will be sent to the MerchTableUIHandler class to display
     */
    private List<PurchaseableItem> GenerateRandomPurchaseableItemList()
    {
        List<PurchaseableItem > list = new List<PurchaseableItem>();
        int numShirts = 0, numButtons = 0, numPosters = 0;

        int requiredWants = (int)Random.Range(minMaxItemPurchaseAmounts.x, minMaxItemPurchaseAmounts.y);

        for (int i = 0; i < requiredWants; i++)
        {
            list.Add(masterItemList[Random.Range(0, masterItemList.Count)]);

            switch (list[i].itemName)
            {
                case "Tshirt":
                    numShirts++;
                    break;
                case "Button":
                    numButtons++; 
                    break;
                case "Poster":
                    numPosters++;
                    break;
                default:
                    Debug.LogError("Invalid item name in GenerateRandomPurchaseableItemList: " + list[i].itemName);
                    break;
            }
        }

        UpdateMerchBoxItemTallies(numShirts, numButtons, numPosters);

        return list;
    }

    /*
     * This method tells the merch boxes how many of each item are needed for this particular customer
     */
    private void UpdateMerchBoxItemTallies(int numShirts, int numButtons, int numPosters)
    {
        tShirtMerchBox.UpdateMerchItemSpawnCounter(numShirts);
        buttonMerchBox.UpdateMerchItemSpawnCounter(numButtons);
        posterMerchBox.UpdateMerchItemSpawnCounter(numPosters);
    }

    /*
     * This method handles calling the MerchTableUIHandler to activate the UI
     */

    private void ActivateCustomerUI()
    {
        List<PurchaseableItem> curCustomerList = GenerateRandomPurchaseableItemList();

        merchTableUIHandler.ActivateAndUpdateCustomerWants(curCustomerList);
    }

    /*
     *  This method returns the current number of customers remaining
     */
    public int ReturnCurrentCustomerCount()
    {
        return customerQueue.Count;
    }

}
