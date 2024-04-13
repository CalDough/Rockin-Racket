using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionController : MonoBehaviour
{
    [Header("Concert Data Loader")]
    public ConcertData concertData;
    public string concertLevelName;
    public Material concertBackgroundMaterial;
    public Sprite concertBackstageBackground;
    public TransitionData concertSceneData;
    public SceneLoader sceneLoader;

    [Header("Concert Background Objects")]
    public Sprite dialogBackground;
    public Material merchTableBackground;
    public GameObject intermissionGroup;
    public GameObject merchTableGroup;
    public GameObject merchTableBackWall;
    public GameObject backstageBackground;
    public GameObject[] currentCharacterPositions;

    [Header("Merch Table Initialization Values")]
    public Vector2[] merchTableScoreConversions;
    [SerializeField] private MerchTable merchTable;

    [Header("Debug Mode")]
    public bool isInDebugMode;
    public ConcertData debugData;


    /*
     * In the start method we grab the current ConcertData, send it to be read, and activate the backstage background
     */
    private void Start()
    {
        // Debug Section
        if (isInDebugMode)
        {
            ConcertData gameManagerConcertData = debugData;
            ReadConcertData(gameManagerConcertData);
            GameManager.Instance.currentConcertData = debugData;
        }
        else
        {
            ConcertData gameManagerConcertData = GameManager.Instance.currentConcertData;
            ReadConcertData(gameManagerConcertData);
        }

        if (!intermissionGroup.activeSelf)
        {
            intermissionGroup.SetActive(true);
        }

        Debug.Log("<color=green> Intermission Started </color>");
    }

    /*
     * In the following method we read the ConcertData file
     */
    private void ReadConcertData(ConcertData curConcert)
    {
        if (curConcert == null)
        {
            Debug.LogError("CONCERT DATA IS NULL");
            concertLevelName = "ERROR";
        }
        else
        {
            if (curConcert.concertName != null)
            {
                concertLevelName = curConcert.concertName;

                if (concertLevelName == "LEVEL01")
                {
                    currentCharacterPositions[0].gameObject.SetActive(true);
                }
                else if (concertLevelName == "LEVEL02")
                {
                    currentCharacterPositions[1].gameObject.SetActive(true);
                }
                else if (concertLevelName == "LEVEL03")
                {
                    currentCharacterPositions[2].gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("CONCERT NAME IS NULL");
            }

            if (curConcert.concertBackground != null)
            {
                concertBackgroundMaterial = curConcert.concertBackground;
                merchTableBackWall.GetComponent<Renderer>().material = concertBackgroundMaterial;
            }
            else
            {
                Debug.LogError("CONCERT BACKGROUND MATERIAL IS NULL");
            }

            if (curConcert.backstageBackground != null)
            {
                concertBackstageBackground = curConcert.backstageBackground;
                backstageBackground.GetComponent<Image>().sprite = curConcert.backstageBackground;
            }
            else
            {
                Debug.LogError("CONCERT BACKSTAGE MATERIAL IS NULL");
            }

            if (curConcert.intermissionCharacterLocations != null)
            {
                // 0 = MJ | 1 = Kurt | 2 = Haley | 3 = Ace
                //characterLocations[0].gameObject.GetComponent<RectTransform>().anchoredPosition = curConcert.intermissionCharacterLocations[0];
                //characterLocations[1].gameObject.GetComponent<RectTransform>().anchoredPosition = curConcert.intermissionCharacterLocations[1];
                //characterLocations[1].gameObject.GetComponent<RectTransform>().anchoredPosition = curConcert.intermissionCharacterLocations[2];
                //characterLocations[1].gameObject.GetComponent<RectTransform>().anchoredPosition = curConcert.intermissionCharacterLocations[3];
            }
            else
            {
                Debug.LogError("CHARACTER LOCATIONS MISSING FOR INTERMISSION");
            }

            if (curConcert.returnToConcertLoader != null)
            {
                concertSceneData = curConcert.returnToConcertLoader;
            }
            else
            {
                Debug.LogError("CONCERT SCENE LOADER IS NULL");
            }
        }
    }

    /*
     * The following method transitions the intermission from the backstage view to the merch table
     */
    public void TransitionToMerchStand()
    {
        intermissionGroup.SetActive(false);
        merchTableGroup.SetActive(true);

        // To determine the number of merch table customers we have, we need to read the score;
        //string concertLetter = GameManager.Instance.currentConcertLetter;
        string concertLetter = GameManager.Instance.currentConcertData.currentConcertLetter;
        int numMerchTableCustomers = 0;

        switch (concertLetter)
        {
            case "A":
                numMerchTableCustomers = (int)Random.Range(merchTableScoreConversions[0].x, merchTableScoreConversions[0].y);
                break;
            case "B":
                numMerchTableCustomers = (int)Random.Range(merchTableScoreConversions[1].x, merchTableScoreConversions[1].y);
                break;
            case "C":
                numMerchTableCustomers = (int)Random.Range(merchTableScoreConversions[2].x, merchTableScoreConversions[2].y);
                break;
            case "D":
                numMerchTableCustomers = (int)Random.Range(merchTableScoreConversions[3].x, merchTableScoreConversions[3].y);
                break;
            case "F":
                numMerchTableCustomers = (int)Random.Range(merchTableScoreConversions[4].x, merchTableScoreConversions[4].y);
                break;
            default:
                numMerchTableCustomers = 5;
                break;
        }
        merchTable.InitalizeMerchTable(numMerchTableCustomers);
    }

    /*
     * This method is called when the player wants to return to the concert
     */
    public void TransitionBackToConcert()
    {
        // Setting the concert state
        //GameManager.Instance.isPostIntermission = true;
        GameManager.Instance.currentConcertData.isPostIntermission = true;

        sceneLoader.SwitchScene(concertSceneData);
    }

}
