using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Data;
/// <summary>
/// AKA Context
/// </summary>
public class PlayerStateManager : MonoBehaviour, IDamagable
{
    //components & instances
    private PlayerBaseState _currentState;
    private PlayerStateFactory _factory;
    public PlayerInput playerInput;
    public PlayerMovementManager pMM;
    public PlayerAnimationManager pAM;
    //private Rigidbody2D _rb;

    //parameters
    [SerializeField] private StateFlags _playerFlags;
    [SerializeField] private float _dashDuration;
    [SerializeField] private List<AttackComboSO> _comboList;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;

    //public event Action MovementUpdate = null;
    public bool faceRight;

    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public StateFlags PlayerFlags { get { return _playerFlags;} }
    public List<AttackComboSO> ComboList { get { return _comboList; } }
    public float DashDuration { get { return _dashDuration;} }

    
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }

    //private int _debugCounter = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        pMM = GetComponent<PlayerMovementManager>();
        pAM = GetComponent<PlayerAnimationManager>();
        _playerFlags = StateFlags.air;
        faceRight = true;
        _factory = new PlayerStateFactory(this);
        _currentState = _factory.GetMidAir();
        _currentState.EnterState();
        //_rb = GetComponent<Rigidbody2D>();
    }

    //Is this actually ok????
    private void FixedUpdate()
    {
        _currentState?.UpdateState();
    }

    private void OnEnable()
    {
        playerInput.actions.actionMaps[0].Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.actionMaps[0].Disable();
    }

    //Flag operations
    public void AddFlag(StateFlags newFlag)
    {
        _playerFlags |= newFlag;
    }
    public void RemoveFlag(StateFlags newFlag)
    {
        _playerFlags &= ~newFlag;
    }
    public bool HasFlag(StateFlags flags)
    {
        return (_playerFlags & flags) == flags;
    }

    //Input callback functions

    public Action AnimationCallback;
    public void OnAnimationEvent()
    {
        AnimationCallback?.Invoke();
    }


    public Action LandingCallback = null;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            RemoveFlag(StateFlags.air);
            LandingCallback?.Invoke();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            AddFlag(StateFlags.air);
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        _currentHealth -= incomingDamage;
        if (_currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
            _currentState = null;
        }
        pAM.PlayState("Die");
    }
}
