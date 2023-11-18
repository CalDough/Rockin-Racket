using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdTrash : MonoBehaviour, IConcertInteractable
{
    public bool IsProjectile { get; private set; }

    public void MakeProjectile()
    {
        StartCoroutine(SetAsProjectileAfterDelay());
    }

    IEnumerator SetAsProjectileAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        IsProjectile = true;
    }

    public void StickToMember(GameObject member)
    {
        IsProjectile = false;
        transform.SetParent(member.transform);
        transform.localPosition = Vector2.zero;
    }

    public void ClickInteraction()
    {
        Destroy(gameObject); 
    }
}