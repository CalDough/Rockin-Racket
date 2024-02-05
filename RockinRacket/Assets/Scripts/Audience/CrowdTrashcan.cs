using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrowdTrashcan : MonoBehaviour
{
    public UnityEvent TrashCleanedUp;

    [Header("Score Variables")]
    public int ScoreBonus = 3;
    public int ScorePenalty = -3;
    public int TotalTrashCleaned = 0;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        CrowdTrash trash = collider.GetComponent<CrowdTrash>();
        if (trash != null)
        {
            //Debug.Log("Destroyed Trash");
            Destroy(trash.gameObject); 
            TotalTrashCleaned++;
            TrashCleanedUp.Invoke();        
            ConcertEvents.instance.e_ScoreChange.Invoke(ScoreBonus);
        }
    }

}