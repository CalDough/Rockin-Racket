using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Data Relating to Customer Wants")]
    [SerializeField] private CustomerWants[] wants;
    [SerializeField] int maxWants;
    [SerializedDictionary("Merch Item", "string Id")]
    public SerializedDictionary<CustomerWants, string> merchItemEncoding;

    private string encodedCustomerWants;
    private int numWants;

    public void GenerateNewWants()
    {
        Debug.Log("Generating new wants with a max value of " + maxWants);
        //numWants = Random.Range(1 , maxWants);
        //wants = new CustomerWants[numWants];
        wants = new CustomerWants[1];

        wants[0] = CustomerWants.tshirt;

        //for (int i = 0; i < wants.Length; i++)
        //{
        //    wants[i] = merchItemEncoding.ElementAt(Random.Range(0 , merchItemEncoding.Count)).Key;
        //    Debug.Log("Wants of i = " + merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key);

        //    //if (wants[i] > 0)
        //    //{
        //    //    if (wants[i] == wants[i - 1])
        //    //    {
        //    //        wants[i] = merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key;
        //    //    }
        //    //}
        //}


        EncodeWants();
    }

    private void EncodeWants()
    {
        encodedCustomerWants = "";

        for (int i = 0; i < wants.Length; i++)
        {
            encodedCustomerWants = encodedCustomerWants + merchItemEncoding[wants[i]] + " ";
        }

        Debug.Log("Encoding wants complete, encoded string: " + encodedCustomerWants);
    }

    public string GetCustomerWants()
    {
        Debug.Log("Returning Customer Wants");
        return encodedCustomerWants;
    }

}
