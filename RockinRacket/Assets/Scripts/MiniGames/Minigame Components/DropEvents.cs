using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropEvents : MonoBehaviour
{
    public static DropEvents current;

    public MyIntEvent e_DropEvent;

    [System.Serializable]
    public class MyIntEvent : UnityEvent<int>
    {

    }

    // Initializing our singleton
    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        if (e_DropEvent == null)
        {
            e_DropEvent = new MyIntEvent();
        }
    }
}