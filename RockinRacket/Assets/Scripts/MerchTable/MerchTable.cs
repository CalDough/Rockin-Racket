using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchTable : MonoBehaviour
{
    [Header("Current Instance Stats")]
    [SerializeField] private int startingCustomerCount;
    [SerializeField] private int currentCustomerCount;
    [SerializeField] private float totalMoneyEarned;
    private Queue<GameObject> customerQueue = new Queue<GameObject>();

    [Header("Location Variables")]
    public Vector3 customerSpawnPosition;
    public Vector3 customerAtMerchTablePosition;
    public Vector3 customerOffscreenDeathPosition;

    [Header("Prefab References")]
    public GameObject customerPrefab;

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
            GameObject curSpawnedPrefab = Instantiate(customerPrefab, customerSpawnPosition, Quaternion.identity);
            curSpawnedPrefab.GetComponent<MTCustomer>().RandomizeAppearance();
            customerQueue.Enqueue(curSpawnedPrefab);
        }
    }


}
