using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    private protected PlayerStateManager _context;
    private protected PlayerStateFactory _factory;
    private protected PlayerBaseState _subState;
    private protected PlayerBaseState _superState;
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    protected abstract void CheckSwitchState();
    //public abstract void InputCallback(InputAction.CallbackContext ctx);
    public PlayerBaseState(PlayerStateManager context, PlayerStateFactory factory)
    {
        _context = context;
        _factory = factory;
        _subState = null;
        _superState = null;
    }
    public void SetSubState(PlayerBaseState newState) 
    {
        _subState = newState;
    }
    public void SetSuperState(PlayerBaseState newState) 
    {
        _superState = newState;
    }
    public virtual void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        _context.CurrentState = newState;
        newState.EnterState();
    }
}

