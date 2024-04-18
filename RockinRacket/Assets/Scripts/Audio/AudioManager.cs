using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public StudioEventEmitter buttonDown;
    public StudioEventEmitter buttonUp;

    public void PlayButtonDown() { buttonDown.Play(); }
    public void PlayButtonUp() { buttonUp.Play(); }
}
