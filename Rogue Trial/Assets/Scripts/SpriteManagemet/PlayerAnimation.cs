using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField, HideInInspector]
    public SpriteRenderer spriteRenderer;

    [SerializeField, HideInInspector]
    public Animator animator;

    [SerializeField, HideInInspector]
    public PlayerMovement movement;

#pragma warning disable CS0109
    [SerializeField, HideInInspector]
    private new Rigidbody2D rigidbody2D;
#pragma warning restore CS0109

    private void OnValidate()
    {
        if(Application.isEditor)
        
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
    public void SetToAttack() => SetAnimationState(AnimationState.ATTACKING);

    public void SetToJump() => SetAnimationState(AnimationState.START_JUMP);

    public enum AnimationState
    {
        IDLE_FLOAT = 0, START_JUMP = 1, JUMPING = 2, FALLING = 3, DEFAULT = 4, DASHING = 5, ATTACKING = 6
    }

    private void SetAnimationState(AnimationState animationState)
    {
        animator.SetInteger("State", (int)animationState);
    }


    private void Start()
    {
        SetAnimationState(AnimationState.DEFAULT);
    }

    AnimationState animationState = AnimationState.DEFAULT;
    private void Update()
    {
        AnimationState tmp = DetermineState(animationState);
        if(tmp!= animationState)
            SetState(tmp);
    }

    private AnimationState DetermineState(AnimationState animationState)
    {
        switch(animationState)
        {
            case AnimationState.IDLE_FLOAT:
                if (movement.IsDashing == true)
                    return AnimationState.DASHING;
                break;
            case AnimationState.START_JUMP:
                if(movement.IsDashing == true)
                    return AnimationState.DASHING;
                else if (movement.IsGrounded == true)
                    return AnimationState.IDLE_FLOAT;
                else if (rigidbody2D.velocity.y > 0)
                    return AnimationState.START_JUMP;
                else
                    return AnimationState.JUMPING;
            case AnimationState.JUMPING:
                if (movement.IsDashing == true)
                    return AnimationState.DASHING;
                else if (movement.IsGrounded == true)
                    return AnimationState.IDLE_FLOAT;
                else if (rigidbody2D.velocity.y < 0)
                    return AnimationState.FALLING;
                break;
            case AnimationState.FALLING:
                if (movement.IsDashing == true)
                    return AnimationState.DASHING;
                if (movement.IsGrounded == true)
                    return AnimationState.IDLE_FLOAT;
                break;
            case AnimationState.DASHING:
                if (movement.IsDashing == true)
                    return AnimationState.DASHING;
                else if (movement.IsGrounded == true)
                    return AnimationState.IDLE_FLOAT;
                else if (movement.IsGrounded != true)
                    return AnimationState.IDLE_FLOAT;
                break;
            default:
                Debug.LogError(animationState + " not implemented in Determine switch in " + this);
                break;
        }
        return animationState;
    }

    void SetState(AnimationState animationState)
    {
        this.animationState = animationState;
        switch (animationState)
        {
            case AnimationState.IDLE_FLOAT:
                SetAnimationState(AnimationState.IDLE_FLOAT);
                break;

            case AnimationState.START_JUMP:
                SetAnimationState(AnimationState.START_JUMP);
                break;

            case AnimationState.JUMPING:
                SetAnimationState(AnimationState.JUMPING);
                break;

            case AnimationState.DASHING:
                SetAnimationState(AnimationState.DASHING);
                break;
            default:
                Debug.LogError(animationState + " not implemented in Set switch in " + this);
                break;
        }
    }
}