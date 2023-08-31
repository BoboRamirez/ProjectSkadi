using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Attacks/Light Attack")]
public class AttackComboSO : ScriptableObject
{
    public AnimationClip clip;
    public string animationClipName;
    /// <summary>
    /// length of interuption window before the end of animation. in secs.
    /// </summary>
    public float interuptGap;
    /// <summary>
    ///length of follow up window after the end of animation. in secs.
    /// </summary>
    public float endingTime;

    public Vector2 hitBoxPointA;
    public Vector2 hitBoxPointB;

    public int damage;
}
