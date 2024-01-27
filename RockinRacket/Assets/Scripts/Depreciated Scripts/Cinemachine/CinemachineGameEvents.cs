using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * This script creates the events used by Minigames that utilize different cinemachine cameras. To create an event, simply
 * declare it below the existing ones, and then create a corresponding initialization/null check in Start()
 * 
 * To add a listener or invoke these events, use the following pattern...
 * 
 * INVOKING: CinemachineGameEvents.instance.<GAME EVENT HERE>.Invoke();
 * 
 * LISTENING: CinemachineGameEvents.instance.<GAME EVENT HERE>.AddListener(<METHOD TO BE EXECUTED NAME HERE>);
 */

namespace cinemachine.actions.handler
{
    public class CinemachineGameEvents : MonoBehaviour
    {
        public static CinemachineGameEvents instance;

        public UnityEvent e_SwitchToBandCam;
        public UnityEvent e_SwitchToTShirtCam;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (e_SwitchToBandCam == null)
            {
                e_SwitchToBandCam = new UnityEvent();
            }

            if (e_SwitchToTShirtCam == null)
            {
                e_SwitchToTShirtCam = new UnityEvent();
            }
        }

    }
}