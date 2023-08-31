using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Land : PlayerBaseState
{
    public PlayerState_Land(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) { }

    public override void EnterState()
    {
        _context.pAM.PlayState("Land");
        _context.AnimationCallback = null;
        _context.AnimationCallback += OnFinishLanding;
    }

    public override void ExitState()
    {
        _context.AnimationCallback = null;
    }

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
    }
   /* public override void InputCallback(InputAction.CallbackContext ctx) { }*/

    protected override void CheckSwitchState()
    {
        //will switch to Grounded after finishing animation
    }

    //called during animation Land
    private void OnFinishLanding()
    {
        if (_context.HasFlag(StateFlags.crouch))
        {
            SwitchState(_factory.GetCrouch());
            return;
        }
        //kinda ugly here
        _context.AnimationCallback = null;
        _context.AnimationCallback += () => SwitchState(_factory.GetGrounded());
    }

}
