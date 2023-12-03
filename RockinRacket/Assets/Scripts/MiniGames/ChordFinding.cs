using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordFinding : MiniGame
{

    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {

        }
    }

    public override void Activate()
    {
        IsCompleted = false;
        isActive = true;
        Debug.Log("Event activated");
        base.Activate();
    }

    public override void Complete()
    {
        Debug.Log("Event complete");
        base.Complete();
    }

    public override void Miss()
    {
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void OpenEvent()
    {
        GameEvents.EventOpened(this);
        HandleOpening();
    }
    public override void CloseEvent()
    {
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void HandleOpening()
    {
        if (!IsCompleted)
        {
            Panels.SetActive(true);
        }
    }
    public override void HandleClosing()
    {
        Panels.SetActive(false);
        if (IsCompleted == false)
        {
            //RestartMiniGameLogic(); 
        }
    }
}
