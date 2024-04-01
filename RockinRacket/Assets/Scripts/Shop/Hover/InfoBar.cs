using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBar : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    public void Hide()
    {
        background.gameObject.SetActive(false);
        //text.text = "";
    }
    public void Show(Item item) {
        background.gameObject.SetActive(true);

        string score = "";
        string difficulty = "";
        if (item.scoreMultiplier != 1)
        {
            score = $"This item increases your score from {item.itemType.ToString()}'s minigames\n";
        }
        if (item.name.Contains("Complex"))
        {
            difficulty = $"This item makes {item.itemType.ToString()}'s minigames more difficult!\n";
        }
        

        if (score.Length + difficulty.Length > 0)
        {
            text.text = score + difficulty;
        } else
        {
            text.text = "No modifiers";
        }

    }
}
