using System;
using System.Collections;
using System.Collections.Generic;

public static class Data
{
    public const int MaxJumpCount = 5;
    public const float GenaralComboGapTime = 0.1f;
}
[Flags]
public enum StateFlags
{
    none = 0b_0000_0000,
    air = 0b_0000_0001,
    crouch = 0b_0000_0010,
    smash = air | crouch,
    moveLeft = 0b_0000_0100,
    moveRight = 0b_0000_1000,
    mid = moveLeft | moveRight,
    dash = 0b_0001_0000,
    lightAttack = 0b_0010_0000,
    heavyAttack = 0b_0100_0000,
}