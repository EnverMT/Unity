using Platformer.Base;
using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(PlayerMover))]
    public class PlayerAnimationHandler : BaseAnimationHandler
    {
        private PlayerMover _mover;

        protected override void Awake()
        {
            base.Awake();

            _mover = GetComponent<PlayerMover>();
        }

        protected override void Update()
        {
            base.Update();

            Animator.SetFloat(Params.Jump.VerticalSpeed, Rbody.linearVelocity.y);
            Animator.SetBool(Params.Jump.OnGround, _mover.OnGround);
        }

        protected override float GetAxis()
        {
            return Input.GetAxisRaw(Params.Axis.Horizontal);
        }
    }

}