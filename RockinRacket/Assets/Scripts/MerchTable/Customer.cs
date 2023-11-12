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
        //Debug.Log("Generating new wants with a max value of " + maxWants);
        numWants = Random.Range(1 , maxWants + 1);
        wants = new CustomerWants[numWants];

        wants[0] = CustomerWants.tshirt;

        for (int i = 0; i < wants.Length; i++)
        {
            CustomerWants tempWant = merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key;

            if (!wants.Contains(tempWant))
            {
                wants[i] = tempWant;
                //Debug.Log("Wants of i = " + wants[i]);
            }

            //wants[i] = merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key;
            //Debug.Log("Wants of i = " + merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key);

            //if (wants[i] > 0)
            //{
            //    if (wants[i] == wants[i - 1])
            //    {
            //        wants[i] = merchItemEncoding.ElementAt(Random.Range(0, merchItemEncoding.Count)).Key;
            //    }
            //}
        }

        //Debug.Log("Method ran");
        EncodeWants();
    }

    private void EncodeWants()
    {
        encodedCustomerWants = "";

        for (int i = 0; i < wants.Length; i++)
        {
            if (merchItemEncoding.ContainsKey(wants[i]))
            {
                encodedCustomerWants = encodedCustomerWants + merchItemEncoding[wants[i]] + " ";
                //Debug.Log(merchItemEncoding[wants[i]]);
            }
        }

        //Debug.Log("Encoding wants complete, encoded string: " + encodedCustomerWants);
    }

    public string GetCustomerWants()
    {
        //Debug.Log("Returning Customer Wants");
        return encodedCustomerWants;
    }

}
