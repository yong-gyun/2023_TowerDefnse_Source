using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager
{
    public Dictionary<Define.Item, ItemController> Items = new Dictionary<Define.Item, ItemController>();

    public int[] ItemPrices = new int[5] { 50, 75, 100, 125, 150 };
    public int CurrentItemPriceIdx = 0;
    public int Gold = 1000;
    public int Score;
    public int Stage;
    public int CurrentWave = 0;
    public int MaxWave = 0;
    public float PlayTime;
    public float GoldAmount;
    public float MoveSpeedAmount;
    public float AttackSpeedAmount;
    public bool Attackable;

    public Define.Item[] CurrentHaveItmes = new Define.Item[3];

    public void Init()
    {
        Items.Add(Define.Item.TowerHeal, null);
        Items.Add(Define.Item.EnemySlow, null);
        Items.Add(Define.Item.Gold, null);
        Items.Add(Define.Item.TowerAttackSpeedUp, null);
        Items.Add(Define.Item.EnemyAttackStop, null);
        Items.Add(Define.Item.SummonPatrolUnit, null);

        GoldAmount = 1f;
        MoveSpeedAmount = 1f;
        AttackSpeedAmount = 1f;
        Attackable = true;
    }

    public Define.Item GetRandItem()
    {
        int count = CurrentHaveItmes.Where(x => x == Define.Item.Unknow).Count();

        if (count == 0)
            return Define.Item.Unknow;

        Define.Item type = Define.Item.Unknow;
        
        while (true)
        {
            int rand = Random.Range(1, 100);

            if (rand <= 15)
                type = Define.Item.TowerHeal;
            else if (rand <= 35)
                type = Define.Item.EnemySlow;
            else if (rand <= 55)
                type = Define.Item.Gold;
            else if (rand <= 70)
                type = Define.Item.TowerAttackSpeedUp;
            else if (rand <= 90)
                type = Define.Item.EnemyAttackStop;
            else
                type = Define.Item.SummonPatrolUnit;

            if (CurrentHaveItmes.Contains(type) == false)
            {
                for (int i = 0; i < CurrentHaveItmes.Length; i++)
                {
                    if(CurrentHaveItmes[i] == Define.Item.Unknow)
                    {
                        CurrentHaveItmes[i] = type;
                        return type;
                    }
                }
            }
        }
    }
}