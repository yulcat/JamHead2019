using System;
using ObjectScript;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [NonSerialized] public bool IsMoving;
    [NonSerialized] public bool CanJump;
    [NonSerialized] public bool CanThrow;
    public Action OnThrow;
    public Action OnJump;

    private void Start()
    {
        var behaviours = Animator.GetBehaviours<CharacterAnimationStateBehaviour>();
        foreach (var behaviour in behaviours)
        {
            behaviour.CharacterAnimation = this;
        }
    }

    public bool TryMove(float input)
    {
        Animator.SetBool("Move", input != 0);
        return IsMoving;
    }

    public void TryJump()
    {
        if (CanJump) Animator.SetTrigger("Jump");
    }

    public void TryThrow()
    {
        if (CanThrow) Animator.SetTrigger("Throw");
    }

    public void SetGround(bool onGround) => Animator.SetBool("OnGround", onGround);

    public void ThrowHead() => OnThrow?.Invoke();

    public void InvokeJump() => OnJump?.Invoke();

    public void PickUp()
    {
        Animator.SetTrigger("PickUp");
    }
}