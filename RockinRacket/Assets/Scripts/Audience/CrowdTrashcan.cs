using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrowdTrashcan : MonoBehaviour
{
    public UnityEvent TrashCleanedUp;
    public int TotalTrashCleaned = 0;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CrowdTrash trash = collider.GetComponent<CrowdTrash>();
        if (trash != null)
        {
            //Debug.Log("Destroyed Trash");
            Destroy(trash.gameObject); 
            TotalTrashCleaned++;
            //CrowdController.Instance.currentTrashCount--;
            //CrowdController.Instance.UpdateCrowdMood(CrowdController.Instance.trashRatingBonus);
            TrashCleanedUp.Invoke();
        }
    }

    private void Awake()
    {

    }
}