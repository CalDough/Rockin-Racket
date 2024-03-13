using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private CreditsPage[] creditsPages;
    //[SerializeField] private GameObject nextBtn;
    [SerializeField] private float animationTime;
    private int currentPage = -1;
    private bool isAnimating = false;

    private void Start()
    {
        foreach (CreditsPage page in creditsPages)
        {
            page.FadeOut(0f);
        }
        StartCoroutine(NextPage());
    }

    public IEnumerator NextPage()
    {

        if (creditsPages.Length - 1 > currentPage && !isAnimating)
        {
            isAnimating = true;
            // fade out old page
            if (currentPage >= 0)
            {
                creditsPages[currentPage].FadeOut(animationTime);
                yield return new WaitForSeconds(animationTime);
            }
            // fade in new page
            currentPage++;
            creditsPages[currentPage].FadeIn(animationTime);
            yield return new WaitForSeconds(animationTime);
            isAnimating = false;
        }
    }
}
