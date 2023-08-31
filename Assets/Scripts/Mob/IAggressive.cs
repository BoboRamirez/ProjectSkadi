public interface IAggressive
{
    public int Damage { get; set; }
    public bool IsWithinAttackRange { get; set; }
    public bool IsAggroed { get; set; }
    public float AttackRange { get; set;  }
    public float AggroRange { get; set; }
    public void OnEnterAttackRange();
    public void OnExitAttackRange();
    public void OnEnterAggroRange();
    public void OnExitAggroRange();
}
