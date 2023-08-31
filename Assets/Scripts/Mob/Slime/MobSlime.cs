using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AKA mob context for slime
/// </summary>
public class MobSlime : Mob, IDamagable, IAggressive, IMoveable
{
    [SerializeField] protected int _maxHealth;
    [SerializeField] protected int _currentHealth;
    [SerializeField]private float _attackRange;
    [SerializeField]private float _aggroRange;
    private Rigidbody2D _rb;
    private Animator _animator;
    public Action AnimationCallback = null;

    private int _damage;
    private bool _isAggroed;
    private bool _isWithinAttackRange;
    private bool _faceRight;
    private SpriteRenderer _spriteRenderer;
    public int MaxHealth { get => _maxHealth; set { } }
    public int CurrentHealth { get => _currentHealth; set { } }
    public float AttackRange { get => _attackRange; set { } }
    public float AggroRange { get => _aggroRange; set { } }
    public bool IsWithinAttackRange { get => _isWithinAttackRange; set => _isWithinAttackRange = value; }
    public bool IsAggroed { get => _isAggroed; set => _isAggroed = value; }
    public Animator Animator { get => _animator; }
    public bool FaceRight { get => _faceRight; set => _faceRight = value; }
    public Rigidbody2D Rb { get => _rb; }
    public int Damage { get => _damage; set => _damage = value; }

    public Dictionary<string, MobBaseState> MobStateList = new();
    private void Awake()
    {
        _damage = 20;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHealth = _maxHealth;
        IsAggroed = false;
        IsWithinAttackRange = false;
        _faceRight = false;
        MobStateList.Add("SlimeWanderState", new SlimeWander(this));
        MobStateList.Add("SlimeAttackState", new SlimeAttack(this));
        MobStateList.Add("SlimeApproachState", new SlimeApproach(this));
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        currentState = MobStateList["SlimeWanderState"];
        currentState.EnterState();
    }
    private void Start()
    {
        
    }

    public void Die()
    {
        currentState.ExitState();
        currentState = null;
        _animator.Play("IdleBlinkEye");
        StartCoroutine(DieOut());
    }

    private IEnumerator DieOut()
    {
        while (_spriteRenderer.color.a > 0)
        {
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - 0.05f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int incomingDamage)
    {
        _currentHealth -= incomingDamage;
        if (_currentHealth <= 0)
            Die();
    }

    private void OnDrawGizmosSelected()
    {
        /*Gizmos.DrawWireCube(transform.position, new Vector3(_aggroRange, _aggroRange, _aggroRange));
        Gizmos.DrawWireCube(transform.position, new Vector3(_attackRange, _attackRange, _attackRange));*/
    }



    public void OnEnterAttackRange()
    {
        currentState.OnEnterAttackRange();
    }

    public void OnExitAttackRange()
    {
        currentState.OnExitAttackRange();
    }

    public void OnEnterAggroRange()
    {
        currentState.OnEnterAggroRange();
    }

    public void OnExitAggroRange()
    {
        currentState.OnExitAggroRange();
    }

    public void SlimeAnimationCallback()
    {
        AnimationCallback?.Invoke();
    }

}
