using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandManagementUI : MonoBehaviour
{
    [SerializeField] private GameObject nextScreenButton;
    [SerializeField] private GameObject lastScreenButton;
    
    [SerializeField] private List<GameObject> screens;

    [SerializeField] private bool isLooping = true;
    

    private int currentScreenIndex = 0;


    private void Start()
    {
        SetScreen(currentScreenIndex);
    }

    private void SetScreen(int index)
    {
        UpdateButtons();
        for (int i=0; i < screens.Count; i++)
        {
            if (i == index)
            {
                screens[i].SetActive(true);
            }
            else
            {
                screens[i].SetActive(false);
            }
        }
    }

    private void UpdateButtons()
    {
        if (!isLooping)
        {
            if (currentScreenIndex == 0)
            {
                lastScreenButton.SetActive(false);
                nextScreenButton.SetActive(true);
            }
            else if (currentScreenIndex >= screens.Count - 1)
            {
                lastScreenButton.SetActive(true);
                nextScreenButton.SetActive(false);
            }
            else
            {
                lastScreenButton.SetActive(true);
                nextScreenButton.SetActive(true);
            }
        }
    }

    public void NextScreen()
    {
        currentScreenIndex = IndexCheck(currentScreenIndex + 1);
        SetScreen(currentScreenIndex);
    }

    public void LastScreen()
    {
        currentScreenIndex = IndexCheck(currentScreenIndex - 1);
        SetScreen(currentScreenIndex);
    }

    private int IndexCheck(int index)
    {
        return (index < 0, index >= screens.Count, isLooping) switch
        {
            (true, false, true) => screens.Count - 1,
            (true, false, false) => 0,
            (false, true, true) => 0,
            (false, true, false) => screens.Count - 1,
            _ => index
        };
    }
}
