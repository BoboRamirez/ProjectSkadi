using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_DashAttack : PlayerBaseState
{
    public PlayerState_DashAttack(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory)
    {
    }

    public override void EnterState()
    {
        _context.AnimationCallback = null;
        _context.AnimationCallback += FinishDashAttack;
        _context.pMM.Stop();
        _context.pAM.PlayState("Dash_Attack");
    }

    public override void ExitState()
    {
    }

    /*public override void InputCallback(InputAction.CallbackContext ctx)
    {
    }*/

    public override void UpdateState()
    {
    }

    protected override void CheckSwitchState()
    {
    }

    private void FinishDashAttack()
    {
        _context.RemoveFlag(StateFlags.lightAttack);
        SwitchState(_factory.GetGrounded());
    }
}
