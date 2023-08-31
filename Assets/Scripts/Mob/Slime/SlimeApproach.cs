using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeApproach : MobBaseState
{
    private new MobSlime _context;
    private float _approachSpeed = 1f;

    public SlimeApproach(MobSlime context) : base(context)
    {
        _context = context;
    }

    public override void FrameUpdate()
    {

        bool toRight = _context.playerGO.transform.position.x - _context.transform.position.x > 0;
        if (toRight != _context.FaceRight )
        {
            _context.FaceRight = toRight;
            (_context as IMoveable).FlipSprite();
        }
        int sign = (toRight ? 1 : -1);
        _context.Rb.velocity = sign * _approachSpeed * Vector2.right;
    }

    public override void PhysicsUpdate()
    {

    }

    protected override void CheckSwitchState()
    {
    }

    public override void EnterState()
    {
        _context.Animator.Play("Walk");
    }

    public override void ExitState()
    {

    }

    public override void OnExitAggroRange()
    {
        SwitchState(_context.MobStateList["SlimeWanderState"]);
    }

    public override void OnEnterAttackRange()
    {
        //Debug.Log("Attack from approach");
        SwitchState(_context.MobStateList["SlimeAttackState"]);
    }

    public override void OnExitAttackRange()
    {
        return;
    }

    public override void OnEnterAggroRange()
    {
        return;
    }
}
