using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameLoadHandler gameLoadHandler;
    [SerializeField] private GameObject[] options;
    private int index = -1;

    public void Up() {
        if (index > 0)
        {
            index--;
            UpdateOptions(index + 1, index);
        }
        if (index == -1)
        {
            index = options.Length;
            UpdateOptions(index - 1, index);
        }
    }
    public void Down() {
        if (index < options.Length - 1)
        {
            index++;
            UpdateOptions(index - 1, index);
        }
        if (index == -1)
        {
            index++;
            UpdateOptions(index - 1, index);
        }
    }

    public void Select()
    {
        if (index == 0)
            gameLoadHandler.SwitchToScene(1);
        else if (index == 1)
            gameLoadHandler.SwitchToScene(10);
        else if (index == 2)
        {
            gameLoadHandler.Save();
        }
    }

    public void Reset()
    {
        options[index].transform.localScale = new Vector3(1f, 1f, 1f);
        index = -1;
    }

    private void UpdateOptions(int oldOption, int newOption)
    {
        if (newOption >= 0 && newOption < options.Length)
        {
            Debug.Log($"new index: {newOption}");
            options[newOption].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (oldOption >= 0 && oldOption < options.Length)
                options[oldOption].transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
