using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerState_Smash : PlayerBaseState
{
    private bool _timeToFinishThisVergil;
    public PlayerState_Smash(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory)
    {

    }

    public override void EnterState()
    {
        _context.pAM.PlayState("Smash");
        _context.AnimationCallback = null;
        _context.AnimationCallback += AnimationCallback;
        _context.LandingCallback = null;
        _context.LandingCallback += ResumeAnimation;
        _context.pMM.Freeze();
        _context.pMM.AddSmashForce();
        _timeToFinishThisVergil = false;
    }

    public override void ExitState()
    {
        _context.AnimationCallback = null;
        _context.LandingCallback = null;
    }
    public override void UpdateState()
    {
    }

    protected override void CheckSwitchState()
    {
    }
    //still not good when smashing from low height.
    private void AnimationCallback()
    {
        if (_timeToFinishThisVergil)
        {
            SwitchState(_factory.GetGrounded());
            return;
        }
        if (_context.HasFlag(StateFlags.air))
            _context.pAM.SetPlaySpeed(0);
        _timeToFinishThisVergil = true;
    }
    private void ResumeAnimation()
    {
        _context.pAM.SetPlaySpeed(1);
    }
}
