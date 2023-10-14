using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class StageBossController : EnemyController
{
    List<Transform> _summonPoints = new List<Transform>();
    Define.Unit[] _summonEnemy = new Define.Unit[3] { Define.Unit.Melee, Define.Unit.RangedAttck, Define.Unit.QuickMove };

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Transform summonPoints = gameObject.FindChild<Transform>("SummonPoints", true);

        for (int i = 0; i < summonPoints.childCount; i++)
        {
            Transform point = summonPoints.GetChild(i);
            _summonPoints.Add(point);
        }

        StartCoroutine(CoSummon());
        return true;
    }

    IEnumerator CoSummon()
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < _summonPoints.Count; i++)
        {
            Transform point = _summonPoints[i];

            if (Managers.Game.Stage == 1)
            {
                EnemyController ec = Managers.Object.SpawnEnemy(_summonEnemy[i % _summonEnemy.Length]);
                ec.transform.position = point.position;
            }
            else
            {
                EnemyController ec = Managers.Object.SpawnEnemy(Define.Unit.MiddleBoss);
                ec.transform.position = point.position;
            }
        }
    }
}