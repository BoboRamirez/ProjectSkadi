using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Move : PlayerBaseState
{
    public PlayerState_Move(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) 
    {
        context.playerInput.actions["moveLeft"].started += OnMoveLeft;
        context.playerInput.actions["moveLeft"].canceled += OnMoveLeft;
        context.playerInput.actions["moveRight"].started += OnMoveRight;
        context.playerInput.actions["moveRight"].canceled += OnMoveRight;
    }
    public override void EnterState()
    {
        _context.pAM.PlayState("Run");
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
            _context.pMM.AddMovementForce(true);
            if (_context.faceRight)
            {
                _context.pMM.FlipSprite();
                _context.faceRight = false;
            }
        }
        else if (_context.HasFlag(StateFlags.moveRight))
        {
            _context.pMM.AddMovementForce(false);
            if (!_context.faceRight)
            {
                _context.pMM.FlipSprite();
                _context.faceRight = true;
            }
        }
        else
        {
            _context.pMM.StopAtGround();
        }
        CheckSwitchState();
    }

    protected override void CheckSwitchState()
    {
        if (_context.HasFlag(StateFlags.air))
        {
            SwitchState(_factory.GetMidAir());
        }
        else if (_context.HasFlag(StateFlags.crouch))
        {
            SwitchState(_factory.GetCrouchMove());
        }
        else if ((_context.PlayerFlags & StateFlags.mid) == StateFlags.none || _context.HasFlag(StateFlags.mid))
        {
            PlayerBaseState newState = _factory.GetGrounded();
            SetSuperState(null);
            newState.SetSubState(null);
            SwitchState(newState);
        }
        else if (_context.HasFlag(StateFlags.dash))
        {
            PlayerBaseState newState = _factory.GetDash();
            SetSubState(newState);
            newState.SetSuperState(this);
            SwitchState(newState);
        }
        else if (_context.HasFlag(StateFlags.lightAttack))
        {
            _context.pMM.Stop();
            PlayerBaseState newState = _factory.GetSwordCombo();
            SetSubState(newState);
            newState.SetSuperState(this);
            SwitchState(newState);
        }
        
    }

    private void OnMoveLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            _context.AddFlag(StateFlags.moveLeft);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            _context.RemoveFlag(StateFlags.moveLeft);
        }
    }
    private void OnMoveRight(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            _context.AddFlag(StateFlags.moveRight);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            _context.RemoveFlag(StateFlags.moveRight);
        }
    }
}
