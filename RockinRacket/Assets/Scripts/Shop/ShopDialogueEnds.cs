using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialogueEnds : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;
    private void OnDisable()
    {
        shopManager.EndShopkeeperDialogue();
    }
}
