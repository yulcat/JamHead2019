using System;
using UnityEngine;

namespace ObjectScript
{
    public class CharacterAnimationStateBehaviour : StateMachineBehaviour
    {
        [Flags]
        public enum AnimationType
        {
            IsMoving = 1 << 0,
            CanJump = 1 << 1,
            CanThrow = 1 << 2,
            IsJump = 1 << 3
        }

        [HideInInspector] public CharacterAnimation CharacterAnimation;
        public AnimationType Animation;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            CharacterAnimation.IsMoving = (Animation & AnimationType.IsMoving) != 0;
            CharacterAnimation.CanThrow = (Animation & AnimationType.CanThrow) != 0;
            CharacterAnimation.CanJump = (Animation & AnimationType.CanJump) != 0;
            if ((Animation & AnimationType.IsJump) != 0) CharacterAnimation.InvokeJump();
        }

//        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,
//            int layerIndex)
//        {
//        }

//        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo,
//            int layerIndex)
//        {
//        }

//        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo,
//            int layerIndex)
//        {
//        }

//        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo,
//            int layerIndex)
//        {
//        }
    }
}