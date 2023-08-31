using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public Rigidbody2D Rb { get; }
    public bool FaceRight { get; set; }
    public virtual void FlipSprite()
    {
        Rb.transform.localScale = new Vector2(-Rb.transform.localScale.x, Rb.transform.localScale.y);
    }
}
