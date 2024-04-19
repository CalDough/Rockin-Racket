using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private CreditsPage[] creditsPages;
    //[SerializeField] private GameObject nextBtn;
    [SerializeField] private float animationTime;
    [SerializeField] private float animationDelay;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private TransitionData transition;
    private int currentPage = -1;
    private bool isAnimating = false;

    private void Start()
    {
        foreach (CreditsPage page in creditsPages)
        {
            print(page.gameObject.name);
            page.gameObject.SetActive(true);
            page.GetUIFades();
            page.SetAlpha(0f);
        }
        StartCoroutine(CreditsAnimation());
    }

    private IEnumerator CreditsAnimation()
    {
        yield return new WaitForSeconds(1f);
        while (creditsPages.Length - 1 > currentPage)
        {
            StartCoroutine(NextPage());
            yield return new WaitForSeconds(animationTime * 2 + animationDelay);
        }
        sceneLoader.SwitchScene(transition);
    }

    public void NextBtn()
    {
        StartCoroutine(NextPage());
    }

    private IEnumerator NextPage()
    {
        // TODO: get rid of if statement
        if (creditsPages.Length - 1 > currentPage && !isAnimating)
        {
            isAnimating = true;
            // fade out old page
            if (currentPage >= 0)
            {
                print("animating out");
                creditsPages[currentPage].FadeOut(animationTime);
                yield return new WaitForSeconds(animationTime);
            }
            // fade in new page
            print("animating in");
            currentPage++;
            creditsPages[currentPage].FadeIn(animationTime);
            yield return new WaitForSeconds(animationTime);
            print("done animating");
            isAnimating = false;
        }
        else
        {
            print("nextPage failure");
        }
    }
}
