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
        if(ConcertController.instance.afterIntermission)
        {
            UpdateObjectStates();
        }
    }

    private void UpdateObjectStates()
    {
        if (PastIntermission)
        {
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
    }

}
