using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ItemOption : MonoBehaviour, 
IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject selection;
    private Image sImage;
    private TMP_Text sName;
    private TMP_Text sDescription;


    //public SpriteRenderer highlight;
    public ItemTest item;

    [SerializeField] private Graphic graphic;

    private void Awake()
    {
        GetComponent<Image>().sprite = item.Sprite;
        sImage = selection.transform.GetChild(0).GetComponent<Image>();
        sName = selection.transform.GetChild(1).GetComponent<TMP_Text>();
        sDescription = selection.transform.GetChild(2).GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //print("Mouse Enter " + item.ItemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //print("Mouse Exit " + item.ItemName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //print("Mouse Down " + item.ItemName);
        sImage.sprite = item.Sprite;
        sName.text = item.ItemName;
        sDescription.text = item.Description;
    }
}
//{
//    public GameObject selection;
//    private Image sImage;
//    private TMP_Text sName;
//    private TMP_Text sDescription;


//    //public SpriteRenderer highlight;
//    public ItemTest item;

//    private Color highlightColor;

//    void Start()
//    {
//        GetComponent<Image>().sprite = item.Sprite;
//        sImage = selection.transform.GetChild(0).GetComponent<Image>();
//        sName = selection.transform.GetChild(1).GetComponent<TMP_Text>();
//        sDescription = selection.transform.GetChild(2).GetComponent<TMP_Text>();
//        //menuName = (TMP_Text)GameObject.Find("MenuName").GetComponent("TMPro");
//        //menuDescription = (TMP_Text)GameObject.Find("MenuDescription").GetComponent("TMPro");
//        //highlightColor = highlight.color;
//        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
//        //if (Instrument)
//        //{
//        //    image.sprite = shipBase.ShipSprite;
//        //}
//        //if (Minigame)
//        //{
//        //    image.sprite = shieldBase.Circle;
//        //    image.color = shieldBase.ShieldColor;
//        //}
//        //if (Stage)
//        //{
//        //    image.sprite = weaponBase.WeaponSprite;
//        //}
//    }

//    private void OnMouseEnter()
//    {
//        print("Mouse Enter " + item.ItemName);
//        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 1);
//        sName.text = item.ItemName;
//        sDescription.text = item.Description;
//        sImage.sprite = item.Sprite;
//    }

//    private void OnMouseExit()
//    {
//        print("Mouse Exit " + item.ItemName);
//        //highlight.color = new Color(highlightColor.r, highlightColor.g, highlightColor.b, 0);
//    }
//    private void OnMouseDown()
//    {
//        print("Mouse Down " + item.ItemName);
//    }
//}
