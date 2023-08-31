using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Roll : PlayerBaseState
{
    public PlayerState_Roll(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory)    {    }
    /*public override void InputCallback(InputAction.CallbackContext ctx)    {    }*/

    public override void EnterState()
    {
        _context.pAM.PlayState("Roll");
        _context.pMM.Dash(_context.faceRight);
        _context.AnimationCallback = null;
        _context.AnimationCallback += OnFinishRolling;
    }

    public override void ExitState()
    {
        _context.AnimationCallback = null;
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    protected override void CheckSwitchState()
    {
        if (!_context.HasFlag(StateFlags.dash))
        {
            SwitchState(_factory.GetCrouchMove());
        }
    }
    //called during animation Roll
    private void OnFinishRolling()
    {
        _context.RemoveFlag(StateFlags.dash);
    }
}
