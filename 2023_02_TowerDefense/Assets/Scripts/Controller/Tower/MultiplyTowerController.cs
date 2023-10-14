using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiplyTowerController : TowerController
{
    [SerializeField] List<EnemyController> _targetList = new List<EnemyController>();

    protected override void UpdateSeraching()
    {
        _targetList = Managers.Object.EnemyPool.OrderBy(a => (a.transform.position - transform.position).magnitude).ToList();
        _targetList.Where(t => t != null);

        if (_targetList.Count > 0)
            _lockTarget = _targetList[0].transform;
    }

    protected override void OnAttack()
    {
        for (int i = 0; i < _targetList.Count; i++)
        {
            if (i == 5)
                break;

            GameObject go = Managers.Resource.Instantiate($"Bullet/{Type}Bullet", _firePoints[i].position, transform.rotation);
            Bullet bullet = go.GetOrAddComponent<Bullet>();
            bullet.SetInfo(this, _targetList[i]);
        }
    }
}
