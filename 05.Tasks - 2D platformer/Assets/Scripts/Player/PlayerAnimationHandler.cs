using UnityEngine;

//[RequireComponent(typeof(PlayerMover))]
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

        _animator.SetFloat(Params.Jump.VerticalSpeed, _rbody.velocity.y);
        _animator.SetBool(Params.Jump.OnGround, _mover.OnGround);
    }

    protected override float GetAxis()
    {
        return Input.GetAxisRaw(Params.Axis.Horizontal);
    }
}
