using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementProxy : MonoBehaviour
{
    [SerializeField] Achievements currentAchievement;

    [SerializeField] SteamworksIntegration achievementUnlocker;

    private bool connectedWithSteam;


    private void Start()
    {
        // Check to see if the application has successfully connected with steam
        achievementUnlocker = GameObject.FindGameObjectWithTag("Steamworks").gameObject.GetComponent<SteamworksIntegration>();

        if (achievementUnlocker != null)
        {
            connectedWithSteam = achievementUnlocker.connectedWithSteam;
        }
        else
        {
            connectedWithSteam = false;
        }
    }

    public void TryToUnlockAchievement()
    {
        if (!connectedWithSteam)
        {
            Debug.LogError("Application not connected with steam");
            return;
        }

        if (achievementUnlocker == null)
        {
            Debug.LogError("Application was not started from start screen so steam connection was never initialized");
            return;
        }

        // Check to see if the achievement has already been unlocked
        string enumAsString = currentAchievement.ToString();
        bool checkUnlockStatus = achievementUnlocker.IsThisAchievementUnlocked(enumAsString);

        // If it hasn't, send a request to unlock the achievement in question
        if (!checkUnlockStatus)
        {
            achievementUnlocker.UnlockAchievement(enumAsString);
            Debug.Log($"Achievement Proxy Sent Request to Steamworks Integration");
        }
    }
}
