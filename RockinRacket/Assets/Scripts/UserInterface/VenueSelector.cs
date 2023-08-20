using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
    This script is a test UI script for selecting a venue or level. It will then bring you to a song selection screen.
*/

public class VenueSelector : ScrollSelector<Venue>
{
    //public List<Venue> AllVenues;
    //public List<Venue> UnlockedVenues;
    
    public Venue SelectedVenue;
    public int currentIndex;
    public TextMeshProUGUI VenueInfoBox;
    public int SongSelectionIndex = 8;
    
    void Start()
    {
        CreateButtons();
        GameStateManager.Instance.SelectedVenue = null;
    }

    public override void OnButtonClick(int index)
    {
        // Update the selected venue
        SelectedVenue = Items[index];
        UpdateVenueInfo(SelectedVenue);
        currentIndex = index;
    }


    public void UpdateVenueInfo(Venue venue)
    {
        if (venue != null)
        {
            string baseInfo = $"Name: {venue.VenueName}\n" +
                            $"Difficulty: {venue.Difficulty}\n" +
                            $"Song Slots: {venue.SongSlots}\n" +
                            $"Venue Size: {venue.venueSize}\n" +
                            $"Is Band Battle: {venue.IsBandBattle}\n";

            string bandBattleInfo = venue.IsBandBattle
                ? $"Fame to Beat: {venue.FameToBeat}\n" +
                $"Money to Beat: {venue.MoneyToBeat}\n" +
                $"Style Score to Beat: {venue.StyleScoreToBeat}"
                : "";

            VenueInfoBox.text = baseInfo + bandBattleInfo;
        }
        else
        {
            VenueInfoBox.text = "No Venue Selected";
        }
    }

    public void EnterVenue()
    {
        Debug.Log("Entering Venue");
        if(SelectedVenue != null)
        {
            GameStateManager.Instance.SelectedVenue = SelectedVenue;
            UserInterfaceManager.Instance.SwitchSceneIndex(SongSelectionIndex);
        }
        
    }

    


}
