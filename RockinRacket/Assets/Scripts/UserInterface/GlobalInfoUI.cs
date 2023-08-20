using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/*
    This script is a test UI script for the displaying variables on the Hub screen and Shop
*/
public class GlobalInfoUI : MonoBehaviour
{
    
    public TextMeshProUGUI InfoText;
    private GameManager gm;

    public void Start()
    {
        if(gm == null)
        {
            if(GameManager.Instance != null)
            {
                gm = GameManager.Instance;
                if(InfoText != null)
                {
                    DisplayPlayerInfo();
                }
            }

        }
    }

    public void DisplayPlayerInfo()
    {
        string playerInfo = "";

        playerInfo += "Money: " + gm.globalMoney + "\n";
        playerInfo += "Fame: " +  gm.globalFame + "\n";
        playerInfo += "Praise: " + gm.praise + "\n";
        playerInfo += "Attention: " + gm.attention + "\n";
        playerInfo += "Difficulty Mode: " + gm.SetMode + "\n";

        InfoText.text = playerInfo;
    }


}
