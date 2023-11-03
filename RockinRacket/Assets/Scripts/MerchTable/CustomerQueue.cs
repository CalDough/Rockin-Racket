using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Customer Queue generates a list of customers for the level based on the given parameter and handles the customer movement 
 * 
 */
public class CustomerQueue : MonoBehaviour
{
    [Header("Customer Information")]
    [SerializeField] int numberOfCustomers;
    [SerializeField] int customerRewardTimer;
    [SerializeField] GameObject customerPrefab;
    [Header("DO NOT PUT ANYTHING IN HERE - JUST VISIBLE FOR DEBUG")]
    [SerializeField] private Customer[] customers;
    [Header("Spawning Information")]
    [SerializeField] Transform spawnFront;
    [SerializeField] Transform spawnBack;
    [SerializeField] Transform offscreenDespawn;
    [SerializeField] float spawnXOffsetMax;
    [SerializeField] float spawnYOffsetMax;
    [SerializeField] int spawnDistance;


    // Private member variables
    private Queue<Customer> queue = new Queue<Customer>();
    private float spawnXOffsetMin;
    private float spawnYOffsetMin;
    private Customer currentCustomer;

    // Start is called before the first frame update
    void Start()
    {
        MerchTableEvents.instance.e_cueNextCustomer.AddListener(CueNextCustomer);

        spawnXOffsetMin = spawnFront.position.x;
        spawnYOffsetMin = spawnFront.position.y;

        // Spawn them in and then add them to the queue
        SpawnCustomersIntially();
        AddCustomersToTheQueue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * This methods spawns customers when the game is created
     */
    private void SpawnCustomersIntially()
    {
        Vector3 spawnPos = spawnFront.position;
        int cycleSpawnOffset = 0;

        for (int i = 0; i < numberOfCustomers; i++)
        {
            GameObject customer = Instantiate(customerPrefab, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z + cycleSpawnOffset), Quaternion.identity);
            //queue.Enqueue(customer);
        }
    }

    /*
     * This method is called as soon as the game starts to transfer the customer array to a queue
     */
    private void AddCustomersToTheQueue()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            queue.Enqueue(customers[i]);
        }
    }

    /*
     * This method triggers the next customer in line
     */
     private void CueNextCustomer()
     {
        // Moving front of line customer to back of line
        MoveCustomertoBackOfLine(currentCustomer);
        // Readding front of line customer to queue
        queue.Enqueue(currentCustomer);
        // Getting next in line
        currentCustomer = queue.Dequeue();
        // Getting customer needs and sending it to the shop handler class
        string customerWants = currentCustomer.GetCustomerWants();
        MerchTableEvents.instance.e_sendCustomerData.Invoke(customerWants);
        // Moving all customers up
        MoveCustomersUp();
    }

    /*
     * This method moves the customer at the front of the line back to the back
     */
    private void MoveCustomertoBackOfLine(Customer customer)
    {
        Vector3 startingPos = gameObject.transform.position;
        customer.transform.position = Vector3.Lerp(startingPos, offscreenDespawn.position, Time.deltaTime);

        // Then move customer to back of line
    }

    /*
     * This method moves all other customers up one position in line
     */
    private void MoveCustomersUp()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].transform.position = new Vector3(customers[i].transform.position.x, customers[i].transform.position.y, customers[i].transform.position.z + 5);
        }
    }


}
