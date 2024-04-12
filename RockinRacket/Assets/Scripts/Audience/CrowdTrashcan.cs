using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class CrowdTrashcan : MonoBehaviour
{
    public UnityEvent TrashCleanedUp;
    public MousePhysics mPhysics;
    public GameObject TrashCanHighlight;
    public string CleanUpEffectPath = "event:/Sound Effects/Mini-games/Trashcan";

    [Header("Score Variables")]
    public int ScoreBonus = 3;
    public int ScorePenalty = -3;
    public int TotalTrashCleaned = 0;
    
    void Update()
    {
        if(mPhysics.currentlyDragging == null)
        {
            TrashCanHighlight.SetActive(false);

        }
        else
        {
            TrashCanHighlight.SetActive(true);
        }
    }

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
            
            FMODUnity.RuntimeManager.PlayOneShot(CleanUpEffectPath);
        }
    }

}