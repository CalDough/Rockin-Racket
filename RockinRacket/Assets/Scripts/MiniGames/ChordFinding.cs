using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ChordFinding : MiniGame
{
    [SerializedDictionary("Chord", "Num Notes")]
    public SerializedDictionary<GameObject, int> chordKey;
    [SerializeField] GameObject[] movingCircles;
    [SerializeField] float scaleFactor;

    private bool isActive = false;
    private int clickedCount;
    private int requiredClicks;
    private GameObject chosenChord;
    private bool startedShrinking = false;

    // Start is called before the first frame update
    void Start()
    {
        clickedCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (startedShrinking == false)
            {
                startedShrinking = true;
                ShrinkCircles();
            }
        }
    }

    public void IncrementClickCount()
    {
        clickedCount++;
        if (clickedCount >= requiredClicks)
        {
            Complete();
        }
    }

    private void ChooseChord()
    {
        int randomDictElem = Random.Range(0, chordKey.Count);
        chosenChord = chordKey.ElementAt(randomDictElem).Key;
        chosenChord.gameObject.SetActive(true);
        requiredClicks = chordKey.ElementAt(randomDictElem).Value;

        //ShrinkCircles();

        StartCoroutine(DeactivateChosenChord(2));

        Debug.Log("Chord Chosen is: " + chosenChord.name);
    }

    private void ShrinkCircles()
    {
        foreach (GameObject circ in movingCircles)
        {
            circ.gameObject.GetComponent<RawImage>().enabled = true;
            //circ.gameObject.GetComponent<MovingCircle>().StartToShrink();
        }
    }

    public override void Activate()
    {
        IsCompleted = false;
        isActive = true;
        startedShrinking = false;
        Debug.Log("Event activated");

        clickedCount = 0;
        ChooseChord();


        base.Activate();
    }

    public override void Complete()
    {
        Debug.Log("Event complete");
        base.Complete();
    }

    public override void Miss()
    {
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void OpenEvent()
    {
        GameEvents.EventOpened(this);
        HandleOpening();
    }
    public override void CloseEvent()
    {
        GameEvents.EventClosed(this);
        HandleClosing();
    }

    public override void HandleOpening()
    {
        if (!IsCompleted)
        {
            Panels.SetActive(true);
        }
    }
    public override void HandleClosing()
    {
        Panels.SetActive(false);
        if (IsCompleted == false)
        {
            //RestartMiniGameLogic(); 
            chosenChord.gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateChosenChord(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(counter);
            counter--;
        }
        Debug.Log("Turning Off Chord");
        chosenChord.gameObject.SetActive(false);

    }
}
