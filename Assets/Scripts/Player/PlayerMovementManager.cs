using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField]
    private float _moveForce, _stopFoceCoeficient, _maxMoveSpeed, _jumpForce, _crouchMoveSpeed, _dashForce, _smashForce;
    private float _newX;
    private ConstantForce2D _cForce;
    private Rigidbody2D _rb;
    private PlayerStateManager _context;
    
    public float YVelocity
    {
        get { return _rb.velocity.y; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _cForce = GetComponent<ConstantForce2D>();
        _rb = GetComponent<Rigidbody2D>();
        _context = GetComponent<PlayerStateManager>();
    }

    // UpdateState is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //y-axis movement
        if (math.abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _newX = _rb.velocity.x > 0 ? _maxMoveSpeed : -_maxMoveSpeed;
            _rb.velocity = new Vector2(_newX, _rb.velocity.y);
        }
        //jump aniamtion dealt in collision
    }

    public void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
    }

    public void FlipSprite()
    {
        Vector2 s = this.transform.localScale;
        this.transform.localScale = new Vector2(s.x * -1, s.y);
    }

    public void AddMovementForce(bool isLeft)
    {
        int sign = isLeft ? 1 : -1;
        _cForce.force = _moveForce * sign * Vector2.left;
    }
    
    public void AddSmashForce()
    {
        _rb.AddForce(_smashForce * Vector2.down, ForceMode2D.Impulse);
    }

    public void StopAtGround()
    {
        _cForce.force = Vector2.zero;
        _rb.AddForce(_stopFoceCoeficient * -1 * _rb.velocity);
        //_rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    public void Stop()
    {
        _cForce.force = Vector2.zero;
        //_rb.AddMovementForce(-1 * _rb.velocity * _stopFoceCoeficient);
        //_rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    public void Freeze()
    {
        _rb.velocity = Vector2.zero;
    }

    public void FreezeVertical()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
    
    public void CrouchMove(bool toRight)
    {
        int sign = toRight ? 1 : -1;
        Stop();
        _rb.velocity = new Vector2(sign * _crouchMoveSpeed, _rb.velocity.y);
    }
    /*private IEnumerator DashC(bool toRight)
    {
        int sign = toRight ? 1 : -1;
        _rb.AddMovementForce(_dashForce * sign * Vector2.right);
        yield return null;
        
    }*/
    public void Dash (bool toRight)
    {
        int sign = toRight ? 1 : -1;
        _rb.AddForce(_dashForce * sign * Vector2.right, ForceMode2D.Impulse);
    }
    /// <summary>
    /// Hardcoded parameter, ugly!!!!!!!!!!!!!!
    /// </summary>
    /// <param name="toRight">is chara face right?</param>
    public void AttackMove(bool toRight)
    {
        int sign = toRight ? 1 : -1;
        _rb.AddForce(8 *  sign * Vector2.right,ForceMode2D.Impulse);
    }
}
