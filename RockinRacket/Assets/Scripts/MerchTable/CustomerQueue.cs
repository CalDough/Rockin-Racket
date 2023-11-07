using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private GameObject[] customers;
    [Header("Spawning Information")]
    [SerializeField] Transform spawnFront;
    [SerializeField] Transform spawnBack;
    [SerializeField] Transform offscreenDespawn;
    [SerializeField] float spawnZOffsetMin;
    [SerializeField] float spawnZOffsetMax;
    [SerializeField] float spawnDistance;
    [SerializeField] float lerpSpeed;


    // Private member variables
    private Queue<GameObject> customerQueue = new Queue<GameObject>();
    private float spawnXOffsetMin;
    private float spawnYOffsetMin;
    private GameObject currentCustomer;
    private bool isLerping = false;
    private float startTime;
    private float journeyLength;
    private GameObject pastCustomer;

    // Start is called before the first frame update
    void Start()
    {
        MerchTableEvents.instance.e_cueNextCustomer.AddListener(CueNextCustomer);

        customers = new GameObject[numberOfCustomers];

        spawnXOffsetMin = spawnFront.position.x;
        spawnYOffsetMin = spawnFront.position.y;

        // Spawn them in and then add them to the queue
        SpawnCustomersIntially();
        AddCustomersToTheQueue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CueNextCustomer();
        }

        // Safeguard for our lerping
        if (isLerping)
        {
            LerpCustomerToSide();
        }
    }

    /*
     * This methods spawns customers when the game is created
     */
    private void SpawnCustomersIntially()
    {
        Vector3 spawnPos = spawnFront.position;
        float cycleSpawnOffset = 0;
        Debug.Log("Spawning Customers");
        for (int i = 0; i < numberOfCustomers; i++)
        {
            float horizontalVariation = Random.Range(spawnZOffsetMin, spawnZOffsetMax);
            GameObject customer = Instantiate(customerPrefab, new Vector3(spawnPos.x + cycleSpawnOffset, spawnPos.y, spawnPos.z + horizontalVariation), customerPrefab.transform.rotation);
            customers[i] = customer;
            customerQueue.Enqueue(customer);
            cycleSpawnOffset += spawnDistance;
        }
        spawnBack = customers[numberOfCustomers - 1].gameObject.transform;
    }

    /*
     * This method is called as soon as the game starts to transfer the customer array to a queue
     */
    private void AddCustomersToTheQueue()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            customerQueue.Enqueue(customers[i]);
        }

        currentCustomer = customerQueue.Dequeue();
    }

    /*
     * This method triggers the next customer in line
     */
     private void CueNextCustomer()
     {
        Debug.Log("<color=orange>Cueing Next Customer</color>");
        pastCustomer = currentCustomer;
        startTime = Time.time;
        journeyLength = Vector3.Distance(spawnFront.position, offscreenDespawn.position);
        isLerping = true;
        StartCoroutine(StopLerp(2));
        // Moving front of line customer to back of line
        //MoveCustomertoBackOfLine(currentCustomer);
        // Readding front of line customer to queue
        customerQueue.Enqueue(pastCustomer);
        // Getting next in line
        currentCustomer = customerQueue.Dequeue();
        // Getting customer needs and sending it to the shop handler class
        currentCustomer.gameObject.GetComponent<Customer>().GenerateNewWants();
        string customerWants = currentCustomer.gameObject.GetComponent<Customer>().GetCustomerWants();
        MerchTableEvents.instance.e_sendCustomerData.Invoke(customerWants);
        //StartCoroutine(MoveCustomersDelay(3));
    }

    IEnumerator MoveCustomersDelay(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        MoveCustomersUp();
    }

    IEnumerator StopLerp(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        isLerping = false;
        pastCustomer.transform.position = new Vector3(spawnBack.transform.position.x + spawnDistance, spawnBack.transform.position.y, spawnBack.transform.position.z);
        MoveCustomersUp();
    }

    /*
     * This method moves the customer at the front of the line back to the back
     */
    private void MoveCustomertoBackOfLine(GameObject customer)
    {
        Debug.Log("Moving customer to back of line");
        Vector3 startingPos = customer.gameObject.transform.position;
        customer.transform.position = Vector3.Lerp(startingPos, offscreenDespawn.position, Time.deltaTime);

        // Then move customer to back of line
    }

    /*
     * This method handles the lerping of the customer offscreen
     */
    private void LerpCustomerToSide()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * lerpSpeed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        pastCustomer.transform.position = Vector3.Lerp(spawnFront.position, offscreenDespawn.position, fractionOfJourney);
    }

    /*
     * This method moves all other customers up one position in line
     */
    private void MoveCustomersUp()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i].transform.position = new Vector3(customers[i].transform.position.x - spawnDistance, customers[i].transform.position.y, customers[i].transform.position.z);
        }
    }


}
