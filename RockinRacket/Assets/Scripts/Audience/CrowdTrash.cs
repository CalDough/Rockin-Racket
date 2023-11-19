using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdTrash : MonoBehaviour, IConcertInteractable
{
    public bool IsProjectile { get; set; } = true;

    public void StickToMember(GameObject member)
    {
        IsProjectile = false;
        transform.SetParent(member.transform);

        Collider2D memberCollider = member.GetComponent<Collider2D>();
        if (memberCollider != null)
        {
            Vector2 direction = (transform.position - member.transform.position).normalized;
            transform.position = member.transform.position + (Vector3)(direction * memberCollider.bounds.extents.magnitude);
        }
        else
        {
            transform.localPosition = Vector2.zero; 
        }
    }

    public void ClickInteraction()
    {
        CrowdController.Instance.currentTrashCount--;
        Destroy(gameObject); 
    }

}