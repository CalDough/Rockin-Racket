using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemOption : MonoBehaviour
{
    public GameObject selection;
    private Image sImage;
    private TMP_Text sName;
    private TMP_Text sDescription;
    

    //public SpriteRenderer highlight;
    public ItemTest item;
    
    private Color highlightColor;

    void Start()
    {
        GetComponent<Image>().sprite = item.Sprite;
        sImage = selection.transform.GetChild(0).GetComponent<Image>();
        sName = selection.transform.GetChild(1).GetComponent<TMP_Text>();
        sDescription = selection.transform.GetChild(2).GetComponent<TMP_Text>();
        //menuName = (TMP_Text)GameObject.Find("MenuName").GetComponent("TMPro");
        //menuDescription = (TMP_Text)GameObject.Find("MenuDescription").GetComponent("TMPro");
        //highlightColor = highlight.color;
        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
        //if (Instrument)
        //{
        //    image.sprite = shipBase.ShipSprite;
        //}
        //if (Minigame)
        //{
        //    image.sprite = shieldBase.Circle;
        //    image.color = shieldBase.ShieldColor;
        //}
        //if (Stage)
        //{
        //    image.sprite = weaponBase.WeaponSprite;
        //}
    }

    private void OnMouseEnter()
    {
        print("Mouse Entered " + item.ItemName);
        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 1);
        sName.text = item.ItemName;
        sDescription.text = item.Description;
        sImage.sprite = item.Sprite;
    }

    private void OnMouseExit()
    {
        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
    }
    private void OnMouseDown()
    {

    }
}
