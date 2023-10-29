using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This script is used to hide certain UI elements during particular scenes when they are placed on the CONSTANT group underneath the canvas
 */
public class ViewSpecificButtons : MonoBehaviour
{
    [SerializeField] GameStateData currentGameState;
    [SerializeField] ConcertState allowedView;

    private Animator anim;
    private ConcertState currentConcertState;
    private PlayerTools currentPlayerTools;
    private delegate void ButtonVisibilityModifier();
    ButtonVisibilityModifier myView;

    // Start is called before the first frame update
    void Start()
    {
        currentConcertState = currentGameState.CurrentConcertState;
        currentPlayerTools = currentGameState.CurrentToolState;

        switch (currentConcertState)
        {
            case ConcertState.BandView:
                myView = AllowedInBandView;
                break;
            case ConcertState.BackstageView:
                myView = AllowedInBackstageView;
                break;
            case ConcertState.AudienceView:
                myView = AllowedInAudienceView;
                break;
            case ConcertState.ShopView:
                myView = AllowedInShopView;
                break;
            case ConcertState.VenueView:
                myView = AllowedInVenueView;
                break;
            default:
                Debug.LogError("No matching concert state in ViewSpecificButton.cs");
                break;
        }
    }

    private void Update()
    {
        CheckForStateChange();
    }

    private void CheckForStateChange()
    {
        if (currentConcertState == allowedView)
        {
            if (gameObject.activeSelf)
            {
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    /*
     * The following methods are delegated depending on the allowed view
     */
    private void AllowedInBandView()
    {
        //gameObject.SetActive(true);
        anim.Play("HypeComfortBarEntrance");
    }
    private void AllowedInBackstageView()
    {
        //gameObject.SetActive(true);
        anim.Play("HypeComfortBarEntrance");
    }
    private void AllowedInAudienceView()
    {
        //gameObject.SetActive(true);
        anim.Play("HypeComfortBarEntrance");
    }
    private void AllowedInShopView()
    {
        //gameObject.SetActive(true);
        anim.Play("HypeComfortBarEntrance");
    }
    private void AllowedInVenueView()
    {
        //gameObject.SetActive(true);
        anim.Play("HypeComfortBarEntrance");
    }


}
