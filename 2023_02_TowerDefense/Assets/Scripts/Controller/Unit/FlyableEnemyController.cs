using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyableEnemyController : EnemyController
{
    protected override void UpdateMove()
    {
        if(_lockTarget == null)
        {
            State = Define.State.Idle;
            return;
        }

        if (Util.GetDistance(transform.position, _lockTarget.transform.position) < AttackInterval)
        {
            State = Define.State.Attack;
            return;
        }

        Vector3 dir = _lockTarget.transform.position - transform.position;
        dir.y = 0f;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
        transform.position += dir.normalized * _moveSpeed * Time.deltaTime;
    }
}
