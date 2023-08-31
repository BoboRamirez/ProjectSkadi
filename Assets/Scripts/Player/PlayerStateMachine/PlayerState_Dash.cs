using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Dash : PlayerBaseState
{

    public PlayerState_Dash(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) 
    {
        _context.playerInput.actions["dash"].started += OnDash;
    }

    public override void EnterState()
    {
        if (_superState == _factory.GetMidAir() || _superState == _factory.GetMove()) 
        {
            _context.pAM.PlayState("Dash");
            _context.pMM.Dash(_context.faceRight);
        }
        else if (_superState == _factory.GetCrouch())
        {
            _context.pAM.PlayState("Roll");
            _context.pMM.Dash(_context.faceRight);
        }
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
   /* public override void InputCallback(InputAction.CallbackContext ctx) { }*/

    protected override void CheckSwitchState()
    {
        if (_context.HasFlag(StateFlags.lightAttack))
        {
            SwitchState(_factory.GetDashAttack());
        }
        else if (!_context.HasFlag(StateFlags.dash))
        {
            if (_superState != null)
            {
                SwitchState(_superState);
                _superState.SetSubState(null);
                _superState = null;
            }
        }
        
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started && !_context.HasFlag(StateFlags.dash))
        {
            _context.AddFlag(StateFlags.dash);
            if (_context.HasFlag(StateFlags.dash) && (_context.HasFlag(StateFlags.air) || !_context.HasFlag(StateFlags.crouch)))
                _context.StartCoroutine(DashCountDown());
        }
    }
    private IEnumerator DashCountDown()
    {
        yield return new WaitForSeconds(_context.DashDuration);
        _context.RemoveFlag(StateFlags.dash);
    }
}
