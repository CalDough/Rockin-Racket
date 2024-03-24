using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttendeeController : MonoBehaviour
{
    /*
    Attendees manage their own boos and such
    In the future, this class may involve swapping attendees out after getting a shirt or getting a low mood score
    */
    [Header("Attendees Variables")]
    public List<ConcertAttendee> AttendeePrefabs;
    private List<ConcertAttendee> shuffledAttendees = new List<ConcertAttendee>();

    public List<Transform> AttendeeLocations;
    //public List<ConcertAttendee> ActiveAttendees;
    //public List<ConcertAttendee> HiddenAttendees;

    public List<int> ConcertTrashPerSong;

    [Header("Trash Variables")]
    public int currentTrashCleanedCount = 0;
    public int currentTrashCount = 0;
    public int maxTrashBeforePunishment = 4;
    public float trashNegativeRatingBonus = 2f;

    public static AttendeeController Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        ConcertEvents.instance.e_SongEnded.AddListener(CalculateTotalTrash);
        foreach (Transform location in AttendeeLocations)
        {
            SpawnAttendeeAtLocation(location);
        }
    }

    private void SpawnAttendeeAtLocation(Transform location)
    {
        if (shuffledAttendees.Count == 0)
        {
            shuffledAttendees = new List<ConcertAttendee>(AttendeePrefabs);
            ShuffleList(shuffledAttendees);
        }

        ConcertAttendee prefab = shuffledAttendees[shuffledAttendees.Count - 1];
        shuffledAttendees.RemoveAt(shuffledAttendees.Count - 1);

        GameObject attendeeInstance = Instantiate(prefab.gameObject, location.position, Quaternion.identity, location);
        AdjustHueAndScale(attendeeInstance);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void AdjustHueAndScale(GameObject attendeeInstance)
    {
        SpriteRenderer spriteRenderer = attendeeInstance.GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.HSVToRGB(Random.value, 0.5f, 1f); 

            Color.RGBToHSV(spriteRenderer.color, out float H, out float S, out float V);
            H += Random.Range(-0.1f, 0.1f); 
            H = Mathf.Repeat(H, 1); 
            S = Mathf.Clamp(S * 0.5f, 0.3f, 0.6f); 
            spriteRenderer.color = Color.HSVToRGB(H, S, V);
        }

        float scaleModifier = 1 + Random.Range(-0.05f, 0.1f); 
        attendeeInstance.transform.localScale *= scaleModifier;
    }

    void OnDestroy()
    {

    }

    public int GetScorePenalty()
    {
        int sum = 0;
        foreach (int trashPerSong in ConcertTrashPerSong)
        {
            sum += trashPerSong;
        }

        int penalty = sum - 5;

        if (penalty < 0)
        {
            return 0;
        }
        else
        {
            return (int)(penalty * trashNegativeRatingBonus);
        }
    }

    private void CalculateTotalTrash()
    {
        var trashObjects = GameObject.FindGameObjectsWithTag("Trash");
        currentTrashCount = trashObjects.Length;
        ConcertTrashPerSong.Add(currentTrashCount);
    }

}
