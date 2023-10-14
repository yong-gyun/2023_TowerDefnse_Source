using System;
using System.Collections;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Define.WorldObject WorldObjectType { get; protected set; }
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }

    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            //TODO 피격시 HP가 닳는 함수
        }
    }

    public float Attack { get { return _attack; } set { _attack = value; } }
    public float AttackRange { get { return _attackRange * Define.TILE_SIZE; } set { _attackRange = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _hp;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _currentAttackTime;
    protected bool _isAttackable;

    protected virtual void OnDead()
    {
        Managers.Object.Despawn(gameObject);
    }

    public virtual void OnDamaged(BaseController bc)
    {
        if (Hp <= 0f)
            return;

        Hp = bc.Attack;

        if (Hp <= 0f)
        {
            Hp = 0f;
            OnDead();
        }
    }

    Coroutine _coroutine;

    protected void ForWait(float duration, Action action, bool duplicate = false)
    {
        if(duplicate)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CoForWait(duration, action));
        }
        else
        {
            StartCoroutine(CoForWait(duration, action));
        }
    }

    IEnumerator CoForWait(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action.Invoke();
    }
}
