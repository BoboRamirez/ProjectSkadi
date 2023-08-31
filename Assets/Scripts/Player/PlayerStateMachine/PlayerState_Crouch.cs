using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Crouch : PlayerBaseState
{
    public PlayerState_Crouch(PlayerStateManager _context, PlayerStateFactory _factory): base(_context, _factory)    
    {
        _context.playerInput.actions["crouchDown"].started += OnCrouch;
        _context.playerInput.actions["crouchDown"].canceled += OnCrouch;
    }

    public override void EnterState()
    {
        _context.pAM.PlayState("Crouch");
        _context.pMM.Stop();
        
    }

    public override void ExitState()    {    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    protected override void CheckSwitchState()
    {
        if (_context.PlayerFlags.HasFlag(StateFlags.air))
        {
            SwitchState(_factory.GetMidAir());
        }
        else if (!_context.PlayerFlags.HasFlag(StateFlags.crouch))
        {
            SwitchState(_factory.GetGrounded());
        }
        
        else if ((_context.PlayerFlags & StateFlags.mid) != StateFlags.none && !_context.HasFlag(StateFlags.mid))
        {
            SwitchState(_factory.GetCrouchMove());
        }
    }
    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            _context.AddFlag(StateFlags.crouch);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            _context.RemoveFlag(StateFlags.crouch);
        }
    }
}
