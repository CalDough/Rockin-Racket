using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionController : MonoBehaviour
{
    [Header("Gameobject References")]
    [SerializeField] private GameObject IntermissionObjects;
    [SerializeField] private MerchTable merchTable;
    [SerializeField] private GameObject merchStandObjects;
    public SceneLoader sceneLoader;
    public TransitionData[] returnToConcert;



    private void Start()
    {
        if (!IntermissionObjects.activeSelf)
        {
            IntermissionObjects.SetActive(true);
        }

        Debug.Log("<color=green> Intermission Started </color>");
    }

    public void TransitionToMerchStand()
    {
        IntermissionObjects.SetActive(false);
        merchStandObjects.SetActive(true);
        merchTable.InitalizeMerchTable(GameManager.Instance.numMerchTableCustomers);
    }

    public void TransitionBackToConcert()
    {
        GameManager.Instance.isPostIntermission = true;

        // Hard coded for level 1 atm, will be fixed later
        sceneLoader.SwitchScene(returnToConcert[0]);
    }

}
