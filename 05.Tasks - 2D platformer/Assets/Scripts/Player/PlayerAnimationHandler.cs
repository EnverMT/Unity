using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PlayerAnimationHandler : BaseAnimationHandler
{
    private Mover _mover;

    protected override void Awake()
    {
        base.Awake();

        _mover = GetComponent<Mover>();
    }

    protected override void Update()
    {
        base.Update();

        _animator.SetFloat(Params.Jump.VerticalSpeed, _rbody.velocity.y);
        _animator.SetBool(Params.Jump.OnGround, _mover.OnGround);
    }

    protected override float GetAxis()
    {
        return Input.GetAxisRaw(Params.Axis.Horizontal);
    }
}
