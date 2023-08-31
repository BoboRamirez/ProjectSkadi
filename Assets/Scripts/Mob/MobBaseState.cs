using UnityEngine;

public abstract class MobBaseState
{
    protected Mob _context;
    protected GameObject _mob;
    protected GameObject _player;

    public MobBaseState(Mob context) 
    {
        _context = context;
        _mob = context.gameObject;
        _player = context.playerGO;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    protected void SwitchState(MobBaseState newState)
    {
        ExitState();
        _context.currentState = newState;
        newState.EnterState();
    }
    public abstract void FrameUpdate();
    public abstract void PhysicsUpdate();
    protected abstract void CheckSwitchState();
    
    public abstract void OnEnterAttackRange();
    public abstract void OnExitAttackRange();
    public abstract void OnEnterAggroRange();
    public abstract void OnExitAggroRange();
  
}
