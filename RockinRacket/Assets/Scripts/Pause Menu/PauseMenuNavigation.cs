using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameLoadHandler gameLoadHandler;
    [SerializeField] private GameObject[] ipodOptions;
    private int index = -1;
    private float scalingDuration = .1f;

    public void Up() {
        Debug.Log($"starting index: {index}");
        if (index > 0)
        {
            index--;
            UpdateOptions(index + 1, index);
        }
        if (index == -1)
        {
            index = ipodOptions.Length - 1;
            UpdateOptions(index + 1, index);
        }
        Debug.Log($"finishing index: {index}");
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
            if (SceneManager.GetActiveScene().buildIndex != 0)
                gameLoadHandler.OpenMainMenu();
            else
                gameLoadHandler.ToggleMenu();
        else if (index == 1)
            if (SceneManager.GetActiveScene().buildIndex != 8)
                gameLoadHandler.OpenSettings();
            else
                gameLoadHandler.ToggleMenu();
        else if (index == 2)
        {
            gameLoadHandler.SaveAndExit();
        }
    }

    public void Reset()
    {
        if (index != -1)
            ipodOptions[index].transform.localScale = new Vector3(1f, 1f, 1f);
        index = -1;
    }

    private void UpdateOptions(int oldOption, int newOption)
    {
        if (newOption >= 0 && newOption < ipodOptions.Length)
        {
            Debug.Log($"successfully visited index: {newOption}");
            StartCoroutine(ScaleOverTime(ipodOptions[newOption].transform, new Vector3(1.2f, 1.2f, 1f), scalingDuration));
            if (oldOption >= 0 && oldOption < ipodOptions.Length)
                StartCoroutine(ScaleOverTime(ipodOptions[oldOption].transform, new Vector3(1f, 1f, 1f), scalingDuration)); ;
        }
    }

    public IEnumerator ScaleOverTime(Transform objectTransform, Vector3 toScale, float duration)
    {
        //if (SceneManager.GetActiveScene().buildIndex == 8)
        //{
        //    objectTransform.localScale = toScale;
        //    yield break;
        //}

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectTransform.localScale;

        while (counter < duration)
        {
            counter += Time.unscaledDeltaTime;
            objectTransform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            //transform.rotation = 
            yield return null;
        }
    }
}
