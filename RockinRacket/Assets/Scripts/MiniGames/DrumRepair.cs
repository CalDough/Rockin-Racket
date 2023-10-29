using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumRepair : MiniGame
{
    [SerializeField] public List<RectTransform> drumHoles;
    [SerializeField] public List<PatchPiece> patchPieces;
    [SerializeField] private RectTransform patchRectTransform;
    [SerializeField] private List<Vector2> predefinedHolePositions;
    [SerializeField] private List<GameObject> predefinedHolePoints;
    public bool randomMember = false;
    public BandRoleName bandRole = BandRoleName.Ace;
    public float BrokenLevelChange = 1;
    
    public override void Activate()
    {
        base.Activate();
        ConcertAudioEvent.AudioBroken(this, BrokenLevelChange, bandRole, true);
        RestartMiniGameLogic();
    }

    public override void Complete()
    {
        base.Complete();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, bandRole, true);
    }
    
    public override void Miss()
    {
        isActiveEvent = false;
        if (durationCoroutine != null) {
            StopCoroutine(durationCoroutine);
        }
        GameEvents.EventMiss(this);
        GameEvents.EventClosed(this);
        HandleClosing();
        ConcertAudioEvent.AudioFixed(this, BrokenLevelChange, bandRole, true);
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

    void Start()
    {
        if(randomMember == true)
        {
            var excludedValues = new List<BandRoleName> { BandRoleName.Default, BandRoleName.Harvey, BandRoleName.Speakers };
            BandRoleName randomValue = BandRoleEnumHelper.GetRandomBandRoleName(excludedValues);
        }

        
        predefinedHolePositions = new List<Vector2>();
        foreach (var point in predefinedHolePoints)
        {
            predefinedHolePositions.Add((Vector2)point.transform.position);
        }
    }


    public override void RestartMiniGameLogic()
    {
        RandomizeHoleLocations();
        IsCompleted = false;
    }

    private void RandomizeHoleLocations()
    {
        List<Vector2> availablePositions = new List<Vector2>(predefinedHolePositions);

        ShuffleList(availablePositions);
        /*
        foreach (RectTransform hole in drumHoles)
        {
            RectTransform patchRect = hole.GetComponent<RectTransform>();
            float width = patchRect.rect.width;
            float height = patchRect.rect.height;

            Vector3[] corners = new Vector3[4];
            patchRectTransform.GetWorldCorners(corners);
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] = transform.InverseTransformPoint(corners[i]);
            }

            float x = Random.Range(corners[0].x + width / 2, corners[3].x - width / 2);
            float y = Random.Range(corners[0].y + height / 2, corners[1].y - height / 2);
            
            patchRect.localPosition = new Vector2(x, y);
            DrumHole currentHole = hole.GetComponent<DrumHole>();
            currentHole.isFilled = false;
        }*/
        
        for (int i = 0; i < drumHoles.Count; i++)
        {
            if (availablePositions.Count > 0)
            {
                Vector2 localPosition = drumHoles[i].parent.GetComponent<RectTransform>().InverseTransformPoint(availablePositions[0]);
                drumHoles[i].anchoredPosition = localPosition;
                
                availablePositions.RemoveAt(0);
                
                DrumHole currentHole = drumHoles[i].GetComponent<DrumHole>();
                currentHole.isFilled = false;
            }
        }
        foreach (PatchPiece patch in patchPieces)
        {
            RectTransform patchRect = patch.GetComponent<RectTransform>();
            float width = patchRect.rect.width;
            float height = patchRect.rect.height;

            Vector3[] corners = new Vector3[4];
            patchRectTransform.GetWorldCorners(corners);
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i] = transform.InverseTransformPoint(corners[i]);
            }

            float x = Random.Range(corners[0].x + width / 2, corners[3].x - width / 2);
            float y = Random.Range(corners[0].y + height / 2, corners[1].y - height / 2);
            
            patchRect.localPosition = new Vector2(x, y);
            patch.canDrag = true;
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public override void HandleOpening()
    {
        if(!IsCompleted)
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
        }
    }

    public void CheckForCompletion()
    {
        bool allRepaired = true;
        foreach (RectTransform hole in drumHoles)
        {
            DrumHole currenthole = hole.GetComponent<DrumHole>();
            if (!currenthole.isFilled)
            {
                allRepaired = false;
                break;
            }
        }

        if (allRepaired)
        {
            CompleteMiniGame();
        }
    }

    private void CompleteMiniGame()
    {
        if(!isActiveEvent || IsCompleted)
        {return;}
        Debug.Log("Drum Repair game completed!");
        this.IsCompleted = true;
        this.Complete();
    }
}
