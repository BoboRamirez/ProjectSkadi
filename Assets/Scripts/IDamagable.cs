using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public void TakeDamage(int incomingDamage);
    public void Die();
}
