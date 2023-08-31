using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Grounded : PlayerBaseState
{

    public PlayerState_Grounded(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) {}
    public override void EnterState()
    {
        _context.pMM.Stop();
        _context.pAM.PlayState("Idle");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    /*public override void InputCallback(InputAction.CallbackContext ctx) { }*/

    protected override void CheckSwitchState()
    {
        if (_context.HasFlag(StateFlags.air))
        {
            SwitchState(_factory.GetMidAir());
        }
        else if (_context.HasFlag(StateFlags.crouch))
        {
            SwitchState(_factory.GetCrouch());
        }
        else if ((_context.PlayerFlags & StateFlags.mid) == StateFlags.moveLeft || (_context.PlayerFlags & StateFlags.mid) == StateFlags.moveRight)
        {
            PlayerBaseState newState = _factory.GetMove();
            SetSubState(newState);
            newState.SetSuperState(this);
            SwitchState(newState);
        }
        else if (_context.HasFlag(StateFlags.lightAttack))
        {
            PlayerBaseState newState = _factory.GetSwordCombo();
            SetSubState(newState);
            newState.SetSuperState(this);
            SwitchState(newState);
        }
    }
}
