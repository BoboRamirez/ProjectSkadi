using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTrigger : MonoBehaviour
{
    private BoxCollider2D _collider;
    private MobSlime _slime;
    [SerializeField] private MobTriggerType _type;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _slime = GetComponentInParent<MobSlime>();
        if (_collider == null)
            throw new System.Exception("Collider not found");
        if (!_collider.isTrigger)
            throw new System.Exception("Collider Not being a trigger!");
        //kinda ugly here
        switch(_type)
        {
            case MobTriggerType.Attack:
                SetDetectionRange(_slime.AttackRange); break;
            case MobTriggerType.Aggro:
                SetDetectionRange(_slime.AggroRange); break;
        }
    }
    private void SetDetectionRange(float range)
    {
        _collider.size = new Vector2(range, range);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // and here. ugly.
            switch (_type)
            {
                case MobTriggerType.Attack:
                    _slime.IsWithinAttackRange = true;
                    _slime.currentState.OnEnterAttackRange();
                    break;
                case MobTriggerType.Aggro:
                    _slime.IsAggroed = true;
                    _slime.currentState.OnEnterAggroRange();
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // and here. ugly.
            switch (_type)
            {
                case MobTriggerType.Attack:
                    _slime.IsWithinAttackRange = false;
                    _slime.OnExitAttackRange();
                    break;
                case MobTriggerType.Aggro:
                    _slime.IsAggroed = false;
                    _slime.currentState.OnExitAggroRange();
                    break;
            }
        }
    }

}
enum MobTriggerType
{ 
    Aggro,
    Attack
}

