using System;
using System.Collections;
using UnityEngine;

public class UnitController : BaseController
{
    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            if(_anim != null)
            {
                switch(_state)
                {
                    case Define.State.Idle:
                        _anim.CrossFade("IDLE", 0.1f);
                        break;
                    case Define.State.Move:
                        _anim.CrossFade("MOVE", 0.1f);
                        break;
                    case Define.State.Attack:
                        _anim.CrossFade("ATTACK", 0.1f);
                        break;
                    case Define.State.Rush:
                        _anim.CrossFade("RUSH", 0.1f);
                        break;
                    case Define.State.Hit:
                        _anim.CrossFade("HIT", 0.1f);
                        break;
                    case Define.State.Die:
                        _anim.CrossFade("DIE", 0.1f);
                        break;
                }
            }
        }
    }

    [SerializeField] protected Define.State _state;

    public Define.Unit Type { get; protected set; }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    
    [SerializeField] protected float _moveSpeed;
    protected Animator _anim;
    [SerializeField] protected TowerController _lockTarget;
    Coroutine _coroutine;

    bool _init;

    private void Start()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
            return false;
        tag = "Unit";
        _anim = GetComponent<Animator>();
        _init = true;
        return true;
    }

    protected void ForWait(float duration, Action action)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoForWait(duration, action));
    }

    IEnumerator CoForWait(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action.Invoke();
    }

    public virtual void SetStat(Define.Unit type)
    {
        Data.UnitStatData stat = Managers.Data.UnitStatDatas[type];
        MaxHp = stat.maxHp;
        Hp = stat.hp;
        Attack = stat.attack;
        AttackSpeed = stat.attackSpeed;
        AttackRange = stat.attackRange;
        MoveSpeed = stat.moveSpeed;
    }

    protected virtual void Update()
    {
        switch (State)
        {
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Move:
                UpdateMove();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
            case Define.State.Rush:
                UpdateRush();
                break;
            case Define.State.Hit:
                UpdateHit();
                break;

        }
    }

    protected virtual void UpdateIdle() { }
    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateHit() { }
    protected virtual void UpdateDie() { }
    protected virtual void UpdateRush() { }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //TODO 부딪힐 시 다른 오브젝트의 이동속도 증가
        }
    }
}
