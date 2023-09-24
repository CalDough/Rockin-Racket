using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    [SerializeField] TransitionData NextScene;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] public bool dialogueIsPlaying { get; private set; } = false;

    [SerializeField] public bool submitPressed { get; private set; } = false;
    public TextAsset DialogueFile;

    public Story currentStory;



}
