using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    private PlayerStateManager _context;
    private Dictionary<string, PlayerBaseState> _states;
    /// <summary>
    /// get context
    /// </summary>
    /// <param name="context"></param>
    public PlayerStateFactory(PlayerStateManager context) 
    { 
        _context = context;
        _states = new Dictionary<string, PlayerBaseState>
        {
            ["grounded"] = new PlayerState_Grounded(_context, this),
            ["midAir"] = new PlayerState_MidAir(_context, this),
            ["move"] = new PlayerState_Move(_context, this),
            ["crouch"] = new PlayerState_Crouch(_context, this),
            ["land"] = new PlayerState_Land(_context, this),
            ["crouchMove"] = new PlayerState_CrouchMove(context, this),
            ["dash"] = new PlayerState_Dash(_context, this),
            ["roll"] = new PlayerState_Roll(_context, this),
            ["swordCombo"] = new PlayerState_LightAttackCombo(_context,this),
            ["dashAttack"] = new PlayerState_DashAttack(context, this),
            ["smash"] = new PlayerState_Smash(_context, this),
        };
    }

    public PlayerBaseState GetGrounded() 
    {
        return _states["grounded"];
    }
    public PlayerBaseState GetMidAir()
    {
        return _states["midAir"];
    }
    public PlayerBaseState GetMove()
    {
        return _states["move"];
    }
    public PlayerBaseState GetCrouch() 
    {
        return _states["crouch"];
    }
    public PlayerBaseState GetLand()
    {
        return _states["land"];
    }
    public PlayerBaseState GetCrouchMove()
    {
        return _states["crouchMove"];
    }
    public PlayerBaseState GetDash()
    {
        return _states["dash"];
    }
    public PlayerBaseState GetRoll()
    {
        return _states["roll"];
    }
    public PlayerBaseState GetSwordCombo()
    {
        return _states["swordCombo"];
    }
    public PlayerBaseState GetDashAttack()
    {
        return _states["dashAttack"];
    }
    public PlayerBaseState GetSmash()
    {
        return _states["smash"];
    }
}
