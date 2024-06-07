using UnityEngine;

public class PlayerAnimationHandler : BaseAnimationHandler
{
    protected override void Update()
    {
        base.Update();

        _animator.SetFloat(AnimatorParams.Jump.VerticalSpeed, _body.velocity.y);
        _animator.SetBool(AnimatorParams.Jump.OnGround, _unit.OnGround);
    }

    protected override float GetAxis()
    {
        return Input.GetAxisRaw(HozirontalAxis);
    }
}
