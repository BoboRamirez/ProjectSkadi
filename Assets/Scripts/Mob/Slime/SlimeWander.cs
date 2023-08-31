using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SlimeWander : MobBaseState
{
    private new MobSlime _context;
    private float _wanderSpeed = 100f;
    private int _dice;
    public SlimeWander(MobSlime context) : base(context)
    { 
        _context = context;
    }


    public override void EnterState()
    {
        _context.AnimationCallback = null;
        _context.AnimationCallback += RollAction;
    }

    public override void ExitState()
    {
        _context.AnimationCallback = null; ;
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

    public override void OnEnterAggroRange()
    {

        SwitchState(_context.MobStateList["SlimeApproachState"]);
    }

    public override void OnEnterAttackRange()
    {
        Debug.Log("Attack from Wander");
        //SwitchState(_context.MobStateList["SlimeAttackState"]);
    }

    public override void OnExitAttackRange()
    {
        return;
    }

    public override void OnExitAggroRange()
    {
        return;
    }

    private void RollAction()
    {
        _dice = Random.Range(0, 3);
        //_dice = 2;
        //Debug.Log(_dice);
        switch (_dice)
        {
            case 0:
                //idle
                Idle();
                break;
            case 2:
                //wander left
                Wander(false);
                break;
            case 1:
                //wander right
                Wander(true);
                break;

        }
    }

    private void Idle()
    {
        _context.Rb.velocity = Vector2.zero;
        if (Random.value > 0.3f)
            _context.Animator.Play("IdleSquish");
        else
            _context.Animator.Play("IdleBlinkEye");
    }
    private void Wander(bool isRight)
    {
        int sign;
        sign = isRight? 1 : -1;
        if (_context.FaceRight != isRight)
        {
            (_context as IMoveable).FlipSprite();
            _context.FaceRight = isRight;
        }
        _context.Rb.AddForce(sign * _wanderSpeed * Vector2.right, ForceMode2D.Force);
        _context.Animator.Play("Walk");
    }


}
