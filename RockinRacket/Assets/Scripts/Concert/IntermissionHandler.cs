using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionHandler : MonoBehaviour
{
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
        }
    }
    
    private void HandleGameStateEnd(object sender, GameStateEventArgs e)
    {
        nextStateButton.gameObject.SetActive(false);
    }
}
