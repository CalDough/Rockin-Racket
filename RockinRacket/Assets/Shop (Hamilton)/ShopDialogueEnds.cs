using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueEnds : MonoBehaviour
{
    [SerializeField] private ShopManager shopPanelManager;
    private void OnDisable()
    {
        shopPanelManager.EndShopkeeperDialogue();
    }
}
