using UnityEngine;

public class PlayerAnimationHandler : BaseAnimationHandler
{
    protected override void Update()
    {
        base.Update();

        _animator.SetFloat(Params.Jump.VerticalSpeed, _body.velocity.y);
        _animator.SetBool(Params.Jump.OnGround, _unit.OnGround);
    }

    protected override float GetAxis()
    {
        return Input.GetAxisRaw(Params.Axis.Horizontal);
    }
}
