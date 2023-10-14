using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : UnitController
{
    public int RewardGold { get { return _rewardGold; } set { _rewardGold = value; } }
    public float AttackInterval 
    { 
        get 
        { 
            if(_lockTarget != null)
                return AttackRange + _lockTarget.Size;
            return AttackRange;
        } 
    }

    public int RewardScore { get { return _rewardScore; } set { _rewardScore = value; } }

    [SerializeField] protected int _rewardGold;
    [SerializeField] protected int _rewardScore;
    protected NavMeshAgent _agent;
    public Define.Priority Priority;
    protected Transform _firePos;
    protected int _layer;
    protected static int _priority;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        _firePos = gameObject.FindChild<Transform>("FirePos", true);
        _agent = GetComponent<NavMeshAgent>();
        _layer = LayerMask.GetMask("Tower");
        _agent.avoidancePriority = _priority;
        return true;
    }

    protected override void Update()
    {
        switch (Priority)
        {
            case Define.Priority.Illusion:
                {
                    if(Managers.Object.IllusionTowers.Count == 0)
                    {
                        Priority = Define.Priority.ProtectedTower;
                        return;
                    }

                    _lockTarget = Util.GetShortestDistance(gameObject, Managers.Object.IllusionTowers);
                }
                break;
            case Define.Priority.Tower:
                {
                    if(_lockTarget == null)
                        Priority = Define.Priority.ProtectedTower;
                }
                break;
            case Define.Priority.ProtectedTower:
                {
                    if(_lockTarget == null)
                    {
                        if (Managers.Object.ProtectedTowers.Count == 0 && Managers.Object.LastProtectedTower != null)
                        {
                            _lockTarget = Managers.Object.LastProtectedTower;
                            return;
                        }

                        _lockTarget = Util.GetShortestDistance(gameObject, Managers.Object.ProtectedTowers);
                    }
                }
                break;
        }

        base.Update();
    }

    public override void SetStat(Define.Unit type)
    {
        Data.UnitStatData stat = Managers.Data.UnitStatDatas[type];
        MaxHp = stat.maxHp;
        Hp = stat.hp;
        Attack = stat.attack;
        AttackSpeed = stat.attackSpeed;
        AttackRange = stat.attackRange;
        MoveSpeed = stat.moveSpeed;
        RewardGold = stat.rewardGold;
        RewardScore = stat.rewardScore;
    }

    protected override void UpdateIdle()
    {
        if(_lockTarget != null && Managers.Game.Attackable)
        {
            if(Util.GetDistance(transform.position, _lockTarget.transform.position) < AttackInterval)
            {
                _currentAttackTime += Time.deltaTime;

                if (_currentAttackTime >= AttackSpeed)
                {
                    if (_anim != null)
                        State = Define.State.Attack;
                    else
                        OnAttacked();
                }
                return;
            }
            else
            {
                State = Define.State.Move;
                return;
            }
        }

        _agent.SetDestination(transform.position);
    }

    protected override void UpdateMove()
    {
        if(_lockTarget == null)
        {
            State = Define.State.Idle;
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, AttackRange, _layer))
        {
            TowerController tc = hit.transform.GetComponent<TowerController>();

            if (tc != null)
            {
                if (tc.Type == Define.Tower.Obstacle)
                {
                    _lockTarget = tc;

                    if (Managers.Game.Attackable)
                        State = Define.State.Attack;
                    else
                        State = Define.State.Idle;

                    return;
                }
            }
        }

        if (Util.GetDistance(gameObject.transform.position, _lockTarget.transform.position) < AttackInterval)
        {
            State = Define.State.Attack;
            return;
        }

        _agent.speed = MoveSpeed * Managers.Game.MoveSpeedAmount;
        _agent.SetDestination(_lockTarget.transform.position);
        
        //Vector2 forward = new Vector2(transform.position.z, transform.position.x);
        //Vector2 steeringTarget = new Vector2(_agent.steeringTarget.z, _agent.steeringTarget.x);
        //Vector2 dir = steeringTarget - forward;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.eulerAngles = Vector3.up * angle;
    }

    protected override void UpdateAttack()
    {
        _agent.SetDestination(transform.position);

        if (_lockTarget == null || _currentAttackTime < AttackSpeed)
        {
            State = Define.State.Idle;
            return;
        }

        if(Util.GetDistance(transform.position, _lockTarget.transform.position) > AttackInterval)
        {
            State = Define.State.Move;
            return;
        }

        Vector3 dir = _lockTarget.transform.position - transform.position;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
    }

    protected virtual void OnAttacked()
    {
        _currentAttackTime = 0f;

        if (_firePos != null)
        {
            GameObject go = Managers.Resource.Instantiate($"Bullet/{Type}Bullet");
            Bullet bullet = go.GetOrAddComponent<Bullet>();
            bullet.SetInfo(this, _lockTarget);
        }
        else
        {
            _lockTarget.OnDamaged(this);
        }
    }

    public override void OnDamaged(BaseController bc)
    {
        if (Hp <= 0f)
            return;

        Hp = bc.Attack;

        if (Hp <= 0f)
        {
            Hp = 0f;
            OnDead();
            return;
        }

        State = Define.State.Hit;
        _agent.SetDestination(transform.position);

        if(Priority > Define.Priority.Illusion && Priority != Define.Priority.Tower)
        {
            Priority = Define.Priority.Tower;
            _lockTarget = bc as TowerController;
        }
    }

    protected void OnChageStateToIdle()
    {
        State = Define.State.Idle;
    }
}