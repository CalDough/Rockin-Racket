using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TShirtCannon;
using UnityEngine.EventSystems;

public class MerchTableHandler : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
 * This method takes player input when they click the mouse and determines what the 
 * player is hitting and at what pressure amount
 */
    public void OnPointerDown(PointerEventData eventData)
    {
        //    if (!isActiveEvent || !canFire)
        //    { return; }

        //    if (mainCamera == null)
        //    { mainCamera = Camera.main; }

        //    if (!IsMouseInPlayableArea())
        //    { return; }

        //    //PlayParticlesAtPosition(eventData.position);
        //    PlaySound();

        //    PressureState currentPressureState = GetPressureState();

        //    Ray ray = mainCamera.ScreenPointToRay(eventData.position);

        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, cannonRange))
        //    {
        //        GameObject target = hit.transform.gameObject;

        //        if (target.CompareTag("Audience"))
        //        {
        //            FireShirt(true, currentPressureState, target);
        //        }
        //        else
        //        {
        //            FireShirt(false, currentPressureState, target);
        //        }
        //    }
        //    StartCoroutine(FireCooldown());
    }
}
