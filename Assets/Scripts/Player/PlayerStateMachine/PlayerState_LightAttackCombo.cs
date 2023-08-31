using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static Data;

public class PlayerState_LightAttackCombo : PlayerBaseState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _lastAttackFinishTime;
    private Coroutine _finishComboCoroutine = null;
    private bool _isActive = false;
    private Collider2D[] _hitTargets = new Collider2D[10];
    private ContactFilter2D _contactFilter;
    private int _hitCount;
    private IDamagable _targetMob;
    private Vector2 _pointA, _pointB;

    public PlayerState_LightAttackCombo(PlayerStateManager context, PlayerStateFactory factory) : base(context, factory) 
    {
        _lastAttackFinishTime = -1f;
        _lastAttackTime = -1f;
        _context.playerInput.actions["lightAttack"].started += OnLightAttack;
        _contactFilter = new ContactFilter2D();
        _contactFilter.layerMask = LayerMask.GetMask("Mob");
        _contactFilter.useLayerMask = true;
    }
    public override void EnterState()
    {
        _context.AnimationCallback = null;
        _context.AnimationCallback += AttackDetection; 

        //_context.AnimationCallback += () => _context.pMM.AttackMove(_context.faceRight); 
        _comboCounter = -1;
        _isActive = true;
        Attack();
    }

    public override void ExitState()    
    {
        _context.AnimationCallback = null;
        _context.StopCoroutine(FinishCombo());
        _context.RemoveFlag(StateFlags.lightAttack);
    }

    public override void UpdateState()    {    }

    protected override void CheckSwitchState()    {    }

    private void Attack()
    {
        FlipDuringAttack();
        _comboCounter++;
        _comboCounter %= _context.ComboList.Count;
        _lastAttackTime = Time.time;
        _lastAttackFinishTime = _lastAttackTime + _context.pAM.AnimatorState.length;
        if (_finishComboCoroutine != null)
            _context.StopCoroutine(_finishComboCoroutine);
        _context.pAM.PlayState(_context.ComboList[_comboCounter].animationClipName);
        
        _finishComboCoroutine = _context.StartCoroutine(FinishCombo());
    }

    private IEnumerator FinishCombo()
    {
        yield return new WaitForSeconds(_context.pAM.AnimatorState.length + GenaralComboGapTime * 8f);
        PlayerBaseState newState = _superState;
        SetSuperState(null);
        newState.SetSubState(null);
        SwitchState(newState);
        yield break;
    }

    private bool IsWithinInputWindow (int comboCounter)
    {
        float interuptTime = _context.ComboList[comboCounter].interuptGap;
        float endingTime = _context.ComboList[comboCounter].endingTime;
        return (Time.time > _lastAttackFinishTime - interuptTime && Time.time < _lastAttackFinishTime + endingTime);
    }
    
    private void FlipDuringAttack ()
    {
        if (_context.HasFlag(StateFlags.mid)) return;
        if (_context.HasFlag(StateFlags.moveLeft) && _context.faceRight)
        {
            _context.pMM.FlipSprite();
            _context.faceRight = false;
        }
        else if (_context.HasFlag(StateFlags.moveRight) && !_context.faceRight)
        {
            _context.pMM.FlipSprite();
            _context.faceRight = true;
        }
    }
    private void OnLightAttack(InputAction.CallbackContext ctx)
    {
        if (!_context.HasFlag(StateFlags.lightAttack))
            _context.AddFlag(StateFlags.lightAttack);
        //Debug.Log(_comboCounter);
        if (_isActive && IsWithinInputWindow(_comboCounter))
        {
            Attack();
        }
    }

    private void AttackDetection()
    {
        //if (_comboCounter == 2)
        _context.pMM.AttackMove(_context.faceRight);
        _pointA = (Vector2)_context.transform.position + _context.ComboList[_comboCounter].hitBoxPointA;
        _pointB = (Vector2)_context.transform.position + _context.ComboList[_comboCounter].hitBoxPointB;
        _hitCount = Physics2D.OverlapArea(_pointA, _pointB, _contactFilter, _hitTargets);
        if (_hitCount <= 0)
            return;
        foreach(Collider2D hitTarget in _hitTargets)
        {
            if (hitTarget == null) 
                continue;
            _targetMob =  hitTarget.gameObject.GetComponent<MobSlime>() as IDamagable;
            if (_targetMob == null)
                continue;
            _targetMob.TakeDamage(_context.ComboList[_comboCounter].damage);
            //Debug.Log(_targetMob.ToString() + "get Hit!");
        }
    }
}
