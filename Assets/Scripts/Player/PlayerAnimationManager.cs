using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static Data;

/// <summary>
/// state here is strictly state of Animation, in contrast with StateMachine of the Player.
/// </summary>
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]private int _currentStateHash;
    [SerializeField]private string _currentStateName;
    // Start is called before the first frame update
    public AnimatorStateInfo AnimatorState { get { return _animator.GetCurrentAnimatorStateInfo(0); } }
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        _currentStateName = "JumpMid";
        _currentStateHash = Animator.StringToHash(_currentStateName);
        _animator.Play(_currentStateName);
    }
    public void PlayState(string stateName)
    {
        int newStateHash = Animator.StringToHash(stateName);
        if (newStateHash == _currentStateHash)
            return;
        _currentStateHash = newStateHash;
        _currentStateName = stateName;
        _animator.Play(stateName);
    }
    public void SetPlaySpeed(float speed = 0)
    {
        if (_animator != null)
            _animator.speed = speed;
    }

    public bool IsAnimationFinished()
    {
         return AnimatorState.normalizedTime >= 1;
    }
    
}
