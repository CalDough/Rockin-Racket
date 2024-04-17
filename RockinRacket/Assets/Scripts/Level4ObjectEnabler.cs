using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4ObjectEnabler : MonoBehaviour
{

    public bool PastIntermission = false;

    public List<GameObject> objectsToEnable = new List<GameObject>();

    public List<GameObject> objectsToDisable = new List<GameObject>();

    void Start()
    {
        ConcertEvents.instance.e_ConcertStarted.AddListener(UpdateObjectStates);
    }

    private void UpdateObjectStates()
    {
        PastIntermission = ConcertController.instance.afterIntermission;
        if (PastIntermission)
        {
            Debug.Log("Disabling Pre-Int Objects");
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject obj in objectsToEnable)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            foreach (GameObject obj in objectsToDisable)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

}
