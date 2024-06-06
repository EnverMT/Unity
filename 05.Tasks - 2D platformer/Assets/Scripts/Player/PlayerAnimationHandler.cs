using UnityEngine;

public class PlayerAnimationHandler : BaseAnimationHandler
{
    private const string ParamVerticalSpeed = "VerticalSpeed";
    private const string ParamOnGround = "OnGround";

    protected override void Update()
    {
        base.Update();

        _animator.SetFloat(ParamVerticalSpeed, _body.velocity.y);
        _animator.SetBool(ParamOnGround, _unit.OnGround);
    }

    protected override float GetAxis()
    {
        return Input.GetAxisRaw(HozirontalAxis);
    }
}
