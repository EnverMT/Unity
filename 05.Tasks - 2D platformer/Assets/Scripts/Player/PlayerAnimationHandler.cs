using Platformer.Base;
using UnityEngine;

namespace Platformer.Player
{
    public class PlayerAnimationHandler : BaseAnimationHandler
    {
        protected override void Update()
        {
            base.Update();

            Animator.SetFloat(Params.Jump.VerticalSpeed, BaseUnit.Rigidbody2D.linearVelocity.y);
            Animator.SetBool(Params.Jump.OnGround, BaseUnit.BaseMovement.OnGround);
        }

        protected override float GetAxis()
        {
            return Input.GetAxisRaw(Params.Axis.Horizontal);
        }
    }
}