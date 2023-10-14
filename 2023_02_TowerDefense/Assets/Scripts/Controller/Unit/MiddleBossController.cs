using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBossController : EnemyController
{
    float _rushDinstance = 5 * Define.TILE_SIZE;

    protected override void UpdateMove()
    {
        base.UpdateMove();

        if (Util.GetDistance(transform.position, _lockTarget.transform.position) < _rushDinstance)
        {
            if (Physics.Raycast(transform.position, transform.forward, LayerMask.GetMask("Block")))
                return;

            State = Define.State.Rush;
        }
    }

    protected override void UpdateRush()
    {
        float attackDistance = 2 * Define.TILE_SIZE;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, attackDistance, LayerMask.GetMask("Tower")))
        {
            _lockTarget = hit.transform.GetComponent<TowerController>();
            _lockTarget.OnDamaged(Attack * 2f);
            State = Define.State.Idle;
            return;
        }

        Vector3 dir = _lockTarget.transform.position - transform.position;
        dir.y = 0f;
        Quaternion qua = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, 20 * Time.deltaTime);
        transform.position += dir.normalized * (_moveSpeed * 2f) * Time.deltaTime;
    }
}