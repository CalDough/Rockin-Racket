using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
 * This script handles the player's current tool state. It is also a singleton.
 * 
 * The player is only able to have a tool state that is allowed in the current scene state. These states are tracked using a scriptable object called GameStateData
 * 
 */


public class PlayerToolState : MonoBehaviour
{
    public static PlayerToolState Instance;

    [SerializeField] GameStateData currentGameState;
    [SerializedDictionary("Player Tool State", "Cursor Texture")]
    public SerializedDictionary<PlayerTools, Texture2D> cursorTextures;
    [SerializeField] CursorMode cursorMode;

    private PlayerTools currentTool;
    private ConcertState currentConcertState;

    void Start()
    {
        // At the start of a concert, the player should start with no tool and be on the band scene.
        currentTool = PlayerTools.DEFAULTNoTool;
        currentConcertState = currentGameState.CurrentConcertState;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateToolTips();
    }

    /*
     * This method changes the mouse cursor depending on the current tool that the player has activated
     */
    private void UpdateToolTips()
    {    
        Texture2D value;
        bool hasValue;

        switch (currentTool)
        {
            case PlayerTools.DEFAULTNoTool:
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                break;
            case PlayerTools.TrashTool:
                hasValue = cursorTextures.TryGetValue(PlayerTools.TrashTool, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Trash Tool Texture Missing");
                }
                break;
            case PlayerTools.TShirtCannonTool:
                hasValue = cursorTextures.TryGetValue(PlayerTools.TShirtCannonTool, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Trash Tool Texture Missing");
                }
                break;
        }
    }

    /*
     * The following methods are called by the button when the tool is activated
     */
    public void RevertToNoTool()
    {
        // Checking to ensure the player isn't clicking the same button twice
        if (currentTool != PlayerTools.DEFAULTNoTool)
        {
            currentTool = PlayerTools.DEFAULTNoTool;
        }
    }
    public void ActivateTrashTool()
    {
        // Checking to ensure the player isn't clicking the same button twice
        if (currentTool != PlayerTools.TrashTool)
        {
            currentTool = PlayerTools.TrashTool;
        }
    }

    public void ActivateTShirtCannonTool()
    {
        // Checking to ensure the player isn't clicking the same button twice
        if (currentTool != PlayerTools.TShirtCannonTool)
        {
            currentTool = PlayerTools.TShirtCannonTool;
        }
    }
}
