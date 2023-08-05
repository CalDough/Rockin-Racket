using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcertUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        AudioManager.Instance.CreateSoundObjects();
        GameStateManager.Instance.StartConcert();
        
    }
}
