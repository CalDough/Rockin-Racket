using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueEnds : MonoBehaviour
{
    [SerializeField] private ShopPanelManager shopPanelManager;
    private void OnDisable()
    {
        shopPanelManager.EndShopkeeperDialogue();
    }
}
