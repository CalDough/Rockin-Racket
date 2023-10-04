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
        animator = GetComponent<Animator>();
    }

    public void PlayTCMReaction(TShirtCannon.PressureState pressure)
    {
        switch (pressure)
        {
            case TShirtCannon.PressureState.Good:
                animator.Play("Audience_Happy");
                goodParticles.Play();
                break;

            case TShirtCannon.PressureState.Weak:
                animator.Play("Audience_Excite");
                break;

            case TShirtCannon.PressureState.Bad:
                animator.Play("Audience_Normal");
                badParticles.Play();
                break;
        }
    }
}
