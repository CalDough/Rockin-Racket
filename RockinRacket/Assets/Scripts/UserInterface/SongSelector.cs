using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    This script is a test UI script for Selecting Songs after the player has selected a Venue to play at.
*/

public class SongSelector : ScrollSelector<SongData>
{
    public SongData SelectedSong;
    public int currentIndex;
    public TextMeshProUGUI SongInfoBox;
    public TextMeshProUGUI SelectedSongInfoBox;
    public TransitionData transitionDataToConcert;
    public GameLoadHandler gameLoadHandler;

    void Start()
    {
        CreateButtons();
    }

    public override void OnButtonClick(int index)
    {
        // Update the selected venue
        SelectedSong = Items[index];
        UpdateSongInfo();
        UpdateSelectedSongInfo();
        currentIndex = index;
    }

    public void AddSong()
    {
        if(SelectedSong == null)
        {return;}
        if( GameStateManager.Instance.CheckCurrentSongs() == true)
        {
            
            GameStateManager.Instance.SelectCustomSong(SelectedSong);
            UpdateSongInfo();
            UpdateSelectedSongInfo();
        }
    }
    public void SongRemove()
    {
        GameStateManager.Instance.RemoveCustomSongs();
        UpdateSongInfo();
        UpdateSelectedSongInfo();
    }

    public void UpdateSongInfo()
    {
        List<GameState> allSongs = GameStateManager.Instance.GetAllSongs();

        if (allSongs.Count > 0)
        {
            string songInfo = "";
            foreach (GameState song in allSongs)
            {
                songInfo += $"Song Name: {song.Song.SongName}\n" +
                            $"Duration: {song.Song.Duration}\n" +
                            $"Difficulty: {song.Song.Difficulty}\n"; 
            }
            SongInfoBox.text = songInfo;
        }
        else
        {
            SongInfoBox.text = "No songs selected for this venue.";
        }
    }

    public void UpdateSelectedSongInfo()
    {
        if (SelectedSong != null)
        {
            string songTypes = "";
            foreach (SongType type in SelectedSong.SongTypes)
            {
                songTypes += type.ToString() + ", ";
            }

            // Remove trailing comma and space
            if (songTypes.Length > 0)
            {
                songTypes = songTypes.Remove(songTypes.Length - 2);
            }
            GameStateManager.Instance.CheckCurrentSongs();
            string songInfo =
                            $"Select up to: {GameStateManager.Instance.songSlotsAvailable} more songs\n"+ 
                            $"Song Name: {SelectedSong.SongName}\n" +
                            $"Description: {SelectedSong.Description}\n" +
                            $"Duration: {SelectedSong.Duration}\n" +
                            $"Difficulty: {SelectedSong.Difficulty}\n" +
                            $"Song Types: {songTypes}\n"
                            ;

            SelectedSongInfoBox.text = songInfo;
        }
        else
        {
            SelectedSongInfoBox.text = "No song selected.";
        }
    }

    public void StartConcert()
    {
        List<GameState> allSongs = GameStateManager.Instance.GetAllSongs();
        if(allSongs.Count > 0)
        {
            //if(GameStateManager.Instance.SelectedVenue.SceneIndex;
            gameLoadHandler.SwitchToScene(transitionDataToConcert.sceneIndex);
        //CustomSceneEvent.CustomTransitionCalled(transitionDataToConcert);
        //GameStateManager.Instance.StartConcert();
        }
    }

}
