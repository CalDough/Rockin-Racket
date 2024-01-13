using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private MerchTableUIHandler merchTableUIHandler;

    [Header("Purchaseable Item Details")]
    public Vector2 minMaxItemPurchaseAmounts;
    [SerializeField] private Sprite tShirtSprite;
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private Sprite posterSprite;
    private PurchaseableItem tShirt;
    private PurchaseableItem button;
    private PurchaseableItem poster;
    private List<PurchaseableItem> masterItemList = new List<PurchaseableItem>();

    [Header("Debug Data")]
    public Button initializeMerchTableManually;

    /*
     * In Awake, we are initializing classes for each of our purchaseable items
     */
    private void Awake()
    {
        tShirt = new PurchaseableItem(1, "Tshirt", tShirtSprite);
        button = new PurchaseableItem(2, "Button", buttonSprite);
        poster = new PurchaseableItem(3, "Poster", posterSprite);
        masterItemList.Add(tShirt);
        masterItemList.Add(button);
        masterItemList.Add(poster);


        // Temporary Debug Code
        initializeMerchTableManually.onClick.AddListener(() => InitalizeMerchTable(5));
    }

    /*
     * This method is initializing the merch table for the current intermission
     */
    public void InitalizeMerchTable(int numCustomersFromConcertPerformance)
    {
        startingCustomerCount = numCustomersFromConcertPerformance;
        currentCustomerCount = startingCustomerCount;

        SpawnCustomers(startingCustomerCount);
    }

    /*
     * This method spawns the customers and enqueues them in the master customer queue
     */
    private void SpawnCustomers(int numCustomers)
    {
        for (int i = 0; i < numCustomers; i++)
        {
            GameObject curSpawnedPrefab = Instantiate(customerPrefab, customerSpawnPosition.position, Quaternion.identity);
            curSpawnedPrefab.GetComponent<MTCustomer>().RandomizeAppearance();
            customerQueue.Enqueue(curSpawnedPrefab);
        }
    }

    /*
     * This method triggers a new customer. Here is what happens when this method is called:
     *      - The top customer on the queue is popped off and set to lerp towards the shop point
     *      - The customer wants box is activated once the customer has arrived at their destintation
     *      - After the customer arrives, the player is able to drop items into their bag
     *      - This method communicates with the MerchTableUIHandler class
     */
    public void TriggerNextCustomer()
    {
        if (customerQueue.Count > 0)
        {
            GameObject curCustomer = customerQueue.Dequeue();
            curCustomer.GetComponent<MTCustomer>().StartLerp(customerSpawnPosition.position, customerAtMerchTablePosition.position);

            List<PurchaseableItem> curCustomerList = GenerateRandomPurchaseableItemList();
        }
    }

    /*
     * This method randomly generates a new list of purchaseable items that the current customer wants. This data
     * will be sent to the MerchTableUIHandler class to display
     */
    private List<PurchaseableItem> GenerateRandomPurchaseableItemList()
    {
        List<PurchaseableItem > list = new List<PurchaseableItem>();

        int requiredWants = (int)Random.Range(minMaxItemPurchaseAmounts.x, minMaxItemPurchaseAmounts.y);

        for (int i = 0; i < requiredWants; i++)
        {
            list.Add(masterItemList[Random.Range(0, masterItemList.Count)]);
        }

        return list;
    }


}
