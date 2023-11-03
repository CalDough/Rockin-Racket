using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Events class for the Merchtable, will be combined with another class in the future
 */
public class MerchTableEvents : MonoBehaviour
{
    public static MerchTableEvents instance;

    public UnityEvent e_cueNextCustomer;
    public UnityEvent<string> e_sendCustomerData;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (e_cueNextCustomer == null)
        {
            e_cueNextCustomer = new UnityEvent();
        }

        if (e_sendCustomerData == null)
        {
            e_sendCustomerData = new UnityEvent<string>();
        }
    }
}
