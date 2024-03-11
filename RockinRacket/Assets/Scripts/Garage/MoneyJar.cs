using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  This class updates the money indicator in the garage scene in the hub
 * 
 */

public class MoneyJar : MonoBehaviour
{
    public Slider moneyBar;
    public TMP_Text moneyText;

    private void Start()
    {
        moneyBar.value = GameManager.Instance.globalMoney;
        moneyText.text = $"Harvey's\nStash\n${GameManager.Instance.globalMoney}";
    }

    private void OnEnable()
    {
        moneyBar.value = GameManager.Instance.globalMoney;
        moneyText.text = $"Harvey's\nStash\n${GameManager.Instance.globalMoney}";
    }
}
