using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text text; 

    private bool returningMenu;

    public void Open() { gameObject.SetActive(true); }
    public void Close() { gameObject.SetActive(false); }

    private void OnEnable()
    {
        if (returningMenu)
        {
            text.text = "Is there anything else I can do for you?";
        }
        else
            returningMenu = true;
    }
}
