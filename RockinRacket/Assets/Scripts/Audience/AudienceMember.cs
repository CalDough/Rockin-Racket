using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is to be used in conjunction with the Audience Prefab and the T-Shirt Cannon minigame and may reference values from
 * Animal.cs and AnimalManager.cs
 */

public class AudienceMember : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Audience audienceController;
    [SerializeField] ParticleSystem goodParticles;
    [SerializeField] ParticleSystem badParticles; 

    private void Start()
    {
        GameStateEvent.OnGameStateStart += HandleGameStateStart;
        animator = GetComponent<Animator>();
    }

    //Reacts to the TCM mini-game and changes animated state
    public void PlayTCMReaction(TShirtCannon.PressureState pressure)
    {
        switch (pressure)
        {
            case TShirtCannon.PressureState.Good:
                animator.Play("Audience_Happy");
                goodParticles.Play();
                break;

            case TShirtCannon.PressureState.Weak:
                animator.Play("Audience_Excited");
                break;

            case TShirtCannon.PressureState.Bad:
                animator.Play("Audience_Normal");
                badParticles.Play();
                break;
        }
    }

    //Reacts to the concert status and changes animated state
    public virtual void HandleGameStateStart(object sender, GameStateEventArgs e)
    {
        switch(e.stateType)
        {   
            case GameModeType.SceneIntro:
                animator.Play("Audience_Normal");
                break;
            case GameModeType.Song:
                animator.Play("Audience_Excited");
                break;
            case GameModeType.Intermission:
                animator.Play("Audience_Excited");
                break;
            case GameModeType.SceneOutro:
                animator.Play("Audience_Normal");
                break;
            default:
                break;
        }
    }

    void OnDestroy()
    {
        GameStateEvent.OnGameStateStart -= HandleGameStateStart;
    }

}
