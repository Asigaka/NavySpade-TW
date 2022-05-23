using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [Space]
    [SerializeField] private string runParametre = "velocity";

    public void SetVelocityParametre(float velocity)
    {
        anim.SetFloat(runParametre, velocity);
    }
}
