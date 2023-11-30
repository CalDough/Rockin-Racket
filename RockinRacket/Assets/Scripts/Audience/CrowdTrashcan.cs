using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdTrashcan : MonoBehaviour
{
    public int TotalTrashCleaned = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CrowdTrash trash = collider.GetComponent<CrowdTrash>();
        if (trash != null)
        {
            Debug.Log("Destroyed Trash");
            Destroy(trash.gameObject); 
            TotalTrashCleaned++;
            CrowdController.Instance.currentTrashCount--;
            CrowdController.Instance.UpdateCrowdMood(CrowdController.Instance.CrowdMoodBonusFromTrash);
        }
    }
}