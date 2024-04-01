using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemModifiers : MonoBehaviour,
IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ShopSelection shopSelection;
    [SerializeField] private CatalogManager catalogManager;
    [SerializeField] private bool isScore;

    public void OnPointerEnter(PointerEventData eventData)
    {
        shopSelection.ShowModifier(isScore, catalogManager.CurrentBandmate);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        shopSelection.StopShowingModifiers();
    }
}
