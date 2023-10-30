using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameLoadHandler gameLoadHandler;
    [SerializeField] private GameObject[] ipodOptions;
    private int index = -1;
    private float scalingDuration = .1f;

    public void Up() {
        if (index > 0)
        {
            index--;
            UpdateOptions(index + 1, index);
        }
        if (index == -1)
        {
            index = ipodOptions.Length;
            UpdateOptions(index - 1, index);
        }
    }
    public void Down() {
        if (index < ipodOptions.Length - 1)
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
            gameLoadHandler.OpenMenu();
        else if (index == 1)
            gameLoadHandler.OpenSettings();
        else if (index == 2)
        {
            gameLoadHandler.SaveAndExit();
        }
    }

    public void Reset()
    {
        ipodOptions[index].transform.localScale = new Vector3(1f, 1f, 1f);
        index = -1;
    }

    private void UpdateOptions(int oldOption, int newOption)
    {
        if (newOption >= 0 && newOption < ipodOptions.Length)
        {
            StartCoroutine(ScaleOverTime(ipodOptions[newOption].transform, new Vector3(1.2f, 1.2f, 1f), scalingDuration));
            if (oldOption >= 0 && oldOption < ipodOptions.Length)
                StartCoroutine(ScaleOverTime(ipodOptions[oldOption].transform, new Vector3(1f, 1f, 1f), scalingDuration)); ;
        }
    }

    public IEnumerator ScaleOverTime(Transform objectTransform, Vector3 toScale, float duration)
    {
        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectTransform.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectTransform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            //transform.rotation = 
            yield return null;
        }
    }
}
