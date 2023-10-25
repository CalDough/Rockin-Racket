using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TrashObject : MonoBehaviour, IPointerClickHandler
{
    public int hitPoints = 3; 
    public int value = 1;     // Value of the trash when cleaned
    public Cleaning cleaning;
    public TextMeshProUGUI hpText;

    void Start()
    {
        UpdateHpText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hitPoints--;
        UpdateHpText();
        if (hitPoints <= 0)
        {
            cleaning.CleanupTrash(this);
        }
    }

    private void UpdateHpText()
    {
        if (hpText != null)
        {
            hpText.text = hitPoints.ToString();
        }
    }
    


}
