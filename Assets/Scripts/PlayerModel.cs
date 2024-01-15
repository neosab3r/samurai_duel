using System;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentNameAnimation;
    private bool isAnimationPlaying = false;

    public Action endAttackAnimationEvent;

    private void Update()
    {
        if (isAnimationPlaying == false)
        {
            return;
        }
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(currentNameAnimation))
        {
            animator.SetBool(currentNameAnimation, false);
        }
    }

    public void SetAnimState(string name, bool isPlay)
    {
        isAnimationPlaying = true;
        currentNameAnimation = name;
        animator.SetBool(name, isPlay);
    }

    public void EndAttackAnim()
    {
        endAttackAnimationEvent?.Invoke();
    }
}