using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TShirtCannon;
using UnityEngine.EventSystems;
using static UnityEngine.InputSystem.UI.VirtualMouseInput;
using AYellowpaper.SerializedCollections;

public class MerchTableHandler : MonoBehaviour
{
    [SerializeField] GameStateData currentGameState;
    [SerializedDictionary("Merch Item", "Cursor Texture")]
    public SerializedDictionary<CustomerWants, Texture2D> cursorTextures;
    [SerializeField] UnityEngine.CursorMode cursorMode;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHeldMerchItem();
    }

    private void UpdateHeldMerchItem()
    {
        Texture2D value;
        bool hasValue;

        switch (currentGameState.currentlyHeldObject)
        {
            case CustomerWants.None:
                Cursor.SetCursor(null, Vector2.zero, cursorMode);
                break;
            case CustomerWants.tshirt:
                hasValue = cursorTextures.TryGetValue(CustomerWants.tshirt, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            case CustomerWants.mug:
                hasValue = cursorTextures.TryGetValue(CustomerWants.mug, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            case CustomerWants.button:
                hasValue = cursorTextures.TryGetValue(CustomerWants.button, out value);
                if (hasValue)
                {
                    Cursor.SetCursor(value, Vector2.zero, cursorMode);
                }
                else
                {
                    Debug.LogError("Item Texture Missing");
                }
                break;
            default:
                Debug.Log("<color=red>MISSING CURSOR TEXTURE MERCHTABLEHANDLER.CS </color>");
                break;
        }
    }
}
