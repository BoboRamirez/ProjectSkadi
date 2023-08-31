using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Data;

public class PlayerState_MidAir : PlayerBaseState
{
    private const float velThresh = 0.05f;
    private int _jumpCounter;
    private bool _isActive;
    public PlayerState_MidAir(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) 
    {
        _isActive = false;
        _jumpCounter = -1;
        _context.playerInput.actions["jump"].started += OnJump;

    }
    public override void EnterState()
    {
        //Debug.Log("enter jump!");
        _jumpCounter = MaxJumpCount;
        _isActive = true;
    }

    public override void ExitState()
    {
        _isActive = false;
        _jumpCounter = -1;
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
            _context.pMM.Stop();
        }

        if (_context.pMM.YVelocity > velThresh)
        {
            _context.pAM.PlayState("JumpUp");
        }
        else if ( _context.pMM.YVelocity < -velThresh) 
        {
            _context.pAM.PlayState("JumpDown");
        }
        else
        {
            _context.pAM.PlayState("JumpMid");
        }

        CheckSwitchState();
    }

    protected override void CheckSwitchState()
    {
        if (!_context.HasFlag(StateFlags.air)) 
        {
            SwitchState(_factory.GetLand());
        }
        else if (_context.HasFlag(StateFlags.smash))
        {
            SwitchState(_factory.GetSmash());
        }
        else if (_context.HasFlag(StateFlags.dash))
        {
            PlayerBaseState newState = _factory.GetDash();
            SetSubState(newState);
            newState.SetSuperState(this);
            SwitchState(newState);
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_jumpCounter > 0 && _isActive && ctx.phase == InputActionPhase.Started)
        {
            _context.pMM.Jump();
            _jumpCounter--;
        }
    }
    
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (_context.CurrentState == _factory.GetCrouch() || _context.CurrentState == _factory.GetCrouchMove()
            || _context.CurrentState == _factory.GetGrounded() || _context.CurrentState == _factory.GetMove())
        {
            _context.AddFlag(StateFlags.air);
            _context.StartCoroutine(JumpHandler(ctx));
        }
        else if (_context.CurrentState == _factory.GetMidAir())
        {
            _context.pMM.FreezeVertical();
            Jump(ctx);
        }
        //will remove the airflag when player hit the ground
    }
    private IEnumerator JumpHandler(InputAction.CallbackContext ctx)
    {
        while (_context.CurrentState != _factory.GetMidAir())
        {
            //frame needed is unstable. need further attention
            //Debug.Log(_debugCounter++);
            yield return null;
        }
        Jump(ctx);
        yield break;
    }
}
