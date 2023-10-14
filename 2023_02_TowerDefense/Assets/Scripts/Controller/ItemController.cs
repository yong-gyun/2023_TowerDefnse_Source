using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public float CurrentDuration;
    public float MaxDuration;
    Define.Item _type;
    Coroutine _coroutine;

    public void OnUsed(Define.Item type)
    {
        _type = type;

        switch(_type)
        {
            case Define.Item.TowerHeal:
                {
                    foreach (TowerController tc in Managers.Object.TowerPool)
                    {
                        float value = 30f % tc.MaxHp;

                        tc.Hp += value;

                        if (tc.Hp > tc.MaxHp)
                            tc.Hp = tc.MaxHp;

                        Clear();
                    }
                }
                break;
            case Define.Item.EnemySlow:
                {
                    Managers.Game.MoveSpeedAmount /= 0.5f;
                    ForWait(10f, () => { Managers.Game.MoveSpeedAmount = 1; });
                }
                break;
            case Define.Item.Gold:
                {
                    Managers.Game.Gold = 2;
                    ForWait(10f, () => { Managers.Game.Gold = 1; });
                }
                break;
            case Define.Item.TowerAttackSpeedUp:
                {
                    Managers.Game.AttackSpeedAmount = 0.5f;
                    ForWait(10f, () => { Managers.Game.AttackSpeedAmount = 1f; });
                }
                break;
            case Define.Item.EnemyAttackStop:
                {
                    Managers.Game.Attackable = false;
                    ForWait(10f, () => { Managers.Game.Attackable = true; });
                }
                break;
        }
    }

    void ForWait(float duration, Action evt)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoForWait(duration, evt));
    }

    IEnumerator CoForWait(float duration, Action evt)
    {
        CurrentDuration = duration;
        MaxDuration = duration;

        while(CurrentDuration > 0)
        {
            CurrentDuration -= Time.deltaTime;
            yield return null;
        }

        CurrentDuration = MaxDuration;
        evt.Invoke();
        Clear();
    }

    void Clear()
    {

    }
}
