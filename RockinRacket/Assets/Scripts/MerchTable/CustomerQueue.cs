using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/*
 * Customer Queue generates a list of customers for the level based on the given parameter and handles the customer movement 
 * 
 */
public class CustomerQueue : MonoBehaviour
{
    [Header("Customer Information")]
    [SerializeField] GameObject customerPrefab;
    [SerializeField] float lerpSpeed;
    [SerializeField] int lerpLength;
    [Header("Spawning Information")]
    [SerializeField] Transform spawnOriginPoint;
    [SerializeField] Transform shopPoint;
    [SerializeField] Transform offScreenDespawnPoint;
    [SerializeField] int maxNumCustomers;
    [Header("Shop Parameters")]
    [SerializeField] int customerRewardTimer;
    [SerializeField] int customerSpawnDelay;
    [Tooltip("The odds of a customer spawning every second will be 1-thevalueyouput/100")]
    [SerializeField] int customerSpawningOdds;

    // Private member variables
    private GameObject currentCustomer;
    private bool isLerping = false;
    private bool customerHasSpawned = false;
    private bool allowCustomerSpawning = false;
    private float startTime;
    private float journeyLength;
    private Transform origin;
    private Transform destination;
    private int currentNumCustomers;
    private bool noMoreCustomers = false;

    // Start is called before the first frame update
    void Start()
    {
        MerchTableEvents.instance.e_cueNextCustomer.AddListener(SetCustomerSatisfied);

        // Initiate Customer Delay, then we can attempt to spawn the first customer
        StartCoroutine(CustomerSpawnCooldown(customerSpawnDelay));

        currentNumCustomers = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!noMoreCustomers)
        {
            if (customerHasSpawned == true)
            {
                currentNumCustomers++;

                if (currentNumCustomers >= maxNumCustomers)
                {
                    noMoreCustomers = true;
                    Debug.Log("Next Customer is the last one");
                    return;
                }
                else
                {
                    customerHasSpawned = false;
                    allowCustomerSpawning = false;
                }
                //isLerping = true;
                //startTime = Time.time;
                //StartCoroutine(StopLerp(lerpLength));
            }

            if (allowCustomerSpawning == true)
            {
                AttemptCustomerSpawn();
            }
        }

        // Safeguard for our lerping
        //if (isLerping)
        //{
        //    LerpCustomer(origin, destination);
        //}
    }

    private void AttemptCustomerSpawn()
    {
        int oddsOfSpawn = Random.Range(1, 100);

        if (oddsOfSpawn < customerSpawningOdds)
        {
            customerHasSpawned = true;
            currentCustomer = Instantiate(customerPrefab, shopPoint.position, customerPrefab.transform.rotation);

            Debug.Log("Customer has spawned");

            //origin = spawnOriginPoint;
            //destination = shopPoint;
            //journeyLength = Vector3.Distance(origin.position, destination.position);

            currentCustomer.gameObject.GetComponent<Customer>().GenerateNewWants();
            string customerWants = currentCustomer.gameObject.GetComponent<Customer>().GetCustomerWants();
            MerchTableEvents.instance.e_sendCustomerData.Invoke(customerWants);
        }
    }

    IEnumerator CustomerSpawnCooldown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        Debug.Log("Customer can spawn again");
        allowCustomerSpawning = true;
    }

    private void SetCustomerSatisfied()
    {
        Debug.Log("Customer Satisfied");
        StartCoroutine(CustomerSpawnCooldown(customerSpawnDelay));
        //origin = shopPoint;
        //destination = offScreenDespawnPoint;
        //journeyLength = Vector3.Distance(origin.position, destination.position);
        //isLerping = true;
        //StartCoroutine(StopLerp(lerpLength));
        //startTime = Time.time;
        Destroy(currentCustomer.gameObject);
    }

    /*
     * This method handles the lerping of the customer to various points
     */
    //private void LerpCustomer(Transform posA, Transform posB)
    //{
    //    // Distance moved equals elapsed time times speed..
    //    float distCovered = (Time.time - startTime) * lerpSpeed;

    //    // Fraction of journey completed equals current distance divided by total distance.
    //    float fractionOfJourney = distCovered / journeyLength;

    //    // Set our position as a fraction of the distance between the markers.
    //    currentCustomer.transform.position = Vector3.Lerp(posA.position, posB.position, fractionOfJourney);
    //}

    //IEnumerator StopLerp(int seconds)
    //{
    //    int counter = seconds;
    //    while (counter > 0)
    //    {
    //        yield return new WaitForSeconds(counter);
    //        counter--;
    //    }
    //    isLerping = false;
    //    StartCoroutine(CustomerSpawnCooldown(customerSpawnDelay));
    //}

}
