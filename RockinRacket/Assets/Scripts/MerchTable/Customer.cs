using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Data Relating to Customer Wants")]
    [SerializeField] CustomerWants[] wants;
    [SerializeField] int maxWants;

    private string encodedCustomerWants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateNewWants()
    {

    }

    private void EncodeWants()
    {

    }

    public string GetCustomerWants()
    {
        return encodedCustomerWants;
    }


}
