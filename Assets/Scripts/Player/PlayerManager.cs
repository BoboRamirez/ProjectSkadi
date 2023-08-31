using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;
using UnityEngine.InputSystem;
using UnityEditor.Tilemaps;
/// <summary>
/// Handles input and logic process
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private StateFlags playerState;
    private bool faceRight;
    private PlayerMovementManager mm;
    private PlayerAnimationManager am;
    [SerializeField] int jumpCounter;
    public StateFlags PlayerState
    {
        get { return playerState; }
    }

    // Start is called before the first frame update
    void Start()
    {
        faceRight = true;
        mm = gameObject.GetComponent<PlayerMovementManager>();
        am = gameObject.GetComponent<PlayerAnimationManager>();
        playerState = StateFlags.air;
        jumpCounter = MaxJumpCount;
    }

    // UpdateState is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// update player state according to input - move left
    /// </summary>
    /// <param name="ctx">InputAction.CallbackContext</param>
    public void HandleMoveLeft(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                playerState |= StateFlags.moveLeft;
                if (faceRight)
                {
                    mm.FlipSprite();
                    faceRight = false;
                }
                break;
            case InputActionPhase.Canceled:
                playerState &= ~StateFlags.moveLeft;
                break;
        }
        //Debug.Log($"left: {playerState}");
    }

    /// <summary>
    /// update player state according to input - move right
    /// </summary>
    /// <param name="ctx">InputAction.CallbackContext</param>
    public void HandleMoveRight(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                playerState |= StateFlags.moveRight;
                if (!faceRight)
                {
                    mm.FlipSprite();
                    faceRight = true;
                }
                break;
            case InputActionPhase.Canceled:
                playerState &= ~StateFlags.moveRight;
                break;
        }
        //Debug.Log($"right: {playerState}");
    }

    public void HandleJump(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started && jumpCounter > 0)
        {
            //playerState is automatically updated 
            mm.Jump();
            jumpCounter--;
        }
    }


}
