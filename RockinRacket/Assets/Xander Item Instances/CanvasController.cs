using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Public variables
    [SerializeField] GameStateData currentGameState;


    // Private Variables
    private GameObject BandViewPanel;
    private GameObject ShopViewPanel;
    private GameObject BackStageViewPanel;
    private GameObject AudienceViewPanel;
    private GameObject VenueView;


    // Start is called before the first frame update
    void Start()
    {
        // Referencing the child panel objects associated with each view
        BandViewPanel = gameObject.transform.GetChild(0).GetComponent<GameObject>();
        ShopViewPanel = gameObject.transform.GetChild(0).GetComponent<GameObject>();
        BackStageViewPanel = gameObject.transform.GetChild(0).GetComponent<GameObject>();
        AudienceViewPanel = gameObject.transform.GetChild(0).GetComponent<GameObject>();
        VenueView = gameObject.transform.GetChild(0).GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
