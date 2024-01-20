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

    public UnityEvent e_customerHasArrived;
    public UnityEvent<Sprite> e_itemDeposited;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (e_itemDeposited != null)
        {
            e_itemDeposited = new UnityEvent<Sprite>();
        }

        if (e_customerHasArrived == null)
        {
            e_customerHasArrived = new UnityEvent();
        }
    }
}
