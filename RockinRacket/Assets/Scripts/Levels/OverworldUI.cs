using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    This script is for a test feature for an overworld level system like in mario. Ignore for now.
*/
public class OverworldUI : MonoBehaviour
{
    [Header("Text Mesh Pro References")]
    
    [SerializeField] private GameObject hoverUIBox;
    [SerializeField] private TextMeshProUGUI hoverLevelNameText;
    [SerializeField] private TextMeshProUGUI hoverInfoText;
    
    [SerializeField] private TextMeshProUGUI enteredLevelNameText;
    [SerializeField] private TextMeshProUGUI enteredLevelText;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private LevelManager levelManager;

    private void OnEnable()
    {
        
        Debug.Log("Level Enable");
        OverworldEvent.OnLevelHovered += HandleLevelHovered;
        OverworldEvent.OnLevelUnhovered += HandleLevelUnhovered;
        OverworldEvent.OnLevelEntered += HandleLevelEntered;
    }

    private void OnDisable()
    {
        OverworldEvent.OnLevelHovered -= HandleLevelHovered;
        OverworldEvent.OnLevelUnhovered -= HandleLevelUnhovered;
        OverworldEvent.OnLevelEntered -= HandleLevelEntered;
    }

    // When a level is hovered over
    private void HandleLevelHovered(object sender, OverworldEventArgs e)
    {
        Debug.Log("Level Hovered");
        hoverUIBox.gameObject.SetActive(true);
        LevelLocation levelLocation = e.LevelLocation;
        LevelData levelData = levelLocation.levelData;

        string entryRequirements = "";
        string startRequirements = "";
        entryRequirements = GetEntryRequirements(levelLocation);
        startRequirements = GetStartRequirements(levelData);
    
        hoverLevelNameText.text = $"{levelData.levelName}";

        if(!string.IsNullOrEmpty(entryRequirements))
        {
        hoverInfoText.text += $"Entry Requirements\n{entryRequirements}";
        }
        if(!string.IsNullOrEmpty(startRequirements))
        {
            hoverInfoText.text += $"Start Requirements\n{startRequirements}";
        }
        
        
    }

    // When a level is unhovered
    private void HandleLevelUnhovered(object sender, OverworldEventArgs e)
    {
        hoverUIBox.gameObject.SetActive(false);
        hoverInfoText.text = "";
        hoverLevelNameText.text = "";

    }

    // When a level is entered
    private void HandleLevelEntered(object sender, OverworldEventArgs e)
    {
        
        Debug.Log("Level Entered");
        LevelLocation levelLocation = e.LevelLocation;
        LevelData levelData = levelLocation.levelData;

        string levelInfo = $"{levelData.levelDescription}\n{levelData.levelInformation}\n";
        string entryRequirements = GetEntryRequirements(levelLocation);
        string startRequirements = GetStartRequirements(levelData);
        

        if(!string.IsNullOrEmpty(entryRequirements))
        {
            levelInfo += $"Entry Requirements\n{entryRequirements}";
        }
        if(!string.IsNullOrEmpty(startRequirements))
        {
            levelInfo += $"Start Requirements\n{startRequirements}";
        }
        enteredLevelNameText.text = $"{levelData.levelName}";
        enteredLevelText.text = levelInfo;
    }

    private string GetStartRequirements(LevelData levelData)
    {
        string startRequirements = "";
        if (levelData != null)
        {
            if(!levelData.requirementsDisabled)
            {
                if (levelData.fameRequirements > 0)
                startRequirements += $"Fame: {gameManager.globalFame}/{levelData.fameRequirements}\n";

                if (levelData.moneyRequirements > 0)
                    startRequirements += $"Money: {gameManager.globalMoney}/{levelData.moneyRequirements}\n";

                if (levelData.itemRequirements.Count > 0)
                {
                    startRequirements += "Items:\n";
                    foreach (string item in levelData.itemRequirements)
                    {
                        if(inventory.HasItem(item))
                        {
                            startRequirements += $"- {item} ✔️\n";
                        }
                        else
                        {
                            startRequirements += $"- {item} ❌\n";
                        }
                    }
                }
            }
            
        }
        return startRequirements;
    }

    private string GetEntryRequirements(LevelLocation levelLocation)
    {
        string entryRequirements = "";
        if (!levelLocation.moveRequirementsDisabled)
        {
            if (levelLocation.fameRequirementsToMove > 0)
                entryRequirements += $"Fame: {gameManager.globalFame}/{levelLocation.fameRequirementsToMove}\n";

            if (levelLocation.moneyRequirementsToMove > 0)
                entryRequirements += $"Money: {gameManager.globalMoney}/{levelLocation.moneyRequirementsToMove}\n";

            if (levelLocation.itemRequirementsToMove.Count > 0)
            {
                entryRequirements += "Items:\n";
                foreach (string item in levelLocation.itemRequirementsToMove)
                {
                    if(inventory.HasItem(item))
                    {
                        entryRequirements += $"- {item} ✔️\n";
                    }
                    else
                    {
                        entryRequirements += $"- {item} ❌\n";
                    }
                }
            }
        }
        return entryRequirements;
    }







}