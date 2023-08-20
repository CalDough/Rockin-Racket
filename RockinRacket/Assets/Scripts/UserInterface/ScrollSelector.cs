using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    This script is for the scrollable object with a list of data to display. 
    Each object is a button to allow players to inform this script on which button was selected.
*/
public abstract class ScrollSelector<T> : MonoBehaviour, IScrollSelector
{
    public GameObject ButtonPrefab;
    public Transform ContentHolder;
    public List<T> Items;

    // Use this for initialization
    void Start()
    {
        CreateButtons();
    }

    virtual public void CreateButtons()
    {
        foreach (Transform child in ContentHolder)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Items.Count; i++)
        {
            GameObject buttonGO = Instantiate(ButtonPrefab, ContentHolder);

            ScrollButton scrollButton = buttonGO.GetComponent<ScrollButton>();
            if (scrollButton == null) // If it's null, check the children
            {
                scrollButton = buttonGO.GetComponentInChildren<ScrollButton>();
            }

            if (scrollButton != null)
            {
                scrollButton.Setup(i, this);
            }
            else
            {
                Debug.LogError("No ScrollButton component found on the button prefab or its children.");
            }
        }
    }

    public abstract void OnButtonClick(int index);
}
public interface IScrollSelector
{
    void OnButtonClick(int index);
}