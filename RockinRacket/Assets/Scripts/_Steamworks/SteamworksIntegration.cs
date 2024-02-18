using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamworksIntegration : MonoBehaviour
{
    public static SteamworksIntegration instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(2767790);
            PrintYourName();
        }
        catch (System.Exception e)
        {
            // Something went wrong connecting with steam
            Debug.LogException(e);
        }
    }

    private void PrintYourName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    /*
     * Check to receive networking data from steam
     */
    private void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    /*
     * Shutdown connection to steam on game close
     */
    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }

    /*
     * The following methods are used for managing achievements
     * 
     */

    // Checks to see if an achievement is unlocked
    public void IsThisAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);

        Debug.Log($"Achievement {id} status: " + ach.State);
    }

    // Unlocks an achievement
    public void UnlockAchievement(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();

        Debug.Log($"Achievement {id} unlocked");
    }

    // Mainly for testing purposes
    public void ClearAchievementStatus(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();

        Debug.Log($"Achievement {id} cleared");
    }
}
