using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MobBaseState
{
    private new MobSlime _context;
    private Collider2D _hitTarget;
    private IDamagable _target;
    public SlimeAttack(MobSlime context) : base(context)
    {
        _context = context;
        
    }

    public override void FrameUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    protected override void CheckSwitchState()
    {
    }

    public override void EnterState()
    {
        _context.AnimationCallback = null;
        _context.AnimationCallback += AttackDetection;
        _context.Animator.Play("Attack");
    }


    public override void ExitState()
    {
        _context.AnimationCallback = null;
    }

    public override void OnEnterAttackRange()
    {
    }

    public override void OnExitAttackRange()
    {
        SwitchState(_context.MobStateList["SlimeApproachState"]);
    }

    public override void OnEnterAggroRange()
    {
    }

    public override void OnExitAggroRange()
    {
    }

    private void AttackDetection()
    {
        _hitTarget  = Physics2D.OverlapCircle(_context.transform.position, 4f, LayerMask.GetMask("Player"));
        Debug.Log(_hitTarget);
        if (_hitTarget == null) return;
        _target = _hitTarget.gameObject.GetComponent<PlayerStateManager>() as IDamagable;
        if (_target == null) return;
        _target.TakeDamage(_context.Damage);
    }
}
