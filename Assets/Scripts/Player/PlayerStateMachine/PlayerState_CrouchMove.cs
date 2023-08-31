using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_CrouchMove : PlayerBaseState
{
    public PlayerState_CrouchMove(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) {}

    public override void EnterState()
    {
        _context.pAM.PlayState("Crouch_Walk");
    }

    public override void ExitState()
    {
    }
    /*public override void InputCallback(InputAction.CallbackContext ctx) 
    {
        
    }*/

    public override void UpdateState()
    {
        if (_context.HasFlag(StateFlags.mid))
        {
            _context.pMM.Stop();
        }
        else if (_context.HasFlag(StateFlags.moveLeft))
        {
            _context.pMM.CrouchMove(false);
            if (_context.faceRight)
            {
                _context.pMM.FlipSprite();
                _context.faceRight = false;
            }
        }
        else if (_context.HasFlag(StateFlags.moveRight))
        {
            _context.pMM.CrouchMove(true);
            if (!_context.faceRight)
            {
                _context.pMM.FlipSprite();
                _context.faceRight = true;
            }
        }
        else
        {
            _context.pMM.Stop();
        }
        CheckSwitchState();
    }

    protected override void CheckSwitchState()
    {
        if (_context.HasFlag(StateFlags.air))
        {
            SwitchState(_factory.GetMidAir());
        }
        else if (!_context.HasFlag(StateFlags.crouch))
        {
            SwitchState(_factory.GetGrounded());
        }
        else if ((_context.PlayerFlags & StateFlags.mid) == StateFlags.none || _context.HasFlag(StateFlags.mid))
        {
            SwitchState(_factory.GetCrouch());
        }
        else if (_context.HasFlag(StateFlags.dash))
        {
            SwitchState(_factory.GetRoll());
        }
    }
}
