using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionHandler : MonoBehaviour
{
    [SerializeField] private GameObject intermissionScreen; // Ken added code

    public Button nextStateButton;

    public void StartNextState()
    {
        if(GameStateManager.Instance.CurrentGameState.UseDuration == false)
        {
            GameStateManager.Instance.EndCurrentGameState();
        }
    }

    void Start()
    {
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        GameStateEvent.OnGameStateEnd += HandleGameStateEnd;

        intermissionScreen.SetActive(false); // Ken added code
    }
    
    void OnDestroy()
    {
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
        GameStateEvent.OnGameStateEnd -= HandleGameStateEnd;
    }

    public void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        if(e.state.UseDuration == false)
        {
            nextStateButton.gameObject.SetActive(true);
            
            intermissionScreen.SetActive(true); // Ken added code
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        nextStateButton.gameObject.SetActive(false);
        
        intermissionScreen.SetActive(false); // Ken added code
    }
}
