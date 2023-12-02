using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] private GameObject boardScreen;

    public void LookAtBoard()
    {
        boardScreen.SetActive(true);
    }

    private void Start()
    {
        boardScreen.SetActive(false);
    }
}
