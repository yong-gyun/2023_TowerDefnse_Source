using System.Collections.Generic;
using UnityEngine;

public class TowerController : BaseController
{
    public Define.Tower Type;
    public bool IsStart { get { return _isStart; } }
    public int Size { get { return _size; } }
    protected Transform _lockTarget;
    protected List<Transform> _firePoints = new List<Transform>();
    protected bool _isStart = false;
    protected int _size;

    public virtual bool OnStart()
    {
        if (_isStart)
            return false;

        Transform firePoints = gameObject.FindChild<Transform>("FirePoints", true);

        if(firePoints != null)
        {
            for (int i = 0; i < firePoints.childCount; i++)
            {
                Transform firePoint = firePoints.GetChild(i);
                _firePoints.Add(firePoint);
            }
        }

        _isStart = true;
        return true;
    }

    protected void SetStat(Define.Tower type)
    {
        Data.TowerStatData stat = Managers.Data.TowerStatDatas[type];
        Type = type;
        MaxHp = stat.maxHp;
        Hp = stat.hp;
        Attack = stat.attack;
        AttackSpeed = stat.attack;
        AttackRange = stat.attackRange;
        _currentAttackTime = AttackSpeed;
        _size = stat.size;
    }

    protected virtual void Update()
    {
        if (_isStart == false || Type == Define.Tower.Obstacle)
            return;

        if (_lockTarget == null)
            UpdateSeraching();
        else
            UpdateAttack();
    }

    protected virtual void UpdateSeraching()
    {
        if(Managers.Object.EnemyPool.Count > 0)
            _lockTarget = Util.GetShortestDistance(gameObject, Managers.Object.EnemyPool).transform;
    }

    protected virtual void UpdateAttack()
    {
        _currentAttackTime += Time.deltaTime;

        if (_currentAttackTime >= AttackSpeed * Managers.Game.AttackSpeedAmount)
        {
            OnAttack();
            _currentAttackTime = 0f;
        }

        Vector3 dir = _lockTarget.position - transform.position;
        dir.y = 0f;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 30 * Time.deltaTime);
    }

    protected virtual void OnAttack()
    {
        if (_firePoints.Count == 0)
            return;

        foreach (Transform firePoint in _firePoints)
        {
            GameObject go = Managers.Resource.Instantiate($"Bullet/{Type}Bullet", firePoint.position, transform.rotation);
            Bullet bullet = go.GetOrAddComponent<Bullet>();
            bullet.SetInfo(this, _lockTarget.GetComponent<UnitController>());
        }
    }

    public virtual void OnDamaged(float damage)
    {
        if (Hp <= 0f)
            return;

        Hp = damage;

        if (Hp <= 0f)
        {
            Hp = 0f;
            OnDead();
        }
    }
}
