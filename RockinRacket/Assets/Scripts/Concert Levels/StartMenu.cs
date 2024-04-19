using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  This script controls the start screen at the start of every concert
 * 
 * 
 */


public class StartMenu : MonoBehaviour
{
    [Header("Line Segment Rendering")]
    public GameObject[] lineSegments;
    [SerializeField] private int lineSegmentCounter = 0;
    [SerializeField] private int maxLineSegments;
    public float lineDisplayRate = .5f;

    [Header("Face Loading")]
    public GameObject[] faces;
    [SerializeField] private int faceCounter = 0;
    [SerializeField] private int maxFaces;
    public GameObject faceBox;

    [Header("Start Button")]
    public GameObject startButton;

    void Start()
    {
        startButton.SetActive(false);
    }
    // This method is called by ConcertManager when the concert is loaded in for the first time
    public void ActivateStartScreenAnimationAndLoadingFaces()
    {
        Debug.Log("<color=green> Activating loading screen animations </color>");

        maxLineSegments = lineSegments.Length;
        maxFaces = faces.Length;

        StartCoroutine(DisplayFaceLoading());
        StartCoroutine(DisplayLineAnimation());
    }

    // This coroutine controls the face loading animation
    private IEnumerator DisplayFaceLoading()
    {
        while (faceCounter < maxFaces)
        {
            if (faceCounter == 0)
            {
                faces[maxFaces - 1].gameObject.SetActive(false);
                faces[faceCounter].gameObject.SetActive(true);
            }
            else
            {
                faces[faceCounter].gameObject.SetActive(true);
                faces[faceCounter - 1].gameObject.SetActive(false);
            }
            
            faceCounter++;

            if (faceCounter >= maxFaces)
            {
                faceCounter = 0;
            }

            yield return new WaitForSeconds(.8f);
        }
    }

    // This coroutine controls the line walking animation
    private IEnumerator DisplayLineAnimation()
    {
        while (lineSegmentCounter < maxLineSegments)
        {
            lineSegments[lineSegmentCounter].gameObject.SetActive(true);
            lineSegmentCounter++;

            yield return new WaitForSeconds(lineDisplayRate);
        }

        RevealLevelStartButton();
    }

    // This method reveals the start button
    private void RevealLevelStartButton()
    {
        Debug.Log("Activating Start Button");
        StopCoroutine(DisplayFaceLoading());
        faceBox.gameObject.SetActive(false);
        startButton.SetActive(true);
        startButton.gameObject.GetComponent<Image>().enabled = true;
        startButton.gameObject.GetComponent<Button>().interactable = true;
    }
}
