using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MerchSource : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameStateData currentGameState;
    [SerializeField] CustomerWants sourceType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * 
     */
    public void OnPointerDown(PointerEventData eventData)
    {
        currentGameState.currentlyHeldObject = sourceType;
        Debug.Log("Player Clicked on " + gameObject.name);
    }
}
